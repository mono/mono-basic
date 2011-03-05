''' <summary>
'''
''' </summary>
Class Message
    Implements IComparable

    Public ReadOnly ID As Integer
    Public VBNCMsg As String = "CHANGEME"
    Public VBMsg As String = "(No corresponding vbc error)"
    Public Level As String = "Error"
    Public Comment As String
    ''' <summary>
    '''
    ''' </summary>
    Sub New(ByVal ID As Integer, Level As String)
        Me.ID = ID
	If Level = "" Then
		If ID >= 40000 Then
		    Me.Level = "Warning"
		Else
		    Me.Level = "Error"
		End If
	Else
		Me.Level = level
	End If
        'Console.WriteLine("Created ID=" & ID)
    End Sub

    ''' <summary>
    '''
    ''' </summary>
    Function CompareTo(ByVal obj As Object) As Integer Implements icomparable.CompareTo
        Dim msg As Message = DirectCast(obj, Message)
        If msg.ID > ID Then
            Return -1
        ElseIf msg.ID = ID Then
            Return 0
        Else
            Return 1
        End If
    End Function
End Class

