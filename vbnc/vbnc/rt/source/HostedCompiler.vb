' 
' Visual Basic.Net Compiler
' Copyright (C) 2004 - 2008 Rolf Bjarne Kvinge, RKvinge@novell.com
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

Public Class HostedCompiler
    Inherits VerificationBase

    Private Shared Helper As CompilerHelper
    Private m_Compilation As Compilation

    Sub New(ByVal Test As Test)
        MyBase.New(Test)
    End Sub

    Friend ReadOnly Property Compilation() As Compilation
        Get
            Return m_Compilation
        End Get
    End Property

    Protected Overrides Function RunVerification() As Boolean
        Dim compilation As Compilation

        If Helper Is Nothing Then
            Helper = New CompilerHelper(Test.Parent.VBNCPath)
        End If

        Try
            compilation = Helper.Compile(Test.GetTestCommandLineArguments(False), Test)
        Catch ex As Exception
            Return False
        End Try

        m_Compilation = compilation

        DescriptiveMessage = m_Compilation.CompilerHelper.ConsoleOutput

        Return compilation.ExitCode = 0
    End Function
End Class
