'
' AssemblyInfo.vb
'
' Authors:
'   Rolf Bjarne Kvinge  (RKvinge@novell.com)
'

'
' Copyright (C) 2007 Novell, Inc (http://www.novell.com)
'
' Permission is hereby granted, free of charge, to any person obtaining
' a copy of this software and associated documentation files (the
' "Software"), to deal in the Software without restriction, including
' without limitation the rights to use, copy, modify, merge, publish,
' distribute, sublicense, and/or sell copies of the Software, and to
' permit persons to whom the Software is furnished to do so, subject to
' the following conditions:
' 
' The above copyright notice and this permission notice shall be
' included in all copies or substantial portions of the Software.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
' EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
' MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
' NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
' LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
' OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
' WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

Imports System.Reflection
Imports System.Collections.ObjectModel
Imports System.Collections.Generic

Namespace Microsoft.VisualBasic.ApplicationServices
    Public Class AssemblyInfo
        Private m_CurrentAssembly As Assembly

        Private Function GetAttribute(Of T As Attribute)() As T
            Dim results() As Object
            results = m_CurrentAssembly.GetCustomAttributes(GetType(T), True)
            If results.Length > 0 Then
                Return DirectCast(results(0), T)
            Else
                Return Nothing
            End If

        End Function

        Public Sub New(ByVal currentAssembly As Assembly)
            m_CurrentAssembly = currentAssembly
        End Sub

        Public ReadOnly Property AssemblyName() As String
            Get
                Return m_CurrentAssembly.GetName.Name
            End Get
        End Property

        Public ReadOnly Property CompanyName() As String
            Get
                Dim attrib As AssemblyCompanyAttribute
                attrib = GetAttribute(Of AssemblyCompanyAttribute)()
                If attrib IsNot Nothing Then
                    Return attrib.Company
                Else
                    Return String.Empty
                End If
            End Get
        End Property

        Public ReadOnly Property Copyright() As String
            Get
                Dim attrib As AssemblyCopyrightAttribute
                attrib = GetAttribute(Of AssemblyCopyrightAttribute)()
                If attrib IsNot Nothing Then
                    Return attrib.Copyright
                Else
                    Return String.Empty
                End If
            End Get
        End Property

        Public ReadOnly Property Description() As String
            Get
                Dim attrib As AssemblyDescriptionAttribute
                attrib = GetAttribute(Of AssemblyDescriptionAttribute)()
                If attrib IsNot Nothing Then
                    Return attrib.Description
                Else
                    Return String.Empty
                End If
            End Get
        End Property

        Public ReadOnly Property DirectoryPath() As String
            Get
                Return System.IO.Path.GetDirectoryName(m_CurrentAssembly.Location)
            End Get
        End Property

#If TARGET_JVM Then
        <MonoNotSupported("GH limitation")> _
        Public ReadOnly Property LoadedAssemblies() As ReadOnlyCollection(Of Assembly)
#Else
        Public ReadOnly Property LoadedAssemblies() As ReadOnlyCollection(Of Assembly)
#End If
            Get
                Return New ReadOnlyCollection(Of Assembly)(AppDomain.CurrentDomain.GetAssemblies())
            End Get
        End Property

        Public ReadOnly Property ProductName() As String
            Get
                Dim attrib As AssemblyProductAttribute
                attrib = GetAttribute(Of AssemblyProductAttribute)()
                If attrib IsNot Nothing Then
                    Return attrib.Product
                Else
                    Return String.Empty
                End If
            End Get
        End Property

        Public ReadOnly Property StackTrace() As String
            Get
                Return Environment.StackTrace
            End Get
        End Property

        Public ReadOnly Property Title() As String
            Get
                Dim attrib As AssemblyTitleAttribute
                attrib = GetAttribute(Of AssemblyTitleAttribute)()
                If attrib IsNot Nothing Then
                    Return attrib.Title
                Else
                    Return String.Empty
                End If
            End Get
        End Property

        Public ReadOnly Property Trademark() As String
            Get
                Dim attrib As AssemblyTrademarkAttribute
                attrib = GetAttribute(Of AssemblyTrademarkAttribute)()
                If attrib IsNot Nothing Then
                    Return attrib.Trademark
                Else
                    Return String.Empty
                End If
            End Get
        End Property

        Public ReadOnly Property Version() As Version
            Get
                Return m_CurrentAssembly.GetName.Version
            End Get
        End Property

        Public ReadOnly Property WorkingSet() As Long
            Get
                Return Environment.WorkingSet
            End Get
        End Property

    End Class
End Namespace
