Imports System
Imports System.Collections
Imports System.Reflection

Namespace MethodResolution6
    Class Test
        Shared Function Main() As Integer
            Dim vt As Type = GetType(Date)
            Dim tmp As Object
            Dim m_ConstantValue As Object = 1L
            tmp = Activator.CreateInstance(vt, New Object() {m_ConstantValue})
            Return CInt(TypeOf tmp Is Date) + 1
        End Function
    End Class
End Namespace