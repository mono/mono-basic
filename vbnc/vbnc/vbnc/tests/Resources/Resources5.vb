Imports System.Resources

Class Resources5
    Shared Function Main() As Integer
        Dim m_Resources As ResourceManager
        Dim result As String

        Try
            m_Resources = New ResourceManager("Resources", System.Reflection.Assembly.GetExecutingAssembly())
            result = m_Resources.GetObject("2005")
            Return 0
        Catch ex As Exception
            Console.WriteLine("Exception: {0}", ex.ToString())
            Return 1
        End Try
    End Function
End Class