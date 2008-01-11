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

#If DEBUG Then
#Const DEBUGPARAMETERACCESS = 0
#End If

Public Class ParameterDescriptor
    Inherits ParameterInfo

    Private m_Parameter As Parameter
    Private m_Position As Integer = -1
    Private m_Operand As Operand
    Private m_TypeName As TypeName
    Private m_Type As Type

    Private m_Name As String
    Private m_Parent As ParsedObject

    <Diagnostics.Conditional("DEBUGPARAMETERACCESS")> _
         Private Sub DumpMethodInfo(Optional ByVal ReturnValue As Object = Nothing)
#If DEBUGPARAMETERACCESS Then
        Dim m As New Diagnostics.StackFrame(1)
        Dim str As String

        Dim name As String
        If m_Parameter IsNot Nothing Then
            name = m_Parameter.FullName
        ElseIf m_Operand IsNot Nothing Then
            name = m_Operand.Name
        Else
            name = "ParameterWithNoName" & m_Position
        End If


        str = " Called: (" & name & "): ParameterInfo."

        If ReturnValue IsNot Nothing Then
            m_Parent.Compiler.Report.WriteLine(Report.ReportLevels.Debug, str & m.GetMethod.Name & " with return value: " & ReturnValue.ToString)
        Else
            m_Parent.Compiler.Report.WriteLine(Report.ReportLevels.Debug, str & m.GetMethod.Name)
        End If
