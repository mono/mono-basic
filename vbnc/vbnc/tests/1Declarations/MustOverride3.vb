Class MustOverride3_Inheriter
    Inherits System.Reflection.ConstructorInfo


    Public Overrides ReadOnly Property Attributes() As System.Reflection.MethodAttributes
        Get

        End Get
    End Property

    Public Overrides ReadOnly Property DeclaringType() As System.Type
        Get

        End Get
    End Property

    Public Overloads Overrides Function GetCustomAttributes(ByVal inherit As Boolean) As Object()

    End Function

    Public Overloads Overrides Function GetCustomAttributes(ByVal attributeType As System.Type, ByVal inherit As Boolean) As Object()

    End Function

    Public Overrides Function GetMethodImplementationFlags() As System.Reflection.MethodImplAttributes

    End Function

    Public Overrides Function GetParameters() As System.Reflection.ParameterInfo()

    End Function

    Public Overloads Overrides Function Invoke(ByVal obj As Object, ByVal invokeAttr As System.Reflection.BindingFlags, ByVal binder As System.Reflection.Binder, ByVal parameters() As Object, ByVal culture As System.Globalization.CultureInfo) As Object

    End Function

    Public Overloads Overrides Function Invoke(ByVal invokeAttr As System.Reflection.BindingFlags, ByVal binder As System.Reflection.Binder, ByVal parameters() As Object, ByVal culture As System.Globalization.CultureInfo) As Object

    End Function

    Public Overrides Function IsDefined(ByVal attributeType As System.Type, ByVal inherit As Boolean) As Boolean

    End Function

    Public Overrides ReadOnly Property MethodHandle() As System.RuntimeMethodHandle
        Get

        End Get
    End Property

    Public Overrides ReadOnly Property Name() As String
        Get

        End Get
    End Property

    Public Overrides ReadOnly Property ReflectedType() As System.Type
        Get

        End Get
    End Property
End Class