Module MainModule

    Function Main() As Integer
        Dim types() As Type = Reflection.Assembly.GetExecutingAssembly.GetTypes
        Dim result As Integer
        For Each t As Type In types
            If t.Name <> "MainModule" AndAlso t.Namespace.Contains(".My") = False Then
                Dim method As Reflection.MethodInfo
                method = t.GetMethod("Main")
                If method IsNot Nothing Then
                    Dim tmpResult As Object
                    tmpResult = method.Invoke(Nothing, Nothing)
                    result += CInt(tmpResult)
                    Debug.WriteLine("Tested: " & method.DeclaringType.FullName & "." & method.Name)
                End If
            End If
        Next
        Change()
    End Function

    Dim TYPES As String() = {"Byte", "SByte", "Short", "UShort", "Integer", "UInteger", "Long", "ULong", "Single", "Double", "Decimal", "String", "Char", "Date", "Boolean"}

    Sub Change()
        Change(IO.Directory.GetFiles("..\..\JaggedArrays", "*VariableInteger?.vb"))
        Change(IO.Directory.GetFiles("..\..\MultiDimensionalArrays", "*VariableInteger?.vb"))
    End Sub

    Sub Change(ByVal sourcefiles() As String)
        For Each file As String In sourcefiles
            For Each type As String In TYPES
                If String.Compare("Integer", type, True) <> 0 Then
                    Change(file, "Integer", type)
                End If
            Next
        Next
    End Sub

    Sub Change(ByVal Filename As String, ByVal Source As String, ByVal ChangeTo As String)
        Dim newFilename As String = Filename.Replace(Source, ChangeTo)
        Dim oldText As String = IO.File.ReadAllText(Filename)
        Dim newText As String = oldText.Replace(Source, ChangeTo)

        Dim exp As String = ""
        Select Case ChangeTo
            Case "Byte"
                exp = "CByte(?)"
            Case "SByte"
                exp = "CSByte(?)"
            Case "UShort", "UInteger", "ULong"
                exp = "?" & ChangeTo.Substring(0, 2)
            Case "Short", "Integer", "Long", "Decimal"
                exp = "?" & ChangeTo.Chars(0)
            Case "Single"
                exp = "?!"
            Case "Double"
                exp = "?#"
            Case "Boolean"
                exp = "CBool(?)"
            Case "Char"
                exp = "Chr(?)"
            Case "Date"
                exp = "#1/1/?#"
            Case "String"
                exp = """?"""
            Case Else
                Stop
        End Select

        For i As Integer = 100 To 1 Step -1
            newText = newText.Replace(i & "I", exp.Replace("?", i.ToString))
        Next

        IO.File.WriteAllText(newFilename, newText)
    End Sub
End Module
