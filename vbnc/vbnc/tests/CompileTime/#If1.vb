'test conditional compilation expressions
'test comparison operators
#Const testString_String = "a" < "b"
#Const testInt_Int = 1 < 2
#Const testBool_Bool = True <> False
#Const testInt_Dec = 1 < 1.02
#Const testAnd = True And True
#Const testAndAlso = True AndAlso True
#Const testOr = False Or True
#Const testOrElse = False OrElse True
#Const testXor = True Xor False
#If False Then
class ConditionalIf1_1
end class
#ElseIf False Then
class ConditionalIf1_3
end class
#Else
Class ConditionalIf1_2
End Class
#End If