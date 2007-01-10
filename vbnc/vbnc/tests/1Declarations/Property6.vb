'Property tests for option strict on.
Namespace Property6
    Class ClassA
        Property Test() As Object
            Get
            End Get
            Set(ByVal value As Object)
            End Set
        End Property
        Property Test(ByVal builtinvar As Integer) As SByte
            Get
            End Get
            Set(ByVal value As SByte)
            End Set
        End Property
        Property Test(ByVal builtinByValvar As Short) As Byte
            Get
            End Get
            Set(ByVal value As Byte)
            End Set
        End Property
        Property Test(ByVal builtinarrayvar() As Integer) As Short
            Get
            End Get
            Set(ByVal value As Short)
            End Set
        End Property
        Property Test(ByVal builtinarrayvar2 As Long()) As UShort
            Get
            End Get
            Set(ByVal value As UShort)
            End Set
        End Property
        Property Test(ByVal objvar As Object) As Integer
            Get
            End Get
            Set(ByVal value As Integer)
            End Set
        End Property
        Property Test(ByVal ByValobjvar As String) As UInteger
            Get
            End Get
            Set(ByVal value As UInteger)
            End Set
        End Property
        Property Test(ByVal classvar As classa) As Long
            Get
            End Get
            Set(ByVal value As Long)
            End Set
        End Property
        Property Test(ByVal classByValvar As classb) As ULong
            Get
            End Get
            Set(ByVal value As ULong)
            End Set
        End Property
        Property Test(ByVal classarrayvar() As classa) As Single
            Get
            End Get
            Set(ByVal value As Single)
            End Set
        End Property
        Property Test(ByVal classByValarrayvar As classb()) As Double
            Get
            End Get
            Set(ByVal value As Double)
            End Set
        End Property
        Property Test(ByVal structvar As structurea) As Decimal
            Get
            End Get
            Set(ByVal value As Decimal)
            End Set
        End Property
        Property Test(ByVal structByValvar As structureb) As Date
            Get
            End Get
            Set(ByVal value As Date)
            End Set
        End Property
        Property Test(ByVal structarrayvar As structurea()) As String
            Get
            End Get
            Set(ByVal value As String)
            End Set
        End Property
        Property Test(ByVal structByValarrayvar() As structureb) As Char
            Get
            End Get
            Set(ByVal value As Char)
            End Set
        End Property
        Property Test(ByVal interfacevar As interfacea) As Boolean
            Get
            End Get
            Set(ByVal value As Boolean)
            End Set
        End Property
        Property Test(ByVal interfaceByValvar As interfaceb) As classa
            Get
            End Get
            Set(ByVal value As classa)
            End Set
        End Property
        Property Test(ByVal interfacearrayvar As interfacea()) As structurea
            Get
            End Get
            Set(ByVal value As structurea)
            End Set
        End Property
        Property Test(ByVal interfaceByValarrayvar() As interfaceb) As interfacea
            Get
            End Get
            Set(ByVal value As interfacea)
            End Set
        End Property
        Property Test(ByVal delegatevar As delegatea) As delegatea
            Get
            End Get
            Set(ByVal value As delegatea)
            End Set
        End Property
        Property Test(ByVal delegateByValvar As delegateb) As enuma
            Get
            End Get
            Set(ByVal value As enuma)
            End Set
        End Property
        Property Test(ByVal delegatearrayvar As delegatea()) As Object
            Get
            End Get
            Set(ByVal value As Object)
            End Set
        End Property
        Property Test(ByVal delegateByValarrayvar() As delegateb) As Object
            Get
            End Get
            Set(ByVal value As Object)
            End Set
        End Property
        Property Test(ByVal enumvar As enuma) As Object
            Get
            End Get
            Set(ByVal value As Object)
            End Set
        End Property
        Property Test(ByVal enumByValvar As enumb) As Object
            Get
            End Get
            Set(ByVal value As Object)
            End Set
        End Property
        Property Test(ByVal enumarrayvar As enuma()) As Object
            Get
            End Get
            Set(ByVal value As Object)
            End Set
        End Property
        Property Test(ByVal enumByValarrayvar() As enumb) As Object
            Get
            End Get
            Set(ByVal value As Object)
            End Set
        End Property
    End Class

    Class ClassB
        Property ParamArrayBuiltInTest1(ByVal ParamArray parramyarraybuiltinparam As Integer()) As SByte
            Get
            End Get
            Set(ByVal value As SByte)
            End Set
        End Property
        Property ParamArrayBuiltInTest2(ByVal ParamArray parramyarraybuiltinparam() As Integer) As Byte
            Get
            End Get
            Set(ByVal value As Byte)
            End Set
        End Property
        Property ParamArrayObjTest1(ByVal ParamArray parramyarrayObjparam() As Object) As Short
            Get
            End Get
            Set(ByVal value As Short)
            End Set
        End Property
        Property ParamArrayObjTest2(ByVal ParamArray parramyarrayObjparam As Object()) As UShort
            Get
            End Get
            Set(ByVal value As UShort)
            End Set
        End Property
        Property ParamArrayClassTest1(ByVal ParamArray parramyarrayClassparam() As ClassB) As Integer
            Get
            End Get
            Set(ByVal value As Integer)
            End Set
        End Property
        Property ParamArrayClassTest2(ByVal ParamArray parramyarrayarrayClassparam As ClassB()) As UInteger
            Get
            End Get
            Set(ByVal value As UInteger)
            End Set
        End Property
        Property ParamArrayStructureTest1(ByVal ParamArray parramyarrayStructureparam() As StructureA) As Long
            Get
            End Get
            Set(ByVal value As Long)
            End Set
        End Property
        Property ParamArrayStructureTest2(ByVal ParamArray parramyarrayStructureparam As StructureA()) As ULong
            Get
            End Get
            Set(ByVal value As ULong)
            End Set
        End Property
        Property ParamArrayInterfaceTest1(ByVal ParamArray parramyarrayInterfaceparam() As InterfaceA) As Decimal
            Get
            End Get
            Set(ByVal value As Decimal)
            End Set
        End Property
        Property ParamArrayInterfaceTest2(ByVal ParamArray parramyarrayInterfaceparam As InterfaceA()) As Single
            Get
            End Get
            Set(ByVal value As Single)
            End Set
        End Property
        Property ParamArrayDelegateTest1(ByVal ParamArray parramyarrayDelegateparam() As DelegateA) As Double
            Get
            End Get
            Set(ByVal value As Double)
            End Set
        End Property
        Property ParamArrayDelegateTest2(ByVal ParamArray parramyarrayDelegateparam As DelegateA()) As Date
            Get
            End Get
            Set(ByVal value As Date)
            End Set
        End Property
        Property ParamArrayEnumTest1(ByVal ParamArray ParamArrayEnumparam() As EnumA) As String
            Get
            End Get
            Set(ByVal value As String)
            End Set
        End Property
        Property ParamArrayEnumTest3(ByVal ParamArray ParamArrayEnumparam As EnumA()) As Char
            Get
            End Get
            Set(ByVal value As Char)
            End Set
        End Property
    End Class

    Class ClassC
        Property OptionalBuiltInTest1(Optional ByVal optionalbuiltinparam As Integer = 1) As Integer
            Get
            End Get
            Set(ByVal value As Integer)
            End Set
        End Property
        Property OptionalBuiltInTest2(Optional ByVal optionalByValbuiltinparam As Integer = 1) As UInteger
            Get
            End Get
            Set(ByVal value As UInteger)
            End Set
        End Property
        Property OptionalBuiltInTest3(Optional ByVal optionalarraybuiltinparam As Integer() = Nothing) As Boolean
            Get
            End Get
            Set(ByVal value As Boolean)
            End Set
        End Property
        Property OptionalBuiltInTest4(Optional ByVal optionalByValarraybuiltinparam() As Integer = Nothing) As Boolean
            Get
            End Get
            Set(ByVal value As Boolean)
            End Set
        End Property
        Property OptionalObjTest1(Optional ByVal optionalObjparam As Object = Nothing) As Short
            Get
            End Get
            Set(ByVal value As Short)
            End Set
        End Property
        Property OptionalObjTest2(Optional ByVal optionalByValObjparam As Object = Nothing) As UShort
            Get
            End Get
            Set(ByVal value As UShort)
            End Set
        End Property
        Property OptionalObjTest3(Optional ByVal optionalarrayObjparam As Object() = Nothing) As Long
            Get
            End Get
            Set(ByVal value As Long)
            End Set
        End Property
        Property OptionalObjTest4(Optional ByVal optionalByValarrayObjparam() As Object = Nothing) As ULong
            Get
            End Get
            Set(ByVal value As ULong)
            End Set
        End Property
        Property OptionalClassTest1(Optional ByVal optionalClassparam As ClassB = Nothing) As Decimal
            Get
            End Get
            Set(ByVal value As Decimal)
            End Set
        End Property
        Property OptionalClassTest2(Optional ByVal optionalByValClassparam As ClassB = Nothing) As Single
            Get
            End Get
            Set(ByVal value As Single)
            End Set
        End Property
        Property OptionalClassTest3(Optional ByVal optionalarrayClassparam As ClassB() = Nothing) As Double
            Get
            End Get
            Set(ByVal value As Double)
            End Set
        End Property
        Property OptionalClassTest4(Optional ByVal optionalByValarrayClassparam() As ClassB = Nothing) As Date
            Get
            End Get
            Set(ByVal value As Date)
            End Set
        End Property

        'Property OptionalStructureTest1(Optional ByVal optionalStructureparam As StructureA = Nothing)
        '
        'Property OptionalStructureTest2(Optional ByVal optionalByValStructureparam As StructureA = Nothing)
        '
        'Property OptionalStructureTest3(Optional ByVal optionalarrayStructureparam As StructureA() = Nothing)
        '
        'Property OptionalStructureTest4(Optional ByVal optionalByValarrayStructureparam() As StructureA = Nothing)
        '

        Property OptionalInterfaceTest1(Optional ByVal optionalInterfaceparam As InterfaceA = Nothing) As structurea
            Get
            End Get
            Set(ByVal value As structurea)
            End Set
        End Property
        Property OptionalInterfaceTest2(Optional ByVal optionalByValInterfaceparam As InterfaceA = Nothing) As interfacea
            Get
            End Get
            Set(ByVal value As interfacea)
            End Set
        End Property
        Property OptionalInterfaceTest3(Optional ByVal optionalarrayInterfaceparam As InterfaceA() = Nothing) As enuma
            Get
            End Get
            Set(ByVal value As enuma)
            End Set
        End Property
        Property OptionalInterfaceTest4(Optional ByVal optionalByValarrayInterfaceparam() As InterfaceA = Nothing) As delegatea
            Get
            End Get
            Set(ByVal value As delegatea)
            End Set
        End Property

        Property OptionalDelegateTest1(Optional ByVal optionalDelegateparam As DelegateA = Nothing) As classa
            Get
            End Get
            Set(ByVal value As classa)
            End Set
        End Property
        Property OptionalDelegateTest2(Optional ByVal optionalByValDelegateparam As DelegateA = Nothing) As Byte
            Get
            End Get
            Set(ByVal value As Byte)
            End Set
        End Property
        Property OptionalDelegateTest3(Optional ByVal optionalarrayDelegateparam As DelegateA() = Nothing) As Short
            Get
            End Get
            Set(ByVal value As Short)
            End Set
        End Property
        Property OptionalDelegateTest4(Optional ByVal optionalByValarrayDelegateparam() As DelegateA = Nothing) As Integer
            Get
            End Get
            Set(ByVal value As Integer)
            End Set
        End Property

        Property OptionalEnumTest1(Optional ByVal optionalEnumparam As EnumA = enuma.value) As Long
            Get
            End Get
            Set(ByVal value As Long)
            End Set
        End Property
        Property OptionalEnumTest2(Optional ByVal optionalByValEnumparam As EnumA = enuma.value) As Decimal
            Get
            End Get
            Set(ByVal value As Decimal)
            End Set
        End Property
        Property OptionalEnumTest3(Optional ByVal optionalarrayEnumparam As EnumA() = Nothing) As Single
            Get
            End Get
            Set(ByVal value As Single)
            End Set
        End Property
        Property OptionalEnumTest4(Optional ByVal optionalByValarrayEnumparam() As EnumA = Nothing) As Double
            Get
            End Get
            Set(ByVal value As Double)
            End Set
        End Property

    End Class

    Class ClassD
        Public Property Test() As system.int16
            Get
            End Get
            Set(ByVal value As system.int16)
            End Set
        End Property

        Private Property Test(ByVal privateTest As Integer) As system.int32
            Get
            End Get
            Set(ByVal value As system.int32)
            End Set
        End Property

        Protected Property Test(ByVal protectedTest As Short) As system.int64
            Get
            End Get
            Set(ByVal value As system.int64)
            End Set
        End Property

        Friend Property Test(ByVal friendTest As Byte) As system.uint16
            Get
            End Get
            Set(ByVal value As system.uint16)
            End Set
        End Property

        Protected Friend Property Test(ByVal protectedfriendTest As Long) As system.uint32
            Get
            End Get
            Set(ByVal value As system.uint32)
            End Set
        End Property

    End Class

    MustInherit Class ClassE
        Property Test() As system.boolean
            Get
            End Get
            Set(ByVal value As system.boolean)
            End Set
        End Property

        Overridable Property OverridesProperty() As system.char
            Get
            End Get
            Set(ByVal value As system.char)
            End Set
        End Property

        MustOverride Property MustOverrideProperty() As system.string
        Overridable Property NotOverridableProperty() As system.datetime
            Get
            End Get
            Set(ByVal value As system.datetime)
            End Set
        End Property

        Overridable Overloads Property OverridesOverloadsProperty() As system.double
            Get
            End Get
            Set(ByVal value As system.double)
            End Set
        End Property
        Overridable Overloads Property OverridesOverloadsProperty(ByVal Param1 As String) As system.single
            Get
            End Get
            Set(ByVal value As system.single)
            End Set
        End Property

        ReadOnly Property ReadOnlyProperty() As Byte
            Get
            End Get
        End Property

        WriteOnly Property WriteOnlyProperty() As SByte
            Set(ByVal value As SByte)
            End Set
        End Property


        Default Overridable Property DefaultOverridableProperty(ByVal idx As Integer) As Object
            Get
            End Get
            Set(ByVal value As Object)
            End Set
        End Property
    End Class

    Class ClassF
        Inherits ClassE

        Default Overrides Property DefaultOverridableProperty(ByVal idx As Integer) As Object
            Get
            End Get
            Set(ByVal value As Object)
            End Set
        End Property

        Shadows Property ShadowsProperty() As system.uint64
            Get
            End Get
            Set(ByVal value As system.uint64)
            End Set
        End Property

        Shared Property SharedProperty() As system.decimal
            Get
            End Get
            Set(ByVal value As system.decimal)
            End Set
        End Property

        Overridable Property OverridableProperty() As system.int64
            Get
            End Get
            Set(ByVal value As system.int64)
            End Set
        End Property

        NotOverridable Overrides Property NotOverridableProperty() As Date
            Get
            End Get
            Set(ByVal value As Date)
            End Set
        End Property

        Overrides Property OverridesProperty() As Char
            Get
            End Get
            Set(ByVal value As Char)
            End Set
        End Property

        Overloads Property OverloadsProperty(ByVal Param1 As Integer) As Single
            Get
            End Get
            Set(ByVal value As Single)
            End Set
        End Property

        Overloads Property OverloadsProperty() As system.object
            Get
            End Get
            Set(ByVal value As system.object)
            End Set
        End Property

        Overrides Property MustOverrideProperty() As String
            Get
            End Get
            Set(ByVal value As String)
            End Set
        End Property

        Overloads Overrides Property OverridesOverloadsProperty() As Double
            Get
            End Get
            Set(ByVal value As Double)
            End Set
        End Property

    End Class

    Class ClassG
        Default WriteOnly Property DefaultWriteOnlyProperty(ByVal idx As Integer) As Boolean
            Set(ByVal value As Boolean)
            End Set
        End Property

        Property Test(ByVal value2 As Date) As Date
            Get
            End Get
            Set(ByVal value As Date)
            End Set
        End Property

    End Class

    Class ClassH
        Default ReadOnly Property DefaultReadOnlyProperty(ByVal idx As Integer) As Integer
            Get
            End Get
        End Property

        Shared Property SharedTest() As Object
            Get
            End Get
            Set(ByVal value As Object)
            End Set
        End Property

        Property Test() As Object
            Get
            End Get
            Set(ByVal value As Object)
            End Set
        End Property

    End Class

    Class ClassI
        Shared Property SharedTest() As Object
            Get
            End Get
            Set(ByVal value As Object)
            End Set
        End Property

        Default Property DefaultProperty(ByVal idx As Integer) As String
            Get
            End Get
            Set(ByVal value As String)
            End Set
        End Property
    End Class


    Structure StructureA
        Dim value As Integer
        Property Test() As Object
            Get
            End Get
            Set(ByVal value As Object)
            End Set
        End Property
        Property Test(ByVal builtinvar As Integer) As SByte
            Get
            End Get
            Set(ByVal value As SByte)
            End Set
        End Property
        Property Test(ByVal builtinByValvar As Short) As Byte
            Get
            End Get
            Set(ByVal value As Byte)
            End Set
        End Property
        Property Test(ByVal builtinarrayvar() As Integer) As Short
            Get
            End Get
            Set(ByVal value As Short)
            End Set
        End Property
        Property Test(ByVal builtinarrayvar2 As Long()) As UShort
            Get
            End Get
            Set(ByVal value As UShort)
            End Set
        End Property
        Property Test(ByVal objvar As Object) As Integer
            Get
            End Get
            Set(ByVal value As Integer)
            End Set
        End Property
        Property Test(ByVal ByValobjvar As String) As UInteger
            Get
            End Get
            Set(ByVal value As UInteger)
            End Set
        End Property
        Property Test(ByVal classvar As classa) As Long
            Get
            End Get
            Set(ByVal value As Long)
            End Set
        End Property
        Property Test(ByVal classByValvar As classb) As ULong
            Get
            End Get
            Set(ByVal value As ULong)
            End Set
        End Property
        Property Test(ByVal classarrayvar() As classa) As Single
            Get
            End Get
            Set(ByVal value As Single)
            End Set
        End Property
        Property Test(ByVal classByValarrayvar As classb()) As Double
            Get
            End Get
            Set(ByVal value As Double)
            End Set
        End Property
        Property Test(ByVal structvar As structurea) As Decimal
            Get
            End Get
            Set(ByVal value As Decimal)
            End Set
        End Property
        Property Test(ByVal structByValvar As structureb) As Date
            Get
            End Get
            Set(ByVal value As Date)
            End Set
        End Property
        Property Test(ByVal structarrayvar As structurea()) As String
            Get
            End Get
            Set(ByVal value As String)
            End Set
        End Property
        Property Test(ByVal structByValarrayvar() As structureb) As Char
            Get
            End Get
            Set(ByVal value As Char)
            End Set
        End Property
        Property Test(ByVal interfacevar As interfacea) As Boolean
            Get
            End Get
            Set(ByVal value As Boolean)
            End Set
        End Property
        Property Test(ByVal interfaceByValvar As interfaceb) As classa
            Get
            End Get
            Set(ByVal value As classa)
            End Set
        End Property
        Property Test(ByVal interfacearrayvar As interfacea()) As structurea
            Get
            End Get
            Set(ByVal value As structurea)
            End Set
        End Property
        Property Test(ByVal interfaceByValarrayvar() As interfaceb) As interfacea
            Get
            End Get
            Set(ByVal value As interfacea)
            End Set
        End Property
        Property Test(ByVal delegatevar As delegatea) As delegatea
            Get
            End Get
            Set(ByVal value As delegatea)
            End Set
        End Property
        Property Test(ByVal delegateByValvar As delegateb) As enuma
            Get
            End Get
            Set(ByVal value As enuma)
            End Set
        End Property
        Property Test(ByVal delegatearrayvar As delegatea()) As Object
            Get
            End Get
            Set(ByVal value As Object)
            End Set
        End Property
        Property Test(ByVal delegateByValarrayvar() As delegateb) As Object
            Get
            End Get
            Set(ByVal value As Object)
            End Set
        End Property
        Property Test(ByVal enumvar As enuma) As Object
            Get
            End Get
            Set(ByVal value As Object)
            End Set
        End Property
        Property Test(ByVal enumByValvar As enumb) As Object
            Get
            End Get
            Set(ByVal value As Object)
            End Set
        End Property
        Property Test(ByVal enumarrayvar As enuma()) As Object
            Get
            End Get
            Set(ByVal value As Object)
            End Set
        End Property
        Property Test(ByVal enumByValarrayvar() As enumb) As Object
            Get
            End Get
            Set(ByVal value As Object)
            End Set
        End Property
    End Structure

    Structure StructureB
        Dim value As Integer
        Property ParamArrayBuiltInTest1(ByVal ParamArray parramyarraybuiltinparam As Integer()) As SByte
            Get
            End Get
            Set(ByVal value As SByte)
            End Set
        End Property
        Property ParamArrayBuiltInTest2(ByVal ParamArray parramyarraybuiltinparam() As Integer) As Byte
            Get
            End Get
            Set(ByVal value As Byte)
            End Set
        End Property
        Property ParamArrayObjTest1(ByVal ParamArray parramyarrayObjparam() As Object) As Short
            Get
            End Get
            Set(ByVal value As Short)
            End Set
        End Property
        Property ParamArrayObjTest2(ByVal ParamArray parramyarrayObjparam As Object()) As UShort
            Get
            End Get
            Set(ByVal value As UShort)
            End Set
        End Property
        Property ParamArrayClassTest1(ByVal ParamArray parramyarrayClassparam() As ClassB) As Integer
            Get
            End Get
            Set(ByVal value As Integer)
            End Set
        End Property
        Property ParamArrayClassTest2(ByVal ParamArray parramyarrayarrayClassparam As ClassB()) As UInteger
            Get
            End Get
            Set(ByVal value As UInteger)
            End Set
        End Property
        Property ParamArrayStructureTest1(ByVal ParamArray parramyarrayStructureparam() As StructureA) As Long
            Get
            End Get
            Set(ByVal value As Long)
            End Set
        End Property
        Property ParamArrayStructureTest2(ByVal ParamArray parramyarrayStructureparam As StructureA()) As ULong
            Get
            End Get
            Set(ByVal value As ULong)
            End Set
        End Property
        Property ParamArrayInterfaceTest1(ByVal ParamArray parramyarrayInterfaceparam() As InterfaceA) As Decimal
            Get
            End Get
            Set(ByVal value As Decimal)
            End Set
        End Property
        Property ParamArrayInterfaceTest2(ByVal ParamArray parramyarrayInterfaceparam As InterfaceA()) As Single
            Get
            End Get
            Set(ByVal value As Single)
            End Set
        End Property
        Property ParamArrayDelegateTest1(ByVal ParamArray parramyarrayDelegateparam() As DelegateA) As Double
            Get
            End Get
            Set(ByVal value As Double)
            End Set
        End Property
        Property ParamArrayDelegateTest2(ByVal ParamArray parramyarrayDelegateparam As DelegateA()) As Date
            Get
            End Get
            Set(ByVal value As Date)
            End Set
        End Property
        Property ParamArrayEnumTest1(ByVal ParamArray ParamArrayEnumparam() As EnumA) As String
            Get
            End Get
            Set(ByVal value As String)
            End Set
        End Property
        Property ParamArrayEnumTest3(ByVal ParamArray ParamArrayEnumparam As EnumA()) As Char
            Get
            End Get
            Set(ByVal value As Char)
            End Set
        End Property
    End Structure

    Structure StructureC
        Dim value As Integer
        Property OptionalBuiltInTest1(Optional ByVal optionalbuiltinparam As Integer = 1) As Integer
            Get
            End Get
            Set(ByVal value As Integer)
            End Set
        End Property
        Property OptionalBuiltInTest2(Optional ByVal optionalByValbuiltinparam As Integer = 1) As UInteger
            Get
            End Get
            Set(ByVal value As UInteger)
            End Set
        End Property
        Property OptionalBuiltInTest3(Optional ByVal optionalarraybuiltinparam As Integer() = Nothing) As Boolean
            Get
            End Get
            Set(ByVal value As Boolean)
            End Set
        End Property
        Property OptionalBuiltInTest4(Optional ByVal optionalByValarraybuiltinparam() As Integer = Nothing) As Boolean
            Get
            End Get
            Set(ByVal value As Boolean)
            End Set
        End Property
        Property OptionalObjTest1(Optional ByVal optionalObjparam As Object = Nothing) As Short
            Get
            End Get
            Set(ByVal value As Short)
            End Set
        End Property
        Property OptionalObjTest2(Optional ByVal optionalByValObjparam As Object = Nothing) As UShort
            Get
            End Get
            Set(ByVal value As UShort)
            End Set
        End Property
        Property OptionalObjTest3(Optional ByVal optionalarrayObjparam As Object() = Nothing) As Long
            Get
            End Get
            Set(ByVal value As Long)
            End Set
        End Property
        Property OptionalObjTest4(Optional ByVal optionalByValarrayObjparam() As Object = Nothing) As ULong
            Get
            End Get
            Set(ByVal value As ULong)
            End Set
        End Property
        Property OptionalClassTest1(Optional ByVal optionalClassparam As ClassB = Nothing) As Decimal
            Get
            End Get
            Set(ByVal value As Decimal)
            End Set
        End Property
        Property OptionalClassTest2(Optional ByVal optionalByValClassparam As ClassB = Nothing) As Single
            Get
            End Get
            Set(ByVal value As Single)
            End Set
        End Property
        Property OptionalClassTest3(Optional ByVal optionalarrayClassparam As ClassB() = Nothing) As Double
            Get
            End Get
            Set(ByVal value As Double)
            End Set
        End Property
        Property OptionalClassTest4(Optional ByVal optionalByValarrayClassparam() As ClassB = Nothing) As Date
            Get
            End Get
            Set(ByVal value As Date)
            End Set
        End Property

        'Property OptionalStructureTest1(Optional ByVal optionalStructureparam As StructureA = Nothing)
        '
        'Property OptionalStructureTest2(Optional ByVal optionalByValStructureparam As StructureA = Nothing)
        '
        'Property OptionalStructureTest3(Optional ByVal optionalarrayStructureparam As StructureA() = Nothing)
        '
        'Property OptionalStructureTest4(Optional ByVal optionalByValarrayStructureparam() As StructureA = Nothing)
        '

        Property OptionalInterfaceTest1(Optional ByVal optionalInterfaceparam As InterfaceA = Nothing) As structurea
            Get
            End Get
            Set(ByVal value As structurea)
            End Set
        End Property
        Property OptionalInterfaceTest2(Optional ByVal optionalByValInterfaceparam As InterfaceA = Nothing) As interfacea
            Get
            End Get
            Set(ByVal value As interfacea)
            End Set
        End Property
        Property OptionalInterfaceTest3(Optional ByVal optionalarrayInterfaceparam As InterfaceA() = Nothing) As enuma
            Get
            End Get
            Set(ByVal value As enuma)
            End Set
        End Property
        Property OptionalInterfaceTest4(Optional ByVal optionalByValarrayInterfaceparam() As InterfaceA = Nothing) As delegatea
            Get
            End Get
            Set(ByVal value As delegatea)
            End Set
        End Property

        Property OptionalDelegateTest1(Optional ByVal optionalDelegateparam As DelegateA = Nothing) As classa
            Get
            End Get
            Set(ByVal value As classa)
            End Set
        End Property
        Property OptionalDelegateTest2(Optional ByVal optionalByValDelegateparam As DelegateA = Nothing) As Byte
            Get
            End Get
            Set(ByVal value As Byte)
            End Set
        End Property
        Property OptionalDelegateTest3(Optional ByVal optionalarrayDelegateparam As DelegateA() = Nothing) As Short
            Get
            End Get
            Set(ByVal value As Short)
            End Set
        End Property
        Property OptionalDelegateTest4(Optional ByVal optionalByValarrayDelegateparam() As DelegateA = Nothing) As Integer
            Get
            End Get
            Set(ByVal value As Integer)
            End Set
        End Property

        Property OptionalEnumTest1(Optional ByVal optionalEnumparam As EnumA = enuma.value) As Long
            Get
            End Get
            Set(ByVal value As Long)
            End Set
        End Property
        Property OptionalEnumTest2(Optional ByVal optionalByValEnumparam As EnumA = enuma.value) As Decimal
            Get
            End Get
            Set(ByVal value As Decimal)
            End Set
        End Property
        Property OptionalEnumTest3(Optional ByVal optionalarrayEnumparam As EnumA() = Nothing) As Single
            Get
            End Get
            Set(ByVal value As Single)
            End Set
        End Property
        Property OptionalEnumTest4(Optional ByVal optionalByValarrayEnumparam() As EnumA = Nothing) As Double
            Get
            End Get
            Set(ByVal value As Double)
            End Set
        End Property

    End Structure

    Structure StructureD
        Dim value As Integer
        Public Property Test() As system.int16
            Get
            End Get
            Set(ByVal value As system.int16)
            End Set
        End Property

        Private Property Test(ByVal privateTest As Integer) As system.int32
            Get
            End Get
            Set(ByVal value As system.int32)
            End Set
        End Property

        'Protected Property Test(ByVal protectedTest As Short) As system.int64
        '    Get
        '    End Get
        '    Set(ByVal value As system.int64)
        '    End Set
        'End Property

        Friend Property Test(ByVal friendTest As Byte) As system.uint16
            Get
            End Get
            Set(ByVal value As system.uint16)
            End Set
        End Property

        'Protected Friend Property Test(ByVal protectedfriendTest As Long) As system.uint32
        '    Get
        '    End Get
        '    Set(ByVal value As system.uint32)
        '    End Set
        'End Property

    End Structure

    Structure StructureE
        Dim value As Integer
        Property Test() As system.boolean
            Get
            End Get
            Set(ByVal value As system.boolean)
            End Set
        End Property

        'Overridable Property OverridesProperty() As system.char
        '    Get
        '    End Get
        '    Set(ByVal value As system.char)
        '    End Set
        'End Property

        'MustOverride Property MustOverrideProperty() As system.string
        'Overridable Property NotOverridableProperty() As system.datetime
        '    Get
        '    End Get
        '    Set(ByVal value As system.datetime)
        '    End Set
        'End Property

        Overloads Property OverridesOverloadsProperty() As system.double
            Get
            End Get
            Set(ByVal value As system.double)
            End Set
        End Property
        Overloads Property OverridesOverloadsProperty(ByVal Param1 As String) As system.single
            Get
            End Get
            Set(ByVal value As system.single)
            End Set
        End Property

        ReadOnly Property ReadOnlyProperty() As Byte
            Get
            End Get
        End Property

        WriteOnly Property WriteOnlyProperty() As SByte
            Set(ByVal value As SByte)
            End Set
        End Property

        Default Property DefaultProperty(ByVal idx As Integer) As String
            Get
            End Get
            Set(ByVal value As String)
            End Set
        End Property



        'Default Overridable Property DefaultOverridableProperty(ByVal idx As Integer) As Object
        '    Get
        '    End Get
        '    Set(ByVal value As Object)
        '    End Set
        'End Property

    End Structure

    Structure StructureF
        Dim value As Integer

        Default Property DefaultOverridableProperty(ByVal idx As Integer) As Object
            Get
            End Get
            Set(ByVal value As Object)
            End Set
        End Property

        Shadows Property ShadowsProperty() As system.uint64
            Get
            End Get
            Set(ByVal value As system.uint64)
            End Set
        End Property

        Shared Property SharedProperty() As system.decimal
            Get
            End Get
            Set(ByVal value As system.decimal)
            End Set
        End Property

        'Overridable Property OverridableProperty() As system.int64
        '    Get
        '    End Get
        '    Set(ByVal value As system.int64)
        '    End Set
        'End Property

        'NotOverridable Overrides Property NotOverridableProperty() As Date
        '    Get
        '    End Get
        '    Set(ByVal value As Date)
        '    End Set
        'End Property

        'Overrides Property OverridesProperty() As Char
        '    Get
        '    End Get
        '    Set(ByVal value As Char)
        '    End Set
        'End Property

        Overloads Property OverloadsProperty(ByVal Param1 As Integer) As Single
            Get
            End Get
            Set(ByVal value As Single)
            End Set
        End Property

        Overloads Property OverloadsProperty() As system.object
            Get
            End Get
            Set(ByVal value As system.object)
            End Set
        End Property

        'Overrides Property MustOverrideProperty() As String
        '    Get
        '    End Get
        '    Set(ByVal value As String)
        '    End Set
        'End Property

        Overloads Property OverridesOverloadsProperty() As Double
            Get
            End Get
            Set(ByVal value As Double)
            End Set
        End Property

    End Structure

    Structure StructureG
        Dim value As Integer
        Default WriteOnly Property DefaultWriteOnlyProperty(ByVal idx As Integer) As Boolean
            Set(ByVal value As Boolean)
            End Set
        End Property
        Property Test(ByVal value2 As Date) As Date
            Get
            End Get
            Set(ByVal value As Date)
            End Set
        End Property

    End Structure

    Structure StructureH
        Dim value As Integer

        Default ReadOnly Property DefaultReadOnlyProperty(ByVal idx As Integer) As Integer
            Get
            End Get
        End Property

        Shared Property SharedTest() As Object
            Get
            End Get
            Set(ByVal value As Object)
            End Set
        End Property

        Property Test() As Object
            Get
            End Get
            Set(ByVal value As Object)
            End Set
        End Property

    End Structure

    Structure StructureI
        Dim value As Integer
        Shared Property SharedTest() As Object
            Get
            End Get
            Set(ByVal value As Object)
            End Set
        End Property

    End Structure

    Interface InterfaceA
        Property Test() As Object
        Property Test(ByVal builtinvar As Integer) As SByte
        Property Test(ByVal builtinByValvar As Short) As Byte
        Property Test(ByVal builtinarrayvar() As Integer) As Short
        Property Test(ByVal builtinarrayvar2 As Long()) As UShort
        Property Test(ByVal objvar As Object) As Integer
        Property Test(ByVal ByValobjvar As String) As UInteger
        Property Test(ByVal classvar As classa) As Long
        Property Test(ByVal classByValvar As classb) As ULong
        Property Test(ByVal classarrayvar() As classa) As Single
        Property Test(ByVal classByValarrayvar As classb()) As Double
        Property Test(ByVal structvar As structurea) As Decimal
        Property Test(ByVal structByValvar As structureb) As Date
        Property Test(ByVal structarrayvar As structurea()) As String
        Property Test(ByVal structByValarrayvar() As structureb) As Char
        Property Test(ByVal interfacevar As interfacea) As Boolean
        Property Test(ByVal interfaceByValvar As interfaceb) As classa
        Property Test(ByVal interfacearrayvar As interfacea()) As structurea
        Property Test(ByVal interfaceByValarrayvar() As interfaceb) As interfacea
        Property Test(ByVal delegatevar As delegatea) As delegatea
        Property Test(ByVal delegateByValvar As delegateb) As enuma
        Property Test(ByVal delegatearrayvar As delegatea()) As Object
        Property Test(ByVal delegateByValarrayvar() As delegateb) As Object
        Property Test(ByVal enumvar As enuma) As Object
        Property Test(ByVal enumByValvar As enumb) As Object
        Property Test(ByVal enumarrayvar As enuma()) As Object
        Property Test(ByVal enumByValarrayvar() As enumb) As Object
    End Interface

    Interface InterfaceB
        Property ParamArrayBuiltInTest1(ByVal ParamArray parramyarraybuiltinparam As Integer()) As SByte
        Property ParamArrayBuiltInTest2(ByVal ParamArray parramyarraybuiltinparam() As Integer) As Byte
        Property ParamArrayObjTest1(ByVal ParamArray parramyarrayObjparam() As Object) As Short
        Property ParamArrayObjTest2(ByVal ParamArray parramyarrayObjparam As Object()) As UShort
        Property ParamArrayClassTest1(ByVal ParamArray parramyarrayClassparam() As ClassB) As Integer
        Property ParamArrayClassTest2(ByVal ParamArray parramyarrayarrayClassparam As ClassB()) As UInteger
        Property ParamArrayStructureTest1(ByVal ParamArray parramyarrayStructureparam() As StructureA) As Long
        Property ParamArrayStructureTest2(ByVal ParamArray parramyarrayStructureparam As StructureA()) As ULong
        Property ParamArrayInterfaceTest1(ByVal ParamArray parramyarrayInterfaceparam() As InterfaceA) As Decimal
        Property ParamArrayInterfaceTest2(ByVal ParamArray parramyarrayInterfaceparam As InterfaceA()) As Single
        Property ParamArrayDelegateTest1(ByVal ParamArray parramyarrayDelegateparam() As DelegateA) As Double
        Property ParamArrayDelegateTest2(ByVal ParamArray parramyarrayDelegateparam As DelegateA()) As Date
        Property ParamArrayEnumTest1(ByVal ParamArray ParamArrayEnumparam() As EnumA) As String
        Property ParamArrayEnumTest3(ByVal ParamArray ParamArrayEnumparam As EnumA()) As Char
    End Interface

    Interface InterfaceC
        Property OptionalBuiltInTest1(Optional ByVal optionalbuiltinparam As Integer = 1) As Integer
        Property OptionalBuiltInTest2(Optional ByVal optionalByValbuiltinparam As Integer = 1) As UInteger
        Property OptionalBuiltInTest3(Optional ByVal optionalarraybuiltinparam As Integer() = Nothing) As Boolean
        Property OptionalBuiltInTest4(Optional ByVal optionalByValarraybuiltinparam() As Integer = Nothing) As Boolean

        Property OptionalObjTest1(Optional ByVal optionalObjparam As Object = Nothing) As Short
        Property OptionalObjTest2(Optional ByVal optionalByValObjparam As Object = Nothing) As UShort
        Property OptionalObjTest3(Optional ByVal optionalarrayObjparam As Object() = Nothing) As Long
        Property OptionalObjTest4(Optional ByVal optionalByValarrayObjparam() As Object = Nothing) As ULong

        Property OptionalClassTest1(Optional ByVal optionalClassparam As ClassB = Nothing) As Decimal
        Property OptionalClassTest2(Optional ByVal optionalByValClassparam As ClassB = Nothing) As Single
        Property OptionalClassTest3(Optional ByVal optionalarrayClassparam As ClassB() = Nothing) As Double
        Property OptionalClassTest4(Optional ByVal optionalByValarrayClassparam() As ClassB = Nothing) As Date

        'Property OptionalStructureTest1(Optional ByVal optionalStructureparam As StructureA = Nothing)
        'Property OptionalStructureTest2(Optional ByVal optionalByValStructureparam As StructureA = Nothing)
        'Property OptionalStructureTest3(Optional ByVal optionalarrayStructureparam As StructureA() = Nothing)
        'Property OptionalStructureTest4(Optional ByVal optionalByValarrayStructureparam() As StructureA = Nothing)

        Property OptionalInterfaceTest1(Optional ByVal optionalInterfaceparam As InterfaceA = Nothing) As structurea
        Property OptionalInterfaceTest2(Optional ByVal optionalByValInterfaceparam As InterfaceA = Nothing) As interfacea
        Property OptionalInterfaceTest3(Optional ByVal optionalarrayInterfaceparam As InterfaceA() = Nothing) As enuma
        Property OptionalInterfaceTest4(Optional ByVal optionalByValarrayInterfaceparam() As InterfaceA = Nothing) As delegatea

        Property OptionalDelegateTest1(Optional ByVal optionalDelegateparam As DelegateA = Nothing) As classa
        Property OptionalDelegateTest2(Optional ByVal optionalByValDelegateparam As DelegateA = Nothing) As Byte
        Property OptionalDelegateTest3(Optional ByVal optionalarrayDelegateparam As DelegateA() = Nothing) As Short
        Property OptionalDelegateTest4(Optional ByVal optionalByValarrayDelegateparam() As DelegateA = Nothing) As Integer

        Property OptionalEnumTest1(Optional ByVal optionalEnumparam As EnumA = enuma.value) As Long
        Property OptionalEnumTest2(Optional ByVal optionalByValEnumparam As EnumA = enuma.value) As Decimal
        Property OptionalEnumTest3(Optional ByVal optionalarrayEnumparam As EnumA() = Nothing) As Single
        Property OptionalEnumTest4(Optional ByVal optionalByValarrayEnumparam() As EnumA = Nothing) As Double
        Default WriteOnly Property DefaultWriteOnlyProperty(ByVal idx As Integer) As Boolean
    End Interface

    Interface InterfaceD
        Property Test() As system.int16
        Property Test(ByVal privateTest As Integer) As system.int32
        Property Test(ByVal protectedTest As Short) As system.int64
        Property Test(ByVal friendTest As Byte) As system.uint16
        Property Test(ByVal protectedfriendTest As Long) As system.uint32
        Default ReadOnly Property DefaultReadOnlyProperty(ByVal idx As Integer) As Integer
    End Interface

    Interface InterfaceE
        Property Test() As system.boolean
        Property OverridesProperty() As system.char
        Property MustOverrideProperty() As system.string
        Property NotOverridableProperty() As system.datetime
        Overloads Property OverridesOverloadsProperty() As system.double
        Overloads Property OverridesOverloadsProperty(ByVal Param1 As String) As system.single
        ReadOnly Property ReadOnlyProperty() As Byte
        WriteOnly Property WriteOnlyProperty() As SByte
        Default Property DefaultProperty(ByVal idx As Integer) As String
    End Interface

    Interface InterfaceF
        Inherits InterfaceE
        Property OverridableProperty() As system.int64
        Property NotOverridableProperty() As Date
        Property OverridesProperty() As Char
        Overloads Property OverloadsProperty(ByVal Param1 As Integer) As Single
        Overloads Property OverloadsProperty() As system.object
        Property MustOverrideProperty() As String
        Overloads Property OverridesOverloadsProperty() As Double
        Default Property DefaultOverridableProperty(ByVal idx As Integer) As Object
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
