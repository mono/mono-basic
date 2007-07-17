Imports System
Imports System.Reflection
Imports Microsoft.VisualBasic.CompilerServices

Public Class ParameterAttributes2
    Shared Function Main() As Integer
        Return Test(1)
    End Function

    Shared Function Test(<OptionCompare()> ByVal value As Integer) As Integer
        Dim param As parameterinfo
        Dim method As methodinfo

        method = CType(methodinfo.getcurrentmethod, methodinfo)
        param = method.getparameters()(0)

        If param.isdefined(GetType(OptionCompareAttribute), False) Then
            Return 0
        Else
            Return 1
        End If
    End Function
End Class