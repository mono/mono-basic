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

#If DEBUG Then
#Const DEBUGMETHODRESOLUTION = 0
#Const DEBUGMETHODADD = 0
#Const EXTENDEDDEBUG = 0
#End If
''' <summary>
''' A module of useful global functions.
''' </summary>
''' <remarks></remarks>
Public Class Helper
    Private m_Compiler As Compiler
    
    Public Shared StringComparer As System.StringComparer = System.StringComparer.OrdinalIgnoreCase
    Public Shared StringComparison As StringComparison = StringComparison.OrdinalIgnoreCase

    Private Shared m_SharedCompilers As New Generic.List(Of Compiler)

    Public Shared LOGMETHODRESOLUTION As Boolean = False

    Public Const ALLMEMBERS As BindingFlags = BindingFlags.FlattenHierarchy Or BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Instance Or BindingFlags.Static

    Public Const ALLNOBASEMEMBERS As BindingFlags = BindingFlags.DeclaredOnly Or BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Instance Or BindingFlags.Static

#If DEBUGREFLECTION Then
    Private Shared object_ids As New ArrayList()
    Private Shared object_nameshash As New Generic.Dictionary(Of String, Object)
    Private Shared object_names As New Generic.List(Of String)

    Private Shared m_DebugReflection As System.Text.StringBuilder
    Private Shared m_DebugReflectionFinalized As Boolean

    Shared ReadOnly Property DebugReflectionOutput() As System.Text.StringBuilder
        Get
            If m_DebugReflection Is Nothing Then
                m_DebugReflection = New System.Text.StringBuilder()
                DebugReflection_Init()
            End If

            Return m_DebugReflection
        End Get
    End Property

    Shared Function DebugReflection_Dump(ByVal Compiler As Compiler) As String
        DebugReflection_Finalize(Compiler)
        Return m_DebugReflection.ToString
    End Function

    Shared Sub DebugReflection_Init()
        m_DebugReflection.AppendLine("Imports System")
        m_DebugReflection.AppendLine("Imports System.Reflection")
        m_DebugReflection.AppendLine("Imports System.Reflection.Emit")
        m_DebugReflection.AppendLine("Class DebugReflection")
        m_DebugReflection.AppendLine("    Shared Sub Main ()")
    End Sub

    Shared Sub DebugReflection_Finalize(ByVal Compiler As Compiler)
        If m_DebugReflectionFinalized Then Return

        m_DebugReflection.AppendLine(String.Format("        {0}.Save(""{1}"")", GetObjectName(Compiler.AssemblyBuilder), IO.Path.GetFileName(Compiler.OutFileName)))
        m_DebugReflection.AppendLine("    End Sub")
        m_DebugReflection.AppendLine("End Class")

        m_DebugReflectionFinalized = True
    End Sub

    Shared Function DebugReflection_BuildArray(Of T)(ByVal Array As T()) As T()
        Dim arr() As T
        ReDim arr(Array.Length - 1)
        System.Array.Copy(Array, arr, Array.Length)
        For i As Integer = 0 To Array.Length - 1
            Helper.DebugReflection_AppendLine("{0}({1}) = {2}", arr, i, arr(i))
        Next
        Return arr
    End Function

    Shared Sub DebugReflection_AppendLine(ByVal Line As String, ByVal ParamArray objects() As Object)
        Dim strs() As String

        If objects.Length > 0 Then
            ReDim strs(objects.Length - 1)
            For i As Integer = 0 To objects.Length - 1
                strs(i) = GetObjectName(objects(i))
            Next
            Select Case strs.Length
                Case 1
                    Line = String.Format(Line, strs(0))
                Case 2
                    Line = String.Format(Line, strs(0), strs(1))
                Case 3
                    Line = String.Format(Line, strs(0), strs(1), strs(2))
                Case 4
                    Line = String.Format(Line, strs(0), strs(1), strs(2), strs(3))
                Case 5
                    Line = String.Format(Line, strs(0), strs(1), strs(2), strs(3), strs(4))
                Case Else
                    Line = String.Format(Line, strs)
            End Select
        End If
        DebugReflectionOutput.AppendLine("        " & Line)
    End Sub

    Shared Function GetObjectName(ByVal obj As Object) As String
        If obj Is Nothing Then Return "Nothing"
        If TypeOf obj Is String Then Return CStr(obj)
        If TypeOf obj Is EmitLog Then obj = DirectCast(obj, EmitLog).TheRealILGenerator

        For i As Integer = 0 To object_ids.Count - 1
            If object_ids(i) Is obj Then Return object_names(i)
        Next

        Dim type As Type = obj.GetType
        Dim name As String
        Dim typename As String

        If type.FullName = "System.Reflection.Emit.TypeBuilderInstantiation" Then type = type.BaseType
        If type.FullName = "System.RuntimeType" Then type = type.BaseType
        If type.FullName = "System.Reflection.MonoGenericClass" Then type = type.BaseType
        If type.FullName = "System.MonoType" Then type = type.BaseType

        Dim counter As Integer = 1
        For i As Integer = 0 To object_ids.Count - 1
            If object_ids(i).GetType Is type Then counter += 1 : Continue For
            If type.IsArray AndAlso object_ids(i).GetType Is type.GetElementType Then counter += 1 : Continue For
        Next
        If type.IsArray Then
            name = type.GetElementType.Name & "Array_" & counter
            typename = type.GetElementType.FullName & "()"
        Else
            name = obj.GetType.Name & "_" & counter
            typename = type.FullName
        End If

        Helper.DebugReflection_AppendLine("Dim " & name & " As " & typename)

        object_ids.Add(obj)
        object_names.Add(name)
        object_nameshash.Add(name, obj)

        Return name
    End Function
#End If

#If DEBUG Then
    Shared Function ShowDebugFor(ByVal name As String) As Boolean
        Static args As Hashtable
        If args Is Nothing Then
            args = New Hashtable(System.StringComparer.OrdinalIgnoreCase)
            Dim env As String = Environment.GetEnvironmentVariable("VBNC_LOG")
            If env Is Nothing Then Return False
            For Each arg As String In env.Split(","c, ":"c, ";"c)
                args.Add(arg, arg)
            Next
        End If
        Return args.ContainsKey(name)
    End Function
#End If

#Region "Helper"

    'Constant methods.
    'Private Shared m_Asc_Char As MethodInfo ', if the constant string is not empty
    'Private Shared m_Asc_String As MethodInfo ', if the constant string is not empty
    'Private Shared m_AscW_Char As MethodInfo ', if the constant string is not empty
    'Private Shared m_AscW_String As MethodInfo ', if the constant string is not empty
    'Private Shared m_Chr_Integer As MethodInfo ', if the constant value is between 0 and 128
    'Private Shared m_ChrW_Integer As MethodInfo
    'Private Shared m_AllConstantFunctions As ArrayList

    'A constant expression is an expression whose value can be fully evaluated at compile time. The type of a constant expression can be Byte, Short, Integer, Long, Char, Single, Double, Decimal, Boolean, String, or any enumeration type. The following constructs are permitted in constant expressions: 
    'Literals (including Nothing).
    'References to constant type members or constant locals.
    'References to members of enumeration types.
    'Parenthesized subexpressions.
    'Coercion expressions, provided the target type is one of the types listed above. Coercions to and from String are an exception to this rule and not allowed because String conversions are always done in the current culture of the execution environment at run time.
    'The +, - and Not unary operators.
    'The +, -, *, ^, Mod, /, \, <<, >>, &, And, Or, Xor, AndAlso, OrElse, =, <, >, <>, <=, and => binary operators, provided each operand is of a type listed above.
    'The following run-time functions: 
    'Microsoft.VisualBasic.Strings.ChrW
    'Microsoft.VisualBasic.Strings.Chr, if the constant value is between 0 and 128
    'Microsoft.VisualBasic.Strings.AscW, if the constant string is not empty
    'Microsoft.VisualBasic.Strings.Asc, if the constant string is not empty

    'Constant expressions of an integral type (Long, Integer, Short, Byte) can be implicitly converted to a narrower integral type, and constant expressions of type Double can be implicitly converted to Single, provided the value of the constant expression is within the range of the destination type. These narrowing conversions are allowed regardless of whether permissive or strict semantics are being used.

    Private Shared Function IsMethod(ByVal m1 As MethodInfo, ByVal Name As String, ByVal ParameterType As Type, ByVal ReturnType As Type) As Boolean
        If m1.IsGenericMethod Then Return False
        If m1.IsGenericMethodDefinition Then Return False

        If CompareNameOrdinal(m1.Name, Name) = False Then Return False

        If Helper.CompareType(m1.ReturnType, ReturnType) = False Then Return False

        Dim p1 As ParameterInfo()
        p1 = m1.GetParameters()
        If p1.Length <> 1 Then Return False

        If Helper.CompareType(p1(0).ParameterType, ParameterType) = False Then Return False

        Return True
    End Function

    Public Function IsConstantMethod(ByVal Method As MethodInfo, ByVal Parameter As Object, ByRef Result As Object) As Boolean
        If Method.MemberType <> MemberTypes.Method Then Return False
        If Not CompareNameOrdinal(Method.DeclaringType.Namespace, "Microsoft.VisualBasic") Then Return False
        If Not CompareNameOrdinal(Method.DeclaringType.Name, "Strings") Then Return False

#If EXTENDEDDEBUG Then
        Compiler.Report.WriteLine("IsConstantMethod: " & Method.Name & ", parameter=" & Parameter.ToString & ", parameter.gettype=" & Parameter.GetType.Name)
#End If
        Dim isConstant As Boolean
        If IsMethod(Method, "Chr", Compiler.TypeCache.System_Int32, Compiler.TypeCache.System_Char) Then
            If TypeOf Parameter Is Integer = False Then Return False
            Dim intParam As Integer = CInt(Parameter)
            'CHECK: Documentation says <= 128, vbc says < 128.
            isConstant = intParam >= 0 AndAlso intParam < 128
            If isConstant Then Result = Microsoft.VisualBasic.Strings.Chr(intParam)
        ElseIf IsMethod(Method, "ChrW", Compiler.TypeCache.System_Int32, Compiler.TypeCache.System_Char) Then
            Helper.Assert(TypeOf Parameter Is Integer)
            isConstant = True
            Result = Microsoft.VisualBasic.Strings.ChrW(CInt(Parameter))
        ElseIf IsMethod(Method, "Asc", Compiler.TypeCache.System_Char, Compiler.TypeCache.System_Int32) Then
            isConstant = TypeOf Parameter Is Char
            If isConstant Then Result = Microsoft.VisualBasic.Asc(CChar(Parameter))
        ElseIf IsMethod(Method, "AscW", Compiler.TypeCache.System_Char, Compiler.TypeCache.System_Int32) Then
            isConstant = TypeOf Parameter Is Char
            If isConstant Then Result = Microsoft.VisualBasic.AscW(CChar(Parameter))
        ElseIf IsMethod(Method, "Asc", Compiler.TypeCache.System_String, Compiler.TypeCache.System_Int32) Then
            isConstant = TypeOf Parameter Is String AndAlso CStr(Parameter) <> ""
            If isConstant Then Result = Microsoft.VisualBasic.Asc(CStr(Parameter))
        ElseIf IsMethod(Method, "AscW", Compiler.TypeCache.System_String, Compiler.TypeCache.System_Int32) Then
            isConstant = TypeOf Parameter Is String AndAlso CStr(Parameter) <> ""
            If isConstant Then Result = Microsoft.VisualBasic.AscW(CStr(Parameter))
        Else
            Return False
        End If

        Return isConstant
    End Function
