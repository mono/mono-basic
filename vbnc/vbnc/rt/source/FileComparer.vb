' 
' Visual Basic.Net COmpiler
' Copyright (C) 2004 - 2006 Rolf Bjarne Kvinge, rbjarnek at users.sourceforge.net
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

Public Class FileComparer

    Public Shared Function Compare(ByVal File1 As String, ByVal File2 As String) As Boolean
        Dim size As Long
        size = New IO.FileInfo(File1).Length
        If New IO.FileInfo(File2).Length <> size Then Return False

        Const BUFFERSIZE As Integer = 1024
        Dim buffer1(BUFFERSIZE - 1) As Byte
        Dim buffer2(BUFFERSIZE - 1) As Byte
        Dim read1, read2 As Integer
        Dim left As Long = size
        Using stream1 As New IO.FileStream(File1, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.ReadWrite, CInt(buffer1.Length), IO.FileOptions.SequentialScan), stream2 As New IO.FileStream(File1, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.ReadWrite, BUFFERSIZE, IO.FileOptions.SequentialScan)
            Do Until left = 0
                read1 = stream1.Read(buffer1, 0, BUFFERSIZE)
                read2 = stream2.Read(buffer2, 0, BUFFERSIZE)
                If read1 <> read2 Then Return False
                For i As Integer = 0 To read1 - 1
                    If buffer1(i) <> buffer2(i) Then Return False
                Next
                left -= read1
            Loop
        End Using
        Return True
    End Function
End Class
