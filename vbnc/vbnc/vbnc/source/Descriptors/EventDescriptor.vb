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
#Const DEBUGEVENTACCESS = 0
#End If
Public Class EventDescriptor
    Inherits EventInfo
    Implements IMemberDescriptor

    Private m_Declaration As EventDeclaration
    Private m_Parent As ParsedObject

    <Diagnostics.Conditional("DEBUGEVENTACCESS")> _
          Protected Sub DumpMethodInfo(Optional ByVal ReturnValue As Object = Nothing)
#If DEBUGEVENTACCESS Then
        Static recursive As Boolean
        If recursive Then Return
        recursive = True

        Dim m As New Diagnostics.StackFrame(1)
        Dim str As String

        Dim name As String = Me.Name
        str = " Called: (" & name & "): EventInfo."

        If ReturnValue IsNot Nothing Then
            m_Parent.Compiler.Report.WriteLine(Report.ReportLevels.Debug, str & m.GetMethod.Name & " with return value: " & ReturnValue.ToString)
        Else
            m_Parent.Compiler.Report.WriteLine(Report.ReportLevels.Debug, str & m.GetMethod.Name)
        End If
        recursive = False
#End If
    End Sub

    ReadOnly Property IsShared() As Boolean Implements IMemberDescriptor.IsShared
        Get
            Return m_Declaration.IsShared
        End Get
    End Property

    ReadOnly Property EventDeclaration() As EventDeclaration
        Get
            Return m_Declaration
        End Get
    End Property

    Sub New(ByVal Declaration As EventDeclaration)
        m_Declaration = Declaration
        m_Parent = Declaration
    End Sub

    ReadOnly Property Compiler() As Compiler
        Get
            Return m_Declaration.Compiler
        End Get
    End Property

    Public Overrides ReadOnly Property Attributes() As System.Reflection.EventAttributes
        Get
            Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
            Return Nothing
        End Get
    End Property

    Public Overrides ReadOnly Property DeclaringType() As System.Type
        Get
            Return m_Declaration.FindFirstParent(Of IType).TypeDescriptor
        End Get
    End Property

    Public Overloads Overrides Function GetAddMethod(ByVal nonPublic As Boolean) As System.Reflection.MethodInfo
        Return m_Declaration.GetAddMethod(nonPublic)
    End Function

    Public Overloads Overrides Function GetCustomAttributes(ByVal inherit As Boolean) As Object()
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return Nothing
    End Function

    Public Overloads Overrides Function GetCustomAttributes(ByVal attributeType As System.Type, ByVal inherit As Boolean) As Object()
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return Nothing
    End Function

    Public Overloads Overrides Function GetRaiseMethod(ByVal nonPublic As Boolean) As System.Reflection.MethodInfo
        Return m_Declaration.GetRaiseMethod(nonPublic)
    End Function

    Public Overloads Overrides Function GetRemoveMethod(ByVal nonPublic As Boolean) As System.Reflection.MethodInfo
        Return m_Declaration.GetRemoveMethod(nonPublic)
    End Function

    Public Overrides Function IsDefined(ByVal attributeType As System.Type, ByVal inherit As Boolean) As Boolean
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return Nothing
    End Function

    Public Overrides ReadOnly Property Name() As String
        Get
            Return m_Declaration.Name
        End Get
    End Property

    Public Overrides ReadOnly Property ReflectedType() As System.Type
        Get
            Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
            Return Nothing
        End Get
    End Property

    Public ReadOnly Property Declaration() As IMember Implements IMemberDescriptor.Declaration
        Get
            Return m_Declaration
        End Get
    End Property

    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        Dim result As Boolean
        result = obj Is Me
        DumpMethodInfo(result)
        Return result
    End Function

    Public Overrides Function GetHashCode() As Integer
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return MyBase.GetHashCode()
    End Function
    Public Overrides Function GetOtherMethods(ByVal nonPublic As Boolean) As System.Reflection.MethodInfo()
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return MyBase.GetOtherMethods(nonPublic)
    End Function
    Public Overrides ReadOnly Property MemberType() As System.Reflection.MemberTypes
        Get
            Dim result As MemberTypes
            result = MemberTypes.Event
            DumpMethodInfo(result)
            Return MyBase.MemberType
        End Get
    End Property
    Public Overrides ReadOnly Property MetadataToken() As Integer
        Get
            Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
            Return MyBase.MetadataToken
        End Get
    End Property
    Public Overrides ReadOnly Property [Module]() As System.Reflection.Module
        Get
            Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
            Return MyBase.[Module]
        End Get
    End Property
    Public Overrides Function ToString() As String
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return MyBase.ToString()
    End Function
End Class
