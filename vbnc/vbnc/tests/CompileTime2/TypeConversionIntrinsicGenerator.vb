#If GENERATOR Then
Imports System.IO

Module TypeConversionIntrinsicGenerator
    '0 = A Empty          A
    '1 = B Object         B
    '2 = C DBNull         C
    '3 = D Boolean        D
    '4 = E Char           E
    '5 = F SByte          F
    '6 = G Byte           G
    '7 = H Int16(Short)   H
    '8 = I UInt16(UShort) I
    '9 = J Int32          J
    '10= K UInt32         K 
    '11= L Int64(Long)    L
    '12= M UInt64(ULong)  M
    '13= N Single         N
    '14= O Double         O
    '15= P Decimal        P
    '16= Q DateTime       Q
    '17= - 17             -
    '18= S String         S

    ''' <summary>
    ''' X=?
    ''' I=Implicit ok
    ''' 0=Explicit ok
    ''' 1=30311
    ''' 2=32007
    ''' 3=30533
    ''' 4=32006
    ''' 5=30532
    ''' 6=30533
    ''' A=30311, only explicit
    ''' </summary>
    ''' <remarks></remarks>
    Public ConversionResultType As String = _
            "XXXXXXXXXXXXXXXXX-X" & _
            "X0X00000000000000-0" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XIXI1000000000001-0" & _
            "XIX1I222222221111-0" & _
            "XIX04I00000000001-0" & _
            "XIX040I0000000001-0" & _
            "XIX0400I000000001-0" & _
            "XIX04000I00000001-0" & _
            "XIX040000I0000001-0" & _
            "XIX0400000I000001-0" & _
            "XIX04000000I00001-0" & _
            "XIX040000000I0001-0" & _
            "XIX0100000000I001-0" & _
            "XIX01000000000I05-0" & _
            "XIX010000000000I1-0" & _
            "XIX1111111111161I-0" & _
            "-------------------" & _
            "XIX00000000000000-I"

    Dim types() As TypeCode = New TypeCode() {TypeCode.Boolean, TypeCode.Byte, TypeCode.SByte, TypeCode.Int16, TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt64, TypeCode.Decimal, TypeCode.Single, TypeCode.Double, TypeCode.Char, TypeCode.String, TypeCode.DateTime, TypeCode.DBNull, TypeCode.Object}

    Private Function GetConv(ByVal op1 As TypeCode, ByVal op2 As TypeCode) As Char
        Return ConversionResultType.Chars(op1 + op2 * 19)
    End Function

    Sub Main()
        Generate("D:\Documentos\Rolf\Proyectos\VB.NET\vbnc\mono-basic\vbnc\vbnc\tests\CompileTime2")
    End Sub

    Sub Generate(ByVal dir As String)
        For i As Integer = 0 To types.Count - 1
            Dim tc As TypeCode = types(i)
            If types(i) = TypeCode.Empty Then Continue For
            Using fs As New FileStream(Path.Combine(dir, "TypeConversionIntrinsic" + tc.ToString() + ".vb"), FileMode.Create)
                Generate(fs, tc)
            End Using
        Next
    End Sub

    Sub Generate(ByVal stream As Stream, ByVal tc As TypeCode)
        Using w As New StreamWriter(stream)
            w.WriteLine("Class TypeConversionIntrinsic")

            w.WriteLine("    Sub LocalVariables")
            WriteVariables(w, "", "i", "        Dim ", Environment.NewLine)
            WriteVariables(w, "", "o", "        Dim ", Environment.NewLine)
            WriteConversions(w, tc)
            w.WriteLine("    End Sub")

            w.Write("    Sub InLocalOutRef(")
            WriteVariables(w, "ByRef", "o", "", ")" + Environment.NewLine)
            WriteVariables(w, "", "i", "        Dim ", Environment.NewLine)
            WriteConversions(w, tc)
            w.WriteLine("    End Sub")

            w.Write("    Sub InRefOutLocal(")
            WriteVariables(w, "ByRef", "i", "", ")" + Environment.NewLine)
            WriteVariables(w, "", "o", "        Dim ", Environment.NewLine)
            WriteConversions(w, tc)
            w.WriteLine("    End Sub")

            w.Write("    Sub InRefOutRef(")
            WriteVariables(w, "ByRef", "i", "", ", ")
            WriteVariables(w, "ByRef", "o", "", ")" + Environment.NewLine)
            WriteConversions(w, tc)
            w.WriteLine("    End Sub")

            w.WriteLine("End Class")
        End Using
    End Sub

    Sub WriteVariables(ByVal w As StreamWriter, ByVal access As String, ByVal prefix As String, ByVal pre As String, ByVal post As String)
        w.Write("{2}{0} {1}_bool As Boolean, {0} {1}_byte As Byte, {0} {1}_sbyte As SByte, {0} {1}_short As Short, {0} {1}_ushort As UShort, {0} {1}_int As Integer, {0} {1}_uint as UInteger, {0} {1}_long As Long, {0} {1}_ulong As ULong, {0} {1}_dec As Decimal, {0} {1}_sng As Single,{0} {1}_dbl As Double, {0} {1}_chr As Char, {0} {1}_str As String, {0} {1}_dt As Date, {0} {1}_dbnull As DBNull, {0} {1}_obj As Object{3}", access, prefix, pre, post)
    End Sub

    Sub WriteConversions(ByVal w As StreamWriter, ByVal tc As TypeCode)
        Dim suffix() As String

        ReDim suffix(20)

        suffix(TypeCode.Boolean) = "bool"
        suffix(TypeCode.Byte) = "byte"
        suffix(TypeCode.SByte) = "sbyte"
        suffix(TypeCode.Int16) = "short"
        suffix(TypeCode.UInt16) = "ushort"
        suffix(TypeCode.Int32) = "int"
        suffix(TypeCode.UInt32) = "uint"
        suffix(TypeCode.Int64) = "long"
        suffix(TypeCode.UInt64) = "ulong"
        suffix(TypeCode.Decimal) = "dec"
        suffix(TypeCode.Single) = "sng"
        suffix(TypeCode.Double) = "dbl"
        suffix(TypeCode.String) = "str"
        suffix(TypeCode.Char) = "chr"
        suffix(TypeCode.Object) = "obj"
        suffix(TypeCode.DateTime) = "dt"
        suffix(TypeCode.DBNull) = "dbnull"

        For i As Integer = 0 To types.Length - 1
            For o As Integer = 0 To types.Length - 1
                Dim c As Char

                If types(i) = TypeCode.Empty OrElse types(o) = TypeCode.Empty Then Continue For
                If types(i) <> tc Then Continue For

                c = GetConv(types(i), types(o))
                If c = "I" Then
                    w.WriteLine("        i_{0} = o_{1}", suffix(types(i)), suffix(types(o)))
                ElseIf c = "0" Then
                    w.WriteLine("#If Not STRICT Or ERRORS")
                    w.WriteLine("        i_{0} = o_{1}", suffix(types(i)), suffix(types(o)))
                    w.WriteLine("#End If")
                Else
                    w.WriteLine("#If ERRORS")
                    w.WriteLine("        i_{0} = o_{1}", suffix(types(i)), suffix(types(o)))
                    w.WriteLine("#End If")
                End If
            Next
        Next
    End Sub
End Module
#End If

