'Sub tests for option strict on.
Namespace Sub1
    Class ClassA
        Sub Test()
        End Sub
        Sub Test(ByVal builtinvar As Integer)
        End Sub
        Sub Test(ByRef builtinbyrefvar As Short)
        End Sub
        Sub Test(ByVal builtinarrayvar() As Integer)
        End Sub
        Sub Test(ByRef builtinarrayvar2 As Long())
        End Sub
        Sub Test(ByVal objvar As Object)
        End Sub
        Sub Test(ByRef byrefobjvar As String)
        End Sub
        Sub Test(ByVal classvar As classa)
        End Sub
        Sub Test(ByRef classbyrefvar As classb)
        End Sub
        Sub Test(ByVal classarrayvar() As classa)
        End Sub
        Sub Test(ByRef classbyrefarrayvar As classb())
        End Sub
        Sub Test(ByVal structvar As structurea)
        End Sub
        Sub Test(ByRef structbyrefvar As structureb)
        End Sub
        Sub Test(ByVal structarrayvar As structurea())
        End Sub
        Sub Test(ByRef structbyrefarrayvar() As structureb)
        End Sub
        Sub Test(ByVal interfacevar As interfacea)
        End Sub
        Sub Test(ByRef interfacebyrefvar As interfaceb)
        End Sub
        Sub Test(ByVal interfacearrayvar As interfacea())
        End Sub
        Sub Test(ByRef interfacebyrefarrayvar() As interfaceb)
        End Sub
        Sub Test(ByVal delegatevar As delegatea)
        End Sub
        Sub Test(ByRef delegatebyrefvar As delegateb)
        End Sub
        Sub Test(ByVal delegatearrayvar As delegatea())
        End Sub
        Sub Test(ByRef delegatebyrefarrayvar() As delegateb)
        End Sub
        Sub Test(ByVal enumvar As enuma)
        End Sub
        Sub Test(ByRef enumbyrefvar As enumb)
        End Sub
        Sub Test(ByVal enumarrayvar As enuma())
        End Sub
        Sub Test(ByRef enumbyrefarrayvar() As enumb)
        End Sub
    End Class

    Class ClassB
        Sub ParamArrayBuiltInTest1(ByVal ParamArray parramyarraybuiltinparam As Integer())
        End Sub
        Sub ParamArrayBuiltInTest2(ByVal ParamArray parramyarraybuiltinparam() As Integer)
        End Sub

        Sub ParamArrayObjTest1(ByVal ParamArray parramyarrayObjparam() As Object)
        End Sub
        Sub ParamArrayObjTest2(ByVal ParamArray parramyarrayObjparam As Object())
        End Sub

        Sub ParamArrayClassTest1(ByVal ParamArray parramyarrayClassparam() As ClassB)
        End Sub
        Sub ParamArrayClassTest2(ByVal ParamArray parramyarrayarrayClassparam As ClassB())
        End Sub

        Sub ParamArrayStructureTest1(ByVal ParamArray parramyarrayStructureparam() As StructureA)
        End Sub
        Sub ParamArrayStructureTest2(ByVal ParamArray parramyarrayStructureparam As StructureA())
        End Sub

        Sub ParamArrayInterfaceTest1(ByVal ParamArray parramyarrayInterfaceparam() As InterfaceA)
        End Sub
        Sub ParamArrayInterfaceTest2(ByVal ParamArray parramyarrayInterfaceparam As InterfaceA())
        End Sub

        Sub ParamArrayDelegateTest1(ByVal ParamArray parramyarrayDelegateparam() As DelegateA)
        End Sub
        Sub ParamArrayDelegateTest2(ByVal ParamArray parramyarrayDelegateparam As DelegateA())
        End Sub

        Sub ParamArrayEnumTest1(ByVal ParamArray ParamArrayEnumparam() As EnumA)
        End Sub
        Sub ParamArrayEnumTest3(ByVal ParamArray ParamArrayEnumparam As EnumA())
        End Sub

    End Class

    Class ClassC
        Sub OptionalBuiltInTest1(Optional ByVal optionalbuiltinparam As Integer = 1)
        End Sub
        Sub OptionalBuiltInTest2(Optional ByRef optionalbyrefbuiltinparam As Integer = 1)
        End Sub
        Sub OptionalBuiltInTest3(Optional ByVal optionalarraybuiltinparam As Integer() = Nothing)
        End Sub
        Sub OptionalBuiltInTest4(Optional ByRef optionalbyrefarraybuiltinparam() As Integer = Nothing)
        End Sub

        Sub OptionalObjTest1(Optional ByVal optionalObjparam As Object = Nothing)
        End Sub
        Sub OptionalObjTest2(Optional ByRef optionalbyrefObjparam As Object = Nothing)
        End Sub
        Sub OptionalObjTest3(Optional ByVal optionalarrayObjparam As Object() = Nothing)
        End Sub
        Sub OptionalObjTest4(Optional ByRef optionalbyrefarrayObjparam() As Object = Nothing)
        End Sub

        Sub OptionalClassTest1(Optional ByVal optionalClassparam As ClassB = Nothing)
        End Sub
        Sub OptionalClassTest2(Optional ByRef optionalbyrefClassparam As ClassB = Nothing)
        End Sub
        Sub OptionalClassTest3(Optional ByVal optionalarrayClassparam As ClassB() = Nothing)
        End Sub
        Sub OptionalClassTest4(Optional ByRef optionalbyrefarrayClassparam() As ClassB = Nothing)
        End Sub

        'Sub OptionalStructureTest1(Optional ByVal optionalStructureparam As StructureA = Nothing)
        'End Sub
        'Sub OptionalStructureTest2(Optional ByRef optionalbyrefStructureparam As StructureA = Nothing)
        'End Sub
        'Sub OptionalStructureTest3(Optional ByVal optionalarrayStructureparam As StructureA() = Nothing)
        'End Sub
        'Sub OptionalStructureTest4(Optional ByRef optionalbyrefarrayStructureparam() As StructureA = Nothing)
        'End Sub

        Sub OptionalInterfaceTest1(Optional ByVal optionalInterfaceparam As InterfaceA = Nothing)
        End Sub
        Sub OptionalInterfaceTest2(Optional ByRef optionalbyrefInterfaceparam As InterfaceA = Nothing)
        End Sub
        Sub OptionalInterfaceTest3(Optional ByVal optionalarrayInterfaceparam As InterfaceA() = Nothing)
        End Sub
        Sub OptionalInterfaceTest4(Optional ByRef optionalbyrefarrayInterfaceparam() As InterfaceA = Nothing)
        End Sub

        Sub OptionalDelegateTest1(Optional ByVal optionalDelegateparam As DelegateA = Nothing)
        End Sub
        Sub OptionalDelegateTest2(Optional ByRef optionalbyrefDelegateparam As DelegateA = Nothing)
        End Sub
        Sub OptionalDelegateTest3(Optional ByVal optionalarrayDelegateparam As DelegateA() = Nothing)
        End Sub
        Sub OptionalDelegateTest4(Optional ByRef optionalbyrefarrayDelegateparam() As DelegateA = Nothing)
        End Sub

        Sub OptionalEnumTest1(Optional ByVal optionalEnumparam As EnumA = enuma.value)
        End Sub
        Sub OptionalEnumTest2(Optional ByRef optionalbyrefEnumparam As EnumA = enuma.value)
        End Sub
        Sub OptionalEnumTest3(Optional ByVal optionalarrayEnumparam As EnumA() = Nothing)
        End Sub
        Sub OptionalEnumTest4(Optional ByRef optionalbyrefarrayEnumparam() As EnumA = Nothing)
        End Sub
    End Class

    Class ClassD
        Public Sub Test()
        End Sub
        Private Sub Test(ByVal privateTest As Integer)
        End Sub
        Protected Sub Test(ByVal protectedTest As Short)
        End Sub
        Friend Sub Test(ByVal friendTest As Byte)
        End Sub
        Protected Friend Sub Test(ByVal protectedfriendTest As Long)
        End Sub
    End Class

    MustInherit Class ClassE
        Sub Test()
        End Sub
        Overridable Sub OverridesSub()
        End Sub
        MustOverride Sub MustOverrideSub()
        Overridable Sub NotOverridableSub()
        End Sub
        Overridable Overloads Sub OverridesOverloadsSub()
        End Sub
        Overridable Overloads Sub OverridesOverloadsSub(ByVal Param1 As String)
        End Sub
    End Class

    Class ClassF
        Inherits ClassE
        Shadows Sub ShadowsSub()
        End Sub
        Shared Sub SharedSub()
        End Sub
        Overridable Sub OverridableSub()
        End Sub
        NotOverridable Overrides Sub NotOverridableSub()
        End Sub
        Overrides Sub OverridesSub()
        End Sub
        Overloads Sub OverloadsSub(ByVal Param1 As Integer)
        End Sub
        Overloads Sub OverloadsSub()
        End Sub
        Overrides Sub MustOverrideSub()
        End Sub
        Overloads Overrides Sub OverridesOverloadsSub()
        End Sub
    End Class

    Class ClassG
        Sub Test(ByVal value As Date)
        End Sub
    End Class

    Class ClassH
        Shared Sub SharedTest()
        End Sub
        Sub Test()
        End Sub
    End Class

    Class ClassI
        Shared Sub SharedTest()
        End Sub
    End Class

    Structure StructureA
        Dim value As Integer
        Sub Test()
        End Sub
        Sub Test(ByVal builtinvar As Integer)
        End Sub
        Sub Test(ByRef builtinbyrefvar As Short)
        End Sub
        Sub Test(ByVal builtinarrayvar() As Integer)
        End Sub
        Sub Test(ByRef builtinarrayvar2 As Long())
        End Sub
        Sub Test(ByVal objvar As Object)
        End Sub
        Sub Test(ByRef byrefobjvar As String)
        End Sub
        Sub Test(ByVal classvar As classa)
        End Sub
        Sub Test(ByRef classbyrefvar As classb)
        End Sub
        Sub Test(ByVal classarrayvar() As classa)
        End Sub
        Sub Test(ByRef classbyrefarrayvar As classb())
        End Sub
        Sub Test(ByVal structvar As structurea)
        End Sub
        Sub Test(ByRef structbyrefvar As structureb)
        End Sub
        Sub Test(ByVal structarrayvar As structurea())
        End Sub
        Sub Test(ByRef structbyrefarrayvar() As structureb)
        End Sub
        Sub Test(ByVal interfacevar As interfacea)
        End Sub
        Sub Test(ByRef interfacebyrefvar As interfaceb)
        End Sub
        Sub Test(ByVal interfacearrayvar As interfacea())
        End Sub
        Sub Test(ByRef interfacebyrefarrayvar() As interfaceb)
        End Sub
        Sub Test(ByVal delegatevar As delegatea)
        End Sub
        Sub Test(ByRef delegatebyrefvar As delegateb)
        End Sub
        Sub Test(ByVal delegatearrayvar As delegatea())
        End Sub
        Sub Test(ByRef delegatebyrefarrayvar() As delegateb)
        End Sub
        Sub Test(ByVal enumvar As enuma)
        End Sub
        Sub Test(ByRef enumbyrefvar As enumb)
        End Sub
        Sub Test(ByVal enumarrayvar As enuma())
        End Sub
        Sub Test(ByRef enumbyrefarrayvar() As enumb)
        End Sub
    End Structure

    Structure StructureB
        Dim value As Integer
        Sub ParamArrayBuiltInTest1(ByVal ParamArray parramyarraybuiltinparam As Integer())
        End Sub
        Sub ParamArrayBuiltInTest2(ByVal ParamArray parramyarraybuiltinparam() As Integer)
        End Sub

        Sub ParamArrayObjTest1(ByVal ParamArray parramyarrayObjparam() As Object)
        End Sub
        Sub ParamArrayObjTest2(ByVal ParamArray parramyarrayObjparam As Object())
        End Sub

        Sub ParamArrayClassTest1(ByVal ParamArray parramyarrayClassparam() As ClassB)
        End Sub
        Sub ParamArrayClassTest2(ByVal ParamArray parramyarrayarrayClassparam As ClassB())
        End Sub

        Sub ParamArrayStructureTest1(ByVal ParamArray parramyarrayStructureparam() As StructureA)
        End Sub
        Sub ParamArrayStructureTest2(ByVal ParamArray parramyarrayStructureparam As StructureA())
        End Sub

        Sub ParamArrayInterfaceTest1(ByVal ParamArray parramyarrayInterfaceparam() As InterfaceA)
        End Sub
        Sub ParamArrayInterfaceTest2(ByVal ParamArray parramyarrayInterfaceparam As InterfaceA())
        End Sub

        Sub ParamArrayDelegateTest1(ByVal ParamArray parramyarrayDelegateparam() As DelegateA)
        End Sub
        Sub ParamArrayDelegateTest2(ByVal ParamArray parramyarrayDelegateparam As DelegateA())
        End Sub

        Sub ParamArrayEnumTest1(ByVal ParamArray ParamArrayEnumparam() As EnumA)
        End Sub
        Sub ParamArrayEnumTest3(ByVal ParamArray ParamArrayEnumparam As EnumA())
        End Sub

    End Structure

    Structure StructureC
        Dim value As Integer
        Sub OptionalBuiltInTest1(Optional ByVal optionalbuiltinparam As Integer = 1)
        End Sub
        Sub OptionalBuiltInTest2(Optional ByRef optionalbyrefbuiltinparam As Integer = 1)
        End Sub
        Sub OptionalBuiltInTest3(Optional ByVal optionalarraybuiltinparam As Integer() = Nothing)
        End Sub
        Sub OptionalBuiltInTest4(Optional ByRef optionalbyrefarraybuiltinparam() As Integer = Nothing)
        End Sub

        Sub OptionalObjTest1(Optional ByVal optionalObjparam As Object = Nothing)
        End Sub
        Sub OptionalObjTest2(Optional ByRef optionalbyrefObjparam As Object = Nothing)
        End Sub
        Sub OptionalObjTest3(Optional ByVal optionalarrayObjparam As Object() = Nothing)
        End Sub
        Sub OptionalObjTest4(Optional ByRef optionalbyrefarrayObjparam() As Object = Nothing)
        End Sub

        Sub OptionalClassTest1(Optional ByVal optionalClassparam As ClassB = Nothing)
        End Sub
        Sub OptionalClassTest2(Optional ByRef optionalbyrefClassparam As ClassB = Nothing)
        End Sub
        Sub OptionalClassTest3(Optional ByVal optionalarrayClassparam As ClassB() = Nothing)
        End Sub
        Sub OptionalClassTest4(Optional ByRef optionalbyrefarrayClassparam() As ClassB = Nothing)
        End Sub

        'Sub OptionalStructureTest1(Optional ByVal optionalStructureparam As StructureA = Nothing)
        'End Sub
        'Sub OptionalStructureTest2(Optional ByRef optionalbyrefStructureparam As StructureA = Nothing)
        'End Sub
        'Sub OptionalStructureTest3(Optional ByVal optionalarrayStructureparam As StructureA() = Nothing)
        'End Sub
        'Sub OptionalStructureTest4(Optional ByRef optionalbyrefarrayStructureparam() As StructureA = Nothing)
        'End Sub

        Sub OptionalInterfaceTest1(Optional ByVal optionalInterfaceparam As InterfaceA = Nothing)
        End Sub
        Sub OptionalInterfaceTest2(Optional ByRef optionalbyrefInterfaceparam As InterfaceA = Nothing)
        End Sub
        Sub OptionalInterfaceTest3(Optional ByVal optionalarrayInterfaceparam As InterfaceA() = Nothing)
        End Sub
        Sub OptionalInterfaceTest4(Optional ByRef optionalbyrefarrayInterfaceparam() As InterfaceA = Nothing)
        End Sub

        Sub OptionalDelegateTest1(Optional ByVal optionalDelegateparam As DelegateA = Nothing)
        End Sub
        Sub OptionalDelegateTest2(Optional ByRef optionalbyrefDelegateparam As DelegateA = Nothing)
        End Sub
        Sub OptionalDelegateTest3(Optional ByVal optionalarrayDelegateparam As DelegateA() = Nothing)
        End Sub
        Sub OptionalDelegateTest4(Optional ByRef optionalbyrefarrayDelegateparam() As DelegateA = Nothing)
        End Sub

        Sub OptionalEnumTest1(Optional ByVal optionalEnumparam As EnumA = enuma.value)
        End Sub
        Sub OptionalEnumTest2(Optional ByRef optionalbyrefEnumparam As EnumA = enuma.value)
        End Sub
        Sub OptionalEnumTest3(Optional ByVal optionalarrayEnumparam As EnumA() = Nothing)
        End Sub
        Sub OptionalEnumTest4(Optional ByRef optionalbyrefarrayEnumparam() As EnumA = Nothing)
        End Sub
    End Structure

    Structure StructureD
        Dim value As Integer
        Public Sub Test()
        End Sub
        Private Sub Test(ByVal privateTest As Integer)
        End Sub
        'Protected Sub Test(ByVal protectedTest As Short)
        'End Sub
        Friend Sub Test(ByVal friendTest As Byte)
        End Sub
        'Protected Friend Sub Test(ByVal protectedfriendTest As Long)
        'End Sub
    End Structure

    Structure StructureE
        Dim value As Integer
        Sub Test()
        End Sub
        Protected Overrides Sub Finalize()
        End Sub
        Overloads Sub OverloadsSub(ByVal Param1 As Integer)
        End Sub
        Overloads Sub OverloadsSub()
        End Sub
        Shadows Sub ShadowsSub()
        End Sub
        Shared Sub SharedSub()
        End Sub
    End Structure

    Interface InterfaceA
        Sub Test()
        Sub Test(ByVal builtinvar As Integer)
        Sub Test(ByRef builtinbyrefvar As Short)
        Sub Test(ByVal builtinarrayvar() As Integer)
        Sub Test(ByRef builtinarrayvar2 As Long())
        Sub Test(ByVal objvar As Object)
        Sub Test(ByRef byrefobjvar As String)
        Sub Test(ByVal classvar As classa)
        Sub Test(ByRef classbyrefvar As classb)
        Sub Test(ByVal classarrayvar() As classa)
        Sub Test(ByRef classbyrefarrayvar As classb())
        Sub Test(ByVal structvar As structurea)
        Sub Test(ByRef structbyrefvar As structureb)
        Sub Test(ByVal structarrayvar As structurea())
        Sub Test(ByRef structbyrefarrayvar() As structureb)
        Sub Test(ByVal interfacevar As interfacea)
        Sub Test(ByRef interfacebyrefvar As interfaceb)
        Sub Test(ByVal interfacearrayvar As interfacea())
        Sub Test(ByRef interfacebyrefarrayvar() As interfaceb)
        Sub Test(ByVal delegatevar As delegatea)
        Sub Test(ByRef delegatebyrefvar As delegateb)
        Sub Test(ByVal delegatearrayvar As delegatea())
        Sub Test(ByRef delegatebyrefarrayvar() As delegateb)
        Sub Test(ByVal enumvar As enuma)
        Sub Test(ByRef enumbyrefvar As enumb)
        Sub Test(ByVal enumarrayvar As enuma())
        Sub Test(ByRef enumbyrefarrayvar() As enumb)
    End Interface

    Interface InterfaceB
        Sub ParamArrayBuiltInTest1(ByVal ParamArray parramyarraybuiltinparam As Integer())
        Sub ParamArrayBuiltInTest2(ByVal ParamArray parramyarraybuiltinparam() As Integer)

        Sub ParamArrayObjTest1(ByVal ParamArray parramyarrayObjparam() As Object)
        Sub ParamArrayObjTest2(ByVal ParamArray parramyarrayObjparam As Object())

        Sub ParamArrayClassTest1(ByVal ParamArray parramyarrayClassparam() As ClassB)
        Sub ParamArrayClassTest2(ByVal ParamArray parramyarrayarrayClassparam As ClassB())

        Sub ParamArrayStructureTest1(ByVal ParamArray parramyarrayStructureparam() As StructureA)
        Sub ParamArrayStructureTest2(ByVal ParamArray parramyarrayStructureparam As StructureA())

        Sub ParamArrayInterfaceTest1(ByVal ParamArray parramyarrayInterfaceparam() As InterfaceA)
        Sub ParamArrayInterfaceTest2(ByVal ParamArray parramyarrayInterfaceparam As InterfaceA())

        Sub ParamArrayDelegateTest1(ByVal ParamArray parramyarrayDelegateparam() As DelegateA)
        Sub ParamArrayDelegateTest2(ByVal ParamArray parramyarrayDelegateparam As DelegateA())

        Sub ParamArrayEnumTest1(ByVal ParamArray ParamArrayEnumparam() As EnumA)
        Sub ParamArrayEnumTest3(ByVal ParamArray ParamArrayEnumparam As EnumA())
    End Interface

    Interface InterfaceC
        Sub OptionalBuiltInTest1(Optional ByVal optionalbuiltinparam As Integer = 1)
        Sub OptionalBuiltInTest2(Optional ByRef optionalbyrefbuiltinparam As Integer = 1)
        Sub OptionalBuiltInTest3(Optional ByVal optionalarraybuiltinparam As Integer() = Nothing)
        Sub OptionalBuiltInTest4(Optional ByRef optionalbyrefarraybuiltinparam() As Integer = Nothing)

        Sub OptionalObjTest1(Optional ByVal optionalObjparam As Object = Nothing)
        Sub OptionalObjTest2(Optional ByRef optionalbyrefObjparam As Object = Nothing)
        Sub OptionalObjTest3(Optional ByVal optionalarrayObjparam As Object() = Nothing)
        Sub OptionalObjTest4(Optional ByRef optionalbyrefarrayObjparam() As Object = Nothing)

        Sub OptionalClassTest1(Optional ByVal optionalClassparam As ClassB = Nothing)
        Sub OptionalClassTest2(Optional ByRef optionalbyrefClassparam As ClassB = Nothing)
        Sub OptionalClassTest3(Optional ByVal optionalarrayClassparam As ClassB() = Nothing)
        Sub OptionalClassTest4(Optional ByRef optionalbyrefarrayClassparam() As ClassB = Nothing)

        Sub OptionalInterfaceTest1(Optional ByVal optionalInterfaceparam As InterfaceA = Nothing)
        Sub OptionalInterfaceTest2(Optional ByRef optionalbyrefInterfaceparam As InterfaceA = Nothing)
        Sub OptionalInterfaceTest3(Optional ByVal optionalarrayInterfaceparam As InterfaceA() = Nothing)
        Sub OptionalInterfaceTest4(Optional ByRef optionalbyrefarrayInterfaceparam() As InterfaceA = Nothing)

        Sub OptionalDelegateTest1(Optional ByVal optionalDelegateparam As DelegateA = Nothing)
        Sub OptionalDelegateTest2(Optional ByRef optionalbyrefDelegateparam As DelegateA = Nothing)
        Sub OptionalDelegateTest3(Optional ByVal optionalarrayDelegateparam As DelegateA() = Nothing)
        Sub OptionalDelegateTest4(Optional ByRef optionalbyrefarrayDelegateparam() As DelegateA = Nothing)

        Sub OptionalEnumTest1(Optional ByVal optionalEnumparam As EnumA = enuma.value)
        Sub OptionalEnumTest2(Optional ByRef optionalbyrefEnumparam As EnumA = enuma.value)
        Sub OptionalEnumTest3(Optional ByVal optionalarrayEnumparam As EnumA() = Nothing)
        Sub OptionalEnumTest4(Optional ByRef optionalbyrefarrayEnumparam() As EnumA = Nothing)
    End Interface

    Interface InterfaceD
        Sub Test()
        Sub Test(ByVal privateTest As Integer)
        Sub Test(ByVal protectedTest As Short)
        Sub Test(ByVal friendTest As Byte)
        Sub Test(ByVal protectedfriendTest As Long)
    End Interface

    Interface InterfaceE
        Sub Test()
        Sub OverridesSub()
        Sub MustOverrideSub()
        Sub NotOverridableSub()
        Overloads Sub OverridesOverloadsSub()
        Overloads Sub OverridesOverloadsSub(ByVal Param1 As String)
    End Interface

    Interface InterfaceF
        Inherits InterfaceE
        Sub OverridableSub()
        Sub NotOverridableSub()
        Sub OverridesSub()
        Overloads Sub OverloadsSub(ByVal Param1 As Integer)
        Overloads Sub OverloadsSub()
        Sub MustOverrideSub()
        Overloads Sub OverridesOverloadsSub()
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
