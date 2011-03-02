Option Strict On
Option Explicit On

Public Class StaticCreateDelegateToSharedMethod
    Public Shared Sub Main()
        Console.WriteLine(m_foo)
    End Sub

    '
    ' A selection of instance members
    '
    Public Sub InstanceMethodA(ByVal sender As Object, ByVal e As EventArgs)
        Console.WriteLine("curious!")
    End Sub

    Public Function Foo() As int32
        Return &H45
    End Function

    Public Property Bar() As int32
        Get
            Return m_bar
        End Get
        Set(ByVal value As int32)
            m_bar = value
        End Set
    End Property

    Public Event WizBangChanged As EventHandler

    Private m_bar As Integer
    Public m_foo As Int32 = 99
    Public m_here As String = "here"

End Class