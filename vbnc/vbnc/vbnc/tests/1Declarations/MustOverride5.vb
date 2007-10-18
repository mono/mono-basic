Class MustOverride5_Inheriter
    Inherits System.Reflection.Binder

    Public Overrides Function BindToField(ByVal bindingAttr As System.Reflection.BindingFlags, ByVal match() As System.Reflection.FieldInfo, ByVal value As Object, ByVal culture As System.Globalization.CultureInfo) As System.Reflection.FieldInfo

    End Function

    Public Overrides Function BindToMethod(ByVal bindingAttr As System.Reflection.BindingFlags, ByVal match() As System.Reflection.MethodBase, ByRef args() As Object, ByVal modifiers() As System.Reflection.ParameterModifier, ByVal culture As System.Globalization.CultureInfo, ByVal names() As String, ByRef state As Object) As System.Reflection.MethodBase

    End Function

    Public Overrides Function ChangeType(ByVal value As Object, ByVal type As System.Type, ByVal culture As System.Globalization.CultureInfo) As Object

    End Function

    Public Overrides Sub ReorderArgumentArray(ByRef args() As Object, ByVal state As Object)

    End Sub

    Public Overrides Function SelectMethod(ByVal bindingAttr As System.Reflection.BindingFlags, ByVal match() As System.Reflection.MethodBase, ByVal types() As System.Type, ByVal modifiers() As System.Reflection.ParameterModifier) As System.Reflection.MethodBase

    End Function

    Public Overrides Function SelectProperty(ByVal bindingAttr As System.Reflection.BindingFlags, ByVal match() As System.Reflection.PropertyInfo, ByVal returnType As System.Type, ByVal indexes() As System.Type, ByVal modifiers() As System.Reflection.ParameterModifier) As System.Reflection.PropertyInfo

    End Function
End Class