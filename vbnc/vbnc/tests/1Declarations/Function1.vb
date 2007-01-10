'Function tests for option strict on.
Namespace Function1
    Class ClassA
        Function Test() As Object
        End Function
        Function Test(ByVal builtinvar As Integer) As SByte
        End Function
        Function Test(ByRef builtinbyrefvar As Short) As Byte
        End Function
        Function Test(ByVal builtinarrayvar() As Integer) As Short
        End Function
        Function Test(ByRef builtinarrayvar2 As Long()) As UShort
        End Function
        Function Test(ByVal objvar As Object) As Integer
        End Function
        Function Test(ByRef byrefobjvar As String) As UInteger
        End Function
        Function Test(ByVal classvar As classa) As Long
        End Function
        Function Test(ByRef classbyrefvar As classb) As ULong
        End Function
        Function Test(ByVal classarrayvar() As classa) As Single
        End Function
        Function Test(ByRef classbyrefarrayvar As classb()) As Double
        End Function
        Function Test(ByVal structvar As structurea) As Decimal
        End Function
        Function Test(ByRef structbyrefvar As structureb) As Date
        End Function
        Function Test(ByVal structarrayvar As structurea()) As String
        End Function
        Function Test(ByRef structbyrefarrayvar() As structureb) As Char
        End Function
        Function Test(ByVal interfacevar As interfacea) As Boolean
        End Function
        Function Test(ByRef interfacebyrefvar As interfaceb) As classa
        End Function
        Function Test(ByVal interfacearrayvar As interfacea()) As structurea
        End Function
        Function Test(ByRef interfacebyrefarrayvar() As interfaceb) As interfacea
        End Function
        Function Test(ByVal delegatevar As delegatea) As delegatea
        End Function
        Function Test(ByRef delegatebyrefvar As delegateb) As enuma
        End Function
        Function Test(ByVal delegatearrayvar As delegatea()) As Object
        End Function
        Function Test(ByRef delegatebyrefarrayvar() As delegateb) As Object
        End Function
        Function Test(ByVal enumvar As enuma) As Object
        End Function
        Function Test(ByRef enumbyrefvar As enumb) As Object
        End Function
        Function Test(ByVal enumarrayvar As enuma()) As Object
        End Function
        Function Test(ByRef enumbyrefarrayvar() As enumb) As Object
        End Function
    End Class

    Class ClassB
        Function ParamArrayBuiltInTest1(ByVal ParamArray parramyarraybuiltinparam As Integer()) As SByte
        End Function
        Function ParamArrayBuiltInTest2(ByVal ParamArray parramyarraybuiltinparam() As Integer) As Byte
        End Function

        Function ParamArrayObjTest1(ByVal ParamArray parramyarrayObjparam() As Object) As Short
        End Function
        Function ParamArrayObjTest2(ByVal ParamArray parramyarrayObjparam As Object()) As UShort
        End Function

        Function ParamArrayClassTest1(ByVal ParamArray parramyarrayClassparam() As ClassB) As Integer
        End Function
        Function ParamArrayClassTest2(ByVal ParamArray parramyarrayarrayClassparam As ClassB()) As UInteger
        End Function

        Function ParamArrayStructureTest1(ByVal ParamArray parramyarrayStructureparam() As StructureA) As Long
        End Function
        Function ParamArrayStructureTest2(ByVal ParamArray parramyarrayStructureparam As StructureA()) As ULong
        End Function

        Function ParamArrayInterfaceTest1(ByVal ParamArray parramyarrayInterfaceparam() As InterfaceA) As Decimal
        End Function
        Function ParamArrayInterfaceTest2(ByVal ParamArray parramyarrayInterfaceparam As InterfaceA()) As Single
        End Function

        Function ParamArrayDelegateTest1(ByVal ParamArray parramyarrayDelegateparam() As DelegateA) As Double
        End Function
        Function ParamArrayDelegateTest2(ByVal ParamArray parramyarrayDelegateparam As DelegateA()) As Date
        End Function

        Function ParamArrayEnumTest1(ByVal ParamArray ParamArrayEnumparam() As EnumA) As String
        End Function
        Function ParamArrayEnumTest3(ByVal ParamArray ParamArrayEnumparam As EnumA()) As Char
        End Function

    End Class

    Class ClassC
        Function OptionalBuiltInTest1(Optional ByVal optionalbuiltinparam As Integer = 1) As Integer
        End Function
        Function OptionalBuiltInTest2(Optional ByRef optionalbyrefbuiltinparam As Integer = 1) As UInteger
        End Function
        Function OptionalBuiltInTest3(Optional ByVal optionalarraybuiltinparam As Integer() = Nothing) As Boolean
        End Function
        Function OptionalBuiltInTest4(Optional ByRef optionalbyrefarraybuiltinparam() As Integer = Nothing) As Boolean
        End Function

        Function OptionalObjTest1(Optional ByVal optionalObjparam As Object = Nothing) As Short
        End Function
        Function OptionalObjTest2(Optional ByRef optionalbyrefObjparam As Object = Nothing) As UShort
        End Function
        Function OptionalObjTest3(Optional ByVal optionalarrayObjparam As Object() = Nothing) As Long
        End Function
        Function OptionalObjTest4(Optional ByRef optionalbyrefarrayObjparam() As Object = Nothing) As ULong
        End Function

        Function OptionalClassTest1(Optional ByVal optionalClassparam As ClassB = Nothing) As Decimal
        End Function
        Function OptionalClassTest2(Optional ByRef optionalbyrefClassparam As ClassB = Nothing) As Single
        End Function
        Function OptionalClassTest3(Optional ByVal optionalarrayClassparam As ClassB() = Nothing) As Double
        End Function
        Function OptionalClassTest4(Optional ByRef optionalbyrefarrayClassparam() As ClassB = Nothing) As Date
        End Function

        'Function OptionalStructureTest1(Optional ByVal optionalStructureparam As StructureA = Nothing)
        'End Function
        'Function OptionalStructureTest2(Optional ByRef optionalbyrefStructureparam As StructureA = Nothing)
        'End Function
        'Function OptionalStructureTest3(Optional ByVal optionalarrayStructureparam As StructureA() = Nothing)
        'End Function
        'Function OptionalStructureTest4(Optional ByRef optionalbyrefarrayStructureparam() As StructureA = Nothing)
        'End Function

        Function OptionalInterfaceTest1(Optional ByVal optionalInterfaceparam As InterfaceA = Nothing) As structurea
        End Function
        Function OptionalInterfaceTest2(Optional ByRef optionalbyrefInterfaceparam As InterfaceA = Nothing) As interfacea
        End Function
        Function OptionalInterfaceTest3(Optional ByVal optionalarrayInterfaceparam As InterfaceA() = Nothing) As enuma
        End Function
        Function OptionalInterfaceTest4(Optional ByRef optionalbyrefarrayInterfaceparam() As InterfaceA = Nothing) As delegatea
        End Function

        Function OptionalDelegateTest1(Optional ByVal optionalDelegateparam As DelegateA = Nothing) As classa
        End Function
        Function OptionalDelegateTest2(Optional ByRef optionalbyrefDelegateparam As DelegateA = Nothing) As Byte
        End Function
        Function OptionalDelegateTest3(Optional ByVal optionalarrayDelegateparam As DelegateA() = Nothing) As Short
        End Function
        Function OptionalDelegateTest4(Optional ByRef optionalbyrefarrayDelegateparam() As DelegateA = Nothing) As Integer
        End Function

        Function OptionalEnumTest1(Optional ByVal optionalEnumparam As EnumA = enuma.value) As Long
        End Function
        Function OptionalEnumTest2(Optional ByRef optionalbyrefEnumparam As EnumA = enuma.value) As Decimal
        End Function
        Function OptionalEnumTest3(Optional ByVal optionalarrayEnumparam As EnumA() = Nothing) As Single
        End Function
        Function OptionalEnumTest4(Optional ByRef optionalbyrefarrayEnumparam() As EnumA = Nothing) As Double
        End Function
    End Class

    Class ClassD
        Public Function Test() As system.int16
        End Function
        Private Function Test(ByVal privateTest As Integer) As system.int32
        End Function
        Protected Function Test(ByVal protectedTest As Short) As system.int64
        End Function
        Friend Function Test(ByVal friendTest As Byte) As system.uint16
        End Function
        Protected Friend Function Test(ByVal protectedfriendTest As Long) As system.uint32
        End Function
    End Class

    MustInherit Class ClassE
        Function Test() As system.boolean
        End Function
        Overridable Function OverridesFunction() As system.char
        End Function
        MustOverride Function MustOverrideFunction() As system.string
        Overridable Function NotOverridableFunction() As system.datetime
        End Function
        Overridable Overloads Function OverridesOverloadsFunction() As system.double
        End Function
        Overridable Overloads Function OverridesOverloadsFunction(ByVal Param1 As String) As system.single
        End Function
    End Class

    Class ClassF
        Inherits ClassE
        Shadows Function ShadowsFunction() As system.uint64
        End Function
        Shared Function SharedFunction() As system.decimal
        End Function
        Overridable Function OverridableFunction() As system.int64
        End Function
        NotOverridable Overrides Function NotOverridableFunction() As Date
        End Function
        Overrides Function OverridesFunction() As Char
        End Function
        Overloads Function OverloadsFunction(ByVal Param1 As Integer) As Single
        End Function
        Overloads Function OverloadsFunction() As system.object
        End Function
        Overrides Function MustOverrideFunction() As String
        End Function
        Overloads Overrides Function OverridesOverloadsFunction() As Double
        End Function
    End Class

    Class ClassG
        Function Test(ByVal value As Date) As Date
        End Function
    End Class

    Class ClassH
        Shared Function SharedTest() As Object
        End Function
        Function Test() As Object
        End Function
    End Class

    Class ClassI
        Shared Function SharedTest() As Object
        End Function
    End Class

    Structure StructureA
        Dim value As Integer
        Function Test() As Object
        End Function
        Function Test(ByVal builtinvar As Integer) As SByte
        End Function
        Function Test(ByRef builtinbyrefvar As Short) As Byte
        End Function
        Function Test(ByVal builtinarrayvar() As Integer) As Short
        End Function
        Function Test(ByRef builtinarrayvar2 As Long()) As UShort
        End Function
        Function Test(ByVal objvar As Object) As Integer
        End Function
        Function Test(ByRef byrefobjvar As String) As UInteger
        End Function
        Function Test(ByVal classvar As classa) As Long
        End Function
        Function Test(ByRef classbyrefvar As classb) As ULong
        End Function
        Function Test(ByVal classarrayvar() As classa) As Single
        End Function
        Function Test(ByRef classbyrefarrayvar As classb()) As Double
        End Function
        Function Test(ByVal structvar As structurea) As Decimal
        End Function
        Function Test(ByRef structbyrefvar As structureb) As Date
        End Function
        Function Test(ByVal structarrayvar As structurea()) As String
        End Function
        Function Test(ByRef structbyrefarrayvar() As structureb) As Char
        End Function
        Function Test(ByVal interfacevar As interfacea) As Boolean
        End Function
        Function Test(ByRef interfacebyrefvar As interfaceb) As classa
        End Function
        Function Test(ByVal interfacearrayvar As interfacea()) As structurea
        End Function
        Function Test(ByRef interfacebyrefarrayvar() As interfaceb) As interfacea
        End Function
        Function Test(ByVal delegatevar As delegatea) As delegatea
        End Function
        Function Test(ByRef delegatebyrefvar As delegateb) As enuma
        End Function
        Function Test(ByVal delegatearrayvar As delegatea()) As Object
        End Function
        Function Test(ByRef delegatebyrefarrayvar() As delegateb) As Object
        End Function
        Function Test(ByVal enumvar As enuma) As Object
        End Function
        Function Test(ByRef enumbyrefvar As enumb) As Object
        End Function
        Function Test(ByVal enumarrayvar As enuma()) As Object
        End Function
        Function Test(ByRef enumbyrefarrayvar() As enumb) As Object
        End Function
    End Structure

    Structure StructureB
        Dim value As Integer
        Function ParamArrayBuiltInTest1(ByVal ParamArray parramyarraybuiltinparam As Integer()) As SByte
        End Function
        Function ParamArrayBuiltInTest2(ByVal ParamArray parramyarraybuiltinparam() As Integer) As Byte
        End Function

        Function ParamArrayObjTest1(ByVal ParamArray parramyarrayObjparam() As Object) As Short
        End Function
        Function ParamArrayObjTest2(ByVal ParamArray parramyarrayObjparam As Object()) As UShort
        End Function

        Function ParamArrayClassTest1(ByVal ParamArray parramyarrayClassparam() As ClassB) As Integer
        End Function
        Function ParamArrayClassTest2(ByVal ParamArray parramyarrayarrayClassparam As ClassB()) As UInteger
        End Function

        Function ParamArrayStructureTest1(ByVal ParamArray parramyarrayStructureparam() As StructureA) As Long
        End Function
        Function ParamArrayStructureTest2(ByVal ParamArray parramyarrayStructureparam As StructureA()) As ULong
        End Function

        Function ParamArrayInterfaceTest1(ByVal ParamArray parramyarrayInterfaceparam() As InterfaceA) As Decimal
        End Function
        Function ParamArrayInterfaceTest2(ByVal ParamArray parramyarrayInterfaceparam As InterfaceA()) As Single
        End Function

        Function ParamArrayDelegateTest1(ByVal ParamArray parramyarrayDelegateparam() As DelegateA) As Double
        End Function
        Function ParamArrayDelegateTest2(ByVal ParamArray parramyarrayDelegateparam As DelegateA()) As Date
        End Function

        Function ParamArrayEnumTest1(ByVal ParamArray ParamArrayEnumparam() As EnumA) As String
        End Function
        Function ParamArrayEnumTest3(ByVal ParamArray ParamArrayEnumparam As EnumA()) As Char
        End Function

    End Structure

    Structure StructureC
        Dim value As Integer
        Function OptionalBuiltInTest1(Optional ByVal optionalbuiltinparam As Integer = 1) As Integer
        End Function
        Function OptionalBuiltInTest2(Optional ByRef optionalbyrefbuiltinparam As Integer = 1) As UInteger
        End Function
        Function OptionalBuiltInTest3(Optional ByVal optionalarraybuiltinparam As Integer() = Nothing) As Boolean
        End Function
        Function OptionalBuiltInTest4(Optional ByRef optionalbyrefarraybuiltinparam() As Integer = Nothing) As Boolean
        End Function

        Function OptionalObjTest1(Optional ByVal optionalObjparam As Object = Nothing) As Short
        End Function
        Function OptionalObjTest2(Optional ByRef optionalbyrefObjparam As Object = Nothing) As UShort
        End Function
        Function OptionalObjTest3(Optional ByVal optionalarrayObjparam As Object() = Nothing) As Long
        End Function
        Function OptionalObjTest4(Optional ByRef optionalbyrefarrayObjparam() As Object = Nothing) As ULong
        End Function

        Function OptionalClassTest1(Optional ByVal optionalClassparam As ClassB = Nothing) As Decimal
        End Function
        Function OptionalClassTest2(Optional ByRef optionalbyrefClassparam As ClassB = Nothing) As Single
        End Function
        Function OptionalClassTest3(Optional ByVal optionalarrayClassparam As ClassB() = Nothing) As Double
        End Function
        Function OptionalClassTest4(Optional ByRef optionalbyrefarrayClassparam() As ClassB = Nothing) As Date
        End Function

        'Function OptionalStructureTest1(Optional ByVal optionalStructureparam As StructureA = Nothing)
        'End Function
        'Function OptionalStructureTest2(Optional ByRef optionalbyrefStructureparam As StructureA = Nothing)
        'End Function
        'Function OptionalStructureTest3(Optional ByVal optionalarrayStructureparam As StructureA() = Nothing)
        'End Function
        'Function OptionalStructureTest4(Optional ByRef optionalbyrefarrayStructureparam() As StructureA = Nothing)
        'End Function

        Function OptionalInterfaceTest1(Optional ByVal optionalInterfaceparam As InterfaceA = Nothing) As structurea
        End Function
        Function OptionalInterfaceTest2(Optional ByRef optionalbyrefInterfaceparam As InterfaceA = Nothing) As interfacea
        End Function
        Function OptionalInterfaceTest3(Optional ByVal optionalarrayInterfaceparam As InterfaceA() = Nothing) As enuma
        End Function
        Function OptionalInterfaceTest4(Optional ByRef optionalbyrefarrayInterfaceparam() As InterfaceA = Nothing) As delegatea
        End Function

        Function OptionalDelegateTest1(Optional ByVal optionalDelegateparam As DelegateA = Nothing) As classa
        End Function
        Function OptionalDelegateTest2(Optional ByRef optionalbyrefDelegateparam As DelegateA = Nothing) As Byte
        End Function
        Function OptionalDelegateTest3(Optional ByVal optionalarrayDelegateparam As DelegateA() = Nothing) As Short
        End Function
        Function OptionalDelegateTest4(Optional ByRef optionalbyrefarrayDelegateparam() As DelegateA = Nothing) As Integer
        End Function

        Function OptionalEnumTest1(Optional ByVal optionalEnumparam As EnumA = enuma.value) As Long
        End Function
        Function OptionalEnumTest2(Optional ByRef optionalbyrefEnumparam As EnumA = enuma.value) As Decimal
        End Function
        Function OptionalEnumTest3(Optional ByVal optionalarrayEnumparam As EnumA() = Nothing) As Single
        End Function
        Function OptionalEnumTest4(Optional ByRef optionalbyrefarrayEnumparam() As EnumA = Nothing) As Double
        End Function
    End Structure

    Structure StructureD
        Dim value As Integer
        Public Function Test() As system.int16
        End Function
        Private Function Test(ByVal privateTest As Integer) As system.int32
        End Function
        'Protected Function Test(ByVal protectedTest As Short) As system.int64
        'End Function
        Friend Function Test(ByVal friendTest As Byte) As system.uint16
        End Function
        'Protected Friend Function Test(ByVal protectedfriendTest As Long) As system.uint32
        'End Function
    End Structure

    Structure StructureE
        Dim value As Integer
        
        Function Test() As Object
        End Function
        Public Overrides Function ToString() As String
        End Function
        Public Overrides Function Equals(ByVal obj As Object) As Boolean
        End Function
        Overloads Function OverloadsFunction(ByVal Param1 As Integer) As system.single
        End Function
        Overloads Function OverloadsFunction() As system.double
        End Function
        Shadows Function ShadowsFunction() As Integer
        End Function
        Shared Function SharedFunction() As Short
        End Function
    End Structure

    Interface InterfaceA
        Function Test() As Object
        Function Test(ByVal builtinvar As Integer) As SByte
        Function Test(ByRef builtinbyrefvar As Short) As Byte
        Function Test(ByVal builtinarrayvar() As Integer) As Short
        Function Test(ByRef builtinarrayvar2 As Long()) As UShort
        Function Test(ByVal objvar As Object) As Integer
        Function Test(ByRef byrefobjvar As String) As UInteger
        Function Test(ByVal classvar As classa) As Long
        Function Test(ByRef classbyrefvar As classb) As ULong
        Function Test(ByVal classarrayvar() As classa) As Single
        Function Test(ByRef classbyrefarrayvar As classb()) As Double
        Function Test(ByVal structvar As structurea) As Decimal
        Function Test(ByRef structbyrefvar As structureb) As Date
        Function Test(ByVal structarrayvar As structurea()) As String
        Function Test(ByRef structbyrefarrayvar() As structureb) As Char
        Function Test(ByVal interfacevar As interfacea) As Boolean
        Function Test(ByRef interfacebyrefvar As interfaceb) As classa
        Function Test(ByVal interfacearrayvar As interfacea()) As structurea
        Function Test(ByRef interfacebyrefarrayvar() As interfaceb) As interfacea
        Function Test(ByVal delegatevar As delegatea) As delegatea
        Function Test(ByRef delegatebyrefvar As delegateb) As enuma
        Function Test(ByVal delegatearrayvar As delegatea()) As Object
        Function Test(ByRef delegatebyrefarrayvar() As delegateb) As Object
        Function Test(ByVal enumvar As enuma) As Object
        Function Test(ByRef enumbyrefvar As enumb) As Object
        Function Test(ByVal enumarrayvar As enuma()) As Object
        Function Test(ByRef enumbyrefarrayvar() As enumb) As Object
    End Interface

    Interface InterfaceB
        Function ParamArrayBuiltInTest1(ByVal ParamArray parramyarraybuiltinparam As Integer()) As SByte
        Function ParamArrayBuiltInTest2(ByVal ParamArray parramyarraybuiltinparam() As Integer) As Byte
        Function ParamArrayObjTest1(ByVal ParamArray parramyarrayObjparam() As Object) As Short
        Function ParamArrayObjTest2(ByVal ParamArray parramyarrayObjparam As Object()) As UShort
        Function ParamArrayClassTest1(ByVal ParamArray parramyarrayClassparam() As ClassB) As Integer
        Function ParamArrayClassTest2(ByVal ParamArray parramyarrayarrayClassparam As ClassB()) As UInteger
        Function ParamArrayStructureTest1(ByVal ParamArray parramyarrayStructureparam() As StructureA) As Long
        Function ParamArrayStructureTest2(ByVal ParamArray parramyarrayStructureparam As StructureA()) As ULong
        Function ParamArrayInterfaceTest1(ByVal ParamArray parramyarrayInterfaceparam() As InterfaceA) As Decimal
        Function ParamArrayInterfaceTest2(ByVal ParamArray parramyarrayInterfaceparam As InterfaceA()) As Single
        Function ParamArrayDelegateTest1(ByVal ParamArray parramyarrayDelegateparam() As DelegateA) As Double
        Function ParamArrayDelegateTest2(ByVal ParamArray parramyarrayDelegateparam As DelegateA()) As Date
        Function ParamArrayEnumTest1(ByVal ParamArray ParamArrayEnumparam() As EnumA) As String
        Function ParamArrayEnumTest3(ByVal ParamArray ParamArrayEnumparam As EnumA()) As Char
    End Interface

    Interface InterfaceC
        Function OptionalBuiltInTest1(Optional ByVal optionalbuiltinparam As Integer = 1) As Integer
        Function OptionalBuiltInTest2(Optional ByRef optionalbyrefbuiltinparam As Integer = 1) As UInteger
        Function OptionalBuiltInTest3(Optional ByVal optionalarraybuiltinparam As Integer() = Nothing) As Boolean
        Function OptionalBuiltInTest4(Optional ByRef optionalbyrefarraybuiltinparam() As Integer = Nothing) As Boolean

        Function OptionalObjTest1(Optional ByVal optionalObjparam As Object = Nothing) As Short
        Function OptionalObjTest2(Optional ByRef optionalbyrefObjparam As Object = Nothing) As UShort
        Function OptionalObjTest3(Optional ByVal optionalarrayObjparam As Object() = Nothing) As Long
        Function OptionalObjTest4(Optional ByRef optionalbyrefarrayObjparam() As Object = Nothing) As ULong

        Function OptionalClassTest1(Optional ByVal optionalClassparam As ClassB = Nothing) As Decimal
        Function OptionalClassTest2(Optional ByRef optionalbyrefClassparam As ClassB = Nothing) As Single
        Function OptionalClassTest3(Optional ByVal optionalarrayClassparam As ClassB() = Nothing) As Double
        Function OptionalClassTest4(Optional ByRef optionalbyrefarrayClassparam() As ClassB = Nothing) As Date

        'Function OptionalStructureTest1(Optional ByVal optionalStructureparam As StructureA = Nothing)
        'Function OptionalStructureTest2(Optional ByRef optionalbyrefStructureparam As StructureA = Nothing)
        'Function OptionalStructureTest3(Optional ByVal optionalarrayStructureparam As StructureA() = Nothing)
        'Function OptionalStructureTest4(Optional ByRef optionalbyrefarrayStructureparam() As StructureA = Nothing)

        Function OptionalInterfaceTest1(Optional ByVal optionalInterfaceparam As InterfaceA = Nothing) As structurea
        Function OptionalInterfaceTest2(Optional ByRef optionalbyrefInterfaceparam As InterfaceA = Nothing) As interfacea
        Function OptionalInterfaceTest3(Optional ByVal optionalarrayInterfaceparam As InterfaceA() = Nothing) As enuma
        Function OptionalInterfaceTest4(Optional ByRef optionalbyrefarrayInterfaceparam() As InterfaceA = Nothing) As delegatea

        Function OptionalDelegateTest1(Optional ByVal optionalDelegateparam As DelegateA = Nothing) As classa
        Function OptionalDelegateTest2(Optional ByRef optionalbyrefDelegateparam As DelegateA = Nothing) As Byte
        Function OptionalDelegateTest3(Optional ByVal optionalarrayDelegateparam As DelegateA() = Nothing) As Short
        Function OptionalDelegateTest4(Optional ByRef optionalbyrefarrayDelegateparam() As DelegateA = Nothing) As Integer

        Function OptionalEnumTest1(Optional ByVal optionalEnumparam As EnumA = enuma.value) As Long
        Function OptionalEnumTest2(Optional ByRef optionalbyrefEnumparam As EnumA = enuma.value) As Decimal
        Function OptionalEnumTest3(Optional ByVal optionalarrayEnumparam As EnumA() = Nothing) As Single
        Function OptionalEnumTest4(Optional ByRef optionalbyrefarrayEnumparam() As EnumA = Nothing) As Double
    End Interface

    Interface InterfaceD
        Function Test() As system.int16
        Function Test(ByVal privateTest As Integer) As system.int32
        Function Test(ByVal protectedTest As Short) As system.int64
        Function Test(ByVal friendTest As Byte) As system.uint16
        Function Test(ByVal protectedfriendTest As Long) As system.uint32
    End Interface

    Interface InterfaceE
        Function Test() As system.boolean
        Function OverridesFunction() As system.char
        Function MustOverrideFunction() As system.string
        Function NotOverridableFunction() As system.datetime
        Overloads Function OverridesOverloadsFunction() As system.double
        Overloads Function OverridesOverloadsFunction(ByVal Param1 As String) As system.single
    End Interface

    Interface InterfaceF
        Inherits InterfaceE
        Function OverridableFunction() As system.int64
        Function NotOverridableFunction() As Date
        Function OverridesFunction() As Char
        Overloads Function OverloadsFunction(ByVal Param1 As Integer) As Single
        Overloads Function OverloadsFunction() As system.object
        Function MustOverrideFunction() As String
        Overloads Function OverridesOverloadsFunction() As Double
    End Interface

    Enum EnumA
        value
    End Enum
    Enum EnumB
        value
    End Enum

    Delegate Sub DelegateA()
    Delegate Function DelegateB() As String
End Namespace
