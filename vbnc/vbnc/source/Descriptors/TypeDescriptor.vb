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
#Const DEBUGTYPEACCESS = 0
#End If
''' <summary>
''' Represents a type, either in the parse tree or of the Type type.
''' </summary>
''' <remarks></remarks>
Public Class TypeDescriptor
    Inherits Type
    Implements IMemberDescriptor

    Private m_Declaration As TypeDeclaration
    Private m_Parent As ParsedObject

    Private m_AllMembers As Generic.List(Of MemberInfo)
    Private m_AllDeclaredMembers As Generic.List(Of MemberInfo)

    Private m_ObjectID As Integer = BaseObject.NewID
#If DEBUG Then
    Private m_AllDescriptor As New Generic.List(Of TypeDescriptor)
    Private m_AllTypeIDs As New Generic.List(Of Integer)
    Private m_Hashed As New Generic.Dictionary(Of Integer, TypeDescriptor)
#End If

    Public Sub ClearCache()
        m_AllMembers = Nothing
        m_AllDeclaredMembers = Nothing
    End Sub

    ReadOnly Property IsShared() As Boolean Implements IMemberDescriptor.IsShared
        Get
            Return m_Declaration.IsShared
        End Get
    End Property

    ReadOnly Property Parent() As ParsedObject
        Get
            Return m_Parent
        End Get
    End Property

    ReadOnly Property Compiler() As Compiler
        Get
            Return m_Parent.Compiler
        End Get
    End Property

    Public Overrides Function ToString() As String
        Return Me.FullName
    End Function

    ReadOnly Property Declaration() As TypeDeclaration
        Get
            Return m_Declaration
        End Get
    End Property

    Private ReadOnly Property MemberDeclaration() As IMember Implements IMemberDescriptor.Declaration
        Get
            Return m_Declaration
        End Get
    End Property

    Shared Function CreateList(ByVal types As System.Collections.IEnumerable) As TypeList
        Dim result As New TypeList
        For Each t As IType In types
            result.Add(t.TypeDescriptor)
        Next
        Return result
    End Function

    Sub New(ByVal Type As TypeDeclaration)
        If Type Is Nothing Then Throw New ArgumentNullException("IType")
        m_Declaration = Type
        m_Parent = m_Declaration
#If DEBUG Then
        VerifyUnique(Type)
#End If
    End Sub

    Protected Sub New(ByVal Parent As ParsedObject)
        Helper.Assert(Parent IsNot Nothing)
        m_Parent = Parent
        m_Declaration = TryCast(Parent, TypeDeclaration)
    End Sub

#If DEBUG Then
    Sub VerifyUnique(ByVal obj As IBaseObject)
        Dim id As Integer = obj.ObjectID
        Helper.Assert(m_AllDescriptor.Contains(Me) = False)
        Helper.Assert(m_AllTypeIDs.Contains(id) = False)
        m_AllDescriptor.Add(Me)
        m_AllTypeIDs.Add(id)
        m_Hashed.Add(id, Me)
        'Diagnostics.Debug.WriteLine("Created td: " & id.ToString)
    End Sub
