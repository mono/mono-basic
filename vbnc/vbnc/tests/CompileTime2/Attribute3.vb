Imports System.Runtime.CompilerServices
Imports System.Reflection

<Assembly: RuntimeCompatibility(WrapNonExceptionThrows:=True)> 

Class Attribute3
    Shared Function Main() As Integer
        Dim attr As RuntimeCompatibilityAttribute
        Dim a As Assembly = Assembly.GetExecutingAssembly

        If a.IsDefined(GetType(RuntimeCompatibilityAttribute), False) = False Then
            Return 1
        End If

        attr = TryCast(a.GetCustomAttributes(GetType(RuntimeCompatibilityAttribute), False)(0), RuntimeCompatibilityAttribute)

        If attr Is Nothing Then
            Return 2
        End If

        If attr.WrapNonExceptionThrows = False Then
            Return 3
        End If

        Return 0
    End Function
End Class