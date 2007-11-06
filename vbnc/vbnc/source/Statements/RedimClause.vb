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
''' RedimClause  ::=  Expression  ArraySizeInitializationModifier
''' 
''' Each clause in the statement must be classified as a variable or a property access 
''' whose type is an array type or Object, and be followed by a list of array bounds. 
''' The number of the bounds must be consistent with the type of the variable;
''' any number of bounds is allowed for Object
''' At run time, an array is instantiated for each expression from left to right with
''' the specified bounds and then assigned to the variable or property. If the variable type is Object, 
''' the number of dimensions is the number of dimensions specified, and the array element type is Object. 
''' If the given number of dimensions is incompatible with the variable or property at run time,
''' a System.InvalidCastException will be thrown
''' 
''' If the Preserve keyword is specified, then the expressions must also be classifiable as a value,
''' and the new size for each dimension except for the rightmost one must be the same as the size 
''' of the existing array. 
''' </summary>
''' <remarks></remarks>
Public Class RedimClause
    Inherits ParsedObject

    Private m_Expression As Expression
    'Private m_ArraySizeInitModifier As ArraySizeInitializationModifier
    Private m_ArgumentList As ArgumentList

    Private m_Rank As Integer
    Private m_IsObjectArray As Boolean
    Private m_ArrayType As Type
    Private m_ElementType As Type

    Private m_AssignStatement As AssignmentStatement

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        If m_Expression IsNot Nothing Then result = m_Expression.ResolveTypeReferences AndAlso result
        If m_ArgumentList IsNot Nothing Then result = m_ArgumentList.ResolveTypeReferences AndAlso result

        Return result
    End Function

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal Expression As Expression, ByVal ArgumentList As ArgumentList) ', ByVal ArraySizeInitModifier As ArraySizeInitializationModifier)
        m_Expression = Expression
        ' m_ArraySizeInitModifier = ArraySizeInitModifier
        m_ArgumentList = ArgumentList
    End Sub

    Private Function GenerateCodeForNewArray(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True
        Dim rankTypes() As Type = Helper.CreateArray(Of Type)(Compiler.TypeCache.System_Int32, m_Rank)

        Helper.Assert(m_Rank >= 1)

        If m_Rank = 1 Then
            result = m_ArgumentList.GenerateCode(Info.Clone(Me, True, False, Compiler.TypeCache.System_Int32), rankTypes) AndAlso result
            Emitter.EmitNewArr(Info, m_ElementType)
        Else
            'Dim ctor As ConstructorInfo
            'ctor = m_ArrayType.GetConstructor(BindingFlags.Instance Or BindingFlags.Public, Nothing, Nothing, rankTypes, Nothing)
            'ctor = Compiler.TypeCache.Array.GetConstructor(BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.Public, Nothing, Type.EmptyTypes, Nothing)
            ''ctor = Compiler.TypeCache.Array.GetConstructor(BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.Public, Nothing, rankTypes, Nothing)
            'Helper.Assert(ctor IsNot Nothing)
            'Emitter.EmitNew(Info, ctor)

            Dim ElementType As Type
            Dim ArrayType As Type

            ElementType = Helper.GetTypeOrTypeBuilder(m_ArrayType.GetElementType)
            ArrayType = Helper.GetTypeOrTypeBuilder(m_ArrayType)

            Emitter.EmitLoadToken(Info, ElementType)
            Emitter.EmitCallOrCallVirt(Info, Compiler.TypeCache.System_Type__GetTypeFromHandle_RuntimeTypeHandle)

            result = Helper.EmitIntegerArray(Info, m_ArgumentList) AndAlso result
            Emitter.EmitCall(Info, Compiler.TypeCache.System_Array__CreateInstance)

            Emitter.EmitCastClass(Info, Compiler.TypeCache.System_Array, ArrayType)
        End If
        Return result
    End Function

    Private Function GenerateCodeForPreserve(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True
        result = m_Expression.GenerateCode(Info.Clone(Me, True, False, m_ArrayType)) AndAlso result
        Emitter.EmitCastClass(Info, m_Expression.ExpressionType, Compiler.TypeCache.System_Array)
        result = GenerateCodeForNewArray(Info) AndAlso result
        Emitter.EmitCall(Info, Compiler.TypeCache.MS_VB_CS_Utils__CopyArray_Array_Array)
        Emitter.EmitCastClass(Info, Compiler.TypeCache.System_Array, m_ArrayType)
        Return result
    End Function

    ReadOnly Property IsPreserve() As Boolean
        Get
            Return Me.FindFirstParent(Of ReDimStatement).IsPreserve
        End Get
    End Property

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True
        Dim preserve As Boolean = Me.IsPreserve

        Dim exp As CompilerGeneratedExpression
        If preserve Then
            exp = New CompilerGeneratedExpression(Me, New CompilerGeneratedExpression.GenerateCodeDelegate(AddressOf GenerateCodeForPreserve), m_ArrayType)
            result = m_Expression.GenerateCode(Info.Clone(Me, exp)) AndAlso result
        Else
            result = m_AssignStatement.GenerateCode(Info) AndAlso result
        End If

        Return result
    End Function

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = m_Expression.ResolveExpression(Info) AndAlso result
        result = m_ArgumentList.ResolveCode(Info) AndAlso result

        If m_Expression.ExpressionType.IsByRef Then
            m_Expression = m_Expression.DereferenceByRef
        End If

        m_ArrayType = m_Expression.ExpressionType
        m_IsObjectArray = Helper.CompareType(Compiler.TypeCache.System_Object, m_ArrayType)

        If m_IsObjectArray Then
            m_Rank = m_ArgumentList.Count
            m_ElementType = Compiler.TypeCache.System_Object
            If m_Rank = 1 Then
                m_ArrayType = m_ElementType.MakeArrayType()
            Else
                m_ArrayType = m_ElementType.MakeArrayType(m_Rank)
            End If
        ElseIf m_ArrayType.IsArray = False Then
            Return Helper.AddError(Me)
        Else
            m_Rank = m_ArrayType.GetArrayRank
            m_ElementType = m_ArrayType.GetElementType()
            If m_ArgumentList.Count <> m_Rank Then
                Return Helper.AddError(Me)
            End If
        End If

        If Me.IsPreserve Then

            For i As Integer = 0 To m_ArgumentList.Count - 1
                Dim arg As Argument = m_ArgumentList(i)
                Dim add As New ConstantExpression(Me, 1, Compiler.TypeCache.System_Int32)
                arg.Expression = New BinaryAddExpression(Me, arg.Expression, add)
                result = arg.Expression.ResolveExpression(Info) AndAlso result
            Next
        Else
            Dim assign As New AssignmentStatement(Me)
            Dim arr As New ArrayCreationExpression(Me)
            Dim exps() As Expression

            exps = Helper.ArgumentsToExpressions(m_ArgumentList.Arguments)
            arr.Init(m_Expression.ExpressionType, exps, Nothing)

            assign.Init(m_Expression, arr)
            result = assign.ResolveStatement(Info) AndAlso result
            m_AssignStatement = assign
        End If

        If m_Expression.Classification.IsPropertyGroupClassification Then
            m_Expression = m_Expression.ReclassifyToPropertyAccessExpression()
            result = m_Expression.ResolveExpression(Info) AndAlso result
        ElseIf m_Expression.Classification.IsVariableClassification Then
        ElseIf m_Expression.Classification.IsPropertyAccessClassification Then
        Else
            Return Helper.AddError(Me, "Redim clause must be classifiable as a property access or variable.")
        End If
        If IsPreserve Then
            If m_Expression.Classification.CanBeValueClassification = False Then
                Return Helper.AddError(Me, "Redim Preserve clause must be classifiable as a value.")
            End If
        End If

        Return result
    End Function

#If DEBUG Then
    Public Sub Dump(ByVal Dumper As IndentedTextWriter)
        m_Expression.Dump(Dumper)
        Dumper.Write(" ")
        'm_ArraySizeInitModifier.Dump()
    End Sub
#End If
End Class