#End If
    End Sub

    ReadOnly Property Compiler() As Compiler
        Get
            Return m_Parent.Compiler
        End Get
    End Property

    Sub New(ByVal Param As Type, ByVal Position As Integer, ByVal Parent As ParsedObject, Optional ByVal Name As String = Nothing)
        Helper.Assert(Param IsNot Nothing)
        m_Position = Position
        m_Type = Param
        m_Parent = Parent
        m_Name = Name
    End Sub

    Sub New(ByVal Param As TypeName, ByVal Position As Integer, ByVal Parent As ParsedObject, Optional ByVal Name As String = Nothing)
        Helper.Assert(Param IsNot Nothing)
        m_Position = Position
        m_TypeName = Param
        m_Parent = Parent
        m_Name = Name
    End Sub

    Sub New(ByVal Param As Operand, ByVal Position As Integer)
        Helper.Assert(Param IsNot Nothing)
        m_Position = Position
        m_Operand = Param
        m_Parent = Param
        m_Name = m_Operand.Name
    End Sub

    Sub New(ByVal Param As Parameter)
        Helper.Assert(Param IsNot Nothing)
        m_Parameter = Param
        m_Position = m_Parameter.Position
        m_Parent = Param
        m_Name = Param.Name
    End Sub

    Overridable ReadOnly Property IsParamArray() As Boolean
        Get
            If m_Parameter IsNot Nothing Then
                Return m_Parameter.Modifiers.Is(ModifierMasks.ParamArray)
            ElseIf m_Type IsNot Nothing AndAlso m_Type.IsArray = False Then
                Return False
            Else
                Compiler.Report.ShowMessage(Messages.VBNC99997, m_Parameter.Location)
            End If
        End Get
    End Property

    Public Overrides ReadOnly Property Attributes() As System.Reflection.ParameterAttributes
        Get
            Dim result As ParameterAttributes
            If m_Parameter IsNot Nothing Then
                If m_Parameter.Modifiers.IsAny(ModifierMasks.Optional Or ModifierMasks.ParamArray) Then
                    result = result Or ParameterAttributes.Optional
                End If
            Else
                result = ParameterAttributes.None
            End If
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property DefaultValue() As Object
        Get
            Dim result As Object = DBNull.Value
            If m_Parameter IsNot Nothing Then
                If m_Parameter.HasConstantValue Then
                    result = m_Parameter.ConstantValue
                ElseIf Me.IsParamArray Then
                    result = Nothing
                Else
                    Helper.Assert(Me.IsOptional = False)
                End If
            Else
                Compiler.Report.ShowMessage(Messages.VBNC99997, m_Parameter.Location)
            End If

            If result Is DBNull.Value Then result = Nothing

            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        Dim result As Boolean = MyBase.Equals(obj)
        DumpMethodInfo(result)
        Return result
    End Function

    Public Overrides Function GetCustomAttributes(ByVal inherit As Boolean) As Object()
        Dim result As Object() = MyBase.GetCustomAttributes(inherit)
        DumpMethodInfo(result)
        Compiler.Report.ShowMessage(Messages.VBNC99997, m_Parameter.Location)
        Return result
    End Function

    Public Overrides Function GetCustomAttributes(ByVal attributeType As System.Type, ByVal inherit As Boolean) As Object()
        Dim result As Object() = MyBase.GetCustomAttributes(attributeType, inherit)
        DumpMethodInfo(result)
        Compiler.Report.ShowMessage(Messages.VBNC99997, m_Parameter.Location)
        Return result
    End Function

    Public Overrides Function GetHashCode() As Integer
        Dim result As Integer = MyBase.GetHashCode
        DumpMethodInfo(result)
        Compiler.Report.ShowMessage(Messages.VBNC99997, m_Parameter.Location)
        Return result
    End Function

    Public Overrides Function GetOptionalCustomModifiers() As System.Type()
        Dim result As Type() = MyBase.GetOptionalCustomModifiers()
        DumpMethodInfo(result)
        Return result
    End Function

    Public Overrides Function GetRequiredCustomModifiers() As System.Type()
        Dim result As Type() = MyBase.GetRequiredCustomModifiers()
        DumpMethodInfo(result)
        Return result
    End Function

    Public Overrides Function IsDefined(ByVal attributeType As System.Type, ByVal inherit As Boolean) As Boolean
        Dim result As Boolean

        If m_Parameter IsNot Nothing Then
            If m_Parameter.CustomAttributes IsNot Nothing Then
                result = m_Parameter.CustomAttributes.IsDefined(attributeType)
            End If
            If result = False AndAlso inherit = True Then
                Compiler.Report.ShowMessage(Messages.VBNC99997, m_Parameter.Location)
            End If
        Else
            Compiler.Report.ShowMessage(Messages.VBNC99997, m_Parameter.Location)
        End If

        DumpMethodInfo(result)
        Return result
    End Function

    Public Overrides ReadOnly Property Member() As System.Reflection.MemberInfo
        Get
            Dim result As MemberInfo = Nothing
            If m_Parameter IsNot Nothing Then
                result = m_Parameter.FindFirstParent(Of IMember).MemberDescriptor
            Else
                Compiler.Report.ShowMessage(Messages.VBNC99997, m_Parameter.Location)
            End If
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property Name() As String
        Get
            Dim result As String
            If m_Name IsNot Nothing Then
                result = m_Name
            ElseIf m_Parameter IsNot Nothing Then
                result = m_Parameter.Name
            ElseIf m_Operand IsNot Nothing Then
                result = m_Operand.Name
            Else
                result = ""
            End If
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property ParameterType() As System.Type
        Get
            Dim result As Type = Nothing
            If m_Type IsNot Nothing Then
                result = m_Type
            ElseIf m_Parameter IsNot Nothing Then
                result = m_Parameter.ParameterType
            Else
                Compiler.Report.ShowMessage(Messages.VBNC99997, m_Parameter.Location)
            End If
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property Position() As Integer
        Get
            Helper.Assert(m_Position > -1)
            DumpMethodInfo(m_Position)
            Return m_Position
        End Get
    End Property

    Public Overrides ReadOnly Property RawDefaultValue() As Object
        Get
            Dim result As Object
            result = DefaultValue
            DumpMethodInfo(result)
            Return result
        End Get
    End Property
    Public Overrides Function ToString() As String
        Dim result As String = MyBase.ToString
        If m_Type IsNot Nothing Then
            result = m_Type.ToString
        Else
            Compiler.Report.ShowMessage(Messages.VBNC99997, m_Parameter.Location)
        End If
        DumpMethodInfo(result)
        Return result
    End Function
End Class
