Class generated
    Shared Sub Main
Dim RuntimeType_1 As System.Type
RuntimeType_1 = GetType(Object)
RuntimeType_1 = GetType(Object)
Dim AssemblyName_1 As System.Reflection.AssemblyName
AssemblyName_1 = New System.Reflection.AssemblyName
AssemblyName_1.Name = "TypeParameter1"
Dim AssemblyBuilder_1 As System.Reflection.Emit.AssemblyBuilder
AssemblyBuilder_1 = System.AppDomain.CurrentDomain.DefineDynamicAssembly(AssemblyName_1, System.Reflection.Emit.AssemblyBuilderAccess.Save, "Z:\mono\head\mono-basic\vbnc\vbnc\tests\Unfixable\testoutput")
Dim ModuleBuilder_1 As System.Reflection.Emit.ModuleBuilder
ModuleBuilder_1 = AssemblyBuilder_1.DefineDynamicModule("TypeParameter1", "Z:\mono\head\mono-basic\vbnc\vbnc\tests\Unfixable\testoutput", True)
Dim TypeBuilder_1 As System.Reflection.Emit.TypeBuilder
TypeBuilder_1 = ModuleBuilder_1.DefineType("TypeParameter1.C1`1", 0)
Dim TypeBuilder_2 As System.Reflection.Emit.TypeBuilder
TypeBuilder_2 = ModuleBuilder_1.DefineType("TypeParameter1.C2`1", 0)
Dim TypeBuilder_3 As System.Reflection.Emit.TypeBuilder
TypeBuilder_3 = ModuleBuilder_1.DefineType("TypeParameter1.C3", 0)
Dim StringArray_1 As System.String()
StringArray_1 = new String () {"Y"}
Dim GenericTypeParameterBuilderArray_1 As System.Reflection.Emit.GenericTypeParameterBuilder()
GenericTypeParameterBuilderArray_1 = TypeBuilder_1.DefineGenericParameters(StringArray_1)
Dim GenericTypeParameterBuilder_1 As System.Reflection.Emit.GenericTypeParameterBuilder
GenericTypeParameterBuilder_1 = GenericTypeParameterBuilderArray_1(0)
GenericTypeParameterBuilder_1.SetGenericParameterAttributes(System.Reflection.GenericParameterAttributes.None)
Dim StringArray_2 As System.String()
StringArray_2 = new String () {"Z"}
Dim GenericTypeParameterBuilderArray_3 As System.Reflection.Emit.GenericTypeParameterBuilder()
GenericTypeParameterBuilderArray_3 = TypeBuilder_2.DefineGenericParameters(StringArray_2)
Dim GenericTypeParameterBuilder_2 As System.Reflection.Emit.GenericTypeParameterBuilder
GenericTypeParameterBuilder_2 = GenericTypeParameterBuilderArray_3(0)
GenericTypeParameterBuilder_2.SetGenericParameterAttributes(System.Reflection.GenericParameterAttributes.None)
TypeBuilder_1.SetParent(RuntimeType_1)
Dim TypeBuilderInstantiation_1 As System.Type
TypeBuilder_2.SetParent(TypeBuilderInstantiation_1)
TypeBuilder_3.SetParent(RuntimeType_1)
Dim MethodBuilder_1 As System.Reflection.Emit.MethodBuilder
MethodBuilder_1 = TypeBuilder_3.DefineMethod("M2", CType(6, System.Reflection.MethodAttributes))
Dim StringArray_3 As System.String()
StringArray_3 = new String () {"A"}
Dim GenericTypeParameterBuilderArray_5 As System.Reflection.Emit.GenericTypeParameterBuilder()
GenericTypeParameterBuilderArray_5 = MethodBuilder_1.DefineGenericParameters(StringArray_3)
Dim GenericTypeParameterBuilder_3 As System.Reflection.Emit.GenericTypeParameterBuilder
GenericTypeParameterBuilder_3 = GenericTypeParameterBuilderArray_5(0)
GenericTypeParameterBuilder_3.SetGenericParameterAttributes(System.Reflection.GenericParameterAttributes.DefaultConstructorConstraint)
Dim MethodBuilder_2 As System.Reflection.Emit.MethodBuilder
MethodBuilder_2 = TypeBuilder_3.DefineMethod("M1", CType(6, System.Reflection.MethodAttributes))
Dim StringArray_4 As System.String()
StringArray_4 = new String () {"B"}
Dim GenericTypeParameterBuilderArray_7 As System.Reflection.Emit.GenericTypeParameterBuilder()
GenericTypeParameterBuilderArray_7 = MethodBuilder_2.DefineGenericParameters(StringArray_4)
Dim GenericTypeParameterBuilder_4 As System.Reflection.Emit.GenericTypeParameterBuilder
GenericTypeParameterBuilder_4 = GenericTypeParameterBuilderArray_7(0)
GenericTypeParameterBuilder_4.SetGenericParameterAttributes(System.Reflection.GenericParameterAttributes.None)
    End Sub
End Class
