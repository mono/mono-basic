Public Interface SomeInterface

    Property Foo As Integer
    Property Bar As Integer

End Interface

Public Class SomeClass(Of T)
    Implements SomeInterface

    Public Function Foo() As Integer
        Return 42
    End Function

    Public Property ImplementsAutoProp As Integer = 56 Implements SomeInterface.Foo
    Public Property ImplementsAutoPropTwo As Integer Implements SomeInterface.Bar

    Public Property ExprInitialisedAutoProp As Integer = Foo() * 2
    Public Property FuncInitialisedAutoProp As Integer = Foo()
    Public Property LiteralInitialisedAutoProp As Integer = 123
    Public Property UnitialisedAutoProp As Integer
    Public Property GenericProp As T
		
    Public Property AutoPropNewInit As New String("Init")
		
    'Accepted without any diagnostic by VBC
    Public Property Prop As Integer = 42 = 42
		
End Class

Module MainModule

    Function Main() As Integer

        'Check the properties have the correct value initialised
        Dim Inst As New SomeClass(Of Integer)

        If Inst.ImplementsAutoProp <> 56 Then
            Return 1
        End If

        If Inst.ExprInitialisedAutoProp <> Inst.Foo() * 2 Then
            Return 1
        End If

        If Inst.FuncInitialisedAutoProp <> Inst.Foo() Then
            Return 1
        End If

        If Inst.LiteralInitialisedAutoProp <> 123 Then
            Return 1
        End If

        If Inst.AutoPropNewInit Is Nothing OrElse Inst.AutoPropNewInit <> "Init" Then
            Return 1
        End If

        Return 0

    End Function
	
End Module