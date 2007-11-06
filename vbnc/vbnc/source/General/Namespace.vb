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

Public Class [Namespace]
    Inherits BaseObject
    Implements INameable

    Private m_Name As String
    Protected m_Global As Boolean

    ReadOnly Property Name() As String Implements INameable.Name
        Get
            Return m_Name
        End Get
    End Property

    ReadOnly Property [Global]() As Boolean
        Get
            Return m_Global
        End Get
    End Property

    Protected Sub New(ByVal Parent As BaseObject)
        MyBase.New(Parent)
    End Sub

    Sub New(ByVal Parent As BaseObject, ByVal Previous As [Namespace], ByVal Name As String)
        MyBase.New(Parent)
        If Previous Is Nothing Then Throw New ArgumentNullException("Namespace")

        If TypeOf Previous Is GlobalNamespace Then
            m_Name = Name
            m_Global = True
        Else
            m_Name = Previous.Name & "." & Name
            m_Global = Previous.Global
        End If
    End Sub

    Sub New(ByVal Parent As BaseObject, ByVal Name As String, ByVal [Global] As Boolean)
        MyBase.New(Parent)
        If Name = "" AndAlso [Global] = False Then Throw New ArgumentNullException("Name")
        m_Name = Name
        m_Global = [Global]
    End Sub

    Overrides Function ToString() As String
        If m_Global Then
            Return "Global." & m_Name
        Else
            Return m_Name
        End If
    End Function

    'Shared Widening Operator CType(ByVal ns As [Namespace]) As String
    '    Return ns.ToString
    'End Operator

    Shared Operator &(ByVal ns As [Namespace], ByVal str As String) As [Namespace]
        If ns Is Nothing Then Throw New InternalException("")
        If TypeOf ns Is GlobalNamespace Then
            Return New [Namespace](ns.Parent, str, True)
        Else
            Return New [Namespace](ns.Parent, ns, str)
        End If
    End Operator

    Shared Operator &(ByVal str As String, ByVal ns As [Namespace]) As [Namespace]
        If ns.Global Then
            Throw New InternalException("")
        Else
            Return New [Namespace](ns.Parent, str & "." & ns.ToString, False)
        End If
    End Operator

    Function StartsWith(ByVal str As String) As Boolean
        Helper.Assert(m_Name IsNot Nothing AndAlso m_Name <> "")
        Helper.Assert(str IsNot Nothing AndAlso str <> "")
        If str.Length <= m_Name.Length Then
            If m_Global AndAlso IsGlobal(str) Then
                str = RemoveGlobal(str)
                Return Helper.CompareName(str, m_Name.Substring(0, str.Length))
            Else
                Return Helper.CompareName(str, m_Name.Substring(0, str.Length))
            End If
        Else
            Return False
        End If
    End Function

    Overloads Function Equals(ByVal A As String, ByVal B As String) As Boolean
        If A.Length + B.Length + 1 <> m_Name.Length Then Return False
        If m_Name.StartsWith(A, Helper.StringComparison) = False Then Return False
        If m_Name.EndsWith(B, Helper.StringComparison) = False Then Return False
        Return m_Name(A.Length) = "."c
    End Function

    Overloads Function Equals(ByVal str As String) As Boolean
        If IsGlobal(str) Then
            If Me.Global = False Then
                Return False
            Else
                Return Helper.CompareName(RemoveGlobal(str), Me.Name)
            End If
        Else
            Return Helper.CompareName(str, Me.Name)
        End If
    End Function

    Shared Operator =(ByVal ns As [Namespace], ByVal str As String) As Boolean
        Return ns.Equals(str)
    End Operator

    Shared Operator <>(ByVal ns As [Namespace], ByVal str As String) As Boolean
        Return Not ns = str
    End Operator

    Shared Function RemoveGlobal(ByVal ns As String) As String
        If IsGlobal(ns) Then
            Return ns.Substring(7)
        Else
            Helper.Assert(ns.StartsWith("Global.", Helper.StringComparison) = False)
            Return ns
        End If
    End Function

    Shared Function IsGlobal(ByVal ns As String) As Boolean
        Return ns.Length > 7 AndAlso Helper.CompareNameStart(ns, "Global.")
    End Function

End Class
