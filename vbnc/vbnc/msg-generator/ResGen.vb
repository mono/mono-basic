Imports System.Xml
Imports System.IO

Class ResGen
	Shared Sub ShowHelp()
		Console.WriteLine("First /mergesource is loaded (if specified) - must be an xml file.")
        Console.WriteLine("Second /mergeinvb is loaded (if specified).")
		Console.WriteLine("Third /mergeinvbnc is loaded (if specified).")
		Console.WriteLine(" -- either mergesource, mergeinoriginal or mergeinchanged must be specified.")
		Console.WriteLine("If /outvb is specified, write messages to a vb code file.")
		Console.WriteLine("If /outxml is specified, write messages to xml file.")
		Console.WriteLine("If /outresx is specified, write messages to a resx file.")
	End Sub
	''' <summary>
	''' The main entry point.
	''' </summary>
	Shared Function Main(ByVal cmdArgs() As String) As Integer
		Try
			Dim MergeInVBNC As String = ""
			Dim MergeInVB As String = ""
			Dim MergeSource As String = ""
			Dim OutVB As String = ""
			Dim OutResX As String = ""
			Dim OutXml As String = ""

			If cmdArgs.Length < 1 Then
				Console.WriteLine("There must be at least one argument.") : Return 1
			End If

			For Each str As String In cmdArgs
				str = str.Trim()
				If str.StartsWith("/") OrElse str.StartsWith("-") Then
					Dim name, value As String
					Dim split As Integer
					str = Mid(str, 2)
					split = str.IndexOf(":")
					If split > 0 Then
						name = Mid(str, 1, str.IndexOf(":"))
						value = Mid(str, str.IndexOf(":") + 2)
					Else
						name = str
						value = ""
					End If
					Select Case name
						Case "mergeinvbnc"
							MergeInVBNC = IO.Path.GetFullPath(value)
						Case "mergeinvb"
							MergeInVB = IO.Path.GetFullPath(value)
						Case "mergesource"
							MergeSource = IO.Path.GetFullPath(value)
						Case "outxml"
							OutXml = IO.Path.GetFullPath(value)
						Case "outvb"
							OutVB = IO.Path.GetFullPath(value)
						Case "outresx"
							OutResX = IO.Path.GetFullPath(value)
						Case "stop"
							Stop
						Case "?", "help"
							ShowHelp()
							Return 0
						Case Else
							Console.WriteLine("Unrecognized commandline argument: " & str)
							Return 1
					End Select
				End If
			Next

			If MergeSource = "" AndAlso MergeInVB = "" AndAlso MergeInVBNC = "" Then
				Console.WriteLine("Either '/mergesource' or '/mergeinvb' or '/mergeinvbnc' must be specified.")
			End If

			Dim messages As New Messages
			If MergeSource <> "" Then messages.LoadXmlFile(MergeSource) : Console.WriteLine("Loaded file " & MergeSource)
			If MergeInVB <> "" Then messages.LoadFile(ReadFile(MergeInVB), False) : Console.WriteLine("Loaded file " & MergeInVB)
			If MergeInVBNC <> "" Then messages.LoadFile(ReadFile(MergeInVBNC), True) : Console.WriteLine("Loaded file " & MergeInVBNC)

			If messages.Count = 0 Then
				Console.WriteLine("No messages was found.")
				Return 0
			End If

			If OutVB <> "" Then WriteFile(OutVB, messages.GetCode) : Console.WriteLine("Written file " & OutVB)
			If OutXml <> "" Then WriteFile(OutXml, messages.GetXml) : Console.WriteLine("Written file " & OutXml)
			If OutResX <> "" Then messages.CreateResXFile(OutResX) : Console.WriteLine("Written file " & OutResX)

			Return 0
		Catch ex As Exception
			Console.WriteLine("Unhandled exception: " & ex.Message)
			Console.WriteLine(ex.StackTrace)
			Return 1
		End Try
	End Function

	Shared Sub WriteFile(ByVal Filename As String, ByVal Contents As String)
		Dim writer As New StreamWriter(Filename)
		writer.Write(Contents)
		writer.Close()
    End Sub

	Shared Function ReadFile(ByVal Filename As String) As String
		Dim reader As New StreamReader(Filename)
		Dim text As String = reader.ReadToEnd
		reader.Close()
		Return text
	End Function
End Class