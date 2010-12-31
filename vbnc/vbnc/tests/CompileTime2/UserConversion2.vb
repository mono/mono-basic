Option Strict Off

Imports System
Imports System.Collections
Imports System.Reflection

Class UserConversion1
    Shared Function Main() As Integer
        Dim wstr As New WString
        Dim str As String
        str = CStr(wstr)
        str = CType(wstr, String)
        str = wstr

        Dim wb As New WByte
        Dim b As Byte
        b = CByte(wb)
        b = CType(wb, Byte)
        b = wb

        Dim wsb As New WSByte
        Dim sb As SByte
        sb = CSByte(wsb)
        sb = CType(wsb, SByte)
        sb = wsb

        Dim ws As New WShort
        Dim s As Short
        s = CShort(ws)
        s = CType(ws, Short)
        s = ws

        Dim wus As New WUShort
        Dim us As UShort
        us = CUShort(wus)
        us = CType(wus, UShort)
        us = wus

        Dim wi As New WInteger
        Dim i As Integer
        i = CInt(wi)
        i = CType(wi, Integer)
        i = wi

        Dim wui As New WUInteger
        Dim ui As UInteger
        ui = CUInt(wui)
        ui = CType(wui, UInteger)
        ui = wui

        Dim wl As New WLong
        Dim l As Long
        l = CLng(wl)
        l = CType(wl, Long)
        l = wl

        Dim wul As New WULong
        Dim ul As ULong
        ul = CULng(wul)
        ul = CType(wul, ULong)
        ul = wul

        Dim wf As New WSingle
        Dim f As Single
        f = CSng(wf)
        f = CType(wf, Single)
        f = wf

        Dim wr As New WDouble
        Dim r As Double
        r = CDbl(wr)
        r = CType(wr, Double)
        r = wr

        Dim wdec As New WDecimal
        Dim dec As Decimal
        dec = CDec(wdec)
        dec = CType(wdec, Decimal)
        dec = wdec

        Dim wbool As New WBoolean
        Dim bool As Boolean
        bool = CBool(wbool)
        bool = CType(wbool, Boolean)
        bool = wbool

        Dim wc As New WChar
        Dim c As Char
        c = CChar(wc)
        c = CType(wc, Char)
        c = wc

        Dim wdt As New WDate
        Dim dt As Date
        dt = CDate(wdt)
        dt = CType(wdt, Date)
        dt = wdt

        Dim a1 As wa1
        Dim b1 As wb1
        b1 = CType(a1, wb1)
        b1 = a1

        Dim a2 As wa2
        Dim b2 As wb2
        b2 = CType(a2, wb2)
        b2 = a2

        Return 0
    End Function

    Class WString
        Shared Narrowing Operator CType(ByVal v As WString) As String
            Return ""
        End Operator
    End Class
    Class WByte
        Shared Narrowing Operator CType(ByVal v As WByte) As Byte
            Return 0
        End Operator
    End Class
    Class WSByte
        Shared Narrowing Operator CType(ByVal v As WSByte) As SByte
            Return 0
        End Operator
    End Class
    Class WShort
        Shared Narrowing Operator CType(ByVal v As WShort) As Short
            Return 0
        End Operator
    End Class
    Class WUShort
        Shared Narrowing Operator CType(ByVal v As WUShort) As UShort
            Return 0
        End Operator
    End Class
    Class WInteger
        Shared Narrowing Operator CType(ByVal v As WInteger) As Integer
            Return 0
        End Operator
    End Class
    Class WUInteger
        Shared Narrowing Operator CType(ByVal v As WUInteger) As UInteger
            Return 0
        End Operator
    End Class
    Class WLong
        Shared Narrowing Operator CType(ByVal v As WLong) As Long
            Return 0
        End Operator
    End Class
    Class WULong
        Shared Narrowing Operator CType(ByVal v As WULong) As ULong
            Return 0
        End Operator
    End Class
    Class WSingle
        Shared Narrowing Operator CType(ByVal v As WSingle) As Single
            Return 0
        End Operator
    End Class
    Class WDouble
        Shared Narrowing Operator CType(ByVal v As WDouble) As Double
            Return 0
        End Operator
    End Class
    Class WDecimal
        Shared Narrowing Operator CType(ByVal v As WDecimal) As Decimal
            Return 0
        End Operator
    End Class
    Class WChar
        Shared Narrowing Operator CType(ByVal v As WChar) As Char
            Return "0"c
        End Operator
    End Class
    Class WBoolean
        Shared Narrowing Operator CType(ByVal v As WBoolean) As Boolean
            Return False
        End Operator
    End Class
    Class WDate
        Shared Narrowing Operator CType(ByVal v As WDate) As Date
            Return Date.MinValue
        End Operator
    End Class

    Class WA1
        'Shared Widening Operator CType(ByVal v As WA1) As WB1
        '    Return Nothing
        'End Operator
    End Class
    Class WB1
        Shared Widening Operator CType(ByVal v As WA1) As WB1
            Return Nothing
        End Operator
    End Class

    Class WA2
        Shared Widening Operator CType(ByVal v As WA2) As WB2
            Return Nothing
        End Operator
    End Class
    Class WB2
        'Shared Widening Operator CType(ByVal v As WA2) As WB2
        '    Return Nothing
        'End Operator
    End Class
End Class