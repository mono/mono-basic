Imports system
Imports System.Diagnostics
Class UsingStatement3

    Sub Test()
        'Extracts all code files from the vbproj file.
        Const filename As String = "vbnc.vbproj"
        Using x As New Xml.XmlTextReader(filename)
            Dim i As Integer
            While x.Read()
                If x.Name = "Compile" AndAlso x.NodeType = Xml.XmlNodeType.Element Then
                    x.MoveToAttribute("Include")
                    Debug.WriteLine("""" & IO.Path.Combine("..\source\", x.Value) & """")
                    i += 1
                End If
            End While
            Debug.WriteLine("Total: " & i.ToString)
        End Using
    End Sub
End Class