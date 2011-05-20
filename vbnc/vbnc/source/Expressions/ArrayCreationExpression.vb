' 
' Visual Basic.Net Compiler
' Copyright (C) 2004 - 2010 Rolf Bjarne Kvinge, RKvinge@novell.com
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
''' ArrayCreationExpression    ::= "New" NonArrayTypeName ArraySizeInitializationModifier ArrayElementInitializer
''' 
''' LAMESPEC? I think the following should be used:
''' ArrayCreationExpression    ::= "New" NonArrayTypeName ArrayNameModifier ArrayElementInitializer
''' 
''' A New expression is classified as a value and the result is the new instance of the type.
''' </summary>
''' <remarks></remarks>
Public Class ArrayCreationExpression
    Inherits Expression

    Private m_NonArrayTypeName As NonArrayTypeName
    Private m_ArrayNameModifier As ArrayNameModifier
    Private m_ArrayElementInitializer As ArrayElementInitializer

    Private m_ExpressionType As Mono.Cecil.TypeReference

    ReadOnly Property ArrayElementInitalizer() As ArrayElementInitializer
        Get
            Return m_ArrayElementInitializer
        End Get
    End Property

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        If m_NonArrayTypeName IsNot Nothing Then result = m_NonArrayTypeName.ResolveTypeReferences AndAlso result
        If m_ArrayNameModifier IsNot Nothing Then result = m_ArrayNameModifier.ResolveTypeReferences AndAlso result
        If m_ArrayElementInitializer IsNot Nothing Then result = m_ArrayElementInitializer.ResolveTypeReferences AndAlso result

        Return result
    End Function

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal NonArrayTypeName As NonArrayTypeName, ByVal ArrayNameModifier As ArrayNameModifier, ByVal ArrayElementInitializer As ArrayElementInitializer)
        m_NonArrayTypeName = NonArrayTypeName
        m_ArrayNameModifier = ArrayNameModifier
        m_ArrayElementInitializer = ArrayElementInitializer
    End Sub

    Sub Init(ByVal ArrayType As Mono.Cecil.TypeReference, ByVal InitializerElements As Expression())
        m_ExpressionType = ArrayType
        m_ArrayElementInitializer = New ArrayElementInitializer(Me)
        m_ArrayElementInitializer.Init(InitializerElements)
    End Sub

    Sub Init(ByVal ArrayType As Mono.Cecil.TypeReference, ByVal ArrayBounds() As Expression, ByVal InitializerElements As Expression())
        m_ExpressionType = ArrayType

        If ArrayBounds IsNot Nothing Then
            m_ArrayNameModifier = New ArrayNameModifier(Me)
            Dim newSizes As New ArraySizeInitializationModifier(Me)
            Dim bounds As New BoundList(newSizes)
            bounds.Init(ArrayBounds)
            newSizes.Init(bounds, Nothing)
            m_ArrayNameModifier.Init(newSizes)
        End If

        If InitializerElements IsNot Nothing Then
            m_ArrayElementInitializer = New ArrayElementInitializer(Me)
            m_ArrayElementInitializer.Init(InitializerElements)
        End If
    End Sub

    Public Shared Sub EmitArrayCreation(ByVal Parent As ParsedObject, ByVal Info As EmitInfo, ByVal ArrayType As Mono.Cecil.TypeReference, ByVal asim As ArraySizeInitializationModifier)
        Dim Ranks As Integer = asim.BoundList.Expressions.Length
        For i As Integer = 0 To Ranks - 1
            Dim litexp As New ConstantExpression(Parent, 1, Parent.Compiler.TypeCache.System_Int32)
            Dim exp As Expression

            exp = New BinaryAddExpression(Parent, asim.BoundList.Expressions(i), litexp)
            exp = New CIntExpression(Parent, exp)

            If exp.ResolveExpression(ResolveInfo.Default(Info.Compiler)) = False Then Throw New InternalException(Parent)
            If exp.GenerateCode(Info.Clone(Parent, True)) = False Then Throw New InternalException(Parent)

            'Emitter.EmitConversion(exp.ExpressionType, Parent.Compiler.TypeCache.System_Int32, Info)
        Next
        EmitArrayConstructor(Info, ArrayType, Ranks)
    End Sub


    ''' <summary>
    ''' Creates an array of the specified type and number of elements (and ranks)
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <param name="arraytype"></param>
    ''' <param name="Elements"></param>
    ''' <remarks></remarks>
    Public Shared Sub EmitArrayCreation(ByVal Info As EmitInfo, ByVal ArrayType As Mono.Cecil.TypeReference, ByVal Elements As Generic.List(Of Integer))
        Dim count As Integer = If(Elements Is Nothing, 0, Elements.Count)
        If count = 0 Then
            Emitter.EmitLoadI4Value(Info, 0)
        Else
            For i As Integer = 0 To Elements.Count - 1
                Emitter.EmitLoadI4Value(Info, Elements(i))
            Next
        End If
        EmitArrayConstructor(Info, ArrayType, count)
    End Sub

    ''' <summary>
    ''' Emits the array constructor.
    ''' The number of elements for each rank must be emitted already.
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <param name="ArrayType"></param>
    ''' <param name="Ranks"></param>
    ''' <remarks></remarks>
    Public Shared Sub EmitArrayConstructor(ByVal Info As EmitInfo, ByVal ArrayType As Mono.Cecil.TypeReference, ByVal Ranks As Integer)
        If Ranks <= 1 Then
            Emitter.EmitNewArr(Info, CecilHelper.GetElementType(ArrayType))
        Else
            Dim minfo As Mono.Cecil.MethodReference
            minfo = New Mono.Cecil.MethodReference(".ctor", ArrayType, Info.Compiler.TypeCache.System_Void, True, False, Mono.Cecil.MethodCallingConvention.Default)
            For i As Integer = 1 To Ranks
                minfo.Parameters.Add(New Mono.Cecil.ParameterDefinition(Info.Compiler.TypeCache.System_Int32))
            Next
            Emitter.EmitNew(Info, minfo)
        End If
    End Sub

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        Dim ArrayType As Mono.Cecil.TypeReference = Helper.GetTypeOrTypeBuilder(Compiler, ExpressionType())
        Dim tmpVar As Mono.Cecil.Cil.VariableDefinition = Emitter.DeclareLocal(Info, ArrayType)

        If m_ArrayNameModifier Is Nothing OrElse m_ArrayNameModifier.IsArrayTypeModifiers Then
            EmitArrayCreation(Info, ArrayType, m_ArrayElementInitializer.Elements)
        ElseIf m_ArrayNameModifier.IsArraySizeInitializationModifier Then
            EmitArrayCreation(Me, Info, ArrayType, m_ArrayNameModifier.AsArraySizeInitializationModifier)
        Else
            Throw New InternalException(Me)
        End If

        Emitter.EmitStoreVariable(Info, tmpVar)

        If m_ArrayElementInitializer IsNot Nothing AndAlso m_ArrayElementInitializer.Initializers IsNot Nothing Then
            With m_ArrayElementInitializer.Initializers
                Dim indices As New Generic.List(Of Integer)
                For i As Integer = 0 To .List.ToArray.GetUpperBound(0)
                    indices.Add(i)
                    result = EmitElementInitializer(Info, .List.ToArray()(i), 1, i, tmpVar, ArrayType, indices) AndAlso result
                    indices.RemoveAt(indices.Count - 1)
                Next
            End With
        End If

        Emitter.EmitLoadVariable(Info, tmpVar)

        Return result
    End Function

    Private Function EmitElementInitializer(ByVal Info As EmitInfo, ByVal Initializer As VariableInitializer, ByVal CurrentDepth As Integer, ByVal ElementIndex As Integer, ByVal ArrayVariable As Mono.Cecil.Cil.VariableDefinition, ByVal ArrayType As Mono.Cecil.TypeReference, ByVal Indices As Generic.List(Of Integer)) As Boolean
        Dim result As Boolean = True
        Dim vi As VariableInitializer = Initializer
        Dim elementType As Mono.Cecil.TypeReference = CecilHelper.GetElementType(ArrayType)

        If vi.IsRegularInitializer Then
            Emitter.EmitLoadVariable(Info, ArrayVariable)
            For i As Integer = 0 To Indices.Count - 1
                Emitter.EmitLoadValue(Info.Clone(Me, True, False, Compiler.TypeCache.System_Int32), Indices(i))
            Next
            If CecilHelper.IsValueType(elementType) AndAlso CecilHelper.IsPrimitive(Compiler, elementType) = False AndAlso Helper.IsEnum(Compiler, elementType) = False Then
                Emitter.EmitLoadElementAddress(Info, elementType, ArrayType)
            End If

            result = vi.AsRegularInitializer.GenerateCode(Info.Clone(Me, True, False, elementType)) AndAlso result
            If CurrentDepth = 1 Then
                Emitter.EmitStoreElement(Info, elementType, ArrayType)
            Else
                Dim setmethod As Mono.Cecil.MethodReference
                setmethod = New Mono.Cecil.MethodReference("Set", ArrayType, Compiler.TypeCache.System_Void, True, False, Mono.Cecil.MethodCallingConvention.Default)
                For i As Integer = 0 To CurrentDepth - 1
                    setmethod.Parameters.Add(New Mono.Cecil.ParameterDefinition(Compiler.TypeCache.System_Int32))
                Next
                setmethod.Parameters.Add(New Mono.Cecil.ParameterDefinition(CecilHelper.GetElementType(ArrayType)))
                'setmethod = CecilHelper.FindDefinition(ArrayType).Methods.GetMethod("Set", settypes)
                Emitter.EmitCallOrCallVirt(Info, setmethod)
            End If
        ElseIf vi.IsArrayElementInitializer Then
            For i As Integer = 0 To vi.AsArrayElementInitializer.Initializers.List.ToArray.GetUpperBound(0)
                Indices.Add(i)
                result = EmitElementInitializer(Info, vi.AsArrayElementInitializer.Initializers.List.ToArray()(i), CurrentDepth + 1, i, ArrayVariable, ArrayType, Indices) AndAlso result
                Indices.RemoveAt(Indices.Count - 1)
            Next
        Else
            Throw New InternalException(Me)
        End If

        Return result
    End Function

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        If m_ExpressionType Is Nothing Then
            result = m_NonArrayTypeName.ResolveTypeReferences AndAlso result
            If m_ArrayNameModifier IsNot Nothing Then result = m_ArrayNameModifier.ResolveCode(Info) AndAlso result

            Dim tmptp As Mono.Cecil.TypeReference = m_NonArrayTypeName.ResolvedType
            If m_ArrayNameModifier.IsArraySizeInitializationModifier Then
                tmptp = m_ArrayNameModifier.AsArraySizeInitializationModifier.CreateArrayType(tmptp)
            ElseIf m_ArrayNameModifier.IsArrayTypeModifiers Then
                tmptp = m_ArrayNameModifier.AsArrayTypeModifiers.CreateArrayType(tmptp)
            Else
                Throw New InternalException(Me)
            End If
            m_ExpressionType = tmptp
        End If

        If m_ArrayElementInitializer IsNot Nothing Then
            Dim elementInfo As New ExpressionResolveInfo(Compiler, m_ExpressionType)
            result = m_ArrayElementInitializer.ResolveCode(elementInfo) AndAlso result
        End If

        If m_ArrayNameModifier IsNot Nothing Then
            Dim elementInfo As New ExpressionResolveInfo(Compiler, m_ExpressionType)
            result = m_ArrayNameModifier.ResolveCode(elementInfo) AndAlso result
        End If

        Classification = New ValueClassification(Me, m_ExpressionType)

        Return result
    End Function

    Overrides ReadOnly Property ExpressionType() As Mono.Cecil.TypeReference
        Get
            If Me.IsResolved Then
                Return m_ExpressionType
            ElseIf m_ExpressionType IsNot Nothing Then
                Return m_ExpressionType
            Else
                Throw New InternalException(Me)
            End If
        End Get
    End Property
End Class
