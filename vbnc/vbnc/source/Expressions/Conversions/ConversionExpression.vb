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

Public MustInherit Class ConversionExpression
    Inherits Expression

    Private m_Expression As Expression
    Public ConversionMethod As MethodReference
    Public IsExplicit As Boolean

    Public Overrides ReadOnly Property AsString() As String
        Get
            Return "CType (" & m_Expression.AsString & ", " & ExpressionType.FullName & ")"
        End Get
    End Property

    Shared Function GetTypeConversion(ByVal Parent As ParsedObject, ByVal fromExpr As Expression, ByVal DestinationType As Mono.Cecil.TypeReference) As Expression

        If Helper.CompareType(fromExpr.ExpressionType, DestinationType) Then
            Return fromExpr
        End If

        Select Case Helper.GetTypeCode(Parent.Compiler, DestinationType)
            Case TypeCode.Boolean
                Return New CBoolExpression(Parent, fromExpr)
            Case TypeCode.Byte
                Return New CByteExpression(Parent, fromExpr)
            Case TypeCode.Char
                Return New CCharExpression(Parent, fromExpr)
            Case TypeCode.DateTime
                Return New CDateExpression(Parent, fromExpr)
            Case TypeCode.Decimal
                Return New CDecExpression(Parent, fromExpr)
            Case TypeCode.Double
                Return New CDblExpression(Parent, fromExpr)
            Case TypeCode.Int16
                Return New CShortExpression(Parent, fromExpr)
            Case TypeCode.Int32
                Return New CIntExpression(Parent, fromExpr)
            Case TypeCode.Int64
                Return New CLngExpression(Parent, fromExpr)
            Case TypeCode.SByte
                Return New CSByteExpression(Parent, fromExpr)
            Case TypeCode.Single
                Return New CSngExpression(Parent, fromExpr)
            Case TypeCode.String
                Return New CStrExpression(Parent, fromExpr)
            Case TypeCode.UInt16
                Return New CUShortExpression(Parent, fromExpr)
            Case TypeCode.UInt32
                Return New CUIntExpression(Parent, fromExpr)
            Case TypeCode.UInt64
                Return New CULngExpression(Parent, fromExpr)
            Case Else
                If CecilHelper.IsByRef(DestinationType) AndAlso CecilHelper.IsByRef(fromExpr.ExpressionType) = False Then
                    Dim elementType As Mono.Cecil.TypeReference = CecilHelper.GetElementType(DestinationType)
                    Dim result As Boolean = True
                    Dim tmp As Expression
                    tmp = GetTypeConversion(Parent, fromExpr, elementType)
                    result = tmp.ResolveExpression(ResolveInfo.Default(Parent.Compiler)) AndAlso result
                    tmp = New GetRefExpression(Parent, tmp)
                    result = tmp.ResolveExpression(ResolveInfo.Default(Parent.Compiler)) AndAlso result
                    If result = False Then Throw New InternalException
                    Return tmp
                Else
                    Return New CTypeExpression(Parent, fromExpr, DestinationType)
                End If
        End Select
    End Function

    Public Overrides Function ResolveTypeReferences() As Boolean
        Return m_Expression.ResolveTypeReferences
    End Function

    ReadOnly Property Expression() As Expression
        Get
            Return m_Expression
        End Get
    End Property

    Sub New(ByVal Parent As ParsedObject, ByVal Expression As Expression)
        MyBase.New(Parent)
        m_Expression = Expression
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal IsExplicit As Boolean)
        MyBase.New(Parent)
        Me.IsExplicit = IsExplicit
    End Sub

    Sub Init(ByVal Expression As Expression)
        m_Expression = Expression
    End Sub

    Public Overrides ReadOnly Property IsConstant() As Boolean
        Get
            If m_Expression.IsConstant = False Then
                Return False
            Else
                Dim value As Object
                value = m_Expression.ConstantValue
                Dim result As Object = Nothing
                If Compiler.TypeResolution.CheckNumericRange(value, result, ExpressionType) Then
                    Return True
                Else
                    Return False
                End If
            End If
        End Get
    End Property

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        If m_Expression.IsResolved = False Then
            result = m_Expression.ResolveExpression(Info) AndAlso result
        End If

        If result = False Then Return result

        result = Helper.VerifyValueClassification(m_Expression, Info) AndAlso result
        Classification = New ValueClassification(Me)

        Return result
    End Function

    Public Shared Function SelectNarrowingConversionOperator(ByVal Compiler As Compiler, ByVal Type As TypeReference, ByVal ReturnType As TypeReference, ByRef ConversionMethod As MethodReference) As Boolean
        Dim methods As Generic.List(Of MethodReference)

        methods = TypeResolution.GetNarrowingConversionOperators(Compiler, Type, ReturnType)
        If methods Is Nothing OrElse methods.Count <> 1 Then Return False
        ConversionMethod = methods(0)
        Return True
    End Function

    Protected Shared Function ValidateForNullable(ByVal Info As ResolveInfo, ByVal Conversion As ConversionExpression, ByRef expTypeCode As TypeCode, ByRef expType As TypeReference) As Boolean
        Dim result As Boolean = True
        Dim ConversionMethod As MethodReference = Nothing

        expType = Conversion.Expression.ExpressionType

        If CecilHelper.IsNullable(expType) Then
            If SelectNarrowingConversionOperator(Info.Compiler, expType, Conversion.ExpressionType, ConversionMethod) = False Then
                Return Info.Compiler.Report.ShowMessage(Messages.VBNC99997, Conversion.Expression.Location)
            End If
            expType = CecilHelper.GetNulledType(expType)
            Conversion.ConversionMethod = ConversionMethod
        End If

        expTypeCode = Helper.GetTypeCode(Info.Compiler, expType)

        Return result
    End Function

    Protected Shared Function GenerateCodeForExpression(ByVal Conversion As ConversionExpression, ByVal Info As EmitInfo, ByRef expTypeCode As TypeCode, ByRef expType As TypeReference) As Boolean
        Dim result As Boolean = True
        Dim Expression As Expression = Conversion.Expression

        expType = Expression.ExpressionType

        result = Expression.Classification.GenerateCode(Info.Clone(Expression, expType)) AndAlso result

        If Conversion.ConversionMethod IsNot Nothing Then
            Emitter.EmitCall(Info, Conversion.ConversionMethod)
            expType = Conversion.ConversionMethod.ReturnType
        End If

        expTypeCode = Helper.GetTypeCode(Info.Compiler, expType)

        Return result
    End Function

    Public Function FindUserDefinedConversionOperator(Optional ByVal ShowNoOperatorsError As Boolean = True) As Boolean
        Dim result As Boolean = True
        Dim expType As TypeReference = Expression.ExpressionType
        Dim destinationType As TypeReference = Me.ExpressionType
        Dim ops As Generic.List(Of MethodReference)
        Dim isNarrowing As Boolean

        ops = Helper.GetWideningConversionOperators(Compiler, expType, destinationType)

        If ops Is Nothing OrElse ops.Count = 0 Then
            ops = Helper.GetNarrowingConversionOperators(Compiler, expType, destinationType)
            isNarrowing = True
        End If

        If ops Is Nothing OrElse ops.Count = 0 Then
            If ShowNoOperatorsError = False Then Return True
            If CecilHelper.IsNullable(expType) AndAlso CecilHelper.IsNullable(destinationType) AndAlso Compiler.TypeResolver.IsExplicitlyConvertible(Compiler, Helper.GetTypeCode(Compiler, CecilHelper.GetNulledType(expType)), Helper.GetTypeCode(Compiler, CecilHelper.GetNulledType(destinationType))) Then
                Return Compiler.Report.ShowMessage(Messages.VBNC30512, Expression.Location, Helper.ToString(Me, expType), Helper.ToString(Me, ExpressionType))
            End If
            Return Compiler.Report.ShowMessage(Messages.VBNC30311, Expression.Location, Helper.ToString(Me, expType), Helper.ToString(Me, ExpressionType))
        End If

        If ops.Count > 1 Then Return Compiler.Report.ShowMessage(Messages.VBNC30311, Me.Location, Helper.ToString(Me, expType), Helper.ToString(Me, ExpressionType))

        If isNarrowing AndAlso IsExplicit = False AndAlso Location.File(Compiler).IsOptionStrictOn Then
            result = Compiler.Report.ShowMessage(Messages.VBNC30512, Me.Location, Helper.ToString(Me, expType), Helper.ToString(Me, ExpressionType)) AndAlso result
        End If

        ConversionMethod = ops(0)

        Return result
    End Function

End Class