#End If

    ''' <summary>
    ''' Gets the Reflection.Emit created type for this descriptor.
    ''' It is a TypeBuilder.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Overridable ReadOnly Property TypeInReflection() As Type
        Get
            Dim result As Type = Nothing

            Helper.Assert(m_Declaration IsNot Nothing)

            result = m_Declaration.TypeBuilder
            If result Is Nothing Then
                result = m_Declaration.EnumBuilder
            End If

            Return result
        End Get
    End Property

#Region "Inherited members from Type"
    <Diagnostics.Conditional("DEBUGTYPEACCESS")> _
        Protected Sub DumpMethodInfo(Optional ByVal ReturnValue As Object = Nothing)
#If DEBUGTYPEACCESS Then
        Static recursive As Boolean
        If recursive Then Return
        recursive = True

        Dim m As New Diagnostics.StackFrame(1)
        Dim str As String

        Dim name As String = Me.FullName
        If name Is Nothing Then name = "(TypeParameter:  " & Me.Name & ")"
        str = " Called: (" & name & "): Type."

        If ReturnValue IsNot Nothing Then
            Compiler.Report.WriteLine(Report.ReportLevels.Debug, str & m.GetMethod.Name & " with return value: " & ReturnValue.ToString)
        Else
            Compiler.Report.WriteLine(Report.ReportLevels.Debug, str & m.GetMethod.Name)
        End If
        recursive = False
#End If
    End Sub

    Public Overrides ReadOnly Property GenericParameterAttributes() As System.Reflection.GenericParameterAttributes
        Get
            Dim result As GenericParameterAttributes
            Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property GenericParameterPosition() As Integer
        Get
            Dim result As Integer
            Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    ''' <summary>
    ''' A hack to prevent the debugger to crash when inspecthing type descriptors.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shadows ReadOnly Property IsVisible() As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides ReadOnly Property IsGenericParameter() As Boolean
        Get
            Dim result As Boolean
            result = False
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property IsGenericType() As Boolean
        Get
            Dim result As Boolean

            Helper.Assert(m_Declaration IsNot Nothing)

            Dim cdecl As IConstructable = TryCast(m_Declaration, IConstructable)
            If cdecl IsNot Nothing Then
                If cdecl.TypeParameters IsNot Nothing Then
                    If cdecl.TypeParameters.Parameters IsNot Nothing Then
                        result = cdecl.TypeParameters.Parameters.Length > 0
                    Else
                        result = False
                    End If
                ElseIf Me.IsNested AndAlso Me.DeclaringType.IsGenericType Then
                    result = True
                Else
                    result = False
                End If
            Else
                result = False
            End If

            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property IsGenericTypeDefinition() As Boolean
        Get
            Dim result As Boolean
            Helper.Assert(m_Declaration IsNot Nothing)

            Dim cdecl As IConstructable = TryCast(m_Declaration, IConstructable)
            If cdecl IsNot Nothing Then
                result = cdecl.TypeParameters IsNot Nothing AndAlso cdecl.TypeParameters.Parameters.Length > 0
            Else
                result = False
            End If
            Helper.Assert(m_Declaration.TypeBuilder Is Nothing OrElse m_Declaration.TypeBuilder.IsGenericTypeDefinition = result)
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property DeclaringType() As System.Type
        Get
            Dim result As Type
            If m_Declaration.IsNestedType Then
                result = m_Declaration.FindFirstParent(Of IType).TypeDescriptor
            Else
                result = Nothing
            End If

            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property FullName() As String
        Get
            Dim result As String
            Helper.Assert(m_Declaration IsNot Nothing)
            result = m_Declaration.FullName

            DumpMethodInfo(result)
            Helper.Assert(result.IndexOf("\"c) = -1)
            Return result
        End Get
    End Property

    Overrides ReadOnly Property Name() As String
        Get
            Dim result As String = ""
            Helper.Assert(m_Declaration IsNot Nothing)
            result = m_Declaration.Name
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property Assembly() As System.Reflection.Assembly
        Get
            Dim result As System.Reflection.Assembly
            result = m_Parent.Compiler.AssemblyBuilder
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property AssemblyQualifiedName() As String
        Get
            DumpMethodInfo()
            Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
            Return Nothing
        End Get
    End Property

    Public Overrides ReadOnly Property BaseType() As System.Type
        Get
            Dim result As Type = Nothing
            Helper.Assert(m_Declaration IsNot Nothing)
            result = m_Declaration.BaseType
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Protected Overrides Function GetAttributeFlagsImpl() As System.Reflection.TypeAttributes
        Dim result As TypeAttributes

        Helper.Assert(m_Declaration IsNot Nothing)

        result = TypeResolution.getTypeAttributeScopeFromScope(m_Declaration.Modifiers, m_Declaration.IsNestedType)
        If TypeOf m_Declaration Is ClassDeclaration Then
            result = result Or TypeAttributes.Class
        ElseIf TypeOf m_Declaration Is InterfaceDeclaration Then
            result = result Or TypeAttributes.Interface Or TypeAttributes.Abstract
        End If
        'If Modifiers.IsNothing(m_Declaration.Modifiers) = False Then
        If m_Declaration.Modifiers.Is(ModifierMasks.NotInheritable) Then
            result = result Or TypeAttributes.Sealed
        End If
        If m_Declaration.Modifiers.Is(ModifierMasks.MustInherit) Then
            result = result Or TypeAttributes.Abstract
        End If
        'End If

        DumpMethodInfo(result)

        Return result
    End Function

    Protected Overrides Function GetConstructorImpl(ByVal bindingAttr As System.Reflection.BindingFlags, ByVal binder As System.Reflection.Binder, ByVal callConvention As System.Reflection.CallingConventions, ByVal types() As System.Type, ByVal modifiers() As System.Reflection.ParameterModifier) As System.Reflection.ConstructorInfo
        Dim result As ConstructorInfo = Nothing
        Dim tmp As MemberInfo()

        tmp = GetMembers(bindingAttr)

        For i As Integer = 0 To tmp.Length - 1
            Dim member As MemberInfo = tmp(i)
            If member.MemberType = MemberTypes.Constructor Then
                If Helper.CompareTypes(Helper.GetTypes(Helper.GetParameters(Compiler, member)), types) Then
                    Helper.Assert(result Is Nothing)
                    result = DirectCast(member, ConstructorInfo)
                End If
            End If
        Next

        DumpMethodInfo(result)

        Return result
    End Function

    Public Overloads Overrides Function GetConstructors(ByVal bindingAttr As System.Reflection.BindingFlags) As System.Reflection.ConstructorInfo()
        Dim result As Generic.List(Of ConstructorInfo)

        result = GetMembers(Of ConstructorInfo)(MemberTypes.Constructor, bindingAttr)

        DumpMethodInfo(result.ToArray)

        Return result.ToArray
    End Function

    Public Overloads Overrides Function GetCustomAttributes(ByVal inherit As Boolean) As Object()
        Dim result As Object() = Nothing
        DumpMethodInfo(result)
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return result
    End Function

    Public Overloads Overrides Function GetCustomAttributes(ByVal attributeType As System.Type, ByVal inherit As Boolean) As Object()
        Dim result As Object()

        result = Helper.FilterCustomAttributes(attributeType, inherit, m_Declaration)

        DumpMethodInfo(result)

        Return result
    End Function

    Public Overrides Function GetElementType() As System.Type
        Dim result As Type

        If Me.IsEnum Then
            result = DirectCast(m_Declaration, EnumDeclaration).EnumConstantType
        Else
            result = Nothing
        End If

        DumpMethodInfo(result)
        Return result
    End Function

    Public Overloads Overrides Function GetEvent(ByVal name As String, ByVal bindingAttr As System.Reflection.BindingFlags) As System.Reflection.EventInfo
        DumpMethodInfo()
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return Nothing
    End Function

    Public Overloads Overrides Function GetEvents(ByVal bindingAttr As System.Reflection.BindingFlags) As System.Reflection.EventInfo()
        DumpMethodInfo()
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return Nothing
    End Function

    Public Overloads Overrides Function GetField(ByVal name As String, ByVal bindingAttr As System.Reflection.BindingFlags) As System.Reflection.FieldInfo
        Dim result As FieldInfo

        Dim members As Generic.List(Of INameable)
        members = m_Declaration.Members.Index.Item(name)

        Helper.Assert(members IsNot Nothing)
        Helper.Assert(members.Count = 1)
        Helper.Assert(TypeOf members(0) Is IFieldMember)
        result = DirectCast(members(0), IFieldMember).FieldDescriptor

        DumpMethodInfo(result)
        Return result
    End Function

    Public Overloads Overrides Function GetFields(ByVal bindingAttr As System.Reflection.BindingFlags) As System.Reflection.FieldInfo()
        DumpMethodInfo()
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return Nothing
    End Function

    Public Overloads Overrides Function GetInterface(ByVal name As String, ByVal ignoreCase As Boolean) As System.Type
        DumpMethodInfo()
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return Nothing
    End Function

    Public Overrides Function GetInterfaces() As System.Type()
        Dim result As New Generic.List(Of Type)
        Dim tmp As Type()

        Helper.Assert(m_Declaration IsNot Nothing)

        tmp = m_Declaration.ImplementedTypes
        If tmp IsNot Nothing Then result.AddRange(m_Declaration.ImplementedTypes)
        If Me.BaseType IsNot Nothing Then
            result.AddRange(Compiler.TypeManager.GetRegisteredType(Me.BaseType).GetInterfaces)
        End If

        For i As Integer = 0 To result.Count - 1
            tmp = Compiler.TypeManager.GetRegisteredType(result(i)).GetInterfaces
            For Each iface As Type In tmp
                If result.Contains(iface) = False Then result.Add(iface)
            Next
        Next

        DumpMethodInfo(result.ToArray)

        Return result.ToArray
    End Function

    Function Filter(ByVal members As Generic.IEnumerable(Of IConstructorMember), ByVal bindingAttr As BindingFlags) As Generic.List(Of ConstructorInfo)
        DumpMethodInfo()
        Dim result As New Generic.List(Of ConstructorInfo)

        For Each member As IConstructorMember In members
            If IsMatch(member, bindingAttr) Then result.Add(member.ConstructorDescriptor)
        Next

        Return result
    End Function

    Function Filter(ByVal members As Generic.IEnumerable(Of MemberInfo), ByVal bindingAttr As BindingFlags) As Generic.List(Of MemberInfo)
        Dim result As New Generic.List(Of MemberInfo)

        Helper.Assert(members IsNot Nothing)

        For Each member As MemberInfo In members
            If IsMatch(member, bindingAttr) Then result.Add(member)
        Next

        DumpMethodInfo(result)

        Return result
    End Function

    Private Function Filter(ByVal members As Generic.IEnumerable(Of IMember), ByVal bindingAttr As BindingFlags) As Generic.List(Of MemberInfo)
        DumpMethodInfo()
        Dim result As New Generic.List(Of MemberInfo)

        For Each member As IMember In members
            If IsMatch(member, bindingAttr) Then result.Add(member.MemberDescriptor)
        Next

        Return result
    End Function

    Private Function IsMatch(ByVal member As IMember, ByVal bindingAttr As BindingFlags) As Boolean
        DumpMethodInfo()
        Dim result As Boolean = False

        If CBool(bindingAttr And BindingFlags.Public) Then
            If member.Modifiers.IsPublic Then result = True
        End If
        If CBool(bindingAttr And BindingFlags.NonPublic) Then
            If member.Modifiers.IsPublic = False Then result = True
        End If

        If member.IsShared Then
            If CBool(bindingAttr And BindingFlags.Static) = False Then result = False
        Else
            If CBool(bindingAttr And BindingFlags.Instance) = False Then result = False
        End If

#If DEBUG Then
        Dim tmp As BindingFlags = (Not (BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Instance Or BindingFlags.Static)) And bindingAttr
        If CInt(tmp) > 0 Then
            Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
            'Helper.NotImplemented("bindingAttr has some value not checked for: " & tmp.ToString)
        End If
#End If

        Return result
    End Function

    Private Function IsMatch(ByVal member As MemberInfo, ByVal bindingAttr As BindingFlags) As Boolean
        Dim result As Boolean = True

        Dim includePublic As Boolean = CBool(bindingAttr And BindingFlags.Public)
        Dim includeNonPublic As Boolean = CBool(bindingAttr And BindingFlags.NonPublic)
        Dim includeStatic As Boolean = CBool(bindingAttr And BindingFlags.Static)
        Dim includeInstance As Boolean = CBool(bindingAttr And BindingFlags.Instance)
        Dim includeFlattenedHierarchy As Boolean = CBool(bindingAttr And BindingFlags.FlattenHierarchy)
        Dim onlyDeclared As Boolean = CBool(bindingAttr And BindingFlags.DeclaredOnly)

        If onlyDeclared AndAlso (Helper.CompareType(member.DeclaringType, Me) = False AndAlso (Me.IsGenericType AndAlso Me.IsGenericTypeDefinition = False AndAlso member.DeclaringType.IsGenericTypeDefinition AndAlso Helper.CompareType(member.DeclaringType, Me.GetGenericTypeDefinition)) = False) Then
            Return False
        End If

        If Helper.IsPublic(member) Then
            If includePublic = False Then result = False
        Else
            If includeNonPublic = False Then
                result = False
            End If
        End If

        If Helper.IsShared(member) Then
            If includeStatic = False Then result = False
        Else
            If includeInstance = False Then result = False
        End If



        If includeFlattenedHierarchy Then
#If EXTENDEDDEBUG Then
            Helper.NotImplementedYet("bindingAttr has some value not checked for: " & BindingFlags.FlattenHierarchy.ToString)
#End If
        End If

#If DEBUG Then
        Dim tmp As BindingFlags = (Not (BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Instance Or BindingFlags.Static Or BindingFlags.FlattenHierarchy Or BindingFlags.DeclaredOnly)) And bindingAttr

        If CInt(tmp) > 0 Then
            'Helper.NotImplementedYet("bindingAttr has some value not checked for: " & tmp.ToString)
        End If
#End If

        Return result
    End Function

    Protected Overridable ReadOnly Property AllDeclaredMembers() As Generic.List(Of MemberInfo)
        Get
            If m_AllDeclaredMembers Is Nothing Then
                Helper.Assert(m_Declaration IsNot Nothing)
                Helper.Assert(m_Declaration.Members IsNot Nothing)

                m_AllDeclaredMembers = m_Declaration.Members.MemberDeclarations
            End If
            Return m_AllDeclaredMembers
        End Get
    End Property

    Protected Overridable ReadOnly Property AllMembers() As Generic.List(Of MemberInfo)
        Get
            If m_AllMembers Is Nothing Then
                Helper.Assert(m_Declaration IsNot Nothing)
                Helper.Assert(m_Declaration.Members IsNot Nothing)

                m_AllMembers = m_Declaration.Members.MemberDeclarations

                Helper.AddMembers(Compiler, Me, m_AllMembers, Helper.GetBaseMembers(Compiler, Me))
            End If

            Return m_AllMembers
        End Get
    End Property

    Private Overloads Function GetMembers(Of T)(ByVal MemberType As MemberTypes, ByVal bindingAttr As System.Reflection.BindingFlags) As Generic.List(Of T)
        Dim result As Generic.List(Of T)
        Dim candidates As Generic.List(Of MemberInfo)

        If (bindingAttr And BindingFlags.DeclaredOnly) = 0 Then
            'Helper.NotImplementedYet("Get* should not be called without DeclaredOnly, if base members are needed call Compiler.TypeManager.GetCache(type).FlattenedMembers")
            candidates = AllMembers
        Else
            candidates = AllDeclaredMembers
        End If

        result = New Generic.List(Of T)(candidates.Count)

        For i As Integer = 0 To candidates.Count - 1
            Dim member As MemberInfo
            member = candidates(i)

            If member.MemberType <> MemberType Then Continue For
            If IsMatch(member, bindingAttr) = False Then Continue For

            result.Add(DirectCast(CObj(member), T))
        Next

        Return result
    End Function

    Public Overloads Overrides Function GetMembers(ByVal bindingAttr As System.Reflection.BindingFlags) As System.Reflection.MemberInfo()
        Dim result As New Generic.List(Of MemberInfo)

        result = New Generic.List(Of MemberInfo)

        If (bindingAttr And BindingFlags.DeclaredOnly) = 0 Then
            'Helper.NotImplementedYet("Get* should not be called without DeclaredOnly, if base members are needed call Compiler.TypeManager.GetCache(type).FlattenedMembers")
            result.AddRange(Filter(AllMembers, bindingAttr).ToArray)
        Else
            result.AddRange(Filter(AllDeclaredMembers, bindingAttr).ToArray)
        End If

        DumpMethodInfo(result.ToArray)
        Return result.ToArray
    End Function

    Protected Overrides Function GetMethodImpl(ByVal name As String, ByVal bindingAttr As System.Reflection.BindingFlags, ByVal binder As System.Reflection.Binder, ByVal callConvention As System.Reflection.CallingConventions, ByVal types() As System.Type, ByVal modifiers() As System.Reflection.ParameterModifier) As System.Reflection.MethodInfo
        Dim result As MethodInfo = Nothing
        Dim allMembers As New Generic.List(Of MemberInfo)

        allMembers.AddRange(Me.GetMembers(bindingAttr))

        allMembers = Helper.FilterByName2(allMembers, name)

        If types IsNot Nothing Then
            For Each m As MemberInfo In allMembers
                Dim mi As MethodInfo = TryCast(m, MethodInfo)
                If mi IsNot Nothing AndAlso TypeResolution.CompareTypes(Helper.GetTypes(Helper.GetParameters(Compiler, mi)), types) Then
                    result = mi
                    Exit For
                End If
            Next
        ElseIf allMembers.Count = 1 Then
            result = TryCast(allMembers(0), MethodInfo)
        Else
            Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        End If

        DumpMethodInfo(result)
        Return result
    End Function

    Public Overloads Overrides Function GetMethods(ByVal bindingAttr As System.Reflection.BindingFlags) As System.Reflection.MethodInfo()
        Dim result As New Generic.List(Of MethodInfo)
        Dim tmp As Generic.List(Of IMethod)

        tmp = m_Declaration.Members.GetSpecificMembers(Of IMethod)()
        For Each item As IMethod In tmp
            Dim inf As MethodInfo = TryCast(item.MethodDescriptor, MethodInfo)
            If inf IsNot Nothing Then
                result.Add(inf)
            End If
        Next

        DumpMethodInfo(result.ToArray)
        Return result.ToArray
    End Function

    Public Overloads Overrides Function GetNestedType(ByVal name As String, ByVal bindingAttr As System.Reflection.BindingFlags) As System.Type
        Dim result As Type = Nothing
        Dim candidates As Type() = GetNestedTypes(bindingAttr)

        For Each candidate As Type In candidates
            If IsMatch(candidate, name, bindingAttr) Then
                Helper.Assert(result Is Nothing) 'Only one result should be found.
                result = candidate
#If Not DEBUG Then
                exit for
#End If
            End If
        Next

        DumpMethodInfo(result)

        Return result
    End Function

    Public Overloads Overrides Function GetNestedTypes(ByVal bindingAttr As System.Reflection.BindingFlags) As System.Type()
        Dim result As New Generic.List(Of Type)

        For Each candidate As TypeDeclaration In m_Declaration.Members.GetSpecificMembers(Of TypeDeclaration)()
            If IsMatch(candidate.TypeDescriptor, Nothing, bindingAttr) Then result.Add(candidate.TypeDescriptor)
        Next

        DumpMethodInfo(result.ToArray)

        Return result.ToArray
    End Function

    Private Function IsMatch(ByVal Type As Type, ByVal SearchedName As String, ByVal bindingAttr As BindingFlags) As Boolean
        'The following BindingFlags filter flags can be used to define which nested types to include in the search: 
        '- You must specify either BindingFlags.Instance or BindingFlags.Static in order to get a return.
        '- Specify BindingFlags.Public to include public nested types in the search.
        '- Specify BindingFlags.NonPublic to include non-public nested types (that is, private and protected members) in the search.
        '- Specify BindingFlags.FlattenHierarchy to include static nested types up the hierarchy.

        'The following BindingFlags modifier flags can be used to change how the search works: 
        '- BindingFlags.IgnoreCase to ignore the case of name.
        '- BindingFlags.DeclaredOnly to search only the nested types declared on the Type, not nested types that were simply inherited.

        'NOTES: 
        '- What is a static nested type?
        '- BindingFlags.FlattenHierarchy's info says nested types are NOT returned.
        '- Could not get the runtimetype to return any inherited nested types, so we are not looking for them eiter.

        If CBool(bindingAttr And (BindingFlags.Instance Or BindingFlags.Static)) = False Then Return False

        If Type.IsNotPublic OrElse Type.IsNestedPublic = False Then
            If CBool(bindingAttr And BindingFlags.NonPublic) = False Then Return False
        ElseIf Type.IsPublic OrElse Type.IsNestedPublic Then
            If CBool(bindingAttr And BindingFlags.Public) = False Then Return False
        End If

        If SearchedName IsNot Nothing Then
            If Helper.CompareName(Type.Name, SearchedName, Not CBool(bindingAttr And BindingFlags.IgnoreCase)) = False Then Return False
        End If

        Return True
    End Function

    Public Overloads Overrides Function GetProperties(ByVal bindingAttr As System.Reflection.BindingFlags) As System.Reflection.PropertyInfo()
        DumpMethodInfo()
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return Nothing
    End Function

    Protected Overrides Function GetPropertyImpl(ByVal name As String, ByVal bindingAttr As System.Reflection.BindingFlags, ByVal binder As System.Reflection.Binder, ByVal returnType As System.Type, ByVal types() As System.Type, ByVal modifiers() As System.Reflection.ParameterModifier) As System.Reflection.PropertyInfo
        Dim result As PropertyInfo = Nothing
        Dim tmp As New Generic.List(Of MemberInfo)

        tmp.AddRange(Me.GetMembers(bindingAttr))

        For Each p As MemberInfo In tmp
            If p.MemberType = MemberTypes.Property AndAlso Helper.CompareName(p.Name, name) Then
                Helper.Assert(result Is Nothing)
                result = DirectCast(p, PropertyInfo)
            End If
        Next

        DumpMethodInfo(result)

        Return result
    End Function

    Public Overrides ReadOnly Property GUID() As System.Guid
        Get
            DumpMethodInfo()
            Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
            Return Nothing
        End Get
    End Property

    Protected Overrides Function HasElementTypeImpl() As Boolean
        Dim result As Boolean

        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        DumpMethodInfo(result)
        Return result
    End Function

    Public Overloads Overrides Function InvokeMember(ByVal name As String, ByVal invokeAttr As System.Reflection.BindingFlags, ByVal binder As System.Reflection.Binder, ByVal target As Object, ByVal args() As Object, ByVal modifiers() As System.Reflection.ParameterModifier, ByVal culture As System.Globalization.CultureInfo, ByVal namedParameters() As String) As Object
        DumpMethodInfo()
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return Nothing
    End Function

    Protected Overrides Function IsArrayImpl() As Boolean
        Dim result As Boolean = False
        DumpMethodInfo(result)
        Return result
    End Function

    Protected Overrides Function IsByRefImpl() As Boolean
        Dim result As Boolean = False
        DumpMethodInfo(result)
        Return result
    End Function

    Protected Overrides Function IsCOMObjectImpl() As Boolean
        DumpMethodInfo()
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
    End Function

    Public Overrides Function IsDefined(ByVal attributeType As System.Type, ByVal inherit As Boolean) As Boolean
        DumpMethodInfo()
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
    End Function

    Protected Overrides Function IsPointerImpl() As Boolean
        Dim result As Boolean = False
        DumpMethodInfo(result)
        Return result
    End Function

    Protected Overrides Function IsPrimitiveImpl() As Boolean
        Dim result As Boolean = False
        DumpMethodInfo(result)
        Return result
    End Function

    Public Overrides ReadOnly Property [Module]() As System.Reflection.Module
        Get
            Dim result As Reflection.Module

            result = m_Parent.Compiler.ModuleBuilder

            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property [Namespace]() As String
        Get
            Dim result As String

            Helper.Assert(m_Declaration IsNot Nothing)

            result = m_Declaration.Namespace
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property UnderlyingSystemType() As System.Type
        Get
            Dim result As Type
            Helper.Assert(m_Declaration IsNot Nothing)
            If m_Declaration.TypeBuilder Is Nothing Then
                result = Me
            Else
                result = m_Declaration.TypeBuilder.UnderlyingSystemType
            End If
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides Function Equals(ByVal o As Object) As Boolean
        Dim result As Boolean
        If o Is Me Then
            result = True
        Else
            result = Helper.CompareType(TryCast(o, Type), Me)
        End If
        DumpMethodInfo(result)
        Return result
    End Function

    Public Overridable Overloads Function Equals(ByVal o As Type) As Boolean
        Dim result As Boolean
        Dim tmp As TypeDescriptor = Me
        Dim MeName As String
        Dim oName As String

        If TypeOf Me Is TypeParameterDescriptor Then
            MeName = Me.Name
        Else
            MeName = Me.FullName
            If MeName Is Nothing Then MeName = Me.Name
        End If
        If TypeOf o Is TypeParameterDescriptor Then
            oName = o.Name
        Else
            oName = o.FullName
            If oName Is Nothing Then oName = o.Name
        End If

        If o Is Me Then
            result = True
        ElseIf TypeOf o Is TypeDescriptor Then
            If o.IsByRef AndAlso Me.IsByRef Then Return Helper.CompareType(Me.GetElementType, o.GetElementType)
            If o.IsArray AndAlso Me.IsArray Then Return Helper.CompareType(Me.GetElementType, o.GetElementType)

            If o.IsGenericType AndAlso Me.IsGenericType AndAlso o.IsGenericTypeDefinition = False AndAlso Me.IsGenericTypeDefinition = False AndAlso o.IsGenericParameter = False AndAlso Me.IsGenericParameter = False Then
                Dim oTypes As Type() = o.GetGenericArguments
                Dim meTypes As Type() = Me.GetGenericArguments()

                If Helper.CompareType(o.GetGenericTypeDefinition, Me.GetGenericTypeDefinition) = False Then
                    Helper.Assert(Helper.CompareName(oName, MeName) = False)
                    Return False
                End If

                If oTypes.Length <> meTypes.Length Then
                    Helper.Assert(Helper.CompareName(oName, MeName) = False)
                    Return False
                End If
                For i As Integer = 0 To oTypes.Length - 1
                    If Helper.CompareType(oTypes(i), meTypes(i)) = False Then
                        Helper.Assert(Helper.CompareName(oName, MeName) = False)
                        Return False
                    End If
                Next
                Helper.Assert(Helper.CompareName(oName, MeName))
                Return True
            End If

            result = Helper.CompareName(oName, MeName)
#If DEBUG Then
            If result Then
                Compiler.Report.WriteLine(Report.ReportLevels.Debug, "Found two equal types by name: " & oName)
            End If
#End If
        ElseIf m_Declaration IsNot Nothing Then
            If m_Declaration.TypeBuilder IsNot Nothing Then
                result = Helper.CompareType(m_Declaration.TypeBuilder, o)
            Else
                result = False
            End If
        Else
            Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        End If
        DumpMethodInfo(result)
        Return result
    End Function

    Public Overrides ReadOnly Property ContainsGenericParameters() As Boolean
        Get
            Dim result As Boolean

            Dim genType As GenericTypeDeclaration = TryCast(m_Declaration, GenericTypeDeclaration)
            If genType IsNot Nothing Then
                If genType.TypeParameters IsNot Nothing Then
                    result = genType.TypeParameters.Parameters.Count > 0
                End If
            End If
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property DeclaringMethod() As System.Reflection.MethodBase
        Get
            Dim result As MethodBase

            Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
            result = Nothing

            DumpMethodInfo(result)
            Return result
        End Get
    End Property
    Public Overrides Function FindInterfaces(ByVal filter As System.Reflection.TypeFilter, ByVal filterCriteria As Object) As System.Type()
        DumpMethodInfo()
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return MyBase.FindInterfaces(filter, filterCriteria)
    End Function
    Public Overrides Function FindMembers(ByVal memberType As System.Reflection.MemberTypes, ByVal bindingAttr As System.Reflection.BindingFlags, ByVal filter As System.Reflection.MemberFilter, ByVal filterCriteria As Object) As System.Reflection.MemberInfo()
        DumpMethodInfo()
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return MyBase.FindMembers(memberType, bindingAttr, filter, filterCriteria)
    End Function
    Public Overrides Function GetArrayRank() As Integer
        DumpMethodInfo()
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return MyBase.GetArrayRank()
    End Function
    Public Overrides Function GetDefaultMembers() As System.Reflection.MemberInfo()
        DumpMethodInfo()
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return MyBase.GetDefaultMembers()
    End Function
    Public Overrides Function GetEvents() As System.Reflection.EventInfo()
        DumpMethodInfo()
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return MyBase.GetEvents()
    End Function

    Public Overrides Function GetGenericArguments() As System.Type()
        Dim result As Type() = Type.EmptyTypes
        Dim tmpResult As New Generic.List(Of Type)

        If m_Declaration IsNot Nothing Then
            Dim gtd As GenericTypeDeclaration = TryCast(m_Declaration, GenericTypeDeclaration)
            If gtd IsNot Nothing Then
                If gtd.TypeParameters IsNot Nothing Then
                    tmpResult.AddRange(gtd.TypeParameters.Parameters.AsTypeArray)
                End If
            End If
            If Me.IsNested Then
                tmpResult.AddRange(Me.DeclaringType.GetGenericArguments)
            End If
        Else
            Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        End If

        result = tmpResult.ToArray

        DumpMethodInfo(result)
        Return result
    End Function

    Public Overrides Function GetGenericParameterConstraints() As System.Type()
        DumpMethodInfo()
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return MyBase.GetGenericParameterConstraints()
    End Function
    Public Overrides Function GetGenericTypeDefinition() As System.Type
        Dim result As Type = Nothing

        If m_Declaration IsNot Nothing Then
            If m_Declaration.TypeBuilder IsNot Nothing Then
                result = m_Declaration.TypeBuilder.GetGenericTypeDefinition
            ElseIf DirectCast(m_Declaration, GenericTypeDeclaration).TypeParameters Is Nothing Then
            Else
                Dim tmpGen As GenericTypeDeclaration = TryCast(m_Declaration, GenericTypeDeclaration)
                If tmpGen IsNot Nothing AndAlso (tmpGen.TypeParameters Is Nothing OrElse tmpGen.TypeParameters.Parameters.Length = 0) Then
                    Return Me
                Else
                    Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
                End If
            End If
        Else
            Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        End If
        DumpMethodInfo(result)
        Return result
    End Function

    Public Overrides Function GetHashCode() As Integer
        Dim result As Integer
        result = m_ObjectID ' MyBase.GetHashCode()
        DumpMethodInfo()

        Return result
    End Function

    Public Overrides Function GetInterfaceMap(ByVal interfaceType As System.Type) As System.Reflection.InterfaceMapping
        DumpMethodInfo()
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return MyBase.GetInterfaceMap(interfaceType)
    End Function
    Public Overrides Function GetMember(ByVal name As String, ByVal bindingAttr As System.Reflection.BindingFlags) As System.Reflection.MemberInfo()
        DumpMethodInfo()
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return MyBase.GetMember(name, bindingAttr)
    End Function
    Public Overrides Function GetMember(ByVal name As String, ByVal type As System.Reflection.MemberTypes, ByVal bindingAttr As System.Reflection.BindingFlags) As System.Reflection.MemberInfo()
        DumpMethodInfo()
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return MyBase.GetMember(name, type, bindingAttr)
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="c"></param>
    ''' <returns>Return Value
    ''' true if the c parameter and the current Type represent the same type, or if the current Type is in 
    ''' the inheritance hierarchy of c, or if the current Type is an interface that c supports. false if 
    ''' none of  these conditions are the case, or if c is a null reference (Nothing in Visual Basic).
    ''' </returns>
    ''' <remarks></remarks>
    Public Overrides Function IsAssignableFrom(ByVal c As System.Type) As Boolean
        Dim result As Boolean
        Helper.Assert(m_Declaration IsNot Nothing)

        If m_Declaration.TypeBuilder IsNot Nothing Then
            result = m_Declaration.TypeBuilder.IsAssignableFrom(c)
        ElseIf TypeOf c Is IType = False Then
            result = False
        Else
            Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        End If

        DumpMethodInfo(result)
        Return result
    End Function

    Protected Overrides Function IsContextfulImpl() As Boolean
        DumpMethodInfo()
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return MyBase.IsContextfulImpl()
    End Function

    Public Overrides Function IsInstanceOfType(ByVal o As Object) As Boolean
        DumpMethodInfo()
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return MyBase.IsInstanceOfType(o)
    End Function

    Protected Overrides Function IsMarshalByRefImpl() As Boolean
        DumpMethodInfo()
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return MyBase.IsMarshalByRefImpl()
    End Function

    Public Overrides Function IsSubclassOf(ByVal c As System.Type) As Boolean
        Dim result As Boolean

        If Helper.CompareType(c, Compiler.TypeCache.System_Object) Then Return True

        Dim base As Type = Me.BaseType
        Do While base IsNot Nothing
            If Helper.CompareType(base, c) Then
                result = True
                Exit Do
            End If
            base = base.BaseType
        Loop
        DumpMethodInfo(result)
        Return result
    End Function

    Protected Overrides Function IsValueTypeImpl() As Boolean
        DumpMethodInfo()
        Return MyBase.IsValueTypeImpl()
    End Function

    Public Overrides Function MakeArrayType() As System.Type
        Dim result As Type
        result = MakeArrayType(1)
        DumpMethodInfo(result)
        Return result
    End Function

    Public Overrides Function MakeArrayType(ByVal rank As Integer) As System.Type
        Dim result As Type = Nothing
        If m_Declaration IsNot Nothing Then
            result = New ArrayTypeDescriptor(Me, rank)
        ElseIf TypeOf Me Is GenericTypeDescriptor Then
            result = New ArrayTypeDescriptor(Me, rank)
        ElseIf TypeOf Me Is ArrayTypeDescriptor Then
            result = New ArrayTypeDescriptor(Me, rank)
        Else
            Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        End If
        'Needs to add this to a cache, otherwise two otherwise equal types might be created with two different 
        'type instances, which is not good is any type comparison would fail.
        Static cache As New Generic.Dictionary(Of String, Type)(Helper.StringComparer)
        If cache.ContainsKey(result.FullName) Then
            result = cache.Item(result.FullName)
        Else
            cache.Add(result.FullName, result)
        End If
        DumpMethodInfo(result)
        Return result
    End Function

    Public Overrides Function MakeByRefType() As System.Type
        Static result As Type = Nothing

        If result Is Nothing Then
            result = New ByRefTypeDescriptor(Me.Parent, Me)
        End If

        DumpMethodInfo(result)

        Return result
    End Function

    Public Overrides Function MakeGenericType(ByVal ParamArray typeArguments() As System.Type) As System.Type
        Dim result As Type = Nothing

        result = Compiler.TypeManager.MakeGenericType(m_Declaration, m_Declaration.TypeDescriptor, typeArguments)

        DumpMethodInfo(result)

        Return result
    End Function

    Public Overrides Function MakePointerType() As System.Type
        Dim result As Type = Nothing
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        DumpMethodInfo(result)
        Return result
    End Function

    Public Overrides ReadOnly Property MemberType() As System.Reflection.MemberTypes
        Get
            Dim result As MemberTypes

            result = MemberTypes.TypeInfo

            Dim tmp As MemberTypes = MyBase.MemberType
            Helper.Assert(result = tmp)
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property MetadataToken() As Integer
        Get
            Dim result As Integer
            DumpMethodInfo(result)
            Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property ReflectedType() As System.Type
        Get
            Dim result As Type = Nothing
            DumpMethodInfo(result)
            Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property StructLayoutAttribute() As System.Runtime.InteropServices.StructLayoutAttribute
        Get
            Dim result As Runtime.InteropServices.StructLayoutAttribute = Nothing
            DumpMethodInfo(result)
            Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property TypeHandle() As System.RuntimeTypeHandle
        Get
            Dim result As RuntimeTypeHandle
            DumpMethodInfo(result)
            Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
            Return result
        End Get
    End Property
#End Region
End Class
