Imports System.Reflection

<Assembly: A("4.3.2.1")> 
<
Assembly: A("en")> 
<Assembly: A("4.3.2.1")
> 
<
Assembly: A("4.3.2.1")
> 

Namespace ImplicitLineContinuationsAttributes
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

<AttributeUsage(System.AttributeTargets.All, AllowMultiple:=True)>
Class AAttribute
    Inherits System.Attribute

    Sub New()
    End Sub

    Sub New(ByVal obj As String)
    End Sub
End Class