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

Public Class CecilCompare
    Inherits VerificationBase

    Sub New(ByVal Test As Test)
        MyBase.New(Test)
    End Sub

    Protected Overrides Function RunVerification() As Boolean
        Try
            Dim cc As New CecilComparer(Me.Test.OutputVBCAssemblyFull, Me.Test.OutputAssemblyFull)
            Dim result As Boolean = cc.Compare

            For Each Str As String In cc.Errors
                MyBase.DescriptiveMessage &= Str & vbNewLine
            Next

            For Each Str As String In cc.Messages
                MyBase.DescriptiveMessage &= Str & vbNewLine
            Next

            Return result
        Catch ex As Exception
            MyBase.DescriptiveMessage = ex.ToString()
            Return False
        End Try
    End Function
End Class
