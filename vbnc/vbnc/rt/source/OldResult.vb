Public Class OldResult
    Private m_Filename As String

    Sub New(ByVal Filename As String)
        m_Filename = Filename
    End Sub

    ReadOnly Property Result() As Test.Results
        Get
            Dim tmp As String
            Dim value As Test.Results

            Dim file As String = IO.Path.GetFileNameWithoutExtension(m_Filename)

            Dim iEnd As Integer
            iEnd = file.LastIndexOf("."c)
            If iEnd > 0 AndAlso System.Enum.IsDefined(GetType(Test.Results), file.Substring(iEnd + 1)) Then
                tmp = file.Substring(iEnd + 1)
            Else
                Dim xml As New Xml.XPath.XPathDocument(m_Filename)
                Dim navigator As Xml.XPath.XPathNavigator
                Dim node As Xml.XPath.XPathNavigator

                navigator = xml.CreateNavigator
                node = navigator.SelectSingleNode("Test/Result")

                tmp = node.Value

                Try
                    Dim destFile As String
                    destFile = IO.Path.Combine(IO.Path.GetDirectoryName(m_Filename), file & "." & tmp & ".testresult")
                    IO.File.Move(m_Filename, destFile)
                    m_Filename = destFile
                Catch

                End Try
            End If

            value = CType([Enum].Parse(GetType(Test.Results), tmp), Test.Results)

            Return value
        End Get
    End Property

    ReadOnly Property Verification() As String
        Get
            Return "(not implemented)"
        End Get
    End Property

    ReadOnly Property Compiler() As String
        Get
            Dim tmp As String

            Dim xml As New Xml.XmlDocument
            Dim node As Xml.XmlNode
            xml.Load(m_Filename)
            node = xml.SelectSingleNode("Test/Compiler")
            tmp = node.InnerText

            Return tmp
        End Get
    End Property

    ReadOnly Property Text() As String
        Get
            Return IO.File.ReadAllText(m_Filename)
        End Get
    End Property
End Class
