Imports System
Imports System.Reflection

Public Class ParameterAttributes1
    Shared Function Main() As Integer
        Return Test()
    End Function

    Shared Function Test(ByVal ParamArray p() As Object) As Integer
        Dim param As parameterinfo
        Dim method As methodinfo

        method = CType(methodinfo.getcurrentmethod, methodinfo)
        param = method.getparameters()(0)

        If param.isdefined(GetType(paramarrayattribute), False) Then
            Return 0
        Else
            Return 1
        End If
    End Function
End Class