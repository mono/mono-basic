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

Imports Microsoft.VisualBasic

''' <summary>
''' A list of resources.
''' </summary>
Public Class Resources
    Inherits ArrayList
    ''' <summary>
    ''' The compiling compiler.
    ''' </summary>
    Private m_Compiler As Compiler

    ''' <summary>
    ''' The compiling compiler.
    ''' </summary>	
    ReadOnly Property Compiler() As Compiler
        Get
            Return m_Compiler
        End Get
    End Property

    ''' <summary>
    ''' A new resource list.
    ''' </summary>	
    Sub New(ByVal Compiler As Compiler)
        m_Compiler = Compiler
    End Sub

    ''' <summary>
    ''' Returns the resource at the specified index.
    ''' </summary>
    Shadows ReadOnly Property Item(ByVal Index As Integer) As Resource
        Get
            Return DirectCast(MyBase.Item(Index), Resource)
        End Get
    End Property

    ''' <summary>
    ''' Add a new resource.
    ''' </summary>
    Shadows Function Add(ByVal Resource As Resource) As Integer
        Return MyBase.Add(Resource)
    End Function

    ''' <summary>
    ''' Add a new resource. Parses the string and verifies it.
    ''' If there are any messages, they are saved, not shown.
    ''' </summary>
    Shadows Function Add(ByVal str As String) As Boolean
        Dim strItems() As String = Split(str, ",")
        Dim files As String()
        Dim isPublic As Boolean = True
        Dim identifier As String = ""

        Select Case strItems.Length
            Case 1
                files = Compiler.CommandLine.GetFullPaths(strItems(0))
            Case 2
                files = Compiler.CommandLine.GetFullPaths(strItems(0))
                identifier = strItems(1)
            Case 3
                files = Compiler.CommandLine.GetFullPaths(strItems(0))
                identifier = strItems(1)
                Select Case strItems(2).ToLower()
                    Case "public"
                        isPublic = True
                    Case "private"
                        isPublic = False
                    Case Else
                        Compiler.Report.SaveMessage(Messages.VBNC2019, "resource", strItems(2))
                        Return False
                End Select
            Case Else
                Compiler.Report.SaveMessage(Messages.VBNC2009, str)
                Return False
        End Select

        If files Is Nothing OrElse files.Length = 0 Then
            Compiler.Report.SaveMessage(Messages.VBNC2001, strItems(0))
            Return False
        End If

        For Each file As String In files
            Add(New Resource(file, identifier, isPublic))
        Next

        Return True
    End Function
End Class
