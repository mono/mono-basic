Class LeftShift1
	Shared Function Main As Integer
        Dim s As Short = 5S
		Dim i As Integer = 400001
		
        s <<= i
		
        Return s - 10
	End Function
End Class
