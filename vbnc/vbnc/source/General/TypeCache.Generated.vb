' 
' Visual Basic.Net Compiler
' Copyright (C) 2004 - 2010 Rolf Bjarne Kvinge, RKvinge@novell.com
' 
' This library is free software; you can redistribute it and/or
' modify it under the terms of the GNU Lesser General Public
' License as published by the Free Software Foundation; either
' version 2.1 of the License, or (at your option) any later version.
' 
' This library is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
' Lesser General Public License for more details.
' 
' You should have received a copy of the GNU Lesser General Public
' License along with this library; if not, write to the Free Software
' Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
' 
Public Partial Class CecilTypeCache
    Public System_Boolean As Mono.Cecil.TypeDefinition
    Public System_Boolean_Array As Mono.Cecil.TypeReference
    Public System_Byte As Mono.Cecil.TypeDefinition
    Public System_Byte_Array As Mono.Cecil.TypeReference
    Public System_Char As Mono.Cecil.TypeDefinition
    Public System_Char_Array As Mono.Cecil.TypeReference
    Public System_DateTime As Mono.Cecil.TypeDefinition
    Public System_Decimal As Mono.Cecil.TypeDefinition
    Public System_Double As Mono.Cecil.TypeDefinition
    Public System_Int32 As Mono.Cecil.TypeDefinition
    Public System_Int64 As Mono.Cecil.TypeDefinition
    Public System_Object As Mono.Cecil.TypeDefinition
    Public System_Object_Array As Mono.Cecil.TypeReference
    Public System_Object_ByRef As Mono.Cecil.TypeReference
    Public System_Int16 As Mono.Cecil.TypeDefinition
    Public System_Single As Mono.Cecil.TypeDefinition
    Public System_String As Mono.Cecil.TypeDefinition
    Public System_String_ByRef As Mono.Cecil.TypeReference
    Public System_String_Array As Mono.Cecil.TypeReference
    Public System_String__ctor_Array As Mono.Cecil.MethodDefinition
    Public System_SByte As Mono.Cecil.TypeDefinition
    Public System_UInt16 As Mono.Cecil.TypeDefinition
    Public System_UInt32 As Mono.Cecil.TypeDefinition
    Public System_UInt64 As Mono.Cecil.TypeDefinition
    Public System_Int32_Array As Mono.Cecil.TypeReference
    Public System_Enum As Mono.Cecil.TypeDefinition
    Public System_ValueType As Mono.Cecil.TypeDefinition
    Public System_Delegate As Mono.Cecil.TypeDefinition
    Public System_MulticastDelegate As Mono.Cecil.TypeDefinition
    Public System_AsyncCallback As Mono.Cecil.TypeDefinition
    Public System_IAsyncResult As Mono.Cecil.TypeDefinition
    Public System_Collections_IEnumerator As Mono.Cecil.TypeDefinition
    Public System_Collections_IEnumerator__get_Current As Mono.Cecil.MethodDefinition
    Public System_Collections_IEnumerator__MoveNext As Mono.Cecil.MethodDefinition
    Public System_Collections_IEnumerable As Mono.Cecil.TypeDefinition
    Public System_Collections_IEnumerable__GetEnumerator As Mono.Cecil.MethodDefinition
    Public System_IDisposable As Mono.Cecil.TypeDefinition
    Public System_IDisposable__Dispose As Mono.Cecil.MethodDefinition
    Public System_RuntimeTypeHandle As Mono.Cecil.TypeDefinition
    Public System_Type As Mono.Cecil.TypeDefinition
    Public System_Type_Array As Mono.Cecil.TypeReference
    Public System_Type__GetTypeFromHandle_RuntimeTypeHandle As Mono.Cecil.MethodDefinition
    Public System_Void As Mono.Cecil.TypeDefinition
    Public System_Exception As Mono.Cecil.TypeDefinition
    Public System_Array As Mono.Cecil.TypeDefinition
    Public System_DBNull As Mono.Cecil.TypeDefinition
    Public System_SerializableAttribute As Mono.Cecil.TypeDefinition
    Public System_Array__SetValue As Mono.Cecil.MethodDefinition
    Public System_Array__GetValue As Mono.Cecil.MethodDefinition
    Public System_Array__CreateInstance As Mono.Cecil.MethodDefinition
    Public System_Activator As Mono.Cecil.TypeDefinition
    Public System_Activator__CreateInstance As Mono.Cecil.MethodDefinition
    Public System_ArgumentException As Mono.Cecil.TypeDefinition
    Public System_Collections_Generic_IList1 As Mono.Cecil.TypeDefinition
    Public System_Collections_Generic_ICollection1 As Mono.Cecil.TypeDefinition
    Public System_Collections_Generic_IEnumerable1 As Mono.Cecil.TypeDefinition
    Public System_Reflection_AssemblyVersionAttribute As Mono.Cecil.TypeDefinition
    Public System_Reflection_AssemblyProductAttribute As Mono.Cecil.TypeDefinition
    Public System_Reflection_AssemblyCompanyAttribute As Mono.Cecil.TypeDefinition
    Public System_Reflection_AssemblyCopyrightAttribute As Mono.Cecil.TypeDefinition
    Public System_Reflection_AssemblyTrademarkAttribute As Mono.Cecil.TypeDefinition
    Public System_Reflection_AssemblyKeyNameAttribute As Mono.Cecil.TypeDefinition
    Public System_Reflection_AssemblyKeyFileAttribute As Mono.Cecil.TypeDefinition
    Public System_Reflection_AssemblyDelaySignAttribute As Mono.Cecil.TypeDefinition
    Public System_Diagnostics_ConditionalAttribute As Mono.Cecil.TypeDefinition
    Public System_Diagnostics_DebuggableAttribute As Mono.Cecil.TypeDefinition
    Public System_Diagnostics_DebuggableAttribute_DebuggingModes As Mono.Cecil.TypeDefinition
    Public System_Diagnostics_DebuggableAttribute__ctor_DebuggingModes As Mono.Cecil.MethodDefinition
    Public System_ParamArrayAttribute As Mono.Cecil.TypeDefinition
    Public System_ParamArrayAttribute__ctor As Mono.Cecil.MethodDefinition
    Public System_Nullable1 As Mono.Cecil.TypeDefinition
    Public System_Nullable1__get_HasValue As Mono.Cecil.MethodDefinition
    Public System_Nullable1__GetValueOrDefault As Mono.Cecil.MethodDefinition
    Public System_Runtime_CompilerServices_RuntimeHelpers As Mono.Cecil.TypeDefinition
    Public System_STAThreadAttribute As Mono.Cecil.TypeDefinition
    Public System_STAThreadAttribute__ctor As Mono.Cecil.MethodDefinition
    Public System_IntPtr As Mono.Cecil.TypeDefinition
    Public System_Runtime_CompilerServices_DateTimeConstantAttribute As Mono.Cecil.TypeDefinition
    Public System_Runtime_CompilerServices_DateTimeConstantAttribute__ctor_Int64 As Mono.Cecil.MethodDefinition
    Public System_DateTime__ctor_Int64 As Mono.Cecil.MethodDefinition
    Public System_Decimal__ctor_Int32 As Mono.Cecil.MethodDefinition
    Public System_Decimal__ctor_Int64 As Mono.Cecil.MethodDefinition
    Public System_Decimal__ctor_Double As Mono.Cecil.MethodDefinition
    Public System_Decimal__ctor_Single As Mono.Cecil.MethodDefinition
    Public System_Decimal__ctor_UInt64 As Mono.Cecil.MethodDefinition
    Public System_Decimal__ctor_Int32_Int32_Int32_Boolean_Byte As Mono.Cecil.MethodDefinition
    Public System_Decimal__Zero As Mono.Cecil.FieldDefinition
    Public System_Decimal__One As Mono.Cecil.FieldDefinition
    Public System_Decimal__MinusOne As Mono.Cecil.FieldDefinition
    Public System_Runtime_CompilerServices_DecimalConstantAttribute As Mono.Cecil.TypeDefinition
    Public System_Runtime_CompilerServices_DecimalConstantAttribute__ctor_Byte_Byte_UInt32_UInt32_UInt32 As Mono.Cecil.MethodDefinition
    Public System_Runtime_CompilerServices_DecimalConstantAttribute__ctor_Byte_Byte_Int32_Int32_Int32 As Mono.Cecil.MethodDefinition
    Public System_Runtime_CompilerServices_AccessedThroughPropertyAttribute As Mono.Cecil.TypeDefinition
    Public System_Runtime_CompilerServices_AccessedThroughPropertyAttribute__ctor_String As Mono.Cecil.MethodDefinition
    Public System_Decimal__Compare_Decimal_Decimal As Mono.Cecil.MethodDefinition
    Public System_DateTime__Compare_DateTime_DateTime As Mono.Cecil.MethodDefinition
    Public System_Decimal__Add_Decimal_Decimal As Mono.Cecil.MethodDefinition
    Public System_Decimal__Subtract_Decimal_Decimal As Mono.Cecil.MethodDefinition
    Public System_Decimal__Divide_Decimal_Decimal As Mono.Cecil.MethodDefinition
    Public System_Decimal__Multiply_Decimal_Decimal As Mono.Cecil.MethodDefinition
    Public System_Decimal__Remainder_Decimal_Decimal As Mono.Cecil.MethodDefinition
    Public System_Decimal__Negate_Decimal As Mono.Cecil.MethodDefinition
    Public System_Reflection_DefaultMemberAttribute As Mono.Cecil.TypeDefinition
    Public System_Reflection_DefaultMemberAttribute__ctor_String As Mono.Cecil.MethodDefinition
    Public System_Convert As Mono.Cecil.TypeDefinition
    Public System_Convert__ToSingle_Decimal As Mono.Cecil.MethodDefinition
    Public System_Convert__ToDouble_Decimal As Mono.Cecil.MethodDefinition
    Public System_Convert__ToBoolean_Decimal As Mono.Cecil.MethodDefinition
    Public System_Convert__ToByte_Decimal As Mono.Cecil.MethodDefinition
    Public System_Convert__ToSByte_Decimal As Mono.Cecil.MethodDefinition
    Public System_Convert__ToInt16_Decimal As Mono.Cecil.MethodDefinition
    Public System_Convert__ToUInt16_Decimal As Mono.Cecil.MethodDefinition
    Public System_Convert__ToInt32_Decimal As Mono.Cecil.MethodDefinition
    Public System_Convert__ToUInt32_Decimal As Mono.Cecil.MethodDefinition
    Public System_Convert__ToInt64_Decimal As Mono.Cecil.MethodDefinition
    Public System_Convert__ToUInt64_Decimal As Mono.Cecil.MethodDefinition
    Public System_String__Concat_String_String As Mono.Cecil.MethodDefinition
    Public System_Diagnostics_Debugger As Mono.Cecil.TypeDefinition
    Public System_Diagnostics_Debugger__Break As Mono.Cecil.MethodDefinition
    Public System_Reflection_Missing As Mono.Cecil.TypeDefinition
    Public System_Reflection_Missing__Value As Mono.Cecil.FieldDefinition
    Public System_Threading_Monitor As Mono.Cecil.TypeDefinition
    Public System_Threading_Monitor__Enter_Object As Mono.Cecil.MethodDefinition
    Public System_Threading_Monitor__Exit_Object As Mono.Cecil.MethodDefinition
    Public System_Runtime_CompilerServices_RuntimeHelpers__GetObjectValue_Object As Mono.Cecil.MethodDefinition
    Public System_Math As Mono.Cecil.TypeDefinition
    Public System_Math__Round_Double As Mono.Cecil.MethodDefinition
    Public System_Math__Pow_Double_Double As Mono.Cecil.MethodDefinition
    Public System_Runtime_InteropServices_DllImportAttribute As Mono.Cecil.TypeDefinition
    Public System_Runtime_InteropServices_MarshalAsAttribute As Mono.Cecil.TypeDefinition
    Public System_Runtime_InteropServices_StructLayoutAttribute As Mono.Cecil.TypeDefinition
    Public System_Runtime_InteropServices_FieldOffsetAttribute As Mono.Cecil.TypeDefinition
    Public System_Runtime_InteropServices_CoClassAttribute As Mono.Cecil.TypeDefinition
    Public System_Runtime_InteropServices_OutAttribute As Mono.Cecil.TypeDefinition
    Public System_Security_Permissions_SecurityAttribute As Mono.Cecil.TypeDefinition
    Public System_Windows_Forms_Form As Mono.Cecil.TypeDefinition
    Public System_Windows_Forms_Application As Mono.Cecil.TypeDefinition
    Public System_Windows_Forms_Application__Run As Mono.Cecil.MethodDefinition
    Public System_Delegate__Combine As Mono.Cecil.MethodDefinition
    Public System_Delegate__Remove As Mono.Cecil.MethodDefinition
    Public MS_VB_CompareMethod As Mono.Cecil.TypeDefinition
    Public MS_VB_CS_Conversions As Mono.Cecil.TypeDefinition
    Public MS_VB_CS_ProjectData As Mono.Cecil.TypeDefinition
    Public MS_VB_CS_LikeOperator As Mono.Cecil.TypeDefinition
    Public MS_VB_Strings As Mono.Cecil.TypeDefinition
    Public MS_VB_CS_StringType As Mono.Cecil.TypeDefinition
    Public MS_VB_MyGroupCollectionAttribute As Mono.Cecil.TypeDefinition
    Public MS_VB_CallType As Mono.Cecil.TypeDefinition
    Public MS_VB_Information As Mono.Cecil.TypeDefinition
    Public MS_VB_Information__IsNumeric As Mono.Cecil.MethodDefinition
    Public MS_VB_Information__SystemTypeName As Mono.Cecil.MethodDefinition
    Public MS_VB_Information__TypeName As Mono.Cecil.MethodDefinition
    Public MS_VB_Information__VbTypeName As Mono.Cecil.MethodDefinition
    Public MS_VB_Interaction As Mono.Cecil.TypeDefinition
    Public MS_VB_Interaction__CallByName As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Versioned As Mono.Cecil.TypeDefinition
    Public MS_VB_CS_Versioned__IsNumeric As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Versioned__SystemTypeName As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Versioned__TypeName As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Versioned__VbTypeName As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Versioned__CallByName As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_StandardModuleAttribute As Mono.Cecil.TypeDefinition
    Public MS_VB_CS_Operators As Mono.Cecil.TypeDefinition
    Public MS_VB_CS_ObjectFlowControl As Mono.Cecil.TypeDefinition
    Public MS_VB_CS_ObjectFlowControl_ForLoopControl As Mono.Cecil.TypeDefinition
    Public MS_VB_CS_Utils As Mono.Cecil.TypeDefinition
    Public MS_VB_CS_OptionCompareAttribute As Mono.Cecil.TypeDefinition
    Public MS_VB_CS_OptionTextAttribute As Mono.Cecil.TypeDefinition
    Public MS_VB_CS_StaticLocalInitFlag As Mono.Cecil.TypeDefinition
    Public MS_VB_CS_StaticLocalInitFlag__State As Mono.Cecil.FieldDefinition
    Public MS_VB_CS_StaticLocalInitFlag__ctor As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_DesignerGeneratedAttribute As Mono.Cecil.TypeDefinition
    Public MS_VB_CS_IncompleteInitialization As Mono.Cecil.TypeDefinition
    Public MS_VB_CS_NewLateBinding As Mono.Cecil.TypeDefinition
    Public MS_VB_CS_LateBinding As Mono.Cecil.TypeDefinition
    Public MS_VB_CS_IncompleteInitialization__ctor As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_NewLateBinding__LateGet_Object_Type_String_Array_Array_Array_Array As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_NewLateBinding__LateSet_Object_Type_String_Array_Array_Array As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_NewLateBinding__LateIndexGet_Object_Array_Array As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_NewLateBinding__LateIndexSet_Object_Array_Array As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_NewLateBinding__LateCall_Object_Type_String_Array_Array_Array_Array_Boolean As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_ProjectData__EndApp As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_ProjectData__CreateProjectError_Int32 As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_ProjectData__ClearProjectError As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_ProjectData__SetProjectError_Exception As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_ProjectData__SetProjectError_Exception_Int32 As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Conversions__ToBoolean_Object As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Conversions__ToChar_Object As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Conversions__ToDate_Object As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Conversions__ToByte_Object As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Conversions__ToSByte_Object As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Conversions__ToShort_Object As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Conversions__ToUShort_Object As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Conversions__ToInteger_Object As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Conversions__ToUInteger_Object As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Conversions__ToLong_Object As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Conversions__ToULong_Object As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Conversions__ToSingle_Object As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Conversions__ToDouble_Object As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Conversions__ToDecimal_Object As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Conversions__ToBoolean_String As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Conversions__ToChar_String As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Conversions__ToDate_String As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Conversions__ToByte_String As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Conversions__ToSByte_String As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Conversions__ToShort_String As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Conversions__ToUShort_String As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Conversions__ToInteger_String As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Conversions__ToUInteger_String As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Conversions__ToLong_String As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Conversions__ToULong_String As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Conversions__ToSingle_String As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Conversions__ToDouble_String As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Conversions__ToDecimal_String As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Conversions__ToDecimal_Boolean As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Conversions__ToString_Decimal As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Conversions__ToString_Boolean As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Conversions__ToString_Char As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Conversions__ToString_DateTime As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Conversions__ToString_Byte As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Conversions__ToString_Int32 As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Conversions__ToString_UInt32 As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Conversions__ToString_Int64 As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Conversions__ToString_UInt64 As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Conversions__ToString_Single As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Conversions__ToString_Double As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Conversions__ToString_Object As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Conversions__ToGenericParameter_Object As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Conversions__ChangeType_Object_Type As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Conversions__ToCharArrayRankOne_String As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_LikeOperator__LikeString_String_String_CompareMethod As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_LikeOperator__LikeObject_Object_Object_CompareMethod As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_StringType__MidStmtStr_String_Int32_Int32_String As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_ObjectFlowControl__CheckForSyncLockOnValueType_Object As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_ObjectFlowControl_ForLoopControl__ForLoopInitObj_Object_Object_Object_Object_Object_Object As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_ObjectFlowControl_ForLoopControl__ForNextCheckDec_Decimal_Decimal_Decimal As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_ObjectFlowControl_ForLoopControl__ForNextCheckObj_Object_Object_Object As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_ObjectFlowControl_ForLoopControl__ForNextCheckR4_Single_Single_Single As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_ObjectFlowControl_ForLoopControl__ForNextCheckR8_Double_Double_Double As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Utils__CopyArray_Array_Array As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Operators__ConditionalCompareObjectEqual_Object_Object_Boolean As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Operators__ConditionalCompareObjectNotEqual_Object_Object_Boolean As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Operators__ConditionalCompareObjectGreater_Object_Object_Boolean As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Operators__ConditionalCompareObjectGreaterEqual_Object_Object_Boolean As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Operators__ConditionalCompareObjectLess_Object_Object_Boolean As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Operators__ConditionalCompareObjectLessEqual_Object_Object_Boolean As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Operators__CompareString_String_String_Boolean As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Operators__ConcatenateObject_Object_Object As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Operators__AddObject_Object_Object As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Operators__AndObject_Object_Object As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Operators__DivideObject_Object_Object As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Operators__ExponentObject_Object_Object As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Operators__IntDivideObject_Object_Object As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Operators__LeftShiftObject_Object_Object As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Operators__ModObject_Object_Object As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Operators__MultiplyObject_Object_Object As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Operators__NegateObject_Object As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Operators__NotObject_Object As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Operators__OrObject_Object_Object As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Operators__PlusObject_Object As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Operators__RightShiftObject_Object_Object As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Operators__SubtractObject_Object_Object As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Operators__XorObject_Object_Object As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Operators__LikeObject_Object_Object_CompareMethod As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Operators__LikeString_String_String_CompareMethod As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Operators__CompareObjectEqual_Object_Object_Boolean As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Operators__CompareObjectNotEqual_Object_Object_Boolean As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Operators__CompareObjectGreater_Object_Object_Boolean As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Operators__CompareObjectGreaterEqual_Object_Object_Boolean As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Operators__CompareObjectLess_Object_Object_Boolean As Mono.Cecil.MethodDefinition
    Public MS_VB_CS_Operators__CompareObjectLessEqual_Object_Object_Boolean As Mono.Cecil.MethodDefinition

    Protected Overrides Sub InitInternal ()
        System_Boolean = [GetType](mscorlib, "System.Boolean")
        System_Boolean_Array = GetArrayType(System_Boolean)
        System_Byte = [GetType](mscorlib, "System.Byte")
        System_Byte_Array = GetArrayType(System_Byte)
        System_Char = [GetType](mscorlib, "System.Char")
        System_Char_Array = GetArrayType(System_Char)
        System_DateTime = [GetType](mscorlib, "System.DateTime")
        System_Decimal = [GetType](mscorlib, "System.Decimal")
        System_Double = [GetType](mscorlib, "System.Double")
        System_Int32 = [GetType](mscorlib, "System.Int32")
        System_Int64 = [GetType](mscorlib, "System.Int64")
        System_Object = [GetType](mscorlib, "System.Object")
        System_Object_Array = GetArrayType(System_Object)
        System_Object_ByRef = GetByRefType(System_Object)
        System_Int16 = [GetType](mscorlib, "System.Int16")
        System_Single = [GetType](mscorlib, "System.Single")
        System_String = [GetType](mscorlib, "System.String")
        System_String_ByRef = GetByRefType(System_String)
        System_String_Array = GetArrayType(System_String)
        System_String__ctor_Array = GetConstructor(System_String, System_Char_Array)
        System_SByte = [GetType](mscorlib, "System.SByte")
        System_UInt16 = [GetType](mscorlib, "System.UInt16")
        System_UInt32 = [GetType](mscorlib, "System.UInt32")
        System_UInt64 = [GetType](mscorlib, "System.UInt64")
        System_Int32_Array = GetArrayType(System_Int32)
        System_Enum = [GetType](mscorlib, "System.Enum")
        System_ValueType = [GetType](mscorlib, "System.ValueType")
        System_Delegate = [GetType](mscorlib, "System.Delegate")
        System_MulticastDelegate = [GetType](mscorlib, "System.MulticastDelegate")
        System_AsyncCallback = [GetType](mscorlib, "System.AsyncCallback")
        System_IAsyncResult = [GetType](mscorlib, "System.IAsyncResult")
        System_Collections_IEnumerator = [GetType](mscorlib, "System.Collections.IEnumerator")
        System_Collections_IEnumerator__get_Current = GetMethod(System_Collections_IEnumerator, "get_Current")
        System_Collections_IEnumerator__MoveNext = GetMethod(System_Collections_IEnumerator, "MoveNext")
        System_Collections_IEnumerable = [GetType](mscorlib, "System.Collections.IEnumerable")
        System_Collections_IEnumerable__GetEnumerator = GetMethod(System_Collections_IEnumerable, "GetEnumerator")
        System_IDisposable = [GetType](mscorlib, "System.IDisposable")
        System_IDisposable__Dispose = GetMethod(System_IDisposable, "Dispose")
        System_RuntimeTypeHandle = [GetType](mscorlib, "System.RuntimeTypeHandle")
        System_Type = [GetType](mscorlib, "System.Type")
        System_Type_Array = GetArrayType(System_Type)
        System_Type__GetTypeFromHandle_RuntimeTypeHandle = GetMethod(System_Type, "GetTypeFromHandle", System_RuntimeTypeHandle)
        System_Void = [GetType](mscorlib, "System.Void")
        System_Exception = [GetType](mscorlib, "System.Exception")
        System_Array = [GetType](mscorlib, "System.Array")
        System_DBNull = [GetType](mscorlib, "System.DBNull")
        System_SerializableAttribute = [GetType](mscorlib, "System.SerializableAttribute")
        System_Array__SetValue = GetMethod(System_Array, "SetValue", System_Object, System_Int32_Array)
        System_Array__GetValue = GetMethod(System_Array, "GetValue", System_Int32_Array)
        System_Array__CreateInstance = GetMethod(System_Array, "CreateInstance", System_Type, System_Int32_Array)
        System_Activator = [GetType](mscorlib, "System.Activator")
        System_Activator__CreateInstance = GetMethod(System_Activator, "CreateInstance")
        System_ArgumentException = [GetType](mscorlib, "System.ArgumentException")
        System_Collections_Generic_IList1 = [GetType](mscorlib, "System.Collections.Generic.IList`1")
        System_Collections_Generic_ICollection1 = [GetType](mscorlib, "System.Collections.Generic.ICollection`1")
        System_Collections_Generic_IEnumerable1 = [GetType](mscorlib, "System.Collections.Generic.IEnumerable`1")
        System_Reflection_AssemblyVersionAttribute = [GetType](mscorlib, "System.Reflection.AssemblyVersionAttribute")
        System_Reflection_AssemblyProductAttribute = [GetType](mscorlib, "System.Reflection.AssemblyProductAttribute")
        System_Reflection_AssemblyCompanyAttribute = [GetType](mscorlib, "System.Reflection.AssemblyCompanyAttribute")
        System_Reflection_AssemblyCopyrightAttribute = [GetType](mscorlib, "System.Reflection.AssemblyCopyrightAttribute")
        System_Reflection_AssemblyTrademarkAttribute = [GetType](mscorlib, "System.Reflection.AssemblyTrademarkAttribute")
        System_Reflection_AssemblyKeyNameAttribute = [GetType](mscorlib, "System.Reflection.AssemblyKeyNameAttribute")
        System_Reflection_AssemblyKeyFileAttribute = [GetType](mscorlib, "System.Reflection.AssemblyKeyFileAttribute")
        System_Reflection_AssemblyDelaySignAttribute = [GetType](mscorlib, "System.Reflection.AssemblyDelaySignAttribute")
        System_Diagnostics_ConditionalAttribute = [GetType](mscorlib, "System.Diagnostics.ConditionalAttribute")
        System_Diagnostics_DebuggableAttribute = [GetType](mscorlib, "System.Diagnostics.DebuggableAttribute")
        System_Diagnostics_DebuggableAttribute_DebuggingModes = [GetType](System_Diagnostics_DebuggableAttribute, "DebuggingModes")
        System_Diagnostics_DebuggableAttribute__ctor_DebuggingModes = GetConstructor(System_Diagnostics_DebuggableAttribute, System_Diagnostics_DebuggableAttribute_DebuggingModes)
        System_ParamArrayAttribute = [GetType](mscorlib, "System.ParamArrayAttribute")
        System_ParamArrayAttribute__ctor = GetConstructor(System_ParamArrayAttribute)
        System_Nullable1 = [GetType](mscorlib, "System.Nullable`1")
        System_Nullable1__get_HasValue = GetMethod(System_Nullable1, "get_HasValue")
        System_Nullable1__GetValueOrDefault = GetMethod(System_Nullable1, "GetValueOrDefault")
        System_Runtime_CompilerServices_RuntimeHelpers = [GetType](mscorlib, "System.Runtime.CompilerServices.RuntimeHelpers")
        System_STAThreadAttribute = [GetType](mscorlib, "System.STAThreadAttribute")
        System_STAThreadAttribute__ctor = GetConstructor(System_STAThreadAttribute)
        System_IntPtr = [GetType](mscorlib, "System.IntPtr")
        System_Runtime_CompilerServices_DateTimeConstantAttribute = [GetType](mscorlib, "System.Runtime.CompilerServices.DateTimeConstantAttribute")
        System_Runtime_CompilerServices_DateTimeConstantAttribute__ctor_Int64 = GetConstructor(System_Runtime_CompilerServices_DateTimeConstantAttribute, System_Int64)
        System_DateTime__ctor_Int64 = GetConstructor(System_DateTime, System_Int64)
        System_Decimal__ctor_Int32 = GetConstructor(System_Decimal, System_Int32)
        System_Decimal__ctor_Int64 = GetConstructor(System_Decimal, System_Int64)
        System_Decimal__ctor_Double = GetConstructor(System_Decimal, System_Double)
        System_Decimal__ctor_Single = GetConstructor(System_Decimal, System_Single)
        System_Decimal__ctor_UInt64 = GetConstructor(System_Decimal, System_UInt64)
        System_Decimal__ctor_Int32_Int32_Int32_Boolean_Byte = GetConstructor(System_Decimal, System_Int32, System_Int32, System_Int32, System_Boolean, System_Byte)
        System_Decimal__Zero = GetField(System_Decimal, "Zero")
        System_Decimal__One = GetField(System_Decimal, "One")
        System_Decimal__MinusOne = GetField(System_Decimal, "MinusOne")
        System_Runtime_CompilerServices_DecimalConstantAttribute = [GetType](mscorlib, "System.Runtime.CompilerServices.DecimalConstantAttribute")
        System_Runtime_CompilerServices_DecimalConstantAttribute__ctor_Byte_Byte_UInt32_UInt32_UInt32 = GetConstructor(System_Runtime_CompilerServices_DecimalConstantAttribute, System_Byte, System_Byte, System_UInt32, System_UInt32, System_UInt32)
        System_Runtime_CompilerServices_DecimalConstantAttribute__ctor_Byte_Byte_Int32_Int32_Int32 = GetConstructor(System_Runtime_CompilerServices_DecimalConstantAttribute, System_Byte, System_Byte, System_Int32, System_Int32, System_Int32)
        System_Runtime_CompilerServices_AccessedThroughPropertyAttribute = [GetType](mscorlib, "System.Runtime.CompilerServices.AccessedThroughPropertyAttribute")
        System_Runtime_CompilerServices_AccessedThroughPropertyAttribute__ctor_String = GetConstructor(System_Runtime_CompilerServices_AccessedThroughPropertyAttribute, System_String)
        System_Decimal__Compare_Decimal_Decimal = GetMethod(System_Decimal, "Compare", System_Decimal, System_Decimal)
        System_DateTime__Compare_DateTime_DateTime = GetMethod(System_DateTime, "Compare", System_DateTime, System_DateTime)
        System_Decimal__Add_Decimal_Decimal = GetMethod(System_Decimal, "Add", System_Decimal, System_Decimal)
        System_Decimal__Subtract_Decimal_Decimal = GetMethod(System_Decimal, "Subtract", System_Decimal, System_Decimal)
        System_Decimal__Divide_Decimal_Decimal = GetMethod(System_Decimal, "Divide", System_Decimal, System_Decimal)
        System_Decimal__Multiply_Decimal_Decimal = GetMethod(System_Decimal, "Multiply", System_Decimal, System_Decimal)
        System_Decimal__Remainder_Decimal_Decimal = GetMethod(System_Decimal, "Remainder", System_Decimal, System_Decimal)
        System_Decimal__Negate_Decimal = GetMethod(System_Decimal, "Negate", System_Decimal)
        System_Reflection_DefaultMemberAttribute = [GetType](mscorlib, "System.Reflection.DefaultMemberAttribute")
        System_Reflection_DefaultMemberAttribute__ctor_String = GetConstructor(System_Reflection_DefaultMemberAttribute, System_String)
        System_Convert = [GetType](mscorlib, "System.Convert")
        System_Convert__ToSingle_Decimal = GetMethod(System_Convert, "ToSingle", System_Decimal)
        System_Convert__ToDouble_Decimal = GetMethod(System_Convert, "ToDouble", System_Decimal)
        System_Convert__ToBoolean_Decimal = GetMethod(System_Convert, "ToBoolean", System_Decimal)
        System_Convert__ToByte_Decimal = GetMethod(System_Convert, "ToByte", System_Decimal)
        System_Convert__ToSByte_Decimal = GetMethod(System_Convert, "ToSByte", System_Decimal)
        System_Convert__ToInt16_Decimal = GetMethod(System_Convert, "ToInt16", System_Decimal)
        System_Convert__ToUInt16_Decimal = GetMethod(System_Convert, "ToUInt16", System_Decimal)
        System_Convert__ToInt32_Decimal = GetMethod(System_Convert, "ToInt32", System_Decimal)
        System_Convert__ToUInt32_Decimal = GetMethod(System_Convert, "ToUInt32", System_Decimal)
        System_Convert__ToInt64_Decimal = GetMethod(System_Convert, "ToInt64", System_Decimal)
        System_Convert__ToUInt64_Decimal = GetMethod(System_Convert, "ToUInt64", System_Decimal)
        System_String__Concat_String_String = GetMethod(System_String, "Concat", System_String, System_String)
        System_Diagnostics_Debugger = [GetType](mscorlib, "System.Diagnostics.Debugger")
        System_Diagnostics_Debugger__Break = GetMethod(System_Diagnostics_Debugger, "Break")
        System_Reflection_Missing = [GetType](mscorlib, "System.Reflection.Missing")
        System_Reflection_Missing__Value = GetField(System_Reflection_Missing, "Value")
        System_Threading_Monitor = [GetType](mscorlib, "System.Threading.Monitor")
        System_Threading_Monitor__Enter_Object = GetMethod(System_Threading_Monitor, "Enter", System_Object)
        System_Threading_Monitor__Exit_Object = GetMethod(System_Threading_Monitor, "Exit", System_Object)
        System_Runtime_CompilerServices_RuntimeHelpers__GetObjectValue_Object = GetMethod(System_Runtime_CompilerServices_RuntimeHelpers, "GetObjectValue", System_Object)
        System_Math = [GetType](mscorlib, "System.Math")
        System_Math__Round_Double = GetMethod(System_Math, "Round", System_Double)
        System_Math__Pow_Double_Double = GetMethod(System_Math, "Pow", System_Double, System_Double)
        System_Runtime_InteropServices_DllImportAttribute = [GetType](mscorlib, "System.Runtime.InteropServices.DllImportAttribute")
        System_Runtime_InteropServices_MarshalAsAttribute = [GetType](mscorlib, "System.Runtime.InteropServices.MarshalAsAttribute")
        System_Runtime_InteropServices_StructLayoutAttribute = [GetType](mscorlib, "System.Runtime.InteropServices.StructLayoutAttribute")
        System_Runtime_InteropServices_FieldOffsetAttribute = [GetType](mscorlib, "System.Runtime.InteropServices.FieldOffsetAttribute")
        System_Runtime_InteropServices_CoClassAttribute = [GetType](mscorlib, "System.Runtime.InteropServices.CoClassAttribute")
        System_Runtime_InteropServices_OutAttribute = [GetType](mscorlib, "System.Runtime.InteropServices.OutAttribute")
        System_Security_Permissions_SecurityAttribute = [GetType](mscorlib, "System.Security.Permissions.SecurityAttribute")
        System_Windows_Forms_Form = [GetType](winforms, "System.Windows.Forms.Form")
        System_Windows_Forms_Application = [GetType](winforms, "System.Windows.Forms.Application")
        System_Windows_Forms_Application__Run = GetMethod(System_Windows_Forms_Application, "Run", System_Windows_Forms_Form)
        System_Delegate__Combine = GetMethod(System_Delegate, "Combine", System_Delegate, System_Delegate)
        System_Delegate__Remove = GetMethod(System_Delegate, "Remove", System_Delegate, System_Delegate)
    End Sub

    Public Overrides Sub InitInternalVB()
        MS_VB_CompareMethod = GetVBType("Microsoft.VisualBasic.CompareMethod")
        MS_VB_CS_Conversions = GetVBType("Microsoft.VisualBasic.CompilerServices.Conversions")
        MS_VB_CS_ProjectData = GetVBType("Microsoft.VisualBasic.CompilerServices.ProjectData")
        MS_VB_CS_LikeOperator = GetVBType("Microsoft.VisualBasic.CompilerServices.LikeOperator")
        MS_VB_Strings = GetVBType("Microsoft.VisualBasic.Strings")
        MS_VB_CS_StringType = GetVBType("Microsoft.VisualBasic.CompilerServices.StringType")
        MS_VB_MyGroupCollectionAttribute = GetVBType("Microsoft.VisualBasic.MyGroupCollectionAttribute")
        MS_VB_CallType = GetVBType("Microsoft.VisualBasic.CallType")
        MS_VB_Information = GetVBType("Microsoft.VisualBasic.Information")
        MS_VB_Interaction = GetVBType("Microsoft.VisualBasic.Interaction")
        MS_VB_CS_Versioned = GetVBType("Microsoft.VisualBasic.CompilerServices.Versioned")
        MS_VB_CS_StandardModuleAttribute = GetVBType("Microsoft.VisualBasic.CompilerServices.StandardModuleAttribute")
        MS_VB_CS_Operators = GetVBType("Microsoft.VisualBasic.CompilerServices.Operators")
        MS_VB_CS_ObjectFlowControl = GetVBType("Microsoft.VisualBasic.CompilerServices.ObjectFlowControl")
        MS_VB_CS_ObjectFlowControl_ForLoopControl = GetVBType("Microsoft.VisualBasic.CompilerServices.ObjectFlowControl+ForLoopControl")
        MS_VB_CS_Utils = GetVBType("Microsoft.VisualBasic.CompilerServices.Utils")
        MS_VB_CS_OptionCompareAttribute = GetVBType("Microsoft.VisualBasic.CompilerServices.OptionCompareAttribute")
        MS_VB_CS_OptionTextAttribute = GetVBType("Microsoft.VisualBasic.CompilerServices.OptionTextAttribute")
        MS_VB_CS_StaticLocalInitFlag = GetVBType("Microsoft.VisualBasic.CompilerServices.StaticLocalInitFlag")
        MS_VB_CS_DesignerGeneratedAttribute = GetVBType("Microsoft.VisualBasic.CompilerServices.DesignerGeneratedAttribute")
        MS_VB_CS_IncompleteInitialization = GetVBType("Microsoft.VisualBasic.CompilerServices.IncompleteInitialization")
        MS_VB_CS_NewLateBinding = GetVBType("Microsoft.VisualBasic.CompilerServices.NewLateBinding")
        MS_VB_CS_LateBinding = GetVBType("Microsoft.VisualBasic.CompilerServices.LateBinding")
    End Sub

    Public Overrides Sub InitInternalVBMembers()
        MS_VB_Information__IsNumeric = GetMethod(MS_VB_Information, "IsNumeric", System_Object)
        MS_VB_Information__SystemTypeName = GetMethod(MS_VB_Information, "SystemTypeName", System_String)
        MS_VB_Information__TypeName = GetMethod(MS_VB_Information, "TypeName", System_Object)
        MS_VB_Information__VbTypeName = GetMethod(MS_VB_Information, "VbTypeName", System_String)
        MS_VB_Interaction__CallByName = GetMethod(MS_VB_Interaction, "CallByName", System_Object, System_String, MS_VB_CallType, System_Object_Array)
        MS_VB_CS_Versioned__IsNumeric = GetMethod(MS_VB_CS_Versioned, "IsNumeric", System_Object)
        MS_VB_CS_Versioned__SystemTypeName = GetMethod(MS_VB_CS_Versioned, "SystemTypeName", System_String)
        MS_VB_CS_Versioned__TypeName = GetMethod(MS_VB_CS_Versioned, "TypeName", System_Object)
        MS_VB_CS_Versioned__VbTypeName = GetMethod(MS_VB_CS_Versioned, "VbTypeName", System_String)
        MS_VB_CS_Versioned__CallByName = GetMethod(MS_VB_CS_Versioned, "CallByName", System_Object, System_String, MS_VB_CallType, System_Object_Array)
        MS_VB_CS_StaticLocalInitFlag__State = GetField(MS_VB_CS_StaticLocalInitFlag, "State")
        MS_VB_CS_StaticLocalInitFlag__ctor = GetConstructor(MS_VB_CS_StaticLocalInitFlag)
        MS_VB_CS_IncompleteInitialization__ctor = GetConstructor(MS_VB_CS_IncompleteInitialization)
        MS_VB_CS_NewLateBinding__LateGet_Object_Type_String_Array_Array_Array_Array = GetMethod(MS_VB_CS_NewLateBinding, "LateGet", System_Object, System_Type, System_String, System_Object_Array, System_String_Array, System_Type_Array, System_Boolean_Array)
        MS_VB_CS_NewLateBinding__LateSet_Object_Type_String_Array_Array_Array = GetMethod(MS_VB_CS_NewLateBinding, "LateSet", System_Object, System_Type, System_String, System_Object_Array, System_String_Array, System_Type_Array)
        MS_VB_CS_NewLateBinding__LateIndexGet_Object_Array_Array = GetMethod(MS_VB_CS_NewLateBinding, "LateIndexGet", System_Object, System_Object_Array, System_String_Array)
        MS_VB_CS_NewLateBinding__LateIndexSet_Object_Array_Array = GetMethod(MS_VB_CS_NewLateBinding, "LateIndexSet", System_Object, System_Object_Array, System_String_Array)
        MS_VB_CS_NewLateBinding__LateCall_Object_Type_String_Array_Array_Array_Array_Boolean = GetMethod(MS_VB_CS_NewLateBinding, "LateCall", System_Object, System_Type, System_String, System_Object_Array, System_String_Array, System_Type_Array, System_Boolean_Array, System_Boolean)
        MS_VB_CS_ProjectData__EndApp = GetMethod(MS_VB_CS_ProjectData, "EndApp")
        MS_VB_CS_ProjectData__CreateProjectError_Int32 = GetMethod(MS_VB_CS_ProjectData, "CreateProjectError", System_Int32)
        MS_VB_CS_ProjectData__ClearProjectError = GetMethod(MS_VB_CS_ProjectData, "ClearProjectError")
        MS_VB_CS_ProjectData__SetProjectError_Exception = GetMethod(MS_VB_CS_ProjectData, "SetProjectError", System_Exception)
        MS_VB_CS_ProjectData__SetProjectError_Exception_Int32 = GetMethod(MS_VB_CS_ProjectData, "SetProjectError", System_Exception, System_Int32)
        MS_VB_CS_Conversions__ToBoolean_Object = GetMethod(MS_VB_CS_Conversions, "ToBoolean", System_Object)
        MS_VB_CS_Conversions__ToChar_Object = GetMethod(MS_VB_CS_Conversions, "ToChar", System_Object)
        MS_VB_CS_Conversions__ToDate_Object = GetMethod(MS_VB_CS_Conversions, "ToDate", System_Object)
        MS_VB_CS_Conversions__ToByte_Object = GetMethod(MS_VB_CS_Conversions, "ToByte", System_Object)
        MS_VB_CS_Conversions__ToSByte_Object = GetMethod(MS_VB_CS_Conversions, "ToSByte", System_Object)
        MS_VB_CS_Conversions__ToShort_Object = GetMethod(MS_VB_CS_Conversions, "ToShort", System_Object)
        MS_VB_CS_Conversions__ToUShort_Object = GetMethod(MS_VB_CS_Conversions, "ToUShort", System_Object)
        MS_VB_CS_Conversions__ToInteger_Object = GetMethod(MS_VB_CS_Conversions, "ToInteger", System_Object)
        MS_VB_CS_Conversions__ToUInteger_Object = GetMethod(MS_VB_CS_Conversions, "ToUInteger", System_Object)
        MS_VB_CS_Conversions__ToLong_Object = GetMethod(MS_VB_CS_Conversions, "ToLong", System_Object)
        MS_VB_CS_Conversions__ToULong_Object = GetMethod(MS_VB_CS_Conversions, "ToULong", System_Object)
        MS_VB_CS_Conversions__ToSingle_Object = GetMethod(MS_VB_CS_Conversions, "ToSingle", System_Object)
        MS_VB_CS_Conversions__ToDouble_Object = GetMethod(MS_VB_CS_Conversions, "ToDouble", System_Object)
        MS_VB_CS_Conversions__ToDecimal_Object = GetMethod(MS_VB_CS_Conversions, "ToDecimal", System_Object)
        MS_VB_CS_Conversions__ToBoolean_String = GetMethod(MS_VB_CS_Conversions, "ToBoolean", System_String)
        MS_VB_CS_Conversions__ToChar_String = GetMethod(MS_VB_CS_Conversions, "ToChar", System_String)
        MS_VB_CS_Conversions__ToDate_String = GetMethod(MS_VB_CS_Conversions, "ToDate", System_String)
        MS_VB_CS_Conversions__ToByte_String = GetMethod(MS_VB_CS_Conversions, "ToByte", System_String)
        MS_VB_CS_Conversions__ToSByte_String = GetMethod(MS_VB_CS_Conversions, "ToSByte", System_String)
        MS_VB_CS_Conversions__ToShort_String = GetMethod(MS_VB_CS_Conversions, "ToShort", System_String)
        MS_VB_CS_Conversions__ToUShort_String = GetMethod(MS_VB_CS_Conversions, "ToUShort", System_String)
        MS_VB_CS_Conversions__ToInteger_String = GetMethod(MS_VB_CS_Conversions, "ToInteger", System_String)
        MS_VB_CS_Conversions__ToUInteger_String = GetMethod(MS_VB_CS_Conversions, "ToUInteger", System_String)
        MS_VB_CS_Conversions__ToLong_String = GetMethod(MS_VB_CS_Conversions, "ToLong", System_String)
        MS_VB_CS_Conversions__ToULong_String = GetMethod(MS_VB_CS_Conversions, "ToULong", System_String)
        MS_VB_CS_Conversions__ToSingle_String = GetMethod(MS_VB_CS_Conversions, "ToSingle", System_String)
        MS_VB_CS_Conversions__ToDouble_String = GetMethod(MS_VB_CS_Conversions, "ToDouble", System_String)
        MS_VB_CS_Conversions__ToDecimal_String = GetMethod(MS_VB_CS_Conversions, "ToDecimal", System_String)
        MS_VB_CS_Conversions__ToDecimal_Boolean = GetMethod(MS_VB_CS_Conversions, "ToDecimal", System_Boolean)
        MS_VB_CS_Conversions__ToString_Decimal = GetMethod(MS_VB_CS_Conversions, "ToString", System_Decimal)
        MS_VB_CS_Conversions__ToString_Boolean = GetMethod(MS_VB_CS_Conversions, "ToString", System_Boolean)
        MS_VB_CS_Conversions__ToString_Char = GetMethod(MS_VB_CS_Conversions, "ToString", System_Char)
        MS_VB_CS_Conversions__ToString_DateTime = GetMethod(MS_VB_CS_Conversions, "ToString", System_DateTime)
        MS_VB_CS_Conversions__ToString_Byte = GetMethod(MS_VB_CS_Conversions, "ToString", System_Byte)
        MS_VB_CS_Conversions__ToString_Int32 = GetMethod(MS_VB_CS_Conversions, "ToString", System_Int32)
        MS_VB_CS_Conversions__ToString_UInt32 = GetMethod(MS_VB_CS_Conversions, "ToString", System_UInt32)
        MS_VB_CS_Conversions__ToString_Int64 = GetMethod(MS_VB_CS_Conversions, "ToString", System_Int64)
        MS_VB_CS_Conversions__ToString_UInt64 = GetMethod(MS_VB_CS_Conversions, "ToString", System_UInt64)
        MS_VB_CS_Conversions__ToString_Single = GetMethod(MS_VB_CS_Conversions, "ToString", System_Single)
        MS_VB_CS_Conversions__ToString_Double = GetMethod(MS_VB_CS_Conversions, "ToString", System_Double)
        MS_VB_CS_Conversions__ToString_Object = GetMethod(MS_VB_CS_Conversions, "ToString", System_Object)
        MS_VB_CS_Conversions__ToGenericParameter_Object = GetMethod(MS_VB_CS_Conversions, "ToGenericParameter", System_Object)
        MS_VB_CS_Conversions__ChangeType_Object_Type = GetMethod(MS_VB_CS_Conversions, "ChangeType", System_Object, System_Type)
        MS_VB_CS_Conversions__ToCharArrayRankOne_String = GetMethod(MS_VB_CS_Conversions, "ToCharArrayRankOne", System_String)
        MS_VB_CS_LikeOperator__LikeString_String_String_CompareMethod = GetMethod(MS_VB_CS_LikeOperator, "LikeString", System_String, System_String, MS_VB_CompareMethod)
        MS_VB_CS_LikeOperator__LikeObject_Object_Object_CompareMethod = GetMethod(MS_VB_CS_LikeOperator, "LikeObject", System_Object, System_Object, MS_VB_CompareMethod)
        MS_VB_CS_StringType__MidStmtStr_String_Int32_Int32_String = GetMethod(MS_VB_CS_StringType, "MidStmtStr", System_String_ByRef, System_Int32, System_Int32, System_String)
        MS_VB_CS_ObjectFlowControl__CheckForSyncLockOnValueType_Object = GetMethod(MS_VB_CS_ObjectFlowControl, "CheckForSyncLockOnValueType", System_Object)
        MS_VB_CS_ObjectFlowControl_ForLoopControl__ForLoopInitObj_Object_Object_Object_Object_Object_Object = GetMethod(MS_VB_CS_ObjectFlowControl_ForLoopControl, "ForLoopInitObj", System_Object, System_Object, System_Object, System_Object, System_Object_ByRef, System_Object_ByRef)
        MS_VB_CS_ObjectFlowControl_ForLoopControl__ForNextCheckDec_Decimal_Decimal_Decimal = GetMethod(MS_VB_CS_ObjectFlowControl_ForLoopControl, "ForNextCheckDec", System_Decimal, System_Decimal, System_Decimal)
        MS_VB_CS_ObjectFlowControl_ForLoopControl__ForNextCheckObj_Object_Object_Object = GetMethod(MS_VB_CS_ObjectFlowControl_ForLoopControl, "ForNextCheckObj", System_Object, System_Object, System_Object_ByRef)
        MS_VB_CS_ObjectFlowControl_ForLoopControl__ForNextCheckR4_Single_Single_Single = GetMethod(MS_VB_CS_ObjectFlowControl_ForLoopControl, "ForNextCheckR4", System_Single, System_Single, System_Single)
        MS_VB_CS_ObjectFlowControl_ForLoopControl__ForNextCheckR8_Double_Double_Double = GetMethod(MS_VB_CS_ObjectFlowControl_ForLoopControl, "ForNextCheckR8", System_Double, System_Double, System_Double)
        MS_VB_CS_Utils__CopyArray_Array_Array = GetMethod(MS_VB_CS_Utils, "CopyArray", System_Array, System_Array)
        MS_VB_CS_Operators__ConditionalCompareObjectEqual_Object_Object_Boolean = GetMethod(MS_VB_CS_Operators, "ConditionalCompareObjectEqual", System_Object, System_Object, System_Boolean)
        MS_VB_CS_Operators__ConditionalCompareObjectNotEqual_Object_Object_Boolean = GetMethod(MS_VB_CS_Operators, "ConditionalCompareObjectNotEqual", System_Object, System_Object, System_Boolean)
        MS_VB_CS_Operators__ConditionalCompareObjectGreater_Object_Object_Boolean = GetMethod(MS_VB_CS_Operators, "ConditionalCompareObjectGreater", System_Object, System_Object, System_Boolean)
        MS_VB_CS_Operators__ConditionalCompareObjectGreaterEqual_Object_Object_Boolean = GetMethod(MS_VB_CS_Operators, "ConditionalCompareObjectGreaterEqual", System_Object, System_Object, System_Boolean)
        MS_VB_CS_Operators__ConditionalCompareObjectLess_Object_Object_Boolean = GetMethod(MS_VB_CS_Operators, "ConditionalCompareObjectLess", System_Object, System_Object, System_Boolean)
        MS_VB_CS_Operators__ConditionalCompareObjectLessEqual_Object_Object_Boolean = GetMethod(MS_VB_CS_Operators, "ConditionalCompareObjectLessEqual", System_Object, System_Object, System_Boolean)
        MS_VB_CS_Operators__CompareString_String_String_Boolean = GetMethod(MS_VB_CS_Operators, "CompareString", System_String, System_String, System_Boolean)
        MS_VB_CS_Operators__ConcatenateObject_Object_Object = GetMethod(MS_VB_CS_Operators, "ConcatenateObject", System_Object, System_Object)
        MS_VB_CS_Operators__AddObject_Object_Object = GetMethod(MS_VB_CS_Operators, "AddObject", System_Object, System_Object)
        MS_VB_CS_Operators__AndObject_Object_Object = GetMethod(MS_VB_CS_Operators, "AndObject", System_Object, System_Object)
        MS_VB_CS_Operators__DivideObject_Object_Object = GetMethod(MS_VB_CS_Operators, "DivideObject", System_Object, System_Object)
        MS_VB_CS_Operators__ExponentObject_Object_Object = GetMethod(MS_VB_CS_Operators, "ExponentObject", System_Object, System_Object)
        MS_VB_CS_Operators__IntDivideObject_Object_Object = GetMethod(MS_VB_CS_Operators, "IntDivideObject", System_Object, System_Object)
        MS_VB_CS_Operators__LeftShiftObject_Object_Object = GetMethod(MS_VB_CS_Operators, "LeftShiftObject", System_Object, System_Object)
        MS_VB_CS_Operators__ModObject_Object_Object = GetMethod(MS_VB_CS_Operators, "ModObject", System_Object, System_Object)
        MS_VB_CS_Operators__MultiplyObject_Object_Object = GetMethod(MS_VB_CS_Operators, "MultiplyObject", System_Object, System_Object)
        MS_VB_CS_Operators__NegateObject_Object = GetMethod(MS_VB_CS_Operators, "NegateObject", System_Object)
        MS_VB_CS_Operators__NotObject_Object = GetMethod(MS_VB_CS_Operators, "NotObject", System_Object)
        MS_VB_CS_Operators__OrObject_Object_Object = GetMethod(MS_VB_CS_Operators, "OrObject", System_Object, System_Object)
        MS_VB_CS_Operators__PlusObject_Object = GetMethod(MS_VB_CS_Operators, "PlusObject", System_Object)
        MS_VB_CS_Operators__RightShiftObject_Object_Object = GetMethod(MS_VB_CS_Operators, "RightShiftObject", System_Object, System_Object)
        MS_VB_CS_Operators__SubtractObject_Object_Object = GetMethod(MS_VB_CS_Operators, "SubtractObject", System_Object, System_Object)
        MS_VB_CS_Operators__XorObject_Object_Object = GetMethod(MS_VB_CS_Operators, "XorObject", System_Object, System_Object)
        MS_VB_CS_Operators__LikeObject_Object_Object_CompareMethod = GetMethod(MS_VB_CS_Operators, "LikeObject", System_Object, System_Object, MS_VB_CompareMethod)
        MS_VB_CS_Operators__LikeString_String_String_CompareMethod = GetMethod(MS_VB_CS_Operators, "LikeString", System_String, System_String, MS_VB_CompareMethod)
        MS_VB_CS_Operators__CompareObjectEqual_Object_Object_Boolean = GetMethod(MS_VB_CS_Operators, "CompareObjectEqual", System_Object, System_Object, System_Boolean)
        MS_VB_CS_Operators__CompareObjectNotEqual_Object_Object_Boolean = GetMethod(MS_VB_CS_Operators, "CompareObjectNotEqual", System_Object, System_Object, System_Boolean)
        MS_VB_CS_Operators__CompareObjectGreater_Object_Object_Boolean = GetMethod(MS_VB_CS_Operators, "CompareObjectGreater", System_Object, System_Object, System_Boolean)
        MS_VB_CS_Operators__CompareObjectGreaterEqual_Object_Object_Boolean = GetMethod(MS_VB_CS_Operators, "CompareObjectGreaterEqual", System_Object, System_Object, System_Boolean)
        MS_VB_CS_Operators__CompareObjectLess_Object_Object_Boolean = GetMethod(MS_VB_CS_Operators, "CompareObjectLess", System_Object, System_Object, System_Boolean)
        MS_VB_CS_Operators__CompareObjectLessEqual_Object_Object_Boolean = GetMethod(MS_VB_CS_Operators, "CompareObjectLessEqual", System_Object, System_Object, System_Boolean)
    End Sub

End Class