#End Region

    Shared Function FilterCustomAttributes(ByVal attributeType As Type, ByVal Inherit As Boolean, ByVal i As IAttributableDeclaration) As Object()
        Dim result As New Generic.List(Of Object)

        Helper.Assert(i IsNot Nothing)

        Dim attribs() As Attribute = i.CustomAttributes.ToArray
        For Each a As Attribute In attribs
            If attributeType Is Nothing OrElse attributeType.IsAssignableFrom(a.AttributeType) Then
                result.Add(a.AttributeInstance)
            End If
        Next

        Dim tD As TypeDescriptor
        If Inherit Then
            Dim base As Type
            Dim baseDecl As TypeDescriptor

            tD = TryCast(i, TypeDescriptor)

            If tD IsNot Nothing Then
                base = DirectCast(i, TypeDescriptor).BaseType
                baseDecl = TryCast(base, TypeDescriptor)

                If baseDecl IsNot Nothing Then
                    result.AddRange(FilterCustomAttributes(attributeType, Inherit, baseDecl.Declaration))
                ElseIf base IsNot Nothing Then
                    result.AddRange(base.GetCustomAttributes(attributeType, Inherit))
                End If
            End If
        End If

        Return result.ToArray
    End Function

    Shared Function IsOnMS() As Boolean
        Return Not IsOnMono()
    End Function

    Shared Function IsOnMono() As Boolean
        Dim t As Type = GetType(Integer)

        If t.GetType().ToString = "System.MonoType" Then
            Return True
        Else
            Return False
        End If
    End Function

    Shared Function VerifyValueClassification(ByRef Expression As Expression, ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True
        If Expression.Classification.IsValueClassification Then
            result = True
        ElseIf Expression.Classification.CanBeValueClassification Then
            Expression = Expression.ReclassifyToValueExpression
            result = Expression.ResolveExpression(Info) AndAlso result
        Else
            Helper.AddError(Expression)
            result = False
        End If
        Return result
    End Function

    Shared Function IsReflectionType(ByVal Type As Type) As Boolean
        Dim typesTypename As String = Type.GetType.Name
        Dim result As Boolean

        result = typesTypename = "TypeBuilder" OrElse typesTypename = "TypeBuilderInstantiation" OrElse typesTypename = "SymbolType"

#If DEBUG Then
        Helper.Assert(result = (Type.GetType.Namespace = "System.Reflection.Emit"), Type.GetType.FullName)
#End If

        Return result
    End Function

    Shared Function IsReflectionMember(ByVal Context As BaseObject, ByVal Member As MemberInfo) As Boolean
        Dim result As Boolean
        If TypeOf Member Is MethodDescriptor Then Return False
        If TypeOf Member Is FieldDescriptor Then Return False
        If TypeOf Member Is ConstructorDescriptor Then Return False
        If TypeOf Member Is EventDescriptor Then Return False
        If TypeOf Member Is TypeDescriptor Then Return False
        If TypeOf Member Is PropertyDescriptor Then Return False

        If Member.DeclaringType IsNot Nothing Then
            result = IsReflectionType(Member.DeclaringType)
        ElseIf Member.MemberType = MemberTypes.TypeInfo OrElse Member.MemberType = MemberTypes.NestedType Then
            result = IsReflectionType(DirectCast(Member, Type))
        Else
            Context.Compiler.Report.ShowMessage(Messages.VBNC99997, Context.Location)
        End If
        Return result
    End Function

    Shared Function IsReflectionMember(ByVal Context As BaseObject, ByVal Members() As MemberInfo) As Boolean
        If Members Is Nothing Then Return True
        If Members.Length = 0 Then Return True

        For Each m As MemberInfo In Members
            If IsReflectionMember(context, m) = False Then Return False
        Next
        Return True
    End Function

    Shared Function IsEmittableMember(ByVal Member As MemberInfo) As Boolean
        Dim result As Boolean

        If Member Is Nothing Then Return True
        result = Member.GetType.Namespace.StartsWith("System")

        Return result
    End Function

    Shared Function IsEmittableMember(ByVal Members() As MemberInfo) As Boolean
        If Members Is Nothing Then Return True
        If Members.Length = 0 Then Return True

        For Each m As MemberInfo In Members
            If IsEmittableMember(m) = False Then Return False
        Next
        Return True
    End Function

    Shared Function GetBaseMembers(ByVal Compiler As Compiler, ByVal Type As Type) As MemberInfo() ' Generic.List(Of MemberInfo)
        Dim result As New Generic.List(Of MemberInfo)
#If EXTENDEDDEBUG Then
        Compiler.Report.WriteLine("Getting base members for type " & Type.FullName)
#End If
        If Type.IsInterface Then
            Dim ifaces() As Type
            ifaces = Type.GetInterfaces()
            For Each iface As Type In ifaces
                result.AddRange(iface.GetMembers(Helper.ALLMEMBERS))
            Next
            'Remove duplicates (might happen since interfaces can have multiple bases)
            Dim tmp As New Generic.List(Of MemberInfo)
            For Each item As MemberInfo In result
                If tmp.Contains(item) = False Then tmp.Add(item)
            Next
            result = tmp
        ElseIf Type.BaseType IsNot Nothing Then
            result.AddRange(Type.BaseType.GetMembers(Helper.ALLMEMBERS))
        End If

        Return result.ToArray
    End Function

    ''' <summary>
    ''' Returns an array of the types of the method info. DEBUG METHOD!!
    ''' </summary>
    ''' <param name="method"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function GetParameterTypes(ByVal Context As BaseObject, ByVal method As MethodInfo) As Type()
        Dim Compiler As Compiler = Context.Compiler
        Dim result As Type()
        Dim builder As MethodBuilder = TryCast(method, MethodBuilder)
        If builder IsNot Nothing Then
            Dim reflectableInfo As MethodInfo = TryCast(Compiler.TypeManager.GetRegisteredMember(Context, builder), MethodInfo)

            If reflectableInfo IsNot Nothing Then
                result = GetParameterTypes(reflectableInfo.GetParameters)
            Else
                Helper.Assert(False)
                result = CType(GetType(MethodBuilder).GetField("m_parameterTypes", BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.Public).GetValue(builder), Type())
            End If
        Else
            result = GetParameterTypes(method.GetParameters())
        End If
        Return result
    End Function

    ''' <summary>
    ''' Returns an array of the types of the method info. DEBUG METHOD!!
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function GetParameterTypes(ByVal Context As BaseObject, ByVal ctor As ConstructorInfo) As Type()
        Dim Compiler As Compiler = Context.Compiler
        Dim result As Type()
        Dim builder As ConstructorBuilder = TryCast(ctor, ConstructorBuilder)
        If builder IsNot Nothing Then
            Dim tmp As ConstructorInfo = TryCast(Compiler.TypeManager.GetRegisteredMember(context, builder), ConstructorInfo)
            Dim method As MethodBuilder
            If tmp IsNot Nothing Then
                result = GetParameterTypes(tmp.GetParameters)
            Else
                Helper.Assert(False)
                method = CType(GetType(ConstructorBuilder).GetField("m_methodBuilder", BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.Public).GetValue(builder), MethodBuilder)
                result = GetParameterTypes(Context, method)
            End If
        Else
            result = GetParameterTypes(ctor.GetParameters())
        End If
        Return result
    End Function

    Shared Function GetGenericParameterConstraints(ByVal Context As BaseObject, ByVal Type As Type) As Type()
        If Type.IsGenericParameter = False Then Throw New InternalException("")
        If TypeOf Type Is GenericTypeParameterBuilder Then
            Context.Compiler.Report.ShowMessage(Messages.VBNC99997, Context.Location)
            Return New Type() {}
        Else
            Return Type.GetGenericParameterConstraints
        End If
    End Function

    Shared Function IsAssembly(ByVal Context As BaseObject, ByVal member As MemberInfo) As Boolean
        Dim mi As MethodInfo = TryCast(member, MethodInfo)
        If mi IsNot Nothing Then
            Return mi.IsAssembly
        Else
            Dim ci As ConstructorInfo = TryCast(member, ConstructorInfo)
            If ci IsNot Nothing Then
                Return ci.IsAssembly
            Else
                Dim fi As FieldInfo = TryCast(member, FieldInfo)
                If fi IsNot Nothing Then
                    Return fi.IsAssembly
                Else
                    Context.Compiler.Report.ShowMessage(Messages.VBNC99997, context.Location)
                End If
            End If
        End If
    End Function

    Shared Function GetNames(ByVal List As IEnumerable) As String()
        Dim result As New Generic.List(Of String)
        For Each item As INameable In List
            result.Add(item.Name)
        Next
        Return result.ToArray
    End Function

    Shared Function GetTypeCode(ByVal Compiler As Compiler, ByVal Type As TypeDescriptor) As TypeCode
        If Helper.IsEnum(Compiler, Type) Then
            Return GetTypeCode(Compiler, Type.GetElementType)
        Else
            Return TypeCode.Object
        End If
    End Function

    ''' <summary>
    ''' Compares two vb-names (case-insensitive)
    ''' </summary>
    ''' <param name="Value1"></param>
    ''' <param name="Value2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function CompareName(ByVal Value1 As String, ByVal Value2 As String) As Boolean
        Helper.Assert(Value1 IsNot Nothing)
        Helper.Assert(Value2 IsNot Nothing)
        Return String.Equals(Value1, Value2, StringComparison.OrdinalIgnoreCase)
    End Function

    Shared Function CompareNameStart(ByVal Whole As String, ByVal Start As String) As Boolean
        Return String.Compare(Whole, 0, Start, 0, Start.Length, StringComparison.OrdinalIgnoreCase) = 0
    End Function

    ''' <summary>
    ''' Compares two strings.
    ''' </summary>
    ''' <param name="Value1"></param>
    ''' <param name="Value2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function CompareName(ByVal Value1 As String, ByVal Value2 As String, ByVal Ordinal As Boolean) As Boolean
        If Ordinal Then
            Return CompareNameOrdinal(Value1, Value2)
        Else
            Return CompareName(Value1, Value2)
        End If
    End Function

    ''' <summary>
    ''' Compares two strings.
    ''' </summary>
    ''' <param name="Value1"></param>
    ''' <param name="Value2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function CompareNameOrdinal(ByVal Value1 As String, ByVal Value2 As String) As Boolean
        Helper.Assert(Value1 IsNot Nothing)
        Helper.Assert(Value2 IsNot Nothing)
        Return String.Equals(Value1, Value2, System.StringComparison.Ordinal)
    End Function

    Shared Function GetTypeCode(ByVal Compiler As Compiler, ByVal Type As Type) As TypeCode
        Dim tD As TypeDescriptor = TryCast(Type, TypeDescriptor)
        If tD Is Nothing Then
            Return System.Type.GetTypeCode(Type)
        Else
            Return GetTypeCode(Compiler, tD)
        End If
    End Function

    Shared Function IsTypeDeclaration(ByVal first As Object) As Boolean
        Return TypeOf first Is TypeDescriptor OrElse TypeOf first Is IType OrElse TypeOf first Is Type
    End Function

    Shared Function IsFieldDeclaration(ByVal first As Object) As Boolean
        Return TypeOf first Is VariableDeclaration OrElse TypeOf first Is FieldInfo
    End Function

    ''' <summary>
    ''' Intrinsic type: all basic types and System.Object.
    ''' </summary>
    ''' <param name="Compiler"></param>
    ''' <param name="Type"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function IsIntrinsicType(ByVal Compiler As Compiler, ByVal Type As Type) As Boolean
        Dim tC As TypeCode = GetTypeCode(Compiler, Type)

        If tC = TypeCode.Object Then
            Return Helper.CompareType(Type, Compiler.TypeCache.System_Object)
        Else
            Return True
        End If
    End Function

    Shared Function IsInterface(ByVal Context As BaseObject, ByVal Type As Type) As Boolean
        Dim tmpTP As Type
        Dim Compiler As Compiler = Context.Compiler

        If Type.GetType.Name = "SymbolType" Then Return Type.IsInterface

        tmpTP = Compiler.TypeManager.GetRegisteredType(Type)

        If TypeOf Type Is GenericTypeParameterBuilder Then
            Return False
        ElseIf TypeOf tmpTP Is TypeParameterDescriptor Then
            Return False
        ElseIf tmpTP.IsByRef Then
            Return False
        ElseIf TypeOf Type Is Type Then
            Return Compiler.TypeManager.GetRegisteredType(Type).IsInterface
        Else
            Context.Compiler.Report.ShowMessage(Messages.VBNC99997, Context.Location)
        End If
    End Function

    Shared Function IsEnum(ByVal Compiler As Compiler, ByVal Type As Type) As Boolean
        If TypeOf Type Is TypeBuilder Then
            Return Type.IsEnum
        ElseIf TypeOf Type Is TypeParameterDescriptor Then
            Return False
        ElseIf TypeOf Type Is TypeDescriptor Then
            Return Type.IsEnum
        End If

        'Dim FullName As String = Type.GetType.FullName
        'If FullName = "System.Type" Then
        'Return Type.IsEnum
        'Else
        If Type.GetType Is Compiler.TypeCache.System_Reflection_Emit_TypeBuilderInstantiation Then
            'ElseIf FullName.Contains("TypeBuilderInstantiation") Then
            Return False
        ElseIf Type.GetType Is Compiler.TypeCache.System_RuntimeType Then
            'ElseIf FullName.Contains("RuntimeType") Then
            Return Type.IsEnum
        ElseIf Type.GetType Is Compiler.TypeCache.System_Reflection_Emit_SymbolType Then
            'ElseIf FullName.Contains("SymbolType") Then
            Return False
        ElseIf Type.GetType.Namespace = "System.Reflection.Emit" Then
            Return False
        ElseIf TypeOf Type Is TypeParameterDescriptor Then
            Return False
        Else
            Return Type.IsEnum
            'Helper.NotImplementedYet("IsEnum of type '" & Type.GetType.FullName & "'")
        End If
    End Function

    Shared Function IsEnumFieldDeclaration(ByVal first As Object) As Boolean
        If TypeOf first Is EnumMemberDeclaration Then Return True
        Dim fld As FieldInfo = TryCast(first, FieldInfo)
        Return fld IsNot Nothing AndAlso fld.DeclaringType.IsEnum
    End Function

    Shared Function IsEventDeclaration(ByVal first As Object) As Boolean
        Return TypeOf first Is EventInfo
    End Function

    Shared Function IsPropertyDeclaration(ByVal first As Object) As Boolean
        Return TypeOf first Is RegularPropertyDeclaration OrElse TypeOf first Is PropertyInfo OrElse TypeOf first Is PropertyDeclaration
    End Function

    Shared Function IsMethodDeclaration(ByVal first As Object) As Boolean
        Return TypeOf first Is SubDeclaration OrElse TypeOf first Is FunctionDeclaration OrElse TypeOf first Is IMethod OrElse TypeOf first Is MethodInfo
    End Function

    ''' <summary>
    ''' Returns all the members in the types with the specified name.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function GetMembersOfTypes(ByVal Compiler As Compiler, ByVal Types As TypeDictionary, ByVal Name As String) As Generic.List(Of MemberInfo)
        Dim result As New Generic.List(Of MemberInfo)
        For Each type As Type In Types.Values
            Dim members As Generic.List(Of MemberInfo)
            members = Compiler.TypeManager.GetCache(type).LookupFlattenedMembers(Name)
            If members IsNot Nothing Then result.AddRange(members)
        Next
        Return result
    End Function

    ''' <summary>
    ''' Returns all the members in the types with the specified name.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function GetMembersOfTypes(ByVal Compiler As Compiler, ByVal Types As TypeList, ByVal Name As String) As Generic.List(Of MemberInfo)
        Dim result As New Generic.List(Of MemberInfo)
        For Each type As Type In Types
            Dim members As Generic.List(Of MemberInfo)
            members = Compiler.TypeManager.GetCache(type).LookupFlattenedMembers(Name)
            If members IsNot Nothing Then result.AddRange(members)
        Next
        Return result
    End Function

    Shared Function GetInstanceConstructors(ByVal type As Type) As Generic.List(Of MemberInfo)
        Dim result As New Generic.List(Of MemberInfo)

        result.AddRange(type.GetConstructors(BindingFlags.Instance Or BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.DeclaredOnly))

        Return result
    End Function

    ''' <summary>
    ''' Removes private members if they are from an external assembly.
    ''' </summary>
    ''' <param name="Members"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function FilterExternalInaccessible(ByVal Compiler As Compiler, ByVal Members As Generic.List(Of MemberInfo)) As Generic.List(Of MemberInfo)
        Dim result As New Generic.List(Of MemberInfo)

        For i As Integer = 0 To Members.Count - 1
            Dim member As MemberInfo = Members(i)
            If (IsPrivate(member) OrElse IsFriend(member)) AndAlso Compiler.Assembly.IsDefinedHere(member.DeclaringType) = False Then
                Continue For
            End If
            result.Add(member)
        Next

        Return result
    End Function

    Shared Function IsProtectedFriend(ByVal Member As MemberInfo) As Boolean
        Helper.Assert(Member IsNot Nothing)
        Select Case Member.MemberType
            Case MemberTypes.Constructor
                Dim ctor As ConstructorInfo = DirectCast(Member, ConstructorInfo)
                Return ctor.IsFamilyOrAssembly
            Case MemberTypes.Event
                Dim eventM As EventInfo = DirectCast(Member, EventInfo)
                Return CBool(Helper.GetEventAccess(eventM) = MethodAttributes.FamORAssem)
            Case MemberTypes.Field
                Dim field As FieldInfo = DirectCast(Member, FieldInfo)
                Return field.IsFamilyOrAssembly
            Case MemberTypes.NestedType
                Dim tp As Type = DirectCast(Member, Type)
                Return tp.IsNestedFamORAssem
            Case MemberTypes.Method
                Dim method As MethodInfo = DirectCast(Member, MethodInfo)
                Return method.IsFamilyOrAssembly OrElse method.IsFamily
            Case MemberTypes.Property
                Dim propM As PropertyInfo = DirectCast(Member, PropertyInfo)
                Return Helper.GetPropertyAccess(propM) = MethodAttributes.FamORAssem
            Case MemberTypes.TypeInfo
                Dim tp As Type = DirectCast(Member, Type)
                Return tp.IsNotPublic OrElse tp.IsNested AndAlso tp.IsNestedFamORAssem
            Case Else
                Throw New InternalException("")
        End Select
    End Function

    Shared Function IsProtectedOrProtectedFriend(ByVal Member As MemberInfo) As Boolean
        Helper.Assert(Member IsNot Nothing)
        Select Case Member.MemberType
            Case MemberTypes.Constructor
                Dim ctor As ConstructorInfo = DirectCast(Member, ConstructorInfo)
                Return ctor.IsFamily OrElse ctor.IsFamilyOrAssembly
            Case MemberTypes.Event
                Dim eventM As EventInfo = DirectCast(Member, EventInfo)
                Dim access As MethodAttributes = Helper.GetEventAccess(eventM)
                Return access = MethodAttributes.Family OrElse access = MethodAttributes.FamORAssem
            Case MemberTypes.Field
                Dim fieldI As FieldInfo = DirectCast(Member, FieldInfo)
                Return fieldI.IsFamily OrElse fieldI.IsFamilyOrAssembly
            Case MemberTypes.NestedType
                Dim type As Type = DirectCast(Member, Type)
                Return type.IsNestedFamily OrElse type.IsNestedFamORAssem
            Case MemberTypes.Method
                Dim method As MethodInfo = DirectCast(Member, MethodInfo)
                Return method.IsFamily OrElse method.IsFamilyOrAssembly
            Case MemberTypes.Property
                Dim propM As PropertyInfo = DirectCast(Member, PropertyInfo)
                Dim access As MethodAttributes = Helper.GetPropertyAccess(propM)
                Return access = MethodAttributes.Family OrElse access = MethodAttributes.FamORAssem
            Case MemberTypes.TypeInfo
                Dim tp As Type = DirectCast(Member, Type)
                Return tp.IsNotPublic OrElse tp.IsNested AndAlso (tp.IsNestedFamily OrElse tp.IsNestedFamORAssem)
            Case Else
                Throw New InternalException("")
        End Select
    End Function

    Shared Function IsFriendOrProtectedFriend(ByVal Member As MemberInfo) As Boolean
        Helper.Assert(Member IsNot Nothing)
        Select Case Member.MemberType
            Case MemberTypes.Constructor
                Dim ctor As ConstructorInfo = DirectCast(Member, ConstructorInfo)
                Return ctor.IsAssembly OrElse ctor.IsFamilyOrAssembly
            Case MemberTypes.Event
                Dim eventM As EventInfo = DirectCast(Member, EventInfo)
                Dim access As MethodAttributes = Helper.GetEventAccess(eventM)
                Return access = MethodAttributes.Assembly OrElse access = MethodAttributes.FamORAssem
            Case MemberTypes.Field
                Dim fieldI As FieldInfo = DirectCast(Member, FieldInfo)
                Return fieldI.IsAssembly OrElse fieldI.IsFamilyOrAssembly
            Case MemberTypes.NestedType
                Dim type As Type = DirectCast(Member, Type)
                Return type.IsNestedAssembly OrElse type.IsNestedFamORAssem
            Case MemberTypes.Method
                Dim method As MethodInfo = DirectCast(Member, MethodInfo)
                Return method.IsAssembly OrElse method.IsFamilyOrAssembly
            Case MemberTypes.Property
                Dim propM As PropertyInfo = DirectCast(Member, PropertyInfo)
                Dim access As MethodAttributes = Helper.GetPropertyAccess(propM)
                Return access = MethodAttributes.Assembly OrElse access = MethodAttributes.FamORAssem
            Case MemberTypes.TypeInfo
                Dim tp As Type = DirectCast(Member, Type)
                Return tp.IsNotPublic OrElse tp.IsNested AndAlso (tp.IsNestedAssembly OrElse tp.IsNestedFamORAssem)
            Case Else
                Throw New InternalException("")
        End Select
    End Function

    ''' <summary>
    ''' Checks if the member is Protected (not Protected Friend)
    ''' </summary>
    ''' <param name="Member"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function IsProtected(ByVal Member As MemberInfo) As Boolean
        Helper.Assert(Member IsNot Nothing)
        Select Case Member.MemberType
            Case MemberTypes.Constructor
                Dim ctor As ConstructorInfo = DirectCast(Member, ConstructorInfo)
                Return ctor.IsFamily
            Case MemberTypes.Event
                Dim eventM As EventInfo = DirectCast(Member, EventInfo)
                Dim access As MethodAttributes = Helper.GetEventAccess(eventM)
                Return access = MethodAttributes.Family
            Case MemberTypes.Field
                Dim fieldI As FieldInfo = DirectCast(Member, FieldInfo)
                Return fieldI.IsFamily
            Case MemberTypes.NestedType
                Dim type As Type = DirectCast(Member, Type)
                Return type.IsNestedFamily
            Case MemberTypes.Method
                Dim method As MethodInfo = DirectCast(Member, MethodInfo)
                Return method.IsFamily
            Case MemberTypes.Property
                Dim propM As PropertyInfo = DirectCast(Member, PropertyInfo)
                Dim access As MethodAttributes = Helper.GetPropertyAccess(propM)
                Return access = MethodAttributes.Family
            Case MemberTypes.TypeInfo
                Dim tp As Type = DirectCast(Member, Type)
                Return tp.IsNotPublic OrElse tp.IsNested AndAlso (tp.IsNestedFamily)
            Case Else
                Throw New InternalException("")
        End Select
    End Function

    ''' <summary>
    ''' Checks if the member is Friend (not Protected Friend)
    ''' </summary>
    ''' <param name="Member"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function IsFriend(ByVal Member As MemberInfo) As Boolean
        Helper.Assert(Member IsNot Nothing)
        Select Case Member.MemberType
            Case MemberTypes.Constructor
                Dim ctor As ConstructorInfo = DirectCast(Member, ConstructorInfo)
                Return ctor.IsAssembly
            Case MemberTypes.Event
                Dim eventM As EventInfo = DirectCast(Member, EventInfo)
                Return CBool(Helper.GetEventAccess(eventM) = MethodAttributes.Assembly)
            Case MemberTypes.Field
                Dim fieldI As FieldInfo = DirectCast(Member, FieldInfo)
                Return fieldI.IsAssembly
            Case MemberTypes.NestedType
                Dim type As Type = DirectCast(Member, Type)
                Return type.IsNestedAssembly
            Case MemberTypes.Method
                Dim method As MethodInfo = DirectCast(Member, MethodInfo)
                Return method.IsAssembly
            Case MemberTypes.Property
                Dim propM As PropertyInfo = DirectCast(Member, PropertyInfo)
                Return Helper.GetPropertyAccess(propM) = MethodAttributes.Assembly
            Case MemberTypes.TypeInfo
                Dim tp As Type = DirectCast(Member, Type)
                Return tp.IsNotPublic OrElse tp.IsNested AndAlso tp.IsNestedAssembly
            Case Else
                Throw New InternalException("")
        End Select
    End Function

    Shared Function IsPrivate(ByVal Member As MemberInfo) As Boolean
        Select Case Member.MemberType
            Case MemberTypes.Constructor
                Return DirectCast(Member, ConstructorInfo).IsPrivate
            Case MemberTypes.Event
                Dim eventM As EventInfo = DirectCast(Member, EventInfo)
                Return CBool(Helper.GetEventAccess(eventM) = MethodAttributes.Private)
            Case MemberTypes.Field
                Return DirectCast(Member, FieldInfo).IsPrivate
            Case MemberTypes.NestedType
                Return DirectCast(Member, Type).IsNestedPrivate
            Case MemberTypes.Method
                Return DirectCast(Member, MethodInfo).IsPrivate
            Case MemberTypes.Property
                Dim pInfo As PropertyInfo = DirectCast(Member, PropertyInfo)
                Return Helper.GetPropertyAccess(pInfo) = MethodAttributes.Private
            Case MemberTypes.TypeInfo
                Dim tp As Type = DirectCast(Member, Type)
                Return tp.IsNested AndAlso tp.IsNestedPrivate
            Case Else
                Throw New InternalException("")
        End Select
    End Function

    Shared Function IsPublic(ByVal Member As MemberInfo) As Boolean
        Select Case Member.MemberType
            Case MemberTypes.Constructor
                Return DirectCast(Member, ConstructorInfo).IsPublic
            Case MemberTypes.Event
                Dim eventM As EventInfo = DirectCast(Member, EventInfo)
                Return CBool(Helper.GetEventAccess(eventM) = MethodAttributes.Public)
            Case MemberTypes.Field
                Return DirectCast(Member, FieldInfo).IsPublic
            Case MemberTypes.Method
                Return DirectCast(Member, MethodInfo).IsPublic
            Case MemberTypes.Property
                Dim pInfo As PropertyInfo = DirectCast(Member, PropertyInfo)
                Return Helper.GetPropertyAccess(pInfo) = MethodAttributes.Public
            Case MemberTypes.TypeInfo, MemberTypes.NestedType
                Dim tp As Type = DirectCast(Member, Type)
                Return tp.IsPublic OrElse (tp.IsNested AndAlso tp.IsNestedPublic)
            Case Else
                Throw New InternalException("")
        End Select
    End Function

    Shared Function FilterByTypeArguments(ByVal Members As Generic.List(Of MemberInfo), ByVal TypeArguments As TypeArgumentList) As Generic.List(Of MemberInfo)
        Dim result As New Generic.List(Of MemberInfo)
        Dim argCount As Integer

        If TypeArguments IsNot Nothing Then argCount = TypeArguments.Count

        For i As Integer = 0 To Members.Count - 1
            Dim member As MemberInfo = Members(i)

            Dim minfo As MethodInfo = TryCast(member, MethodInfo)
            If minfo IsNot Nothing Then
                If minfo.GetGenericArguments.Length = argCount Then
                    If argCount > 0 Then
                        member = TypeArguments.Parent.Compiler.TypeManager.MakeGenericMethod(TypeArguments.Parent, minfo, minfo.GetGenericArguments, TypeArguments.AsTypeArray)
                        result.Add(member)
                    Else
                        result.Add(member)
                    End If
                Else
                    'Helper.StopIfDebugging()
                End If
            Else
                result.Add(member)
            End If
        Next

        Return result
    End Function

    <Obsolete()> Shared Function FilterByName(ByVal members() As MemberInfo, ByVal Name As String) As Generic.List(Of MemberInfo)
        Dim result As New Generic.List(Of MemberInfo)
        Helper.AssertNotNothing(members)
        For Each member As MemberInfo In members
            If Helper.CompareName(member.Name, Name) Then result.Add(member)
        Next
        Return result
    End Function

    Shared Function FilterByName(ByVal members() As PropertyInfo, ByVal Name As String) As Generic.List(Of PropertyInfo)
        Dim result As New Generic.List(Of PropertyInfo)
        Helper.AssertNotNothing(members)
        For Each member As PropertyInfo In members
            If Helper.CompareName(member.Name, Name) Then result.Add(member)
        Next
        Return result
    End Function

    Shared Function FilterByName2(ByVal Members As Generic.List(Of MemberInfo), ByVal Name As String) As Generic.List(Of MemberInfo)
        Dim result As New Generic.List(Of MemberInfo)
        For Each member As MemberInfo In Members
            If Helper.CompareName(member.Name, Name) Then result.Add(member)
        Next
        Return result
    End Function

    Shared Function FilterByName(ByVal Context As BaseObject, ByVal collection As ICollection, ByVal Name As String) As ArrayList
        Dim result As New ArrayList
        Dim tmpname As String = ""
        For Each obj As Object In collection
            If TypeOf obj Is INameable Then
                tmpname = DirectCast(obj, INameable).Name
            ElseIf TypeOf obj Is MemberInfo Then
                tmpname = DirectCast(obj, MemberInfo).Name
            Else
                Context.Compiler.Report.ShowMessage(Messages.VBNC99997, Context.Location)
            End If
            If Helper.CompareName(Name, tmpname) Then result.Add(obj)
        Next

        Return result
    End Function

    Shared Function FilterByName(ByVal collection As Generic.List(Of Type), ByVal Name As String) As Generic.List(Of Type)
        Dim result As New Generic.List(Of Type)
        Dim tmpname As String = ""
        For Each obj As Type In collection
            If Helper.CompareName(Name, obj.Name) Then result.Add(obj)
        Next

        Return result
    End Function

    Shared Sub FilterByName(ByVal collection As Generic.List(Of Type), ByVal Name As String, ByVal result As Generic.List(Of MemberInfo))
        For Each obj As Type In collection
            If Helper.CompareName(Name, obj.Name) Then result.Add(obj)
        Next
    End Sub

    Shared Sub FilterByName(ByVal collection As TypeDictionary, ByVal Name As String, ByVal result As Generic.List(Of MemberInfo))
        For Each obj As Type In collection.Values
            If Helper.CompareName(Name, obj.Name) Then result.Add(obj)
        Next
    End Sub

    Shared Function FilterByName(ByVal Types As TypeList, ByVal Name As String) As TypeList
        Dim result As New TypeList
        For Each obj As Type In Types
            If Helper.CompareName(Name, obj.Name) Then result.Add(obj)
        Next
        Return result
    End Function

    Shared Function FilterByName(ByVal Types As TypeDictionary, ByVal Name As String) As Type
        If Types.ContainsKey(Name) Then
            Return Types(Name)
        Else
            Return Nothing
        End If
    End Function

    '''' <summary>
    '''' Returns a list of type descriptors that only are modules.
    '''' </summary>
    '''' <param name="Types"></param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    '<Obsolete()> Shared Function FilterToModules(ByVal Compiler As Compiler, ByVal Types As Generic.List(Of Type)) As Generic.List(Of Type)
    '    Dim result As New Generic.List(Of Type)
    '    For Each t As Type In Types
    '        If IsModule(Compiler, t) Then result.Add(t)
    '    Next
    '    Return result
    'End Function

    ''' <summary>
    ''' Returns a list of type descriptors that only are modules.
    ''' </summary>
    ''' <param name="Types"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function FilterToModules(ByVal Compiler As Compiler, ByVal Types As TypeDictionary) As Generic.List(Of Type)
        Dim result As New Generic.List(Of Type)
        For Each t As Type In Types.Values
            If IsModule(Compiler, t) Then result.Add(t)
        Next
        Return result
    End Function

    ''' <summary>
    ''' Finds a non-private, non-shared constructor with no parameters in the array.
    ''' If nothing found, returns nothing.
    ''' </summary>
    ''' <param name="Constructors"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDefaultConstructor(ByVal Constructors() As ConstructorInfo) As ConstructorInfo
        For i As Integer = 0 To Constructors.GetUpperBound(0)
            If HasParameters(Constructors(i)) = False OrElse HasOnlyOptionalParameters(Constructors(i)) Then
                If Constructors(i).IsStatic = False AndAlso Constructors(i).IsPrivate = False Then
                    Return Constructors(i)
                End If
            End If
        Next
        Return Nothing
    End Function

    Function HasOnlyOptionalParameters(ByVal Constructor As ConstructorInfo) As Boolean
        Helper.Assert(HasParameters(Constructor))
        Return Constructor.GetParameters(0).IsOptional
    End Function

    ''' <summary>
    ''' Returns true if this constructor has any parameter, default or normal parameter.
    ''' </summary>
    ''' <param name="Constructor"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function HasParameters(ByVal Constructor As ConstructorInfo) As Boolean
        Return Constructor.GetParameters().Length > 0
    End Function

    ''' <summary>
    ''' Finds a public, non-shared constructor with no parameters of the type.
    ''' If nothing found, returns nothing.
    ''' </summary>
    ''' <param name="tp"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDefaultConstructor(ByVal tp As Type) As ConstructorInfo
        Return GetDefaultConstructor(GetConstructors(tp))
    End Function

    Function GetDefaultGenericConstructor(ByVal tn As ConstructedTypeName) As ConstructorInfo
        Dim result As ConstructorInfo
        Dim candidates() As ConstructorInfo

        Dim openconstructor As ConstructorInfo
        If tn.ResolvedType.GetType.Name = "TypeBuilderInstantiation" Then
            candidates = tn.OpenResolvedType.GetConstructors(BindingFlags.DeclaredOnly Or BindingFlags.Instance Or BindingFlags.Public Or BindingFlags.NonPublic)
            openconstructor = GetDefaultConstructor(candidates)
            result = TypeBuilder.GetConstructor(tn.ClosedResolvedType, openconstructor)
        Else
            candidates = tn.ClosedResolvedType.GetConstructors(BindingFlags.DeclaredOnly Or BindingFlags.Instance Or BindingFlags.Public Or BindingFlags.NonPublic)
            result = GetDefaultConstructor(candidates)
            ' result = New GenericConstructorDescriptor(tn, tn.ClosedResolvedType, result)
        End If

        Return result
    End Function

    ''' <summary>
    ''' Returns all the constructors of the type descriptor. (instance + static + public + nonpublic)
    ''' </summary>
    ''' <param name="tp"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetConstructors(ByVal tp As Type) As ConstructorInfo()
        Helper.Assert(tp IsNot Nothing)
        Helper.Assert(TypeOf tp Is TypeBuilder = False AndAlso tp.GetType.Name <> "TypeBuilderInstantiation")
        Return tp.GetConstructors(BindingFlags.Instance Or BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Static Or BindingFlags.DeclaredOnly)
    End Function

    Shared Function GetParameterTypes(ByVal Parameters As ParameterInfo()) As Type()
        Dim result() As Type
        Helper.Assert(Parameters IsNot Nothing)
        ReDim result(Parameters.Length - 1)
        For i As Integer = 0 To Parameters.GetUpperBound(0)
            result(i) = Parameters(i).ParameterType
        Next
        Return result
    End Function

    ''' <summary>
    ''' Checks if the specified type is a VB Module.
    ''' </summary>
    ''' <param name="type"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function IsModule(ByVal Compiler As Compiler, ByVal type As Type) As Boolean
        Dim result As Boolean
        If TypeOf type Is TypeDescriptor Then
            Return IsModule(Compiler, DirectCast(type, TypeDescriptor))
        ElseIf TypeOf Compiler.TypeCache.MS_VB_CS_StandardModuleAttribute Is TypeDescriptor Then
            'We're compiling the vbruntime, so no external type may be a module (we know that we're not referencing any external assemblies with modules)
            Return False
        ElseIf Compiler.TypeCache.MS_VB_CS_StandardModuleAttribute Is Nothing Then
            Return False
        Else
            result = type.IsClass AndAlso type.IsDefined(Compiler.TypeCache.MS_VB_CS_StandardModuleAttribute, False)
            Return result
        End If
    End Function

#If ENABLECECIL Then
    ''' <summary>
    ''' Checks if the specified type is a VB Module.
    ''' </summary>
    ''' <param name="type"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function IsModule(ByVal Compiler As Compiler, ByVal type As Mono.Cecil.TypeDefinition) As Boolean
        Dim result As Boolean
        'If TypeOf type Is TypeDescriptor Then
        '    Return IsModule(Compiler, DirectCast(type, TypeDescriptor))
        'Else
        result = type.IsClass AndAlso Compiler.CecilTypeCache.MS_VB_CS_StandardModuleAttribute IsNot Nothing AndAlso type.CustomAttributes.IsDefined(Compiler.CecilTypeCache.MS_VB_CS_StandardModuleAttribute)

        'Compiler.Report.WriteLine("IsModule: type=" & type.FullName & ", result=" & result.ToString)
        'If type.Name = "Constants" Then Stop
        Return result
        'End If
    End Function
#End If

    ''' <summary>
    ''' Checks if the specified type is a VB Module.
    ''' </summary>
    ''' <param name="type"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function IsModule(ByVal Compiler As Compiler, ByVal type As TypeDescriptor) As Boolean
        If type.Declaration IsNot Nothing Then
            Return type.Declaration.IsModule
        Else
            Return IsModule(Compiler, type.TypeInReflection)
        End If
    End Function

    Shared Function FilterByName(ByVal lst As Generic.List(Of TypeDescriptor), ByVal Name As String) As Generic.List(Of TypeDescriptor)
        Dim result As New Generic.List(Of TypeDescriptor)
        For Each t As TypeDescriptor In lst
            If Helper.CompareName(t.Name, Name) Then result.Add(t)
        Next
        Return result
    End Function

    '''' <summary>
    '''' Returns all members from the specified type.
    '''' Included: 
    '''' - all scopes for the compiling code, public and protected for external assemblies.
    '''' - instance and shared members.
    '''' - inherited members.
    '''' </summary>
    '''' <param name="Type"></param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    '<Obsolete()> Shared Function GetMembers(ByVal Compiler As Compiler, ByVal Type As Type) As MemberInfo()
    '    Static cache As New Generic.Dictionary(Of Type, Generic.List(Of MemberInfo))
    '    Dim result As Generic.List(Of MemberInfo)

    '    If TypeOf Type Is TypeDescriptor = False AndAlso cache.ContainsKey(Type) Then
    '        result = cache(Type)
    '    Else
    '        Dim reflectableType As Type
    '        reflectableType = Compiler.TypeManager.GetRegisteredType(Type)

    '        Dim memberCache As MemberCache
    '        If Compiler.TypeManager.MemberCache.ContainsKey(reflectableType) = False Then
    '            memberCache = New MemberCache(Compiler, reflectableType)
    '        Else
    '            memberCache = Compiler.TypeManager.MemberCache(reflectableType)
    '        End If

    '        Dim result2 As Generic.List(Of MemberInfo)
    '        result2 = memberCache.FlattenedCache.GetAllMembers

    '        'result = New Generic.List(Of MemberInfo)
    '        'result.AddRange(reflectableType.GetMembers(Helper.ALLNOBASEMEMBERS))

    '        ''RemoveShadowed(Compiler, result)

    '        'If reflectableType.BaseType IsNot Nothing Then
    '        '    AddMembers(Compiler, Type, result, GetMembers(Compiler, reflectableType.BaseType))
    '        'ElseIf reflectableType.IsGenericParameter = False AndAlso reflectableType.IsInterface Then
    '        '    Dim ifaces() As Type
    '        '    ifaces = reflectableType.GetInterfaces()
    '        '    For Each iface As Type In ifaces
    '        '        Helper.AddMembers(Compiler, reflectableType, result, iface.GetMembers(Helper.ALLMEMBERS))
    '        '    Next
    '        '    Helper.AddMembers(Compiler, reflectableType, result, Compiler.TypeCache.Object.GetMembers(Helper.ALLMEMBERS))
    '        'End If
    '        'result = Helper.FilterExternalInaccessible(Compiler, result)

    '        'Helper.Assert(result.Count <= result2.Count)

    '        result = result2

    '        If TypeOf Type Is TypeDescriptor = False Then cache.Add(Type, result)
    '    End If

    '    Return result.ToArray
    'End Function

    '''' <summary>
    '''' Gets all the members in the specified type with the specified name.
    '''' Returns Nothing if nothing is found.
    '''' </summary>
    '''' <param name="Type"></param>
    '''' <param name="Name"></param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    '<Obsolete()> Shared Function GetMembers(ByVal Compiler As Compiler, ByVal Type As Type, ByVal Name As String) As MemberInfo()
    '    Dim result As New Generic.List(Of MemberInfo)

    '    result.AddRange(GetMembers(Compiler, Type))
    '    result = Helper.FilterByName2(result, Name)

    '    Return result.ToArray
    'End Function

    ''' <summary>
    ''' Creates an integer array of the arguments.
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <param name="Arguments"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function EmitIntegerArray(ByVal Info As EmitInfo, ByVal Arguments As ArgumentList) As Boolean
        Dim result As Boolean = True

        Dim arrayType As Type = Info.Compiler.TypeCache.System_Int32_Array
        Dim elementType As Type = arrayType.GetElementType
        Dim tmpVar As LocalBuilder = Info.ILGen.DeclareLocal(arrayType)
        Dim elementInfo As EmitInfo = Info.Clone(Info.Context, True, False, elementType)

        'Create the array.
        ArrayCreationExpression.EmitArrayCreation(Info, arrayType, New Generic.List(Of Integer)(New Integer() {Arguments.Count}))

        'Save it into a temporary variable.
        Emitter.EmitStoreVariable(Info, tmpVar)

        'Store every element into its index in the array.
        For i As Integer = 0 To Arguments.Count - 1
            'Load the array variable.
            Emitter.EmitLoadVariable(Info, tmpVar)
            Emitter.EmitLoadI4Value(Info, i)
            'Load all the indices.
            result = Arguments(i).GenerateCode(elementInfo) AndAlso result
            'Store the element in the arry.
            Emitter.EmitStoreElement(elementInfo, elementType, arrayType)
            'Increment the indices.
        Next

        'Load the final array onto the stack.
        Emitter.EmitLoadVariable(Info, tmpVar)

        Return result
    End Function

    Shared Function EmitStoreArrayElement(ByVal Info As EmitInfo, ByVal ArrayVariable As Expression, ByVal Arguments As ArgumentList) As Boolean
        Dim result As Boolean = True
        Dim ArrayType As Type = ArrayVariable.ExpressionType
        Dim ElementType As Type = ArrayType.GetElementType
        Dim isNonPrimitiveValueType As Boolean = ElementType.IsPrimitive = False AndAlso ElementType.IsValueType
        Dim isArraySetValue As Boolean = ArrayType.GetArrayRank > 1
        Dim newValue As Expression = Info.RHSExpression

        Helper.Assert(newValue IsNot Nothing)
        Helper.Assert(newValue.Classification.IsValueClassification)

        result = ArrayVariable.GenerateCode(Info.Clone(Info.Context, True, False, ArrayType)) AndAlso result

        If isArraySetValue Then
            result = newValue.GenerateCode(Info.Clone(Info.Context, True, False, ElementType)) AndAlso result
            If ElementType.IsValueType Then
                Emitter.EmitBox(Info, ElementType)
            End If
            result = EmitIntegerArray(Info, Arguments) AndAlso result
            Emitter.EmitCallOrCallVirt(Info, Info.Compiler.TypeCache.System_Array__SetValue)
        Else
            Dim methodtypes As New Generic.List(Of Type)
            Dim elementInfo As EmitInfo = Info.Clone(Info.Context, True, False, Info.Compiler.TypeCache.System_Int32)
            For i As Integer = 0 To Arguments.Count - 1
                result = Arguments(i).GenerateCode(elementInfo) AndAlso result
                Emitter.EmitConversion(Arguments(i).Expression.ExpressionType, Info.Compiler.TypeCache.System_Int32, Info)
                methodtypes.Add(Info.Compiler.TypeCache.System_Int32)
            Next

            Dim rInfo As EmitInfo = Info.Clone(Info.Context, True, False, ElementType)
            methodtypes.Add(ElementType)

            If isNonPrimitiveValueType Then
                Emitter.EmitLoadElementAddress(Info, ElementType, ArrayType)
                result = Info.RHSExpression.Classification.GenerateCode(rInfo) AndAlso result
                Emitter.EmitStoreObject(Info, ElementType)
            Else
                result = Info.RHSExpression.Classification.GenerateCode(rInfo) AndAlso result
                Emitter.EmitStoreElement(Info, ElementType, ArrayType)
            End If
        End If
        Return result
    End Function

    Shared Function EmitLoadArrayElement(ByVal Info As EmitInfo, ByVal ArrayVariable As Expression, ByVal Arguments As ArgumentList) As Boolean
        Dim result As Boolean = True
        Dim ArrayType As Type = ArrayVariable.ExpressionType
        Dim ElementType As Type = ArrayType.GetElementType
        Dim isNonPrimitiveValueType As Boolean = ElementType.IsPrimitive = False AndAlso ElementType.IsValueType
        Dim isArrayGetValue As Boolean = ArrayType.GetArrayRank > 1

        result = ArrayVariable.GenerateCode(Info) AndAlso result

        If isArrayGetValue Then
            result = Arguments.GenerateCode(Info, Helper.CreateArray(Of Type)(Info.Compiler.TypeCache.System_Int32, Arguments.Length)) AndAlso result
            'result = EmitIntegerArray(Info, Arguments) AndAlso result
            Dim getMethod As MethodInfo
            getMethod = ArrayElementInitializer.GetGetMethod(Info.Compiler, ArrayType)
            Helper.Assert(getMethod IsNot Nothing, "getMethod for type " & ArrayType.FullName & " could not be found (" & ArrayType.GetType.Name & ")")
            Emitter.EmitCallVirt(Info, getMethod)
            'Emitter.EmitCallOrCallVirt(Info, Info.Compiler.TypeCache.Array_GetValue)
            'If ElementType.IsValueType Then
            '    Emitter.EmitUnbox(Info, ElementType)
            'Else
            '    Emitter.EmitCastClass(Info, Info.Compiler.TypeCache.Object, ElementType)
            'End If
        Else
            Dim elementInfo As EmitInfo = Info.Clone(Info.Context, True, False, Info.Compiler.TypeCache.System_Int32)
            Dim methodtypes(Arguments.Count - 1) As Type
            For i As Integer = 0 To Arguments.Count - 1
                result = Arguments(i).GenerateCode(elementInfo) AndAlso result
                Emitter.EmitConversion(Info.Stack.Peek, Info.Compiler.TypeCache.System_Int32, Info)
                methodtypes(i) = Info.Compiler.TypeCache.System_Int32
            Next

            If isNonPrimitiveValueType Then
                Emitter.EmitLoadElementAddress(Info, ElementType, ArrayType)
                Emitter.EmitLoadObject(Info, ElementType)
            Else
                Emitter.EmitLoadElement(Info, ArrayType)
            End If
        End If
        Return result
    End Function

    ''' <summary>
    ''' Emits the instanceexpression (if any), the arguments (if any), the optional arguments (if any) and then calls the method (virt or not).
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <param name="InstanceExpression"></param>
    ''' <param name="Arguments"></param>
    ''' <param name="Method"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function EmitArgumentsAndCallOrCallVirt(ByVal Info As EmitInfo, ByVal InstanceExpression As Expression, ByVal Arguments As ArgumentList, ByVal Method As MethodBase) As Boolean
        Dim result As Boolean = True
        Dim needsConstrained As Boolean
        Dim constrainedLocal As LocalBuilder = Nothing

        needsConstrained = InstanceExpression IsNot Nothing AndAlso InstanceExpression.ExpressionType.IsGenericParameter

        If InstanceExpression IsNot Nothing Then
            Dim ieDesiredType As Type
            Dim ieInfo As EmitInfo

            If needsConstrained Then
                ieDesiredType = InstanceExpression.ExpressionType
            Else
                ieDesiredType = Method.DeclaringType
                If ieDesiredType.IsValueType Then
                    ieDesiredType = Info.Compiler.TypeManager.MakeByRefType(CType(Info.Method, ParsedObject), ieDesiredType)
                End If
            End If

            ieInfo = Info.Clone(Info.Context, True, False, ieDesiredType)

            Dim derefExp As DeRefExpression = TryCast(InstanceExpression, DeRefExpression)
            If needsConstrained AndAlso derefExp IsNot Nothing Then
                result = derefExp.Expression.GenerateCode(Info.Clone(Info.Context, True, False, derefExp.Expression.ExpressionType)) AndAlso result
            Else
                Dim getRef As GetRefExpression = TryCast(InstanceExpression, GetRefExpression)
                If getRef IsNot Nothing AndAlso getRef.Expression.ExpressionType.IsValueType AndAlso Helper.CompareType(Method.DeclaringType, Info.Compiler.TypeCache.System_Object) Then
                    result = getRef.Expression.GenerateCode(ieInfo) AndAlso result
                    Emitter.EmitBox(Info, getRef.Expression.ExpressionType)
                Else
                    result = InstanceExpression.GenerateCode(ieInfo) AndAlso result
                End If

                If needsConstrained Then
                    constrainedLocal = Emitter.DeclareLocal(Info, InstanceExpression.ExpressionType)
                    Emitter.EmitStoreVariable(Info, constrainedLocal)
                    Emitter.EmitLoadVariableLocation(Info, constrainedLocal)
                End If
            End If

        End If

        Dim copyBacksA As Generic.List(Of LocalBuilder) = Nothing
        Dim copyBacksB As Generic.List(Of Expression) = Nothing

        If Arguments IsNot Nothing Then
            Dim methodParameters() As ParameterInfo
            methodParameters = Helper.GetParameters(Info.Compiler, Method)

            For i As Integer = 0 To methodParameters.Length - 1
                Dim arg As Argument
                Dim exp As Expression
                Dim local As LocalBuilder

                If methodParameters(i).ParameterType.IsByRef = False Then Continue For

                arg = Arguments.Arguments(i)
                exp = arg.Expression

                If exp Is Nothing Then Continue For
                If exp.Classification Is Nothing Then Continue For
                If exp.Classification.IsPropertyAccessClassification = False Then Continue For

                If copyBacksA Is Nothing Then
                    copyBacksA = New Generic.List(Of LocalBuilder)
                    copyBacksB = New Generic.List(Of Expression)
                End If
                local = Emitter.DeclareLocal(Info, methodParameters(i).ParameterType.GetElementType)
                copyBacksA.Add(local)
                If exp.Classification.AsPropertyAccess.Property.CanWrite = False Then
                    copyBacksB.Add(Nothing)
                Else
                    copyBacksB.Add(exp)
                End If

                result = arg.GenerateCode(Info, methodParameters(i)) AndAlso result
                Emitter.EmitStoreVariable(Info, local)
                arg.Expression = New LoadLocalExpression(arg, local)
            Next

            result = Arguments.GenerateCode(Info, methodParameters) AndAlso result
        End If

        If needsConstrained Then
            Emitter.EmitConstrainedCallVirt(Info, Method, InstanceExpression.ExpressionType)
        ElseIf InstanceExpression IsNot Nothing AndAlso (TypeOf InstanceExpression Is MyClassExpression OrElse TypeOf InstanceExpression Is MyBaseExpression) Then
            Emitter.EmitCall(Info, Method)
        Else
            Emitter.EmitCallOrCallVirt(Info, Method)
        End If

        If copyBacksA IsNot Nothing Then
            For i As Integer = 0 To copyBacksA.Count - 1
                Dim local As LocalBuilder = copyBacksA(i)
                Dim exp As Expression = copyBacksB(i)

                If exp Is Nothing Then Continue For

                result = exp.GenerateCode(Info.Clone(Info.Context, New LoadLocalExpression(exp, local))) AndAlso result
            Next
        End If

        If constrainedLocal IsNot Nothing Then
            Emitter.FreeLocal(constrainedLocal)
        End If

        If Info.DesiredType IsNot Nothing AndAlso Info.DesiredType.IsByRef Then
            Dim tmp As LocalBuilder
            tmp = Emitter.DeclareLocal(Info, Info.DesiredType.GetElementType)
            Emitter.EmitStoreVariable(Info, tmp)
            Emitter.EmitLoadVariableLocation(Info, tmp)
            Emitter.FreeLocal(tmp)
        End If

        Return result
    End Function

    Shared Function GetInvokeMethod(ByVal Compiler As Compiler, ByVal DelegateType As Type) As MethodInfo
        Helper.Assert(IsDelegate(Compiler, DelegateType), "The type '" & DelegateType.FullName & "' is not a delegate.")
        Return DelegateType.GetMethod(DelegateDeclaration.STR_Invoke, BindingFlags.DeclaredOnly Or BindingFlags.Public Or BindingFlags.Instance)
    End Function

    Shared Function IsDelegate(ByVal Compiler As Compiler, ByVal Type As Type) As Boolean
        Return Helper.IsSubclassOf(Compiler.TypeCache.System_MulticastDelegate, Type)
    End Function

    ''' <summary>
    ''' Returns true if the type has a default property
    ''' </summary>
    ''' <param name="Type"></param>
    ''' <param name="DefaultProperties"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function HasDefaultProperty(ByVal Context As BaseObject, ByVal Type As Type, ByRef DefaultProperties As Generic.List(Of PropertyInfo)) As Boolean
        Dim attrib As Reflection.DefaultMemberAttribute
        Dim Compiler As Compiler = Context.compiler
        Dim tD As TypeDescriptor

        tD = TryCast(Type, TypeDescriptor)

        If tD Is Nothing Then
            Dim members As New Generic.List(Of MemberInfo)
            Dim properties As New Generic.List(Of PropertyInfo)

            Helper.Assert(Type IsNot Nothing)
            attrib = CType(System.Attribute.GetCustomAttribute(Type, GetType(DefaultMemberAttribute), True), DefaultMemberAttribute)

            If attrib Is Nothing Then Return False

            'members = Helper.FilterByName(Type.GetMembers(), attrib.MemberName)
            members = Compiler.TypeManager.GetCache(Type).LookupFlattenedMembers(attrib.MemberName)

            If members IsNot Nothing Then
                For Each member As MemberInfo In members
                    If member.MemberType = MemberTypes.Property Then
                        properties.Add(DirectCast(member, PropertyInfo))
                    Else
                        Throw New InternalException("")
                    End If
                Next
            End If
            DefaultProperties = properties
            Return True
        Else
            Dim members As New Generic.List(Of MemberInfo)
            Dim properties As New Generic.List(Of PropertyInfo)
            'members.AddRange(tD.GetMembers())
            members.AddRange(Compiler.TypeManager.GetCache(tD).Cache.GetAllMembers)
            For Each member As MemberInfo In members
                Dim propD As PropertyDescriptor = TryCast(member, PropertyDescriptor)
                Dim prop As PropertyInfo = TryCast(member, PropertyInfo)
                If propD IsNot Nothing Then
                    If propD.IsDefault Then properties.Add(propD)
                ElseIf prop IsNot Nothing Then
                    Context.Compiler.Report.ShowMessage(Messages.VBNC99997, Context.Location) 'I don't know if this is a possibility with generic types.
                End If
            Next
            If properties.Count = 0 Then
                If tD.BaseType IsNot Nothing Then
                    Return Helper.HasDefaultProperty(Context, tD.BaseType, DefaultProperties)
                Else
                    Return False
                End If
            Else
                DefaultProperties = properties
                Return True
            End If
        End If
    End Function

    Shared Function GetDefaultMemberAttribute(ByVal Type As Type) As DefaultMemberAttribute
        Dim attrib As Reflection.DefaultMemberAttribute
        Dim tD As TypeDescriptor

        tD = TryCast(Type, TypeDescriptor)

        If tD Is Nothing Then
            attrib = CType(System.Attribute.GetCustomAttribute(Type, GetType(DefaultMemberAttribute), True), DefaultMemberAttribute)
        Else
            Dim types() As Object
            types = tD.GetCustomAttributes(True)
            attrib = Nothing
        End If
        Return attrib
    End Function

    Shared Function IsShadows(ByVal Context As BaseObject, ByVal Member As MemberInfo) As Boolean
        Dim result As Boolean = True
        Select Case Member.MemberType
            Case MemberTypes.Method, MemberTypes.Constructor
                Return DirectCast(Member, MethodBase).IsHideBySig = False
            Case MemberTypes.Property
                Return CBool(Helper.GetPropertyAttributes(DirectCast(Member, PropertyInfo)) And MethodAttributes.HideBySig) = False
            Case MemberTypes.Field
                Return True
            Case MemberTypes.TypeInfo
                Return True
            Case MemberTypes.NestedType
                Return True
            Case MemberTypes.Event
                Return DirectCast(Member, EventInfo).GetAddMethod.IsHideBySig = False
            Case Else
                Context.Compiler.Report.ShowMessage(Messages.VBNC99997, Context.Location)
        End Select
    End Function

    Shared Function IsShared(ByVal Member As MemberInfo) As Boolean
        Dim result As Boolean = True
        Select Case Member.MemberType
            Case MemberTypes.Method, MemberTypes.Constructor
                Return DirectCast(Member, MethodBase).IsStatic
            Case MemberTypes.Property
                Dim pInfo As PropertyInfo = DirectCast(Member, PropertyInfo)
                Return CBool(Helper.GetPropertyAttributes(pInfo) And MethodAttributes.Static)
            Case MemberTypes.Field
                Dim fInfo As FieldInfo = DirectCast(Member, FieldInfo)
                Return fInfo.IsStatic
            Case MemberTypes.TypeInfo
                Return False
            Case MemberTypes.NestedType
                Return False
            Case MemberTypes.Event
                Return DirectCast(Member, EventInfo).GetAddMethod.IsStatic
            Case Else
                Throw New InternalException("")
        End Select
    End Function

    Shared Function GetTypes(ByVal Params As ParameterInfo()) As Type()
        Dim result() As Type = Nothing

        If Params Is Nothing Then Return result
        ReDim result(Params.GetUpperBound(0))
        For i As Integer = 0 To Params.GetUpperBound(0)
            result(i) = Params(i).ParameterType
        Next
        Return result
    End Function

    Shared Function GetTypes(ByVal Arguments As Generic.List(Of Argument)) As Type()
        Dim result() As Type = Type.EmptyTypes

        If Arguments Is Nothing Then Return result
        ReDim result(Arguments.Count - 1)
        For i As Integer = 0 To Arguments.Count - 1
            Helper.Assert(Arguments(i) IsNot Nothing)
            If Arguments(i) IsNot Nothing AndAlso Arguments(i).Expression IsNot Nothing Then
                result(i) = Arguments(i).Expression.ExpressionType
            End If
        Next
        Return result
    End Function

    Shared Function GetTypes(ByVal Params As ParameterInfo()()) As Type()()
        Dim result()() As Type

        Helper.Assert(Params IsNot Nothing)

        ReDim result(Params.GetUpperBound(0))
        For i As Integer = 0 To Params.GetUpperBound(0)
            result(i) = Helper.GetTypes(Params(i))
        Next

        Return result
    End Function

#If ENABLECECIL Then
    Shared Function GetTypeDefinition(ByVal Compiler As Compiler, ByVal Type As Type) As Mono.Cecil.TypeReference
        Dim tD As TypeDescriptor = TryCast(Type, TypeDescriptor)
        If tD IsNot Nothing Then
            Return tD.Declaration.CecilType
        Else
            Return Compiler.ModuleBuilderCecil.Import(Type)
        End If
    End Function
#End If
    Shared Sub ApplyTypeArguments(ByVal Context As BaseObject, ByVal Members As Generic.List(Of MemberInfo), ByVal TypeArguments As TypeArgumentList)
        If TypeArguments Is Nothing OrElse TypeArguments.Count = 0 Then Return

        For i As Integer = Members.Count - 1 To 0 Step -1
            Members(i) = ApplyTypeArguments(Context, Members(i), TypeArguments)
            If Members(i) Is Nothing Then Members.RemoveAt(i)
        Next

    End Sub

    Shared Function ApplyTypeArguments(ByVal Context As BaseObject, ByVal Member As MemberInfo, ByVal TypeArguments As TypeArgumentList) As MemberInfo
        Dim result As MemberInfo
        Dim minfo As MethodInfo

        minfo = TryCast(Member, MethodInfo)
        If minfo IsNot Nothing Then
            Dim args() As Type
            args = minfo.GetGenericArguments()

            If args.Length = TypeArguments.Count Then
                result = TypeArguments.Compiler.TypeManager.MakeGenericMethod(TypeArguments.Parent, minfo, args, TypeArguments.AsTypeArray)
            Else
                result = Nothing
            End If
        Else
            result = Nothing
            Context.Compiler.Report.ShowMessage(Messages.VBNC99997, Context.Location)
        End If

        Return result
    End Function

    Shared Function ApplyTypeArguments(ByVal Parent As ParsedObject, ByVal OpenType As Type, ByVal TypeParameters As Type(), ByVal TypeArguments() As Type) As Type
        Dim result As Type = Nothing

        If OpenType Is Nothing Then Return Nothing

        Helper.Assert(TypeParameters IsNot Nothing AndAlso TypeArguments IsNot Nothing)
        Helper.Assert(TypeParameters.Length = TypeArguments.Length)

        If OpenType.IsGenericParameter Then
            For i As Integer = 0 To TypeParameters.Length - 1
                If Helper.CompareName(TypeParameters(i).Name, OpenType.Name) Then
                    result = TypeArguments(i)
                    Exit For
                End If
            Next
            Helper.Assert(result IsNot Nothing)
        ElseIf OpenType.IsGenericType Then
            Dim typeParams() As Type
            Dim typeArgs As New Generic.List(Of Type)

            typeParams = OpenType.GetGenericArguments()

            For i As Integer = 0 To typeParams.Length - 1
                For j As Integer = 0 To TypeParameters.Length - 1
                    If Helper.CompareName(typeParams(i).Name, TypeParameters(j).Name) Then
                        typeArgs.Add(TypeArguments(j))
                        Exit For
                    End If
                Next
                If typeArgs.Count - 1 < i Then typeArgs.Add(typeParams(i))
            Next

            Helper.Assert(typeArgs.Count = typeParams.Length AndAlso typeArgs.Count > 0)

            result = Parent.Compiler.TypeManager.MakeGenericType(Parent, OpenType, typeArgs.ToArray)
        ElseIf OpenType.IsGenericTypeDefinition Then
            Parent.Compiler.Report.ShowMessage(Messages.VBNC99997, Parent.Location)
        ElseIf OpenType.ContainsGenericParameters Then
            If OpenType.IsArray Then
                Dim elementType As Type
                elementType = OpenType.GetElementType
                elementType = ApplyTypeArguments(Parent, elementType, TypeParameters, TypeArguments)
                result = New ArrayTypeDescriptor(Parent, elementType, OpenType.GetArrayRank)
            ElseIf OpenType.IsByRef Then
                Dim elementType As Type
                elementType = OpenType.GetElementType
                elementType = ApplyTypeArguments(Parent, elementType, TypeParameters, TypeArguments)
                result = New ByRefTypeDescriptor(Parent, elementType)
            Else
                Parent.Compiler.Report.ShowMessage(Messages.VBNC99997, Parent.Location)
            End If
        Else
            result = OpenType
        End If

        Helper.Assert(result IsNot Nothing)

        Return result
    End Function

    Shared Function ApplyTypeArguments(ByVal Parent As ParsedObject, ByVal OpenParameter As ParameterInfo, ByVal TypeParameters As Type(), ByVal TypeArguments() As Type) As ParameterInfo
        Dim result As ParameterInfo

        Helper.Assert(TypeParameters IsNot Nothing AndAlso TypeArguments IsNot Nothing)
        Helper.Assert(TypeParameters.Length = TypeArguments.Length)

        Dim paramType As Type
        paramType = ApplyTypeArguments(Parent, OpenParameter.ParameterType, TypeParameters, TypeArguments)

        If paramType Is OpenParameter.ParameterType Then
            result = OpenParameter
        Else
            result = Parent.Compiler.TypeManager.MakeGenericParameter(Parent, OpenParameter, paramType)
        End If

        Helper.Assert(result IsNot Nothing)

        Return result
    End Function

    Shared Function ApplyTypeArguments(ByVal Parent As ParsedObject, ByVal OpenParameters As ParameterInfo(), ByVal TypeParameters As Type(), ByVal TypeArguments() As Type) As ParameterInfo()
        Dim result(OpenParameters.Length - 1) As ParameterInfo

        For i As Integer = 0 To result.Length - 1
            result(i) = ApplyTypeArguments(Parent, OpenParameters(i), TypeParameters, TypeArguments)
        Next

        Return result
    End Function

    Shared Function GetConversionOperators(ByVal Compiler As Compiler, ByVal Names As Generic.Queue(Of String), ByVal Type As Type, ByVal ReturnType As Type) As Generic.List(Of MethodInfo)
        Dim ops As Generic.List(Of MethodInfo)

        ops = GetOperators(Compiler, Names, Type)

        For i As Integer = ops.Count - 1 To 0 Step -1
            If CompareType(ops(i).ReturnType, ReturnType) = False Then ops.RemoveAt(i)
        Next

        Return ops
    End Function


    Shared Function GetWideningConversionOperators(ByVal Compiler As Compiler, ByVal Type As Type, ByVal ReturnType As Type) As Generic.List(Of MethodInfo)
        Return GetConversionOperators(Compiler, New Generic.Queue(Of String)(New String() {"op_Implicit"}), Type, ReturnType)
    End Function

    Shared Function GetNarrowingConversionOperators(ByVal Compiler As Compiler, ByVal Type As Type, ByVal ReturnType As Type) As Generic.List(Of MethodInfo)
        Return GetConversionOperators(Compiler, New Generic.Queue(Of String)(New String() {"op_Explicit"}), Type, ReturnType)
    End Function

    Shared Function GetOperators(ByVal Compiler As Compiler, ByVal Names As Generic.Queue(Of String), ByVal Type As Type) As Generic.List(Of MethodInfo)
        Dim result As New Generic.List(Of MethodInfo)
        Dim testName As String

        'Dim members() As MemberInfo
        Dim members As Generic.List(Of MemberInfo)
        'members = Type.GetMembers(BindingFlags.Static Or BindingFlags.Public Or BindingFlags.NonPublic)
        members = Compiler.TypeManager.GetCache(Type).FlattenedCache.GetAllMembers

        Do Until Names.Count = 0
            testName = Names.Dequeue

            For Each member As MemberInfo In members
                If member.MemberType = MemberTypes.Method Then
                    Dim method As MethodInfo = DirectCast(member, MethodInfo)
                    If method.IsSpecialName AndAlso Helper.CompareName(method.Name, testName) AndAlso method.IsStatic Then
                        result.Add(method)
                    End If
                End If
            Next
            If result.Count > 0 Then Exit Do
        Loop

        Return result
    End Function

    Shared Function GetUnaryOperators(ByVal Compiler As Compiler, ByVal Op As UnaryOperators, ByVal Type As Type) As Generic.List(Of MethodInfo)
        Dim opName As String
        Dim opNameAlternatives As New Generic.Queue(Of String)

        opName = Enums.GetStringAttribute(Op).Value
        opNameAlternatives.Enqueue(opName)

        Select Case Op
            Case UnaryOperators.Not
                opNameAlternatives.Enqueue("op_LogicalNot")
        End Select

        Return GetOperators(Compiler, opNameAlternatives, Type)
    End Function

    Shared Function GetBinaryOperators(ByVal Compiler As Compiler, ByVal Op As BinaryOperators, ByVal Type As Type) As Generic.List(Of MethodInfo)
        Dim opName As String
        Dim opNameAlternatives As New Generic.Queue(Of String)

        opName = Enums.GetStringAttribute(Op).Value
        opNameAlternatives.Enqueue(opName)

        Select Case Op
            Case BinaryOperators.And
                opNameAlternatives.Enqueue("op_LogicalAnd")
            Case BinaryOperators.Or
                opNameAlternatives.Enqueue("op_LogicalOr")
            Case BinaryOperators.ShiftLeft
                'See: http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconOperatorOverloadingUsageGuidelines.asp
                opNameAlternatives.Enqueue("op_SignedRightShift")
            Case BinaryOperators.ShiftRight
                opNameAlternatives.Enqueue("op_UnsignedRightShift")
        End Select

        Return GetOperators(Compiler, opNameAlternatives, Type)
    End Function

    ''' <summary>
    ''' Finds the parent namespace of the specified namespace.
    ''' "NS1.NS2" => "NS1"
    ''' "NS1" => ""
    ''' "" => Nothing
    ''' Nothing =>InternalException()
    ''' </summary>
    ''' <param name="Namespace"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function GetNamespaceParent(ByVal [Namespace] As String) As String
        If [Namespace] Is Nothing Then
            Throw New InternalException("")
        ElseIf [Namespace] = String.Empty Then
            Return Nothing
        Else
            Dim dotIdx As Integer
            dotIdx = [Namespace].LastIndexOf("."c)
            If dotIdx > 0 Then
                Return [Namespace].Substring(0, dotIdx)
            ElseIf dotIdx = 0 Then
                Throw New InternalException("A namespace starting with a dot??")
            Else
                Return String.Empty
            End If
        End If
    End Function

    Shared Function IsAccessibleExternal(ByVal Compiler As Compiler, ByVal Member As MemberInfo) As Boolean
        If Member.DeclaringType IsNot Nothing Then
            If Member.DeclaringType.Assembly IsNot Nothing Then
                If Member.DeclaringType.Assembly Is Compiler.AssemblyBuilder Then Return True
            End If
        End If

        If IsPublic(Member) Then Return True
        If IsProtectedFriend(Member) Then Return True
        If IsPrivate(Member) Then Return False
        If IsFriend(Member) Then Return False

        Return False
    End Function

    ''' <summary>
    ''' Checks if the called type is accessible from the caller type.
    ''' </summary>
    ''' <param name="CalledType"></param>
    ''' <param name="CallerType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function IsAccessible(ByVal CalledType As Type, ByVal CallerType As Type) As Boolean
        If Not CalledType.Assembly Is CallerType.Assembly Then
            'The types are not in the same assembly, they can only be accessible if the
            'called type is public and all its declaring types are public.
            Dim declType As Type = CalledType
            Do Until declType Is Nothing
                If declType.IsPublic = False AndAlso declType.IsNestedPublic = False Then Return False
                declType = declType.DeclaringType
            Loop
            Return True
        End If

        'If it is the same type they are obviously accessible.
        If CompareType(CalledType, CallerType) Then Return True

        'Now both types are in the same assembly.

        'If the called type is not a nested type it is accessible.
        If CalledType.DeclaringType Is Nothing Then Return True

        'The caller can descend once into a private type, check if that is the case
        If CalledType.IsNestedPrivate Then
            'don't fail here, because could be the private nesting is further up the hierarchy
            If Helper.CompareType(CalledType.DeclaringType, CallerType) Then
                Return True
            End If
        End If

        'Add all the surrounding types of the caller type to a list.
        Dim callerHierarchy As New Generic.List(Of Type)
        Dim tmp As Type = CallerType.DeclaringType
        Do Until tmp Is Nothing
            callerHierarchy.Add(tmp)
            tmp = tmp.DeclaringType
        Loop

        Dim tmpCaller As Type = CalledType.DeclaringType
        Do Until tmpCaller Is Nothing
            If callerHierarchy.Contains(tmpCaller) Then
                'The caller can descend once into a private type, check that here.
                If CalledType.IsNestedPrivate Then Return Helper.CompareType(CalledType.DeclaringType, tmpCaller)

                'We've reached a common surrounding type.
                'No matter what accessibility level this type has 
                'it is accessible.
                Return True
            End If
            If tmpCaller.IsNestedPrivate Then
                'There is a private type here...
                Return False
            End If
            tmpCaller = tmpCaller.DeclaringType
        Loop

        'If the called type is a private nested type and the above checks failed, it is inaccessible
        If CalledType.IsNestedPrivate Then Return Helper.CompareType(CalledType.DeclaringType, CallerType)

        'There is no common surrounding type, and the access level of all 
        'surrounding types of the called types are non-private, so the type
        'is accessible.
        Return True
    End Function

    Shared Function IsAccessible(ByVal Context As BaseObject, ByVal CalledMethodAccessability As MethodAttributes, ByVal CalledType As Type) As Boolean
        Dim Compiler As Compiler = Context.Compiler

        Helper.Assert(Compiler IsNot Nothing)
        Helper.Assert(CalledType IsNot Nothing)

        'Checks it the accessed method / type is accessible from the current compiling code
        '(for attributes that is not contained within a type)

        Dim testNested As Type = CalledType
        Dim compiledType As Boolean = Compiler.Assembly.IsDefinedHere(CalledType)
        Dim mostDeclaredType As Type = Nothing

        Do Until testNested Is Nothing
            mostDeclaredType = testNested
            'If it is a nested private type, it is not accessible.
            If testNested.IsNestedPrivate Then Return False
            'If it is not a nested public type in an external assembly, it is not accessible.
            If compiledType = False AndAlso testNested.IsNestedPublic = False AndAlso testNested.IsNested Then Return False
            testNested = testNested.DeclaringType
        Loop

        'If the most external type is not public then it is not accessible.
        If compiledType = False AndAlso mostDeclaredType.IsPublic = False Then Return False

        'The type is at least accessible now, check the method.

        Dim ac As MethodAttributes = (CalledMethodAccessability And MethodAttributes.MemberAccessMask)
        Dim isPrivate As Boolean = ac = MethodAttributes.Private
        Dim isFriend As Boolean = ac = MethodAttributes.Assembly OrElse ac = MethodAttributes.FamORAssem
        Dim isProtected As Boolean = ac = MethodAttributes.Family OrElse ac = MethodAttributes.FamORAssem
        Dim isPublic As Boolean = ac = MethodAttributes.Public

        'If the member is private, the member is not accessible
        '(to be accessible the types must be equal or the caller type must
        'be a nested type of the called type, cases already covered).
        If isPrivate Then Return False

        If isFriend AndAlso isProtected Then
            'Friend and Protected
            'If it is an external type it is not accessible.
            Return compiledType
        ElseIf isFriend Then
            'Friend, but not Protected
            'If it is an external type it is not accessible.
            Return compiledType
        ElseIf isProtected Then
            'Protected, but not Friend
            'It is not accessible.
            Return False
        ElseIf isPublic Then
            Return True
        End If

        Context.Compiler.Report.ShowMessage(Messages.VBNC99997, Context.Location) '("No accessibility??")

        Return False
    End Function


    Shared Function IsAccessible(ByVal Context As BaseObject, ByVal CalledMethodAccessability As MethodAttributes, ByVal CalledType As Type, ByVal CallerType As Type) As Boolean
        'If both types are equal everything is accessible.
        If CompareType(CalledType, CallerType) Then Return True

        'If the callertype is a nested class of the called type, then everything is accessible as well.
        If IsNested(CalledType, CallerType) Then Return True

        'If the called type is not accessible from the caller, the member cannot be accessible either.
        If IsAccessible(CalledType, CallerType) = False Then Return False

        Dim ac As MethodAttributes = (CalledMethodAccessability And MethodAttributes.MemberAccessMask)
        Dim isPrivate As Boolean = ac = MethodAttributes.Private
        Dim isFriend As Boolean = ac = MethodAttributes.Assembly OrElse ac = MethodAttributes.FamORAssem
        Dim isProtected As Boolean = ac = MethodAttributes.Family OrElse ac = MethodAttributes.FamORAssem
        Dim isPublic As Boolean = ac = MethodAttributes.Public

        'Public members are always accessible!
        If isPublic Then Return True

        'If the member is private, the member is not accessible
        '(to be accessible the types must be equal or the caller type must
        'be a nested type of the called type, cases already covered).
        'Catch: Enum members of public enums can apparently be private.
        If isPrivate Then Return CalledType.IsEnum

        If isFriend AndAlso isProtected Then
            'Friend and Protected
            'Both types must be in the same assembly or CallerType must inherit from CalledType.
            Return (CalledType.Assembly Is CallerType.Assembly) OrElse (CallerType.IsSubclassOf(CalledType))
        ElseIf isFriend Then
            'Friend, but not Protected
            'Both types must be in the same assembly
            Return CalledType.Assembly Is CallerType.Assembly
        ElseIf isProtected Then
            'Protected, but not Friend
            'CallerType must inherit from CalledType.
            Return CallerType.IsSubclassOf(CalledType)
        End If

        Context.Compiler.Report.ShowMessage(Messages.VBNC99997, Context.Location) '("No accessibility??")

        'private 	    = 1	= 0001
        'famandassembly = 2 = 0010
        'Assembly       = 3 = 0011
        'family         = 4 = 0100
        'famorassembly  = 5 = 0101
        'public 	    = 6	= 0110

        Return False
    End Function

    ''' <summary>
    ''' Returns true if CallerType is a nested class of CalledType.
    ''' Returns false if both types are equal.
    ''' </summary>
    ''' <param name="CalledType"></param>
    ''' <param name="CallerType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function IsNested(ByVal CalledType As Type, ByVal CallerType As Type) As Boolean
        Dim tmp As Type = CallerType.DeclaringType
        Do Until tmp Is Nothing
            If CompareType(CalledType, tmp) Then Return True
            tmp = tmp.DeclaringType
        Loop
        Return False
    End Function

    Shared Function IsAccessible(ByVal Context As BaseObject, ByVal FieldAccessability As FieldAttributes, ByVal CalledType As Type, ByVal CallerType As Type) As Boolean
        'The fieldattributes for accessibility are the same as methodattributes.
        Return IsAccessible(Context, CType(FieldAccessability, MethodAttributes), CalledType, CallerType)
    End Function

    Shared Function CreateGenericTypename(ByVal Typename As String, ByVal TypeArgumentCount As Integer) As String
        If TypeArgumentCount = 0 Then
            Return Typename
        Else
            Return String.Concat(Typename, "`", TypeArgumentCount.ToString)
        End If
    End Function

    Shared Function CreateArray(Of T)(ByVal Value As T, ByVal Length As Integer) As T()
        Dim result(Length - 1) As T
        For i As Integer = 0 To Length - 1
            result(i) = Value
        Next
        Return result
    End Function

    Shared Function GetDelegateArguments(ByVal Compiler As Compiler, ByVal delegateType As Type) As ParameterInfo()
        Dim method As MethodInfo = GetInvokeMethod(Compiler, delegateType)
        Return method.GetParameters
    End Function

    ''' <summary>
    ''' Finds the member with the exact same signature.
    ''' </summary>
    ''' <param name="grp"></param>
    ''' <param name="params"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function ResolveGroupExact(ByVal Context As BaseObject, ByVal grp As Generic.List(Of MemberInfo), ByVal params() As Type) As MemberInfo
        Dim Compiler As Compiler = Context.Compiler
        Dim result As MemberInfo = Nothing

        For i As Integer = 0 To grp.Count - 1
            Dim member As MemberInfo = grp(i)
            Select Case member.MemberType
                Case MemberTypes.Method
                    Dim method As MethodInfo = DirectCast(member, MethodInfo)
                    Dim paramtypes As Type() = Helper.GetParameterTypes(method.GetParameters)
                    If Helper.CompareTypes(paramtypes, params) Then
                        Helper.Assert(result Is Nothing)
                        result = method
                        Exit For
                    End If
                Case MemberTypes.Property
                    Dim method As PropertyInfo = DirectCast(member, PropertyInfo)
                    Dim paramtypes As Type() = Helper.GetParameterTypes(Helper.GetParameters(Compiler, method))
                    If Helper.CompareTypes(paramtypes, params) Then
                        Helper.Assert(result Is Nothing)
                        result = method
                        Exit For
                    End If
                Case MemberTypes.Event
                    Context.Compiler.Report.ShowMessage(Messages.VBNC99997, Context.Location)
                Case Else
                    Throw New InternalException("")
            End Select
        Next

        Return result
    End Function

    ''' <summary>
    ''' Finds the member with the exact same signature.
    ''' </summary>
    ''' <param name="grp"></param>
    ''' <param name="params"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function ResolveGroupExact(ByVal grp As Generic.List(Of MethodBase), ByVal params() As Type) As MethodBase
        Dim result As MethodBase = Nothing

        For i As Integer = 0 To grp.Count - 1
            Dim member As MethodBase = grp(i)
            Dim paramtypes As Type() = Helper.GetParameterTypes(member.GetParameters)
            If Helper.CompareTypes(paramtypes, params) Then
                Helper.Assert(result Is Nothing)
                result = member
                Exit For
            End If
        Next

        Return result
    End Function

    Shared Function MakeArrayType(ByVal OriginalType As Type, ByVal Ranks As Integer) As Type
        Dim result As Type = OriginalType
        If Ranks = 1 Then
            result = result.MakeArrayType()
        ElseIf Ranks > 1 Then
            result = result.MakeArrayType(Ranks)
        Else
            Throw New InternalException("")
        End If
        Return result
    End Function

    <Diagnostics.Conditional("DEBUG")> Sub DumpDefine(ByVal Context As BaseObject, ByVal Method As MethodBuilder)
        Dim Compiler As Compiler = Context.Compiler
        Dim rettype As Type = Method.ReturnType
        Dim strrettype As String
        If rettype IsNot Nothing Then
            If rettype.FullName Is Nothing Then
                strrettype = rettype.Name
            Else
                strrettype = rettype.FullName
            End If
        Else
            strrettype = ""
        End If
        Dim tp As Type = Method.DeclaringType
        Dim paramstypes As Type() = Helper.GetParameterTypes(context, Method)
        Dim attribs As MethodAttributes = Method.Attributes
        Dim impl As MethodImplAttributes = Method.GetMethodImplementationFlags
        Dim name As String = tp.FullName & ":" & Method.Name

#If EXTENDEDDEBUG Then
        Try
            System.Console.ForegroundColor = ConsoleColor.Red
        Catch ex As Exception

        End Try

        'Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, String.Format("Defined method '{0}'. Attributes: '{1}'. ImplAttributes: '{2}'. Parameters: '{3}', ReturnType: '{4}'", name, attribs.ToString.Replace("PrivateScope, ", ""), impl.ToString, TypesToString(paramstypes), strrettype))

        Try
            System.Console.ResetColor()
        Catch ex As Exception

        End Try
#End If
    End Sub

    <Diagnostics.Conditional("DEBUG")> Sub DumpDefine(ByVal Context As BaseObject, ByVal Method As ConstructorBuilder)
        Dim rettype As Type = Nothing
        Dim strrettype As String
        If rettype IsNot Nothing Then
            strrettype = rettype.FullName
        Else
            strrettype = ""
        End If
        Dim tp As Type = Method.DeclaringType
        Dim paramstypes As Type() = Helper.GetParameterTypes(context, Method)
        Dim attribs As MethodAttributes = Method.Attributes
        Dim impl As MethodImplAttributes = Method.GetMethodImplementationFlags
        Dim name As String = tp.FullName & ":" & Method.Name

#If EXTENDEDDEBUG Then
        'Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, String.Format("Defined ctor '{0}'. Attributes: '{1}'. ImplAttributes: '{2}'. Parameters: '{3}', ReturnType: '{4}'", name, attribs.ToString.Replace("PrivateScope, ", ""), impl.ToString, TypesToString(paramstypes), strrettype))
#End If

    End Sub

    Shared Function IsTypeConvertibleToAny(ByVal TypesToSearch As Type(), ByVal TypeToFind As Type) As Boolean
        For Each t As Type In TypesToSearch
            If Helper.CompareType(t, TypeToFind) OrElse t.IsSubclassOf(TypeToFind) Then Return True
        Next
        Return False
    End Function

    Shared Function IsNothing(Of T)(ByVal Value As T) As Boolean
        Return Value Is Nothing
    End Function

    <Diagnostics.Conditional("EXTENDEDDEBUG")> Sub AddCheck(ByVal Message As String)
#If EXTENDEDDEBUG Then
        Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, "Skipped check: " & Message)
#End If
    End Sub

    Shared Function DefineCollection(ByVal Collection As IEnumerable) As Boolean
        Dim result As Boolean = True
        For Each obj As IBaseObject In Collection
            result = obj.Define AndAlso result
        Next
        Return result
    End Function

    Shared Function DefineMembersCollection(ByVal Collection As Generic.IEnumerable(Of IDefinableMember)) As Boolean
        Dim result As Boolean = True
        For Each obj As IDefinableMember In Collection
            result = obj.DefineMember AndAlso result
        Next
        Return result
    End Function

    Shared Function ResolveCodeCollection(ByVal Collection As IEnumerable, ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True
        If Info Is Nothing Then Info = ResolveInfo.Default(Info.Compiler)
        Helper.AssertNotNothing(Collection)
        For Each obj As BaseObject In Collection
            result = obj.ResolveCode(Info) AndAlso result
            'Helper.Assert(result = (obj.Compiler.Report.Errors = 0))
        Next
        Return result
    End Function

    Shared Function ResolveTypeReferencesCollection(ByVal Collection As IEnumerable) As Boolean
        Dim result As Boolean = True
        For Each obj As ParsedObject In Collection
            result = obj.ResolveTypeReferences AndAlso result
            'vbnc.Helper.Assert(result = (obj.Compiler.Report.Errors = 0))
        Next
        Return result
    End Function

    Shared Function ResolveTypeReferences(ByVal ParamArray Collection As ParsedObject()) As Boolean
        Dim result As Boolean = True
        For Each obj As ParsedObject In Collection
            If obj IsNot Nothing Then result = obj.ResolveTypeReferences AndAlso result
        Next
        Return result
    End Function

    Shared Function ResolveStatementCollection(ByVal Collection As IEnumerable, ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True
        For Each obj As Statement In Collection
            result = obj.ResolveStatement(Info) AndAlso result
        Next
        Return result
    End Function

    Shared Function GenerateCodeCollection(ByVal Collection As IEnumerable, ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True
        For Each obj As IBaseObject In Collection
            result = obj.GenerateCode(Info) AndAlso result
        Next
        Return result
    End Function

    Shared Function GenerateCodeCollection(ByVal Collection As IList, ByVal Info As EmitInfo, ByVal Types As Type()) As Boolean
        Dim result As Boolean = True
        Helper.Assert(Collection.Count = Types.Length)
        For i As Integer = 0 To Collection.Count - 1
            result = DirectCast(Collection(i), IBaseObject).GenerateCode(Info.Clone(Info.Context, Info.IsRHS, Info.IsExplicitConversion, Types(i))) AndAlso result
        Next
        Return result
    End Function

    Shared Function CloneExpressionArray(ByVal Expressions() As Expression, ByVal NewParent As ParsedObject) As Expression()
        Dim result(Expressions.GetUpperBound(0)) As Expression
        For i As Integer = 0 To result.GetUpperBound(0)
            If Expressions(i) IsNot Nothing Then
                result(i) = Expressions(i).Clone(NewParent)
            End If
        Next
        Return result
    End Function

    ReadOnly Property Compiler() As Compiler
        Get
            Return m_Compiler
        End Get
    End Property

    ''' <summary>
    ''' If there is only one shared compiler, that one is returned, otherwise nothing is returned.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared ReadOnly Property SharedCompiler() As Compiler
        Get
            If m_SharedCompilers.Count = 1 Then
                Return m_SharedCompilers(0)
            Else
                Return Nothing
            End If
        End Get
    End Property

    Sub New(ByVal Compiler As Compiler)
        m_Compiler = Compiler
        If m_SharedCompilers.Contains(Compiler) = False Then
            m_SharedCompilers.Add(Compiler)
        End If
    End Sub

    <Diagnostics.DebuggerHidden()> _
    <Diagnostics.Conditional("DEBUG")> _
    Shared Sub Assert(ByVal Condition As Boolean, ByVal Message As String)
        If Condition = False Then
            Diagnostics.Debug.WriteLine(Message)
            If SharedCompiler IsNot Nothing Then SharedCompiler.Report.WriteLine(Report.ReportLevels.Debug, Message)
        End If
        Assert(Condition)
    End Sub

    <Diagnostics.Conditional("DEBUG")> _
    <Diagnostics.DebuggerHidden()> Shared Sub Assert(ByVal Condition As Boolean)
        If Condition = False Then Helper.Stop()
    End Sub

    <Diagnostics.Conditional("DEBUG")> _
    <Diagnostics.DebuggerHidden()> Shared Sub AssertNotNothing(ByVal Value As Object)
        If Value Is Nothing Then Helper.Stop()
        If TypeOf Value Is IEnumerable Then AssertNotNothing(DirectCast(Value, IEnumerable))
    End Sub

    <Diagnostics.DebuggerHidden()> _
    <Diagnostics.Conditional("DEBUG")> _
    Shared Sub AssertNotNothing(ByVal Value As IEnumerable)
        If Value Is Nothing Then
            Helper.Stop()
        Else
            For Each obj As Object In Value
                If obj Is Nothing Then Helper.Stop()
            Next
        End If
    End Sub

    Shared Sub AssertType(Of T)(ByVal Collection As IEnumerable)
        For Each v As Object In Collection
            Assert(TypeOf v Is T)
        Next
    End Sub


    <Diagnostics.DebuggerHidden()> Shared Function AddError(ByVal Compiler As Compiler, ByVal Location As Span, Optional ByVal Message As String = Nothing) As Boolean
        If Message Is Nothing Then
            Message = "<no message written yet>"
        End If
        Return Compiler.Report.ShowMessage(Messages.VBNC99999, Location, Message)
    End Function

    <Diagnostics.DebuggerHidden()> Shared Function AddError(ByVal Context As BaseObject, Optional ByVal Message As String = Nothing) As Boolean
        If Message Is Nothing Then
            Message = "<no message written yet>"
        End If
        Return Context.Compiler.Report.ShowMessage(Messages.VBNC99999, Context.Location, Message)
    End Function

    <Diagnostics.DebuggerHidden()> Shared Sub AddWarning(Optional ByVal Message As String = "(No message provided)")
        Dim msg As String
        msg = "A warning message should have been shown: '" & Message & "'"
        Diagnostics.Debug.WriteLine(msg)
        Console.WriteLine(msg)
        If IsDebugging() Then
            'Helper.Stop()
        Else
            'Throw New NotImplementedException(msg)
        End If
    End Sub

    Shared Function IsBootstrapping() As Boolean
        Return Reflection.Assembly.GetExecutingAssembly.Location.Contains("SelfCompile.exe")
    End Function

    Shared Function IsDebugging() As Boolean
        'Return False
        If Diagnostics.Debugger.IsAttached = False Then Return False
        If Reflection.Assembly.GetEntryAssembly Is Nothing Then Return False
        If Reflection.Assembly.GetEntryAssembly.FullName.Contains("rt") Then Return False
        If AppDomain.CurrentDomain.FriendlyName.Contains("rt") Then Return False
        Return True
    End Function

    <Diagnostics.DebuggerHidden()> Shared Sub ErrorRecoveryNotImplemented()
        Console.WriteLine("Error recovery not implemented yet.")
    End Sub

    <Diagnostics.DebuggerHidden()> Private Shared Sub IndirectedStop()
        Stop
    End Sub

    Class StopException
        Inherits Exception

        Sub New(ByVal Message As String)
            MyBase.New(Message)
        End Sub
    End Class

    <Diagnostics.DebuggerHidden()> Shared Sub StopIfDebugging(Optional ByVal Condition As Boolean = True)
        If Condition AndAlso IsDebugging() Then
            IndirectedStop()
        End If
    End Sub

    <Diagnostics.DebuggerHidden()> Shared Sub [Stop](Optional ByVal Message As String = "")
        If IsDebugging() Then
            IndirectedStop()
        Else
            Throw New InternalException(Message)
        End If
    End Sub

    ''' <summary>
    ''' This function takes a string as an argument and split it on the space character,
    ''' with the " as acceptable character.
    ''' </summary>
    Shared Function ParseLine(ByVal strLine As String) As String()
        Dim strs As New ArrayList
        Dim bInQuote As Boolean
        Dim iStart As Integer
        Dim builder As New System.Text.StringBuilder

        For i As Integer = 0 To strLine.Length - 1
            If strLine.Chars(i) = "\"c AndAlso i < strLine.Length - 1 AndAlso strLine.Chars(i + 1) = """"c Then
                builder.Append(""""c)
                i += 1
            ElseIf strLine.Chars(i) = """"c Then
                If strLine.Length - 1 >= i + 1 AndAlso strLine.Chars(i + 1) = """"c Then
                    builder.Append(""""c)
                Else
                    bInQuote = Not bInQuote
                End If
            ElseIf bInQuote = False AndAlso strLine.Chars(i) = " "c Then
                If builder.ToString.Trim() <> "" Then strs.Add(builder.ToString)
                builder.Length = 0
                iStart = i + 1
            Else
                builder.Append(strLine.Chars(i))
            End If
        Next
        If builder.Length > 0 Then strs.Add(builder.ToString)

        'Add the strings to the return value
        Dim stt(strs.Count - 1) As String
        For i As Integer = 0 To strs.Count - 1
            stt(i) = DirectCast(strs(i), String)
        Next

        Return stt
    End Function

    ''' <summary>
    ''' Get the type attribute from the scope
    ''' </summary>
    ''' <param name="Modifiers"></param>
    ''' <param name="isNested"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Scope: 
    ''' Private = private
    ''' Protected = family
    ''' Protected Friend = famorassem
    ''' Friend = assembly
    ''' Public = public
    ''' </remarks>
    Shared Function getTypeAttributeScopeFromScope(ByVal Modifiers As Modifiers, ByVal isNested As Boolean) As System.Reflection.TypeAttributes
        If Not isNested Then
            'If vbnc.Modifiers.IsNothing(Modifiers) = False Then
            If Modifiers.Is(ModifierMasks.Public) Then
                Return Reflection.TypeAttributes.Public
            Else
                Return Reflection.TypeAttributes.NotPublic
            End If
            'Else
            '  Return TypeAttributes.NotPublic
            'End If
        Else
            'If vbnc.Modifiers.IsNothing(Modifiers) = False Then
            If Modifiers.Is(ModifierMasks.Public) Then
                Return Reflection.TypeAttributes.NestedPublic
            ElseIf Modifiers.Is(ModifierMasks.Friend) Then
                If Modifiers.Is(ModifierMasks.Protected) Then
                    Return Reflection.TypeAttributes.NestedFamORAssem
                    '0Return Reflection.TypeAttributes.NotPublic
                    'Return Reflection.TypeAttributes.VisibilityMask
                Else
                    Return Reflection.TypeAttributes.NestedAssembly
                    'Return Reflection.TypeAttributes.NotPublic
                End If
            ElseIf Modifiers.Is(ModifierMasks.Protected) Then
                Return Reflection.TypeAttributes.NestedFamily
                'Return Reflection.TypeAttributes.NotPublic
            ElseIf Modifiers.Is(ModifierMasks.Private) Then
                Return Reflection.TypeAttributes.NestedPrivate
            Else
                'Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, "Default scope set to public...")
                Return Reflection.TypeAttributes.NestedPublic
            End If
            ' Else
            'Return Reflection.TypeAttributes.NestedPublic
            'End If
        End If
    End Function

    'TODO: This function is horribly inefficient. Change to use shift operators.
    Shared Function BinToInt(ByVal str As String) As ULong
        Dim len As Integer = str.Length
        For i As Integer = len To 1 Step -1
            Select Case str.Chars(i - 1)
                Case "1"c
                    BinToInt += CULng(2 ^ (len - i))
                Case "0"c
                    'ok
                Case Else
                    Throw New ArgumentOutOfRangeException("str", str, "Invalid binary number: cannot contain character " & str.Chars(i - 1))
            End Select
        Next
    End Function

    Shared Function DecToDbl(ByVal str As String) As Double
        Return Double.Parse(str, USCulture)
    End Function

    Shared ReadOnly Property USCulture() As Globalization.CultureInfo
        Get
            Return Globalization.CultureInfo.GetCultureInfo("en-US")
        End Get
    End Property

    Shared Function DecToInt(ByVal str As String) As Decimal
        Return Decimal.Parse(str)
    End Function

    'TODO: This function can also be severely optimized.
    Shared Function HexToInt(ByVal str As String) As ULong
        Dim i, n As Integer
        Dim l As Integer = str.Length
        For i = l To 1 Step -1
            Select Case str.Chars(i - 1)
                Case "0"c
                    n = 0
                Case "1"c
                    n = 1
                Case "2"c
                    n = 2
                Case "3"c
                    n = 3
                Case "4"c
                    n = 4
                Case "5"c
                    n = 5
                Case "6"c
                    n = 6
                Case "7"c
                    n = 7
                Case "8"c
                    n = 8
                Case "9"c
                    n = 9
                Case "a"c, "A"c
                    n = 10
                Case "b"c, "B"c
                    n = 11
                Case "c"c, "C"c
                    n = 12
                Case "d"c, "D"c
                    n = 13
                Case "e"c, "E"c
                    n = 14
                Case "f"c, "F"c
                    n = 15
                Case Else
                    Throw New ArgumentOutOfRangeException("str", str, "Invalid hex number: cannot contain character " & str.Chars(i - 1))
            End Select

            HexToInt += CULng(n * (16 ^ (l - i)))
        Next
    End Function

    Shared Function IntToHex(ByVal Int As ULong) As String
        Return Microsoft.VisualBasic.Hex(Int)
    End Function

    Shared Function IntToBin(ByVal Int As ULong) As String
        If Int = 0 Then Return "0"
        IntToBin = ""
        Do Until Int = 0
            If CBool(Int And 1UL) Then
                IntToBin = "1" & IntToBin
            Else
                IntToBin = "0" & IntToBin
            End If
            Int >>= 1
        Loop
    End Function

    Shared Function IntToOct(ByVal Int As ULong) As String
        Return Microsoft.VisualBasic.Oct(Int)
    End Function

    'TODO: This function can also be severely optimized.
    Shared Function OctToInt(ByVal str As String) As ULong
        Dim i, n As Integer
        Dim l As Integer = str.Length
        For i = l To 1 Step -1
            Select Case str.Chars(i - 1)
                Case "0"c
                    n = 0
                Case "1"c
                    n = 1
                Case "2"c
                    n = 2
                Case "3"c
                    n = 3
                Case "4"c
                    n = 4
                Case "5"c
                    n = 5
                Case "6"c
                    n = 6
                Case "7"c
                    n = 7
                Case Else
                    Throw New ArgumentOutOfRangeException("str", str, "Invalid octal number: cannot contain character " & str.Chars(i - 1))
            End Select
            OctToInt += CULng(n * (8 ^ (l - i)))
        Next
    End Function

    '#If DEBUG Then
    '    ''' <summary>
    '    ''' Invokes Dump of all objects in the collection, but not the object ifself
    '    ''' </summary>
    '    ''' <param name="obj"></param>
    '    ''' <remarks></remarks>
    '    Shared Sub DumpCollection(ByVal obj As IList, ByVal Dumper As IndentedTextWriter, Optional ByVal Delimiter As String = "")
    '        Dim tmpDelimiter As String = ""
    '        For Each o As BaseObject In obj
    '            Dumper.Write(tmpDelimiter)
    '            o.Dump(Dumper)
    '            tmpDelimiter = Delimiter
    '        Next
    '    End Sub

    '    ''' <summary>
    '    ''' Invokes Dump of all objects in the collection, but not the object ifself
    '    ''' </summary>
    '    ''' <param name="obj"></param>
    '    ''' <remarks></remarks>
    '    Shared Sub DumpCollection(ByVal obj As IList, ByVal Dumper As IndentedTextWriter, ByVal Prefix As String, ByVal Postfix As String)
    '        For Each o As BaseObject In obj
    '            Dumper.Write(Prefix)
    '            o.Dump(Dumper)
    '            Dumper.Write(Postfix)
    '        Next
    '    End Sub

    '    ''' <summary>
    '    ''' Invokes Dump of all objects in the collection, but not the object ifself
    '    ''' </summary>
    '    ''' <param name="obj"></param>
    '    ''' <remarks></remarks>
    '    Shared Sub DumpCollection(ByVal obj As IEnumerable, ByVal Dumper As IndentedTextWriter, Optional ByVal Delimiter As String = "")
    '        Dim tmpDelimiter As String = ""
    '        For Each o As BaseObject In obj
    '            Dumper.Write(tmpDelimiter)
    '            o.Dump(Dumper)
    '            tmpDelimiter = Delimiter
    '        Next
    '    End Sub
    '#End If

    ''' <summary>
    ''' Returns a sequence number, incremented in 1 on every call
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function GetSequenceNumber() As Integer
        Static number As Integer
        number += 1
        Return number
    End Function

    ''' <summary>
    ''' Converts the value into how it would look in a source file. 
    ''' I.E: if it is a date, surround with #, if it is a string, surround with "
    ''' </summary>
    ''' <param name="Value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function ValueToCodeConstant(ByVal Value As Object) As String
        If TypeOf Value Is String Then
            Return """" & Value.ToString.Replace("""", """""") & """"
        ElseIf TypeOf Value Is Char Then
            Return """" & Value.ToString.Replace("""", """""") & """c"
        ElseIf TypeOf Value Is Date Then
            Return "#" & Value.ToString & "#"
        ElseIf Value Is Nothing Then
            Return KS.Nothing.ToString
        Else
            Return Value.ToString
        End If
    End Function

    ''' <summary>
    ''' If the argument is a typedescriptor, looks up the 
    ''' </summary>
    ''' <param name="Type"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function GetTypeOrTypeBuilder(ByVal Type As Type) As Type
        If Type Is Nothing Then Return Nothing
        Dim tmp As TypeDescriptor = TryCast(Type, TypeDescriptor)
        If tmp Is Nothing Then
            Return Type
        Else
            Return tmp.TypeInReflection
        End If
    End Function

    ''' <summary>
    ''' If the argument is a typedescriptor, looks up the 
    ''' </summary>
    ''' <param name="Ctor"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function GetCtorOrCtorBuilder(ByVal Ctor As ConstructorInfo) As ConstructorInfo
        Dim tmp As ConstructorDescriptor = TryCast(Ctor, ConstructorDescriptor)
        If tmp Is Nothing Then
            Return Ctor
        Else
            Helper.Assert(tmp.ConstructorInReflection IsNot Nothing)
            Return tmp.ConstructorInReflection
        End If
    End Function

    Shared Function GetMethodOrMethodBuilder(ByVal Method As MethodInfo) As MethodInfo
        Dim tmp As MethodDescriptor = TryCast(Method, MethodDescriptor)
        If tmp Is Nothing Then
            Return Method
        Else
            Return tmp.MethodInReflection
        End If
    End Function

    Shared Function GetPropertyOrPropertyBuilder(ByVal [Property] As PropertyInfo) As PropertyInfo
        Dim tmp As PropertyDescriptor = TryCast([Property], PropertyDescriptor)
        If tmp Is Nothing Then
            Return [Property]
        Else
            Return tmp.PropertyInReflection
        End If
    End Function

    Shared Sub GetPropertyOrPropertyBuilder(ByVal Properties As Generic.List(Of PropertyInfo))
        For i As Integer = 0 To Properties.Count - 1
            Dim tmp As PropertyDescriptor = TryCast(Properties(i), PropertyDescriptor)
            If tmp IsNot Nothing Then
                Properties(i) = tmp.PropertyInReflection
            End If
        Next
    End Sub

    Shared Function GetFieldOrFieldBuilder(ByVal Field As FieldInfo) As FieldInfo
        Dim tmp As FieldDescriptor = TryCast(Field, FieldDescriptor)
        If tmp Is Nothing Then
            Return Field
        Else
            Return tmp.FieldInReflection
        End If
    End Function

    Shared Sub GetFieldOrFieldBuilder(ByVal Fields As Generic.List(Of FieldInfo))
        For i As Integer = 0 To Fields.Count - 1
            Dim tmp As FieldDescriptor = TryCast(Fields(i), FieldDescriptor)
            If tmp IsNot Nothing Then
                Fields(i) = tmp.FieldInReflection
            End If
        Next
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Type"></param>
    ''' <remarks></remarks>
    Shared Sub SetTypeOrTypeBuilder(ByVal Type As Type())
        If Type Is Nothing Then Return
        For i As Integer = 0 To Type.Length - 1
            Helper.Assert(Type(i) IsNot Nothing)
            Type(i) = GetTypeOrTypeBuilder(Type(i))
        Next
    End Sub

    Shared Function GetTypeOrTypeBuilders(ByVal Type As Type(), Optional ByVal OnlySuccessful As Boolean = False) As Type()
        Dim result() As Type
        If Type Is Nothing Then Return Nothing

        ReDim result(Type.GetUpperBound(0))
        For i As Integer = 0 To Type.GetUpperBound(0)
            Dim tmp As Type
            tmp = GetTypeOrTypeBuilder(Type(i))
            If tmp Is Nothing AndAlso OnlySuccessful Then
                result(i) = Type(i)
            Else
                result(i) = tmp
            End If
        Next
        Return result
    End Function

    Shared Function IsAssignable(ByVal Context As BaseObject, ByVal FromType As Type, ByVal ToType As Type) As Boolean
        Dim Compiler As Compiler = Context.Compiler
        'If TypeOf FromType Is TypeDescriptor Then FromType = FromType.UnderlyingSystemType
        'If TypeOf ToType Is TypeDescriptor Then ToType = ToType.UnderlyingSystemType
#If EXTENDEDDEBUG Then
        Compiler.Report.WriteLine("IsAssignable (FromType := " & FromType.FullName & ", ToType := " & ToType.FullName)
#End If
        If FromType Is ToType Then
            Return True
        ElseIf Helper.CompareType(FromType, Compiler.TypeCache.Nothing) Then
            Return True
        ElseIf FromType.IsArray = True AndAlso ToType.IsArray = True AndAlso FromType.FullName Is Nothing AndAlso ToType.FullName Is Nothing AndAlso FromType.Name.Equals(ToType.Name, StringComparison.Ordinal) Then
            Return True
        ElseIf CompareType(ToType, Compiler.TypeCache.System_Object) Then
            Return True
        ElseIf TypeOf ToType Is GenericTypeParameterBuilder AndAlso TypeOf FromType Is Type Then
            Return ToType.Name = FromType.Name
        ElseIf ToType.GetType Is Compiler.TypeCache.System_Reflection_Emit_TypeBuilderInstantiation Then
            'ElseIf ToType.GetType.Name = "TypeBuilderInstantiation" Then
            If Helper.CompareType(Helper.GetTypeOrTypeBuilder(FromType), ToType) Then
                Return True
            Else
                Return False
            End If
            Return True
        ElseIf TypeOf ToType Is TypeDescriptor = False AndAlso TypeOf FromType Is TypeDescriptor = False AndAlso ToType.IsAssignableFrom(FromType) Then
            Return True
        ElseIf IsInterface(Context, ToType) Then
            Dim ifaces() As Type = Compiler.TypeManager.GetRegisteredType(FromType).GetInterfaces()
            For Each iface As Type In ifaces
                If Helper.IsAssignable(Context, iface, ToType) Then Return True
                If Helper.CompareType(iface, ToType) Then Return True
                If Helper.IsSubclassOf(ToType, iface) Then Return True
            Next
            If IsInterface(Context, FromType) AndAlso FromType.IsGenericType AndAlso ToType.IsGenericType Then
                Dim baseFromI, baseToI As Type
                baseFromI = FromType.GetGenericTypeDefinition
                baseToI = ToType.GetGenericTypeDefinition
                If Helper.CompareType(baseFromI, baseToI) Then
                    Dim fromArgs, toArgs As Type()
                    fromArgs = FromType.GetGenericArguments
                    toArgs = ToType.GetGenericArguments
                    If fromArgs.Length = toArgs.Length Then
                        For i As Integer = 0 To toArgs.Length - 1
                            If Helper.IsAssignable(Context, fromArgs(i), toArgs(i)) = False Then Return False
                        Next
                        Return True
                    End If
                End If
            End If
            Return False
        ElseIf Helper.IsEnum(Compiler, FromType) AndAlso Compiler.TypeResolution.IsImplicitlyConvertible(Context, GetEnumType(Compiler, FromType), ToType) Then
            Return True
        ElseIf ToType.FullName IsNot Nothing AndAlso FromType.FullName IsNot Nothing AndAlso ToType.FullName.Equals(FromType.FullName, StringComparison.Ordinal) Then
            Return True
        ElseIf Helper.CompareType(Compiler.TypeCache.System_UInt32, ToType) AndAlso Helper.CompareType(Compiler.TypeCache.System_UInt16, FromType) Then
            Return True
        ElseIf Helper.CompareType(FromType, Compiler.TypeCache.System_Object) Then
            Return False
        ElseIf Helper.IsSubclassOf(ToType, FromType) Then
            Return True
        ElseIf Helper.IsSubclassOf(FromType, ToType) Then
            Return False
        ElseIf FromType.IsArray AndAlso ToType.IsArray Then
            Dim fromElement As Type = FromType.GetElementType
            Dim toElement As Type = ToType.GetElementType
            If fromElement.IsValueType Xor toElement.IsValueType Then
                Return False
            Else
                Return Helper.IsAssignable(Context, fromElement, toElement)
            End If
        Else
            'Helper.NotImplementedYet("Don't know if it possible to convert from " & FromType.Name & " to " & ToType.Name)
            Return False
        End If
    End Function

    Shared Function GetMostEncompassedTypes(ByVal Compiler As Compiler, ByVal Types() As TypeCode) As TypeCode()
        Dim result As Generic.List(Of TypeCode)

        If Types Is Nothing Then Return Nothing
        If Types.Length <= 1 Then Return Types

        result = New Generic.List(Of TypeCode)(Types)

        If result.Count <= 1 Then Return result.ToArray

        Dim didSomething As Boolean = False
        Do
            didSomething = False
            For i As Integer = result.Count - 2 To 0 Step -1
                If IsFirstEncompassingSecond(Compiler, result(i), result(i + 1)) Then
                    result.RemoveAt(i)
                    didSomething = True
                ElseIf IsFirstEncompassingSecond(Compiler, result(i + 1), result(i)) Then
                    result.RemoveAt(i + 1)
                    didSomething = True
                End If
            Next
        Loop While didSomething

        Return result.ToArray
    End Function

    Shared Function IsFirstEncompassingSecond(ByVal Compiler As Compiler, ByVal First As TypeCode, ByVal Second As TypeCode) As Boolean
        If First = Second Then Return False
        Return Compiler.TypeResolution.IsImplicitlyConvertible(Compiler, Second, First)
    End Function

    Shared Function IsNullableType(ByVal Compiler As Compiler, ByVal Type As Type) As Boolean
        If Type.IsValueType = False Then Return False
        If CompareType(Type, Compiler.TypeCache.System_Nullable1) Then Return True

        If Type.IsGenericTypeDefinition Then Return False
        If Type.IsGenericParameter Then Return False
        If Type.IsGenericType = False Then Return False
        Return Helper.CompareType(Type.GetGenericTypeDefinition, Compiler.TypeCache.System_Nullable1)
    End Function

    Shared Function IsSubclassOf(ByVal BaseClass As Type, ByVal DerivedClass As Type) As Boolean
        Dim base As Type = DerivedClass.BaseType
        Do While base IsNot Nothing
            If Helper.CompareType(base, BaseClass) Then Return True
            base = base.BaseType
        Loop
        Return False
    End Function

    Shared Function DoesTypeImplementInterface(ByVal Context As BaseObject, ByVal Type As Type, ByVal [Interface] As Type) As Boolean
        Dim ifaces() As Type
        ifaces = Context.Compiler.TypeManager.GetRegisteredType(Type).GetInterfaces
        For Each iface As Type In ifaces
            If Helper.IsAssignable(context, iface, [Interface]) Then Return True
        Next
        Return False
        Return Array.IndexOf(Type.GetInterfaces, [Interface]) >= 0
    End Function

    Shared Function GetEnumType(ByVal Compiler As Compiler, ByVal EnumType As Type) As Type
        Helper.Assert(EnumType.IsEnum)
        EnumType = Compiler.TypeManager.GetRegisteredType(EnumType)
        If TypeOf EnumType Is TypeDescriptor Then
            Return DirectCast(DirectCast(EnumType, TypeDescriptor).Declaration, EnumDeclaration).EnumConstantType
        Else
            Dim fInfo As FieldInfo
            fInfo = EnumType.GetField(EnumDeclaration.EnumTypeMemberName, BindingFlags.Static Or BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Instance)

            Helper.Assert(fInfo IsNot Nothing)

            Return fInfo.FieldType
        End If
    End Function

    ''' <summary>
    ''' Creates a CType expression containing the specified FromExpression if necessary.
    ''' </summary>
    ''' <param name="Parent"></param>
    ''' <param name="FromExpression"></param>
    ''' <param name="DestinationType"></param>
    ''' <param name="result"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function CreateTypeConversion(ByVal Parent As ParsedObject, ByVal FromExpression As Expression, ByVal DestinationType As Type, ByRef result As Boolean) As Expression
        Dim fromExpr As Expression

        Helper.Assert(FromExpression IsNot Nothing)

        fromExpr = FromExpression

        Dim fromType As Type
        fromType = FromExpression.ExpressionType

#If EXTENDEDDEBUG Then
        Parent.Compiler.Report.WriteLine("Creating type conversion, from " & fromType.FullName & " to " & DestinationType.FullName)
        If DestinationType.IsByRef Then
            Parent.Compiler.Report.WriteLine(">DestinationType.ElementType = " & DestinationType.GetElementType.FullName)
            Parent.Compiler.Report.WriteLine(">IsAssignable to DestinationType.ElementType = " & IsAssignable(Parent.Compiler, fromType, DestinationType.GetElementType))
        End If
#End If

        Helper.Assert(fromType IsNot Nothing)

        If Helper.CompareType(fromType, DestinationType) Then
            'do nothing
        ElseIf fromExpr.ExpressionType.IsByRef AndAlso IsAssignable(Parent, fromType.GetElementType, DestinationType) Then
            'do nothing
        ElseIf DestinationType.IsByRef AndAlso IsAssignable(Parent, fromExpr.ExpressionType, DestinationType.GetElementType) Then
#If EXTENDEDDEBUG Then
            Parent.Compiler.Report.WriteLine(">3")
#End If
            If fromExpr.ExpressionType.IsByRef = False AndAlso Helper.CompareType(fromExpr.ExpressionType, DestinationType.GetElementType) = False Then
                fromExpr = New CTypeExpression(Parent, fromExpr, DestinationType.GetElementType)
                result = fromExpr.ResolveExpression(ResolveInfo.Default(Parent.Compiler)) AndAlso result
            End If
            'do nothing
        ElseIf DestinationType.IsByRef AndAlso Parent.Compiler.TypeResolution.IsImplicitlyConvertible(Parent, fromExpr.ExpressionType, DestinationType.GetElementType) Then
            Dim tmpExp As Expression
            tmpExp = CreateTypeConversion(Parent, fromExpr, DestinationType.GetElementType, result)
            If result = False Then Return fromExpr

            fromExpr = tmpExp
        ElseIf CompareType(fromExpr.ExpressionType, Parent.Compiler.TypeCache.Nothing) Then
            'do nothing
        ElseIf CompareType(DestinationType, Parent.Compiler.TypeCache.System_Enum) AndAlso fromExpr.ExpressionType.IsEnum Then
            fromExpr = New BoxExpression(Parent, fromExpr, DestinationType)
        ElseIf CompareType(fromExpr.ExpressionType, DestinationType) = False Then
            Dim CTypeExp As Expression

            If fromExpr.ExpressionType.IsByRef Then
                fromExpr = New DeRefExpression(fromExpr, fromExpr)
            End If

            CTypeExp = ConversionExpression.GetTypeConversion(Parent, fromExpr, DestinationType)
            result = CTypeExp.ResolveExpression(ResolveInfo.Default(Parent.Compiler)) AndAlso result
            fromExpr = CTypeExp
        End If

#If EXTENDEDDEBUG Then
        If fromType IsNot FromExpression Then
            Parent.Compiler.Report.WriteLine(Report.ReportLevels.Debug, "Created type conversion from '" & FromExpression.ExpressionType.Name & "' to '" & DestinationType.Name & "'")
        End If
#End If

        Return fromExpr
    End Function

    ''' <summary>
    ''' Returns true if all types in both arrays are the exact same types.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function CompareTypes(ByVal Types1() As Type, ByVal Types2() As Type) As Boolean
        If Types1 Is Nothing AndAlso Types2 Is Nothing Then
            Return True
        ElseIf Types1 Is Nothing Xor Types2 Is Nothing Then
            Return False
        Else
            If Types1.Length <> Types2.Length Then Return False
            For i As Integer = 0 To Types1.Length - 1
                If Helper.CompareType(Types1(i), Types2(i)) = False Then Return False
            Next
            Return True
        End If
    End Function

#If ENABLECECIL Then
    Shared Function CompareType(ByVal t1 As Mono.Cecil.TypeReference, ByVal t2 As Mono.Cecil.TypeReference) As Boolean
        If t1 Is t2 Then Return True
        Helper.Assert(t1.FullName.Equals(t2.FullName) = False)
        Return False
    End Function
#End If

    Shared Function CompareType(ByVal t1 As Type, ByVal t2 As Type) As Boolean
        If t1 Is Nothing AndAlso t2 Is Nothing Then Return True
        If t1 Is Nothing Xor t2 Is Nothing Then Return False

        Dim td1, td2 As TypeDescriptor
        td1 = TryCast(t1, TypeDescriptor)
        td2 = TryCast(t2, TypeDescriptor)

        If td1 IsNot Nothing AndAlso td2 IsNot Nothing Then
            'They are both type descriptors.
            Return td1.Equals(td2)
        ElseIf td1 Is Nothing AndAlso td2 Is Nothing Then
            'None of them are type descriptors.
            If t1 Is Nothing Then Return False
            Return t1.Equals(t2)
        Else
            If td1 Is Nothing Then
                Dim tmp As Type = t1
                td1 = td2
                t2 = t1
            End If
            'Only td1 is a type descriptor
            If td1.Declaration IsNot Nothing Then
                If td1.Declaration.TypeBuilder IsNot Nothing Then
                    Return td1.Declaration.TypeBuilder.Equals(t2)
                Else
                    Return False 'If t2 is a Type, but td1 doesn't have a TypeBuilder yet, both types cannot be equal.
                End If
            ElseIf TypeOf td1 Is TypeParameterDescriptor Then
                'td2 is not a typeparameterdescriptor
                Dim tdp1 As TypeParameterDescriptor = DirectCast(td1, TypeParameterDescriptor)

                Return tdp1.Equals(t2)
            ElseIf td1.IsArray <> t2.IsArray Then
                Return False
            ElseIf td1.IsByRef AndAlso t2.IsByRef Then
                Return Helper.CompareType(td1.GetElementType, t2.GetElementType)
            ElseIf TypeOf td1 Is GenericTypeDescriptor Then
                Dim tdg1 As GenericTypeDescriptor = DirectCast(td1, GenericTypeDescriptor)
                If t2.IsGenericParameter = False AndAlso t2.IsGenericType = False AndAlso t2.IsGenericTypeDefinition = False AndAlso t2.ContainsGenericParameters = False Then
                    Return False
                ElseIf Helper.CompareType(tdg1.BaseType, t2.BaseType) = False Then
                    Return False
                ElseIf Helper.CompareType(tdg1.GetGenericTypeDefinition, t2.GetGenericTypeDefinition) = False Then
                    Return False
                Else
                    Dim args1, args2 As Type()
                    args1 = tdg1.GetGenericArguments
                    args2 = t2.GetGenericArguments
                    If args1.Length <> args2.Length Then Return False
                    For i As Integer = 0 To args1.Length - 1
                        If Helper.CompareType(args1(i), args2(i)) = False Then Return False
                    Next
                    Return True
                End If
            Else
                'td1 is a type descriptor, but it does not have a type declaration?
                Return False 'Helper.NotImplemented()
            End If
        End If
    End Function

    ''' <summary>
    ''' Creates a vb-like representation of the parameters
    ''' </summary>
    ''' <param name="Params"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Overloads Shared Function ToString(ByVal Compiler As Compiler, ByVal Params As ParameterInfo()) As String
        Dim result As String = ""
        Dim sep As String = ""

        For Each t As ParameterInfo In Params
            Dim tmp As String
            If t.ParameterType.IsByRef Then
                tmp = "ByRef " & t.ParameterType.GetElementType.ToString
            Else
                tmp = t.ParameterType.ToString
            End If
            If t.IsOptional Then
                tmp = "Optional " & tmp
            End If
            If Helper.IsParamArrayParameter(Compiler, t) Then
                tmp = "ParamArray " & tmp
            End If
            result = result & sep & tmp
            sep = ", "
        Next

        Return "(" & result & ")"

    End Function

    Shared Function IsParamArrayParameter(ByVal Compiler As Compiler, ByVal Parameter As ParameterInfo) As Boolean
        Dim pD As ParameterDescriptor = TryCast(Parameter, ParameterDescriptor)
        Dim result As Boolean
        If pD IsNot Nothing Then
            result = pD.IsParamArray
        Else
            result = Parameter.IsDefined(Compiler.TypeCache.System_ParamArrayAttribute, False)
            LogResolutionMessage(Compiler, "IsParamArrayParameter: result=" & result & ", ParamArrayAttribute=" & Compiler.TypeCache.System_ParamArrayAttribute.FullName)
        End If
        Return result
    End Function

    Overloads Shared Function ToString(ByVal Types As Type()) As String
        Dim result As String = ""
        Dim sep As String = ""

        For Each t As Type In Types
            Helper.Assert(t IsNot Nothing)
            result &= sep & t.ToString
            sep = ", "
        Next

        Return "{" & result & "}"
    End Function

    Overloads Shared Function ToString(ByVal Accessibility As FieldAttributes) As String
        Select Case Accessibility
            Case FieldAttributes.FamANDAssem
                Return "Protected Friend"
            Case FieldAttributes.FamORAssem
                Return "Protected Friend"
            Case FieldAttributes.Family
                Return "Protected"
            Case FieldAttributes.Assembly
                Return "Friend"
            Case FieldAttributes.Public
                Return "Public"
            Case FieldAttributes.Private
                Return "Private"
            Case Else
                Return "<unknown>"
        End Select
    End Function

    Overloads Shared Function ToString(ByVal Accessibility As MethodAttributes) As String
        Select Case Accessibility
            Case MethodAttributes.FamANDAssem
                Return "Protected Friend"
            Case MethodAttributes.FamORAssem
                Return "Protected Friend"
            Case MethodAttributes.Family
                Return "Protected"
            Case MethodAttributes.Assembly
                Return "Friend"
            Case MethodAttributes.Public
                Return "Public"
            Case MethodAttributes.Private
                Return "Private"
            Case Else
                Return "<unknown>"
        End Select
    End Function

    Overloads Shared Function ToString(ByVal Accessibility As TypeAttributes) As String
        Select Case Accessibility
            Case TypeAttributes.NestedFamANDAssem
                Return "Protected Friend"
            Case TypeAttributes.NestedFamORAssem
                Return "Protected Friend"
            Case TypeAttributes.NestedFamANDAssem
                Return "Protected"
            Case TypeAttributes.NestedAssembly, TypeAttributes.NotPublic
                Return "Friend"
            Case TypeAttributes.NestedPublic, TypeAttributes.Public
                Return "Public"
            Case TypeAttributes.NestedPrivate
                Return "Private"
            Case Else
                Return "<unknown>"
        End Select
    End Function

    Overloads Shared Function ToString(ByVal Context As BaseObject, ByVal Member As MemberInfo) As String
        Dim Compiler As Compiler = Context.Compiler
        Dim result As String
        Select Case Member.MemberType
            Case MemberTypes.Constructor
                Dim cinfo As MethodBase = DirectCast(Member, ConstructorInfo)
                result = "Sub New(" & Helper.ToString(Compiler, Helper.GetParameters(Compiler, cinfo)) & ")"
            Case MemberTypes.Method
                Dim minfo As MethodBase = DirectCast(Member, MethodBase)
                result = minfo.Name & "(" & Helper.ToString(Compiler, Helper.GetParameters(Compiler, minfo)) & ")"
            Case MemberTypes.Property
                Dim pinfo As PropertyInfo = DirectCast(Member, PropertyInfo)
                result = pinfo.Name & "(" & Helper.ToString(Compiler, Helper.GetParameters(Compiler, pinfo)) & ")"
            Case Else
                Context.Compiler.Report.ShowMessage(Messages.VBNC99997, Context.Location)
                result = ""
        End Select
        Return result
    End Function

    <Diagnostics.Conditional("DEBUGMETHODRESOLUTION")> Shared Sub LogResolutionMessage(ByVal Compiler As Compiler, ByVal msg As String)
        If LOGMETHODRESOLUTION Then
            Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, msg)
        End If
    End Sub

    <Diagnostics.Conditional("DEBUGMETHODADD")> Shared Sub LogAddMessage(ByVal Compiler As Compiler, ByVal msg As String, Optional ByVal condition As Boolean = True)
        If True AndAlso condition Then
            Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, msg)
        End If
    End Sub

    ''' <summary>
    ''' Creates the expression that is to be emitted for an optional parameter.
    ''' </summary>
    ''' <param name="Parent"></param>
    ''' <param name="Parameter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function GetOptionalValueExpression(ByVal Parent As ParsedObject, ByVal Parameter As ParameterInfo) As Expression
        Dim result As Expression
        If Helper.CompareType(Parameter.ParameterType, Parent.Compiler.TypeCache.System_Object) AndAlso Helper.IsOnMS AndAlso Parameter.RawDefaultValue Is DBNull.Value Then
            'Mono hasn't implemented ParameterInfo.RawDefaultValue yet.

            'If an Object parameter does not specify a default value, then the expression 
            'System.Reflection.Missing.Value is used. 
            result = New LoadFieldExpression(Parent, Parent.Compiler.TypeCache.System_Reflection_Missing__Value)
        ElseIf Helper.CompareType(Parameter.ParameterType, Parent.Compiler.TypeCache.System_Int32) AndAlso Parameter.IsDefined(Parent.Compiler.TypeCache.MS_VB_CS_OptionCompareAttribute, False) Then
            'If an optional Integer parameter 
            'has the Microsoft.VisualBasic.CompilerServices.OptionCompareAttribute attribute, 
            'then the literal 1 is supplied for text comparisons and the literal 0 otherwise
            Dim cExp As ConstantExpression
            If Parent.Location.File(Parent.Compiler).IsOptionCompareText Then
                cExp = New ConstantExpression(Parent, 1I, Parent.Compiler.TypeCache.System_Int32)
            Else
                cExp = New ConstantExpression(Parent, 0I, Parent.Compiler.TypeCache.System_Int32)
            End If
            result = cExp
        Else
            'If optional parameters remain, the default value 
            'specified in the optional parameter declaration is matched to the parameter. 
            Dim cExp As ConstantExpression
            cExp = New ConstantExpression(Parent, Parameter.DefaultValue, Parameter.ParameterType)
            result = cExp
        End If
        Return result
    End Function

    Shared Function ArgumentsToExpressions(ByVal Arguments As Generic.List(Of Argument)) As Expression()
        Dim result(Arguments.Count - 1) As Expression

        For i As Integer = 0 To Arguments.Count - 1
            result(i) = Arguments(i).Expression
        Next

        Return result
    End Function


    Shared Function IsFirstLessGeneric(ByVal Compiler As Compiler, ByVal M As MethodBase, ByVal N As MethodBase) As Boolean

    End Function

    Shared Function IsFirstLessGeneric(ByVal Compiler As Compiler, ByVal M As MethodInfo, ByVal N As MethodInfo) As Boolean
        Dim methodTypeParametersM As Type()
        Dim methodTypeParametersN As Type()

        'A member M is determined to be less generic than a member N using the following steps:

        methodTypeParametersM = M.GetGenericArguments
        methodTypeParametersN = N.GetGenericArguments

        '-	If M has fewer method type parameters than N, then M is less generic than N.
        If methodTypeParametersM.Length < methodTypeParametersN.Length Then
            Return True
        ElseIf methodTypeParametersM.Length > methodTypeParametersN.Length Then
            Return False
        End If

        '-	Otherwise, if for each pair of matching parameters Mj and Nj, Mj and Nj are equally generic with respect
        '   to type parameters on the method, or Mj is less generic with respect to type parameters on the method,
        '   and at least one Mj is less generic than Nj, then M is less generic than N.

        '-	Otherwise, if for each pair of matching parameters Mj and Nj, Mj and Nj are equally generic with respect
        '   to type parameters on the type, or Mj is less generic with respect to type parameters on the type, and 
        '  at least one Mj is less generic than Nj, then M is less generic than N.
    End Function


    Shared Function IsFirstMoreApplicable(ByVal Context As BaseObject, ByVal Arguments As Generic.List(Of Argument), ByVal MTypes As Type(), ByVal NTypes() As Type) As Boolean
        Dim Compiler As Compiler = Context.Compiler
        Dim result As Boolean = True
        'A member M is considered more applicable than N if their signatures are different and, 
        'for each pair of parameters Mj and Nj that matches an argument Aj, 
        'one of the following conditions is true:
        '*	Mj and Nj have identical types, or
        '*	There exists a widening conversion from the type of Mj to the type Nj, or
        '*	Aj is the literal 0, Mj is a numeric type and Nj is an enumerated type, or
        '*	Mj is Byte and Nj is SByte, or
        '*  Mj is Short and Nj is UShort, or
        '*	Mj is Integer and Nj is UInteger, or 
        '*	Mj is Long and Nj is ULong.

        'LAMESPEC?
        'I've found that the previous section must be:
        '*	Mj is Byte and Nj is SByte, or
        '*  Mj is Short/Byte and Nj is UShort, or
        '*	Mj is Integer/Short/Byte and Nj is UInteger, or 
        '*	Mj is Long/Integer/Short/Byte and Nj is ULong.
        'example that doesn't work otherwise:
        ' Two methods with parameter types Int32 and UInt64 which is passed in a UInt16.

        'A member M is considered more applicable than N if their signatures are different 
        If Helper.CompareTypes(MTypes, NTypes) Then
            'Signatures are not different so none is more applicable
            Return False
        End If

        For i As Integer = 0 To Arguments.Count - 1
            Dim is1stMoreApplicable As Boolean
            Dim isEqual, isWidening, isLiteral0 As Boolean
            Dim isMByte, isMShort, isMInteger, isMLong As Boolean
            Dim isNByte, isNShort, isNInteger, isNLong As Boolean
            'Dim isMSigned, isNUnsigned As Boolean 'Names are not accurate for Byte/SByte

            If MTypes.Length - 1 < i OrElse NTypes.Length - 1 < i Then Exit For

            '*	Mj and Nj have identical types, or
            isEqual = Helper.CompareType(MTypes(i), NTypes(i))

            '*	There exists a widening conversion from the type of Mj to the type Nj, or
            isWidening = Compiler.TypeResolution.IsImplicitlyConvertible(context, MTypes(i), NTypes(i))

            '*	Aj is the literal 0, Mj is a numeric type and Nj is an enumerated type, or
            isLiteral0 = IsLiteral0Expression(Compiler, Arguments(i).Expression) AndAlso Compiler.TypeResolution.IsNumericType(MTypes(i)) AndAlso Helper.IsEnum(Compiler, NTypes(i))

            isMByte = Helper.CompareType(MTypes(i), Compiler.TypeCache.System_Byte)
            isMShort = isMByte = False AndAlso Helper.CompareType(MTypes(i), Compiler.TypeCache.System_Int16)
            isMInteger = isMByte = False AndAlso isMShort = False AndAlso Helper.CompareType(MTypes(i), Compiler.TypeCache.System_Int32)
            isMLong = isMByte = False AndAlso isMShort = False AndAlso isMInteger = False AndAlso Helper.CompareType(MTypes(i), Compiler.TypeCache.System_Int64)

            isNByte = Helper.CompareType(NTypes(i), Compiler.TypeCache.System_SByte)
            isNShort = isNByte = False AndAlso Helper.CompareType(NTypes(i), Compiler.TypeCache.System_UInt16)
            isNInteger = isNByte = False AndAlso isNShort = False AndAlso Helper.CompareType(NTypes(i), Compiler.TypeCache.System_UInt32)
            isNLong = isNByte = False AndAlso isNShort = False AndAlso isNInteger = False AndAlso Helper.CompareType(NTypes(i), Compiler.TypeCache.System_uInt64)

            ''*	Mj is Byte and Nj is SByte, or
            'isByte = Helper.CompareType(MTypes(i), Compiler.TypeCache.System_Byte) AndAlso Helper.CompareType(NTypes(i), Compiler.TypeCache.System_SByte)

            ''*	Mj is Short and Nj is UShort, or
            'isShort = Helper.CompareType(MTypes(i), Compiler.TypeCache.System_Int16) AndAlso Helper.CompareType(NTypes(i), Compiler.TypeCache.System_UInt16)

            ''*	Mj is Integer and Nj is UInteger, or 
            'isInteger = Helper.CompareType(MTypes(i), Compiler.TypeCache.System_Int32) AndAlso Helper.CompareType(NTypes(i), Compiler.TypeCache.System_UInt32)

            ''*	Mj is Long and Nj is ULong.
            'isLong = Helper.CompareType(MTypes(i), Compiler.TypeCache.System_Int64) AndAlso Helper.CompareType(NTypes(i), Compiler.TypeCache.System_UInt64)

            is1stMoreApplicable = isEqual OrElse isWidening OrElse isLiteral0
            is1stMoreApplicable = is1stMoreApplicable OrElse (isMByte AndAlso isNByte)
            is1stMoreApplicable = is1stMoreApplicable OrElse ((isMByte OrElse isMShort) AndAlso isNShort)
            is1stMoreApplicable = is1stMoreApplicable OrElse ((isMByte OrElse isMShort OrElse isMInteger) AndAlso isNInteger)
            is1stMoreApplicable = is1stMoreApplicable OrElse ((isMByte OrElse isMShort OrElse isMInteger OrElse isMLong) AndAlso isNLong)
            result = is1stMoreApplicable AndAlso result
        Next

        Return result
    End Function


    Shared Function IsLiteral0Expression(ByVal Compiler As Compiler, ByVal exp As Expression) As Boolean
        If exp Is Nothing Then Return False
        Dim litExp As LiteralExpression = TryCast(exp, LiteralExpression)
        If litExp Is Nothing Then Return False
        If litExp.ConstantValue Is Nothing Then Return False
        If Compiler.TypeResolution.IsIntegralType(litExp.ConstantValue.GetType) = False Then Return False
        If CDbl(litExp.ConstantValue) = 0.0 Then Return True
        Return False
    End Function

    Shared Function IsFirstLessGeneric(ByVal Context As BaseObject) As Boolean
        'A member M is determined to be less generic than a member N using the following steps:
        '-	If M has fewer method type parameters than N, then M is less generic than N.
        '-	Otherwise, if for each pair of matching parameters Mj and Nj, Mj and Nj are equally generic with respect to type parameters on the method, or Mj is less generic with respect to type parameters on the method, and at least one Mj is less generic than Nj, then M is less generic than N.
        '-	Otherwise, if for each pair of matching parameters Mj and Nj, Mj and Nj are equally generic with respect to type parameters on the type, or Mj is less generic with respect to type parameters on the type, and at least one Mj is less generic than Nj, then M is less generic than N.
        Context.Compiler.Report.ShowMessage(Messages.VBNC99997, Context.Location)
    End Function

    Shared Function IsAccessible(ByVal Context As BaseObject, ByVal Caller As TypeDeclaration, ByVal Method As MethodBase) As Boolean
        If Caller Is Nothing Then
            Return Helper.IsAccessible(Context, Method.Attributes, Method.DeclaringType)
        Else
            Return Helper.IsAccessible(Context, Method.Attributes, Method.DeclaringType, Caller.TypeDescriptor)
        End If
    End Function

    Shared Function IsAccessible(ByVal Context As BaseObject, ByVal Caller As TypeDeclaration, ByVal [Property] As PropertyInfo) As Boolean
        If Caller Is Nothing Then
            Return Helper.IsAccessible(Context, GetPropertyAccess([Property]), [Property].DeclaringType)
        Else
            Return Helper.IsAccessible(Context, GetPropertyAccess([Property]), [Property].DeclaringType, Caller.TypeDescriptor)
        End If
    End Function

    Shared Function GetMethodAccessibilityString(ByVal Attributes As MethodAttributes) As String
        Attributes = Attributes And MethodAttributes.MemberAccessMask
        Select Case Attributes
            Case MethodAttributes.Public
                Return "Public"
            Case MethodAttributes.Private
                Return "Private"
            Case MethodAttributes.FamANDAssem, MethodAttributes.FamORAssem
                Return "Protected Friend"
            Case MethodAttributes.Family
                Return "Protected"
            Case MethodAttributes.Assembly
                Return "Friend"
            Case Else
                Return "Public"
        End Select
    End Function

    Shared Function GetMethodAttributes(ByVal Member As MemberInfo) As MethodAttributes
        Select Case Member.MemberType
            Case MemberTypes.Method
                Return DirectCast(Member, MethodInfo).Attributes
            Case MemberTypes.Property
                Return GetPropertyAttributes(DirectCast(Member, PropertyInfo))
            Case Else
                Throw New InternalException("")
        End Select
    End Function

    Shared Function GetVisibility(ByVal Compiler As Compiler, ByVal CallerType As Type, ByVal CalledType As Type) As MemberVisibility
        Helper.Assert(CallerType IsNot Nothing)
        Helper.Assert(CalledType IsNot Nothing)
        Helper.Assert(Compiler.Assembly.IsDefinedHere(CallerType))

        If Helper.CompareType(CallerType, CalledType) Then Return MemberVisibility.All

        If Compiler.Assembly.IsDefinedHere(CalledType) Then
            If Helper.IsNested(CalledType, CallerType) Then
                Return MemberVisibility.All
            ElseIf Helper.IsSubclassOf(CalledType, CallerType) Then
                Return MemberVisibility.PublicProtectedFriend
            Else
                Return MemberVisibility.PublicFriend
            End If
        Else
            If Helper.IsSubclassOf(CalledType, CallerType) Then
                Return MemberVisibility.PublicProtected
            Else
                Return MemberVisibility.Public
            End If
        End If
    End Function

    Shared Function GetVisibilityString(ByVal Member As MemberInfo) As String
        Select Case Member.MemberType
            Case MemberTypes.Constructor
                Dim info As ConstructorInfo = DirectCast(Member, ConstructorInfo)
                Return ToString(info.Attributes)
            Case MemberTypes.Event
                Dim info As EventInfo = DirectCast(Member, EventInfo)
                Return ToString(Helper.GetEventAccess(info))
            Case MemberTypes.Field
                Dim info As FieldInfo = DirectCast(Member, FieldInfo)
                Return ToString(info.Attributes)
            Case MemberTypes.Method
                Dim info As MethodInfo = DirectCast(Member, MethodInfo)
                Return ToString(info.Attributes)
            Case MemberTypes.NestedType
                Dim info As Type = DirectCast(Member, Type)
                Return ToString(info.Attributes)
            Case MemberTypes.Property
                Dim info As PropertyInfo = DirectCast(Member, PropertyInfo)
                Return ToString(Helper.GetPropertyAccess(info))
            Case MemberTypes.TypeInfo
                Dim info As Type = DirectCast(Member, Type)
                Return ToString(info.Attributes)
            Case Else
                Throw New InternalException
        End Select
    End Function

    Shared Function GetPropertyAttributes(ByVal [Property] As PropertyInfo) As MethodAttributes
        Dim result As MethodAttributes
        Dim getA, setA As MethodAttributes
        Dim getM, setM As MethodInfo

        getM = [Property].GetGetMethod(True)
        setM = [Property].GetSetMethod(True)

        Helper.Assert(getM IsNot Nothing OrElse setM IsNot Nothing)

        If getM IsNot Nothing Then
            getA = getM.Attributes
        End If

        If setM IsNot Nothing Then
            setA = setM.Attributes
        End If

        result = setA Or getA

        Dim visibility As MethodAttributes
        visibility = result And MethodAttributes.MemberAccessMask
        If visibility = MethodAttributes.MemberAccessMask Then
            visibility = MethodAttributes.Public
            result = (result And (Not MethodAttributes.MemberAccessMask)) Or visibility
        End If

        Return result
    End Function

    Shared Function GetEventAttributes(ByVal [Event] As EventInfo) As MethodAttributes
        Dim result As MethodAttributes
        Dim getA, setA, raiseA As MethodAttributes
        Dim getM, setM, raiseM As MethodInfo

        getM = [Event].GetAddMethod(True)
        setM = [Event].GetRemoveMethod(True)
        raiseM = [Event].GetRaiseMethod(True)

        Helper.Assert(getM IsNot Nothing OrElse setM IsNot Nothing OrElse raiseM IsNot Nothing)

        If getM IsNot Nothing Then
            getA = getM.Attributes
        End If

        If setM IsNot Nothing Then
            setA = setM.Attributes
        End If

        If raiseM IsNot Nothing Then
            raiseA = raiseM.Attributes
        End If

        result = setA Or getA Or raiseA

        Return result
    End Function

    Shared Function GetPropertyAccess(ByVal [Property] As PropertyInfo) As MethodAttributes
        Dim result As MethodAttributes

        result = GetPropertyAttributes([Property])
        result = result And MethodAttributes.MemberAccessMask

        Return result
    End Function

    Shared Function GetEventAccess(ByVal [Event] As EventInfo) As MethodAttributes
        Dim result As MethodAttributes

        result = GetEventAttributes([Event])
        result = result And MethodAttributes.MemberAccessMask

        Return result
    End Function

    Shared Function IsAccessible(ByVal Context As BaseObject, ByVal Caller As TypeDeclaration, ByVal Member As MemberInfo) As Boolean
        Select Case Member.MemberType
            Case MemberTypes.Constructor, MemberTypes.Method
                Return IsAccessible(Context, Caller, DirectCast(Member, MethodBase))
            Case MemberTypes.Property
                Return IsAccessible(Context, Caller, DirectCast(Member, PropertyInfo))
            Case Else
                Throw New InternalException("")
        End Select
    End Function

    Overloads Shared Function GetParameters(ByVal Context As BaseObject, ByVal Member As MemberInfo) As ParameterInfo()
        Select Case Member.MemberType
            Case MemberTypes.Method
                Return GetParameters(Context, DirectCast(Member, MethodInfo))
            Case MemberTypes.Property
                Return DirectCast(Member, PropertyInfo).GetIndexParameters
            Case MemberTypes.Constructor
                Return DirectCast(Member, ConstructorInfo).GetParameters
            Case Else
                Throw New InternalException("")
        End Select
    End Function

    Overloads Shared Function GetParameters(ByVal Context As BaseObject, ByVal Members As Generic.IList(Of MemberInfo)) As ParameterInfo()()
        Dim result As ParameterInfo()()
        ReDim result(Members.Count - 1)
        For i As Integer = 0 To result.Length - 1
            result(i) = GetParameters(Context, Members(i))
        Next
        Return result
    End Function

    ''' <summary>
    ''' Gets the parameters of the specified constructor 
    ''' </summary>
    ''' <param name="constructor"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Overloads Shared Function GetParameters(ByVal Context As BaseObject, ByVal constructor As ConstructorInfo) As ParameterInfo()
        Dim Compiler As Compiler = Context.Compiler
        If Helper.IsReflectionMember(Context, constructor) Then
            Dim ctor As MemberInfo
            ctor = Compiler.TypeManager.GetRegisteredMember(Context, constructor)
            Helper.Assert(Helper.IsReflectionMember(Context, ctor) = False)
            Return DirectCast(ctor, ConstructorInfo).GetParameters
        Else
            Return constructor.GetParameters
        End If
    End Function

    'Overloads Shared Function GetParameters(ByVal members As MemberInfo()) As ParameterInfo()
    '    Helper.NotImplemented() : Return Nothing
    'End Function

    ''' <summary>
    ''' Gets the parameters of the specified method
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Overloads Shared Function GetParameters(ByVal Context As BaseObject, ByVal method As MethodInfo) As ParameterInfo()
        Dim Compiler As Compiler = Context.Compiler
        If TypeOf method Is MethodDescriptor Then
            Return method.GetParameters()
        End If

        Dim name As String = method.GetType.Name
        If name = "MethodBuilderInstantiation" Then
            Return method.GetGenericMethodDefinition.GetParameters
        ElseIf name = "SymbolMethod" OrElse name = "MonoArrayMethod" Then
            Return CreateArray(Of ParameterInfo)(New ParameterDescriptor(Compiler.TypeCache.System_Int32, 1, Nothing), method.DeclaringType.GetArrayRank())
        Else
            Return method.GetParameters
        End If
        If Compiler.theAss.IsDefinedHere(method.DeclaringType) Then
            Context.Compiler.Report.ShowMessage(Messages.VBNC99997, Context.Location)
            Return Nothing
            'Return DirectCast(Compiler.theAss.FindBuildingType(method.DeclaringType), ContainerType).FindMethod(method).GetParameters()
        Else
            Return method.GetParameters
        End If
    End Function

    ''' <summary>
    ''' Gets the parameters of the specified method
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Overloads Shared Function GetParameters(ByVal Context As BaseObject, ByVal method As MethodBase) As ParameterInfo()
        If TypeOf method Is MethodInfo Then
            Return GetParameters(Context, DirectCast(method, MethodInfo))
        ElseIf TypeOf method Is ConstructorInfo Then
            Return GetParameters(Context, DirectCast(method, ConstructorInfo))
        Else
            Helper.Stop()
            Throw New NotImplementedException
        End If
    End Function

    ''' <summary>
    ''' Adds all the members to the derived class members, unless they are shadowed or overridden
    ''' </summary>
    ''' <param name="DerivedClassMembers"></param>
    ''' <param name="BaseClassMembers"></param>
    ''' <remarks></remarks>
    Shared Sub AddMembers(ByVal Compiler As Compiler, ByVal Type As Type, ByVal DerivedClassMembers As Generic.List(Of MemberInfo), ByVal BaseClassMembers As MemberInfo())
        Dim shadowed As New Generic.List(Of String)
        Dim overridden As New Generic.List(Of String)

        If BaseClassMembers.Length = 0 Then Return

        Helper.Assert(Type IsNot Nothing)
        Dim logging As Boolean

        If Type.BaseType IsNot Nothing Then
            logging = False 'Type.BaseType.Name = "Form"
        End If

        LogAddMessage(Compiler, "", logging)

        If Type.BaseType IsNot Nothing Then
            LogAddMessage(Compiler, String.Format("Adding members to type '{0}' from its base type '{1}'", Type.Name, Type.BaseType.Name), logging)
        Else
            LogAddMessage(Compiler, String.Format("Adding members to type '{0}' from its unknown base type", Type.Name), logging)
        End If

        For Each member As MemberInfo In DerivedClassMembers
            Select Case member.MemberType
                Case MemberTypes.Constructor
                    'Constructors are not added.
                Case MemberTypes.Event
                    'Events can only be shadows
                    shadowed.Add(member.Name)
                Case MemberTypes.Field
                    shadowed.Add(member.Name)
                Case MemberTypes.Method
                    Dim mInfo As MethodInfo = DirectCast(member, MethodInfo)
                    If mInfo.IsHideBySig Then
                        overridden.AddRange(GetOverloadableSignatures(Compiler, mInfo))
                    Else
                        shadowed.Add(mInfo.Name)
                    End If
                Case MemberTypes.NestedType
                    shadowed.Add(member.Name)
                Case MemberTypes.Property
                    Dim pInfo As PropertyInfo = DirectCast(member, PropertyInfo)
                    If CBool(Helper.GetPropertyAttributes(pInfo) And MethodAttributes.HideBySig) Then
                        overridden.AddRange(GetOverloadableSignatures(Compiler, pInfo))
                    Else
                        shadowed.Add(pInfo.Name)
                    End If
                Case MemberTypes.TypeInfo
                    shadowed.Add(member.Name)
                Case Else
                    Throw New InternalException("")
            End Select
        Next

        For i As Integer = 0 To shadowed.Count - 1
            LogAddMessage(Compiler, "Shadows:    " & shadowed(i), logging)
            shadowed(i) = shadowed(i).ToLowerInvariant
        Next
        For i As Integer = 0 To overridden.Count - 1
            LogAddMessage(Compiler, "Overridden: " & overridden(i), logging)
            overridden(i) = overridden(i).ToLowerInvariant
        Next

        For Each member As MemberInfo In BaseClassMembers
            Dim name As String = member.Name.ToLowerInvariant

            If shadowed.Contains(name) Then
                LogAddMessage(Compiler, "Discarded (shadowed): " & name, logging)
                Continue For
            End If


            Select Case member.MemberType
                Case MemberTypes.Constructor
                    LogAddMessage(Compiler, "Discarded (constructor): " & name, logging)
                    Continue For 'Constructors are not added
                Case MemberTypes.Method, MemberTypes.Property
                    Dim signatures As String()
                    Dim found As Boolean

                    If IsAccessibleExternal(Compiler, member) = False Then
                        LogAddMessage(Compiler, "Discarted (not accessible): " & name, logging)
                        Continue For
                    End If

                    found = False
                    signatures = GetOverloadableSignatures(Compiler, member)
                    For Each signature As String In signatures
                        name = signature.ToLowerInvariant
                        If overridden.Contains(name) Then
                            found = True
                            Exit For
                        End If
                    Next
                    If found = True Then
                        LogAddMessage(Compiler, "Discarded (overridden, " & member.MemberType.ToString() & "): " & name, logging)
                        Continue For
                    End If
                Case MemberTypes.Event, MemberTypes.Field, MemberTypes.NestedType, MemberTypes.TypeInfo
                    If IsAccessibleExternal(Compiler, member) = False Then
                        LogAddMessage(Compiler, "Discarted (not accessible): " & name, logging)
                        Continue For
                    End If
                Case Else
                    Throw New InternalException("")
            End Select

            'Not shadowed nor overriden
            LogAddMessage(Compiler, "Added (" & member.MemberType.ToString & "): " & name, logging)
            DerivedClassMembers.Add(member)
        Next

        LogAddMessage(Compiler, "", logging)
    End Sub

    Shared Function IsHideBySig(ByVal Member As MemberInfo) As Boolean
        Select Case Member.MemberType
            Case MemberTypes.Constructor
                Return False
            Case MemberTypes.Event, MemberTypes.Field, MemberTypes.NestedType, MemberTypes.TypeInfo
                Return False
            Case MemberTypes.Property
                Dim pInfo As PropertyInfo = DirectCast(Member, PropertyInfo)
                Return CBool(GetPropertyAttributes(pInfo) And MethodAttributes.HideBySig)
            Case MemberTypes.Method
                Dim mInfo As MethodInfo = DirectCast(Member, MethodInfo)
                Return mInfo.IsHideBySig
            Case Else
                Throw New InternalException("")
        End Select
    End Function


    Shared Function GetOverloadableSignatures(ByVal Compiler As Compiler, ByVal Member As MemberInfo) As String()
        Dim result As New Generic.List(Of String)
        Dim params() As ParameterInfo
        Dim types() As Type
        Dim sep As String = ""

        params = Helper.GetParameters(Compiler, Member)
        types = Helper.GetTypes(params)

        Dim signature As String = ""
        For i As Integer = 0 To types.Length - 1
            If types(i).IsByRef Then types(i) = types(i).GetElementType
            If params(i).IsOptional Then
                result.Add(Member.Name & "(" & signature & ")")
            End If
            signature &= sep & types(i).Namespace & "." & types(i).Name
            sep = ", "
        Next

        result.Add(Member.Name & "(" & signature & ")")

        Return result.ToArray
    End Function

    Shared Function GetCombination(ByVal tp1 As TypeCode, ByVal tp2 As TypeCode) As TypeCombinations
        Return CType(CInt(tp1) << TypeCombinations.SHIFT Or CInt(tp2), TypeCombinations)
    End Function


    Shared Function ShowClassificationError(ByVal Compiler As Compiler, ByVal Location As Span, ByVal ActualClassification As ExpressionClassification, ByVal Expected As String) As Boolean
        Select Case ActualClassification.Classification
            Case ExpressionClassification.Classifications.Type
                Dim tp As Type = ActualClassification.AsTypeClassification.Type
                Return Compiler.Report.ShowMessage(Messages.VBNC30691, Location, tp.Name, tp.Namespace)
            Case ExpressionClassification.Classifications.Value
                Dim vC As ValueClassification = ActualClassification.AsValueClassification
                If vC.IsConstant Then
                    Return Compiler.Report.ShowMessage(Messages.VBNC30074, Location)
                Else
                    Helper.AddError(Compiler, Location, "Expected " & Expected & " got " & ActualClassification.Classification.ToString())
                End If
            Case Else
                Helper.AddError(Compiler, Location, "Expected " & Expected & " got " & ActualClassification.Classification.ToString())
        End Select
        Return False
    End Function
End Class

