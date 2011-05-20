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

Public Class ConcatExpression
    Inherits BinaryExpression

    Protected Overrides Function ResolveExpressions(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = MyBase.ResolveExpressions(Info) AndAlso result

        If result = False Then Return result

        Dim l, r As Boolean
        Dim lObj, rObj As Boolean

        l = Helper.CompareType(m_LeftExpression.ExpressionType, Compiler.TypeCache.System_DBNull)
        r = Helper.CompareType(m_RightExpression.ExpressionType, Compiler.TypeCache.System_DBNull)
        
        If l AndAlso r = False Then 'DBNull & whatever
            m_LeftExpression = New NothingConstantExpression(Me)
            result = m_LeftExpression.ResolveExpression(Info) AndAlso result
        ElseIf l = False AndAlso r Then 'whatever & DBNull
            m_RightExpression = New NothingConstantExpression(Me)
            result = m_RightExpression.ResolveExpression(Info) AndAlso result
        ElseIf l AndAlso r Then 'DBNull & DBNull
            Return Compiler.Report.ShowMessage(Messages.VBNC30452, Me.Location, "&", Helper.ToString(Compiler, LeftType), Helper.ToString(Compiler, RightType))
        End If

        If l = False Then
            lObj = Helper.CompareType(m_LeftExpression.ExpressionType, Compiler.TypeCache.System_Object)
            If Location.File(Compiler).IsOptionStrictOn AndAlso lObj Then
                result = Compiler.Report.ShowMessage(Messages.VBNC30038, Me.Location, "&")
            End If
            If LeftTypeCode <> TypeCode.Object Then m_LeftExpression = New CStrExpression(Me, m_LeftExpression)
            result = m_LeftExpression.ResolveExpression(Info) AndAlso result
        End If

        If r = False Then
            rObj = Helper.CompareType(m_RightExpression.ExpressionType, Compiler.TypeCache.System_Object)
            If Location.File(Compiler).IsOptionStrictOn AndAlso rObj Then
                result = Compiler.Report.ShowMessage(Messages.VBNC30038, Me.Location, "&")
            End If
            If RightTypeCode <> TypeCode.Object Then m_RightExpression = New CStrExpression(Me, m_RightExpression)
            result = m_RightExpression.ResolveExpression(Info) AndAlso result
        End If

        Return result
    End Function

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        ValidateBeforeGenerateCode(Info)

        Dim expInfo As EmitInfo = Info.Clone(Me, True, False, OperandType)

        result = m_LeftExpression.GenerateCode(expInfo) AndAlso result
        result = m_RightExpression.GenerateCode(expInfo) AndAlso result

        Select Case OperandTypeCode
            Case TypeCode.String
                Emitter.EmitCall(Info, Compiler.TypeCache.System_String__Concat_String_String)
            Case TypeCode.Object
                Helper.Assert(Helper.CompareType(OperandType, Compiler.TypeCache.System_Object))
                Emitter.EmitCall(Info, Compiler.TypeCache.MS_VB_CS_Operators__ConcatenateObject_Object_Object)
            Case Else
                Throw New InternalException(Me)
        End Select
        Return result
    End Function

    Sub New(ByVal Parent As ParsedObject, ByVal LExp As Expression, ByVal RExp As Expression)
        MyBase.New(Parent, LExp, RExp)
    End Sub

    Public Overrides ReadOnly Property Keyword() As KS
        Get
            Return KS.Concat
        End Get
    End Property

    Public Overrides Function GetConstant(ByRef result As Object, ByVal ShowError As Boolean) As Boolean
        Dim lvalue As Object = Nothing
        Dim rvalue As Object = Nothing

        If Not m_LeftExpression.GetConstant(lvalue, ShowError) Then Return False
        If Not m_RightExpression.GetConstant(rvalue, ShowError) Then Return False

        If ((TypeOf lvalue Is String OrElse TypeOf lvalue Is Char) AndAlso (TypeOf rvalue Is String OrElse TypeOf rvalue Is Char)) = False Then
            If ShowError Then Show30059()
            Return False
        End If

        result = CStr(lvalue) & CStr(rvalue)

        Return True
    End Function
End Class

