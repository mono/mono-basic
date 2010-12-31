
Option Strict On

Imports System.Collections.Generic

Class A
    Public Shared Function Main() As Integer
        Dim FileNumber As Integer = -500
        Dim m_OpenFiles As Dictionary(Of Integer, String) = Nothing

        If FileNumber <= 0 OrElse FileNumber > 255 OrElse ((Not m_OpenFiles Is Nothing) AndAlso m_OpenFiles.ContainsKey(FileNumber)) Then
            Return 0
        End If

        Return 1
    End Function
End Class