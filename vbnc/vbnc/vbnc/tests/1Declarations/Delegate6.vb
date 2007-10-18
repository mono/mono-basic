Namespace Delegate6
    Class Base
        Private Delegate Sub DS1()
        Friend Delegate Sub DS2()
        Protected Delegate Sub DS3()
        Protected Friend Delegate Sub DS4()
        Public Delegate Sub DS5()

        Private Delegate Function DF1() As Integer
        Friend Delegate Function DF2() As String
        Protected Delegate Function DF3() As Base
        Protected Friend Delegate Function DF4() As Integer()
        Public Delegate Function DF5() As Delegate6_Class()
    End Class

    Class Derived
        Inherits Base
        Shadows Delegate Sub DS1()
        Shadows Delegate Function DF2() As Integer
    End Class

    'Private Delegate Sub DS1()
    Friend Delegate Sub DS2()
    'Protected Delegate Sub DS3()
    'Protected Friend Delegate Sub DS4()
    Public Delegate Sub DS5()

    'Private Delegate Function DF1() As Integer
    Friend Delegate Function DF2() As String
    'Protected Delegate Function DF3() As Delegate5_Test
    'Protected Friend Delegate Function DF4() As Integer()
    Public Delegate Function DF5() As Delegate6_Class()

    Delegate Sub DEBS1(ByVal builtinparam As Integer)
    Delegate Sub DEBS2(ByRef builtinbyrefparam As Integer)
    Delegate Sub DEBS3(ByRef builtinbyrefarrayparam As Integer())
    Delegate Sub DEBS4(ByRef builtinbyrefarrayparam2() As Integer)
    Delegate Sub DEBS5(ByVal builtinarrayparam As Integer())
    Delegate Sub DEBS6(ByVal builtinarrayparam2() As Integer)
    Delegate Function DEBF1() As Integer
    Delegate Function DEBF2() As Integer()

    Delegate Sub DECS1(ByVal classparam As Object)
    Delegate Sub DECS2(ByRef classbyrefparam As Object)
    Delegate Sub DECS3(ByRef classbyrefarrayparam As Object())
    Delegate Sub DECS4(ByRef classbyrefarrayparam2() As Object)
    Delegate Sub DECS5(ByVal classarrayparam As Object())
    Delegate Sub DECS6(ByVal classarrayparam2() As Object)
    Delegate Function DECF1() As Object
    Delegate Function DECF2() As Object()

    Delegate Sub DICS1(ByVal classparam As Delegate6_Class)
    Delegate Sub DICS2(ByRef classbyrefparam As Delegate6_Class)
    Delegate Sub DICS3(ByRef classbyrefarrayparam As Delegate6_Class())
    Delegate Sub DICS4(ByRef classbyrefarrayparam2() As Delegate6_Class)
    Delegate Sub DICS5(ByVal classarrayparam As Delegate6_Class())
    Delegate Sub DICS6(ByVal classarrayparam2() As Delegate6_Class)
    Delegate Function DICF1() As Delegate6_Class
    Delegate Function DICF2() As Delegate6_Class()

    Delegate Sub DIIS1(ByVal interfaceparam As Delegate6_Interface)
    Delegate Sub DIIS2(ByRef interfacebyrefparam As Delegate6_Interface)
    Delegate Sub DIIS3(ByRef interfacebyrefarrayparam As Delegate6_Interface())
    Delegate Sub DIIS4(ByRef interfacebyrefarrayparam2() As Delegate6_Interface)
    Delegate Sub DIIS5(ByVal interfacearrayparam As Delegate6_Interface())
    Delegate Sub DIIS6(ByVal interfacearrayparam2() As Delegate6_Interface)
    Delegate Function DIIF1() As Delegate6_Interface
    Delegate Function DIIF2() As Delegate6_Interface()

    Delegate Sub DISS1(ByVal structureparam As Delegate6_Structure)
    Delegate Sub DISS2(ByRef structurebyrefparam As Delegate6_Structure)
    Delegate Sub DISS3(ByRef structurebyrefarrayparam As Delegate6_Structure())
    Delegate Sub DISS4(ByRef structurebyrefarrayparam2() As Delegate6_Structure)
    Delegate Sub DISS5(ByVal structurearrayparam As Delegate6_Structure())
    Delegate Sub DISS6(ByVal structurearrayparam2() As Delegate6_Structure)
    Delegate Function DISF1() As Delegate6_Structure
    Delegate Function DISF2() As Delegate6_Structure()

    Delegate Sub DIES1(ByVal enumparam As Delegate6_Enum)
    Delegate Sub DIES2(ByRef enumbyrefparam As Delegate6_Enum)
    Delegate Sub DIES3(ByRef enumbyrefarrayparam As Delegate6_Enum())
    Delegate Sub DIES4(ByRef enumbyrefarrayparam2() As Delegate6_Enum)
    Delegate Sub DIES5(ByVal enumarrayparam As Delegate6_Enum())
    Delegate Sub DIES6(ByVal enumarrayparam2() As Delegate6_Enum)
    Delegate Function DIEF1() As Delegate6_Enum
    Delegate Function DIEF2() As Delegate6_Enum()

    Delegate Sub DIDS1(ByVal delegateparam As Delegate6_Delegate)
    Delegate Sub DIDS2(ByRef delegatebyrefparam As Delegate6_Delegate)
    Delegate Sub DIDS3(ByRef delegatebyrefarrayparam As Delegate6_Delegate())
    Delegate Sub DIDS4(ByRef delegatebyrefarrayparam2() As Delegate6_Delegate)
    Delegate Sub DIDS5(ByVal delegatearrayparam As Delegate6_Delegate())
    Delegate Sub DIDS6(ByVal delegatearrayparam2() As Delegate6_Delegate)
    Delegate Function DIDF1() As Delegate6_Delegate
    Delegate Function DIDF2() As Delegate6_Delegate()
End Namespace

Public Structure Delegate6_Structure
    Public value As Integer
End Structure
Public Interface Delegate6_Interface

End Interface
Public Class Delegate6_Class

End Class
Public Enum Delegate6_Enum
    somevalue
End Enum
Public Delegate Sub Delegate6_Delegate()