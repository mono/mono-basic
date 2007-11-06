' 
' Visual Basic.Net Compiler
' Copyright (C) 2004 - 2007 Rolf Bjarne Kvinge, RKvinge@novell.com
' 
' This library is free software; you can redistribute it and/or
' modify it under the terms of the GNU Lesser General Public
' License as published by the Free Software Foundation; either
' version 2.1 of the License, or (at your option) any later version.
' 
' This library is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
' Lesser General Public License for more details.
' 
' You should have received a copy of the GNU Lesser General Public
' License along with this library; if not, write to the Free Software
' Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
' 

''' <summary>
''' ArrayElementInitializer  ::=  {  [  VariableInitializerList  ]  }
''' </summary>
''' <remarks></remarks>
Public Class ArrayElementInitializer
    Inherits ParsedObject

    Private m_VariableInitializerList As VariableInitializerList

    Private m_Elements As Generic.List(Of Integer)

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        If m_VariableInitializerList IsNot Nothing Then result = m_VariableInitializerList.ResolveTypeReferences AndAlso result

        Return result
    End Function

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal VariableInitializerList As VariableInitializerList)
        m_VariableInitializerList = VariableInitializerList
    End Sub

    Sub Init(ByVal Elements As Expression())
        m_VariableInitializerList = New VariableInitializerList(Me)
        For Each e As Expression In Elements
            Dim vi As VariableInitializer
            vi = New VariableInitializer(Me)
            vi.Init(e)
            m_VariableInitializerList.Add(vi)
        Next
    End Sub

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True
        
        If m_VariableInitializerList IsNot Nothing Then
            Dim expInfo As ExpressionResolveInfo = TryCast(Info, ExpressionResolveInfo)
            Dim elementInfo As ResolveInfo
            If expInfo IsNot Nothing Then
                Helper.Assert(expInfo.LHSType.GetElementType IsNot Nothing)
                Helper.Assert(expInfo.LHSType.IsArray)
                If expInfo.LHSType isnot nothing andalso expInfo.LHSType.GetArrayRank > 1 Then
                    Dim newArrayRank As Integer = expInfo.LHSType.GetArrayRank - 1
                    Dim elementType As System.Type = expInfo.LHSType.GetElementType.MakeArrayType(newArrayRank)
                    elementInfo = New ExpressionResolveInfo(Compiler, elementType)
                Else
                    elementInfo = New ExpressionResolveInfo(Compiler, expInfo.LHSType.GetElementType)
                End If
            Else
                Helper.StopIfDebugging(True)
                elementInfo = Info
            End If

                Helper.Assert(expInfo Is Nothing OrElse DirectCast(elementInfo, ExpressionResolveInfo).LHSType IsNot Nothing)

                result = m_VariableInitializerList.ResolveCode(elementInfo) AndAlso result
            End If
        result = SetElements() AndAlso result

        Compiler.Helper.AddCheck("Array element initializers must all have the same number of elements / ranks.")

        Return result
    End Function

    Function SetElements() As Boolean
        Dim result As Boolean = True
        m_Elements = New Generic.List(Of Integer)
        If m_VariableInitializerList IsNot Nothing Then
            If m_VariableInitializerList.List.ToArray.Length > 0 Then
                If m_VariableInitializerList.List.ToArray(0).IsArrayElementInitializer Then
                    m_Elements.AddRange(m_VariableInitializerList.List.ToArray(0).AsArrayElementInitializer.Elements)
                End If
                m_Elements.Insert(0, m_VariableInitializerList.List.ToArray.Length)
            End If
        End If
        Return result
    End Function

    ReadOnly Property Elements() As Generic.List(Of Integer)
        Get
            Return m_Elements
        End Get
    End Property

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        Dim arraytype As Type = Info.DesiredType
        Dim elementtype As Type = arraytype.GetElementType
        Dim tmpvar As LocalBuilder = Info.ILGen.DeclareLocal(Helper.GetTypeOrTypeBuilder(arraytype))
        Dim elementInfo As EmitInfo = Info.Clone(Me, True, False, elementtype)
        Dim indexInfo As EmitInfo = Info.Clone(Me, True, False, Compiler.TypeCache.System_Int32)

        'Create the array.
        ArrayCreationExpression.EmitArrayCreation(Info, arraytype, m_Elements)

        'Save it into a temporary variable.
        Emitter.EmitStoreVariable(Info, tmpvar)

        'Calculate the total number of elements.
        Dim elements As Integer = 1
        For i As Integer = 0 To m_Elements.Count - 1
            elements *= m_Elements(i)
        Next
        If m_Elements.Count = 0 AndAlso elements = 1 Then elements = 0

        'Create a list of the current indices.
        Dim indices As New Generic.List(Of Integer)
        For i As Integer = 0 To m_Elements.Count - 1
            indices.Add(0)
        Next

        'Get the set method, if it is a multidimensional array.
        Dim method As MethodInfo = Nothing
        If m_Elements.Count > 1 Then
            method = GetSetMethod(Compiler, arraytype)
        End If

        'Store every element into its index in the array.
        For i As Integer = 1 To elements
            'Load the array variable.
            Emitter.EmitLoadVariable(Info, tmpvar)
            'Load all the indices.
            For j As Integer = 0 To indices.Count - 1
                Emitter.EmitLoadI4Value(indexInfo, indices(j))
            Next
            If elementtype.IsValueType AndAlso elementtype.IsPrimitive = False AndAlso elementtype.IsEnum = False Then
                Emitter.EmitLoadElementAddress(Info, elementtype, arraytype)
            End If
            'Get the element expression.
            Dim elementExpression As Expression
            elementExpression = GetRegularInitializer(indices)
            'Generate the element expression
            result = elementExpression.GenerateCode(elementInfo) AndAlso result
            'Store the element in the arry.
            If m_Elements.Count > 1 Then
                Emitter.EmitCallVirt(elementInfo, method)
            Else
                Emitter.EmitStoreElement(elementInfo, elementtype, arraytype)
            End If
            'Increment the indices.
            For j As Integer = indices.Count - 1 To 0 Step -1
                If indices(j) + 1 = m_Elements(j) Then
                    indices(j) = 0
                Else
                    indices(j) += 1
                    Exit For
                End If
            Next
        Next

        'Load the final array onto the stack.
        Emitter.EmitLoadVariable(Info, tmpvar)

        Return result
    End Function

    Shared Function GetGetMethod(ByVal Compiler As Compiler, ByVal ArrayType As Type) As MethodInfo
        Dim result As MethodInfo
        Dim elementType As Type = ArrayType.GetElementType
        Dim ranks As Integer = ArrayType.GetArrayRank
        Dim methodtypes As Type() = Helper.CreateArray(Of Type)(Compiler.TypeCache.System_Int32, ranks)

        If Compiler.Assembly.IsDefinedHere(ArrayType) OrElse Compiler.Assembly.IsDefinedHere(elementType) Then
            ArrayType = Helper.GetTypeOrTypeBuilder(ArrayType)
            elementType = Helper.GetTypeOrTypeBuilder(elementType)
            result = Compiler.ModuleBuilder.GetArrayMethod(ArrayType, "Get", CallingConventions.HasThis Or CallingConventions.Standard, elementType, methodtypes)
        Else
            result = ArrayType.GetMethod("Get", BindingFlags.ExactBinding Or BindingFlags.Instance Or BindingFlags.Public Or BindingFlags.DeclaredOnly, Nothing, methodtypes, Nothing)
        End If

        Return result
    End Function

    Shared Function GetSetMethod(ByVal Compiler As Compiler, ByVal ArrayType As Type) As MethodInfo
        Dim result As MethodInfo
        Dim elementType As Type = ArrayType.GetElementType
        Dim ranks As Integer = ArrayType.GetArrayRank
        Dim methodtypes As Type() = Helper.CreateArray(Of Type)(Compiler.TypeCache.System_Int32, ranks + 1)

        methodtypes(ranks) = elementType

        If Compiler.Assembly.IsDefinedHere(ArrayType) OrElse Compiler.Assembly.IsDefinedHere(elementType) Then
            ArrayType = Helper.GetTypeOrTypeBuilder(ArrayType)
            elementType = Helper.GetTypeOrTypeBuilder(elementType)
            result = Compiler.ModuleBuilder.GetArrayMethod(ArrayType, "Set", CallingConventions.HasThis Or CallingConventions.Standard, Nothing, methodtypes)
        Else
            result = ArrayType.GetMethod("Set", BindingFlags.ExactBinding Or BindingFlags.Instance Or BindingFlags.Public Or BindingFlags.DeclaredOnly, Nothing, methodtypes, Nothing)
        End If

        Return result
    End Function

    Private Function GetRegularInitializer(ByVal indices As Generic.List(Of Integer)) As Expression
        Dim ai As ArrayElementInitializer = Me
        Dim result As Expression

        Dim index As Integer
        For i As Integer = 0 To indices.Count - 2
            index = indices(i)
            Helper.Assert(ai.m_VariableInitializerList.List.ToArray.Length > index)
            Helper.Assert(ai.m_VariableInitializerList.List.ToArray(index).IsArrayElementInitializer)
            ai = ai.m_VariableInitializerList.List.ToArray(index).AsArrayElementInitializer
        Next
        index = indices(indices.Count - 1)
        Helper.Assert(ai.m_VariableInitializerList.List.ToArray.Length > index)
        Helper.Assert(ai.m_VariableInitializerList.List.ToArray(index).IsRegularInitializer)
        result = ai.m_VariableInitializerList.List.ToArray(index).AsRegularInitializer

        Return result
    End Function

    Sub AddInitializer(ByVal Expression As Expression)
        Dim init As New VariableInitializer(Me)
        init.Init(Expression)
        m_VariableInitializerList.Add(init)
        If SetElements() = False Then Throw New InternalException(Me)
    End Sub

    ReadOnly Property Initializers() As VariableInitializerList
        Get
            Return m_VariableInitializerList
        End Get
    End Property

    Shared Function CanBeMe(ByVal tm As tm) As Boolean
        Return tm.CurrentToken.Equals(KS.LBrace)
    End Function
End Class
