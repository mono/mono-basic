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

    Public Function ConvertToByte(ByRef result As Object, ByVal ShowError As Boolean) As Boolean
        Dim tpCode As TypeCode

        tpCode = Helper.GetTypeCode(Compiler, CecilHelper.GetType(Compiler, result))

        Select Case tpCode
            Case TypeCode.Boolean, TypeCode.Byte
                result = CByte(result) 'No range checking needed.
            Case TypeCode.SByte, TypeCode.Int16, TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt32, TypeCode.UInt64, TypeCode.Int64, TypeCode.Single, TypeCode.Double, TypeCode.Decimal
                If Compiler.TypeResolution.CheckNumericRange(result, result, ExpressionType) = False Then
                    If ShowError Then Compiler.Report.ShowMessage(Messages.VBNC30439, Location, Helper.ToString(Expression, ExpressionType))
                    Return False
                End If
            Case TypeCode.DBNull
                result = CByte(0)
            Case Else
                If ShowError Then Compiler.Report.ShowMessage(Messages.VBNC30060, Location, result.ToString, Helper.ToString(Expression, ExpressionType))
                Return False
        End Select

        Return True
    End Function

    Public Function ConvertToSByte(ByRef result As Object, ByVal ShowError As Boolean) As Boolean
        Dim tpCode As TypeCode

        tpCode = Helper.GetTypeCode(Compiler, CecilHelper.GetType(Compiler, result))

        Select Case tpCode
            Case TypeCode.SByte
                result = CSByte(result) 'No range checking needed.
            Case TypeCode.Boolean, TypeCode.Byte, TypeCode.Int16, TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt32, TypeCode.UInt64, TypeCode.Int64, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.DBNull
                If Compiler.TypeResolution.CheckNumericRange(result, result, ExpressionType) = False Then
                    If ShowError Then Compiler.Report.ShowMessage(Messages.VBNC30439, Location, Helper.ToString(Expression, ExpressionType))
                    Return False
                End If
            Case Else
                If ShowError Then Compiler.Report.ShowMessage(Messages.VBNC30060, Location, result.ToString, Helper.ToString(Expression, ExpressionType))
                Return False
        End Select

        Return True
    End Function

    Public Function ConvertToShort(ByRef result As Object, ByVal ShowError As Boolean) As Boolean
        Dim tpCode As TypeCode

        tpCode = Helper.GetTypeCode(Compiler, CecilHelper.GetType(Compiler, result))

        Select Case tpCode
            Case TypeCode.Boolean, TypeCode.SByte, TypeCode.Byte, TypeCode.Int16
                result = CShort(result) 'No range checking needed.
            Case TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt32, TypeCode.UInt64, TypeCode.Int64, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.DBNull
                If Compiler.TypeResolution.CheckNumericRange(result, result, ExpressionType) = False Then
                    If ShowError Then Compiler.Report.ShowMessage(Messages.VBNC30439, Location, Helper.ToString(Expression, ExpressionType))
                    Return False
                End If
            Case Else
                If ShowError Then Compiler.Report.ShowMessage(Messages.VBNC30060, Location, result.ToString, Helper.ToString(Expression, ExpressionType))
                Return False
        End Select

        Return True
    End Function

    Public Function ConvertToUShort(ByRef result As Object, ByVal ShowError As Boolean) As Boolean
        Dim tpCode As TypeCode

        tpCode = Helper.GetTypeCode(Compiler, CecilHelper.GetType(Compiler, result))

        Select Case tpCode
            Case TypeCode.Boolean, TypeCode.Byte, TypeCode.UInt16
                result = CUShort(result) 'No range checking needed.
            Case TypeCode.Int16, TypeCode.Int32, TypeCode.SByte, TypeCode.UInt32, TypeCode.UInt64, TypeCode.Int64, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.DBNull
                If Compiler.TypeResolution.CheckNumericRange(result, result, ExpressionType) = False Then
                    If ShowError Then Compiler.Report.ShowMessage(Messages.VBNC30439, Location, Helper.ToString(Expression, ExpressionType))
                    Return False
                End If
            Case Else
                If ShowError Then Compiler.Report.ShowMessage(Messages.VBNC30060, Location, result.ToString, Helper.ToString(Expression, ExpressionType))
                Return False
        End Select

        Return True
    End Function

    Public Function ConvertToInt32(ByRef originalValue As Object, ByVal ShowError As Boolean) As Boolean
        Dim tpCode As TypeCode

        tpCode = Helper.GetTypeCode(Compiler, CecilHelper.GetType(Compiler, originalValue))
        Select Case tpCode
            Case TypeCode.Boolean, TypeCode.SByte, TypeCode.Byte, TypeCode.Int16, TypeCode.UInt16, TypeCode.Int32
                originalValue = CInt(originalValue) 'No range checking needed.
            Case TypeCode.UInt32, TypeCode.UInt64, TypeCode.Int64, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.DBNull
                If Compiler.TypeResolution.CheckNumericRange(originalValue, originalValue, ExpressionType) = False Then
                    If ShowError Then Compiler.Report.ShowMessage(Messages.VBNC30439, Location, Helper.ToString(Expression, ExpressionType))
                    Return False
                End If
            Case Else
                If ShowError Then Compiler.Report.ShowMessage(Messages.VBNC30060, Location, originalValue.ToString, Helper.ToString(Expression, ExpressionType))
                Return False
        End Select

        Return True
    End Function

    Public Function ConvertToUInt32(ByRef result As Object, ByVal ShowError As Boolean) As Boolean
        Dim tpCode As TypeCode

        tpCode = Helper.GetTypeCode(Compiler, CecilHelper.GetType(Compiler, result))
        Select Case tpCode
            Case TypeCode.Boolean, TypeCode.Byte, TypeCode.UInt16, TypeCode.UInt32
                result = CUInt(result) 'No range checking needed.
            Case TypeCode.SByte, TypeCode.Int16, TypeCode.Int32, TypeCode.UInt64, TypeCode.Int64, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.DBNull
                If Compiler.TypeResolution.CheckNumericRange(result, result, ExpressionType) = False Then
                    If ShowError Then Compiler.Report.ShowMessage(Messages.VBNC30439, Location, Helper.ToString(Expression, ExpressionType))
                    Return False
                End If
            Case Else
                If ShowError Then Compiler.Report.ShowMessage(Messages.VBNC30060, Location, result.ToString, Helper.ToString(Expression, ExpressionType))
                Return False
        End Select

        Return True
    End Function

    Public Function ConvertToLong(ByRef result As Object, ByVal ShowError As Boolean) As Boolean
        Dim tpCode As TypeCode

        tpCode = Helper.GetTypeCode(Compiler, CecilHelper.GetType(Compiler, result))

        Select Case tpCode
            Case TypeCode.Boolean, TypeCode.SByte, TypeCode.Byte, TypeCode.Int16, TypeCode.UInt16, TypeCode.Int32, TypeCode.Int64, TypeCode.UInt32
                result = CLng(result) 'No range checking needed.
            Case TypeCode.UInt64, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.DBNull
                If Compiler.TypeResolution.CheckNumericRange(result, result, ExpressionType) = False Then
                    If ShowError Then Compiler.Report.ShowMessage(Messages.VBNC30439, Location, Helper.ToString(Expression, ExpressionType))
                    Return False
                End If
            Case Else
                If ShowError Then Compiler.Report.ShowMessage(Messages.VBNC30060, Location, result.ToString, Helper.ToString(Expression, ExpressionType))
                Return False
        End Select

        Return True
    End Function

    Public Function ConvertToULong(ByRef result As Object, ByVal ShowError As Boolean) As Boolean
        Dim tpCode As TypeCode

        tpCode = Helper.GetTypeCode(Compiler, CecilHelper.GetType(Compiler, result))
        Select Case tpCode
            Case TypeCode.Boolean, TypeCode.Byte, TypeCode.UInt16, TypeCode.UInt32, TypeCode.UInt64
                result = CULng(result) 'No range checking needed.
            Case TypeCode.Int16, TypeCode.Int32, TypeCode.SByte, TypeCode.Int64, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.DBNull
                If Compiler.TypeResolution.CheckNumericRange(result, result, ExpressionType) = False Then
                    If ShowError Then Compiler.Report.ShowMessage(Messages.VBNC30439, Location, Helper.ToString(Expression, ExpressionType))
                    Return False
                End If
            Case Else
                If ShowError Then Compiler.Report.ShowMessage(Messages.VBNC30060, Location, result.ToString, Helper.ToString(Expression, ExpressionType))
                Return False
        End Select

        Return True
    End Function

    Public Function ConvertToSingle(ByRef result As Object, ByVal ShowError As Boolean) As Boolean
        Dim tpCode As TypeCode

        tpCode = Helper.GetTypeCode(Compiler, CecilHelper.GetType(Compiler, result))

        Select Case tpCode
            Case TypeCode.Boolean, TypeCode.SByte, TypeCode.Byte, TypeCode.Int16, TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt32, TypeCode.UInt64, TypeCode.Int64, TypeCode.Single, TypeCode.Decimal
                result = CSng(result) 'No range checking needed.
            Case TypeCode.Double, TypeCode.DBNull
                If Compiler.TypeResolution.CheckNumericRange(result, result, ExpressionType) = False Then
                    If ShowError Then Compiler.Report.ShowMessage(Messages.VBNC30439, Location, Helper.ToString(Expression, ExpressionType))
                    Return False
                End If
            Case Else
                If ShowError Then Compiler.Report.ShowMessage(Messages.VBNC30060, Location, result.ToString, Helper.ToString(Expression, ExpressionType))
                Return False
        End Select

        Return True
    End Function

    Public Function ConvertToDouble(ByRef result As Object, ByVal ShowError As Boolean) As Boolean
        Dim tpCode As TypeCode

        tpCode = Helper.GetTypeCode(Compiler, CecilHelper.GetType(Compiler, result))

        Select Case tpCode
            Case TypeCode.Boolean, TypeCode.SByte, TypeCode.Byte, TypeCode.Int16, TypeCode.UInt16, TypeCode.Int32, _
            TypeCode.UInt32, TypeCode.UInt64, TypeCode.Int64, TypeCode.Single, TypeCode.Double, TypeCode.Decimal
                result = CDbl(result) 'No range checking needed.
            Case TypeCode.DBNull
                result = CDbl(0)
            Case Else
                If ShowError Then Compiler.Report.ShowMessage(Messages.VBNC30060, Location, result.ToString, Helper.ToString(Expression, ExpressionType))
                Return False
        End Select

        Return True
    End Function

    Public Function ConvertToDecimal(ByRef result As Object, ByVal ShowError As Boolean) As Boolean
        Dim tpCode As TypeCode

        tpCode = Helper.GetTypeCode(Compiler, CecilHelper.GetType(Compiler, result))

        Select Case tpCode
            Case TypeCode.Boolean, TypeCode.SByte, TypeCode.Byte, TypeCode.Int16, TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt32, TypeCode.UInt64, TypeCode.Int64, TypeCode.Decimal
                result = CDec(result) 'No range checking needed.
            Case TypeCode.Single, TypeCode.Double, TypeCode.DBNull
                If Compiler.TypeResolution.CheckNumericRange(result, result, ExpressionType) = False Then
                    If ShowError Then Compiler.Report.ShowMessage(Messages.VBNC30439, Expression.Location, ExpressionType.ToString)
                    Return False
                End If
            Case Else
                If ShowError Then Compiler.Report.ShowMessage(Messages.VBNC30060, Expression.Location, result.ToString, ExpressionType.ToString)
                Return False
        End Select

        Return True
    End Function

    Public Function ConvertToDate(ByRef result As Object, ByVal ShowError As Boolean) As Boolean
        Dim tpCode As TypeCode

        tpCode = Helper.GetTypeCode(Compiler, CecilHelper.GetType(Compiler, result))

        If Not TypeOf result Is Date Then
            If ShowError Then Show30059()
            Return False
        End If

        Return True
    End Function

    Public Function ConvertToChar(ByRef result As Object, ByVal ShowError As Boolean) As Boolean
        Dim tpCode As TypeCode

        tpCode = Helper.GetTypeCode(Compiler, CecilHelper.GetType(Compiler, result))

        Select Case tpCode
            Case TypeCode.String
                If CStr(result).Length = 1 Then
                    result = CChar(result)
                Else
                    If ShowError Then Compiler.Report.ShowMessage(Messages.VBNC30060, Location, result.ToString, ExpressionType.ToString)
                    Return False
                End If
            Case TypeCode.Char
                result = CChar(result)
            Case TypeCode.DBNull
                result = VB.ChrW(0)
            Case Else
                If ShowError Then Compiler.Report.ShowMessage(Messages.VBNC30060, Location, result.ToString, ExpressionType.ToString)
                Return False
        End Select

        Return True
    End Function

    Public Function ConvertToString(ByRef result As Object, ByVal ShowError As Boolean) As Boolean
        Dim tpCode As TypeCode

        If result Is Nothing Then Return True

        tpCode = Helper.GetTypeCode(Compiler, CecilHelper.GetType(Compiler, result))

        Select Case tpCode
            Case TypeCode.Char, TypeCode.String
                result = CStr(result)
            Case TypeCode.DBNull
                result = DBNull.Value
            Case Else
                If ShowError Then Compiler.Report.ShowMessage(Messages.VBNC30060, Location, result.ToString, Helper.ToString(Expression, ExpressionType))
                Return False
        End Select

        Return True
    End Function

    Public Function ConvertToBoolean(ByRef result As Object, ByVal ShowError As Boolean) As Boolean
        Dim tpCode As TypeCode

        tpCode = Helper.GetTypeCode(Compiler, CecilHelper.GetType(Compiler, result))

        Select Case tpCode
            Case TypeCode.Boolean, TypeCode.SByte, TypeCode.Byte, TypeCode.Int16, TypeCode.UInt16, TypeCode.Int32, _
              TypeCode.UInt32, TypeCode.UInt64, TypeCode.Int64, TypeCode.Single, TypeCode.Double, TypeCode.Decimal
                result = CBool(result) 'No range checking needed.
            Case TypeCode.DBNull
                result = CBool(Nothing)
            Case Else
                If ShowError Then Compiler.Report.ShowMessage(Messages.VBNC30060, Location, result.ToString, Helper.ToString(Expression, ExpressionType))
                Return False
        End Select

        Return True
    End Function

    Shared Function GetTypeConversion(ByVal Parent As ParsedObject, ByVal fromExpr As Expression, ByVal DestinationType As Mono.Cecil.TypeReference, Optional ByVal IsExplicit As Boolean = False) As Expression
        Dim convExp As ConversionExpression

        If Helper.CompareType(fromExpr.ExpressionType, DestinationType) Then
            Return fromExpr
        End If

        Select Case Helper.GetTypeCode(Parent.Compiler, DestinationType)
            Case TypeCode.Boolean
                convExp = New CBoolExpression(Parent, fromExpr)
            Case TypeCode.Byte
                convExp = New CByteExpression(Parent, fromExpr)
            Case TypeCode.Char
                convExp = New CCharExpression(Parent, fromExpr)
            Case TypeCode.DateTime
                convExp = New CDateExpression(Parent, fromExpr)
            Case TypeCode.Decimal
                convExp = New CDecExpression(Parent, fromExpr)
            Case TypeCode.Double
                convExp = New CDblExpression(Parent, fromExpr)
            Case TypeCode.Int16
                convExp = New CShortExpression(Parent, fromExpr)
            Case TypeCode.Int32
                convExp = New CIntExpression(Parent, fromExpr)
            Case TypeCode.Int64
                convExp = New CLngExpression(Parent, fromExpr)
            Case TypeCode.SByte
                convExp = New CSByteExpression(Parent, fromExpr)
            Case TypeCode.Single
                convExp = New CSngExpression(Parent, fromExpr)
            Case TypeCode.String
                convExp = New CStrExpression(Parent, fromExpr)
            Case TypeCode.UInt16
                convExp = New CUShortExpression(Parent, fromExpr)
            Case TypeCode.UInt32
                convExp = New CUIntExpression(Parent, fromExpr)
            Case TypeCode.UInt64
                convExp = New CULngExpression(Parent, fromExpr)
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
                    convExp = New CTypeExpression(Parent, fromExpr, DestinationType)
                End If
        End Select
        convExp.IsExplicit = IsExplicit
        Return convExp
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

