' 
' Visual Basic.Net Compiler
' Copyright (C) 2004 - 2007 Rolf Bjarne Kvinge, RKvinge@novell.com
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

Public Class TypeCache
    Private m_Compiler As Compiler

    Public [Nothing] As System.Type
    Public DelegateUnresolvedType As System.Type

    Public [Boolean] As System.Type
    Public [Byte] As System.Type
    Public Byte_Array As System.Type
    Public [Char] As System.Type
    Public [Date] As System.Type
    Public [Decimal] As System.Type
    Public [Double] As System.Type
    Public [Integer] As System.Type
    Public [Long] As System.Type
    Public [Object] As System.Type
    Public Object_Array As System.Type
    Public [Short] As System.Type
    Public [Single] As System.Type
    Public [String] As System.Type
    Public [String_ByRef] As System.Type
    Public [SByte] As System.Type
    Public [UShort] As System.Type
    Public [UInteger] As System.Type
    Public [ULong] As System.Type
    Public [Integer_Array] As System.Type
    Public [String_Array] As System.Type
    Public [Enum] As System.Type
    Public [Structure] As System.Type
    Public [Delegate] As System.Type
    Public [MulticastDelegate] As System.Type
    Public [AsyncCallback] As System.Type
    Public [IAsyncResult] As System.Type
    Public [IEnumerator] As System.Type
    Public [IEnumerator_get_Current] As System.Reflection.MethodInfo
    Public [IEnumerator_MoveNext] As System.Reflection.MethodInfo
    Public [IEnumerable] As System.Type
    Public [IEnumerable_GetEnumerator] As System.Reflection.MethodInfo
    Public [IDisposable] As System.Type
    Public [IDisposable_Dispose] As System.Reflection.MethodInfo
    Public [ValueType] As System.Type
    Public [System_RuntimeTypeHandle] As System.Type
    Public [Type] As System.Type
    Public [Type__GetTypeFromHandle_RuntimeTypeHandle] As System.Reflection.MethodInfo
    Public [Void] As System.Type
    Public [Exception] As System.Type
    Public [Array] As System.Type
    Public [Array_SetValue] As System.Reflection.MethodInfo
    Public [Array_GetValue] As System.Reflection.MethodInfo
    Public [Array_CreateInstance] As System.Reflection.MethodInfo
    Public System_ArgumentException As System.Type
    Public [System_Collections_Generic_IList1] As System.Type
    Public [System_Collections_Generic_ICollection1] As System.Type
    Public [System_Collections_Generic_IEnumerable1] As System.Type
    Public [System_Reflection_AssemblyVersionAttribute] As System.Type
    Public [System_Reflection_AssemblyProductAttribute] As System.Type
    Public [System_Reflection_AssemblyCompanyAttribute] As System.Type
    Public [System_Reflection_AssemblyCopyrightAttribute] As System.Type
    Public [System_Reflection_AssemblyTrademarkAttribute] As System.Type
    Public [System_Reflection_AssemblyKeyFileAttribute] As System.Type
    Public [System_Reflection_AssemblyKeyNameAttribute] As System.Type
    Public System_Reflection_AssemblyDelaySignAttribute As System.Type
    Public [System_Diagnostics_DebuggableAttribute] As System.Type
    Public [System_Diagnostics_DebuggableAttribute_DebuggingModes] As System.Type
    Public [System_Diagnostics_DebuggableAttribute__ctor_DebuggingModes] As System.Reflection.ConstructorInfo
    Public [System_ParamArrayAttribute] As System.Type
    Public [System_ParamArrayAttribute__ctor] As System.Reflection.ConstructorInfo
    Public [System_Nullable] As System.Type
    Public [RuntimeHelpers] As System.Type
    Public [STAThreadAttribute] As System.Type
    Public [STAThreadAttribute_Ctor] As System.Reflection.ConstructorInfo
    Public [IntPtr] As System.Type
    Public [DateTimeConstantAttribute] As System.Type
    Public [DateConstructor_Int64] As System.Reflection.ConstructorInfo
    Public [DecimalConstructor_Int32] As System.Reflection.ConstructorInfo
    Public [DecimalConstructor_Int64] As System.Reflection.ConstructorInfo
    Public [DecimalConstructor_Double] As System.Reflection.ConstructorInfo
    Public [DecimalConstructor_Single] As System.Reflection.ConstructorInfo
    Public [DecimalConstructor_UInt64] As System.Reflection.ConstructorInfo
    Public [DecimalConstructor_Int32_Int32_Int32_Boolean_Byte] As System.Reflection.ConstructorInfo
    Public [Decimal_Zero] As System.Reflection.FieldInfo
    Public [Decimal_One] As System.Reflection.FieldInfo
    Public [Decimal_MinusOne] As System.Reflection.FieldInfo
    Public [DecimalConstantAttribute] As System.Type
    Public [DecimalConstantAttributeConstructor_Byte_Byte_UInt32_UInt32_UInt32] As System.Reflection.ConstructorInfo
    Public [DecimalConstantAttributeConstructor_Byte_Byte_Int32_Int32_Int32] As System.Reflection.ConstructorInfo
    Public [Decimal_Compare__Decimal_Decimal] As System.Reflection.MethodInfo
    Public [Date_Compare__Date_Date] As System.Reflection.MethodInfo
    Public [Decimal_Add__Decimal_Decimal] As System.Reflection.MethodInfo
    Public [Decimal_Subtract__Decimal_Decimal] As System.Reflection.MethodInfo
    Public [Decimal_Divide__Decimal_Decimal] As System.Reflection.MethodInfo
    Public [Decimal_Multiply__Decimal_Decimal] As System.Reflection.MethodInfo
    Public [Decimal_Remainder__Decimal_Decimal] As System.Reflection.MethodInfo
    Public [Decimal_Negate__Decimal] As System.Reflection.MethodInfo
    Public [ParamArrayAttribute] As System.Type
    Public [ParamArrayAttributeConstructor] As System.Reflection.ConstructorInfo
    Public [DefaultMemberAttribute] As System.Type
    Public [DefaultMemberAttributeConstructor] As System.Reflection.ConstructorInfo
    Public System_Convert As Type
    Public [System_Convert_ToSingle__Decimal] As System.Reflection.MethodInfo
    Public [System_Convert_ToDouble__Decimal] As System.Reflection.MethodInfo
    Public [System_Convert_ToBoolean__Decimal] As System.Reflection.MethodInfo
    Public [System_Convert_ToByte__Decimal] As System.Reflection.MethodInfo
    Public [System_Convert_ToSByte__Decimal] As System.Reflection.MethodInfo
    Public [System_Convert_ToInt16__Decimal] As System.Reflection.MethodInfo
    Public [System_Convert_ToUInt16__Decimal] As System.Reflection.MethodInfo
    Public [System_Convert_ToInt32__Decimal] As System.Reflection.MethodInfo
    Public [System_Convert_ToUInt32__Decimal] As System.Reflection.MethodInfo
    Public [System_Convert_ToInt64__Decimal] As System.Reflection.MethodInfo
    Public [System_Convert_ToUInt64__Decimal] As System.Reflection.MethodInfo
    Public [String_Concat__String_String] As System.Reflection.MethodInfo
    Public System_Diagnostics_ConditionalAttribute As Type
    Public [System_Diagnostics_Debugger_Break] As System.Reflection.MethodInfo
    Public [System_Reflection_Missing_Value] As System.Reflection.FieldInfo
    Public [System_Threading_Monitor] As System.Type
    Public [System_Threading_Monitor_Enter__Object] As System.Reflection.MethodInfo
    Public [System_Threading_Monitor_Exit__Object] As System.Reflection.MethodInfo
    Public [System_Runtime_CompilerServices_RuntimeHelpers__GetObjectValue_Object] As System.Reflection.MethodInfo
    Public [System_Math] As System.Type
    Public [System_Math_Round__Double] As System.Reflection.MethodInfo
    Public [System_Math_Pow__Double_Double] As System.Reflection.MethodInfo
    Public [System_Runtime_InteropServices_DllImportAttribute] As System.Type
    Public System_Windows_Forms_Form As System.Type
    Public System_Windows_Forms_Application As System.Type
    Public System_Windows_Forms_Application__Run As MethodInfo
    Public [StandardModuleAttribute] As System.Type
    Public [MS_VB_CompareMethod] As System.Type
    Public [MS_VB_CS_Conversions] As System.Type
    Public [MS_VB_CS_ProjectData] As System.Type
    Public [MS_VB_CS_LikeOperator] As System.Type
    Public [MS_VB_Strings] As System.Type
    Public MS_VB_MyGroupCollectionAttribute As System.Type
    Public MS_VB_CallType As System.Type
    Public MS_VB_Information As System.Type
    Public MS_VB_Information_IsNumeric As System.Reflection.MethodInfo
    Public MS_VB_Information_SystemTypeName As System.Reflection.MethodInfo
    Public MS_VB_Information_TypeName As System.Reflection.MethodInfo
    Public MS_VB_Information_VbTypeName As System.Reflection.MethodInfo
    Public MS_VB_Interaction As System.Type
    Public MS_VB_Interaction_CallByName As System.Reflection.MethodInfo
    Public MS_VB_CS_Versioned As System.Type
    Public MS_VB_CS_Versioned_CallByName As System.Reflection.MethodInfo
    Public MS_VB_CS_Versioned_IsNumeric As System.Reflection.MethodInfo
    Public MS_VB_CS_Versioned_SystemTypeName As System.Reflection.MethodInfo
    Public MS_VB_CS_Versioned_TypeName As System.Reflection.MethodInfo
    Public MS_VB_CS_Versioned_VbTypeName As System.Reflection.MethodInfo
    Public [MS_VB_CS_StringType] As System.Type
    Public [MS_VB_CS_StandardModuleAttribute] As System.Type
    Public [MS_VB_CS_Operators] As System.Type
    Public [MS_VB_CS_OFC] As System.Type
    Public [MS_VB_CS_Utils] As System.Type
    Public [MS_VB_CS_OptionCompareAttribute] As System.Type
    Public [MS_VB_CS_OptionTextAttribute] As System.Type
    Public [MS_VB_CS_StaticLocalInitFlag] As System.Type
    Public [MS_VB_CS_StaticLocalInitFlag_State] As System.Reflection.FieldInfo
    Public [MS_VB_CS_StaticLocalInitFlag_Ctor] As System.Reflection.ConstructorInfo
    Public [MS_VB_CS_IncompleteInitializationException] As System.Type
    Public [MS_VB_CS_IncompleteInitializationException__ctor] As System.Reflection.ConstructorInfo
    Public [MS_VB_CS_PD_EndApp] As System.Reflection.MethodInfo
    Public [MS_VB_CS_PD_CreateProjectError__Integer] As System.Reflection.MethodInfo
    Public [MS_VB_CS_PD_ClearProjectError] As System.Reflection.MethodInfo
    Public [MS_VB_CS_PD_SetProjectError__Exception] As System.Reflection.MethodInfo
    Public [MS_VB_CS_PD_SetProjectError__Exception_Integer] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Conversions_ToBoolean__Object] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Conversions_ToChar__Object] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Conversions_ToDate__Object] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Conversions_ToByte__Object] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Conversions_ToSByte__Object] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Conversions_ToShort__Object] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Conversions_ToUShort__Object] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Conversions_ToInteger__Object] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Conversions_ToUInteger__Object] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Conversions_ToLong__Object] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Conversions_ToULong__Object] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Conversions_ToSingle__Object] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Conversions_ToDouble__Object] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Conversions_ToDecimal__Object] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Conversions_ToBoolean__String] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Conversions_ToChar__String] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Conversions_ToDate__String] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Conversions_ToByte__String] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Conversions_ToSByte__String] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Conversions_ToShort__String] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Conversions_ToUShort__String] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Conversions_ToInteger__String] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Conversions_ToUInteger__String] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Conversions_ToLong__String] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Conversions_ToULong__String] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Conversions_ToSingle__String] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Conversions_ToDouble__String] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Conversions_ToDecimal__String] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Conversions_ToDecimal__Boolean] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Conversions_ToString__Decimal] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Conversions_ToString__Boolean] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Conversions_ToString__Char] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Conversions_ToString__Date] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Conversions_ToString__Byte] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Conversions_ToString__Integer] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Conversions_ToString__UInteger] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Conversions_ToString__Long] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Conversions_ToString__ULong] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Conversions_ToString__Single] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Conversions_ToString__Double] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Conversions_ToString__Object] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Conversions_ToGenericParameter_T__Object] As System.Reflection.MethodInfo
    Public [MS_VB_CS_LikeOperator_LikeString__String_String_CompareMethod] As System.Reflection.MethodInfo
    Public [MS_VB_CS_LikeOperator_LikeObject__Object_Object_CompareMethod] As System.Reflection.MethodInfo
    Public [MS_VB_CS_StringType_MidStmtStr__String_Integer_Integer_String] As System.Reflection.MethodInfo
    Public [MS_VB_CS_OFC_CheckForSyncLockOnValueType__Object] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Utils__CopyArray_Array_Array] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Operators_ConditionalCompareObjectEqual__Object_Object_Bool] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Operators_ConditionalCompareObjectNotEqual__Object_Object_Bool] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Operators_ConditionalCompareObjectGreater__Object_Object_Bool] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Operators_ConditionalCompareObjectGreaterEqual__Object_Object_Bool] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Operators_ConditionalCompareObjectLess__Object_Object_Bool] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Operators_ConditionalCompareObjectLessEqual__Object_Object_Bool] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Operators_CompareString__String_String_Bool] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Operators_ConcatenateObject__Object_Object] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Operators_AddObject__Object_Object] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Operators_AndObject__Object_Object] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Operators_DivideObject__Object_Object] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Operators_ExponentObject__Object_Object] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Operators_IntDivideObject__Object_Object] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Operators_LeftShiftObject__Object_Object] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Operators_ModObject__Object_Object] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Operators_MultiplyObject__Object_Object] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Operators_NegateObject__Object] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Operators_NotObject__Object] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Operators_OrObject__Object_Object] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Operators_PlusObject__Object] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Operators_RightShiftObject__Object_Object] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Operators_SubtractObject__Object_Object] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Operators_XorObject__Object_Object] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Operators_LikeObject__Object_Object] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Operators_LikeString__String_String] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Operators_CompareObjectEqual__Object_Object_Bool] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Operators_CompareObjectNotEqual__Object_Object_Bool] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Operators_CompareObjectGreater__Object_Object_Bool] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Operators_CompareObjectGreaterEqual__Object_Object_Bool] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Operators_CompareObjectLess__Object_Object_Bool] As System.Reflection.MethodInfo
    Public [MS_VB_CS_Operators_CompareObjectLessEqual__Object_Object_Bool] As System.Reflection.MethodInfo
    Public [Delegate_Combine] As System.Reflection.MethodInfo
    Public [Delegate_Remove] As System.Reflection.MethodInfo

    Public vbruntime As System.Reflection.Assembly
    Public mscorlib As System.Reflection.Assembly
    Public winforms As System.Reflection.Assembly

    'Stupid MS type system with some optimizations requires this
    Public System_MonoType As Type
    Public System_RuntimeType As Type
    Public System_Reflection_Emit_TypeBuilder As Type
    Public System_Reflection_Emit_TypeBuilderInstantiation As Type
    Public System_Reflection_Emit_SymbolType As Type

    Sub New(ByVal Compiler As Compiler)
        m_Compiler = Compiler
    End Sub

    ReadOnly Property Compiler() As Compiler
        Get
            Return m_Compiler
        End Get
    End Property

    Private Sub Init_corlib()
        Me.Boolean = Me.GetType(mscorlib, "System.Boolean")
        Me.Byte = Me.GetType(mscorlib, "System.Byte")
        Me.Byte_Array = Me.Byte.MakeArrayType()
        Me.Char = Me.GetType(mscorlib, "System.Char")
        Me.Date = Me.GetType(mscorlib, "System.DateTime")
        Me.Decimal = Me.GetType(mscorlib, "System.Decimal")
        Me.Double = Me.GetType(mscorlib, "System.Double")
        Me.Integer = Me.GetType(mscorlib, "System.Int32")
        Me.Long = Me.GetType(mscorlib, "System.Int64")
        Me.Object = Me.GetType(mscorlib, "System.Object")
        Me.Object_Array = Me.Object.MakeArrayType
        Me.Short = Me.GetType(mscorlib, "System.Int16")
        Me.Single = Me.GetType(mscorlib, "System.Single")
        Me.String = Me.GetType(mscorlib, "System.String")
        Me.String_ByRef = [String].MakeByRefType
        Me.SByte = Me.GetType(mscorlib, "System.SByte")
        Me.UShort = Me.GetType(mscorlib, "System.UInt16")
        Me.UInteger = Me.GetType(mscorlib, "System.UInt32")
        Me.ULong = Me.GetType(mscorlib, "System.UInt64")
        Me.Integer_Array = [Integer].MakeArrayType
        Me.String_Array = [String].MakeArrayType
        Me.Enum = Me.GetType(mscorlib, "System.Enum")
        Me.Structure = Me.GetType(mscorlib, "System.ValueType")
        Me.Delegate = Me.GetType(mscorlib, "System.Delegate")
        Me.MulticastDelegate = Me.GetType(mscorlib, "System.MulticastDelegate")
        Me.AsyncCallback = Me.GetType(mscorlib, "System.AsyncCallback")
        Me.IAsyncResult = Me.GetType(mscorlib, "System.IAsyncResult")
        Me.IEnumerator = Me.GetType(mscorlib, "System.Collections.IEnumerator")
        Me.IEnumerator_get_Current = GetProperty(IEnumerator, "Current").GetGetMethod(False)
        Me.IEnumerator_MoveNext = GetMethod(IEnumerator, "MoveNext")
        Me.IEnumerable = Me.GetType(mscorlib, "System.Collections.IEnumerable")
        Me.IEnumerable_GetEnumerator = GetMethod(IEnumerable, "GetEnumerator")
        Me.IDisposable = Me.GetType(mscorlib, "System.IDisposable")
        Me.IDisposable_Dispose = GetMethod(IDisposable, "Dispose")
        Me.ValueType = Me.GetType(mscorlib, "System.ValueType")
        Me.System_RuntimeTypeHandle = Me.GetType(mscorlib, "System.RuntimeTypeHandle")
        Me.Type = Me.GetType(mscorlib, "System.Type")
        Me.Type__GetTypeFromHandle_RuntimeTypeHandle = GetMethod(Type, "GetTypeFromHandle", System_RuntimeTypeHandle)
        Me.Void = Me.GetType(mscorlib, "System.Void")
        Me.Exception = Me.GetType(mscorlib, "System.Exception")
        Me.Array = Me.GetType(mscorlib, "System.Array")
        Me.Array_SetValue = GetMethod(Array, "SetValue", Me.Object, Me.Integer_Array)
        Me.Array_GetValue = GetMethod(Array, "GetValue", Me.Integer_Array)
        Me.Array_CreateInstance = GetMethod(Array, "CreateInstance", Me.Type, Me.Integer_Array)
        Me.System_ArgumentException = Me.GetType(mscorlib, "System.ArgumentException")
        Me.System_Collections_Generic_IList1 = Me.GetType(mscorlib, "System.Collections.Generic.IList`1")
        Me.System_Collections_Generic_ICollection1 = Me.GetType(mscorlib, "System.Collections.Generic.ICollection`1")
        Me.System_Collections_Generic_IEnumerable1 = Me.GetType(mscorlib, "System.Collections.Generic.IEnumerable`1")
        Me.System_Reflection_AssemblyVersionAttribute = Me.GetType(mscorlib, "System.Reflection.AssemblyVersionAttribute")
        Me.System_Reflection_AssemblyProductAttribute = Me.GetType(mscorlib, "System.Reflection.AssemblyProductAttribute")
        Me.System_Reflection_AssemblyCompanyAttribute = Me.GetType(mscorlib, "System.Reflection.AssemblyCompanyAttribute")
        Me.System_Reflection_AssemblyCopyrightAttribute = Me.GetType(mscorlib, "System.Reflection.AssemblyCopyrightAttribute")
        Me.System_Reflection_AssemblyTrademarkAttribute = Me.GetType(mscorlib, "System.Reflection.AssemblyTrademarkAttribute")
        Me.System_Reflection_AssemblyKeyNameAttribute = Me.GetType(mscorlib, "System.Reflection.AssemblyKeyNameAttribute")
        Me.System_Reflection_AssemblyKeyFileAttribute = Me.GetType(mscorlib, "System.Reflection.AssemblyKeyFileAttribute")
        Me.System_Reflection_AssemblyDelaySignAttribute = Me.GetType(mscorlib, "System.Reflection.AssemblyDelaySignAttribute")
        Me.System_Diagnostics_ConditionalAttribute = Me.GetType(mscorlib, "System.Diagnostics.ConditionalAttribute")
        Me.System_Diagnostics_DebuggableAttribute = Me.GetType(mscorlib, "System.Diagnostics.DebuggableAttribute")
        Me.System_Diagnostics_DebuggableAttribute_DebuggingModes = Me.GetType(mscorlib, "System.Diagnostics.DebuggableAttribute+DebuggingModes")
        Me.System_Diagnostics_DebuggableAttribute__ctor_DebuggingModes = GetConstructor(System_Diagnostics_DebuggableAttribute, System_Diagnostics_DebuggableAttribute_DebuggingModes)
        Me.System_ParamArrayAttribute = Me.GetType(mscorlib, "System.ParamArrayAttribute")
        Me.System_ParamArrayAttribute__ctor = GetConstructor(System_ParamArrayAttribute)
        Me.System_Nullable = Me.GetType(mscorlib, "System.Nullable`1")
        Me.RuntimeHelpers = Me.GetType(mscorlib, "System.Runtime.CompilerServices.RuntimeHelpers")
        Me.STAThreadAttribute = Me.GetType(mscorlib, "System.STAThreadAttribute")
        Me.STAThreadAttribute_Ctor = GetConstructor(STAThreadAttribute)
        Me.IntPtr = Me.GetType(mscorlib, "System.IntPtr")
        Me.DateTimeConstantAttribute = Me.GetType(mscorlib, "System.Runtime.CompilerServices.DateTimeConstantAttribute")
        Me.DateConstructor_Int64 = GetConstructor(Me.Date, Me.Long)
        Me.DecimalConstructor_Int32 = GetConstructor(Me.Decimal, Me.Integer)
        Me.DecimalConstructor_Int64 = GetConstructor(Me.Decimal, Me.Long)
        Me.DecimalConstructor_Double = GetConstructor(Me.Decimal, Me.Double)
        Me.DecimalConstructor_Single = GetConstructor(Me.Decimal, Me.Single)
        Me.DecimalConstructor_UInt64 = GetConstructor(Me.Decimal, Me.ULong)
        Me.DecimalConstructor_Int32_Int32_Int32_Boolean_Byte = GetConstructor(Me.Decimal, Me.Integer, Me.Integer, Me.Integer, Me.Boolean, Me.Byte)
        Me.Decimal_Zero = GetField(Me.Decimal, "Zero")
        Me.Decimal_One = GetField(Me.Decimal, "One")
        Me.Decimal_MinusOne = GetField(Me.Decimal, "MinusOne")
        Me.DecimalConstantAttribute = Me.GetType(mscorlib, "System.Runtime.CompilerServices.DecimalConstantAttribute")
        Me.DecimalConstantAttributeConstructor_Byte_Byte_UInt32_UInt32_UInt32 = GetConstructor(Me.DecimalConstantAttribute, Me.Byte, Me.Byte, Me.UInteger, Me.UInteger, Me.UInteger)
        Me.DecimalConstantAttributeConstructor_Byte_Byte_Int32_Int32_Int32 = GetConstructor(Me.DecimalConstantAttribute, Me.Byte, Me.Byte, Me.Integer, Me.Integer, Me.Integer)
        Me.Decimal_Compare__Decimal_Decimal = GetMethod(Me.Decimal, "Compare", Me.Decimal, Me.Decimal)
        Me.Date_Compare__Date_Date = GetMethod(Me.Date, "Compare", Me.Date, Me.Date)
        Me.Decimal_Add__Decimal_Decimal = GetMethod(Me.Decimal, "Add", Me.Decimal, Me.Decimal)
        Me.Decimal_Subtract__Decimal_Decimal = GetMethod(Me.Decimal, "Subtract", Me.Decimal, Me.Decimal)
        Me.Decimal_Divide__Decimal_Decimal = GetMethod(Me.Decimal, "Divide", Me.Decimal, Me.Decimal)
        Me.Decimal_Multiply__Decimal_Decimal = GetMethod(Me.Decimal, "Multiply", Me.Decimal, Me.Decimal)
        Me.Decimal_Remainder__Decimal_Decimal = GetMethod(Me.Decimal, "Remainder", Me.Decimal, Me.Decimal)
        Me.Decimal_Negate__Decimal = GetMethod(Me.Decimal, "Negate", Me.Decimal)
        Me.ParamArrayAttribute = Me.GetType(mscorlib, "System.ParamArrayAttribute")
        Me.ParamArrayAttributeConstructor = GetConstructor(ParamArrayAttribute)
        Me.DefaultMemberAttribute = Me.GetType(mscorlib, "System.Reflection.DefaultMemberAttribute")
        Me.DefaultMemberAttributeConstructor = GetConstructor(DefaultMemberAttribute, Me.String)
        Me.System_Convert = Me.GetType(mscorlib, "System.Convert")
        Me.System_Convert_ToSingle__Decimal = GetMethod(System_Convert, "ToSingle", Me.Decimal)
        Me.System_Convert_ToDouble__Decimal = GetMethod(System_Convert, "ToDouble", Me.Decimal)
        Me.System_Convert_ToBoolean__Decimal = GetMethod(System_Convert, "ToBoolean", Me.Decimal)
        Me.System_Convert_ToByte__Decimal = GetMethod(System_Convert, "ToByte", Me.Decimal)
        Me.System_Convert_ToSByte__Decimal = GetMethod(System_Convert, "ToSByte", Me.Decimal)
        Me.System_Convert_ToInt16__Decimal = GetMethod(System_Convert, "ToInt16", Me.Decimal)
        Me.System_Convert_ToUInt16__Decimal = GetMethod(System_Convert, "ToUInt16", Me.Decimal)
        Me.System_Convert_ToInt32__Decimal = GetMethod(System_Convert, "ToInt32", Me.Decimal)
        Me.System_Convert_ToUInt32__Decimal = GetMethod(System_Convert, "ToUInt32", Me.Decimal)
        Me.System_Convert_ToInt64__Decimal = GetMethod(System_Convert, "ToInt64", Me.Decimal)
        Me.System_Convert_ToUInt64__Decimal = GetMethod(System_Convert, "ToUInt64", Me.Decimal)
        Me.String_Concat__String_String = GetMethod(Me.String, "Concat", Me.String, Me.String)
        Me.System_Diagnostics_Debugger_Break = Me.GetType(mscorlib, "System.Diagnostics.Debugger").GetMethod("Break", System.Type.EmptyTypes)
        Me.System_Reflection_Missing_Value = Me.GetType(mscorlib, "System.Reflection.Missing").GetField("Value")
        Me.System_Threading_Monitor = Me.GetType(mscorlib, "System.Threading.Monitor")
        Me.System_Threading_Monitor_Enter__Object = GetMethod(System_Threading_Monitor, "Enter", Me.Object)
        Me.System_Threading_Monitor_Exit__Object = GetMethod(System_Threading_Monitor, "Exit", Me.Object)
        Me.System_Runtime_CompilerServices_RuntimeHelpers__GetObjectValue_Object = GetMethod(RuntimeHelpers, "GetObjectValue", Me.Object)
        Me.System_Math = Me.GetType(mscorlib, "System.Math")
        Me.System_Math_Round__Double = GetMethod(System_Math, "Round", Me.Double)
        Me.System_Math_Pow__Double_Double = GetMethod(System_Math, "Pow", Me.Double, Me.Double)
        Me.System_Runtime_InteropServices_DllImportAttribute = Me.GetType(mscorlib, "System.Runtime.InteropServices.DllImportAttribute")

        Me.System_Windows_Forms_Form = Me.GetType(winforms, "System.Windows.Forms.Form")
        Me.System_Windows_Forms_Application = Me.GetType(winforms, "System.Windows.Forms.Application")
        Me.System_Windows_Forms_Application__Run = Me.GetMethod(Me.System_Windows_Forms_Application, "Run", Me.System_Windows_Forms_Form)

    End Sub

    Private Sub Init_Optimizations()
        If Helper.IsOnMono Then
            System_MonoType = Me.GetType(mscorlib, "System.MonoType")
        Else
            System_RuntimeType = Me.GetType(mscorlib, "System.RuntimeType")
            System_Reflection_Emit_TypeBuilderInstantiation = Me.GetType(mscorlib, "System.Reflection.Emit.TypeBuilderInstantiation")
            System_Reflection_Emit_SymbolType = Me.GetType(mscorlib, "System.Reflection.Emit.SymbolType")
        End If
        System_Reflection_Emit_TypeBuilder = Me.GetType(mscorlib, "System.Reflection.Emit.TypeBuilder")

    End Sub

    Private Sub Init()
        Init_Optimizations()
        Init_corlib()
        Init_vbruntime()

        [Nothing] = GetType([Nothing])
        DelegateUnresolvedType = GetType(DelegateUnresolvedType)

    End Sub

    Private Function GetVBType(ByVal Name As String) As Type
        Dim result As Type = Nothing

        If Compiler.CommandLine.NoVBRuntimeRef AndAlso vbruntime Is Nothing Then
            Dim tps As Generic.List(Of Type)
            tps = Compiler.TypeManager.GetType(Name, True)

            If tps.Count = 1 Then
                result = tps(0)
