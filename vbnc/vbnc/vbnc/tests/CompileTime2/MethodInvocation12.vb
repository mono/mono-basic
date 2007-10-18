Imports System
Imports System.Collections
Imports System.Reflection

Namespace MethodInvocation12
    Class Test
        Shared Function Main() As Integer
            Emit(GetBytes(CSByte(1))(0))
            Return 0
        End Function
        Shared Function GetBytes(ByVal value As SByte) As Byte()
            Return New Byte() {1}
        End Function
        Shared Sub Emit(ByVal value As Byte)

        End Sub
    End Class
End Namespace