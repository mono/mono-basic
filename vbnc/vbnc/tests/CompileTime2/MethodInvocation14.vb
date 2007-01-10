Imports System
Imports System.Collections
Imports System.Reflection

Namespace MethodInvocation14
    Class Test

        Sub New(ByVal ParamArray v() As MemberInfo)

        End Sub

        Shared Function Main() As Integer
            Dim ctors As ConstructorInfo()
            Dim t2 As New Test(ctors)
            Return 0
        End Function
    End Class
End Namespace