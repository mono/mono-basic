Imports System.Resources
Imports System.Xml

''' <summary>
'''
''' </summary>
Class Messages
	'The store for the messages
	Private m_Messages As New Hashtable

	Private Const FILEWARNING As String = _
	"'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''" & vbNewLine & _
	"'    This file has automatically been generated                            " & vbNewLine & _
	"'    Any change in this file will be lost!!                                " & vbNewLine & _
	"'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''" & vbNewLine


	Private COMMENT As String = FILEWARNING

	''' <summary>
	''' Add the specified message 
	''' </summary>
	Sub Add(ByVal Msg As Message)
		If Contains(Msg.ID) Then Return
		m_Messages.Add(Msg.ID, Msg)
	End Sub

	''' <summary>
	''' Is this message here?
	''' </summary>
	Function Contains(ByVal ID As Integer) As Boolean
		Return m_Messages.ContainsKey(ID)
	End Function

	''' <summary>
	''' Lookup the message of the specified ID
	''' </summary>
	Default ReadOnly Property Item(ByVal ID As Integer) As Message
		Get
			Return CType(m_Messages(ID), Message)
		End Get
	End Property

	''' <summary>
	''' The number of messages.
	''' </summary>
	ReadOnly Property Count() As Integer
		Get
			Return m_Messages.Count
		End Get
	End Property

	''' <summary>
	''' Returns an ordered array of messages (ordered by ID)
	''' </summary>
	Function ToArray() As Message()
		Dim result() As Message
		ReDim result(Count - 1)

		m_Messages.Values.CopyTo(result, 0)
		Array.Sort(result)

		Return result
	End Function

	''' <summary>
	''' Creates a ResX file of the messages
	''' </summary>
	Sub CreateResXFile(ByVal filename As String)
		Dim writer As New ResXResourceWriter(filename)
		writer.addresource("warning", FILEWARNING)
		For Each msg As Message In ToArray()
			writer.addresource(msg.ID.ToString, msg.VBNCMsg)
		Next
		writer.generate()
		writer.close()
	End Sub

	''' <summary>
	''' Loads the specified text file of format: ID "Message"
	''' if IsVBNCMessages = true then the message strings are loaded as VBNCMsg, 
	''' otherwise they are loaded as VBMsg
	''' </summary>
	Sub LoadFile(ByVal contents As String, ByVal IsVBNCMessages As Boolean)
		Dim lines As String() = Split(contents, vbNewLine)

		For i As Integer = 0 To UBound(lines)
			Dim line As String
			line = lines(i).Trim()
			If line.StartsWith("#") Then GoTo nextline
			If line = "" Then GoTo nextline

			Dim strid, value As String
			Dim id As Integer
			Dim splitter As Integer = line.IndexOf(" "c)

			If splitter = -1 Then Throw New ArgumentOutOfRangeException(String.Format("Malformed line (line #{0}): {1}", i.ToString, line))

            strid = Mid(line, 1, splitter)
            id = CInt(strid)
            'Console.WriteLine("Read ID=" & strid & ", converted into: " & id.ToString())
			value = Trim(Mid(line, splitter + 1))
			If value.StartsWith("""") AndAlso value.EndsWith("""") Then
				value = Mid(value, 2, value.Length - 2)
			End If

			Dim msg As Message
			If Contains(id) Then
				msg = Item(id)
			Else
				msg = New Message(id, "")
				Add(msg)
			End If
			If IsVBNCMessages Then
				msg.VBNCMsg = value
			Else
				msg.VBMsg = value
			End If
nextline:
		Next
	End Sub
	''' <summary>
	''' Loads the specified xml file of format:
	'''   (Messages)
	'''		(Message id="123" level="Error")
	'''			(Comment)Some comment(/Comment)
	'''			(VBNCValue)VBNC message(/VBNCValue)
	'''			(VBValue)VB message(/VBValue)
	'''		(/Message)
	'''   (/Messages)
	''' </summary>
	Sub LoadXmlFile(ByVal Filename As String)
		Dim xml As New System.Xml.XmlTextReader(Filename)
        xml.WhitespaceHandling = WhitespaceHandling.Significant

		Try
			While xml.Read
				If xml.NodeType = System.Xml.XmlNodeType.Comment Then
					'do nothing
					COMMENT = xml.Value
				ElseIf xml.NodeType = System.Xml.XmlNodeType.XmlDeclaration Then
					'do nothing
				ElseIf xml.Name = "Messages" Then
					'do nothing
				ElseIf xml.Name = "Message" Then
					Dim id As Integer, level As String
                    id = CInt(xml.GetAttribute("id"))
					level = xml.GetAttribute("level")
                    'Console.WriteLine("Read ID=" & xml.GetAttribute("id") & ", level: " & level & ", converted into: " & id.ToString())
					xml.Read()
                    Dim msg As New Message(id, level)
					While xml.Name <> "Message"
						If xml.Name = "ChangedValue" OrElse xml.Name = "VBNCValue" Then
							xml.Read()
							msg.VBNCMsg = xml.Value
							xml.Read()
						ElseIf xml.Name = "VBValue" Then
							xml.Read()
							msg.VBMsg = xml.Value
							xml.Read()
						ElseIf xml.Name = "Comment" Then
							If xml.IsEmptyElement = False Then
								xml.Read()
								msg.Comment = xml.Value
								xml.Read()
							End If
						Else
							Throw New ArgumentOutOfRangeException(String.Format("The xml file '{0}' has an invalid format.", Filename))
						End If
						xml.Read()
					End While
					Add(msg)
				Else
					Throw New ArgumentOutOfRangeException(String.Format("The xml file '{0}' has an invalid format.", Filename))
				End If
			End While
		Catch ex As Exception
			Throw
		End Try
	End Sub

	''' <summary>
	''' Creates an xml file according to the specified xml format:
	'''   (Messages)
	'''		(Message id="123" level="Error")
	'''			(ChangedValue)VBNC message(/ChangedValue)
	'''			(VBValue)VB message(/VBValue)
	'''		(/Message)
	'''   (/Messages)
	''' </summary>
	Function GetXml() As String
		Dim str As New IO.MemoryStream()
		Dim strWriter As New IO.StreamWriter(str)
		Dim xml As New System.Xml.XmlTextWriter(strWriter)
        xml.Formatting = Formatting.Indented

		xml.WriteStartDocument()
		xml.WriteComment(COMMENT)

		xml.WriteStartElement("Messages")
		For Each msg As Message In ToArray()
			xml.WriteStartElement("Message")
			xml.WriteAttributeString("id", msg.ID.ToString)
			xml.WriteAttributeString("level", msg.Level)
			xml.WriteStartElement("Comment")
			If msg.Comment = "" Then
				xml.WriteString("NC")
			Else
				xml.WriteString(msg.Comment)
			End If
			xml.WriteEndElement()
			xml.WriteStartElement("VBNCValue")
			xml.WriteString(msg.VBNCMsg)
			xml.WriteEndElement()
			xml.WriteStartElement("VBValue")
			xml.WriteString(msg.VBMsg)
			xml.WriteEndElement()
			xml.WriteEndElement()
		Next
		xml.WriteEndElement()
		xml.WriteEndDocument()

		xml.Flush()
		strWriter.Flush()
		str.Flush()

		Dim reader As New IO.StreamReader(str)
		str.Position = 0
		Dim result As String = reader.ReadToEnd
		reader.Close()
		str.Close()
		Return result
	End Function

	''' <summary>
	''' Creates vb code for the messages.
	''' </summary>
	Function GetCode() As String
		Dim c As New System.Text.StringBuilder

		Try
			Dim filename As String
            filename = "License.txt"
			Dim entireFilename, prevFilename As String
			entireFilename = IO.Path.GetFullPath(filename)
			Do Until IO.File.Exists(entireFilename)
				prevFilename = entireFilename
				filename = "..\" & filename
				entireFilename = IO.Path.GetFullPath(filename)
				If entireFilename = prevFilename Then Exit Do
			Loop

			If IO.File.Exists(filename) Then
				Dim license As String
				license = ResGen.ReadFile(filename)
				c.Append(license)
			Else
				MsgBox("Could not add license to code, license.txt wasn't found.")
			End If
		Catch ex As IO.IOException
			MsgBox("Could not add license to code: " & ex.Message)
		End Try
		c.Append(FILEWARNING)
		c.Append(vbNewLine)
        c.Append("Public Enum Messages" & vbNewLine)
        For Each msg As Message In Me.ToArray()
            c.Append("    ''' <summary>" & vbNewLine)
            c.Append("    ''' VBNC = """ & XMLEncode(msg.VBNCMsg) & """" & vbNewLine)
            c.Append("    ''' VB   = """ & XMLEncode(msg.VBMsg) & """" & vbNewLine)
            c.Append("    ''' </summary>" & vbNewLine)
            c.Append("    ''' <remarks></remarks>" & vbNewLine)
            c.Append("    <Message(MessageLevel." & msg.Level & ")> VBNC" & msg.ID.ToString & " = " & msg.ID.ToString & vbNewLine)
            c.Append("" & vbNewLine)
            'Console.WriteLine("Written ID=" & msg.ID.ToString)
        Next
		c.Append("End Enum" & vbNewLine)

		Return c.ToString
	End Function


	''' <summary>
	''' Encode a string for display in XML
	''' </summary>
	Function XMLEncode(ByVal str As String) As String
		Dim result As New System.Text.StringBuilder(str)
		result.Replace("&", "&amp;")
		result.Replace("<", "&lt;")
		result.Replace(">", "&gt;")
		'		result.Replace("=", "&eq;")
		Return result.ToString
	End Function
End Class


