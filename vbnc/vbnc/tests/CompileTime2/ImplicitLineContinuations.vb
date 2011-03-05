Imports System.Reflection

<Assembly: A("4.3.2.1")> 
<
Assembly: A("en")> 
<Assembly: A("4.3.2.1")
> 
<
Assembly: A("4.3.2.1")
> 

<Assembly: A(b:=
"b", a:=
"a")> 

Namespace Attributes
    <Obsolete("C")>
    Class Obs
        <Obsolete("S")>
        Sub S()
        End Sub

        <Obsolete("F")>
        Function F() As Integer
        End Function

        <Obsolete("P")>
        Property P As Object
            <Obsolete("PG")>
            Get
            End Get
            <Obsolete("PS")>
            Set(ByVal value As Object)
            End Set
        End Property

        <Obsolete("D")>
        Delegate Sub D()

        <obsolete("NE")>
        Enum E
            a
        End Enum

        <Obsolete("NS")>
        Structure NS
            Dim i As Integer
        End Structure
    End Class
    <
    Obsolete("s2")
    >
    Class S2

    End Class
End Namespace

Namespace AfterComma
    Module Comma
        <A("",
            "")>
        Public Function test1(ByVal x As Integer,
                          ByVal y As Object, ByVal z As Object)
            Return 1
        End Function
        Sub S(ByVal a As Integer,
              ByVal b As Short)
            Dim i,
                j As Integer(,
                    )
            test1(3,
                  4, 6)
            j(2,
                3) = 2
        End Sub
    End Module
End Namespace

Namespace Parenthesis
    Module Parenthesis
        <A(
            "")>
        Public Function test2(
                         ByVal x As Integer, ByVal y As Object, ByVal z As Object)
            Return 1
        End Function
        Public Function A(
                         ByVal x As Integer(
                                     ),
                                 ByVal y As Integer) As Short
            Dim i(,
                  ) As Integer
            test2(
                1, 2, 3)
            test2(
                1, i(
                    1, 2), 4)
            ReDim i(
                3, 4)

        End Function
        Sub S(
             )
        End Sub
        Delegate Sub D(
                      )

        Sub S(ByVal i As Integer
            )
        End Sub
    End Module
End Namespace

Namespace Braces
    Module Braces
        Dim i As Integer() = {
            1, 2}
        Dim j As Integer(,) = {
            {
                1,
                2},
     {
  1, 2}}
    End Module
End Namespace

Class AfterMemberQualifier
    Class A
        Implements AfterMemberQualifier.
            I
    End Class
    Class B
        Inherits AfterMemberQualifier.
            A
    End Class
    Interface I

    End Interface
    Interface I2
        Inherits AfterMemberQualifier.
            I
    End Interface
    Structure S
        Dim i As AfterMemberQualifier.
            I2
    End Structure
    Enum E As System.
        Byte
        e
    End Enum
    Delegate Sub D(ByVal i As Global.
                   system.
                   int32)
    Sub M()
        Dim i As system.
            int32
        Dim e As Object = aftermemberqualifier.
            E.
            e
    End Sub
End Class

Class BinaryOperators
    Dim i As Integer = 1 +
        2 -
        3 *
        4 /
        5 \
        6 Mod
        7 And
        8 Or
        9 Xor
        10 ^
        1 &
        2 >>
        3 <<
        4 =
        5 >
        6 <
        7 <=
        8 >=
        9 <>
        10 Like
        3 AndAlso
        4 OrElse
        5
    Dim i2 As Boolean = "" Is
        "?"
    Dim i3 As Boolean = "?" IsNot
        "!"
End Class

Class AssignmentOperators
    Dim i As Integer = 2
    Const c As Integer =
        3
    Sub S()
        Dim k As Integer
        k =
            1
        k +=
            2
        k -=
            3
        k /=
            4
        k \=
            5
        k <<=
            6
        k >>=
            7
        k ^=
            8
        k &=
            9

        S2(b:=
            2, a:=
            1)
        For i As Integer =
            2 To 3
        Next
    End Sub
    Sub S2(ByVal a As Integer, ByVal b As Integer)
    End Sub
    Sub S3(Optional ByVal a As Integer =
           3)
        Dim i As Integer =
            3
    End Sub
End Class

Public Class Misc
    Sub S()
        Dim o As Object
        Dim b As Boolean
        Dim i As Integer

        b = TypeOf o Is
            Object
        b = o Is
            o
        b = o IsNot
            o

        Select Case i
            Case Is
                = 2
            Case Is
                =
                3
            Case =
3
        End Select

    End Sub
End Class

<AttributeUsage(System.AttributeTargets.All, AllowMultiple:=True)>
Class AAttribute
    Inherits System.Attribute
    Public a As String
    Public b As String
    Sub New()
    End Sub

    Sub New(ByVal a As String)
    End Sub

    Sub New(ByVal a As String, ByVal b As String)
    End Sub
End Class

