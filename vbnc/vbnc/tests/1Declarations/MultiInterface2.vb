Interface MultiInterface2_MultiInterface1
    Sub A()
End Interface
Interface MultiInterface2_MultiInterface3
    Inherits MultiInterface2_MultiInterface1, MultiInterface2_MultiInterface2
End Interface
Interface MultiInterface2_MultiInterface2
    Sub B()
End Interface