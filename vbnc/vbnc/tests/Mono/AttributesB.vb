Imports System
'Testing 'Inherited' in attributes
<AttributeUsage(AttributeTargets.Class, AllowMultiple:=True, Inherited:=True)> _
Public Class AuthorAttribute
    Inherits Attribute
    Public Name As Object
    Public Sub New(ByVal Name As String)
        Me.Name = Name
    End Sub
    Public ReadOnly Property NameP() As String
        Get
            Return CStr(Name)
        End Get
    End Property
End Class


<Author("Robin Cook"), Author("Authur Haily")> _
Public Class C1

    Public Function S1() As Object
    End Function

End Class

Public Class C2
    Inherits C1

End Class

Module Test
    Function Main() As Integer

    End Function
End Module