#If DEBUG Then
            ElseIf tps.Count > 1 Then
                Compiler.Report.WriteLine("Found " & tps.Count & " types with the name " & Name)
#End If
            End If
        Else
            result = vbruntime.GetType(Name)
        End If

#If DEBUG Then
        If result Is Nothing Then
            Compiler.Report.WriteLine("Could not load VB Type: " & Name)
        End If
#End If

        Return result
    End Function

    Private Shadows Function [GetType](ByVal Assembly As Assembly, ByVal FullName As String) As Type
        If Assembly Is Nothing Then Return Nothing
        Return Assembly.GetType(FullName, False, False)
    End Function

    Private Function GetProperty(ByVal Type As Type, ByVal Name As String, ByVal ParamArray Types() As Type) As PropertyInfo
        If Type Is Nothing Then Return Nothing
        Return Type.GetProperty(Name, BindingFlags.Instance Or BindingFlags.Static Or BindingFlags.Public Or BindingFlags.ExactBinding, Nothing, Nothing, Types, Nothing)
    End Function

    Private Function GetConstructor(ByVal Type As Type, ByVal ParamArray Types() As Type) As ConstructorInfo
        If Type Is Nothing Then Return Nothing
        Return Type.GetConstructor(BindingFlags.Public Or BindingFlags.Static Or BindingFlags.Instance Or BindingFlags.ExactBinding, Nothing, Types, Nothing)
    End Function

    Private Function GetMethod(ByVal Type As Type, ByVal Name As String, ByVal ParamArray Types() As Type) As MethodInfo
        Dim result As MethodInfo
        If Type Is Nothing Then
