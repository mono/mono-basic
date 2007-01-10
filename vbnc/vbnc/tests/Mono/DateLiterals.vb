Imports System.Threading
Imports System.Globalization

Module DateLiterals
    Function Main() As Integer
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture


        Dim d As Date
        Dim d1 As Date

        d = #12/1/2001 3:24:59 PM#
        d1 = #12/1/2001 3:24:59 PM#

        If d1 <> d Then
            System.Console.WriteLine("#A1 : values d and d1 are not same") : Return 1
        End If

        d = #12/1/2001#
        d1 = #12/1/2001 12:00:00 PM#

        System.Console.WriteLine(d)
        System.Console.WriteLine(d1)

        'if d1 <> d then
        '	System.Console.WriteLine("#A2 : values d and d1 are not same"):return 1
        'end if

        d = #3:24:59 AM#
        d1 = #3:24:59 AM#

        If d1 <> d Then
            System.Console.WriteLine("#A3 : values d and d1 are not same") : Return 1
        End If

        d = #3:24:59 PM#
        d1 = #3:24:59 PM#

        System.Console.WriteLine(d)
        System.Console.WriteLine(d1)

        If d1 <> d Then
            System.Console.WriteLine("#A4 : values d and d1 are not same") : Return 1
        End If

        d = #3:00:00 PM#
        d1 = #3:00:00 PM#

        If d1 <> d Then
            System.Console.WriteLine("#A5 : values d and d1 are not same") : Return 1
        End If

        d = #3:13:00 PM#
        d1 = #3:13:00 PM#

        If d1 <> d Then
            System.Console.WriteLine("#A6 : values d and d1 are not same") : Return 1
        End If
    End Function
End Module
