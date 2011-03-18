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

Public MustInherit Class TypeCacheBase
    Private m_Compiler As Compiler

    Sub New(ByVal Compiler As Compiler)
        m_Compiler = Compiler
    End Sub

    ReadOnly Property Compiler() As Compiler
        Get
            Return m_Compiler
        End Get
    End Property

#If DEBUG Then
    'This method will generate the other partial TypeCache class.
    Shared Sub Generate()
        Dim path As String = "..\source\General\"
        Dim file As String = path & "TypeCache.in"
        Dim filename As String = IO.Path.GetFullPath(file)
        Dim content As String = IO.File.ReadAllText(filename)
        Dim lines As String() = content.Split(New String() {VB.vbCr, VB.vbLf, VB.vbCrLf}, StringSplitOptions.RemoveEmptyEntries)

        Dim cecil As New System.Text.StringBuilder
        Dim all As New System.Text.StringBuilder

        all.AppendLine(VB.Join(content.Substring(0, content.IndexOf("''") - 1).Split(New String() {VB.vbCr, VB.vbLf, VB.vbCrLf}, StringSplitOptions.RemoveEmptyEntries), Environment.NewLine))

        cecil.AppendLine("Public Partial Class CecilTypeCache")
        cecil.AppendLine(Generate(lines))
        cecil.AppendLine("End Class")

        all.AppendLine(cecil.ToString)

        IO.File.WriteAllText(path & "TypeCache.Generated.vb", all.ToString)

        IO.File.Copy(IO.Path.Combine(path, "TypeCache.vb"), IO.Path.Combine(path, "TypeCache.vb.old"), True)

        Dim oldContents As String = IO.File.ReadAllText(IO.Path.Combine(path, "TypeCache.vb"))
        Dim iStart As Integer = oldContents.IndexOf("'START" & " SRE") + ("'START " & "SRE").Length + 2
        Dim iEnd As Integer = oldContents.IndexOf("'END SRE", iStart) - 2
        oldContents = oldContents.Remove(iStart, iEnd - iStart)
        oldContents = oldContents.Insert(iStart, all.ToString())
        IO.File.WriteAllText(IO.Path.Combine(path, "TypeCache.vb"), oldContents)
        System.Diagnostics.Debug.WriteLine("Written TypeCache.vb, saved to TypeCache.vb.old")
    End Sub

    Shared Function Generate(ByVal Lines As String()) As String
        Dim variables As New System.Text.StringBuilder
        Dim getters As New System.Text.StringBuilder
        Dim vbtypes As New System.Text.StringBuilder
        Dim vbmembers As New System.Text.StringBuilder

        'Turns out using public fields instead of private fields with property getters is about 10% faster during bootstrapping.
        'Quite possibly because there's quite more code to compile with the properties
        Dim publicfields As Boolean = True

        If publicfields Then
            getters.AppendLine("    Protected Overrides Sub InitInternal ()")
        End If

        vbtypes.AppendLine("    Public Overrides Sub InitInternalVB()")
        vbmembers.AppendLine("    Public Overrides Sub InitInternalVBMembers()")

        For Each line As String In Lines
            If line.StartsWith("'") Then Continue For
            If line.Trim = "" Then Continue For

            Dim splitted() As String = line.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)
            Dim name, type, find, parameters As String
            Dim param As Integer = Integer.MaxValue
            Dim noparaminname As Boolean = False
            Dim isVB As Boolean = False
            Dim isVBMember As Boolean = False

            parameters = Nothing

            Select Case splitted(0)
                Case "vbtype"
                    name = splitted(1)
                    name = name.Replace("Microsoft.VisualBasic.CompilerServices.", "MS_VB_CS_")
                    name = name.Replace("Microsoft.VisualBasic.", "MS_VB_")
                    name = name.Replace("""", "").Replace(".", "_").Replace("`", "").Replace("+", "_")
                    type = "Mono.Cecil.TypeDefinition"
                    find = "GetVBType"
                    parameters = splitted(1)
                    isVB = True
                Case "type"
                    name = splitted(2).Replace("""", "").Replace(".", "_").Replace("`", "").Replace("+", "_")
                    type = "Mono.Cecil.TypeDefinition"
                    find = "[GetType]"
                    parameters = splitted(1) & ", " & splitted(2)

                    If splitted(2).IndexOf("+"c) > 0 Then
                        Dim declaringtype As String = splitted(2).Substring(0, splitted(2).LastIndexOf("+"c)).Replace(".", "_").Replace("""", "")
                        Dim nestedtype As String = """" & splitted(2).Substring(splitted(2).LastIndexOf("+"c) + 1)
                        parameters = declaringtype & ", " & nestedtype
                    End If

                Case "array"
                    name = splitted(1).Replace("""", "").Replace(".", "_").Replace("`", "") & "_Array"
                    type = "Mono.Cecil.TypeReference"
                    find = "GetArrayType"
                    parameters = splitted(1)
                Case "byref"
                    name = splitted(1).Replace("""", "").Replace(".", "_").Replace("`", "") & "_ByRef"
                    type = "Mono.Cecil.TypeReference"
                    find = "GetByRefType"
                    parameters = splitted(1)
                Case "method", "method2"
                    name = splitted(1) & "__" & splitted(2).Replace("""", "").Replace(".", "_").Replace("`", "")
                    type = "Mono.Cecil.MethodDefinition"
                    If splitted(0) <> "method" Then noparaminname = True
                    param = 3
                    find = "GetMethod"
                    parameters = splitted(1) & ", " & splitted(2)
                    isVBMember = splitted(1).StartsWith("MS_")
                Case "property"
                    name = splitted(1) & "__" & splitted(2).Replace("""", "").Replace(".", "_").Replace("`", "")
                    type = "Mono.Cecil.PropertyDefinition"
                    param = 3
                    find = "GetProperty"
                    parameters = splitted(1) & ", " & splitted(2)
                    isVBMember = splitted(1).StartsWith("MS_")
                Case "field"
                    name = splitted(1) & "__" & splitted(2).Replace("""", "").Replace(".", "_").Replace("`", "")
                    type = "Mono.Cecil.FieldDefinition"
                    find = "GetField"
                    parameters = splitted(1) & ", " & splitted(2)
                    isVBMember = splitted(1).StartsWith("MS_")
                Case "ctor"
                    name = splitted(1) & "__ctor"
                    type = "Mono.Cecil.MethodDefinition"
                    param = 2
                    find = "GetConstructor"
                    parameters = splitted(1)
                    isVBMember = splitted(1).StartsWith("MS_")
                Case Else
                    Helper.Stop()
                    Throw New NotImplementedException(splitted(0))
            End Select

            For i As Integer = param To splitted.GetUpperBound(0)
                If noparaminname = False Then
                    Dim lastUScore As Integer
                    Dim p As String = splitted(i)
                    p = splitted(i)
                    p = p.Replace("_ByRef", "")
                    lastUScore = p.LastIndexOf("_"c) + 1
                    name &= "_" & p.Substring(lastUScore)
                End If
                If parameters IsNot Nothing Then parameters &= ", "
                parameters &= splitted(i)
            Next

            If publicfields Then
                variables.AppendLine(String.Format("    Public {0} As {1}", name, type))
            Else
                variables.AppendLine(String.Format("    Private m_{0} As {1}", name, type))
            End If

            Dim text As String
            If publicfields Then
                text = String.Format("        {0} = {1}({2})", name, find, parameters)
                If isVB Then
                    vbtypes.AppendLine(text)
                ElseIf isVBMember Then
                    vbmembers.AppendLine(text)
                Else
                    getters.AppendLine(text)
                End If
            Else
                getters.AppendLine(String.Format("    Public ReadOnly Property {0} As {1}", name, type))
                getters.AppendLine(String.Format("        Get"))
                getters.AppendLine(String.Format("            If m_{0} Is Nothing Then", name))
                getters.AppendLine(String.Format("                m_{0} = {1}({2})", name, find, parameters))
                getters.AppendLine(String.Format("            End If"))
                getters.AppendLine(String.Format("            Return m_{0}", name))
                getters.AppendLine(String.Format("        End Get"))
                getters.AppendLine(String.Format("    End Property"))
            End If
        Next

        If publicfields Then
            getters.AppendLine("    End Sub")
        End If

        vbtypes.AppendLine("    End Sub")
        vbmembers.AppendLine("    End Sub")

        Return variables.ToString & Environment.NewLine & getters.ToString & Environment.NewLine & vbtypes.ToString & Environment.NewLine & vbmembers.ToString
    End Function
#End If

    Protected Overloads Function [GetType](ByVal Assembly As Mono.Cecil.AssemblyDefinition, ByVal FullName As String) As Mono.Cecil.TypeDefinition
        If Assembly Is Nothing Then Return Nothing
        Dim result As Mono.Cecil.TypeDefinition = Nothing
        Dim split As String() = FullName.Split("+"c)
        For i As Integer = 0 To split.Length - 1
            If result Is Nothing Then
                result = Assembly.MainModule.GetType(split(i))
            Else
                result = Me.GetType(result, split(i))
            End If
        Next
        If result Is Nothing Then Compiler.Report.WriteLine(String.Format("Could not load the type '{0}' from the assembly '{1}'", FullName, Assembly.Name.FullName))
        Return result
    End Function

    Protected Overloads Function [GetType](ByVal Type As Mono.Cecil.TypeDefinition, ByVal Name As String) As Mono.Cecil.TypeDefinition
        If Type Is Nothing Then Return Nothing
        Dim result As Mono.Cecil.TypeDefinition = Nothing
        For Each item As Mono.Cecil.TypeDefinition In Type.NestedTypes
            If Helper.CompareNameOrdinal(item.Name, Name) Then
                result = item
                Exit For
            End If
        Next
        If result Is Nothing Then Compiler.Report.WriteLine(String.Format("Could not load the nested type '{0}' from the type '{1}'", Name, Type.FullName))
        Return result
    End Function

    Protected Function GetByRefType(ByVal Type As Mono.Cecil.TypeDefinition) As Mono.Cecil.TypeReference
        Return New ByReferenceType(Type)
    End Function

    Protected Function GetArrayType(ByVal Type As Mono.Cecil.TypeDefinition) As Mono.Cecil.TypeReference
        Return New Mono.Cecil.ArrayType(Type)
    End Function

    Protected Function GetProperty(ByVal Type As Mono.Cecil.TypeDefinition, ByVal Name As String, ByVal ParamArray Types() As Mono.Cecil.TypeReference) As Mono.Cecil.PropertyDefinition
        If Type Is Nothing Then Return Nothing
        Dim properties As Mono.Collections.Generic.Collection(Of PropertyDefinition)
        properties = CecilHelper.FindProperties(Type.Properties, Name)
        For i As Integer = 0 To properties.Count - 1
            Dim prop As Mono.Cecil.PropertyDefinition = properties(i)
            Dim params As Mono.Collections.Generic.Collection(Of ParameterDefinition) = prop.Parameters

            If IsMatch(Types, params) Then Return prop
        Next

        Return Nothing
    End Function

    Private Function IsMatch(ByVal Types As Mono.Cecil.TypeReference(), ByVal Parameters As Mono.Collections.Generic.Collection(Of ParameterDefinition)) As Boolean
        If Parameters.Count = 0 AndAlso (Types Is Nothing OrElse Types.Length = 0) Then Return True
        If Parameters.Count <> Types.Length Then Return False
        For j As Integer = 0 To Parameters.Count - 1
            If Helper.CompareType(Parameters(j).ParameterType, Types(j)) = False Then Return False
        Next
        Return True
    End Function

    Protected Function GetConstructor(ByVal Type As Mono.Cecil.TypeDefinition, ByVal ParamArray Types() As Mono.Cecil.TypeReference) As Mono.Cecil.MethodDefinition
        Return GetMethod(Type, ".ctor", Types)
    End Function

    Protected Function GetMethod(ByVal Type As Mono.Cecil.TypeDefinition, ByVal Name As String, ByVal ParamArray Types() As Mono.Cecil.TypeReference) As Mono.Cecil.MethodDefinition
        Dim result As Mono.Cecil.MethodDefinition = Nothing
        If Type Is Nothing Then
#If DEBUG Then
            Compiler.Report.WriteLine("Could not load the method '" & Name & "', the specified type was Nothing.")
#End If
            Return Nothing
        End If

        For i As Integer = 0 To Type.Methods.Count - 1
            Dim md As MethodDefinition = Type.Methods(i)
            If Helper.CompareNameOrdinal(md.Name, Name) = False Then Continue For
            If Helper.CompareTypes(Helper.GetTypes(md.Parameters), Types) = False Then Continue For
            result = md
            Exit For
        Next
#If DEBUG Then
        If result Is Nothing Then
            Compiler.Report.WriteLine(Report.ReportLevels.Debug, "Could not find the method '" & Name & "' on the type '" & Type.FullName)
        End If
#End If

        Return result
    End Function

    Protected Function GetField(ByVal Type As Mono.Cecil.TypeDefinition, ByVal Name As String) As Mono.Cecil.FieldDefinition
        If Type Is Nothing Then Return Nothing
        Return CecilHelper.FindField(Type.Fields, Name)
    End Function

    Sub Init()
        InitOptimizations()
        InitAssemblies()
        InitInternal()
        InitVBNCTypes()
    End Sub

    Protected MustOverride Sub InitInternal()

    Protected MustOverride Sub InitOptimizations()

    Protected MustOverride Sub InitAssemblies()

    Protected MustOverride Sub InitVBNCTypes()

    Public MustOverride Sub InitInternalVB()

    Public Overridable Sub InitInternalVBMembers()

    End Sub

End Class

Public Class CecilTypeCache
    Inherits TypeCacheBase

    Public vbruntime As Mono.Cecil.AssemblyDefinition
    Public mscorlib As Mono.Cecil.AssemblyDefinition
    Public winforms As Mono.Cecil.AssemblyDefinition

    'Special vbnc types
    Public [Nothing] As Mono.Cecil.TypeDefinition
    Public DelegateUnresolvedType As Mono.Cecil.TypeDefinition

    'Stupid MS type system with some optimizations requires this
    Public System_MonoType As Mono.Cecil.TypeDefinition
    Public System_RuntimeType As Mono.Cecil.TypeDefinition
    Public System_Reflection_Emit_TypeBuilder As Mono.Cecil.TypeDefinition
    Public System_Reflection_Emit_TypeBuilderInstantiation As Mono.Cecil.TypeDefinition
    Public System_Reflection_Emit_SymbolType As Mono.Cecil.TypeDefinition

    Sub New(ByVal Compiler As Compiler)
        MyBase.New(Compiler)
    End Sub

    Protected Function GetVBType(ByVal Name As String) As Mono.Cecil.TypeDefinition
        Dim result As Mono.Cecil.TypeDefinition = Nothing

        If String.IsNullOrEmpty(Compiler.CommandLine.VBRuntime) AndAlso vbruntime Is Nothing Then
            Dim tps As Generic.List(Of Mono.Cecil.TypeReference)
            tps = Compiler.TypeManager.GetType(Name, True)

            If tps.Count = 1 Then
                result = CecilHelper.FindDefinition(tps(0))
#If DEBUG Then
            ElseIf tps.Count > 1 Then
                Compiler.Report.WriteLine("Found " & tps.Count & " types with the name " & Name)
#End If
            End If
        Else
            result = [GetType](vbruntime, Name)
        End If

#If DEBUG Then
        If result Is Nothing Then
            Compiler.Report.WriteLine("Could not load VB Type: " & Name)
        End If
#End If

        Return result
    End Function

    Protected Overrides Sub InitAssemblies()
        For Each a As Mono.Cecil.AssemblyDefinition In Compiler.TypeManager.CecilAssemblies
            If a.Name.Name = "Microsoft.VisualBasic" Then
                vbruntime = a : Continue For
            ElseIf a.Name.Name = "mscorlib" Then
                mscorlib = a : Continue For
            ElseIf a.Name.Name = "System.Windows.Forms" Then
                winforms = a : Continue For
            End If
        Next
    End Sub

    Protected Overrides Sub InitOptimizations()
        If Helper.IsOnMono Then
            System_MonoType = Me.GetType(mscorlib, "System.MonoType")
        Else
            System_RuntimeType = Me.GetType(mscorlib, "System.RuntimeType")
            System_Reflection_Emit_TypeBuilderInstantiation = Me.GetType(mscorlib, "System.Reflection.Emit.TypeBuilderInstantiation")
            System_Reflection_Emit_SymbolType = Me.GetType(mscorlib, "System.Reflection.Emit.SymbolType")
        End If
        System_Reflection_Emit_TypeBuilder = Me.GetType(mscorlib, "System.Reflection.Emit.TypeBuilder")

    End Sub

    Protected Overrides Sub InitVBNCTypes()
        [Nothing] = New Mono.Cecil.TypeDefinition("Nothing", "vbnc", Mono.Cecil.TypeAttributes.Public Or Mono.Cecil.TypeAttributes.Class, Me.System_Object)
        DelegateUnresolvedType = New Mono.Cecil.TypeDefinition("DelegateUnresolvedType", "vbnc", Mono.Cecil.TypeAttributes.Public Or Mono.Cecil.TypeAttributes.Class, Me.System_Object)
    End Sub

End Class

'START SRE
'' 
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
        If MS_VB_CS_ObjectFlowControl IsNot Nothing Then
            For i As Integer = 0 To MS_VB_CS_ObjectFlowControl.NestedTypes.Count - 1
                If MS_VB_CS_ObjectFlowControl.NestedTypes(i).Name = "ForLoopControl" Then
                    MS_VB_CS_ObjectFlowControl_ForLoopControl = MS_VB_CS_ObjectFlowControl.NestedTypes(i)
                    Exit For
                End If
            Next
        End If
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



'END SRE