#If DEBUG Then
            Compiler.Report.WriteLine("Could not load the method '" & Name & "', the specified type was Nothing.")
#End If
            Return Nothing
        End If
        result = Type.GetMethod(Name, BindingFlags.Public Or BindingFlags.Static Or BindingFlags.Instance Or BindingFlags.ExactBinding, Nothing, Types, Nothing)

#If DEBUG Then
        If result Is Nothing Then
            Compiler.Report.WriteLine(Report.ReportLevels.Debug, "Could not find the method '" & Name & "' on the type '" & Type.FullName)
        End If
#End If

        Return result
    End Function

    Private Function GetField(ByVal Type As Type, ByVal Name As String) As FieldInfo
        If Type Is Nothing Then Return Nothing
        Return Type.GetField(Name)
    End Function

    Sub Init_vbruntime(Optional ByVal WithCurrentAssembly As Boolean = False)
        Dim staticBinding As BindingFlags = BindingFlags.Public Or BindingFlags.Instance Or BindingFlags.Static Or BindingFlags.ExactBinding

        If vbruntime Is Nothing AndAlso WithCurrentAssembly = False Then Return
        If MS_VB_CompareMethod IsNot Nothing Then Return 'already loaded
        MS_VB_CompareMethod = GetVBType("Microsoft.VisualBasic.CompareMethod")
        MS_VB_CS_Conversions = GetVBType("Microsoft.VisualBasic.CompilerServices.Conversions")
        MS_VB_CS_ProjectData = GetVBType("Microsoft.VisualBasic.CompilerServices.ProjectData")
        MS_VB_CS_LikeOperator = GetVBType("Microsoft.VisualBasic.CompilerServices.LikeOperator")

        MS_VB_Strings = GetVBType("Microsoft.VisualBasic.Strings")
        MS_VB_CS_StringType = GetVBType("Microsoft.VisualBasic.CompilerServices.StringType")
        MS_VB_MyGroupCollectionAttribute = GetVBType("Microsoft.VisualBasic.MyGroupCollectionAttribute")
        MS_VB_CallType = GetVBType("Microsoft.VisualBasic.CallType")

        MS_VB_Information = GetVBType("Microsoft.VisualBasic.Information")
        MS_VB_Information_IsNumeric = GetMethod(MS_VB_Information, "IsNumeric", Me.Object)
        MS_VB_Information_SystemTypeName = GetMethod(MS_VB_Information, "SystemTypeName", Me.String)
        MS_VB_Information_TypeName = GetMethod(MS_VB_Information, "TypeName", Me.Object)
        MS_VB_Information_VbTypeName = GetMethod(MS_VB_Information, "VbTypeName", Me.String)
        MS_VB_Interaction = GetVBType("Microsoft.VisualBasic.Interaction")
        MS_VB_Interaction_CallByName = GetMethod(MS_VB_Interaction, "CallByName", Me.Object, Me.String, Me.MS_VB_CallType, Me.Object_Array)

        MS_VB_CS_Versioned = GetVBType("Microsoft.VisualBasic.CompilerServices.Versioned")
        MS_VB_CS_Versioned_IsNumeric = GetMethod(MS_VB_CS_Versioned, "IsNumeric", Me.Object)
        MS_VB_CS_Versioned_SystemTypeName = GetMethod(MS_VB_CS_Versioned, "SystemTypeName", Me.String)
        MS_VB_CS_Versioned_TypeName = GetMethod(MS_VB_CS_Versioned, "TypeName", Me.Object)
        MS_VB_CS_Versioned_TypeName = GetMethod(MS_VB_CS_Versioned, "VbTypeName", Me.String)
        MS_VB_CS_Versioned_CallByName = GetMethod(MS_VB_CS_Versioned, "CallByName", Me.Object, Me.String, Me.MS_VB_CallType, Me.Object_Array)

        MS_VB_CS_StandardModuleAttribute = GetVBType("Microsoft.VisualBasic.CompilerServices.StandardModuleAttribute")
        MS_VB_CS_Operators = GetVBType("Microsoft.VisualBasic.CompilerServices.Operators")
        MS_VB_CS_OFC = GetVBType("Microsoft.VisualBasic.CompilerServices.ObjectFlowControl")
        MS_VB_CS_Utils = GetVBType("Microsoft.VisualBasic.CompilerServices.Utils")
        MS_VB_CS_OptionCompareAttribute = GetVBType("Microsoft.VisualBasic.CompilerServices.OptionCompareAttribute")
        MS_VB_CS_OptionTextAttribute = GetVBType("Microsoft.VisualBasic.CompilerServices.OptionTextAttribute")

        MS_VB_CS_StaticLocalInitFlag = GetVBType("Microsoft.VisualBasic.CompilerServices.StaticLocalInitFlag")
        MS_VB_CS_StaticLocalInitFlag_State = GetField(MS_VB_CS_StaticLocalInitFlag, "State")
        MS_VB_CS_StaticLocalInitFlag_Ctor = GetConstructor(MS_VB_CS_StaticLocalInitFlag)

        MS_VB_CS_IncompleteInitializationException = GetVBType("Microsoft.VisualBasic.CompilerServices.IncompleteInitialization")
        MS_VB_CS_IncompleteInitializationException__ctor = GetConstructor(MS_VB_CS_IncompleteInitializationException)

        MS_VB_CS_PD_EndApp = GetMethod(MS_VB_CS_ProjectData, "EndApp")
        MS_VB_CS_PD_CreateProjectError__Integer = GetMethod(MS_VB_CS_ProjectData, "CreateProjectError", [Integer])
        MS_VB_CS_PD_ClearProjectError = GetMethod(MS_VB_CS_ProjectData, "ClearProjectError")
        MS_VB_CS_PD_SetProjectError__Exception = GetMethod(MS_VB_CS_ProjectData, "SetProjectError", Exception)
        MS_VB_CS_PD_SetProjectError__Exception_Integer = GetMethod(MS_VB_CS_ProjectData, "SetProjectError", Exception, [Integer])

        MS_VB_CS_Conversions_ToBoolean__Object = GetMethod(MS_VB_CS_Conversions, "ToBoolean", Me.Object)
        MS_VB_CS_Conversions_ToChar__Object = GetMethod(MS_VB_CS_Conversions, "ToChar", Me.Object)
        MS_VB_CS_Conversions_ToDate__Object = GetMethod(MS_VB_CS_Conversions, "ToDate", Me.Object)
        MS_VB_CS_Conversions_ToByte__Object = GetMethod(MS_VB_CS_Conversions, "ToByte", Me.Object)
        MS_VB_CS_Conversions_ToSByte__Object = GetMethod(MS_VB_CS_Conversions, "ToSByte", Me.Object)
        MS_VB_CS_Conversions_ToShort__Object = GetMethod(MS_VB_CS_Conversions, "ToShort", Me.Object)
        MS_VB_CS_Conversions_ToUShort__Object = GetMethod(MS_VB_CS_Conversions, "ToUShort", Me.Object)
        MS_VB_CS_Conversions_ToInteger__Object = GetMethod(MS_VB_CS_Conversions, "ToInteger", Me.Object)
        MS_VB_CS_Conversions_ToUInteger__Object = GetMethod(MS_VB_CS_Conversions, "ToUInteger", Me.Object)
        MS_VB_CS_Conversions_ToLong__Object = GetMethod(MS_VB_CS_Conversions, "ToLong", Me.Object)
        MS_VB_CS_Conversions_ToULong__Object = GetMethod(MS_VB_CS_Conversions, "ToULong", Me.Object)
        MS_VB_CS_Conversions_ToSingle__Object = GetMethod(MS_VB_CS_Conversions, "ToSingle", Me.Object)
        MS_VB_CS_Conversions_ToDouble__Object = GetMethod(MS_VB_CS_Conversions, "ToDouble", Me.Object)
        MS_VB_CS_Conversions_ToDecimal__Object = GetMethod(MS_VB_CS_Conversions, "ToDecimal", Me.Object)
        MS_VB_CS_Conversions_ToBoolean__String = GetMethod(MS_VB_CS_Conversions, "ToBoolean", Me.String)
        MS_VB_CS_Conversions_ToChar__String = GetMethod(MS_VB_CS_Conversions, "ToChar", Me.String)
        MS_VB_CS_Conversions_ToDate__String = GetMethod(MS_VB_CS_Conversions, "ToDate", Me.String)
        MS_VB_CS_Conversions_ToByte__String = GetMethod(MS_VB_CS_Conversions, "ToByte", Me.String)
        MS_VB_CS_Conversions_ToSByte__String = GetMethod(MS_VB_CS_Conversions, "ToSByte", Me.String)
        MS_VB_CS_Conversions_ToShort__String = GetMethod(MS_VB_CS_Conversions, "ToShort", Me.String)
        MS_VB_CS_Conversions_ToUShort__String = GetMethod(MS_VB_CS_Conversions, "ToUShort", Me.String)
        MS_VB_CS_Conversions_ToInteger__String = GetMethod(MS_VB_CS_Conversions, "ToInteger", Me.String)
        MS_VB_CS_Conversions_ToUInteger__String = GetMethod(MS_VB_CS_Conversions, "ToUInteger", Me.String)
        MS_VB_CS_Conversions_ToLong__String = GetMethod(MS_VB_CS_Conversions, "ToLong", Me.String)
        MS_VB_CS_Conversions_ToULong__String = GetMethod(MS_VB_CS_Conversions, "ToULong", Me.String)
        MS_VB_CS_Conversions_ToSingle__String = GetMethod(MS_VB_CS_Conversions, "ToSingle", Me.String)
        MS_VB_CS_Conversions_ToDouble__String = GetMethod(MS_VB_CS_Conversions, "ToDouble", Me.String)
        MS_VB_CS_Conversions_ToDecimal__String = GetMethod(MS_VB_CS_Conversions, "ToDecimal", Me.String)
        MS_VB_CS_Conversions_ToDecimal__Boolean = GetMethod(MS_VB_CS_Conversions, "ToDecimal", Me.Boolean)
        MS_VB_CS_Conversions_ToString__Decimal = GetMethod(MS_VB_CS_Conversions, "ToString", Me.Decimal)
        MS_VB_CS_Conversions_ToString__Boolean = GetMethod(MS_VB_CS_Conversions, "ToString", Me.Boolean)
        MS_VB_CS_Conversions_ToString__Char = GetMethod(MS_VB_CS_Conversions, "ToString", Me.Char)
        MS_VB_CS_Conversions_ToString__Date = GetMethod(MS_VB_CS_Conversions, "ToString", Me.Date)
        MS_VB_CS_Conversions_ToString__Byte = GetMethod(MS_VB_CS_Conversions, "ToString", Me.Byte)
        MS_VB_CS_Conversions_ToString__Integer = GetMethod(MS_VB_CS_Conversions, "ToString", Me.Integer)
        MS_VB_CS_Conversions_ToString__UInteger = GetMethod(MS_VB_CS_Conversions, "ToString", Me.UInteger)
        MS_VB_CS_Conversions_ToString__Long = GetMethod(MS_VB_CS_Conversions, "ToString", Me.Long)
        MS_VB_CS_Conversions_ToString__ULong = GetMethod(MS_VB_CS_Conversions, "ToString", Me.ULong)
        MS_VB_CS_Conversions_ToString__Single = GetMethod(MS_VB_CS_Conversions, "ToString", Me.Single)
        MS_VB_CS_Conversions_ToString__Double = GetMethod(MS_VB_CS_Conversions, "ToString", Me.Double)
        MS_VB_CS_Conversions_ToString__Object = GetMethod(MS_VB_CS_Conversions, "ToString", Me.Object)
        MS_VB_CS_Conversions_ToGenericParameter_T__Object = GetMethod(MS_VB_CS_Conversions, "ToGenericParameter", Me.Object)

        MS_VB_CS_LikeOperator_LikeString__String_String_CompareMethod = GetMethod(MS_VB_CS_LikeOperator, "LikeString", Me.String, Me.String, MS_VB_CompareMethod)
        MS_VB_CS_LikeOperator_LikeObject__Object_Object_CompareMethod = GetMethod(MS_VB_CS_LikeOperator, "LikeObject", Me.Object, Me.Object, MS_VB_CompareMethod)

        MS_VB_CS_StringType_MidStmtStr__String_Integer_Integer_String = GetMethod(MS_VB_CS_StringType, "MidStmtStr", String_ByRef, Me.Integer, Me.Integer, Me.String)

        MS_VB_CS_OFC_CheckForSyncLockOnValueType__Object = GetMethod(MS_VB_CS_OFC, "CheckForSyncLockOnValueType", Me.Object)

        MS_VB_CS_Utils__CopyArray_Array_Array = GetMethod(MS_VB_CS_Utils, "CopyArray", Array, Array)

        MS_VB_CS_Operators_ConditionalCompareObjectEqual__Object_Object_Bool = GetMethod(MS_VB_CS_Operators, "ConditionalCompareObjectEqual", Me.Object, Me.Object, Me.Boolean)
        MS_VB_CS_Operators_ConditionalCompareObjectNotEqual__Object_Object_Bool = GetMethod(MS_VB_CS_Operators, "ConditionalCompareObjectNotEqual", Me.Object, Me.Object, Me.Boolean)
        MS_VB_CS_Operators_ConditionalCompareObjectGreater__Object_Object_Bool = GetMethod(MS_VB_CS_Operators, "ConditionalCompareObjectGreater", Me.Object, Me.Object, Me.Boolean)
        MS_VB_CS_Operators_ConditionalCompareObjectGreaterEqual__Object_Object_Bool = GetMethod(MS_VB_CS_Operators, "ConditionalCompareObjectGreaterEqual", Me.Object, Me.Object, Me.Boolean)
        MS_VB_CS_Operators_ConditionalCompareObjectLess__Object_Object_Bool = GetMethod(MS_VB_CS_Operators, "ConditionalCompareObjectLess", Me.Object, Me.Object, Me.Boolean)
        MS_VB_CS_Operators_ConditionalCompareObjectLessEqual__Object_Object_Bool = GetMethod(MS_VB_CS_Operators, "ConditionalCompareObjectLessEqual", Me.Object, Me.Object, Me.Boolean)
        MS_VB_CS_Operators_CompareString__String_String_Bool = GetMethod(MS_VB_CS_Operators, "CompareString", Me.String, Me.String, Me.Boolean)
        MS_VB_CS_Operators_ConcatenateObject__Object_Object = GetMethod(MS_VB_CS_Operators, "ConcatenateObject", Me.Object, Me.Object)
        MS_VB_CS_Operators_AddObject__Object_Object = GetMethod(MS_VB_CS_Operators, "AddObject", Me.Object, Me.Object)
        MS_VB_CS_Operators_AndObject__Object_Object = GetMethod(MS_VB_CS_Operators, "AndObject", Me.Object, Me.Object)
        MS_VB_CS_Operators_DivideObject__Object_Object = GetMethod(MS_VB_CS_Operators, "DivideObject", Me.Object, Me.Object)
        MS_VB_CS_Operators_ExponentObject__Object_Object = GetMethod(MS_VB_CS_Operators, "ExponentObject", Me.Object, Me.Object)
        MS_VB_CS_Operators_IntDivideObject__Object_Object = GetMethod(MS_VB_CS_Operators, "IntDivideObject", Me.Object, Me.Object)
        MS_VB_CS_Operators_LeftShiftObject__Object_Object = GetMethod(MS_VB_CS_Operators, "LeftShiftObject", Me.Object, Me.Object)
        MS_VB_CS_Operators_ModObject__Object_Object = GetMethod(MS_VB_CS_Operators, "ModObject", Me.Object, Me.Object)
        MS_VB_CS_Operators_MultiplyObject__Object_Object = GetMethod(MS_VB_CS_Operators, "MultiplyObject", Me.Object, Me.Object)
        MS_VB_CS_Operators_NegateObject__Object = GetMethod(MS_VB_CS_Operators, "NegateObject", Me.Object)
        MS_VB_CS_Operators_NotObject__Object = GetMethod(MS_VB_CS_Operators, "NotObject", Me.Object)
        MS_VB_CS_Operators_OrObject__Object_Object = GetMethod(MS_VB_CS_Operators, "OrObject", Me.Object, Me.Object)
        MS_VB_CS_Operators_PlusObject__Object = GetMethod(MS_VB_CS_Operators, "PlusObject", Me.Object)
        MS_VB_CS_Operators_RightShiftObject__Object_Object = GetMethod(MS_VB_CS_Operators, "RightShiftObject", Me.Object, Me.Object)
        MS_VB_CS_Operators_SubtractObject__Object_Object = GetMethod(MS_VB_CS_Operators, "SubtractObject", Me.Object, Me.Object)
        MS_VB_CS_Operators_XorObject__Object_Object = GetMethod(MS_VB_CS_Operators, "XorObject", Me.Object, Me.Object)
        MS_VB_CS_Operators_LikeObject__Object_Object = GetMethod(MS_VB_CS_Operators, "LikeObject", Me.Object, Me.Object, MS_VB_CompareMethod)
        MS_VB_CS_Operators_LikeString__String_String = GetMethod(MS_VB_CS_Operators, "LikeString", Me.String, Me.String, MS_VB_CompareMethod)
        MS_VB_CS_Operators_CompareObjectEqual__Object_Object_Bool = GetMethod(MS_VB_CS_Operators, "CompareObjectEqual", Me.Object, Me.Object, Me.Boolean)
        MS_VB_CS_Operators_CompareObjectNotEqual__Object_Object_Bool = GetMethod(MS_VB_CS_Operators, "CompareObjectNotEqual", Me.Object, Me.Object, Me.Boolean)
        MS_VB_CS_Operators_CompareObjectGreater__Object_Object_Bool = GetMethod(MS_VB_CS_Operators, "CompareObjectGreater", Me.Object, Me.Object, Me.Boolean)
        MS_VB_CS_Operators_CompareObjectGreaterEqual__Object_Object_Bool = GetMethod(MS_VB_CS_Operators, "CompareObjectGreaterEqual", Me.Object, Me.Object, Me.Boolean)
        MS_VB_CS_Operators_CompareObjectLess__Object_Object_Bool = GetMethod(MS_VB_CS_Operators, "CompareObjectLess", Me.Object, Me.Object, Me.Boolean)

        MS_VB_CS_Operators_CompareObjectLessEqual__Object_Object_Bool = GetMethod(MS_VB_CS_Operators, "CompareObjectLessEqual", Me.Object, Me.Object, Me.Boolean)

        Delegate_Combine = GetMethod(Me.Delegate, "Combine", Me.Delegate, Me.Delegate)
        Delegate_Remove = GetMethod(Me.Delegate, "Remove", Me.Delegate, Me.Delegate)
        StandardModuleAttribute = GetVBType("Microsoft.VisualBasic.CompilerServices.StandardModuleAttribute")
    End Sub

    Sub Init(ByVal Assemblies As Generic.List(Of Assembly))
        For Each a As Assembly In Assemblies
            If a.GetName.Name = "Microsoft.VisualBasic" Then
                vbruntime = a : Continue For
            ElseIf a.GetName.Name = "mscorlib" Then
                mscorlib = a : Continue For
            ElseIf a.GetName.Name = "System.Windows.Forms" Then
                winforms = a : Continue For
            End If
        Next

        Init()
        If vbruntime IsNot Nothing Then
            Init_vbruntime()
        End If
    End Sub

    Shared Function GetCombination(ByVal tp1 As TypeCode, ByVal tp2 As TypeCode) As TypeCombinations
        Return CType(CInt(tp1) << TypeCombinations.SHIFT Or CInt(tp2), TypeCombinations)
    End Function
End Class