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

Public MustInherit Class ParsedObject
    Inherits BaseObject

    Private m_HasErrors As Boolean

#If DEBUG Then
    Private m_TypeReferencesResolved As Diagnostics.StackTrace
    Private m_CodeResolved As Diagnostics.StackTrace
#End If

    <Diagnostics.Conditional("DEBUG"), Diagnostics.DebuggerHidden()> Sub CheckTypeReferencesNotResolved()
#If DEBUG Then
        If m_TypeReferencesResolved IsNot Nothing Then
            Compiler.Report.WriteLine("ResolveTypeReferences called multiple times:")
            Compiler.Report.WriteLine(m_TypeReferencesResolved.ToString)
            Helper.Stop()
        End If
        'm_TypeReferencesResolved = New Diagnostics.StackTrace(True)
#End If
    End Sub

    <Diagnostics.Conditional("DEBUG"), Diagnostics.DebuggerHidden()> Sub CheckCodeNotResolved()
#If DEBUG Then
        If m_CodeResolved IsNot Nothing Then
            Compiler.Report.WriteLine("ResolveCode called multiple times:")
            Compiler.Report.WriteLine(m_CodeResolved.ToString)
            Helper.Stop()
        End If
        m_CodeResolved = New Diagnostics.StackTrace(True)
#End If
    End Sub

    ReadOnly Property CodeResolved() As Boolean
        <Diagnostics.DebuggerHidden()> Get
#If DEBUG Then

            Return m_CodeResolved IsNot Nothing
#End If
        End Get
    End Property

    ReadOnly Property TypeReferencesResolved() As Boolean
        <Diagnostics.DebuggerHidden()> Get
#If DEBUG Then

            Return m_TypeReferencesResolved IsNot Nothing
#End If
        End Get
    End Property

    Protected Sub New(ByVal Parent As ParsedObject)
        MyBase.new(Parent)
    End Sub

    Protected Sub New(ByVal Parent As ParsedObject, ByVal Location As Span)
        MyBase.new(Parent, Location)
    End Sub

    Protected Sub New(ByVal Parent As Compiler)
        MyBase.new(Parent)
    End Sub

    Property HasErrors() As Boolean
        Get
            Return m_HasErrors
        End Get
        Set(ByVal value As Boolean)
            m_HasErrors = value
        End Set
    End Property

    Shadows Property Parent() As ParsedObject
        Get
            Return DirectCast(MyBase.Parent, ParsedObject)
        End Get
        Set(ByVal value As ParsedObject)
            MyBase.Parent = value
        End Set
    End Property

    '<Obsolete("Not implemented?")> _
    Overridable Function ResolveTypeReferences() As Boolean
        Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, "Does '" & Me.GetType.ToString & "' have no type references?")
        Return True
    End Function
End Class
