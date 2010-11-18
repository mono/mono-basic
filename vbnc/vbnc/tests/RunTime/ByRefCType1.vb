Module Application
    Function ReturnByRef(ByRef aInOut As UInt16) As UInt32
    End Function

    Sub Main()
        Try
            REM            Dim res As UInt16 = 42
            REM            ReturnByRef(res)
            ReturnByRef(CType(42, UInt16))
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub
End Module