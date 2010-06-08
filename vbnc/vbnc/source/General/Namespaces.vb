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
''' A list of namespaces.
''' </summary>
''' <remarks></remarks>
Public Class Namespaces
    Inherits Generic.List(Of [Namespace])

    Private m_Hashed As Generic.Dictionary(Of String, [Namespace])

    ''' <summary>
    ''' Checks if the specified name exists in the list.
    ''' </summary>
    ''' <param name="Name"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ContainsKey(ByVal Name As String) As Boolean
        Return m_Hashed.ContainsKey(Name)
    End Function

    ''' <summary>
    ''' Loops up the namespace of the specified namespace.
    ''' Returns nothing if nothing is found.
    ''' </summary>
    ''' <param name="Name"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Default Overloads ReadOnly Property Item(ByVal Name As String) As [Namespace]
        Get
            If ContainsKey(Name) Then
                Return m_Hashed(Name)
            Else
                Return Nothing
            End If
        End Get
    End Property

    Function FindNamespace(ByVal A As String, ByVal B As String) As [Namespace]
        For i As Integer = 0 To Count - 1
            If Item(i).Equals(A, B) Then Return Item(i)
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Looks up the specified child of the namespace.
    ''' Returns nothing if nothing is found.
    ''' </summary>
    ''' <param name="Parent"></param>
    ''' <param name="Child"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Default Overloads ReadOnly Property Item(ByVal Parent As [Namespace], ByVal Child As String) As [Namespace]
        Get
            Dim name As String = Parent.Name & "." & Child
            If ContainsKey(name) Then
                Return Item(name)
            Else
                Return Nothing
            End If
        End Get
    End Property

    ''' <summary>
    ''' Creates a string array with all the namespaces.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property NamespacesAsString() As String()
        Get
            Dim result(Count - 1) As String
            Dim tmp() As [Namespace] = Me.ToArray
            For i As Integer = 0 To Count - 1
                result(i) = tmp(i).Name
            Next
            Return result
        End Get
    End Property

    Overloads Sub Add(ByVal Parent As BaseObject, ByVal ns As String, ByVal IsGlobal As Boolean)
        If ns = "" Then Return
        Add(New [Namespace](Parent, ns, IsGlobal))
    End Sub

    ''' <summary>
    ''' Adds a namespace and all its variants
    ''' (i.e. for System.Xml it adds System and System.Xml)
    ''' </summary>
    ''' <param name="ns"></param>
    ''' <param name="IsGlobal"></param>
    ''' <remarks></remarks>
    Sub AddAllNamespaces(ByVal Parent As BaseObject, ByVal ns As String, ByVal IsGlobal As Boolean)
        If ns = String.Empty Then Return

        If Me.ContainsKey(ns) Then Return

        Add(Parent, ns, IsGlobal)

        Dim idx As Integer = ns.LastIndexOf("."c)
        If idx < 0 Then Return

        Dim tmp As String = ns
        Do
            tmp = tmp.Substring(0, idx)
            If Me.ContainsKey(tmp) Then Return

            Add(Parent, tmp, IsGlobal)

            idx = tmp.LastIndexOf("."c)
        Loop While idx >= 0
    End Sub

    Overloads Sub Add(ByVal ns As [Namespace])
        If Not ContainsKey(ns.Name) Then
            MyBase.Add(ns)
            m_Hashed.Add(ns.Name, ns)
        End If
    End Sub

    Overloads Sub AddRange(ByVal Parent As BaseObject, ByVal Namespaces As String(), ByVal IsGlobal As Boolean)
        For Each ns As String In Namespaces
            Add(Parent, ns, IsGlobal)
        Next
    End Sub

    Sub New()
        m_Hashed = New Generic.Dictionary(Of String, [Namespace])(Helper.StringComparer)
    End Sub

    ''' <summary>
    ''' Searches the namespaces of the current compiling assembly / referenced assemblies to check
    ''' if the name is a namespace.
    ''' </summary>
    ''' <param name="Name"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function IsNamespace(ByVal Name As String, ByVal onlyExact As Boolean) As Boolean
        Name = vbnc.Namespace.RemoveGlobal(Name)
        If ContainsKey(Name) Then
            Return True
        ElseIf onlyExact = False Then
            For Each strNS As [Namespace] In Me
                If strNS.StartsWith(Name) Then Return True
            Next
        End If
        Return False
    End Function

    Function IsNamespaceExact(ByVal A As String, ByVal B As String) As Boolean
        For i As Integer = 0 To Me.Count - 1
            Dim ns As [Namespace] = Me.Item(i)
            If ns.Equals(A, B) Then Return True
        Next
        Return False
    End Function

End Class