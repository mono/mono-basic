Imports System
Class Test
    Shared Function Main() As Integer
        Dim a, b, c, d, e, f As Boolean
        Dim result As Integer
#If AString = "aabb" Then
        a = True
#Else
        a = False
        Console.WriteLine("A failed")
        result += 1
#End If
#If BBoolean Then
        b = True
#Else
        b = False
        Console.WriteLine("B failed")
        result += 1
#End If
#If CString = "mm nn pp" Then
        c = True
#Else
        c = False
        Console.WriteLine("C failed")
        result += 1
#End If
#If DString = "zz aa" Then
        d = True
#Else
        d = False
        Console.WriteLine("D failed")
        result += 1
#End If
#If EBoolean Then
        e = False
        Console.WriteLine("E failed")
        result += 1
#Else
        e = True
#End If
#If FBooleanAsInt Then
        f = True
#Else
        f = False
        Console.WriteLine("F failed")
        result += 1
#End If
        Return result
    End Function
End Class