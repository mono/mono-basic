'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off

Module M
    Function Main() As Integer
        Dim ErrMsg As String = ""
        Dim count As Integer
        For i As Long = 16 To 6 Step -1
            count = count + 1
        Next i
        If count <> 11 Then
            ErrMsg = "#A1 For not working"
        End If

        count = 0
        For i As Short = 16 To 6 Step -1
            count = count + 1
        Next i
        If count <> 11 Then
            ErrMsg = ErrMsg & vbCrLf & "#A2 For not working"
        End If

        count = 0
        For i As Object = 16 To 6 Step -1
            count = count + 1
        Next i
        If count <> 11 Then
            ErrMsg = ErrMsg & vbCrLf & "#A3 For not working"
        End If

        count = 0
        For i As Double = 16 To 6 Step -0.1
            count = count + 1
        Next i
        If count <> 101 Then
            ErrMsg = ErrMsg & vbCrLf & "#A4 For not working"
        End If

        count = 0
        For i As Single = 16 To 6 Step -0.1
            count = count + 1
        Next i
        If count <> 100 Then
            ErrMsg = ErrMsg & vbCrLf & "#A5 For not working"
        End If

        count = 0
        For i As Decimal = 16 To 6 Step -0.1
            count = count + 1
        Next i
        If count <> 101 Then
            ErrMsg = ErrMsg & vbCrLf & "#A6 For not working"
        End If
        If (ErrMsg <> "") Then
            Throw New System.Exception(ErrMsg)
        End If
    End Function
End Module
