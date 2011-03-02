Option Strict On
Option Explicit On


Public Class StaticCreateDelegateToSharedMethod

    Public Shared Sub Main()
        ' All lines here should fail with BC30369:
        ' "Cannot refer to an instance member of a class from within a shared method or shared member initializer without an explicit instance of the class."
        'Console.WriteLine(m_foo)
        'Console.WriteLine(Foo())
        Dim eh As New EventHandler(AddressOf SharedMethodA)
        AddHandler WizBangChanged, eh
        'Bar = 100
        '' This causes a very wierd error in MSFT's vbc, it reports overload
        'resolution()
        '' failed on WriteLine, but reports each overload alongwith the message
        'from(BC30369)
        'Console.WriteLine(Bar)
    End Sub

    '
    ' A selection of instance members
    '
    Public Sub InstanceMethodA(ByVal sender As Object, ByVal e As EventArgs)
        Console.WriteLine("curious!")
    End Sub
    Public Shared Sub SharedMethodA(ByVal sender As Object, ByVal e As EventArgs)
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


'Class AnyDifferenceWhenAccessFromAnotherClass
'    Sub Accesses()
'        ' As above.
'        Console.WriteLine(StaticCreateDelegateToSharedMethod.m_foo)
'        Console.WriteLine(StaticCreateDelegateToSharedMethod.Foo())
'        Dim eh As New EventHandler(AddressOf StaticCreateDelegateToSharedMethod.InstanceMethodA)
'        AddHandler StaticCreateDelegateToSharedMethod.WizBangChanged, eh
'        StaticCreateDelegateToSharedMethod.Bar = 100
'        Console.WriteLine(StaticCreateDelegateToSharedMethod.Bar)
'    End Sub
'End Class