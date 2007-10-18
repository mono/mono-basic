' This test case runs on .Net 1.1 as well
' Though the vb spec says the date format should be as follows
' #[Whitespace+]DateOrTime[Whitespace+]#

Module DateLiterals
    Function Main() As Integer
        Dim d As Date

        d = #1/1/2004 5:05:07 PM#
    End Function
End Module



