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

    Private Shared Function IsMethod(ByVal m1 As Mono.Cecil.MethodReference, ByVal Name As String, ByVal ParameterType As Mono.Cecil.TypeReference, ByVal ReturnType As Mono.Cecil.TypeReference) As Boolean
        If CecilHelper.IsGenericMethod(m1) Then Return False
        If CecilHelper.IsGenericMethodDefinition(m1) Then Return False

        If CompareNameOrdinal(m1.Name, Name) = False Then Return False

        If Helper.CompareType(m1.ReturnType, ReturnType) = False Then Return False

        Dim p1 As Mono.Collections.Generic.Collection(Of Mono.Cecil.ParameterDefinition)
        p1 = m1.Parameters()
        If p1.Count <> 1 Then Return False

        If Helper.CompareType(p1(0).ParameterType, ParameterType) = False Then Return False

        Return True
    End Function

    Public Function IsConstantMethod(ByVal Method As Mono.Cecil.MethodReference, ByVal Parameter As Object, ByRef Result As Object) As Boolean
        If Method.DeclaringType.Namespace IsNot Nothing AndAlso Not CompareNameOrdinal(Method.DeclaringType.Namespace, "Microsoft.VisualBasic") Then Return False
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

    Shared Function CreateList(ByVal types As System.Collections.IEnumerable) As TypeList
        Dim result As New TypeList
        For Each t As IType In types
            result.Add(t.CecilType)
        Next
        Return result
    End Function

    Public Shared Function GetAttributes(ByVal m_Declaration As ConstructorDeclaration) As Mono.Cecil.MethodAttributes
        Dim flags As Mono.Cecil.MethodAttributes
        flags = Mono.Cecil.MethodAttributes.SpecialName Or Mono.Cecil.MethodAttributes.RTSpecialName

        'LAMESPEC: shared constructors have implicit public access.
        'VBC: shared constructors defaults to private.
        'VBC: errors if shared constructors aren't private
        If m_Declaration.IsShared Then
            flags = flags Or Mono.Cecil.MethodAttributes.Private Or Mono.Cecil.MethodAttributes.Static
        Else
            flags = flags Or m_Declaration.Modifiers.GetMethodAttributeScope
        End If

        Return flags
    End Function

    Public Shared Function GetAttributes(ByVal m_Declaration As MethodBaseDeclaration) As Mono.Cecil.MethodAttributes
        Dim result As Mono.Cecil.MethodAttributes
        Dim cd As ConstructorDeclaration = TryCast(m_Declaration, ConstructorDeclaration)

        If cd IsNot Nothing Then Return GetAttributes(cd)

        result = m_Declaration.Modifiers.GetMethodAttributeScope

        'If Modifiers.IsNothing(m_Declaration.Modifiers) = False Then
        If m_Declaration.IsShared Then
            result = result Or Mono.Cecil.MethodAttributes.Static
        End If
        If m_Declaration.Modifiers.Is(ModifierMasks.MustOverride) Then
            If m_Declaration.Modifiers.Is(ModifierMasks.Overrides) = False Then
                result = result Or Mono.Cecil.MethodAttributes.NewSlot
            End If
            result = result Or Mono.Cecil.MethodAttributes.Abstract Or Mono.Cecil.MethodAttributes.Virtual Or Mono.Cecil.MethodAttributes.CheckAccessOnOverride
        End If
        If m_Declaration.Modifiers.Is(ModifierMasks.NotOverridable) Then
            result = result Or Mono.Cecil.MethodAttributes.Final
        End If
        If m_Declaration.Modifiers.Is(ModifierMasks.Overridable) Then
            result = result Or Mono.Cecil.MethodAttributes.NewSlot Or Mono.Cecil.MethodAttributes.Virtual Or Mono.Cecil.MethodAttributes.CheckAccessOnOverride
        End If
        If m_Declaration.Modifiers.Is(ModifierMasks.Overrides) Then
            result = result Or Mono.Cecil.MethodAttributes.Virtual Or Mono.Cecil.MethodAttributes.CheckAccessOnOverride
        End If
        If m_Declaration.Modifiers.Is(ModifierMasks.Overloads) Then
            result = result Or Mono.Cecil.MethodAttributes.HideBySig
        End If
        'End If

        If TypeOf m_Declaration.Parent Is PropertyDeclaration Then
            result = result Or Mono.Cecil.MethodAttributes.SpecialName
        End If

        If TypeOf m_Declaration Is ExternalSubDeclaration Then
            result = result Or Mono.Cecil.MethodAttributes.Static
        End If

        If m_Declaration.HandlesOrImplements IsNot Nothing Then
            If m_Declaration.HandlesOrImplements.ImplementsClause IsNot Nothing Then
                result = result Or Mono.Cecil.MethodAttributes.Virtual Or Mono.Cecil.MethodAttributes.CheckAccessOnOverride
                If m_Declaration.Modifiers.Is(ModifierMasks.Overrides) = False Then
                    result = result Or Mono.Cecil.MethodAttributes.NewSlot
                End If
                If m_Declaration.Modifiers.Is(ModifierMasks.Overridable) = False AndAlso m_Declaration.Modifiers.Is(ModifierMasks.MustOverride) = False AndAlso m_Declaration.Modifiers.Is(ModifierMasks.Overrides) = False Then
                    result = result Or Mono.Cecil.MethodAttributes.Final
                End If
            End If
        End If

        If TypeOf m_Declaration.Parent Is EventDeclaration Then
            If DirectCast(m_Declaration.Parent, EventDeclaration).ImplementsClause IsNot Nothing Then
                result = result Or Mono.Cecil.MethodAttributes.Virtual Or Mono.Cecil.MethodAttributes.NewSlot Or Mono.Cecil.MethodAttributes.CheckAccessOnOverride Or Mono.Cecil.MethodAttributes.Final
            End If
        End If

        If m_Declaration.DeclaringType.IsInterface Then
            result = result Or Mono.Cecil.MethodAttributes.Abstract Or Mono.Cecil.MethodAttributes.Virtual Or Mono.Cecil.MethodAttributes.CheckAccessOnOverride Or Mono.Cecil.MethodAttributes.NewSlot
        End If
        If TypeOf m_Declaration Is OperatorDeclaration OrElse TypeOf m_Declaration Is ConversionOperatorDeclaration Then
            result = result Or Mono.Cecil.MethodAttributes.SpecialName
        ElseIf TypeOf m_Declaration Is EventHandlerDeclaration Then
            result = result Or Mono.Cecil.MethodAttributes.SpecialName
        End If

        If m_Declaration.CustomAttributes IsNot Nothing AndAlso m_Declaration.CustomAttributes.IsDefined(m_Declaration.Compiler.TypeCache.System_Runtime_InteropServices_DllImportAttribute) Then
            result = result Or Mono.Cecil.MethodAttributes.PInvokeImpl
        End If

        If TypeOf m_Declaration Is ExternalSubDeclaration Then
            result = result Or Mono.Cecil.MethodAttributes.PInvokeImpl
        End If

        If m_Declaration.HasSecurityCustomAttribute Then
            result = result Or Mono.Cecil.MethodAttributes.HasSecurity
        End If

        Return result
    End Function


    Public Shared Function GetAttributes(ByVal Compiler As Compiler, ByVal m_Declaration As IFieldMember) As Mono.Cecil.FieldAttributes
        Dim result As Mono.Cecil.FieldAttributes

        If m_Declaration.Modifiers.Is(ModifierMasks.WithEvents) Then
            result = Mono.Cecil.FieldAttributes.Private
        Else
            result = m_Declaration.Modifiers.GetFieldAttributeScope(DirectCast(m_Declaration, BaseObject).FindFirstParent(Of TypeDeclaration))
        End If

        If m_Declaration.Modifiers.Is(ModifierMasks.Static) Then
            result = result Or Mono.Cecil.FieldAttributes.SpecialName
            If DirectCast(m_Declaration, BaseObject).FindFirstParent(Of IMethod).IsShared Then
                result = result Or Mono.Cecil.FieldAttributes.Static
            End If
        End If
        If m_Declaration.Modifiers.Is(ModifierMasks.Shared) OrElse m_Declaration.IsShared Then
            result = result Or Mono.Cecil.FieldAttributes.Static
        End If
        If TypeOf m_Declaration Is EnumMemberDeclaration Then
            result = result Or Mono.Cecil.FieldAttributes.Static Or Mono.Cecil.FieldAttributes.Literal
        End If
        If TypeOf m_Declaration Is ConstantDeclaration Then
            result = result Or Mono.Cecil.FieldAttributes.Static
            If m_Declaration.FieldType IsNot Nothing Then
                If Helper.CompareType(m_Declaration.FieldType, Compiler.TypeCache.System_Decimal) Then
                    result = result Or Mono.Cecil.FieldAttributes.InitOnly
                ElseIf Helper.CompareType(m_Declaration.FieldType, Compiler.TypeCache.System_DateTime) Then
                    result = result Or Mono.Cecil.FieldAttributes.InitOnly
                Else
                    result = result Or Mono.Cecil.FieldAttributes.Literal Or Mono.Cecil.FieldAttributes.HasDefault
                End If
            End If
        End If
        If m_Declaration.Modifiers.Is(ModifierMasks.ReadOnly) Then
            result = result Or Mono.Cecil.FieldAttributes.InitOnly
        End If

        Return result
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
            Helper.StopIfDebugging(result = False)
        Else
            Helper.AddError(Expression)
            result = False
        End If
        Return result
    End Function

    Shared Function IsEmittableMember(ByVal Compiler As Compiler, ByVal Member As Mono.Cecil.MemberReference) As Boolean
        Dim result As Boolean

        If Member Is Nothing Then Return True
        If TypeOf Member Is Mono.Cecil.GenericParameter Then Return True
        If TypeOf Member Is Mono.Cecil.ArrayType Then Return True
        result = FindAssembly(Member) Is Compiler.AssemblyBuilderCecil

        Return result
    End Function

    Shared Function FindAssembly(ByVal member As Mono.Cecil.MemberReference) As Mono.Cecil.AssemblyDefinition
        Helper.Assert(member IsNot Nothing)
        Dim type As Mono.Cecil.TypeReference = TryCast(member, Mono.Cecil.TypeReference)
        If type IsNot Nothing Then Return FindAssembly(type)
        Return FindAssembly(member.DeclaringType)
    End Function

    Shared Function FindAssembly(ByVal type As Mono.Cecil.TypeReference) As Mono.Cecil.AssemblyDefinition
        Helper.Assert(type IsNot Nothing)

        While type.DeclaringType IsNot Nothing
            If type.Module IsNot Nothing AndAlso type.Module.Assembly IsNot Nothing Then Return type.Module.Assembly
            type = type.DeclaringType
        End While
        Dim tS As Mono.Cecil.TypeSpecification = TryCast(type, Mono.Cecil.TypeSpecification)
        While tS IsNot Nothing
            type = tS.ElementType
            tS = TryCast(type, Mono.Cecil.TypeSpecification)
        End While
        'Helper.Assert(type IsNot Nothing AndAlso type.[Module] IsNot Nothing)

        If type Is Nothing OrElse type.Module Is Nothing Then
            Return Nothing
        Else
            Return type.Module.Assembly
        End If
    End Function

    Shared Function GetParameterTypes(ByVal Context As BaseObject, ByVal member As Mono.Cecil.MemberReference) As Mono.Cecil.TypeReference()
        Dim params As Mono.Collections.Generic.Collection(Of ParameterDefinition) = GetParameters(Context, member)
        Dim result() As Mono.Cecil.TypeReference

        If params Is Nothing Then Return Nothing

        ReDim result(params.Count - 1)

        For i As Integer = 0 To params.Count - 1
            result(i) = params(i).ParameterType
        Next

        Return result
    End Function

    Shared Function GetGenericParameters(ByVal Member As MemberReference) As Mono.Collections.Generic.Collection(Of GenericParameter)
        Dim methodReference As MethodReference
        Dim typeReference As TypeReference

        methodReference = TryCast(Member, MethodReference)
        If methodReference IsNot Nothing Then Return CecilHelper.FindDefinition(methodReference).GenericParameters

        typeReference = TryCast(Member, TypeReference)
        If typeReference IsNot Nothing Then Return CecilHelper.FindDefinition(typeReference).GenericParameters

        Return Nothing
    End Function

    Shared Function GetGenericParameterConstraints(ByVal Context As BaseObject, ByVal Type As Mono.Cecil.TypeReference) As Mono.Collections.Generic.Collection(Of TypeReference)
        Dim tG As Mono.Cecil.GenericParameter = TryCast(Type, Mono.Cecil.GenericParameter)

        If tG IsNot Nothing Then Return tG.Constraints

        Dim tD As Mono.Cecil.TypeDefinition = CecilHelper.FindDefinition(Type)
        If CecilHelper.IsGenericParameter(Type) = False Then Throw New InternalException("")
        Throw New NotImplementedException
    End Function

    Shared Function GetNames(ByVal List As IEnumerable) As String()
        Dim result As New Generic.List(Of String)
        For Each item As INameable In List
            result.Add(item.Name)
        Next
        Return result.ToArray
    End Function

    Shared Function GetTypeCode(ByVal Compiler As Compiler, ByVal Type As Mono.Cecil.TypeReference) As TypeCode
        If Helper.IsEnum(Compiler, Type) Then
            Return GetTypeCode(Compiler, Helper.GetEnumType(Compiler, Type))
        ElseIf Helper.CompareType(Type, Compiler.TypeCache.System_Byte) Then
            Return TypeCode.Byte
        ElseIf Helper.CompareType(Type, Compiler.TypeCache.System_Boolean) Then
            Return TypeCode.Boolean
        ElseIf Helper.CompareType(Type, Compiler.TypeCache.System_Char) Then
            Return TypeCode.Char
        ElseIf Helper.CompareType(Type, Compiler.TypeCache.System_DateTime) Then
            Return TypeCode.DateTime
        ElseIf Helper.CompareType(Type, Compiler.TypeCache.System_DBNull) Then
            Return TypeCode.DBNull
        ElseIf Helper.CompareType(Type, Compiler.TypeCache.System_Decimal) Then
            Return TypeCode.Decimal
        ElseIf Helper.CompareType(Type, Compiler.TypeCache.System_Double) Then
            Return TypeCode.Double
        ElseIf Helper.CompareType(Type, Compiler.TypeCache.System_Int16) Then
            Return TypeCode.Int16
        ElseIf Helper.CompareType(Type, Compiler.TypeCache.System_Int32) Then
            Return TypeCode.Int32
        ElseIf Helper.CompareType(Type, Compiler.TypeCache.System_Int64) Then
            Return TypeCode.Int64
        ElseIf Helper.CompareType(Type, Compiler.TypeCache.System_SByte) Then
            Return TypeCode.SByte
        ElseIf Helper.CompareType(Type, Compiler.TypeCache.System_Single) Then
            Return TypeCode.Single
        ElseIf Helper.CompareType(Type, Compiler.TypeCache.System_String) Then
            Return TypeCode.String
        ElseIf Helper.CompareType(Type, Compiler.TypeCache.System_UInt16) Then
            Return TypeCode.UInt16
        ElseIf Helper.CompareType(Type, Compiler.TypeCache.System_UInt32) Then
            Return TypeCode.UInt32
        ElseIf Helper.CompareType(Type, Compiler.TypeCache.System_UInt64) Then
            Return TypeCode.UInt64
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

    Shared Function IsTypeDeclaration(ByVal first As Object) As Boolean
        Return TypeOf first Is IType OrElse TypeOf first Is Mono.Cecil.TypeDefinition
    End Function

    Shared Function IsFieldDeclaration(ByVal first As Object) As Boolean
        Return TypeOf first Is TypeVariableDeclaration OrElse TypeOf first Is Mono.Cecil.FieldReference
    End Function

    ''' <summary>
    ''' Intrinsic type: all basic types and System.Object.
    ''' </summary>
    ''' <param name="Compiler"></param>
    ''' <param name="Type"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function IsIntrinsicType(ByVal Compiler As Compiler, ByVal Type As Mono.Cecil.TypeReference) As Boolean
        Dim tC As TypeCode = GetTypeCode(Compiler, Type)

        If tC = TypeCode.Object Then
            Return Helper.CompareType(Type, Compiler.TypeCache.System_Object)
        Else
            Return True
        End If
    End Function

    Shared Function IsInterface(ByVal Context As BaseObject, ByVal Type As Mono.Cecil.TypeReference) As Boolean
        If TypeOf Type Is Mono.Cecil.GenericParameter Then Return False
        If TypeOf Type Is Mono.Cecil.ArrayType Then Return False
        If TypeOf Type Is ByReferenceType Then Return False
        Return CecilHelper.FindDefinition(Type).IsInterface
    End Function

    Shared Function IsEnum(ByVal Compiler As Compiler, ByVal Type As Mono.Cecil.TypeReference) As Boolean
        If TypeOf Type Is Mono.Cecil.GenericParameter Then Return False
        If TypeOf Type Is Mono.Cecil.ArrayType Then Return False
        If TypeOf Type Is ByReferenceType Then Return False
        Return CecilHelper.FindDefinition(Type).IsEnum
    End Function

    Shared Function IsEnumFieldDeclaration(ByVal Compiler As Compiler, ByVal first As Object) As Boolean
        If TypeOf first Is EnumMemberDeclaration Then Return True
        Dim fld As Mono.Cecil.FieldReference = TryCast(first, Mono.Cecil.FieldReference)
        Return fld IsNot Nothing AndAlso Helper.IsEnum(Compiler, fld.DeclaringType)
    End Function

    Shared Function IsEventDeclaration(ByVal first As Object) As Boolean
        Return TypeOf first Is Mono.Cecil.EventReference
    End Function

    Shared Function IsPropertyDeclaration(ByVal first As Object) As Boolean
        Return TypeOf first Is RegularPropertyDeclaration OrElse TypeOf first Is Mono.Cecil.PropertyReference OrElse TypeOf first Is PropertyDeclaration
    End Function

    Shared Function IsMethodDeclaration(ByVal first As Object) As Boolean
        Return TypeOf first Is SubDeclaration OrElse TypeOf first Is FunctionDeclaration OrElse TypeOf first Is IMethod OrElse TypeOf first Is Mono.Cecil.MethodReference
    End Function

    ''' <summary>
    ''' Returns all the members in the types with the specified name.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function GetMembersOfTypes(ByVal Compiler As Compiler, ByVal Types As TypeDictionary, ByVal Name As String) As Mono.Collections.Generic.Collection(Of Mono.Cecil.MemberReference)
        Dim result As Mono.Collections.Generic.Collection(Of Mono.Cecil.MemberReference) = Nothing

        If Types Is Nothing Then Return Nothing

        For Each type As Mono.Cecil.TypeReference In Types.Values
            Dim members As Mono.Collections.Generic.Collection(Of Mono.Cecil.MemberReference)
            members = Compiler.TypeManager.GetCache(type).LookupFlattenedMembers(Name)
            If members IsNot Nothing Then
                If result Is Nothing Then result = New Mono.Collections.Generic.Collection(Of Mono.Cecil.MemberReference)
                result.AddRange(members)
            End If
        Next

        Return result
    End Function

    ''' <summary>
    ''' Returns all the members in the types with the specified name.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function GetMembersOfTypes(ByVal Compiler As Compiler, ByVal Types As TypeList, ByVal Name As String) As Mono.Collections.Generic.Collection(Of Mono.Cecil.MemberReference)
        Dim result As Mono.Collections.Generic.Collection(Of Mono.Cecil.MemberReference) = Nothing

        If Types Is Nothing Then Return Nothing

        For Each type As Mono.Cecil.TypeReference In Types
            Dim members As Mono.Collections.Generic.Collection(Of Mono.Cecil.MemberReference)
            members = Compiler.TypeManager.GetCache(type).LookupFlattenedMembers(Name)
            If members IsNot Nothing AndAlso members.Count > 0 Then
                If result Is Nothing Then result = New Mono.Collections.Generic.Collection(Of Mono.Cecil.MemberReference)
                result.AddRange(members)
            End If
        Next

        Return result
    End Function

    Shared Function GetInstanceConstructors(ByVal type As Mono.Cecil.TypeReference) As Mono.Collections.Generic.Collection(Of Mono.Cecil.MethodReference)
        Dim result As New Mono.Collections.Generic.Collection(Of Mono.Cecil.MethodReference)
        Dim ctors As Mono.Collections.Generic.Collection(Of MethodReference) = CecilHelper.GetConstructors(type)

        For i As Integer = 0 To ctors.Count - 1
            Dim ctor As Mono.Cecil.MethodReference = DirectCast(ctors(i), Mono.Cecil.MethodReference)
            If Helper.IsShared(ctor) = False Then result.Add(ctor)
        Next

        Return result
    End Function

    ''' <summary>
    ''' Removes private members if they are from an external assembly.
    ''' </summary>
    ''' <param name="Members"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function FilterExternalInaccessible(ByVal Compiler As Compiler, ByVal Members As Mono.Collections.Generic.Collection(Of Mono.Cecil.MemberReference)) As Mono.Collections.Generic.Collection(Of Mono.Cecil.MemberReference)
        Dim result As New Mono.Collections.Generic.Collection(Of Mono.Cecil.MemberReference)

        For i As Integer = 0 To Members.Count - 1
            Dim member As Mono.Cecil.MemberReference = Members(i)
            If (IsPrivate(member) OrElse IsFriend(member)) AndAlso Compiler.Assembly.IsDefinedHere(CecilHelper.FindDefinition(member.DeclaringType)) = False Then
                Continue For
            End If
            result.Add(member)
        Next

        Return result
    End Function

    Shared Function IsProtectedFriend(ByVal Member As Mono.Cecil.MemberReference) As Boolean
        Return GetAccessibility(Member) = (ModifierMasks.Protected Or ModifierMasks.Friend)
    End Function

    Shared Function IsProtectedOrProtectedFriend(ByVal Member As Mono.Cecil.MemberReference) As Boolean
        Return (GetAccessibility(Member) And ModifierMasks.Protected) = ModifierMasks.Protected
    End Function

    Shared Function IsFriendOrProtectedFriend(ByVal Member As Mono.Cecil.MemberReference) As Boolean
        Return (GetAccessibility(Member) And ModifierMasks.Friend) = ModifierMasks.Friend
    End Function

    ''' <summary>
    ''' Checks if the member is Protected (not Protected Friend)
    ''' </summary>
    ''' <param name="Member"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function IsProtected(ByVal Member As Mono.Cecil.MemberReference) As Boolean
        Return GetAccessibility(Member) = ModifierMasks.Protected
    End Function

    Shared Function GetVisibility(ByVal Attributes As Mono.Cecil.MethodAttributes) As ModifierMasks
        Dim attrib As Mono.Cecil.MethodAttributes = Attributes And Mono.Cecil.MethodAttributes.MemberAccessMask
        Select Case attrib
            Case Mono.Cecil.MethodAttributes.Private, Mono.Cecil.MethodAttributes.CompilerControlled
                Return ModifierMasks.Private
            Case Mono.Cecil.MethodAttributes.FamANDAssem
                Throw New NotImplementedException
            Case Mono.Cecil.MethodAttributes.Assembly
                Return ModifierMasks.Friend
            Case Mono.Cecil.MethodAttributes.Family
                Return ModifierMasks.Protected
            Case Mono.Cecil.MethodAttributes.FamORAssem
                Return ModifierMasks.Protected Or ModifierMasks.Friend
            Case Mono.Cecil.MethodAttributes.Public
                Return ModifierMasks.Public
            Case Else
                Throw New InternalException(String.Format("Attributes: {0} = {1}", attrib, CInt(attrib)))
        End Select
    End Function

    Shared Function GetVisibility(ByVal Attributes As Mono.Cecil.FieldAttributes) As ModifierMasks
        Select Case Attributes And Mono.Cecil.FieldAttributes.FieldAccessMask
            Case Mono.Cecil.FieldAttributes.Private
                Return ModifierMasks.Private
            Case Mono.Cecil.FieldAttributes.FamANDAssem
                Throw New NotImplementedException
            Case Mono.Cecil.FieldAttributes.Assembly
                Return ModifierMasks.Friend
            Case Mono.Cecil.FieldAttributes.Family
                Return ModifierMasks.Protected
            Case Mono.Cecil.FieldAttributes.FamORAssem
                Return ModifierMasks.Protected Or ModifierMasks.Friend
            Case Mono.Cecil.FieldAttributes.Public
                Return ModifierMasks.Public
            Case Else
                Throw New InternalException
        End Select
    End Function

    Shared Function GetVisibility(ByVal Attributes As Mono.Cecil.TypeAttributes) As ModifierMasks
        Select Case Attributes And Mono.Cecil.TypeAttributes.VisibilityMask
            Case Mono.Cecil.TypeAttributes.NestedPrivate, Mono.Cecil.TypeAttributes.NotPublic
                Return ModifierMasks.Private
            Case Mono.Cecil.TypeAttributes.NestedFamANDAssem
                Throw New NotImplementedException
            Case Mono.Cecil.TypeAttributes.NestedAssembly
                Return ModifierMasks.Friend
            Case Mono.Cecil.TypeAttributes.NestedFamily
                Return ModifierMasks.Protected
            Case Mono.Cecil.TypeAttributes.NestedFamORAssem
                Return ModifierMasks.Protected Or ModifierMasks.Friend
            Case Mono.Cecil.TypeAttributes.NestedPublic, Mono.Cecil.TypeAttributes.Public
                Return ModifierMasks.Public
            Case Else
                Throw New InternalException
        End Select
    End Function

    ''' <summary>
    ''' Checks if the member is Friend (not Protected Friend)
    ''' </summary>
    ''' <param name="Member"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function GetAccessibility(ByVal Member As Mono.Cecil.MemberReference) As ModifierMasks
        Helper.Assert(Member IsNot Nothing)
        If TypeOf Member Is Mono.Cecil.MethodReference Then
            Return GetVisibility(CecilHelper.FindDefinition(DirectCast(Member, Mono.Cecil.MethodReference)).Attributes)
        ElseIf TypeOf Member Is Mono.Cecil.TypeReference Then
            Return GetVisibility(CecilHelper.FindDefinition(DirectCast(Member, Mono.Cecil.TypeReference)).Attributes)
        ElseIf TypeOf Member Is Mono.Cecil.EventReference Then
            Dim eD As Mono.Cecil.EventDefinition = CecilHelper.FindDefinition(DirectCast(Member, Mono.Cecil.EventReference))
            If eD.AddMethod IsNot Nothing Then Return GetVisibility(eD.AddMethod.Attributes)
            If eD.RemoveMethod IsNot Nothing Then Return GetVisibility(eD.RemoveMethod.Attributes)
            If eD.InvokeMethod IsNot Nothing Then Return GetVisibility(eD.InvokeMethod.Attributes)
            Return 0
        ElseIf TypeOf Member Is Mono.Cecil.FieldReference Then
            Dim fD As Mono.Cecil.FieldDefinition = CecilHelper.FindDefinition(DirectCast(Member, Mono.Cecil.FieldReference))
            Return GetVisibility(fD.Attributes)
        ElseIf TypeOf Member Is Mono.Cecil.PropertyReference Then
            Dim pD As Mono.Cecil.PropertyDefinition = CecilHelper.FindDefinition(DirectCast(Member, Mono.Cecil.PropertyReference))
            Return GetVisibility(GetPropertyAccess(pD))
        Else
            Throw New NotImplementedException
        End If
    End Function

    ''' <summary>
    ''' Checks if the member is Friend (not Protected Friend)
    ''' </summary>
    ''' <param name="Member"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function IsFriend(ByVal Member As Mono.Cecil.MemberReference) As Boolean
        Return GetAccessibility(Member) = ModifierMasks.Friend
    End Function

    Shared Function IsPrivate(ByVal Member As Mono.Cecil.MemberReference) As Boolean
        Return GetAccessibility(Member) = ModifierMasks.Private
    End Function

    Shared Function IsPrivate(ByVal Method As Mono.Cecil.MethodReference) As Boolean
        Dim mD As Mono.Cecil.MethodDefinition

        mD = TryCast(Method, Mono.Cecil.MethodDefinition)

        If mD IsNot Nothing Then Return mD.IsPrivate

        mD = CecilHelper.FindDefinition(Method)

        If mD IsNot Nothing Then Return mD.IsPrivate

        Throw New NotImplementedException
    End Function

    Shared Function IsFamilyOrAssembly(ByVal Method As Mono.Cecil.MethodReference) As Boolean
        Dim mD As Mono.Cecil.MethodDefinition

        mD = TryCast(Method, Mono.Cecil.MethodDefinition)

        If mD IsNot Nothing Then Return mD.IsFamilyOrAssembly

        mD = CecilHelper.FindDefinition(Method)

        If mD IsNot Nothing Then Return mD.IsFamilyOrAssembly

        Throw New NotImplementedException
    End Function

    Shared Function IsPublic(ByVal Member As Mono.Cecil.MemberReference) As Boolean
        Return GetAccessibility(Member) = ModifierMasks.Public
    End Function

    Shared Function FilterByTypeArguments(ByVal Members As Mono.Collections.Generic.Collection(Of Mono.Cecil.MemberReference), ByVal TypeArguments As TypeArgumentList) As Mono.Collections.Generic.Collection(Of Mono.Cecil.MemberReference)
        Dim result As New Mono.Collections.Generic.Collection(Of Mono.Cecil.MemberReference)
        Dim argCount As Integer

        If TypeArguments IsNot Nothing Then argCount = TypeArguments.Count

        For i As Integer = 0 To Members.Count - 1
            Dim member As Mono.Cecil.MemberReference = Members(i)

            Dim minfo As Mono.Cecil.MethodReference = TryCast(member, Mono.Cecil.MethodReference)
            If minfo IsNot Nothing Then
                If CecilHelper.GetGenericArguments(minfo).Count = argCount Then
                    If argCount > 0 Then
                        member = TypeArguments.Parent.Compiler.TypeManager.MakeGenericMethod(TypeArguments.Parent, minfo, CecilHelper.GetGenericArguments(minfo), TypeArguments.ArgumentCollection)
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

    Shared Function FilterByName(ByVal Context As BaseObject, ByVal collection As ICollection, ByVal Name As String) As ArrayList
        Dim result As New ArrayList
        Dim tmpname As String = ""
        For Each obj As Object In collection
            If TypeOf obj Is INameable Then
                tmpname = DirectCast(obj, INameable).Name
            ElseIf TypeOf obj Is Mono.Cecil.MemberReference Then
                tmpname = DirectCast(obj, Mono.Cecil.MemberReference).Name
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

    Shared Sub FilterByName(ByVal collection As Mono.Collections.Generic.Collection(Of Mono.Cecil.TypeReference), ByVal Name As String, ByVal result As Generic.List(Of Mono.Cecil.MemberReference))
        For Each obj As Mono.Cecil.TypeReference In collection
            If Helper.CompareName(Name, obj.Name) Then result.Add(obj)
        Next
    End Sub

    Shared Sub FilterByName(ByVal collection As TypeDictionary, ByVal Name As String, ByVal result As Mono.Collections.Generic.Collection(Of Mono.Cecil.MemberReference))
        For Each obj As Mono.Cecil.TypeReference In collection.Values
            If Helper.CompareName(Name, obj.Name) Then result.Add(obj)
        Next
    End Sub

    Shared Function FilterByName(ByVal Types As TypeList, ByVal Name As String) As TypeList
        Dim result As New TypeList
        For Each obj As Mono.Cecil.TypeReference In Types
            If Helper.CompareName(Name, obj.Name) Then result.Add(obj)
        Next
        Return result
    End Function

    Shared Function FilterByName(ByVal Types As TypeDictionary, ByVal Name As String) As Mono.Cecil.TypeReference
        If Types.ContainsKey(Name) Then
            Return Types(Name)
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Returns a list of type descriptors that only are modules.
    ''' </summary>
    ''' <param name="Types"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function FilterToModules(ByVal Compiler As Compiler, ByVal Types As TypeDictionary) As Generic.List(Of Mono.Cecil.TypeReference)
        Dim result As New Generic.List(Of Mono.Cecil.TypeReference)
        For Each t As Mono.Cecil.TypeReference In Types.Values
            If IsModule(Compiler, t) Then result.Add(t)
        Next
        Return result
    End Function

    Shared Function GetDefaultGenericConstructor(ByVal closedResolvedType As Mono.Cecil.TypeReference) As Mono.Cecil.MethodReference
        Dim result As Mono.Cecil.MethodReference
        Dim candidates As Mono.Collections.Generic.Collection(Of MethodDefinition)

        candidates = CecilHelper.FindDefinition(closedResolvedType).Methods
        result = GetDefaultConstructor(candidates)

        If result IsNot Nothing Then
            result = CecilHelper.GetCorrectMember(result, closedResolvedType)
        End If

        Return result
    End Function

    Shared Function HasOnlyOptionalParameters(ByVal Constructor As Mono.Cecil.MethodDefinition) As Boolean
        Helper.Assert(HasParameters(Constructor))
        Return Constructor.Parameters(0).IsOptional
    End Function

    ''' <summary>
    ''' Returns true if this constructor has any parameter, default or normal parameter.
    ''' </summary>
    ''' <param name="Constructor"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function HasParameters(ByVal Constructor As Mono.Cecil.MethodDefinition) As Boolean
        Return Constructor.Parameters.Count > 0
    End Function

    ''' <summary>
    ''' Finds a non-private, non-shared constructor with no parameters in the array.
    ''' If nothing found, returns nothing.
    ''' </summary>
    ''' <param name="Constructors"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function GetDefaultConstructor(ByVal Constructors As Mono.Collections.Generic.Collection(Of MethodDefinition)) As Mono.Cecil.MethodDefinition
        For i As Integer = 0 To Constructors.Count - 1
            If Not Constructors(i).IsConstructor Then Continue For
            If HasParameters(Constructors(i)) = False OrElse HasOnlyOptionalParameters(Constructors(i)) Then
                If CecilHelper.IsStatic(Constructors(i)) = False AndAlso Helper.IsPrivate(Constructors(i)) = False Then
                    Return Constructors(i)
                End If
            End If
        Next
        Return Nothing
    End Function

    Shared Function GetDefaultConstructor(ByVal tp As Mono.Cecil.TypeReference) As Mono.Cecil.MethodDefinition
        Dim td As TypeDefinition = CecilHelper.FindDefinition(tp)
        If td Is Nothing Then Return Nothing
        Return GetDefaultConstructor(td.Methods)
    End Function

    Shared Function GetParameterTypes(ByVal Parameters As Mono.Cecil.ParameterReference()) As Mono.Cecil.TypeReference()
        Dim result() As Mono.Cecil.TypeReference
        Helper.Assert(Parameters IsNot Nothing)
        ReDim result(Parameters.Length - 1)
        For i As Integer = 0 To Parameters.GetUpperBound(0)
            result(i) = Parameters(i).ParameterType
        Next
        Return result
    End Function

    Shared Function GetParameterTypes(ByVal Parameters As Mono.Collections.Generic.Collection(Of ParameterDefinition)) As Mono.Cecil.TypeReference()
        Dim result() As Mono.Cecil.TypeReference
        Helper.Assert(Parameters IsNot Nothing)
        ReDim result(Parameters.Count - 1)
        For i As Integer = 0 To Parameters.Count - 1
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
    Shared Function IsModule(ByVal Compiler As Compiler, ByVal type As Mono.Cecil.TypeReference) As Boolean
        Dim result As Boolean
        Dim annotation As Object = type.Annotations(Compiler)

        If annotation IsNot Nothing AndAlso TypeOf annotation Is ModuleDeclaration Then Return True

        result = CecilHelper.IsClass(type) AndAlso Compiler.TypeCache.MS_VB_CS_StandardModuleAttribute IsNot Nothing AndAlso CecilHelper.IsDefined(CecilHelper.FindDefinition(type).CustomAttributes, Compiler.TypeCache.MS_VB_CS_StandardModuleAttribute)

        Return result
    End Function

    ''' <summary>
    ''' Creates an integer array of the arguments.
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <param name="Arguments"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function EmitIntegerArray(ByVal Info As EmitInfo, ByVal Arguments As Expression()) As Boolean
        Dim result As Boolean = True

        Dim arrayType As Mono.Cecil.TypeReference = Info.Compiler.TypeCache.System_Int32_Array
        Dim elementType As Mono.Cecil.TypeReference = CecilHelper.GetElementType(arrayType)
        Dim tmpVar As Mono.Cecil.Cil.VariableDefinition = Emitter.DeclareLocal(Info, arrayType)
        Dim elementInfo As EmitInfo = Info.Clone(Info.Context, True, False, elementType)

        'Create the array.
        ArrayCreationExpression.EmitArrayCreation(Info, arrayType, New Generic.List(Of Integer)(New Integer() {Arguments.Length}))

        'Save it into a temporary variable.
        Emitter.EmitStoreVariable(Info, tmpVar)

        'Store every element into its index in the array.
        For i As Integer = 0 To Arguments.Length - 1
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

    ''' <summary>
    ''' Creates an integer array of the arguments.
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <param name="Arguments"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function EmitIntegerArray(ByVal Info As EmitInfo, ByVal Arguments As ArgumentList) As Boolean
        Dim result As Boolean = True

        Dim arrayType As Mono.Cecil.TypeReference = Info.Compiler.TypeCache.System_Int32_Array
        Dim elementType As Mono.Cecil.TypeReference = CecilHelper.GetElementType(arrayType)
        Dim tmpVar As Mono.Cecil.Cil.VariableDefinition = Emitter.DeclareLocal(Info, arrayType)
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
        Dim ArrayType As Mono.Cecil.TypeReference = ArrayVariable.ExpressionType
        Dim ElementType As Mono.Cecil.TypeReference = CecilHelper.GetElementType(ArrayType)
        Dim isNonPrimitiveValueType As Boolean = CecilHelper.IsPrimitive(Info.Compiler, ElementType) = False AndAlso CecilHelper.IsValueType(ElementType)
        Dim isArraySetValue As Boolean = CecilHelper.GetArrayRank(ArrayType) > 1
        Dim newValue As Expression = Info.RHSExpression

        Helper.Assert(newValue IsNot Nothing)
        Helper.Assert(newValue.Classification.IsValueClassification)

        result = ArrayVariable.GenerateCode(Info.Clone(Info.Context, True, False, ArrayType)) AndAlso result

        If isArraySetValue Then
            result = newValue.GenerateCode(Info.Clone(Info.Context, True, False, ElementType)) AndAlso result
            If CecilHelper.IsValueType(ElementType) Then
                Emitter.EmitBox(Info, ElementType)
            End If
            result = EmitIntegerArray(Info, Arguments) AndAlso result
            Emitter.EmitCallOrCallVirt(Info, Info.Compiler.TypeCache.System_Array__SetValue)
        Else
            Dim methodtypes As New Generic.List(Of Mono.Cecil.TypeReference)
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
        Dim ArrayType As Mono.Cecil.TypeReference = ArrayVariable.ExpressionType
        Dim ElementType As Mono.Cecil.TypeReference = CecilHelper.GetElementType(ArrayType)
        Dim isNonPrimitiveValueType As Boolean = CecilHelper.IsPrimitive(Info.Compiler, ElementType) = False AndAlso CecilHelper.IsValueType(ElementType)
        Dim isArrayGetValue As Boolean = CecilHelper.GetArrayRank(ArrayType) > 1

        result = ArrayVariable.GenerateCode(Info) AndAlso result

        If isArrayGetValue Then
            result = Arguments.GenerateCode(Info, Helper.CreateArray(Of Mono.Cecil.TypeReference)(Info.Compiler.TypeCache.System_Int32, Arguments.Length)) AndAlso result
            'result = EmitIntegerArray(Info, Arguments) AndAlso result
            Dim getMethod As Mono.Cecil.MethodReference
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
            Dim methodtypes(Arguments.Count - 1) As Mono.Cecil.TypeReference
            For i As Integer = 0 To Arguments.Count - 1
                Dim exp As Expression = Arguments(i).Expression
                If Info.Compiler.TypeResolution.IsImplicitlyConvertible(Compiler.m_Compiler, exp.ExpressionType, Info.Compiler.TypeCache.System_Int32) = False Then
                    'TODO: This should be done during resultion, not emission
                    exp = New CIntExpression(exp, exp)
                End If
                result = exp.GenerateCode(elementInfo) AndAlso result
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
    Shared Function EmitArgumentsAndCallOrCallVirt(ByVal Info As EmitInfo, ByVal InstanceExpression As Expression, ByVal Arguments As ArgumentList, ByVal Method As Mono.Cecil.MethodReference) As Boolean
        Dim result As Boolean = True
        Dim needsConstrained As Boolean
        Dim constrainedLocal As Mono.Cecil.Cil.VariableDefinition = Nothing

        needsConstrained = InstanceExpression IsNot Nothing AndAlso CecilHelper.IsGenericParameter(InstanceExpression.ExpressionType)

        Helper.Assert(Method IsNot Nothing)

        If InstanceExpression IsNot Nothing AndAlso CecilHelper.FindDefinition(Method).IsStatic = False Then
            Dim ieDesiredType As Mono.Cecil.TypeReference
            Dim ieInfo As EmitInfo

            If needsConstrained Then
                ieDesiredType = InstanceExpression.ExpressionType
            Else
                ieDesiredType = Method.DeclaringType
                If CecilHelper.IsValueType(ieDesiredType) Then
                    ieDesiredType = Info.Compiler.TypeManager.MakeByRefType(CType(Info.Method, ParsedObject), ieDesiredType)
                End If
            End If

            ieInfo = Info.Clone(Info.Context, True, False, ieDesiredType)

            Dim derefExp As DeRefExpression = TryCast(InstanceExpression, DeRefExpression)
            If needsConstrained AndAlso derefExp IsNot Nothing Then
                result = derefExp.Expression.GenerateCode(Info.Clone(Info.Context, True, False, derefExp.Expression.ExpressionType)) AndAlso result
            Else
                Dim getRef As GetRefExpression = TryCast(InstanceExpression, GetRefExpression)
                If getRef IsNot Nothing AndAlso CecilHelper.IsValueType(getRef.Expression.ExpressionType) AndAlso Helper.CompareType(Method.DeclaringType, Info.Compiler.TypeCache.System_Object) Then
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

        Dim copyBacksA As Generic.List(Of Mono.Cecil.Cil.VariableDefinition) = Nothing
        Dim copyBacksB As Generic.List(Of Expression) = Nothing

        If Arguments IsNot Nothing Then
            Dim methodParameters As Mono.Collections.Generic.Collection(Of ParameterDefinition)
            methodParameters = Helper.GetParameters(Info.Compiler, Method)

            For i As Integer = 0 To methodParameters.Count - 1
                Dim arg As Argument
                Dim exp As Expression
                Dim local As Mono.Cecil.Cil.VariableDefinition
                Dim propAccess As PropertyAccessClassification

                If CecilHelper.IsByRef(methodParameters(i).ParameterType) = False Then Continue For

                arg = Arguments.Arguments(i)
                exp = arg.Expression

                If exp Is Nothing Then Continue For

                If exp.Classification Is Nothing Then Continue For
                If exp.Classification.IsPropertyAccessClassification = False Then Continue For

                propAccess = exp.Classification.AsPropertyAccess

                If copyBacksA Is Nothing Then
                    copyBacksA = New Generic.List(Of Mono.Cecil.Cil.VariableDefinition)
                    copyBacksB = New Generic.List(Of Expression)
                End If
                local = Emitter.DeclareLocal(Info, CecilHelper.GetElementType(methodParameters(i).ParameterType))
                copyBacksA.Add(local)
                If CecilHelper.FindDefinition(propAccess.Property).SetMethod Is Nothing Then
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
                Dim local As Mono.Cecil.Cil.VariableDefinition = copyBacksA(i)
                Dim exp As Expression = copyBacksB(i)

                If exp Is Nothing Then Continue For

                result = exp.GenerateCode(Info.Clone(Info.Context, New LoadLocalExpression(exp, local))) AndAlso result
            Next
        End If

        If constrainedLocal IsNot Nothing Then
            Emitter.FreeLocal(constrainedLocal)
        End If

        If Info.DesiredType IsNot Nothing AndAlso CecilHelper.IsByRef(Info.DesiredType) Then
            Dim tmp As Mono.Cecil.Cil.VariableDefinition
            tmp = Emitter.DeclareLocal(Info, CecilHelper.GetElementType(Info.DesiredType))
            Emitter.EmitStoreVariable(Info, tmp)
            Emitter.EmitLoadVariableLocation(Info, tmp)
            Emitter.FreeLocal(tmp)
        End If

        Return result
    End Function

    Shared Function GetInvokeMethod(ByVal Compiler As Compiler, ByVal DelegateType As Mono.Cecil.TypeReference) As Mono.Cecil.MethodReference
        Helper.Assert(IsDelegate(Compiler, DelegateType), "The type '" & DelegateType.FullName & "' is not a delegate.")
        Dim results As Mono.Collections.Generic.Collection(Of Mono.Cecil.MemberReference) = Compiler.TypeManager.GetCache(DelegateType).Lookup(DelegateDeclaration.STR_Invoke, MemberVisibility.Public).Members
        If results IsNot Nothing AndAlso results.Count = 1 AndAlso TypeOf results(0) Is Mono.Cecil.MethodReference Then
            Return DirectCast(results(0), Mono.Cecil.MethodReference)
        Else
            Throw New NotImplementedException
        End If
    End Function

    Shared Function IsDelegate(ByVal Compiler As Compiler, ByVal Type As Mono.Cecil.TypeReference) As Boolean
        Return Helper.IsSubclassOf(Compiler.TypeCache.System_MulticastDelegate, Type)
    End Function

    Public Shared Function CompareParameterTypes(ByVal a As Mono.Collections.Generic.Collection(Of ParameterDefinition), ByVal b As Mono.Collections.Generic.Collection(Of ParameterDefinition)) As Boolean
        If a.Count <> b.Count Then Return False
        For i As Integer = 0 To a.Count - 1
            If Helper.CompareType(a(i).ParameterType, b(i).ParameterType) = False Then Return False
        Next
        Return True
    End Function

    Private Shared Sub AddPropertyUnlessSignatureMatches(ByVal properties As Mono.Collections.Generic.Collection(Of Mono.Cecil.PropertyReference), ByVal prop As Mono.Cecil.PropertyReference)
        For i As Integer = 0 To properties.Count - 1
            If Helper.CompareParameterTypes(prop.Parameters, properties(i).Parameters) = True Then Return
        Next
        properties.Add(prop)
    End Sub


    ''' <summary>
    ''' Returns true if the type has a default property
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function HasDefaultProperty(ByVal Context As BaseObject, ByVal tp As Mono.Cecil.TypeReference, ByRef properties As Mono.Collections.Generic.Collection(Of Mono.Cecil.PropertyReference)) As Boolean
        Dim Compiler As Compiler = Context.Compiler
        Dim members As Mono.Collections.Generic.Collection(Of Mono.Cecil.MemberReference)
        Dim defaultName As String = Nothing

        If tp Is Nothing Then Return False

        If properties Is Nothing Then properties = New Mono.Collections.Generic.Collection(Of Mono.Cecil.PropertyReference)
        members = Compiler.TypeManager.GetCache(tp).GetAllMembers()

        For i As Integer = 0 To members.Count - 1
            Dim p As Mono.Cecil.PropertyReference = TryCast(members(i), Mono.Cecil.PropertyReference)
            Dim pD As PropertyDeclaration

            If p Is Nothing Then Continue For

            If p.Annotations.Contains(Compiler) Then
                pD = DirectCast(p.Annotations(Compiler), PropertyDeclaration)
                If pD.Modifiers.Is(ModifierMasks.Default) Then
                    AddPropertyUnlessSignatureMatches(properties, p)
                End If
                Continue For
            End If

            Dim p2 As Mono.Cecil.PropertyReference = CecilHelper.FindDefinition(p)
            If p2.Annotations.Contains(Compiler) Then
                pD = DirectCast(p2.Annotations(Compiler), PropertyDeclaration)
                If pD.Modifiers.Is(ModifierMasks.Default) Then
                    AddPropertyUnlessSignatureMatches(properties, p)
                End If
                Continue For
            End If

            'OPTIMIZATION: cache default attribute per type
            Dim pDef As Mono.Cecil.TypeDefinition = CecilHelper.FindDefinition(p.DeclaringType)
            Dim defaultAttribute As Mono.Cecil.CustomAttribute = Helper.GetDefaultMemberAttribute(Compiler, pDef)
            If defaultAttribute Is Nothing Then Continue For
            If defaultAttribute.ConstructorArguments.Count <> 1 Then Continue For
            If TypeOf defaultAttribute.ConstructorArguments(0).Value Is String = False Then Continue For
            defaultName = DirectCast(defaultAttribute.ConstructorArguments(0).Value, String)

            If Helper.CompareNameOrdinal(p.Name, defaultName) Then
                AddPropertyUnlessSignatureMatches(properties, p)
            End If
        Next

        If Helper.CompareType(Compiler.TypeCache.System_Object, tp) = False Then
            If CecilHelper.IsInterface(tp) Then
                Dim interfaces As Mono.Collections.Generic.Collection(Of TypeReference) = CecilHelper.GetInterfaces(tp, False)
                Dim result As Boolean
                If interfaces IsNot Nothing Then
                    For i As Integer = 0 To interfaces.Count - 1
                        result = HasDefaultProperty(Context, interfaces(i), properties) OrElse result
                    Next
                End If
                Return properties.Count > 0
            Else
                Return HasDefaultProperty(Context, CecilHelper.GetBaseType(tp), properties)
            End If
        End If

        Return properties.Count > 0
    End Function

    Shared Function GetDefaultMemberAttribute(ByVal Compiler As Compiler, ByVal Type As Mono.Cecil.TypeReference) As Mono.Cecil.CustomAttribute
        Dim attribs As Mono.Collections.Generic.Collection(Of CustomAttribute)
        Dim attrib As Mono.Cecil.CustomAttribute = Nothing
        Dim tD As Mono.Cecil.TypeDefinition = CecilHelper.FindDefinition(Type)

        attribs = CecilHelper.GetCustomAttributes(tD, Compiler.TypeCache.System_Reflection_DefaultMemberAttribute)

        If attribs IsNot Nothing AndAlso attribs.Count = 1 Then
            attrib = attribs(0)
        End If

        Return attrib
    End Function

    Shared Function IsDefaultProperty(ByVal Compiler As Compiler, ByVal P As PropertyDefinition) As Boolean
        Dim ca As CustomAttribute = GetDefaultMemberAttribute(Compiler, P.DeclaringType)
        If ca Is Nothing Then Return False
        Return Helper.CompareName(DirectCast(ca.ConstructorArguments(0).Value, String), P.Name)
    End Function


    Shared Function IsShadows(ByVal Context As BaseObject, ByVal Member As Mono.Cecil.MemberReference) As Boolean
        Dim result As Boolean = True
        Select Case CecilHelper.GetMemberType(Member)
            Case MemberTypes.Method, MemberTypes.Constructor
                Return CecilHelper.FindDefinition(DirectCast(Member, Mono.Cecil.MethodReference)).IsHideBySig = False
            Case MemberTypes.Property
                Return CBool(Helper.GetPropertyAttributes(DirectCast(Member, Mono.Cecil.PropertyReference)) And Mono.Cecil.MethodAttributes.HideBySig) = False
            Case MemberTypes.Field
                Return True
            Case MemberTypes.TypeInfo
                Return True
            Case MemberTypes.NestedType
                Return True
            Case MemberTypes.Event
                Return CecilHelper.FindDefinition(DirectCast(Member, Mono.Cecil.EventReference)).AddMethod.IsHideBySig = False
            Case Else
                Context.Compiler.Report.ShowMessage(Messages.VBNC99997, Context.Location)
        End Select
        Return False
    End Function

    Shared Function IsShared(ByVal Member As Mono.Cecil.MemberReference) As Boolean
        Dim result As Boolean = True
        Select Case CecilHelper.GetMemberType(Member)
            Case MemberTypes.Method, MemberTypes.Constructor
                Return CecilHelper.FindDefinition(DirectCast(Member, Mono.Cecil.MethodReference)).IsStatic
            Case MemberTypes.Property
                Dim pInfo As Mono.Cecil.PropertyDefinition = CecilHelper.FindDefinition(DirectCast(Member, Mono.Cecil.PropertyReference))
                Return CecilHelper.IsStatic(pInfo)
            Case MemberTypes.Field
                Dim fInfo As Mono.Cecil.FieldDefinition = CecilHelper.FindDefinition(DirectCast(Member, Mono.Cecil.FieldReference))
                Return fInfo.IsStatic
            Case MemberTypes.TypeInfo
                Return False
            Case MemberTypes.NestedType
                Return False
            Case MemberTypes.Event
                Return CecilHelper.FindDefinition(DirectCast(Member, Mono.Cecil.EventReference)).AddMethod.IsStatic
            Case Else
                Throw New InternalException("")
        End Select
    End Function

    Shared Function GetTypes(ByVal Params As Mono.Collections.Generic.Collection(Of ParameterDefinition)) As Mono.Cecil.TypeReference()
        Dim result() As Mono.Cecil.TypeReference = Nothing

        If Params Is Nothing Then Return result
        ReDim result(Params.Count - 1)
        For i As Integer = 0 To Params.Count - 1
            result(i) = Params(i).ParameterType
        Next
        Return result
    End Function

    Shared Function GetReturnType(ByVal Member As MemberReference) As TypeReference
        Dim mr As MethodReference = TryCast(Member, MethodReference)
        If mr IsNot Nothing Then Return mr.ReturnType

        Return Nothing
    End Function

    Shared Function GetTypes(ByVal Arguments As Generic.List(Of Argument)) As Mono.Cecil.TypeReference()
        Dim result() As Mono.Cecil.TypeReference = New Mono.Cecil.TypeReference() {}

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

    Shared Function GetTypeDefinition(ByVal Compiler As Compiler, ByVal Type As Mono.Cecil.TypeReference) As Mono.Cecil.TypeReference
        Return CecilHelper.FindDefinition(Type)
    End Function

    Shared Function GetTypeOrTypeReference(ByVal Compiler As Compiler, ByVal Type As Mono.Cecil.TypeReference) As Mono.Cecil.TypeReference
        If Type Is Nothing Then Return Nothing
        If TypeOf Type Is Mono.Cecil.GenericParameter Then Return Type

        If Compiler.Assembly.IsDefinedHere(Type) Then
            Return Type
        ElseIf TypeOf Type Is ByReferenceType Then
            Dim refType As ByReferenceType = DirectCast(Type, ByReferenceType)
            Dim elementType As Mono.Cecil.TypeReference
            elementType = GetTypeOrTypeReference(Compiler, refType.ElementType)
            If elementType Is refType.ElementType Then Return Type
            Return New ByReferenceType(elementType)
        ElseIf TypeOf Type Is Mono.Cecil.ArrayType Then
            Dim arrType As Mono.Cecil.ArrayType = DirectCast(Type, Mono.Cecil.ArrayType)
            Dim elementType As Mono.Cecil.TypeReference
            elementType = GetTypeOrTypeReference(Compiler, arrType.ElementType)
            If elementType Is arrType.ElementType Then Return Type
            Dim result As Mono.Cecil.ArrayType = New Mono.Cecil.ArrayType(elementType, arrType.Rank)
            For i As Integer = 0 To arrType.Rank - 1
                result.Dimensions(i) = arrType.Dimensions(i)
            Next
            Return result
        ElseIf TypeOf Type Is Mono.Cecil.GenericInstanceType Then
            Dim git As Mono.Cecil.GenericInstanceType = DirectCast(Type, Mono.Cecil.GenericInstanceType)
            Dim elementType As Mono.Cecil.TypeReference = GetTypeOrTypeReference(Compiler, git.ElementType)
            Dim result As New Mono.Cecil.GenericInstanceType(elementType)
            For i As Integer = 0 To git.GenericArguments.Count - 1
                result.GenericArguments.Add(GetTypeOrTypeReference(Compiler, git.GenericArguments(i)))
            Next
            Return result
        Else
            Return Compiler.AssemblyBuilderCecil.MainModule.Import(CecilHelper.FindDefinition(Type))
        End If
    End Function

    Private Shared Function GetTypeReference(ByVal Compiler As Compiler, ByVal Type As Mono.Cecil.TypeReference) As Mono.Cecil.TypeReference
        Return GetTypeOrTypeReference(Compiler, Type)
    End Function

    Shared Sub ApplyTypeArguments(ByVal Context As BaseObject, ByVal Members As Mono.Collections.Generic.Collection(Of Mono.Cecil.MemberReference), ByVal TypeArguments As TypeArgumentList)
        If TypeArguments Is Nothing OrElse TypeArguments.Count = 0 Then Return

        For i As Integer = Members.Count - 1 To 0 Step -1
            Members(i) = ApplyTypeArguments(Context, Members(i), TypeArguments)
            If Members(i) Is Nothing Then Members.RemoveAt(i)
        Next

    End Sub

    Shared Function ApplyTypeArguments(ByVal Context As BaseObject, ByVal Member As Mono.Cecil.MemberReference, ByVal TypeArguments As TypeArgumentList) As Mono.Cecil.MemberReference
        Dim result As Mono.Cecil.MemberReference
        Dim minfo As Mono.Cecil.MethodReference

        minfo = TryCast(Member, Mono.Cecil.MethodReference)
        If minfo IsNot Nothing Then
            Dim args As Mono.Collections.Generic.Collection(Of Mono.Cecil.TypeReference)
            args = CecilHelper.GetGenericArguments(minfo)

            If args.Count = TypeArguments.Count Then
                result = TypeArguments.Compiler.TypeManager.MakeGenericMethod(TypeArguments.Parent, minfo, args, TypeArguments.ArgumentCollection)
            Else
                result = Nothing
            End If
        Else
            result = Nothing
            Context.Compiler.Report.ShowMessage(Messages.VBNC99997, Context.Location)
        End If

        Return result
    End Function

    Shared Function ApplyTypeArguments(ByVal Parent As ParsedObject, ByVal OpenType As Mono.Cecil.TypeReference, ByVal TypeParameters As Mono.Collections.Generic.Collection(Of TypeReference), ByVal TypeArguments As Mono.Collections.Generic.Collection(Of TypeReference)) As Mono.Cecil.TypeReference
        Dim result As Mono.Cecil.TypeReference = Nothing

        If OpenType Is Nothing Then Return Nothing

        Helper.Assert(TypeParameters IsNot Nothing AndAlso TypeArguments IsNot Nothing)
        Helper.Assert(TypeParameters.Count = TypeArguments.Count)

        If CecilHelper.IsGenericParameter(OpenType) Then
            For i As Integer = 0 To TypeParameters.Count - 1
                If Helper.CompareName(TypeParameters(i).Name, OpenType.Name) Then
                    result = TypeArguments(i)
                    Exit For
                End If
            Next
            Helper.Assert(result IsNot Nothing)
        ElseIf CecilHelper.IsGenericType(OpenType) Then
            Dim typeParams As Mono.Collections.Generic.Collection(Of TypeReference)
            Dim typeArgs As New Mono.Collections.Generic.Collection(Of TypeReference)

            typeParams = CecilHelper.GetGenericArguments(OpenType)

            For i As Integer = 0 To typeParams.Count - 1
                For j As Integer = 0 To TypeParameters.Count - 1
                    If Helper.CompareName(typeParams(i).Name, TypeParameters(j).Name) Then
                        typeArgs.Add(TypeArguments(j))
                        Exit For
                    End If
                Next
                If typeArgs.Count - 1 < i Then typeArgs.Add(typeParams(i))
            Next

            Helper.Assert(typeArgs.Count = typeParams.Count AndAlso typeArgs.Count > 0)

            result = Parent.Compiler.TypeManager.MakeGenericType(Parent, OpenType, typeArgs)
        ElseIf CecilHelper.IsGenericTypeDefinition(OpenType) Then
            Parent.Compiler.Report.ShowMessage(Messages.VBNC99997, Parent.Location)
        ElseIf CecilHelper.ContainsGenericParameters(OpenType) Then
            If CecilHelper.IsArray(OpenType) Then
                Dim elementType As Mono.Cecil.TypeReference
                elementType = CecilHelper.GetElementType(OpenType)
                elementType = ApplyTypeArguments(Parent, elementType, TypeParameters, TypeArguments)
                result = New Mono.Cecil.ArrayType(elementType, CecilHelper.GetArrayRank(OpenType))
            ElseIf CecilHelper.IsByRef(OpenType) Then
                Dim elementType As Mono.Cecil.TypeReference
                elementType = CecilHelper.GetElementType(OpenType)
                elementType = ApplyTypeArguments(Parent, elementType, TypeParameters, TypeArguments)
                result = Parent.Compiler.TypeManager.MakeByRefType(Parent, elementType)
            Else
                Parent.Compiler.Report.ShowMessage(Messages.VBNC99997, Parent.Location)
            End If
        Else
            result = OpenType
        End If

        Helper.Assert(result IsNot Nothing)

        Return result
    End Function

    Shared Function ApplyTypeArguments(ByVal Parent As ParsedObject, ByVal OpenParameter As Mono.Cecil.ParameterReference, ByVal TypeParameters As Mono.Collections.Generic.Collection(Of TypeReference), ByVal TypeArguments As Mono.Collections.Generic.Collection(Of TypeReference)) As Mono.Cecil.ParameterReference
        Dim result As Mono.Cecil.ParameterReference

        Helper.Assert(TypeParameters IsNot Nothing AndAlso TypeArguments IsNot Nothing)
        Helper.Assert(TypeParameters.Count = TypeArguments.Count)

        Dim paramType As Mono.Cecil.TypeReference
        paramType = ApplyTypeArguments(Parent, OpenParameter.ParameterType, TypeParameters, TypeArguments)

        If paramType Is OpenParameter.ParameterType Then
            result = OpenParameter
        Else
            result = Parent.Compiler.TypeManager.MakeGenericParameter(Parent, OpenParameter, paramType)
        End If

        Helper.Assert(result IsNot Nothing)

        Return result
    End Function

    Shared Function ApplyTypeArguments(ByVal Parent As ParsedObject, ByVal OpenParameters As Mono.Collections.Generic.Collection(Of ParameterReference), ByVal TypeParameters As Mono.Collections.Generic.Collection(Of TypeReference), ByVal TypeArguments As Mono.Collections.Generic.Collection(Of TypeReference)) As Mono.Cecil.ParameterReference()
        Dim result(OpenParameters.Count - 1) As Mono.Cecil.ParameterReference

        For i As Integer = 0 To result.Length - 1
            result(i) = ApplyTypeArguments(Parent, OpenParameters(i), TypeParameters, TypeArguments)
        Next

        Return result
    End Function

    Shared Function GetConversionOperators(ByVal Compiler As Compiler, ByVal Names As String(), ByVal Type As Mono.Cecil.TypeReference, ByVal ReturnType As Mono.Cecil.TypeReference) As Generic.List(Of Mono.Cecil.MethodReference)
        Dim ops As Generic.List(Of Mono.Cecil.MethodReference)

        ops = GetOperators(Compiler, Names, Type)

        If ops Is Nothing Then
            ops = GetOperators(Compiler, Names, ReturnType)
        Else
            ops.AddRange(GetOperators(Compiler, Names, ReturnType))
        End If

        If ops IsNot Nothing Then
            For i As Integer = ops.Count - 1 To 0 Step -1
                If CompareType(ops(i).ReturnType, ReturnType) = False Then
                    ops.RemoveAt(i)
                ElseIf CompareType(ops(i).Parameters(0).ParameterType, Type) = False Then
                    ops.RemoveAt(i)
                End If
            Next
        End If

        Return ops
    End Function

    Shared Function GetWideningConversionOperators(ByVal Compiler As Compiler, ByVal Type As Mono.Cecil.TypeReference, ByVal ReturnType As Mono.Cecil.TypeReference) As Generic.List(Of Mono.Cecil.MethodReference)
        Return GetConversionOperators(Compiler, New String() {"op_Implicit"}, Type, ReturnType)
    End Function

    Shared Function GetNarrowingConversionOperators(ByVal Compiler As Compiler, ByVal Type As Mono.Cecil.TypeReference, ByVal ReturnType As Mono.Cecil.TypeReference) As Generic.List(Of Mono.Cecil.MethodReference)
        Return GetConversionOperators(Compiler, New String() {"op_Explicit"}, Type, ReturnType)
    End Function

    Shared Function GetOperators(ByVal Compiler As Compiler, ByVal Names() As String, ByVal Type As Mono.Cecil.TypeReference) As Generic.List(Of Mono.Cecil.MethodReference)
        Dim result As New Generic.List(Of Mono.Cecil.MethodReference)

        'Dim members() As MemberInfo
        Dim members As Generic.List(Of Mono.Cecil.MemberReference)
        members = Compiler.TypeManager.GetCache(Type).GetAllFlattenedMembers(MemberVisibility.All)

        For i As Integer = 0 To Names.Length - 1
            Dim testName As String = Names(i)
            For m As Integer = 0 To members.Count - 1
                Dim member As MemberReference = members(m)
                Dim mR As Mono.Cecil.MethodReference = TryCast(member, Mono.Cecil.MethodReference)
                If mR IsNot Nothing Then
                    Dim mD As Mono.Cecil.MethodDefinition = CecilHelper.FindDefinition(mR)
                    If mD.IsSpecialName AndAlso Helper.CompareName(mD.Name, testName) AndAlso mD.IsStatic Then
                        result.Add(mR)
                    End If
                End If
            Next
            If result.Count > 0 Then Exit For
        Next

        Return result
    End Function

    Shared Function GetUnaryOperators(ByVal Compiler As Compiler, ByVal Op As UnaryOperators, ByVal Type As Mono.Cecil.TypeReference) As Generic.List(Of Mono.Cecil.MethodReference)
        Dim opNameAlternatives As String() = Nothing

        Select Case Op
            Case UnaryOperators.Add
                opNameAlternatives = New String() {"op_UnaryPlus"}
            Case UnaryOperators.Minus
                opNameAlternatives = New String() {"op_UnaryNegation"}
            Case UnaryOperators.Not
                opNameAlternatives = New String() {"op_OnesComplement", "op_LogicalNot"}
        End Select

        Return GetOperators(Compiler, opNameAlternatives, Type)
    End Function

    Shared Function GetBinaryOperators(ByVal Compiler As Compiler, ByVal Op As BinaryOperators, ByVal Type As Mono.Cecil.TypeReference) As Generic.List(Of Mono.Cecil.MethodReference)
        Dim opNameAlternatives As String() = Nothing

        Select Case Op
            Case BinaryOperators.And
                opNameAlternatives = New String() {"op_BitwiseAnd", "op_LogicalAnd"}
            Case BinaryOperators.Like
                opNameAlternatives = New String() {"op_Like"}
            Case BinaryOperators.Mod
                opNameAlternatives = New String() {"op_Modulus"}
            Case BinaryOperators.Or
                opNameAlternatives = New String() {"op_BitwiseOr", "op_LogicalOr"}
            Case BinaryOperators.XOr
                opNameAlternatives = New String() {"op_ExclusiveOr"}
            Case BinaryOperators.LT
                opNameAlternatives = New String() {"op_LessThan"}
            Case BinaryOperators.GT
                opNameAlternatives = New String() {"op_GreaterThan"}
            Case BinaryOperators.Equals
                opNameAlternatives = New String() {"op_Equality"}
            Case BinaryOperators.NotEqual
                opNameAlternatives = New String() {"op_Inequality"}
            Case BinaryOperators.LE
                opNameAlternatives = New String() {"op_LessThanOrEqual"}
            Case BinaryOperators.GE
                opNameAlternatives = New String() {"op_GreaterThanOrEqual"}
            Case BinaryOperators.Concat
                opNameAlternatives = New String() {"op_Concatenate"}
            Case BinaryOperators.Mult
                opNameAlternatives = New String() {"op_Multiply"}
            Case BinaryOperators.Add
                opNameAlternatives = New String() {"op_Addition"}
            Case BinaryOperators.Minus
                opNameAlternatives = New String() {"op_Subtraction"}
            Case BinaryOperators.Power
                opNameAlternatives = New String() {"op_Exponent"}
            Case BinaryOperators.RealDivision
                opNameAlternatives = New String() {"op_Division"}
            Case BinaryOperators.IntDivision
                opNameAlternatives = New String() {"op_IntegerDivision"}
            Case BinaryOperators.ShiftLeft
                'See: http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconOperatorOverloadingUsageGuidelines.asp
                opNameAlternatives = New String() {"op_LeftShift", "op_SignedRightShift"}
            Case BinaryOperators.ShiftRight
                opNameAlternatives = New String() {"op_RightShift", "op_UnsignedRightShift"}
            Case BinaryOperators.IsTrue
                opNameAlternatives = New String() {"op_True"}
            Case BinaryOperators.IsFalse
                opNameAlternatives = New String() {"op_False"}
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

    Shared Function IsAccessibleExternal(ByVal Compiler As Compiler, ByVal Member As Mono.Cecil.MemberReference) As Boolean
        If Compiler.Assembly.IsDefinedHere(Member) Then Return True

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
    Shared Function IsAccessible(ByVal Compiler As Compiler, ByVal CalledType As Mono.Cecil.TypeReference, ByVal CallerType As Mono.Cecil.TypeReference) As Boolean
        Dim calledTypeD As Mono.Cecil.TypeDefinition = CecilHelper.FindDefinition(CalledType)
        Dim callerTypeD As Mono.Cecil.TypeDefinition = CecilHelper.FindDefinition(CallerType)

        If Not Compiler.Assembly.IsDefinedHere(CalledType) Then
            'The types are not in the same assembly, they can only be accessible if the
            'called type is public and all its declaring types are public.
            Dim declType As Mono.Cecil.TypeDefinition = calledTypeD
            Do Until declType Is Nothing
                If declType.IsPublic = False AndAlso declType.IsNestedPublic = False Then Return False
                declType = CecilHelper.FindDefinition(declType.DeclaringType)
            Loop
            Return True
        End If

        'If it is the same type they are obviously accessible.
        If CompareType(CalledType, CallerType) Then Return True

        'Now both types are in the same assembly.

        'If the called type is not a nested type it is accessible.
        If CalledType.DeclaringType Is Nothing Then Return True

        'The caller can descend once into a private type, check if that is the case
        If calledTypeD.IsNestedPrivate Then
            'don't fail here, because could be the private nesting is further up the hierarchy
            If Helper.CompareType(CalledType.DeclaringType, CallerType) Then
                Return True
            End If
        End If

        'Add all the surrounding types of the caller type to a list.
        Dim callerHierarchy As New Generic.List(Of Mono.Cecil.TypeReference)
        Dim tmp As Mono.Cecil.TypeReference = CallerType.DeclaringType
        Do Until tmp Is Nothing
            callerHierarchy.Add(tmp)
            tmp = tmp.DeclaringType
        Loop

        Dim tmpCaller As Mono.Cecil.TypeDefinition = CecilHelper.FindDefinition(CalledType.DeclaringType)
        Do Until tmpCaller Is Nothing
            If callerHierarchy.Contains(tmpCaller) Then
                'The caller can descend once into a private type, check that here.
                If calledTypeD.IsNestedPrivate Then Return Helper.CompareType(CalledType.DeclaringType, tmpCaller)

                'We've reached a common surrounding type.
                'No matter what accessibility level this type has 
                'it is accessible.
                Return True
            End If
            If tmpCaller.IsNestedPrivate Then
                'There is a private type here...
                Return False
            End If
            tmpCaller = CecilHelper.FindDefinition(tmpCaller.DeclaringType)
        Loop

        'If the called type is a private nested type and the above checks failed, it is inaccessible
        If calledTypeD.IsNestedPrivate Then Return Helper.CompareType(CalledType.DeclaringType, CallerType)

        'There is no common surrounding type, and the access level of all 
        'surrounding types of the called types are non-private, so the type
        'is accessible.
        Return True
    End Function

    Shared Function IsAccessible(ByVal Context As BaseObject, ByVal CalledMethodAccessability As Mono.Cecil.MethodAttributes, ByVal CalledType As Mono.Cecil.TypeReference) As Boolean
        Dim Compiler As Compiler = Context.Compiler

        Helper.Assert(Compiler IsNot Nothing)
        Helper.Assert(CalledType IsNot Nothing)

        'Checks it the accessed method / type is accessible from the current compiling code
        '(for attributes that is not contained within a type)

        Dim testNested As Mono.Cecil.TypeDefinition = CecilHelper.FindDefinition(CalledType)
        Dim compiledType As Boolean = Compiler.Assembly.IsDefinedHere(CalledType)
        Dim mostDeclaredType As Mono.Cecil.TypeDefinition = Nothing

        Do Until testNested Is Nothing
            mostDeclaredType = testNested
            'If it is a nested private type, it is not accessible.
            If testNested.IsNestedPrivate Then Return False
            'If it is not a nested public type in an external assembly, it is not accessible.
            If compiledType = False AndAlso testNested.IsNestedPublic = False AndAlso testNested.IsNested Then Return False
            testNested = CecilHelper.FindDefinition(testNested.DeclaringType)
        Loop

        'If the most external type is not public then it is not accessible.
        If compiledType = False AndAlso mostDeclaredType.IsPublic = False Then Return False

        'The type is at least accessible now, check the method.

        Dim ac As Mono.Cecil.MethodAttributes = (CalledMethodAccessability And Mono.Cecil.MethodAttributes.MemberAccessMask)
        Dim isPrivate As Boolean = ac = Mono.Cecil.MethodAttributes.Private
        Dim isFriend As Boolean = ac = Mono.Cecil.MethodAttributes.Assembly OrElse ac = Mono.Cecil.MethodAttributes.FamORAssem
        Dim isProtected As Boolean = ac = Mono.Cecil.MethodAttributes.Family OrElse ac = Mono.Cecil.MethodAttributes.FamORAssem
        Dim isPublic As Boolean = ac = Mono.Cecil.MethodAttributes.Public

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


    Shared Function IsAccessible(ByVal Context As BaseObject, ByVal CalledMethodAccessability As Mono.Cecil.MethodAttributes, ByVal CalledType As Mono.Cecil.TypeReference, ByVal CallerType As Mono.Cecil.TypeReference) As Boolean
        'If both types are equal everything is accessible.
        If CompareType(CalledType, CallerType) Then Return True

        'If the callertype is a nested class of the called type, then everything is accessible as well.
        If IsNested(CalledType, CallerType) Then Return True

        'If the called type is not accessible from the caller, the member cannot be accessible either.
        If IsAccessible(Context.Compiler, CalledType, CallerType) = False Then Return False

        Dim ac As Mono.Cecil.MethodAttributes = (CalledMethodAccessability And Mono.Cecil.MethodAttributes.MemberAccessMask)
        Dim isPrivate As Boolean = ac = Mono.Cecil.MethodAttributes.Private
        Dim isFriend As Boolean = ac = Mono.Cecil.MethodAttributes.Assembly OrElse ac = Mono.Cecil.MethodAttributes.FamORAssem
        Dim isProtected As Boolean = ac = Mono.Cecil.MethodAttributes.Family OrElse ac = Mono.Cecil.MethodAttributes.FamORAssem
        Dim isPublic As Boolean = ac = Mono.Cecil.MethodAttributes.Public

        'Public members are always accessible!
        If isPublic Then Return True

        'If the member is private, the member is not accessible
        '(to be accessible the types must be equal or the caller type must
        'be a nested type of the called type, cases already covered).
        'Catch: Enum members of public enums can apparently be private.
        If isPrivate Then Return Helper.IsEnum(Context.Compiler, CalledType)

        If isFriend AndAlso isProtected Then
            'Friend and Protected
            'Both types must be in the same assembly or CallerType must inherit from CalledType.
            Return Context.Compiler.Assembly.IsDefinedHere(CalledType) OrElse (Helper.IsSubclassOf(CalledType, CallerType))
        ElseIf isFriend Then
            'Friend, but not Protected
            'Both types must be in the same assembly
            Return Context.Compiler.Assembly.IsDefinedHere(CalledType)
        ElseIf isProtected Then
            'Protected, but not Friend
            'CallerType must inherit from CalledType.
            Return Helper.IsSubclassOf(CalledType, CallerType)
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
    Shared Function IsNested(ByVal CalledType As Mono.Cecil.TypeReference, ByVal CallerType As Mono.Cecil.TypeReference) As Boolean
        Dim tmp As Mono.Cecil.TypeReference = CecilHelper.FindDefinition(CallerType).DeclaringType
        Do Until tmp Is Nothing
            If CompareType(CalledType, tmp) Then Return True
            tmp = CecilHelper.FindDefinition(tmp).DeclaringType
        Loop
        Return False
    End Function

    Shared Function IsAccessible(ByVal Context As BaseObject, ByVal FieldAccessability As Mono.Cecil.FieldAttributes, ByVal CalledType As Mono.Cecil.TypeReference, ByVal CallerType As Mono.Cecil.TypeReference) As Boolean
        'The fieldattributes for accessibility are the same as methodattributes.
        Return IsAccessible(Context, CType(FieldAccessability, Mono.Cecil.MethodAttributes), CalledType, CallerType)
    End Function

    Shared Function CreateGenericTypename(ByVal Typename As Identifier, ByVal TypeArguments As TypeParameters) As Identifier
        If TypeArguments Is Nothing OrElse TypeArguments.Parameters.Count = 0 Then Return Typename
        Return New Identifier(Typename.Parent, CreateGenericTypename(Typename.Identifier, TypeArguments.Parameters.Count), Typename.Location, TypeCharacters.Characters.None)
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

    Shared Function GetDelegateArguments(ByVal Compiler As Compiler, ByVal delegateType As Mono.Cecil.TypeReference) As Mono.Collections.Generic.Collection(Of ParameterDefinition)
        Dim invoke As Mono.Cecil.MethodReference = GetInvokeMethod(Compiler, delegateType)
        Return GetParameters(Compiler, invoke)
    End Function

    ''' <summary>
    ''' Finds the member with the exact same signature.
    ''' </summary>
    ''' <param name="grp"></param>
    ''' <param name="params"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function ResolveGroupExact(ByVal Context As BaseObject, ByVal grp As Generic.List(Of Mono.Cecil.MemberReference), ByVal params() As Mono.Cecil.TypeReference) As Mono.Cecil.MemberReference
        Dim Compiler As Compiler = Context.Compiler

        For i As Integer = 0 To grp.Count - 1
            Dim member As Mono.Cecil.MemberReference = grp(i)
            Dim paramtypes As Mono.Cecil.TypeReference() = Helper.GetParameterTypes(Context, member)
            If Helper.CompareTypes(paramtypes, params) Then Return member

            If paramtypes.Length <> params.Length Then Continue For

            Dim found As Boolean = True
            For j As Integer = 0 To paramtypes.Length - 1
                If Helper.IsSubclassOf(paramtypes(j), params(j)) = False Then
                    found = False
                    Exit For
                End If
            Next
            If found Then Return member
        Next

        Return Nothing
    End Function

    Shared Function IsTypeConvertibleToAny(ByVal TypesToSearch As Mono.Cecil.TypeReference(), ByVal TypeToFind As Mono.Cecil.TypeReference) As Boolean
        For i As Integer = 0 To TypesToSearch.Length - 1
            Dim t As Mono.Cecil.TypeReference = TypesToSearch(i)
            If Helper.CompareType(t, TypeToFind) OrElse Helper.IsSubclassOf(t, TypeToFind) Then Return True
        Next
        Return False
    End Function

    Shared Function IsTypeConvertibleToAny(ByVal TypeToSearch As Mono.Cecil.TypeReference, ByVal TypesToFind As Mono.Collections.Generic.Collection(Of TypeReference)) As Boolean
        For i As Integer = 0 To TypesToFind.Count - 1
            Dim t As Mono.Cecil.TypeReference = TypesToFind(i)
            If Helper.CompareType(t, TypeToSearch) OrElse Helper.IsSubclassOf(TypeToSearch, t) Then Return True
        Next
        Return False
    End Function

    Shared Function IsTypeConvertibleToAny(ByVal TypesToSearch As Mono.Collections.Generic.Collection(Of TypeReference), ByVal TypeToFind As Mono.Cecil.TypeReference) As Boolean
        For i As Integer = 0 To TypesToSearch.Count - 1
            Dim t As Mono.Cecil.TypeReference = TypesToSearch(i)
            If Helper.CompareType(t, TypeToFind) OrElse Helper.IsSubclassOf(TypeToFind, t) Then Return True
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

    Shared Function GenerateCodeCollection(ByVal Collection As IList, ByVal Info As EmitInfo, ByVal Types As Mono.Cecil.TypeReference()) As Boolean
        Dim result As Boolean = True
        Helper.Assert(Collection.Count = Types.Length)
        For i As Integer = 0 To Collection.Count - 1
            result = DirectCast(Collection(i), IBaseObject).GenerateCode(Info.Clone(Info.Context, Info.IsRHS, Info.IsExplicitConversion, Types(i))) AndAlso result
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

    <Diagnostics.DebuggerHidden()> _
    Shared Sub ErrorRecoveryNotImplemented(ByVal Location As Span)
        Console.WriteLine("{0}: Compiler error around this location, the compiler hasn't implemented the error message, nor error recovery, so the compiler will probably crash soon.", Location.AsString(BaseObject.m_Compiler))
        Console.WriteLine(New System.Diagnostics.StackTrace().ToString())
        Helper.StopIfDebugging()
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
    Shared Function getTypeAttributeScopeFromScope(ByVal Modifiers As Modifiers, ByVal isNested As Boolean) As Mono.Cecil.TypeAttributes
        If Not isNested Then
            'If vbnc.Modifiers.IsNothing(Modifiers) = False Then
            If Modifiers.Is(ModifierMasks.Public) Then
                Return Mono.Cecil.TypeAttributes.Public
            Else
                Return Mono.Cecil.TypeAttributes.NotPublic
            End If
            'Else
            '  Return TypeAttributes.NotPublic
            'End If
        Else
            'If vbnc.Modifiers.IsNothing(Modifiers) = False Then
            If Modifiers.Is(ModifierMasks.Public) Then
                Return Mono.Cecil.TypeAttributes.NestedPublic
            ElseIf Modifiers.Is(ModifierMasks.Friend) Then
                If Modifiers.Is(ModifierMasks.Protected) Then
                    Return Mono.Cecil.TypeAttributes.NestedFamORAssem
                    '0Return Reflection.TypeAttributes.NotPublic
                    'Return Reflection.TypeAttributes.VisibilityMask
                Else
                    Return Mono.Cecil.TypeAttributes.NestedAssembly
                    'Return Reflection.TypeAttributes.NotPublic
                End If
            ElseIf Modifiers.Is(ModifierMasks.Protected) Then
                Return Mono.Cecil.TypeAttributes.NestedFamily
                'Return Reflection.TypeAttributes.NotPublic
            ElseIf Modifiers.Is(ModifierMasks.Private) Then
                Return Mono.Cecil.TypeAttributes.NestedPrivate
            Else
                'Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, "Default scope set to public...")
                Return Mono.Cecil.TypeAttributes.NestedPublic
            End If
            ' Else
            'Return Reflection.TypeAttributes.NestedPublic
            'End If
        End If
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
        HexToInt = 0
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
        OctToInt = 0
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
    Shared Function GetTypeOrTypeBuilder(ByVal Compiler As Compiler, ByVal Type As Mono.Cecil.TypeReference) As Mono.Cecil.TypeReference
        Return GetTypeOrTypeReference(Compiler, Type)
    End Function

    Shared Function GetMethodOrMethodReference(ByVal Compiler As Compiler, ByVal Method As Mono.Cecil.MethodReference) As Mono.Cecil.MethodReference
        Helper.Assert(Method IsNot Nothing)
        Helper.Assert(Method.DeclaringType IsNot Nothing)

        If Method.Annotations.Contains("MemberInReflection") Then
            Return GetMethodOrMethodReference(Compiler, DirectCast(Method.Annotations("MemberInReflection"), Mono.Cecil.MethodReference))
        End If

        Dim upper As Mono.Cecil.TypeReference
        upper = Method.DeclaringType
        While upper.DeclaringType IsNot Nothing
            upper = upper.DeclaringType
        End While
        Helper.Assert(upper.Module IsNot Nothing)
        Helper.Assert(upper.Module.Assembly IsNot Nothing)
        If Compiler.AssemblyBuilderCecil Is upper.Module.Assembly Then
            Return Method
        Else
            Return CecilHelper.MakeEmittable(Method)
        End If
    End Function

    Shared Function GetFieldOrFieldReference(ByVal Compiler As Compiler, ByVal field As Mono.Cecil.FieldReference) As Mono.Cecil.FieldReference
        If field.Annotations.Contains("MemberInReflection") Then
            Return DirectCast(field.Annotations("MemberInReflection"), Mono.Cecil.FieldReference)
        ElseIf Compiler.AssemblyBuilderCecil Is field.DeclaringType.Module.Assembly Then
            Return field
        Else
            Return Compiler.AssemblyBuilderCecil.MainModule.Import(field)
        End If
    End Function

    Shared Function GetPropertyOrPropertyBuilder(ByVal Compiler As Compiler, ByVal [Property] As Mono.Cecil.PropertyReference) As Mono.Cecil.PropertyReference
        If Compiler.Assembly.IsDefinedHere([Property]) Then
            Return [Property]
        Else
            Return [Property]
        End If
    End Function

    Shared Function GetFieldOrFieldBuilder(ByVal Compiler As Compiler, ByVal Field As Mono.Cecil.FieldReference) As Mono.Cecil.FieldReference
        Return GetFieldOrFieldReference(Compiler, Field)
    End Function

    Shared Sub GetFieldOrFieldBuilder(ByVal Compiler As Compiler, ByVal Fields As Generic.List(Of Mono.Cecil.FieldReference))
        For i As Integer = 0 To Fields.Count - 1
            Fields(i) = GetFieldOrFieldBuilder(Compiler, Fields(i))
        Next
    End Sub

    Shared Sub SetTypeOrTypeBuilder(ByVal Compiler As Compiler, ByVal Type As Mono.Cecil.TypeReference())
        If Type Is Nothing Then Return
        For i As Integer = 0 To Type.Length - 1
            Helper.Assert(Type(i) IsNot Nothing)
            Type(i) = GetTypeOrTypeBuilder(Compiler, Type(i))
        Next
    End Sub

    Shared Function GetTypeOrTypeBuilders(ByVal Compiler As Compiler, ByVal Type As Mono.Cecil.TypeReference(), Optional ByVal OnlySuccessful As Boolean = False) As Mono.Cecil.TypeReference()
        Dim result() As Mono.Cecil.TypeReference
        If Type Is Nothing Then Return Nothing

        ReDim result(Type.GetUpperBound(0))
        For i As Integer = 0 To Type.GetUpperBound(0)
            Dim tmp As Mono.Cecil.TypeReference
            tmp = GetTypeOrTypeBuilder(Compiler, Type(i))
            If tmp Is Nothing AndAlso OnlySuccessful Then
                result(i) = Type(i)
            Else
                result(i) = tmp
            End If
        Next
        Return result
    End Function

    Shared Function IsAssignable(ByVal Context As BaseObject, ByVal FromType As Mono.Cecil.TypeReference, ByVal ToType As Mono.Cecil.TypeReference) As Boolean
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
        ElseIf Helper.CompareType(FromType, ToType) Then
            Return True
        ElseIf CecilHelper.IsArray(FromType) = True AndAlso CecilHelper.IsArray(ToType) = True AndAlso FromType.FullName Is Nothing AndAlso ToType.FullName Is Nothing AndAlso FromType.Name.Equals(ToType.Name, StringComparison.Ordinal) Then
            Return True
        ElseIf CompareType(ToType, Compiler.TypeCache.System_Object) Then
            Return True
            'ElseIf TypeOf ToType Is GenericTypeParameterBuilder AndAlso TypeOf FromType Is Type Then
            '    Return ToType.Name = FromType.Name
            'ElseIf ToType.GetType Is Compiler.TypeCache.System_Reflection_Emit_TypeBuilderInstantiation Then
            '    'ElseIf ToType.GetType.Name = "TypeBuilderInstantiation" Then
            '    If Helper.CompareType(Helper.GetTypeOrTypeBuilder(FromType), ToType) Then
            '        Return True
            '    Else
            '        Return False
            '    End If
            '    Return True
            '    'ElseIf TypeOf ToType Is TypeDescriptor = False AndAlso TypeOf FromType Is TypeDescriptor = False AndAlso ToType.IsAssignableFrom(FromType) Then
            '    '    Return True
        ElseIf TypeOf FromType Is GenericParameter Then
            Dim gp As GenericParameter = DirectCast(FromType, GenericParameter)
            If gp.HasConstraints Then
                For i As Integer = 0 To gp.Constraints.Count - 1
                    If Helper.IsAssignable(Context, gp.Constraints(i), ToType) Then Return True
                Next
            End If
            Return False
        ElseIf IsInterface(Context, ToType) Then
            Dim ifaces As Mono.Collections.Generic.Collection(Of TypeReference) = CecilHelper.GetInterfaces(FromType, True)
            If ifaces IsNot Nothing Then
                For i As Integer = 0 To ifaces.Count - 1
                    Dim iface As Mono.Cecil.TypeReference = ifaces(i)
                    If Helper.CompareType(iface, ToType) Then Return True
                    If Helper.IsAssignable(Context, iface, ToType) Then Return True
                    If Helper.IsSubclassOf(ToType, iface) Then Return True
                Next
            End If
            If IsInterface(Context, FromType) AndAlso CecilHelper.IsGenericType(FromType) AndAlso CecilHelper.IsGenericType(ToType) Then
                Dim baseFromI, baseToI As Mono.Cecil.TypeReference
                baseFromI = CecilHelper.GetGenericTypeDefinition(FromType)
                baseToI = CecilHelper.GetGenericTypeDefinition(ToType)
                If Helper.CompareType(baseFromI, baseToI) Then
                    Dim fromArgs, toArgs As Mono.Collections.Generic.Collection(Of TypeReference)
                    fromArgs = CecilHelper.GetGenericArguments(FromType)
                    toArgs = CecilHelper.GetGenericArguments(ToType)
                    If fromArgs.Count = toArgs.Count Then
                        For i As Integer = 0 To toArgs.Count - 1
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
        ElseIf TypeOf FromType Is Mono.Cecil.ArrayType AndAlso Helper.Compare(ToType, Compiler.TypeCache.System_Array) Then
            Return True
        ElseIf CecilHelper.IsArray(FromType) AndAlso CecilHelper.IsArray(ToType) Then
            Dim fromArray As ArrayType = DirectCast(FromType, ArrayType)
            Dim toArray As ArrayType = DirectCast(ToType, ArrayType)
            Dim fromElement As Mono.Cecil.TypeReference = CecilHelper.GetElementType(FromType)
            Dim toElement As Mono.Cecil.TypeReference = CecilHelper.GetElementType(ToType)
            If CecilHelper.IsValueType(fromElement) Xor CecilHelper.IsValueType(toElement) Then
                Return False
            ElseIf fromArray.Rank <> toArray.Rank Then
                Return False
            Else
                Return Helper.IsAssignable(Context, fromElement, toElement)
            End If
        ElseIf Helper.IsSubclassOf(ToType, FromType) Then
            Return True
        ElseIf Helper.IsSubclassOf(FromType, ToType) Then
            Return False
        Else
            'Helper.NotImplementedYet("Don't know if it possible to convert from " & FromType.Name & " to " & ToType.Name)
            Return False
        End If
    End Function

    Shared Function GetMostEncompassedType(ByVal Compiler As Compiler, ByVal Types As Generic.List(Of TypeReference)) As TypeReference
        Dim result() As Boolean

        '•	If an intrinsic widening conversion exists from a type A to a type B, and if neither A nor B are interfaces, then A is encompassed by B, and B encompasses A.
        '•	The most encompassing type in a set of types is the one type that encompasses all other types in the set. 
        '   If no single type encompasses all other types, then the set has no most encompassing type. 
        '   In intuitive terms, the most encompassing type is the “largest” type in the set—the one type to 
        '   which each of the other types can be converted through a widening conversion.
        '•	The most encompassed type in a set of types is the one type that is encompassed by all other types in the set. 
        '   If no single type is encompassed by all other types, then the set has no most encompassed type. 
        '   In intuitive terms, the most encompassed type is the “smallest” type in the set—the one type that 
        '   can be converted to each of the other types through a narrowing conversion.

        If Types Is Nothing OrElse Types.Count = 0 Then Return Nothing
        If Types.Count = 1 Then Return Types(0)

        ReDim result(Types.Count - 1)
        For i As Integer = 0 To result.Length - 1
            result(i) = True
        Next

        For i As Integer = 0 To result.Length - 1
            For j As Integer = i + 1 To result.Length - 1
                If result(j) AndAlso IsFirstEncompassingSecond(Compiler, Types(i), Types(j)) Then
                    result(j) = False
                ElseIf result(i) AndAlso IsFirstEncompassingSecond(Compiler, Types(j), Types(i)) Then
                    result(i) = False
                End If
            Next
        Next

        Dim count As Integer
        Dim index As Integer
        For i As Integer = 0 To result.Length - 1
            If result(i) Then
                count += 1
                index = i
            End If
        Next

        If count <> 1 Then Return Nothing
        Return Types(index)
    End Function

    Shared Function IsFirstEncompassingSecond(ByVal Compiler As Compiler, ByVal First As TypeReference, ByVal Second As TypeReference) As Boolean
        If First Is Second Then Return False
        Return Compiler.TypeResolution.IsImplicitlyConvertible(Compiler, Second, First)
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

    Shared Function IsNullableType(ByVal Compiler As Compiler, ByVal Type As Mono.Cecil.TypeReference) As Boolean
        If CecilHelper.IsValueType(Type) = False Then Return False
        If CompareType(Type, Compiler.TypeCache.System_Nullable1) Then Return True

        If CecilHelper.IsGenericTypeDefinition(Type) Then Return False
        If CecilHelper.IsGenericParameter(Type) Then Return False
        If CecilHelper.IsGenericType(Type) = False Then Return False
        Return Helper.CompareType(CecilHelper.GetGenericTypeDefinition(Type), Compiler.TypeCache.System_Nullable1)
    End Function

    Shared Function IsSubclassOf(ByVal BaseClass As Mono.Cecil.TypeReference, ByVal DerivedClass As Mono.Cecil.TypeReference) As Boolean
        If TypeOf BaseClass Is Mono.Cecil.GenericParameter Xor TypeOf DerivedClass Is Mono.Cecil.GenericParameter Then Return False
        If TypeOf DerivedClass Is ArrayType Then
            If Helper.CompareType(BaseClass, Compiler.CurrentCompiler.TypeCache.System_Array) Then Return True
            Return False
        End If
        If TypeOf BaseClass Is Mono.Cecil.ArrayType Or TypeOf DerivedClass Is Mono.Cecil.ArrayType Then Return False
        Dim base As Mono.Cecil.TypeDefinition = CecilHelper.FindDefinition(BaseClass)
        Dim derived As Mono.Cecil.TypeDefinition = CecilHelper.FindDefinition(DerivedClass)
        Dim current As Mono.Cecil.TypeDefinition = CecilHelper.FindDefinition(derived.BaseType)

        Do While current IsNot Nothing
            If Helper.CompareType(current, base) Then Return True
            current = CecilHelper.FindDefinition(CecilHelper.FindDefinition(current).BaseType)
        Loop
        Return False
    End Function

    Shared Function DoesTypeImplementInterface(ByVal Context As BaseObject, ByVal Type As Mono.Cecil.TypeReference, ByVal [Interface] As Mono.Cecil.TypeReference) As Boolean
        Dim ifaces As Mono.Collections.Generic.Collection(Of TypeReference)
        ifaces = CecilHelper.GetInterfaces(Type, True)
        For Each iface As Mono.Cecil.TypeReference In ifaces
            'If Helper.IsAssignable(Context, iface, [Interface]) Then Return True
            If Helper.CompareType(iface, [Interface]) Then Return True
            If DoesTypeImplementInterface(Context, iface, [Interface]) Then Return True
        Next
        Return False
    End Function

    Shared Function GetEnumType(ByVal Compiler As Compiler, ByVal EnumType As Mono.Cecil.TypeReference) As Mono.Cecil.TypeReference
        Dim tp As Mono.Cecil.TypeDefinition = CecilHelper.FindDefinition(EnumType)
        Dim fInfo As Mono.Cecil.FieldReference

        Helper.Assert(Helper.IsEnum(Compiler, EnumType))

        tp = CecilHelper.FindDefinition(EnumType)
        fInfo = CecilHelper.FindField(tp.Fields, EnumDeclaration.EnumTypeMemberName)

        Helper.Assert(fInfo IsNot Nothing)

        Return fInfo.FieldType
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
    Shared Function CreateTypeConversion(ByVal Parent As ParsedObject, ByVal FromExpression As Expression, ByVal DestinationType As Mono.Cecil.TypeReference, ByRef result As Boolean) As Expression
        Dim res As Expression = Nothing
        result = IsConvertible(Parent, FromExpression, FromExpression.ExpressionType, DestinationType, True, res, True, Nothing)
        Return res
    End Function

    Shared Function IsConvertible(ByVal Parent As ParsedObject, ByVal FromExpression As Expression, ByVal FromType As TypeReference, ByVal DestinationType As TypeReference, ByVal CreateConversionExpression As Boolean, ByRef convExpr As Expression, ByVal ShowError As Boolean, ByVal isStrict As Boolean?, Optional ByVal considerConstantExpressions As Boolean = True) As Boolean
        Dim Compiler As Compiler = Parent.Compiler
        Dim TypeCache As CecilTypeCache = Compiler.TypeCache
        Dim toArray As ArrayType
        Dim fromArray As ArrayType
        Dim fromTD As TypeDefinition
        Dim toTD As TypeDefinition
        Dim fromElement As TypeReference
        Dim toElement As TypeReference
        Dim isFromReference As Boolean
        Dim fromTG As GenericParameter
        Dim toTG As GenericParameter
        Dim fromTC As TypeCode
        Dim toTC As TypeCode
        Dim isFromNullable As Boolean
        Dim isToNullable As Boolean
        Dim isFromEnum As Boolean
        Dim isToEnum As Boolean
        Dim byrefTo As ByReferenceType = TryCast(DestinationType, ByReferenceType)
        Dim byrefFrom As ByReferenceType = TryCast(FromType, ByReferenceType)

        Dim constant As Object = Nothing

        If Not isStrict.HasValue Then
            isStrict = Parent.Location.File(Compiler).IsOptionStrictOn
        End If

        convExpr = FromExpression

        If byrefTo IsNot Nothing AndAlso byrefFrom Is Nothing Then
            If IsConvertible(Parent, FromExpression, FromType, byrefTo.ElementType, CreateConversionExpression, convExpr, ShowError, isStrict) Then
                'If CreateConversionExpression Then
                '    convExpr = New GetRefExpression(Parent, convExpr)
                '    If convExpr.ResolveExpression(ResolveInfo.Default(Parent.Compiler)) = False Then Return False
                'End If
                Return True
            End If
            Return False
        ElseIf byrefFrom IsNot Nothing AndAlso byrefTo Is Nothing Then
            If IsConvertible(Parent, FromExpression, byrefFrom.ElementType, DestinationType, False, convExpr, ShowError, isStrict) Then
                If CreateConversionExpression Then
                    convExpr = New DeRefExpression(Parent, convExpr)
                    If convExpr.ResolveExpression(ResolveInfo.Default(Parent.Compiler)) = False Then Return False
                    If Not IsConvertible(Parent, convExpr, byrefFrom.ElementType, DestinationType, True, convExpr, ShowError, isStrict) Then Return False
                End If
                Return True
            End If
            Return False
        End If

        'Identity/Default conversions
        '•	From a type to itself.
        If Helper.CompareType(FromType, DestinationType) Then
            'no conversion required
            Return True
        End If
        '•	From an anonymous delegate type generated for a lambda method reclassification to any delegate type with an identical signature.
        'TODO
        '•	From the literal Nothing to a type.
        If Helper.CompareType(FromType, Compiler.TypeCache.Nothing) Then
            'not sure if a conversion is required here
            If CreateConversionExpression Then
                convExpr = New CTypeExpression(Parent, convExpr, DestinationType)
                If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
            End If
            Return True
        End If

        toTD = CecilHelper.FindDefinition(DestinationType)
        fromTD = CecilHelper.FindDefinition(FromType)
        toArray = TryCast(DestinationType, ArrayType)
        fromArray = TryCast(FromType, ArrayType)
        isFromReference = fromTD IsNot Nothing AndAlso ((fromTD.IsInterface OrElse fromTD.IsClass) AndAlso (fromTD.IsValueType = False OrElse fromArray IsNot Nothing))
        isFromNullable = Helper.IsNullableType(Compiler, FromType)
        isToNullable = Helper.IsNullableType(Compiler, DestinationType)
        isFromEnum = Helper.IsEnum(Compiler, FromType)
        isToEnum = Helper.IsEnum(Compiler, DestinationType)
        fromTC = Helper.GetTypeCode(Compiler, FromType)
        toTC = Helper.GetTypeCode(Compiler, DestinationType)

        'Numeric conversions
        'Widening conversions:
        '•	From Byte to UShort, Short, UInteger, Integer, ULong, Long, Decimal, Single, or Double.
        '•	From SByte to Short, Integer, Long, Decimal, Single, or Double.
        '•	From UShort to UInteger, Integer, ULong, Long, Decimal, Single, or Double.
        '•	From Short to Integer, Long, Decimal, Single or Double.
        '•	From UInteger to ULong, Long, Decimal, Single, or Double.
        '•	From Integer to Long, Decimal, Single or Double.
        '•	From ULong to Decimal, Single, or Double.
        '•	From Long to Decimal, Single or Double.
        '•	From Decimal to Single or Double.
        '•	From Single to Double.
        If isFromNullable = False AndAlso isToNullable = False AndAlso isFromEnum = False AndAlso isToEnum = False Then
            Select Case fromTC
                Case TypeCode.Byte, TypeCode.SByte, TypeCode.UInt16, TypeCode.Int16, TypeCode.UInt32, TypeCode.Int32, TypeCode.UInt64, TypeCode.Int64, TypeCode.Decimal, TypeCode.Single
                    If (toTC <> TypeCode.Object OrElse Helper.CompareType(DestinationType, TypeCache.System_Object)) AndAlso Compiler.TypeResolution.IsImplicitlyConvertible(Compiler, fromTC, toTC) Then
                        If CreateConversionExpression Then
                            convExpr = New CTypeExpression(Parent, FromExpression, DestinationType)
                            If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
                        End If
                        Return True
                    End If
            End Select
        End If
        '•	From the literal 0 to an enumerated type. (widening)
        If considerConstantExpressions AndAlso Helper.IsEnum(Compiler, DestinationType) AndAlso Helper.IsLiteral0Expression(Compiler, FromExpression) Then
            If CreateConversionExpression Then
                convExpr = New CTypeExpression(Parent, FromExpression, DestinationType)
                If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
            End If
            Return True
        End If
        '•	From an enumerated type to its underlying numeric type, or to a numeric type that its underlying numeric type has a widening conversion to.
        If Helper.IsEnum(Compiler, FromType) AndAlso toTC <> TypeCode.Object AndAlso Helper.IsEnum(Compiler, DestinationType) = False Then
            Dim enumType As TypeReference = Helper.GetEnumType(Compiler, FromType)
            If Compiler.TypeResolution.IsImplicitlyConvertible(Compiler, Helper.GetTypeCode(Compiler, enumType), toTC) Then
                convExpr = New CTypeExpression(Parent, FromExpression, DestinationType, CTypeConversionType.Intrinsic)
                If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
                Return True
            End If
        End If
        '•	From a constant expression of type ULong, Long, UInteger, Integer, UShort, Short, Byte, or SByte to a narrower type, provided the value of the constant expression is within the range of the destination type.
        'SPECBUG: this doesn't include the other numeric types, Single, Double and Decimal
        If considerConstantExpressions AndAlso (fromTC = TypeCode.UInt64 OrElse fromTC = TypeCode.Int64 OrElse fromTC = TypeCode.UInt32 OrElse fromTC = TypeCode.Int32 OrElse fromTC = TypeCode.UInt16 OrElse fromTC = TypeCode.Int16 OrElse fromTC = TypeCode.Byte OrElse fromTC = TypeCode.SByte OrElse fromTC = TypeCode.Single OrElse fromTC = TypeCode.Double OrElse fromTC = TypeCode.Decimal) Then
            If FromExpression.GetConstant(constant, False) Then
                If Helper.CompareType(DestinationType, TypeCache.System_DBNull) = False AndAlso Compiler.TypeResolution.CheckNumericRange(constant, constant, DestinationType) Then
                    'No conversion should be required here
                    convExpr = New CTypeExpression(Parent, FromExpression, DestinationType)
                    If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
                    Return True
                End If
            End If
        End If

        '•	From a numeric type to an enumerated type.
        If Helper.IsEnum(Compiler, DestinationType) AndAlso Compiler.TypeResolution.IsNumericType(FromType) Then
            If isStrict Then
                If ShowError Then Compiler.Report.ShowMessage(Messages.VBNC30512, Parent.Location, Helper.ToString(Compiler, FromType), Helper.ToString(Compiler, DestinationType))
                Return False
            End If
            If CreateConversionExpression Then
                convExpr = New CTypeExpression(Parent, FromExpression, DestinationType)
                If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
            End If
            Return True
        End If
        '•	From an enumerated type to a numeric type its underlying numeric type has a narrowing conversion to.
        If Helper.IsEnum(Compiler, FromType) AndAlso Compiler.TypeResolution.IsNumericType(DestinationType) Then
            'if DestinationType is a numeric type the enum's type has a widening conversion to, we'll hit a widening conversion case above
            If isStrict Then
                If ShowError Then Compiler.Report.ShowMessage(Messages.VBNC30512, Parent.Location, Helper.ToString(Compiler, FromType), Helper.ToString(Compiler, DestinationType))
                Return False
            End If
            If CreateConversionExpression Then
                convExpr = New CTypeExpression(Parent, FromExpression, DestinationType)
                If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
            End If
            Return True
        End If
        '•	From an enumerated type to another enumerated type. 
        If Helper.IsEnum(Compiler, DestinationType) Then
            If Helper.IsEnum(Compiler, FromType) Then
                If isStrict Then
                    If ShowError Then Compiler.Report.ShowMessage(Messages.VBNC30512, Parent.Location, Helper.ToString(Compiler, FromType), Helper.ToString(Compiler, DestinationType))
                    Return False
                End If
                If CreateConversionExpression Then
                    convExpr = New CTypeExpression(Parent, FromExpression, Helper.GetEnumType(Compiler, DestinationType), CTypeConversionType.Intrinsic)
                    If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
                End If
                Return True
            ElseIf Helper.CompareType(FromType, TypeCache.System_Enum) Then
                If isStrict Then
                    If ShowError Then Compiler.Report.ShowMessage(Messages.VBNC30512, Parent.Location, Helper.ToString(Compiler, FromType), Helper.ToString(Compiler, DestinationType))
                    Return False
                End If
                If CreateConversionExpression Then
                    convExpr = New CTypeExpression(Parent, FromExpression, DestinationType, CTypeConversionType.Unbox_Ldobj)
                    If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
                End If
                Return True
            End If
        End If

        'Reference conversions
        '•	From a reference type to a base type.
        If isFromReference AndAlso Helper.IsSubclassOf(DestinationType, FromType) Then
            'no conversion required
            Return True
        End If

        If isFromReference AndAlso Helper.CompareType(DestinationType, TypeCache.System_Object) Then
            'no conversion required.
            'this case is required for at least arrays (since arrays don't explicitly inherit from object)
            Return True
        End If

        '•	From a reference type to an interface type, provided that the type implements the interface or a variant compatible interface.
        If isFromReference AndAlso toTD IsNot Nothing AndAlso toTD.IsInterface Then
            If Helper.DoesTypeImplementInterface(Parent, FromType, DestinationType) Then
                'no conversion required
                Return True
            End If
        End If
        If CecilHelper.IsArray(FromType) AndAlso toTD IsNot Nothing AndAlso toTD.IsInterface Then
            If Helper.DoesTypeImplementInterface(Parent, FromType, DestinationType) Then
                'no conversion required
                Return True
            End If
        End If

        '•	From an interface type to Object.
        If isFromReference AndAlso toTD IsNot Nothing AndAlso fromTD.IsInterface AndAlso Helper.Compare(Parent.Compiler.TypeCache.System_Object, DestinationType) Then
            'No conversion required
            Return True
        End If
        '•	From an interface type to a variant compatible interface type.
        '•	From a delegate type to a variant compatible delegate type.

        'https://connect.microsoft.com/VisualStudio/feedback/details/649909/vb-10-specification-does-not-cover-conversion-from-array-type-derived-to-ilist-of-base
        'This is from C#'s ECMA-334:
        '•	From a one-dimensional array-type S[] to System.Collections.Generic.IList<T> and base interfaces of this interface, provided there is an implicit reference conversion from S to T.
        If fromArray IsNot Nothing AndAlso fromArray.Rank = 1 AndAlso toTD IsNot Nothing AndAlso toTD.IsInterface AndAlso toTD.GenericParameters.Count = 1 Then
            Dim isIList As Boolean = Helper.CompareType(toTD, TypeCache.System_Collections_Generic_IList1)
            isIList = isIList OrElse Helper.CompareType(toTD, TypeCache.System_Collections_Generic_IEnumerable1)
            isIList = isIList OrElse Helper.CompareType(toTD, TypeCache.System_Collections_Generic_ICollection1)
            If isIList Then
                Dim git As GenericInstanceType = TryCast(DestinationType, GenericInstanceType)
                If git IsNot Nothing AndAlso git.GenericArguments.Count = 1 AndAlso IsConvertible(Parent, FromExpression, fromArray.GetElementType(), git.GenericArguments(0), False, Nothing, False, True) Then
                    'No conversion expression is required I think
                    Return True
                End If
            End If
        End If

        'Narrowing conversions for reference conversions are done at the end of the widening conversions

        'Anonymous Delegate conversions
        '•	From an anonymous delegate type generated for a lambda method reclassification to any wider delegate type. (widening)
        '•	From an anonymous delegate type generated for a lambda method reclassification to any narrower delegate type. (narrowing)
        'TODO

        'Array conversions
        If toArray IsNot Nothing AndAlso fromArray IsNot Nothing Then
            toElement = toArray.ElementType
            fromElement = fromArray.ElementType

            '•	From an array type S with an element type SE to an array type T with an element type TE, provided all of the following are true:
            '   •	S and T differ only in element type.
            If toArray.Rank <> fromArray.Rank Then
                If ShowError Then
                    Compiler.Report.ShowMessage(Messages.VBNC30414, Parent.Location, Helper.ToString(Compiler, FromType), Helper.ToString(Compiler, DestinationType))
                End If
                Return False
            End If
            If toArray.Dimensions.Count <> fromArray.Dimensions.Count Then
                If ShowError Then
                    Compiler.Report.ShowMessage(Messages.VBNC30414, Parent.Location, Helper.ToString(Compiler, FromType), Helper.ToString(Compiler, DestinationType))
                End If
                Return False
            End If
            '   •	Both SE and TE are reference types or are type parameters known to be a reference type.
            '   •	A widening reference, array, or type parameter conversion exists from SE to TE.
            If CecilHelper.IsReferenceTypeOrGenericReferenceTypeParameter(toElement) AndAlso CecilHelper.IsReferenceTypeOrGenericReferenceTypeParameter(fromElement) Then
                If IsConvertible(Parent, FromExpression, fromElement, toElement, False, Nothing, False, True) Then
                    If CreateConversionExpression Then
                        convExpr = New CTypeExpression(Parent, FromExpression, DestinationType, CTypeConversionType.Castclass)
                        If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
                    End If
                    Return True
                End If
            End If

            '•	From an array type S with an enumerated element type SE to an array type T with an element type TE, provided all of the following are true:
            If Helper.IsEnum(Parent.Compiler, fromElement) Then
                '   •	S and T differ only in element type.
                'This has been checked above

                '   •	TE is the underlying type of SE.
                Dim enumT As TypeReference = Helper.GetEnumType(Parent.Compiler, fromElement)
                If Helper.CompareType(enumT, toElement) Then
                    'Not sure if a conversion is required here
                    Return True
                End If
            End If

            'Narrowing conversions:
            '•	From an array type S with an element type SE, to an array type T with an element type TE, provided that all of the following are true:
            '   •	S and T differ only in element type.
            '   •	Both SE and TE are reference types or are type parameters not known to be value types.
            '   •	A narrowing reference, array, or type parameter conversion exists from SE to TE.
            If CecilHelper.IsValueType(toElement) = False AndAlso CecilHelper.IsValueType(fromElement) = False Then
                If IsConvertible(Parent, FromExpression, fromElement, toElement, False, Nothing, False, False) Then
                    If isStrict Then
                        If ShowError Then Compiler.Report.ShowMessage(Messages.VBNC30512, Parent.Location, Helper.ToString(Compiler, FromType), Helper.ToString(Compiler, DestinationType))
                        Return False
                    End If
                    If CreateConversionExpression Then
                        convExpr = New CTypeExpression(Parent, FromExpression, DestinationType, CTypeConversionType.Castclass)
                        If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
                    End If
                    Return True
                End If
            End If

            '•	From an array type S with an element type SE to an array type T with an enumerated element type TE, provided all of the following are true:
            If Helper.IsEnum(Parent.Compiler, toElement) Then
                '   •	S and T differ only in element type.
                'This has been checked above

                '   •	SE is the underlying type of TE.
                Dim enumT As TypeReference = Helper.GetEnumType(Parent.Compiler, toElement)
                If Helper.CompareType(enumT, fromElement) Then
                    If isStrict Then
                        If ShowError Then Compiler.Report.ShowMessage(Messages.VBNC30512, Parent.Location, Helper.ToString(Compiler, FromType), Helper.ToString(Compiler, DestinationType))
                        Return False
                    End If
                    If CreateConversionExpression Then
                        'Not sure if this is the right conversion
                        convExpr = New CTypeExpression(Parent, FromExpression, DestinationType, CTypeConversionType.Castclass)
                        If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
                    End If
                    Return True
                End If
            End If

            If ShowError Then
                If Helper.IsEnum(Compiler, fromElement) = False AndAlso CecilHelper.IsValueType(fromElement) AndAlso CecilHelper.IsValueType(toElement) = False Then
                    Compiler.Report.ShowMessage(Messages.VBNC30333, Parent.Location, Helper.ToString(Compiler, FromType), Helper.ToString(Compiler, DestinationType), Helper.ToString(Compiler, fromElement), Helper.ToString(Compiler, toElement))
                Else
                    Compiler.Report.ShowMessage(Messages.VBNC30332, Parent.Location, Helper.ToString(Compiler, FromType), Helper.ToString(Compiler, DestinationType), Helper.ToString(Compiler, fromElement), Helper.ToString(Compiler, toElement))
                End If
            End If
            Return False
        End If

        'Value Type conversions
        If fromTD IsNot Nothing AndAlso fromTD.IsValueType Then
            '•	From a value type to a base type.
            If Helper.CompareType(DestinationType, Parent.Compiler.TypeCache.System_Object) OrElse Helper.CompareType(DestinationType, Parent.Compiler.TypeCache.System_ValueType) Then
                If CreateConversionExpression Then
                    convExpr = New BoxExpression(Parent, convExpr, DestinationType)
                    If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
                End If
                Return True
            End If

            If Helper.IsEnum(Compiler, FromType) AndAlso Helper.CompareType(DestinationType, TypeCache.System_Enum) Then
                If CreateConversionExpression Then
                    convExpr = New CTypeExpression(Parent, convExpr, DestinationType, CTypeConversionType.Box)
                    If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
                End If
                Return True
            End If

            '•	From a value type to an interface type that the type implements.
            If fromTD.HasInterfaces Then
                Dim interfaces As Mono.Collections.Generic.Collection(Of TypeReference)
                interfaces = CecilHelper.GetInterfaces(FromType, True)
                For i As Integer = 0 To interfaces.Count - 1
                    If Helper.CompareType(DestinationType, interfaces(i)) Then
                        If CreateConversionExpression Then
                            convExpr = New BoxExpression(Parent, convExpr, DestinationType)
                            If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
                        End If
                        Return True
                    End If

                    If Helper.DoesTypeImplementInterface(Parent, interfaces(i), DestinationType) Then
                        If CreateConversionExpression Then
                            convExpr = New BoxExpression(Parent, convExpr, DestinationType)
                            If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
                        End If
                        Return True
                    End If
                Next
            End If
        End If

        'Narrowing conversions:
        '•	From Boolean to Byte, SByte, UShort, Short, UInteger, Integer, ULong, Long, Decimal, Single, or Double.
        '•	From Byte, SByte, UShort, Short, UInteger, Integer, ULong, Long, Decimal, Single, or Double to Boolean.
        '•	From Byte to SByte.
        '•	From SByte to Byte, UShort, UInteger, or ULong.
        '•	From UShort to Byte, SByte, or Short.
        '•	From Short to Byte, SByte, UShort, UInteger, or ULong.
        '•	From UInteger to Byte, SByte, UShort, Short, or Integer.
        '•	From Integer to Byte, SByte, UShort, Short, UInteger, or ULong.
        '•	From ULong to Byte, SByte, UShort, Short, UInteger, Integer, or Long.
        '•	From Long to Byte, SByte, UShort, Short, UInteger, Integer, or ULong.
        '•	From Decimal to Byte, SByte, UShort, Short, UInteger, Integer, ULong, or Long.
        '•	From Single to Byte, SByte, UShort, Short, UInteger, Integer, ULong, Long, or Decimal.
        '•	From Double to Byte, SByte, UShort, Short, UInteger, Integer, ULong, Long, Decimal, or Single.
        If isFromNullable = False AndAlso isToNullable = False Then
            Select Case fromTC
                Case TypeCode.Byte, TypeCode.SByte, TypeCode.UInt16, TypeCode.Int16, TypeCode.UInt32, TypeCode.Int32, TypeCode.UInt64, TypeCode.Int64, TypeCode.Decimal, TypeCode.Single
                    If toTC <> TypeCode.Object AndAlso Compiler.TypeResolution.IsExplicitlyConvertible(Compiler, fromTC, toTC) Then
                        If isStrict Then
                            If ShowError Then
                                If Helper.CompareType(TypeCache.System_Char, DestinationType) Then
                                    If Helper.CompareType(TypeCache.System_Decimal, FromType) OrElse Helper.CompareType(TypeCache.System_Double, FromType) OrElse Helper.CompareType(TypeCache.System_Single, FromType) OrElse Helper.CompareType(TypeCache.System_DBNull, FromType) OrElse Helper.CompareType(TypeCache.System_DateTime, FromType) Then
                                        Compiler.Report.ShowMessage(Messages.VBNC30311, Parent.Location, Helper.ToString(Compiler, FromType), Helper.ToString(Compiler, DestinationType))
                                    Else
                                        Compiler.Report.ShowMessage(Messages.VBNC32007, Parent.Location, Helper.ToString(Compiler, FromType))
                                    End If
                                ElseIf Helper.CompareType(TypeCache.System_DateTime, DestinationType) Then
                                    Compiler.Report.ShowMessage(Messages.VBNC30311, Parent.Location, Helper.ToString(Compiler, FromType), Helper.ToString(Compiler, DestinationType))
                                ElseIf Helper.CompareType(TypeCache.System_DBNull, DestinationType) Then
                                    Compiler.Report.ShowMessage(Messages.VBNC30311, Parent.Location, Helper.ToString(Compiler, FromType), Helper.ToString(Compiler, DestinationType))
                                Else
                                    Compiler.Report.ShowMessage(Messages.VBNC30512, Parent.Location, Helper.ToString(Compiler, FromType), Helper.ToString(Compiler, DestinationType))
                                End If
                            End If
                            Return False
                        End If
                        If CreateConversionExpression Then
                            convExpr = New CTypeExpression(Parent, FromExpression, DestinationType, CTypeConversionType.Intrinsic)
                            If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
                        End If
                        Return True
                    End If
                Case TypeCode.Boolean, TypeCode.Double
                    If toTC <> TypeCode.Object AndAlso Compiler.TypeResolution.IsExplicitlyConvertible(Compiler, fromTC, toTC) Then
                        If isStrict Then
                            If ShowError Then
                                If Helper.CompareType(TypeCache.System_Char, DestinationType) Then
                                    Compiler.Report.ShowMessage(Messages.VBNC30311, Parent.Location, Helper.ToString(Compiler, FromType), Helper.ToString(Compiler, DestinationType))
                                ElseIf Helper.CompareType(TypeCache.System_DateTime, DestinationType) Then
                                    If Helper.CompareType(TypeCache.System_Double, FromType) Then
                                        Compiler.Report.ShowMessage(Messages.VBNC30533, Parent.Location)
                                    Else
                                        Compiler.Report.ShowMessage(Messages.VBNC30311, Parent.Location, Helper.ToString(Compiler, FromType), Helper.ToString(Compiler, DestinationType))
                                    End If
                                ElseIf Helper.CompareType(TypeCache.System_DBNull, DestinationType) Then
                                    Compiler.Report.ShowMessage(Messages.VBNC30311, Parent.Location, Helper.ToString(Compiler, FromType), Helper.ToString(Compiler, DestinationType))
                                Else
                                    Compiler.Report.ShowMessage(Messages.VBNC30512, Parent.Location, Helper.ToString(Compiler, FromType), Helper.ToString(Compiler, DestinationType))
                                End If
                            End If
                            Return False
                        End If
                        If CreateConversionExpression Then
                            convExpr = New CTypeExpression(Parent, FromExpression, DestinationType)
                            If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
                        End If
                        Return True
                    End If
            End Select
        End If

        'Reference conversions
        If toTD IsNot Nothing AndAlso toTD.IsValueType Then
            '•	From a reference type to a more derived value type.
            If Helper.CompareType(FromType, Parent.Compiler.TypeCache.System_Object) OrElse Helper.CompareType(FromType, Parent.Compiler.TypeCache.System_ValueType) Then
                If isStrict Then
                    If ShowError Then Compiler.Report.ShowMessage(Messages.VBNC30512, Parent.Location, Helper.ToString(Compiler, FromType), Helper.ToString(Compiler, DestinationType))
                    Return False
                End If
                If CreateConversionExpression Then
                    If Helper.IsIntrinsicType(Compiler, DestinationType) Then
                        convExpr = New CTypeExpression(Parent, convExpr, DestinationType)
                    Else
                        convExpr = New CTypeExpression(Parent, convExpr, DestinationType, CTypeConversionType.Unbox_Ldobj)
                    End If
                    If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
                End If
                Return True
            End If
            '•	From an interface type to a value type, provided the value type implements the interface type
            If fromTD.IsInterface AndAlso Helper.DoesTypeImplementInterface(Parent, DestinationType, FromType) Then
                If isStrict Then
                    If ShowError Then Compiler.Report.ShowMessage(Messages.VBNC30512, Parent.Location, Helper.ToString(Compiler, FromType), Helper.ToString(Compiler, DestinationType))
                    Return False
                End If
                If CreateConversionExpression Then
                    convExpr = New CTypeExpression(Parent, convExpr, DestinationType, CTypeConversionType.Unbox_Ldobj)
                    If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
                End If
                Return True
            End If
        End If

        'Nullable Value Type conversions
        If fromTD IsNot Nothing AndAlso fromTD.IsValueType Then
            Dim nulledTo As TypeReference = Nothing
            Dim nulledFrom As TypeReference = Nothing

            If isFromNullable Then nulledFrom = CecilHelper.GetNulledType(FromType)
            If isToNullable Then nulledTo = CecilHelper.GetNulledType(DestinationType)

            '•	From a type T to the type T?.
            If isFromNullable = False AndAlso isToNullable AndAlso Helper.CompareType(nulledTo, FromType) Then
                If CreateConversionExpression Then
                    convExpr = New CTypeExpression(Parent, convExpr, DestinationType, CTypeConversionType.ToNullable)
                    If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
                End If
                Return True
            End If

            '•	From a type T? to a type S?, where there is a widening conversion from the type T to the type S.
            If isFromNullable AndAlso isToNullable AndAlso IsConvertible(Parent, FromExpression, nulledFrom, nulledTo, False, Nothing, False, True) Then
                If CreateConversionExpression Then
                    convExpr = New CTypeExpression(Parent, convExpr, DestinationType, CTypeConversionType.NullableToNullable)
                    If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
                End If
                Return True
            End If

            '•	From a type T to a type S?, where there is a widening conversion from the type T to the type S.
            If isFromNullable = False AndAlso isToNullable AndAlso IsConvertible(Parent, FromExpression, FromType, nulledTo, False, Nothing, False, True) Then
                If CreateConversionExpression Then
                    convExpr = New CTypeExpression(Parent, convExpr, nulledTo, CTypeConversionType.Intrinsic)
                    If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
                    convExpr = New CTypeExpression(Parent, convExpr, DestinationType, CTypeConversionType.ToNullable)
                    If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
                End If
                Return True
            End If

            '•	From a type T? to an interface type that the type T implements.
            If isFromNullable AndAlso toTD.IsInterface AndAlso Helper.DoesTypeImplementInterface(Parent, nulledFrom, DestinationType) Then
                If CreateConversionExpression Then
                    convExpr = New BoxExpression(Parent, convExpr, FromType)
                    If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
                    convExpr = New CTypeExpression(Parent, convExpr, DestinationType, CTypeConversionType.Castclass)
                    If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
                End If
                Return True
            End If

            'narrowing conversions
            '•	From a type T? to a type T.
            If isFromNullable AndAlso isToNullable = False AndAlso Helper.CompareType(nulledFrom, DestinationType) Then
                If isStrict Then
                    If ShowError Then Compiler.Report.ShowMessage(Messages.VBNC30512, Parent.Location, Helper.ToString(Compiler, FromType), Helper.ToString(Compiler, DestinationType))
                    Return False
                End If
                If CreateConversionExpression Then
                    convExpr = New CTypeExpression(Parent, convExpr, DestinationType, CTypeConversionType.FromNullable)
                    If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
                End If
                Return True
            End If
            '•	From a type T? to a type S?, where there is a narrowing conversion from the type T to the type S.
            If isFromNullable AndAlso isToNullable AndAlso IsConvertible(Parent, FromExpression, nulledFrom, nulledTo, False, Nothing, False, False) Then
                If isStrict Then
                    If ShowError Then Compiler.Report.ShowMessage(Messages.VBNC30512, Parent.Location, Helper.ToString(Compiler, FromType), Helper.ToString(Compiler, DestinationType))
                    Return False
                End If
                If CreateConversionExpression Then
                    'denullify
                    convExpr = New CTypeExpression(Parent, convExpr, nulledFrom, CTypeConversionType.FromNullable)
                    If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
                    'convert
                    If Not IsConvertible(Parent, convExpr, nulledFrom, nulledTo, True, convExpr, True, isStrict) Then Return False
                    'renullify
                    convExpr = New CTypeExpression(Parent, convExpr, DestinationType, CTypeConversionType.ToNullable)
                    If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
                End If
                Return True
            End If
            '•	From a type T to a type S?, where there is a narrowing conversion from the type T to the type S.
            If isFromNullable = False AndAlso isToNullable AndAlso IsConvertible(Parent, FromExpression, FromType, nulledTo, False, Nothing, False, False) Then
                If isStrict Then
                    If ShowError Then Compiler.Report.ShowMessage(Messages.VBNC30512, Parent.Location, Helper.ToString(Compiler, FromType), Helper.ToString(Compiler, DestinationType))
                    Return False
                End If
                If CreateConversionExpression Then
                    'convert
                    If Not IsConvertible(Parent, convExpr, FromType, nulledTo, True, convExpr, True, isStrict) Then Return False
                    'renullify
                    convExpr = New CTypeExpression(Parent, convExpr, DestinationType, CTypeConversionType.ToNullable)
                    If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
                End If
                Return True
            End If
            '•	From a type S? to a type T, where there is a conversion from the type S to the type T.
            If isFromNullable AndAlso isToNullable = False AndAlso IsConvertible(Parent, FromExpression, nulledFrom, DestinationType, False, Nothing, False, False) Then
                If isStrict Then
                    If ShowError Then Compiler.Report.ShowMessage(Messages.VBNC30512, Parent.Location, Helper.ToString(Compiler, FromType), Helper.ToString(Compiler, DestinationType))
                    Return False
                End If
                If CreateConversionExpression Then
                    'denullify
                    convExpr = New CTypeExpression(Parent, convExpr, nulledFrom, CTypeConversionType.FromNullable)
                    If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
                    'convert
                    If Not IsConvertible(Parent, convExpr, nulledFrom, DestinationType, True, convExpr, True, Nothing) Then Return False
                End If
                Return True
            End If
        End If

        'String conversions
        If Helper.CompareType(DestinationType, Compiler.TypeCache.System_String) Then
            '•	From Char to String.
            '•	From Char() to String.
            If Helper.CompareType(FromType, Compiler.TypeCache.System_Char) OrElse Helper.CompareType(FromType, Compiler.TypeCache.System_Char_Array) Then
                If CreateConversionExpression Then
                    convExpr = New CStrExpression(Parent, FromExpression)
                    If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
                End If
                Return True
            End If
            If fromTC <> TypeCode.DBNull AndAlso (fromTC <> TypeCode.Object OrElse Helper.CompareType(TypeCache.System_Object, FromType)) Then
                If isStrict Then
                    If ShowError Then
                        If Helper.CompareType(TypeCache.System_DBNull, FromType) Then
                            Compiler.Report.ShowMessage(Messages.VBNC30311, Parent.Location, Helper.ToString(Compiler, FromType), Helper.ToString(Compiler, DestinationType))
                        Else
                            Compiler.Report.ShowMessage(Messages.VBNC30512, Parent.Location, Helper.ToString(Compiler, FromType), Helper.ToString(Compiler, DestinationType))
                        End If
                    End If
                    Return False
                End If
                If CreateConversionExpression Then
                    convExpr = New CTypeExpression(Parent, FromExpression, DestinationType)
                    If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
                End If
                Return True
            End If
        ElseIf Helper.CompareType(FromType, TypeCache.System_String) Then
            '•	From String to Char.
            If Helper.CompareType(DestinationType, Compiler.TypeCache.System_Char) Then
                If isStrict Then
                    If ShowError Then Compiler.Report.ShowMessage(Messages.VBNC30512, Parent.Location, Helper.ToString(Compiler, FromType), Helper.ToString(Compiler, DestinationType))
                    Return False
                End If
                If CreateConversionExpression Then
                    convExpr = New CCharExpression(Parent, FromExpression)
                    If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
                End If
                Return True
            End If
            '•	From String to Char().
            If Helper.CompareType(DestinationType, Compiler.TypeCache.System_Char_Array) Then
                If isStrict Then
                    If ShowError Then Compiler.Report.ShowMessage(Messages.VBNC30512, Parent.Location, Helper.ToString(Compiler, FromType), Helper.ToString(Compiler, DestinationType))
                    Return False
                End If
                If CreateConversionExpression Then
                    convExpr = New CTypeExpression(Parent, FromExpression, Compiler.TypeCache.System_Char_Array)
                    If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
                End If
                Return True
            End If
            '•	From String to Boolean and from Boolean to String.
            '•	Conversions between String and Byte, SByte, UShort, Short, UInteger, Integer, ULong, Long, Decimal, Single, or Double.
            '•	From String to Date and from Date to String.
            'from boolean is handled in the numeric conversion code
            If toTC <> TypeCode.Object Then
                If isStrict Then
                    If ShowError Then
                        If Helper.CompareType(TypeCache.System_DBNull, DestinationType) Then
                            Compiler.Report.ShowMessage(Messages.VBNC30311, Parent.Location, Helper.ToString(Compiler, FromType), Helper.ToString(Compiler, DestinationType))
                        Else
                            Compiler.Report.ShowMessage(Messages.VBNC30512, Parent.Location, Helper.ToString(Compiler, FromType), Helper.ToString(Compiler, DestinationType))
                        End If
                    End If
                    Return False
                End If
                If CreateConversionExpression Then
                    convExpr = New CTypeExpression(Parent, FromExpression, DestinationType)
                    If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
                End If
                Return True
            End If
        End If

        'Type Parameter conversions
        fromTG = TryCast(FromType, GenericParameter)
        If fromTG IsNot Nothing Then
            '•	From a type parameter to Object.
            If Helper.CompareType(DestinationType, Parent.Compiler.TypeCache.System_Object) Then
                If CreateConversionExpression Then
                    convExpr = New BoxExpression(Parent, convExpr, DestinationType)
                    If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
                End If
                Return True
            End If

            If fromTG.HasConstraints Then
                For i As Integer = 0 To fromTG.Constraints.Count - 1
                    Dim constraint As TypeReference = fromTG.Constraints(i)

                    '•	From a type parameter to an interface type constraint or any interface variant compatible with an interface type constraint.
                    ' Same implementation as "From a type parameter to a class constraint, or a base type of the class constraint." below

                    '•	From a type parameter to an interface implemented by a class constraint.
                    If toTD IsNot Nothing AndAlso toTD.IsInterface Then
                        If Helper.DoesTypeImplementInterface(Parent, constraint, DestinationType) Then
                            If CreateConversionExpression Then
                                convExpr = New CTypeExpression(Parent, convExpr, DestinationType, CTypeConversionType.Box)
                                If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
                                convExpr = New CTypeExpression(Parent, convExpr, DestinationType, CTypeConversionType.Castclass)
                                If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
                            End If
                            Return True
                        End If
                    End If

                    '•	From a type parameter to an interface variant compatible with an interface implemented by a class constraint.
                    'TODO

                    '•	From a type parameter to a class constraint, or a base type of the class constraint.
                    If Helper.CompareType(constraint, DestinationType) Then
                        If CreateConversionExpression Then
                            convExpr = New CTypeExpression(Parent, convExpr, DestinationType, CTypeConversionType.Box)
                            If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
                            convExpr = New CTypeExpression(Parent, convExpr, DestinationType, CTypeConversionType.Castclass)
                            If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
                        End If
                        Return True
                    End If
                    If Helper.IsSubclassOf(DestinationType, fromTG.Constraints(i)) Then
                        If CreateConversionExpression Then
                            convExpr = New CTypeExpression(Parent, convExpr, DestinationType, CTypeConversionType.Box)
                            If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
                            convExpr = New CTypeExpression(Parent, convExpr, DestinationType, CTypeConversionType.Castclass)
                            If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
                        End If
                        Return True
                    End If

                    '•	From a type parameter T to a type parameter constraint TX, or anything TX has a widening conversion to.
                    'TODO

                Next
            End If

            'narrowing conversions
            '•	From Object to a type parameter (handled below)
            '•	From a type parameter to an interface type, provided the type parameter is not constrained to that interface or constrained to a class that implements that interface.
            If toTD IsNot Nothing AndAlso toTD.IsInterface Then
                If isStrict Then
                    If ShowError Then Compiler.Report.ShowMessage(Messages.VBNC30512, Parent.Location, Helper.ToString(Compiler, FromType), Helper.ToString(Compiler, DestinationType))
                    Return False
                End If
                If CreateConversionExpression Then
                    convExpr = New CTypeExpression(Parent, FromExpression, DestinationType, CTypeConversionType.Castclass)
                    If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
                End If
                Return True
            End If
            '•	From an interface type to a type parameter (handled below)
            If fromTG.HasConstraints Then
                For i As Integer = 0 To fromTG.Constraints.Count - 1
                    Dim constraint As TypeReference = fromTG.Constraints(i)

                    '•	From a type parameter to a derived type of a class constraint.
                    If CecilHelper.IsClass(constraint) Then 'A bit more is needed here
                        If isStrict Then
                            If ShowError Then Compiler.Report.ShowMessage(Messages.VBNC30311, Parent.Location, Helper.ToString(Compiler, FromType), Helper.ToString(Compiler, DestinationType))
                            Return False
                        End If
                        If CreateConversionExpression Then
                            convExpr = New CTypeExpression(Parent, FromExpression, DestinationType, CTypeConversionType.Box_CastClass)
                            If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
                        End If
                        Return True
                    End If

                    '•	From a type parameter T to anything a type parameter constraint TX has a narrowing conversion to.
                    If IsConvertible(Parent, convExpr, constraint, DestinationType, False, Nothing, False, Nothing) Then
                        If isStrict Then
                            If ShowError Then Compiler.Report.ShowMessage(Messages.VBNC30512, Parent.Location, Helper.ToString(Compiler, FromType), Helper.ToString(Compiler, DestinationType))
                            Return False
                        End If
                        If CreateConversionExpression Then
                            convExpr = New CTypeExpression(Parent, convExpr, constraint, CTypeConversionType.Box_CastClass)
                            If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
                            If Not IsConvertible(Parent, convExpr, constraint, DestinationType, True, convExpr, True, Nothing) Then Return True
                        End If
                        Return True
                    End If
                Next
            End If

            If ShowError Then
                Compiler.Report.ShowMessage(Messages.VBNC30311, Parent.Location, Helper.ToString(Compiler, FromType), Helper.ToString(Compiler, DestinationType))
            End If
            Return False
        End If

        toTG = TryCast(DestinationType, GenericParameter)
        If toTG IsNot Nothing Then
            '•	From Object to a type parameter.
            '•	From an interface type to a type parameter.
            If Helper.CompareType(FromType, TypeCache.System_Object) OrElse (fromTD IsNot Nothing AndAlso fromTD.IsInterface) Then
                If isStrict Then
                    If ShowError Then Compiler.Report.ShowMessage(Messages.VBNC30512, Parent.Location, Helper.ToString(Compiler, FromType), Helper.ToString(Compiler, DestinationType))
                    Return False
                End If
                If CreateConversionExpression Then
                    convExpr = New CTypeExpression(Parent, FromExpression, DestinationType, CTypeConversionType.MS_VB_CS_Conversions_ToGenericParameter)
                    If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
                End If
                Return True
            End If
        End If

        'Reference (narrowing) conversions
        If fromTD IsNot Nothing AndAlso toTD IsNot Nothing Then
            If ((fromTD.IsInterface OrElse fromTD.IsClass) AndAlso fromTD.IsValueType = False) AndAlso (toTD.IsInterface OrElse toTD.IsClass) AndAlso toTD.IsValueType = False Then
                '•	From a reference type to a more derived type.
                If Helper.IsSubclassOf(FromType, DestinationType) Then
                    If isStrict Then
                        If ShowError Then Compiler.Report.ShowMessage(Messages.VBNC30512, Parent.Location, Helper.ToString(Compiler, FromType), Helper.ToString(Compiler, DestinationType))
                        Return False
                    End If
                    convExpr = New CTypeExpression(Parent, FromExpression, DestinationType, CTypeConversionType.Castclass)
                    If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
                    Return True
                End If
                '•	From a class type to an interface type, provided the class type does not implement the interface type or an interface type variant compatible with it.
                '•	From an interface type to a class type. 
                '•	From an interface type to another interface type, provided there is no inheritance relationship between the two types and provided they are not variant compatible.
                'Put those 3 conditions in one bug chunk here
                If fromTD.IsInterface OrElse toTD.IsInterface Then
                    If isStrict Then
                        If ShowError Then Compiler.Report.ShowMessage(Messages.VBNC30512, Parent.Location, Helper.ToString(Compiler, FromType), Helper.ToString(Compiler, DestinationType))
                        Return False
                    End If
                    If CreateConversionExpression Then
                        convExpr = New CTypeExpression(Parent, FromExpression, DestinationType, CTypeConversionType.Castclass)
                        If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
                    End If
                    Return True
                End If
            End If
        End If

        'Check user defined operators
        If fromTD IsNot Nothing AndAlso toTD IsNot Nothing AndAlso _
            ((fromTC = TypeCode.Object AndAlso Helper.CompareType(TypeCache.System_Object, DestinationType) = False) OrElse _
                (toTC = TypeCode.Object AndAlso Helper.CompareType(TypeCache.System_Object, DestinationType) = False)) Then
            'user-defined operators can only exist if either the from or to type aren't intrinsic types

            Dim ops As Generic.List(Of MethodReference)
            Dim isNarrowing As Boolean
            ops = Helper.GetWideningConversionOperators(Compiler, FromType, DestinationType)

            If ops Is Nothing OrElse ops.Count = 0 Then
                ops = Helper.GetNarrowingConversionOperators(Compiler, FromType, DestinationType)
                isNarrowing = True
            End If

            If ops Is Nothing OrElse ops.Count = 0 Then
                If ShowError Then
                    If isFromNullable AndAlso isToNullable AndAlso Compiler.TypeResolver.IsExplicitlyConvertible(Compiler, Helper.GetTypeCode(Compiler, CecilHelper.GetNulledType(FromType)), Helper.GetTypeCode(Compiler, CecilHelper.GetNulledType(DestinationType))) Then
                        Compiler.Report.ShowMessage(Messages.VBNC30512, FromExpression.Location, Helper.ToString(FromExpression, FromType), Helper.ToString(FromExpression, DestinationType))
                    Else
                        Compiler.Report.ShowMessage(Messages.VBNC30311, FromExpression.Location, Helper.ToString(FromExpression, FromType), Helper.ToString(FromExpression, DestinationType))
                    End If
                End If
                Return False
            End If

            If ops.Count > 1 Then
                If ShowError Then
                    Return Compiler.Report.ShowMessage(Messages.VBNC30311, FromExpression.Location, Helper.ToString(Compiler, FromType), Helper.ToString(Compiler, DestinationType))
                End If
                Return False
            End If

            If isNarrowing AndAlso isStrict.Value Then
                If ShowError Then
                    Return Compiler.Report.ShowMessage(Messages.VBNC30512, FromExpression.Location, Helper.ToString(Compiler, FromType), Helper.ToString(Compiler, DestinationType))
                End If
                Return False
            End If

            If CreateConversionExpression Then
                convExpr = New CTypeExpression(Parent, FromExpression, DestinationType, CTypeConversionType.UserDefinedOperator)
                DirectCast(convExpr, CTypeExpression).ConversionMethod = ops(0)
                If Not convExpr.ResolveExpression(ResolveInfo.Default(Compiler)) Then Return False
            End If
            Return True
        End If


        If ShowError Then
            If Helper.CompareType(TypeCache.System_DBNull, DestinationType) Then
                Compiler.Report.ShowMessage(Messages.VBNC30311, Parent.Location, Helper.ToString(Compiler, FromType), Helper.ToString(Compiler, DestinationType))
            ElseIf Helper.CompareType(TypeCache.System_Char, FromType) Then
                If Helper.CompareType(TypeCache.System_DateTime, DestinationType) OrElse Helper.CompareType(TypeCache.System_Double, DestinationType) OrElse Helper.CompareType(TypeCache.System_Single, DestinationType) OrElse Helper.CompareType(TypeCache.System_Decimal, DestinationType) OrElse Helper.Compare(TypeCache.System_Boolean, DestinationType) Then
                    Compiler.Report.ShowMessage(Messages.VBNC30311, Parent.Location, Helper.ToString(Compiler, FromType), Helper.ToString(Compiler, DestinationType))
                Else
                    Compiler.Report.ShowMessage(Messages.VBNC32006, Parent.Location, Helper.ToString(Compiler, DestinationType))
                End If
            ElseIf Helper.CompareType(TypeCache.System_Char, DestinationType) Then
                If Helper.CompareType(TypeCache.System_Decimal, FromType) OrElse Helper.CompareType(TypeCache.System_Double, FromType) OrElse Helper.CompareType(TypeCache.System_Single, FromType) OrElse Helper.CompareType(TypeCache.System_DBNull, FromType) OrElse Helper.CompareType(TypeCache.System_DateTime, FromType) OrElse Helper.CompareType(TypeCache.System_Boolean, FromType) Then
                    Compiler.Report.ShowMessage(Messages.VBNC30311, Parent.Location, Helper.ToString(Compiler, FromType), Helper.ToString(Compiler, DestinationType))
                Else
                    Compiler.Report.ShowMessage(Messages.VBNC32007, Parent.Location, Helper.ToString(Compiler, FromType))
                End If
            ElseIf Helper.CompareType(TypeCache.System_DateTime, FromType) AndAlso Helper.CompareType(TypeCache.System_Double, DestinationType) Then
                Compiler.Report.ShowMessage(Messages.VBNC30532, Parent.Location)
            ElseIf Helper.CompareType(TypeCache.System_DateTime, DestinationType) AndAlso Helper.CompareType(TypeCache.System_Double, FromType) Then
                Compiler.Report.ShowMessage(Messages.VBNC30533, Parent.Location)
            Else
                Compiler.Report.ShowMessage(Messages.VBNC30311, Parent.Location, Helper.ToString(Compiler, FromType), Helper.ToString(Compiler, DestinationType))
            End If
        End If
        Return False
    End Function

    ''' <summary>
    ''' Returns true if all types in both arrays are the exact same types.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function CompareTypes(ByVal Types1() As Mono.Cecil.TypeReference, ByVal Types2() As Mono.Cecil.TypeReference) As Boolean
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

    Shared Function CompareMethod(ByVal m1 As Mono.Cecil.MethodReference, ByVal m2 As Mono.Cecil.MethodReference) As Boolean
        Dim g1 As Mono.Cecil.GenericInstanceMethod
        Dim g2 As Mono.Cecil.GenericInstanceMethod

        If m1 Is Nothing AndAlso m2 Is Nothing Then Return True
        If m1 Is Nothing Xor m2 Is Nothing Then Return False

        If m1 Is m2 Then Return True
        If Helper.CompareNameOrdinal(m1.Name, m2.Name) = False Then Return False
        If m1.Parameters.Count <> m2.Parameters.Count Then Return False
        If m1.GenericParameters.Count <> m2.GenericParameters.Count Then Return False
        If Helper.Compare(m1.DeclaringType, m2.DeclaringType) = False Then Return False

        For i As Integer = 0 To m1.Parameters.Count - 1
            If Helper.CompareType(m1.Parameters(i).ParameterType, m2.Parameters(i).ParameterType) = False Then Return False
        Next

        g1 = TryCast(m1, Mono.Cecil.GenericInstanceMethod)
        g2 = TryCast(m2, Mono.Cecil.GenericInstanceMethod)

        If g1 IsNot Nothing AndAlso g2 IsNot Nothing Then
            If g1.GenericArguments.Count <> g2.GenericArguments.Count Then Return False
            For i As Integer = 0 To g1.GenericArguments.Count - 1
                If Helper.CompareType(g1.GenericArguments(i), g2.GenericArguments(i)) = False Then Return False
            Next
        ElseIf g1 IsNot Nothing Xor g2 IsNot Nothing Then
            Return False
        End If

        Return True
    End Function

    Shared Function CompareType(ByVal t1 As Mono.Cecil.TypeReference, ByVal t2 As Mono.Cecil.TypeReference) As Boolean
        If t1 Is t2 Then Return True
        If t1 Is Nothing OrElse t2 Is Nothing Then Return False

        Dim g1 As Mono.Cecil.GenericParameter = TryCast(t1, Mono.Cecil.GenericParameter)
        Dim g2 As Mono.Cecil.GenericParameter = TryCast(t2, Mono.Cecil.GenericParameter)

        If g1 IsNot Nothing AndAlso g2 IsNot Nothing Then
            Return Helper.CompareNameOrdinal(g1.Name, g2.Name) AndAlso Helper.Compare(g1.Owner, g2.Owner)
        ElseIf g1 IsNot Nothing Xor g2 IsNot Nothing Then
            Return False
        End If

        Dim gi1 As Mono.Cecil.GenericInstanceType = TryCast(t1, Mono.Cecil.GenericInstanceType)
        Dim gi2 As Mono.Cecil.GenericInstanceType = TryCast(t2, Mono.Cecil.GenericInstanceType)

        If gi1 IsNot Nothing AndAlso gi2 IsNot Nothing Then
            If gi1 Is gi2 Then Return True
            If Not Helper.CompareType(gi1.GetElementType, gi2.GetElementType) Then Return False
            If gi1.GenericArguments.Count <> gi2.GenericArguments.Count Then Return False
            For i As Integer = 0 To gi1.GenericArguments.Count - 1
                If Helper.CompareType(gi1.GenericArguments(i), gi2.GenericArguments(i)) = False Then
                    Return False
                End If
            Next
            Helper.Assert(gi1.FullName = gi2.FullName)
            Return True
        ElseIf gi1 IsNot Nothing Xor gi2 IsNot Nothing Then
            Return False
        End If

        Dim a1 As Mono.Cecil.ArrayType = TryCast(t1, Mono.Cecil.ArrayType)
        Dim a2 As Mono.Cecil.ArrayType = TryCast(t2, Mono.Cecil.ArrayType)
        If a1 IsNot Nothing AndAlso a2 IsNot Nothing Then
            If a1.Dimensions.Count <> a2.Dimensions.Count Then Return False
            For i As Integer = 0 To a1.Dimensions.Count - 1
                If a1.Dimensions(i).LowerBound.HasValue Xor a2.Dimensions(i).LowerBound.HasValue Then Return False
                If a1.Dimensions(i).UpperBound.HasValue Xor a2.Dimensions(i).UpperBound.HasValue Then Return False

                If a1.Dimensions(i).LowerBound.HasValue AndAlso a2.Dimensions(i).LowerBound.HasValue Then
                    If a1.Dimensions(i).LowerBound.Value <> a2.Dimensions(i).LowerBound.Value Then Return False
                End If

                If a1.Dimensions(i).UpperBound.HasValue AndAlso a2.Dimensions(i).UpperBound.HasValue Then
                    If a1.Dimensions(i).UpperBound.Value <> a2.Dimensions(i).UpperBound.Value Then Return False
                End If
            Next
            Return CompareType(a1.ElementType, a2.ElementType)
        ElseIf a1 IsNot Nothing Xor a2 IsNot Nothing Then
            'Only one of them is an array
            Return False
        End If

        Dim r1 As ByReferenceType = TryCast(t1, ByReferenceType)
        Dim r2 As ByReferenceType = TryCast(t2, ByReferenceType)
        If r1 IsNot Nothing AndAlso r2 IsNot Nothing Then
            Return Helper.CompareType(r1.ElementType, r2.ElementType)
        ElseIf r1 IsNot Nothing Xor r2 IsNot Nothing Then
            Return False
        End If

        If t1.IsNested AndAlso t2.IsNested AndAlso CompareType(t1.DeclaringType, t2.DeclaringType) = False Then
            Return False
        End If

        If CecilHelper.FindDefinition(t1) Is CecilHelper.FindDefinition(t2) Then Return True
        If t1.FullName IsNot Nothing AndAlso t2.FullName IsNot Nothing AndAlso Helper.CompareNameOrdinal(t1.FullName, t2.FullName) Then Return True
        Helper.Assert(t1.FullName.Equals(t2.FullName) = False)

        Return False
    End Function

    Shared Function Compare(ByVal g1 As Mono.Cecil.IGenericParameterProvider, ByVal g2 As Mono.Cecil.IGenericParameterProvider) As Boolean
        Helper.Assert(g1 IsNot Nothing AndAlso g2 IsNot Nothing)
        Dim m1 As Mono.Cecil.MethodReference = TryCast(g1, Mono.Cecil.MethodReference)
        Dim m2 As Mono.Cecil.MethodReference = TryCast(g2, Mono.Cecil.MethodReference)

        If m1 IsNot Nothing AndAlso m2 IsNot Nothing Then
            Return m1 Is m2
        ElseIf m1 IsNot Nothing Xor m2 IsNot Nothing Then
            Return False
        End If

        Dim t1 As Mono.Cecil.TypeReference = TryCast(g1, Mono.Cecil.TypeReference)
        Dim t2 As Mono.Cecil.TypeReference = TryCast(g2, Mono.Cecil.TypeReference)

        If t1 IsNot Nothing AndAlso t2 IsNot Nothing Then
            Return Helper.CompareType(t1, t2)
        End If

        Throw New NotImplementedException
    End Function

    ''' <summary>
    ''' Creates a vb-like representation of the parameters
    ''' </summary>
    ''' <param name="Params"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Overloads Shared Function ToString(ByVal Context As BaseObject, ByVal Params As Mono.Collections.Generic.Collection(Of ParameterDefinition)) As String
        Dim result As New Text.StringBuilder()

        result.Append("(")
        For i As Integer = 0 To Params.Count - 1
            Dim t As Mono.Cecil.ParameterDefinition = Params(i)
            Dim tmp As String = String.Empty
            Dim tp As TypeReference

            tp = t.ParameterType
            If CecilHelper.IsByRef(tp) Then
                result.Append("ByRef ")
                tp = CecilHelper.GetElementType(tp)
            End If
            If t.IsOptional Then
                result.Append("Optional ")
            End If
            If Helper.IsParamArrayParameter(Context, t) Then
                result.Append("ParamArray ")
            End If
            result.Append(t.Name)
            If CecilHelper.IsArray(tp) AndAlso CecilHelper.GetArrayRank(tp) = 1 Then
                result.Append("()")
                tp = CecilHelper.GetElementType(tp)
            End If
            result.Append(" As ")
            result.Append(ToString(Context, tp))
            If i < Params.Count - 1 Then result.Append(", ")
        Next
        result.Append(")")

        Return result.ToString()
    End Function

    Shared Function IsParamArrayParameter(ByVal Context As BaseObject, ByVal Parameter As Mono.Cecil.ParameterReference) As Boolean
        Dim result As Boolean
        Dim pD As Mono.Cecil.ParameterDefinition = CecilHelper.FindDefinition(Parameter)
        result = CecilHelper.IsDefined(pD.CustomAttributes, Context.Compiler.TypeCache.System_ParamArrayAttribute)
        LogResolutionMessage(Context.Compiler, "IsParamArrayParameter: result=" & result & ", ParamArrayAttribute=" & Context.Compiler.TypeCache.System_ParamArrayAttribute.FullName)
        Return result
    End Function

    Shared Function GetMemberName(ByVal Member As MemberReference) As String
        Dim mr As MethodReference = TryCast(Member, MethodReference)
        If mr IsNot Nothing Then Return GetMethodName(mr)
        Return Member.Name
    End Function

    Shared Function GetMethodName(ByVal Method As MethodReference) As String
        Select Case Method.Name
            Case "op_BitwiseAnd", "op_LogicalAnd"
                Return "And"
            Case "op_Like"
                Return "Like"
            Case "op_Modulus"
                Return "Mod"
            Case "op_BitwiseOr", "op_LogicalOr"
                Return "Or"
            Case "op_ExclusiveOr"
                Return "XOr"
            Case "op_LessThan"
                Return "<"
            Case "op_GreaterThan"
                Return ">"
            Case "op_Equality"
                Return "="
            Case "op_Inequality"
                Return "<>"
            Case "op_LessThanOrEqual"
                Return "<="
            Case "op_GreaterThanOrEqual"
                Return ">="
            Case "op_Concatenate"
                Return "&"
            Case "op_Multiply"
                Return "*"
            Case "op_Addition"
                Return "+"
            Case "op_Subtraction"
                Return "-"
            Case "op_Exponent"
                Return "^"
            Case "op_Division"
                Return "/"
            Case "op_IntegerDivision"
                Return "\"
            Case "op_LeftShift", "op_SignedRightShift"
                Return "<<"
            Case "op_RightShift", "op_UnsignedRightShift"
                Return ">>"
            Case "op_True"
                Return "IsTrue"
            Case "op_False"
                Return "IsFalse"
            Case Else
                Return Method.Name
        End Select
    End Function

    Overloads Shared Function ToString(ByVal Types As Mono.Cecil.TypeReference()) As String
        Dim result As String = ""
        Dim sep As String = ""

        If Types IsNot Nothing Then
            For Each t As Mono.Cecil.TypeReference In Types
                Helper.Assert(t IsNot Nothing)
                result &= sep & t.ToString
                sep = ", "
            Next
        End If

        Return "{" & result & "}"
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

    Overloads Shared Function ToString(ByVal Accessibility As ModifierMasks) As String
        Select Case Accessibility
            Case ModifierMasks.Protected Or ModifierMasks.Friend
                Return "Protected Friend"
            Case ModifierMasks.Protected
                Return "Protected"
            Case ModifierMasks.Friend
                Return "Friend"
            Case ModifierMasks.Public
                Return "Public"
            Case ModifierMasks.Private
                Return "Private"
            Case Else
                Return "<unknown>"
        End Select
    End Function

    Overloads Shared Function ToString(ByVal Accessibility As Mono.Cecil.FieldAttributes) As String
        Select Case Accessibility
            Case Mono.Cecil.FieldAttributes.FamANDAssem
                Return "Protected Friend"
            Case Mono.Cecil.FieldAttributes.FamORAssem
                Return "Protected Friend"
            Case Mono.Cecil.FieldAttributes.Family
                Return "Protected"
            Case Mono.Cecil.FieldAttributes.Assembly
                Return "Friend"
            Case Mono.Cecil.FieldAttributes.Public
                Return "Public"
            Case Mono.Cecil.FieldAttributes.Private
                Return "Private"
            Case Else
                Return "<unknown>"
        End Select
    End Function

    Overloads Shared Function ToString(ByVal Accessibility As Mono.Cecil.MethodAttributes) As String
        Select Case Accessibility
            Case Mono.Cecil.MethodAttributes.FamANDAssem
                Return "Protected Friend"
            Case Mono.Cecil.MethodAttributes.FamORAssem
                Return "Protected Friend"
            Case Mono.Cecil.MethodAttributes.Family
                Return "Protected"
            Case Mono.Cecil.MethodAttributes.Assembly
                Return "Friend"
            Case Mono.Cecil.MethodAttributes.Public
                Return "Public"
            Case Mono.Cecil.MethodAttributes.Private
                Return "Private"
            Case Else
                Return "<unknown>"
        End Select
    End Function

    Overloads Shared Function ToString(ByVal Accessibility As Mono.Cecil.TypeAttributes) As String
        Select Case Accessibility
            Case Mono.Cecil.TypeAttributes.NestedFamANDAssem
                Return "Protected Friend"
            Case Mono.Cecil.TypeAttributes.NestedFamORAssem
                Return "Protected Friend"
            Case Mono.Cecil.TypeAttributes.NestedFamANDAssem
                Return "Protected"
            Case Mono.Cecil.TypeAttributes.NestedAssembly, Mono.Cecil.TypeAttributes.NotPublic
                Return "Friend"
            Case Mono.Cecil.TypeAttributes.NestedPublic, Mono.Cecil.TypeAttributes.Public
                Return "Public"
            Case Mono.Cecil.TypeAttributes.NestedPrivate
                Return "Private"
            Case Else
                Return "<unknown>"
        End Select
    End Function

    Overloads Shared Function ToString(ByVal Context As BaseObject, ByVal Member As MethodReference) As String
        Dim builder As New Text.StringBuilder()
        Dim isSub As Boolean = Helper.CompareType(Member.ReturnType, Context.Compiler.TypeCache.System_Void)

        builder.Append(ToString(GetAccessibility(Member)))
        builder.Append(" ")
        If Helper.CompareNameOrdinal(Member.Name, ".ctor") Then
            builder.Append("Sub New(")
            builder.Append(Helper.ToString(Context, Helper.GetParameters(Context, Member)))
            builder.Append(")")
        Else
            If isSub Then
                builder.Append("Sub ")
            Else
                builder.Append("Function ")
            End If
            builder.Append(Member.Name)
            If Member.HasGenericParameters Then
                builder.Append("(Of ")
                For i As Integer = 0 To Member.GenericParameters.Count - 1
                    Dim gp As GenericParameter = Member.GenericParameters(i)
                    Dim constraints As New Text.StringBuilder
                    Dim constraintCount As Integer

                    If i > 0 Then builder.Append(", ")
                    builder.Append(gp.Name)

                    If gp.HasNotNullableValueTypeConstraint Then
                        If constraintCount > 0 Then constraints.Append(", ")
                        constraints.Append("Structure")
                        constraintCount += 1
                    End If
                    If gp.HasDefaultConstructorConstraint AndAlso gp.HasNotNullableValueTypeConstraint = False Then
                        If constraintCount > 0 Then constraints.Append(", ")
                        constraints.Append("New")
                        constraintCount += 1
                    End If
                    If gp.HasReferenceTypeConstraint Then
                        If constraintCount > 0 Then constraints.Append(", ")
                        constraints.Append("Class")
                        constraintCount += 1
                    End If
                    For c As Integer = 0 To gp.Constraints.Count - 1
                        If Helper.CompareType(Context.Compiler.TypeCache.System_ValueType, gp.Constraints(i)) Then Continue For
                        If constraintCount > 0 Then constraints.Append(", ")
                        constraints.Append(gp.Constraints(i).Name)
                        constraintCount += 1
                    Next
                    If constraintCount > 0 Then
                        builder.Append(" As ")
                        If constraintCount > 1 Then builder.Append("{")
                        builder.Append(constraints)
                        If constraintCount > 1 Then builder.Append("}")
                    End If
                Next
                builder.Append(")")
            End If
            'builder.Append("(")
            'For i As Integer = 0 To Member.Parameters.Count - 1
            'If i > 0 Then builder.Append(", ")
            'Dim param As ParameterDefinition = Member.Parameters(i)
            'If CecilHelper.IsByRef(param.ParameterType) Then builder.Append("ByRef ")
            'builder.Append(param.Name)
            'builder.Append(" As ")
            'builder.Append(Helper.ToString(Context, param.ParameterType))
            builder.Append(Helper.ToString(Context, Member.Parameters))
            'Next
            'builder.Append(")")
            If isSub = False Then
                builder.Append(" As ")
                builder.Append(Helper.ToString(Context, Member.ReturnType))
            End If
        End If

        Return builder.ToString()
    End Function

    Overloads Shared Function ToString(ByVal Context As BaseObject, ByVal Member As TypeReference) As String
        Dim typeDefinition As TypeDefinition

        If Member Is Nothing Then Return "Nothing"

        typeDefinition = TryCast(Member, TypeDefinition)
        If typeDefinition IsNot Nothing AndAlso Helper.IsDelegate(Context.Compiler, typeDefinition) Then
            Dim builder As New Text.StringBuilder()
            Dim delegateType As Mono.Cecil.TypeDefinition = DirectCast(Member, Mono.Cecil.TypeDefinition)
            Dim invoke As Mono.Cecil.MethodReference = GetInvokeMethod(Context.Compiler, delegateType)

            builder.Append("Delegate ")
            builder.Append(ToString(Context, invoke))
            If Helper.CompareType(invoke.ReturnType, Context.Compiler.TypeCache.System_Void) Then
                builder.Replace("Delegate Sub " + invoke.Name + "(", "Delegate Sub " + delegateType.Name + "(")
            Else
                builder.Replace("Delegate Function " + invoke.Name + "(", "Delegate Function " + delegateType.Name + "(")
            End If
            Return builder.ToString()
        ElseIf CecilHelper.IsNullable(Member) Then
            Return ToString(Context, CecilHelper.GetNulledType(Member)) & "?"
        Else
            If Helper.CompareType(Member, Context.Compiler.TypeCache.System_Byte) Then
                Return "Byte"
            ElseIf Helper.CompareType(Member, Context.Compiler.TypeCache.System_Boolean) Then
                Return "Boolean"
            ElseIf Helper.CompareType(Member, Context.Compiler.TypeCache.System_Char) Then
                Return "Char"
            ElseIf Helper.CompareType(Member, Context.Compiler.TypeCache.System_DateTime) Then
                Return "Date"
            ElseIf Helper.CompareType(Member, Context.Compiler.TypeCache.System_DBNull) Then
                Return "System.DBNull"
            ElseIf Helper.CompareType(Member, Context.Compiler.TypeCache.System_Decimal) Then
                Return "Decimal"
            ElseIf Helper.CompareType(Member, Context.Compiler.TypeCache.System_Double) Then
                Return "Double"
            ElseIf Helper.CompareType(Member, Context.Compiler.TypeCache.System_Int16) Then
                Return "Short"
            ElseIf Helper.CompareType(Member, Context.Compiler.TypeCache.System_Int32) Then
                Return "Integer"
            ElseIf Helper.CompareType(Member, Context.Compiler.TypeCache.System_Int64) Then
                Return "Long"
            ElseIf Helper.CompareType(Member, Context.Compiler.TypeCache.System_SByte) Then
                Return "SByte"
            ElseIf Helper.CompareType(Member, Context.Compiler.TypeCache.System_Single) Then
                Return "Single"
            ElseIf Helper.CompareType(Member, Context.Compiler.TypeCache.System_String) Then
                Return "String"
            ElseIf Helper.CompareType(Member, Context.Compiler.TypeCache.System_UInt16) Then
                Return "UShort"
            ElseIf Helper.CompareType(Member, Context.Compiler.TypeCache.System_UInt32) Then
                Return "UInteger"
            ElseIf Helper.CompareType(Member, Context.Compiler.TypeCache.System_UInt64) Then
                Return "ULong"
            ElseIf Helper.CompareType(Member, Context.Compiler.TypeCache.System_Object) Then
                Return "Object"
            Else
                Return Member.ToString()
            End If
        End If
    End Function

    Overloads Shared Function ToString(ByVal Context As BaseObject, ByVal Member As PropertyReference) As String
        Dim builder As New Text.StringBuilder()
        Dim pd As PropertyDefinition = CecilHelper.FindDefinition(Member)

        builder.Append(ToString(GetAccessibility(pd)))
        builder.Append(" ")
        If pd.GetMethod Is Nothing Then
            builder.Append("WriteOnly ")
        ElseIf pd.SetMethod Is Nothing Then
            builder.Append("ReadOnly ")
        End If
        If IsDefaultProperty(Context.Compiler, pd) Then
            builder.Append("Default ")
        End If
        builder.Append("Property ")
        builder.Append(Member.Name)
        builder.Append(Helper.ToString(Context, Helper.GetParameters(Context, Member)))
        builder.Append(" As ")
        builder.Append(Helper.ToString(Context, pd.PropertyType))

        Return builder.ToString()
    End Function

    Overloads Shared Function ToString(ByVal Context As BaseObject, ByVal Member As Mono.Cecil.MemberReference) As String
        Dim methodReference As MethodReference
        Dim propertyReference As PropertyReference
        Dim typeReference As TypeReference

        methodReference = TryCast(Member, MethodReference)
        If methodReference IsNot Nothing Then Return ToString(Context, methodReference)

        propertyReference = TryCast(Member, PropertyReference)
        If propertyReference IsNot Nothing Then Return ToString(Context, propertyReference)

        typeReference = TryCast(Member, TypeReference)
        If typeReference IsNot Nothing Then Return ToString(Context, DirectCast(Member, TypeReference))

        Context.Compiler.Report.ShowMessage(Messages.VBNC99997, Context.Location)

        Return String.Empty
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
    Shared Function GetOptionalValueExpression(ByVal Parent As ParsedObject, ByVal Parameter As Mono.Cecil.ParameterDefinition) As Expression
        Dim result As Expression
        If Helper.CompareType(Parameter.ParameterType, Parent.Compiler.TypeCache.System_Object) AndAlso Helper.IsOnMS AndAlso Parameter.Constant Is DBNull.Value Then
            'Mono hasn't implemented ParameterInfo.RawDefaultValue yet.

            'If an Object parameter does not specify a default value, then the expression 
            'System.Reflection.Missing.Value is used. 
            result = New LoadFieldExpression(Parent, Parent.Compiler.TypeCache.System_Reflection_Missing__Value)
        ElseIf Helper.CompareType(Parameter.ParameterType, Parent.Compiler.TypeCache.System_Int32) AndAlso CecilHelper.IsDefined(Parameter.CustomAttributes, Parent.Compiler.TypeCache.MS_VB_CS_OptionCompareAttribute) Then
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
            cExp = New ConstantExpression(Parent, Parameter.Constant, Parameter.ParameterType)
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

    Shared Function IsFirstMoreApplicable(ByVal Context As BaseObject, ByVal Arguments As Generic.List(Of Argument), ByVal MTypes As Mono.Cecil.TypeReference(), ByVal NTypes() As Mono.Cecil.TypeReference) As Boolean
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
            'isWidening = Compiler.TypeResolution.IsImplicitlyConvertible(Context, MTypes(i), NTypes(i))
            isWidening = Helper.IsConvertible(Arguments(i), Arguments(i).Expression, MTypes(i), NTypes(i), False, Nothing, False, True, False)

            '*	Aj is the literal 0, Mj is a numeric type and Nj is an enumerated type, or
            isLiteral0 = IsLiteral0Expression(Compiler, Arguments(i).Expression) AndAlso Compiler.TypeResolution.IsNumericType(MTypes(i)) AndAlso Helper.IsEnum(Compiler, NTypes(i))

            isMByte = Helper.CompareType(MTypes(i), Compiler.TypeCache.System_Byte)
            isMShort = isMByte = False AndAlso Helper.CompareType(MTypes(i), Compiler.TypeCache.System_Int16)
            isMInteger = isMByte = False AndAlso isMShort = False AndAlso Helper.CompareType(MTypes(i), Compiler.TypeCache.System_Int32)
            isMLong = isMByte = False AndAlso isMShort = False AndAlso isMInteger = False AndAlso Helper.CompareType(MTypes(i), Compiler.TypeCache.System_Int64)

            isNByte = Helper.CompareType(NTypes(i), Compiler.TypeCache.System_SByte)
            isNShort = isNByte = False AndAlso Helper.CompareType(NTypes(i), Compiler.TypeCache.System_UInt16)
            isNInteger = isNByte = False AndAlso isNShort = False AndAlso Helper.CompareType(NTypes(i), Compiler.TypeCache.System_UInt32)
            isNLong = isNByte = False AndAlso isNShort = False AndAlso isNInteger = False AndAlso Helper.CompareType(NTypes(i), Compiler.TypeCache.System_UInt64)

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
        Dim constant As Object = Nothing

        If exp Is Nothing Then Return False
        Dim litExp As LiteralExpression = TryCast(exp, LiteralExpression)
        If litExp Is Nothing Then Return False
        If litExp.GetConstant(constant, False) = False Then Return False
        If Compiler.TypeResolution.IsIntegralType(CecilHelper.GetType(Compiler, constant)) = False Then Return False
        If CDbl(constant) = 0.0 Then Return True
        Return False
    End Function

    Shared Function IsFirstLessGeneric(ByVal Context As BaseObject) As Boolean
        'A member M is determined to be less generic than a member N using the following steps:
        '-	If M has fewer method type parameters than N, then M is less generic than N.
        '-	Otherwise, if for each pair of matching parameters Mj and Nj, Mj and Nj are equally generic with respect to type parameters on the method, or Mj is less generic with respect to type parameters on the method, and at least one Mj is less generic than Nj, then M is less generic than N.
        '-	Otherwise, if for each pair of matching parameters Mj and Nj, Mj and Nj are equally generic with respect to type parameters on the type, or Mj is less generic with respect to type parameters on the type, and at least one Mj is less generic than Nj, then M is less generic than N.
        Context.Compiler.Report.ShowMessage(Messages.VBNC99997, Context.Location)
        Return False
    End Function

    Shared Function IsAccessible(ByVal Context As BaseObject, ByVal Caller As Mono.Cecil.TypeReference, ByVal Method As Mono.Cecil.MethodReference) As Boolean
        If Caller Is Nothing Then
            Return Helper.IsAccessible(Context, CecilHelper.FindDefinition(Method).Attributes, Method.DeclaringType)
        Else
            Return Helper.IsAccessible(Context, CecilHelper.FindDefinition(Method).Attributes, Method.DeclaringType, Caller)
        End If
    End Function

    Shared Function IsAccessible(ByVal Context As BaseObject, ByVal Caller As Mono.Cecil.TypeReference, ByVal [Property] As Mono.Cecil.PropertyReference) As Boolean
        If Caller Is Nothing Then
            Return Helper.IsAccessible(Context, GetPropertyAccess([Property]), [Property].DeclaringType)
        Else
            Return Helper.IsAccessible(Context, GetPropertyAccess([Property]), [Property].DeclaringType, Caller)
        End If
    End Function

    Shared Function GetMethodAccessibilityString(ByVal Attributes As Mono.Cecil.MethodAttributes) As String
        Attributes = Attributes And Mono.Cecil.MethodAttributes.MemberAccessMask
        Select Case Attributes
            Case Mono.Cecil.MethodAttributes.Public
                Return "Public"
            Case Mono.Cecil.MethodAttributes.Private
                Return "Private"
            Case Mono.Cecil.MethodAttributes.FamANDAssem, Mono.Cecil.MethodAttributes.FamORAssem
                Return "Protected Friend"
            Case Mono.Cecil.MethodAttributes.Family
                Return "Protected"
            Case Mono.Cecil.MethodAttributes.Assembly
                Return "Friend"
            Case Else
                Return "Public"
        End Select
    End Function

    Shared Function GetMethodAttributes(ByVal Member As Mono.Cecil.MemberReference) As Mono.Cecil.MethodAttributes
        Select Case CecilHelper.GetMemberType(Member)
            Case MemberTypes.Method, MemberTypes.Constructor
                Return CecilHelper.FindDefinition(DirectCast(Member, Mono.Cecil.MethodReference)).Attributes
            Case MemberTypes.Property
                Return GetPropertyAttributes(DirectCast(Member, Mono.Cecil.PropertyReference))
            Case Else
                Throw New InternalException("")
        End Select
    End Function

    Shared Function GetVisibility(ByVal Compiler As Compiler, ByVal CallerType As Mono.Cecil.TypeReference, ByVal CalledType As Mono.Cecil.TypeReference) As MemberVisibility
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

    Shared Function GetVisibilityString(ByVal Member As Mono.Cecil.MemberReference) As String
        Return ToString(GetAccessibility(Member))
    End Function

    Shared Function GetPropertyAttributes(ByVal [Property] As Mono.Cecil.PropertyReference) As Mono.Cecil.MethodAttributes
        Dim result As Mono.Cecil.MethodAttributes
        Dim getA, setA As Mono.Cecil.MethodAttributes
        Dim getM, setM As Mono.Cecil.MethodDefinition
        Dim prop As Mono.Cecil.PropertyDefinition = CecilHelper.FindDefinition([Property])

        getM = CecilHelper.FindDefinition(prop.GetMethod)
        setM = CecilHelper.FindDefinition(prop.SetMethod)

        Helper.Assert(getM IsNot Nothing OrElse setM IsNot Nothing)

        If getM IsNot Nothing Then
            getA = getM.Attributes
        End If

        If setM IsNot Nothing Then
            setA = setM.Attributes
        End If

        result = setA Or getA

        Dim visibility As Mono.Cecil.MethodAttributes
        visibility = result And Mono.Cecil.MethodAttributes.MemberAccessMask
        If visibility = Mono.Cecil.MethodAttributes.MemberAccessMask Then
            visibility = Mono.Cecil.MethodAttributes.Public
            result = (result And (Not Mono.Cecil.MethodAttributes.MemberAccessMask)) Or visibility
        End If

        Return result
    End Function

    Shared Function GetEventAttributes(ByVal [Event] As Mono.Cecil.EventReference) As Mono.Cecil.MethodAttributes
        Dim ev As Mono.Cecil.EventDefinition = CecilHelper.FindDefinition([Event])
        Dim result As Mono.Cecil.MethodAttributes
        Dim getA, setA, raiseA As Mono.Cecil.MethodAttributes
        Dim getM, setM, raiseM As Mono.Cecil.MethodDefinition

        getM = ev.AddMethod
        setM = ev.RemoveMethod
        raiseM = ev.InvokeMethod

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

    Shared Function GetPropertyAccess(ByVal [Property] As Mono.Cecil.PropertyReference) As Mono.Cecil.MethodAttributes
        Dim result As Mono.Cecil.MethodAttributes

        result = GetPropertyAttributes([Property])
        result = result And Mono.Cecil.MethodAttributes.MemberAccessMask

        Return result
    End Function

    Shared Function GetEventAccess(ByVal [Event] As Mono.Cecil.EventReference) As Mono.Cecil.MethodAttributes
        Dim result As Mono.Cecil.MethodAttributes

        result = GetEventAttributes([Event])
        result = result And Mono.Cecil.MethodAttributes.MemberAccessMask

        Return result
    End Function

    Shared Function IsAccessible(ByVal Context As BaseObject, ByVal Caller As Mono.Cecil.TypeReference, ByVal Member As Mono.Cecil.MemberReference) As Boolean
        Select Case CecilHelper.GetMemberType(Member)
            Case MemberTypes.Constructor, MemberTypes.Method
                Return IsAccessible(Context, Caller, DirectCast(Member, Mono.Cecil.MethodReference))
            Case MemberTypes.Property
                Return IsAccessible(Context, Caller, DirectCast(Member, Mono.Cecil.PropertyReference))
            Case Else
                Throw New InternalException("")
        End Select
    End Function

    Overloads Shared Function GetParameters(ByVal Context As BaseObject, ByVal Member As Mono.Cecil.MemberReference) As Mono.Collections.Generic.Collection(Of ParameterDefinition)
        Dim mR As Mono.Cecil.MethodReference = TryCast(Member, Mono.Cecil.MethodReference)
        If mR IsNot Nothing Then Return mR.ResolvedParameters

        Dim pR As Mono.Cecil.PropertyReference = TryCast(Member, Mono.Cecil.PropertyReference)
        If pR IsNot Nothing Then Return CecilHelper.FindDefinition(pR).Parameters

        Dim tR As Mono.Cecil.TypeReference = TryCast(Member, Mono.Cecil.TypeReference)
        If tR IsNot Nothing Then Return Nothing

        Dim fR As Mono.Cecil.FieldReference = TryCast(Member, Mono.Cecil.FieldReference)
        If fR IsNot Nothing Then Return Nothing

        Dim eR As Mono.Cecil.EventReference = TryCast(Member, Mono.Cecil.EventReference)
        If eR IsNot Nothing Then Return CecilHelper.FindDefinition(eR).InvokeMethod.Parameters()

        Throw New NotImplementedException
    End Function

    ''' <summary>
    ''' Gets the parameters in the definition (not inflated)
    ''' </summary>
    ''' <param name="Member"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Overloads Shared Function GetOriginalParameters(ByVal Member As Mono.Cecil.MemberReference) As Mono.Collections.Generic.Collection(Of ParameterDefinition)
        Dim mR As Mono.Cecil.MethodReference = TryCast(Member, Mono.Cecil.MethodReference)
        If mR IsNot Nothing Then Return CecilHelper.FindDefinition(mR).Parameters

        Dim pR As Mono.Cecil.PropertyReference = TryCast(Member, Mono.Cecil.PropertyReference)
        If pR IsNot Nothing Then Return CecilHelper.FindDefinition(pR).Parameters

        Dim tR As Mono.Cecil.TypeReference = TryCast(Member, Mono.Cecil.TypeReference)
        If tR IsNot Nothing Then Return Nothing

        Dim fR As Mono.Cecil.FieldReference = TryCast(Member, Mono.Cecil.FieldReference)
        If fR IsNot Nothing Then Return Nothing

        Dim eR As Mono.Cecil.EventReference = TryCast(Member, Mono.Cecil.EventReference)
        If eR IsNot Nothing Then Return CecilHelper.FindDefinition(eR).InvokeMethod.Parameters()

        Throw New NotImplementedException
    End Function

    Overloads Shared Function GetParameters(ByVal Context As BaseObject, ByVal Member As Mono.Cecil.MethodReference) As Mono.Collections.Generic.Collection(Of ParameterDefinition)
        Return Member.ResolvedParameters
    End Function

    ''' <summary>
    ''' Adds all the members to the derived class members, unless they are shadowed or overridden
    ''' </summary>
    ''' <param name="DerivedClassMembers"></param>
    ''' <param name="BaseClassMembers"></param>
    ''' <remarks></remarks>
    Shared Sub AddMembers(ByVal Compiler As Compiler, ByVal Type As Type, ByVal DerivedClassMembers As Generic.List(Of Mono.Cecil.MemberReference), ByVal BaseClassMembers As Mono.Cecil.MemberReference())
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

        For Each member As Mono.Cecil.MemberReference In DerivedClassMembers
            Select Case CecilHelper.GetMemberType(member)
                Case MemberTypes.Constructor
                    'Constructors are not added.
                Case MemberTypes.Event
                    'Events can only be shadows
                    shadowed.Add(member.Name)
                Case MemberTypes.Field
                    shadowed.Add(member.Name)
                Case MemberTypes.Method
                    Dim mInfo As Mono.Cecil.MethodDefinition = CecilHelper.FindDefinition(DirectCast(member, Mono.Cecil.MethodReference))
                    If mInfo.IsHideBySig Then
                        overridden.AddRange(GetOverloadableSignatures(Compiler, mInfo))
                    Else
                        shadowed.Add(mInfo.Name)
                    End If
                Case MemberTypes.NestedType
                    shadowed.Add(member.Name)
                Case MemberTypes.Property
                    Dim pInfo As Mono.Cecil.PropertyReference = DirectCast(member, Mono.Cecil.PropertyReference)
                    If CBool(Helper.GetPropertyAttributes(pInfo) And Mono.Cecil.MethodAttributes.HideBySig) Then
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

        For Each member As Mono.Cecil.MemberReference In BaseClassMembers
            Dim name As String = member.Name.ToLowerInvariant

            If shadowed.Contains(name) Then
                LogAddMessage(Compiler, "Discarded (shadowed): " & name, logging)
                Continue For
            End If


            Select Case CecilHelper.GetMemberType(member)
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
                        LogAddMessage(Compiler, "Discarded (overridden, " & CecilHelper.GetMemberType(member).ToString() & "): " & name, logging)
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
            LogAddMessage(Compiler, "Added (" & CecilHelper.GetMemberType(member).ToString & "): " & name, logging)
            DerivedClassMembers.Add(member)
        Next

        LogAddMessage(Compiler, "", logging)
    End Sub

    Shared Function IsHideBySig(ByVal Member As Mono.Cecil.MemberReference) As Boolean
        Select Case CecilHelper.GetMemberType(Member)
            Case MemberTypes.Constructor
                Return False
            Case MemberTypes.Event, MemberTypes.Field, MemberTypes.NestedType, MemberTypes.TypeInfo
                Return False
            Case MemberTypes.Property
                Dim pInfo As Mono.Cecil.PropertyDefinition = CecilHelper.FindDefinition(DirectCast(Member, Mono.Cecil.PropertyReference))
                Return CBool(GetPropertyAttributes(pInfo) And Mono.Cecil.MethodAttributes.HideBySig)
            Case MemberTypes.Method
                Dim mInfo As Mono.Cecil.MethodDefinition = CecilHelper.FindDefinition(DirectCast(Member, Mono.Cecil.MethodReference))
                Return mInfo.IsHideBySig
            Case Else
                Throw New InternalException("")
        End Select
    End Function

    Shared Function GetOverloadableSignatures(ByVal Compiler As Compiler, ByVal Member As Mono.Cecil.MemberReference) As String()
        Dim result As New Generic.List(Of String)
        Dim params As Mono.Collections.Generic.Collection(Of ParameterDefinition)
        Dim types() As Mono.Cecil.TypeReference
        Dim sep As String = ""

        params = Helper.GetParameters(Compiler, Member)
        types = Helper.GetTypes(params)

        Dim signature As String = ""
        For i As Integer = 0 To types.Length - 1
            If CecilHelper.IsByRef(types(i)) Then types(i) = CecilHelper.GetElementType(types(i))
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
                Dim tp As Mono.Cecil.TypeReference = ActualClassification.AsTypeClassification.Type
                Return Compiler.Report.ShowMessage(Messages.VBNC30691, Location, tp.Name, tp.Namespace)
            Case ExpressionClassification.Classifications.Value
                Dim vC As ValueClassification = ActualClassification.AsValueClassification
                Dim constant As Object = Nothing
                If vC.GetConstant(constant, False) Then
                    Return Compiler.Report.ShowMessage(Messages.VBNC30074, Location)
                ElseIf vC.ReclassifiedClassification IsNot Nothing AndAlso vC.ReclassifiedClassification.IsVariableClassification Then
                    Dim vVar As VariableClassification = vC.ReclassifiedClassification.AsVariableClassification
                    If vVar.FieldDefinition IsNot Nothing AndAlso (vVar.FieldDefinition.Attributes And Mono.Cecil.FieldAttributes.InitOnly) = Mono.Cecil.FieldAttributes.InitOnly Then
                        Return Compiler.Report.ShowMessage(Messages.VBNC30064, Location)
                    ElseIf vVar.GetConstant(constant, False) Then
                        Return Compiler.Report.ShowMessage(Messages.VBNC30074, Location)
                    Else
                        Helper.AddError(Compiler, Location, "Expected " & Expected & " got " & ActualClassification.Classification.ToString())
                    End If
                Else
                    Helper.AddError(Compiler, Location, "Expected " & Expected & " got " & ActualClassification.Classification.ToString())
                End If
            Case ExpressionClassification.Classifications.MethodGroup
                Return Compiler.Report.ShowMessage(Messages.VBNC30068, Location)
            Case Else
                Helper.AddError(Compiler, Location, "Expected " & Expected & " got " & ActualClassification.Classification.ToString())
        End Select
        Return False
    End Function

    Shared Function GetCoClassType(ByVal Compiler As Compiler, ByVal Type As TypeReference) As TypeReference
        Dim td As TypeDefinition = CecilHelper.FindDefinition(Type)
        Dim result As TypeReference = Nothing

        For i As Integer = 0 To td.CustomAttributes.Count - 1
            Dim attrib As CustomAttribute = td.CustomAttributes(i)
            If Helper.CompareType(attrib.AttributeType, Compiler.TypeCache.System_Runtime_InteropServices_CoClassAttribute) = False Then Continue For
            If attrib.Constructor Is Nothing Then Continue For
            If attrib.ConstructorArguments.Count <> 1 Then Continue For
            result = TryCast(attrib.ConstructorArguments(0).Value, TypeReference)
            If result IsNot Nothing Then Exit For
        Next

        Return result
    End Function

    Shared Function GetDominantType(ByVal Compiler As Compiler, ByVal types As Generic.List(Of TypeReference)) As TypeReference
        Dim implicit() As Boolean
        Dim count As Integer
        Dim index As Integer

        'Given a set of types, it is often necessary in situations such as type inference to determine the dominant type of the set. 

        If types Is Nothing OrElse types.Count = 0 Then Return Nothing
        If types.Count = 1 Then Return types(0)

        ReDim implicit(types.Count - 1)

        For i As Integer = 0 To implicit.Length - 1
            implicit(i) = True
        Next

        'The dominant type of a set of types is determined by first removing any types that one or more other types do not have an implicit conversion to. 
        For i As Integer = 0 To types.Count - 1
            For j As Integer = 0 To types.Count - 1
                If i = j Then Continue For
                If Compiler.TypeResolution.IsImplicitlyConvertible(Compiler, types(i), types(j)) = False Then
                    implicit(j) = False
                    Exit For
                End If
            Next
        Next

        count = 0
        For i As Integer = 0 To implicit.Length - 1
            If implicit(i) Then
                index = i
                count += 1
            End If
        Next

        'If there are no types left at this point, there is no dominant type. 
        If count = 0 Then Return Nothing

        'The dominant type is then the most encompassed of the remaining types. 
        If count = 1 Then Return types(index)

        'The dominant type is then the most encompassed of the remaining types. 
        'If there is more than one type that is most encompassed, then there is no dominant type. 
        Return GetMostEncompassedType(Compiler, types)
    End Function

    Public Shared Function VerifyConstraints(ByVal Context As ParsedObject, ByVal parameters As Mono.Collections.Generic.Collection(Of GenericParameter), ByVal arguments As Mono.Collections.Generic.Collection(Of TypeReference), ByVal ShowErrors As Boolean) As Boolean
        Dim result As Boolean = True

        For i As Integer = 0 To Math.Min(parameters.Count, arguments.Count) - 1
            Dim param As GenericParameter = parameters(i)
            Dim arg As TypeReference = arguments(i)
            Dim gt As GenericParameter = TryCast(arg, GenericParameter)

            If param.HasDefaultConstructorConstraint Then
                If gt IsNot Nothing Then
                    If gt.HasDefaultConstructorConstraint = False AndAlso gt.HasNotNullableValueTypeConstraint = False Then
                        Dim tr As TypeReference = TryCast(param.Owner, TypeReference)
                        If ShowErrors Then
                            If Helper.CompareType(tr, Context.Compiler.TypeCache.System_Nullable1) Then
                                result = Context.Compiler.Report.ShowMessage(Messages.VBNC33101, Context.Location, Helper.ToString(Context, arg))
                            Else
                                result = Context.Compiler.Report.ShowMessage(Messages.VBNC32084, Context.Location, Helper.ToString(Context, arg), param.Name)
                            End If
                        End If
                        result = False
                        Continue For
                    End If
                Else
                    Dim ctor As MethodReference = Helper.GetDefaultConstructor(arg)
                    If (ctor Is Nothing OrElse Helper.IsPublic(ctor) = False) AndAlso CecilHelper.IsValueType(arg) = False Then
                        If ShowErrors Then Context.Compiler.Report.ShowMessage(Messages.VBNC32083, Context.Location, Helper.ToString(Context, arg), param.Name)
                        result = False
                        Continue For
                    End If
                End If
            End If

            If param.HasNotNullableValueTypeConstraint Then
                If gt Is Nothing Then
                    If CecilHelper.IsValueType(arg) = False Then
                        If ShowErrors Then Context.Compiler.Report.ShowMessage(Messages.VBNC32105, Context.Location, Helper.ToString(Context, arg), param.Name)
                        result = False
                    End If
                Else
                    If gt.HasNotNullableValueTypeConstraint = False Then
                        If ShowErrors Then Context.Compiler.Report.ShowMessage(Messages.VBNC32105, Context.Location, Helper.ToString(Context, arg), param.Name)
                        result = False
                    End If
                End If
            End If

            If param.HasReferenceTypeConstraint Then
                If gt IsNot Nothing Then
                    If gt.HasReferenceTypeConstraint = False Then
                        If ShowErrors Then Context.Compiler.Report.ShowMessage(Messages.VBNC32106, Context.Location, Helper.ToString(Context, arg), param.Name)
                        result = False
                    End If
                Else
                    If CecilHelper.IsClass(arg) = False Then
                        If ShowErrors Then Context.Compiler.Report.ShowMessage(Messages.VBNC32106, Context.Location, Helper.ToString(Context, arg), param.Name)
                        result = False
                    End If
                End If
            End If

            If param.HasConstraints Then
                For c As Integer = 0 To param.Constraints.Count - 1
                    Dim constr As TypeReference = param.Constraints(i)

                    If param.HasNotNullableValueTypeConstraint AndAlso Helper.CompareType(constr, Context.Compiler.TypeCache.System_ValueType) Then Continue For
                    If Helper.CompareType(constr, arg) Then Continue For

                    If gt Is Nothing Then
                        If Helper.IsInterface(Context, constr) Then
                            If Helper.DoesTypeImplementInterface(Context, arg, constr) = False Then
                                If ShowErrors Then Context.Compiler.Report.ShowMessage(Messages.VBNC32044, Context.Location, Helper.ToString(Context, arg), Helper.ToString(Context, constr))
                                result = False
                            End If
                        Else
                            If Helper.IsSubclassOf(constr, arg) = False Then
                                If ShowErrors Then Context.Compiler.Report.ShowMessage(Messages.VBNC32044, Context.Location, Helper.ToString(Context, arg), Helper.ToString(Context, constr))
                                result = False
                            End If
                        End If
                    Else
                        Dim found As Boolean = False

                        For c2 As Integer = 0 To gt.Constraints.Count - 1
                            If Helper.CompareType(constr, gt.Constraints(c2)) Then
                                found = True
                                Exit For
                            End If
                            If Helper.DoesTypeImplementInterface(Context, gt.Constraints(c2), constr) Then
                                found = True
                                Exit For
                            End If
                        Next
                        If found = False Then
                            If ShowErrors Then Context.Compiler.Report.ShowMessage(Messages.VBNC32044, Context.Location, Helper.ToString(Context, arg), Helper.ToString(Context, constr))
                            result = False
                        End If
                    End If
                Next
            End If
        Next
        Return result
    End Function
End Class

