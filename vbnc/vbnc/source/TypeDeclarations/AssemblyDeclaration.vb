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
#Const DEBUGRESOLVE = True
#If DEBUG Then
#Const EXTENDEDDEBUG = 0
#End If

''' <summary>
''' This is the root for the parse tree
''' Start  ::=
'''	[  OptionStatement+  ]
'''	[  ImportsStatement+  ]
'''	[  AttributesStatement+  ]
'''	[  NamespaceMemberDeclaration+  ]
''' </summary>
''' <remarks></remarks>
Public Class AssemblyDeclaration
    Inherits ParsedObject

    ''' <summary>
    ''' The attributes of this assembly.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_Attributes As New Attributes(Me)

    ''' <summary>
    ''' The name of the assembly. 
    ''' </summary>
    ''' <remarks></remarks>
    Private m_Name As String

    ''' <summary>
    ''' All the non-nested types in this assembly.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_Members As New MemberDeclarations(Me)

    ''' <summary>
    ''' All the types as an array of type declarations.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_TypeDeclarations() As TypeDeclaration

    Private m_GroupedClasses As Generic.List(Of MyGroupData)

    Property GroupedClasses() As Generic.List(Of MyGroupData)
        Get
            Return m_GroupedClasses
        End Get
        Set(ByVal value As Generic.List(Of MyGroupData))
            m_GroupedClasses = value
        End Set
    End Property

    ReadOnly Property Members() As MemberDeclarations
        Get
            Return m_Members
        End Get
    End Property

    ReadOnly Property TypeDeclarations() As TypeDeclaration()
        Get
            Return m_TypeDeclarations
        End Get
    End Property

    Sub New(ByVal Parent As Compiler)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal Attributes As Attributes)
        If m_Attributes Is Nothing Then
            m_Attributes = Attributes
        Else
            m_Attributes.AddRange(Attributes)
        End If
        m_TypeDeclarations = m_Members.GetSpecificMembers(Of TypeDeclaration).ToArray

        Helper.Assert(m_Members.Count = m_TypeDeclarations.Length)
    End Sub

    Public Function CreateDefinitions() As Boolean
        Dim result As Boolean = True

        If m_Attributes IsNot Nothing Then
            For i As Integer = 0 To m_Attributes.Count - 1
                result = m_Attributes(i).CreateDefinition() AndAlso result
            Next
        End If

        For i As Integer = 0 To Types.Length - 1
            result = Types(i).CreateDefinition() AndAlso result
        Next

        Return result
    End Function

    Private Function DefineTypeHierarchy(ByVal Type As TypeDeclaration) As Boolean
        Dim result As Boolean = True

        result = Type.DefineTypeHierarchy AndAlso result

        For Each NestedType As TypeDeclaration In Type.Members.GetSpecificMembers(Of TypeDeclaration)()
            result = DefineTypeHierarchy(NestedType) AndAlso result
        Next

        Return result
    End Function

    Friend Function Emit(ByVal Type As TypeDeclaration) As Boolean
        Dim result As Boolean = True

        result = Type.GenerateCode(Nothing) AndAlso result
        result = Type.Members.GenerateCode(Nothing) AndAlso result
        For Each NestedType As TypeDeclaration In Type.Members.GetSpecificMembers(Of TypeDeclaration)()
            result = Emit(NestedType) AndAlso result
        Next

        Return result
    End Function

    Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        For i As Integer = 0 To m_TypeDeclarations.Length - 1
            Dim type As TypeDeclaration = m_TypeDeclarations(i)

#If EXTENDEDDEBUG Then
            Dim iCount As Integer
            iCount += 1
            Try
                System.Console.ForegroundColor = ConsoleColor.Green
            Catch ex As Exception

            End Try
            Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, "ResolveCode " & type.FullName & " (" & iCount & " of " & m_TypeDeclarations.Length & " types)")
            Try
                System.Console.ResetColor()
            Catch ex As Exception

            End Try
#End If
            result = type.ResolveCode(Info) AndAlso result
            Compiler.VerifyConsistency(result, type.Location)
        Next

        result = m_Attributes.ResolveCode(Info) AndAlso result

        Return result
    End Function

    Function CreateImplicitTypes() As Boolean
        Dim result As Boolean = True

        For Each Type As TypeDeclaration In Me.Types
#If EXTENDEDDEBUG Then
            Dim iCount As Integer
            iCount += 1
            Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, "CreateImplicitTypes " & Type.FullName & " (" & iCount & " of " & m_TypeDeclarations.Length & " types)")
#End If
            Dim tmp As IHasImplicitTypes = TryCast(Type, IHasImplicitTypes)
            If tmp IsNot Nothing Then result = tmp.CreateImplicitTypes AndAlso result

            result = CreateImplicitTypes(Type) AndAlso result
        Next

        Return result
    End Function

    Private Function CreateImplicitTypes(ByVal Type As TypeDeclaration) As Boolean
        Dim result As Boolean = True

        For Each NestedType As TypeDeclaration In Type.Members.GetSpecificMembers(Of TypeDeclaration)()
            result = CreateImplicitTypes(NestedType) AndAlso result
        Next

        For Each Member As IHasImplicitTypes In Type.Members.GetSpecificMembers(Of IHasImplicitTypes)()
            result = Member.CreateImplicitTypes() AndAlso result
        Next

        Return result
    End Function

    Function ResolveBaseTypes() As Boolean
        Dim result As Boolean = True

        For i As Integer = 0 To m_TypeDeclarations.Length - 1
            result = m_TypeDeclarations(i).ResolveBaseType() AndAlso result
        Next

        Return result
    End Function

    Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        For Each type As TypeDeclaration In m_TypeDeclarations
#If EXTENDEDDEBUG Then
            Dim iCount As Integer
            iCount += 1
            Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, "ResolveTypeReferences " & type.FullName & " (" & iCount & " of " & m_TypeDeclarations.Length & " types)")
#End If
            result = ResolveTypeReferences(type) AndAlso result
        Next

        result = m_Attributes.ResolveTypeReferences AndAlso result

        Return result
    End Function

    Private Overloads Function ResolveTypeReferences(ByVal Type As TypeDeclaration) As Boolean
        Dim result As Boolean = True

        result = Type.ResolveTypeReferences AndAlso result

        If result = False Then Return result

        For Each Member As ParsedObject In Type.Members
            Dim NestedType As TypeDeclaration = TryCast(Member, TypeDeclaration)
            If NestedType IsNot Nothing Then
                result = ResolveTypeReferences(NestedType) AndAlso result
            Else
                result = Member.ResolveTypeReferences() AndAlso result
            End If
            If result = False Then Return result
        Next

        Return result
    End Function

    Function CreateImplicitMembers() As Boolean
        Dim result As Boolean = True

        For Each Type As TypeDeclaration In Me.Types
#If EXTENDEDDEBUG Then
            Dim iCount As Integer
            iCount += 1
            Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, "CreateImplicitMembers " & Type.FullName & " (" & iCount & " of " & m_TypeDeclarations.Length & " types)")
#End If
            Dim tmp As IHasImplicitMembers = TryCast(Type, IHasImplicitMembers)
            If tmp IsNot Nothing Then result = tmp.CreateImplicitMembers AndAlso result

            result = CreateImplicitMembers(Type) AndAlso result
        Next

        Return result
    End Function

    Public Function CreateImplicitInstanceConstructors() As Boolean
        Dim result As Boolean = True

        For i As Integer = 0 To Me.Types.Length - 1
            result = CreateImplicitInstanceConstructors(Me.Types(i)) AndAlso result
        Next

        Return result
    End Function

    Private Function CreateImplicitInstanceConstructors(ByVal Type As TypeDeclaration) As Boolean
        Dim result As Boolean = True

        result = Type.CreateImplicitInstanceConstructors() AndAlso result

        For i As Integer = 0 To Type.Members.Count - 1
            Dim t As TypeDeclaration = TryCast(Type.Members(i), TypeDeclaration)
            If t Is Nothing Then Continue For
            result = CreateImplicitInstanceConstructors(t) AndAlso result
        Next

        Return result
    End Function

    Public Function CreateImplicitSharedConstructors() As Boolean
        Dim result As Boolean = True

        For i As Integer = 0 To Me.Types.Length - 1
            result = CreateImplicitSharedConstructors(Me.Types(i)) AndAlso result
        Next

        Return result
    End Function

    Private Function CreateImplicitSharedConstructors(ByVal Type As TypeDeclaration) As Boolean
        Dim result As Boolean = True

        result = Type.CreateImplicitSharedConstructors() AndAlso result

        For i As Integer = 0 To Type.Members.Count - 1
            Dim t As TypeDeclaration = TryCast(Type.Members(i), TypeDeclaration)
            If t Is Nothing Then Continue For
            result = CreateImplicitSharedConstructors(t) AndAlso result
        Next

        Return result
    End Function

    Public Function CreateDelegateMembers() As Boolean
        Dim result As Boolean = True

        For i As Integer = 0 To Types.Length - 1
            result = CreateDelegateMembers(Types(i)) AndAlso result
        Next

        Return result
    End Function

    Private Function CreateDelegateMembers(ByVal Type As TypeDeclaration) As Boolean
        Dim result As Boolean = True
        Dim dd As DelegateDeclaration = TryCast(Type, DelegateDeclaration)

        If dd IsNot Nothing Then result = dd.CreateDelegateMembers() AndAlso result

        For i As Integer = 0 To Type.Members.Count - 1
            Dim t As TypeDeclaration = TryCast(Type.Members(i), TypeDeclaration)
            If t Is Nothing Then Continue For
            result = CreateDelegateMembers(t) AndAlso result
        Next

        Return result
    End Function

    Public Function CreateRegularEventMembers() As Boolean
        Dim result As Boolean = True

        For i As Integer = 0 To Types.Length - 1
            result = CreateRegularEventMembers(Types(i)) AndAlso result
        Next

        Return result
    End Function

    Private Function CreateRegularEventMembers(ByVal Type As TypeDeclaration) As Boolean
        Dim result As Boolean = True

        For i As Integer = 0 To Type.Members.Count - 1
            Dim t As TypeDeclaration = TryCast(Type.Members(i), TypeDeclaration)
            If t IsNot Nothing Then result = CreateRegularEventMembers(t) AndAlso result
            Dim red As RegularEventDeclaration = TryCast(Type.Members(i), RegularEventDeclaration)
            If red IsNot Nothing Then result = red.CreateRegularEventMembers AndAlso result
        Next

        Return result
    End Function

    Public Function CreateWithEventsMembers() As Boolean
        Dim result As Boolean = True

        For i As Integer = 0 To Types.Length - 1
            result = CreateWithEventsMembers(Types(i)) AndAlso result
        Next

        Return result
    End Function

    Private Function CreateWithEventsMembers(ByVal member As IMember) As Boolean
        Dim result As Boolean = True
        Dim tvd As TypeVariableDeclaration
        Dim t As TypeDeclaration

        tvd = TryCast(member, TypeVariableDeclaration)
        If tvd IsNot Nothing Then result = tvd.CreateWithEventsMembers AndAlso result

        t = TryCast(member, TypeDeclaration)
        If t Is Nothing Then Return result

        For i As Integer = 0 To t.Members.Count - 1
            result = CreateWithEventsMembers(t.Members(i)) AndAlso result
        Next

        Return result
    End Function

    Public Function CreateMyGroupMembers() As Boolean
        Dim result As Boolean = True

        For i As Integer = 0 To Me.Types.Length - 1
            result = CreateMyGroupMembers(Types(i)) AndAlso result
        Next

        Return result
    End Function

    Private Function CreateMyGroupMembers(ByVal Type As TypeDeclaration) As Boolean
        Dim cd As ClassDeclaration
        Dim result As Boolean = True

        cd = TryCast(Type, ClassDeclaration)
        If cd IsNot Nothing Then result = cd.CreateMyGroupMembers() AndAlso result

        For i As Integer = 0 To Type.Members.Count - 1
            Dim t As TypeDeclaration = TryCast(Type.Members(i), TypeDeclaration)
            If t Is Nothing Then Continue For
            result = CreateMyGroupMembers(t) AndAlso result
        Next

        Return result
    End Function

    Private Function CreateImplicitMembers(ByVal Type As TypeDeclaration) As Boolean
        Dim result As Boolean = True

        For Each NestedType As TypeDeclaration In Type.Members.GetSpecificMembers(Of TypeDeclaration)()
            result = CreateImplicitMembers(NestedType) AndAlso result
        Next

        For Each Member As IHasImplicitMembers In Type.Members.GetSpecificMembers(Of IHasImplicitMembers)()
            result = Member.CreateImplicitMembers() AndAlso result
        Next

        Return result
    End Function

    Function ResolveMembers() As Boolean
        Dim result As Boolean = True

        For Each type As TypeDeclaration In m_TypeDeclarations
#If EXTENDEDDEBUG Then
            Dim iCount As Integer
            iCount += 1
            Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, "ResolveMembers " & type.FullName & " (" & iCount & " of " & m_TypeDeclarations.Length & " types)")
#End If
            result = ResolveMembers(type) AndAlso result
        Next

        Return result
    End Function

    Private Shared Function ResolveMembers(ByVal Type As TypeDeclaration) As Boolean
        Dim result As Boolean = True

        For Each n As IBaseObject In Type.Members.GetSpecificMembers(Of IBaseObject)()
            Dim nType As TypeDeclaration = TryCast(n, TypeDeclaration)
            Dim nMember As INonTypeMember = TryCast(n, INonTypeMember)

            If nType IsNot Nothing Then
                result = ResolveMembers(nType) AndAlso result
            ElseIf nMember IsNot Nothing Then
                'Resolve all non-type members.
                result = nMember.ResolveMember(ResolveInfo.Default(Type.Compiler)) AndAlso result
            Else
                Helper.Stop() '?
            End If
        Next

        Return result
    End Function

    ''' <summary>
    ''' - Base classes for classes, modules, structures, enums, interfaces and delegates are set.
    ''' - Implemented interfaces for classes are set.
    ''' - Type parameters for classes and structures are set.
    ''' - Classes, modules, structures, interfaces, enums,  delegates and events should implement IDefinable.DefineTypeHierarchy()
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DefineTypeHierarchy() As Boolean
        Dim result As Boolean = True

        For Each type As TypeDeclaration In m_TypeDeclarations
#If EXTENDEDDEBUG Then
            Dim iCount As Integer
            iCount += 1
            Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, "DefineTypeHierarchy " & type.FullName & " (" & iCount & " of " & m_TypeDeclarations.Length & " types)")
#End If
            result = DefineTypeHierarchy(type) AndAlso result
        Next

        Return result
    End Function

    Function DefineConstants() As Boolean
        Dim result As Boolean = True

        For i As Integer = 0 To Types.Length - 1
            result = DefineConstants(Types(i)) AndAlso result
        Next

        Return result
    End Function

    Private Function DefineConstants(ByVal Type As TypeDeclaration) As Boolean
        Dim result As Boolean = True

        For i As Integer = 0 To Type.Members.Count - 1
            Dim nestedType As TypeDeclaration = TryCast(Type.Members(i), TypeDeclaration)
            If nestedType IsNot Nothing Then
                result = DefineConstants(nestedType) AndAlso result
                Continue For
            End If

            Dim constant As ConstantDeclaration = TryCast(Type.Members(i), ConstantDeclaration)
            If constant IsNot Nothing Then
                result = constant.DefineConstant AndAlso result
                Continue For
            End If

            Dim enumc As EnumMemberDeclaration = TryCast(Type.Members(i), EnumMemberDeclaration)
            If enumc IsNot Nothing Then
                result = enumc.DefineConstant AndAlso result
                Continue For
            End If
        Next

        Return result
    End Function

    Function DefineOptionalParameters() As Boolean
        Dim result As Boolean = True

        For i As Integer = 0 To Types.Length - 1
            result = DefineOptionalParameters(Types(i)) AndAlso result
        Next

        Return result
    End Function

    Private Function DefineOptionalParameters(ByVal Type As TypeDeclaration) As Boolean
        Dim result As Boolean = True

        For i As Integer = 0 To Type.Members.Count - 1
            Dim nestedType As TypeDeclaration = TryCast(Type.Members(i), TypeDeclaration)
            If nestedType IsNot Nothing Then
                result = DefineOptionalParameters(nestedType) AndAlso result
                Continue For
            End If

            Dim method As MethodBaseDeclaration = TryCast(Type.Members(i), MethodBaseDeclaration)
            If method IsNot Nothing Then
                result = method.DefineOptionalParameters AndAlso result
                Continue For
            End If

            Dim prop As PropertyDeclaration = TryCast(Type.Members(i), PropertyDeclaration)
            If prop IsNot Nothing Then
                result = prop.Signature.Parameters.DefineOptionalParameters AndAlso result
                If prop.SetDeclaration IsNot Nothing Then
                    result = prop.SetDeclaration.Signature.Parameters.DefineOptionalParameters AndAlso result
                End If
                If prop.GetDeclaration IsNot Nothing Then
                    result = prop.GetDeclaration.Signature.Parameters.DefineOptionalParameters AndAlso result
                End If
                Continue For
            End If
        Next

        Return result
    End Function

    Public Function DefineSecurityDeclarations() As Boolean
        Dim result As Boolean = True

        For i As Integer = 0 To Types.Length - 1
            result = DefineSecurityDeclarations(Types(i)) AndAlso result
        Next

        Return result
    End Function

    Private Function DefineSecurityDeclarations(ByVal Type As TypeDeclaration) As Boolean
        Dim result As Boolean = True

        For i As Integer = 0 To Type.Members.Count - 1
            Dim nestedType As TypeDeclaration = TryCast(Type.Members(i), TypeDeclaration)
            If nestedType IsNot Nothing Then
                result = DefineSecurityDeclarations(nestedType) AndAlso result
                Continue For
            End If

            Dim method As MethodBaseDeclaration = TryCast(Type.Members(i), MethodBaseDeclaration)
            If method IsNot Nothing Then
                result = method.DefineSecurityDeclarations AndAlso result
                Continue For
            End If
        Next

        Return result
    End Function

    Function EmitAttributes() As Boolean
        Dim result As Boolean = True

        If m_Attributes IsNot Nothing Then
            result = m_Attributes.GenerateCode(Nothing) AndAlso result
        End If

        Return result
    End Function

    ''' <summary>
    ''' - All code is emitted for fields with initializers.
    ''' - All the code is emitted for each and every method, constructor, operator and property.
    ''' - Classes, modules, structures, methods, constructors, properties, events, operators should implement IEmittable.Emit(Info as EmitInfo)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Emit() As Boolean
        Dim result As Boolean = True

        result = EmitAttributes() AndAlso result

        For Each type As TypeDeclaration In m_TypeDeclarations
#If EXTENDEDDEBUG Then
            Dim iCount As Integer
            iCount += 1
            Try
                System.Console.ForegroundColor = ConsoleColor.Yellow
            Catch ex As Exception

            End Try
            Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, "Emit " & type.FullName & " (" & iCount & " of " & m_TypeDeclarations.Length & " types)")
            Try
                System.Console.ResetColor()
            Catch ex As Exception

            End Try
#End If
            result = Emit(type) AndAlso result
        Next

        SetFileVersion()
        SetAdditionalAttributes()

        Return result
    End Function

    Sub SetAdditionalAttributes()
        Dim cab As Mono.Cecil.CustomAttribute

        If Compiler.CommandLine.Define.IsDefined("DEBUG") Then
            cab = New Mono.Cecil.CustomAttribute(Helper.GetMethodOrMethodReference(Compiler, Compiler.TypeCache.System_Diagnostics_DebuggableAttribute__ctor_DebuggingModes))
            cab.ConstructorArguments.Add(New CustomAttributeArgument(Compiler.TypeCache.System_Diagnostics_DebuggableAttribute, System.Diagnostics.DebuggableAttribute.DebuggingModes.DisableOptimizations Or Diagnostics.DebuggableAttribute.DebuggingModes.Default))
            Me.Compiler.AssemblyBuilderCecil.CustomAttributes.Add(cab)
        End If
    End Sub

    Sub SetFileVersion()
        Dim product As String = String.Empty
        Dim productversion As String = String.Empty
        Dim company As String = String.Empty
        Dim copyright As String = String.Empty
        Dim trademark As String = String.Empty

        Dim att As Mono.Collections.Generic.Collection(Of CustomAttribute)
        Dim custom_attributes As Mono.Collections.Generic.Collection(Of CustomAttribute) = Me.Compiler.AssemblyBuilderCecil.CustomAttributes

        att = CecilHelper.GetCustomAttributes(custom_attributes, Compiler.TypeCache.System_Reflection_AssemblyProductAttribute)
        If att IsNot Nothing AndAlso att.Count > 0 Then product = CecilHelper.GetAttributeCtorString(att(0), 0)
        att = CecilHelper.GetCustomAttributes(custom_attributes, Compiler.TypeCache.System_Reflection_AssemblyVersionAttribute)
        If att IsNot Nothing AndAlso att.Count > 0 Then productversion = CecilHelper.GetAttributeCtorString(att(0), 0)
        att = CecilHelper.GetCustomAttributes(custom_attributes, Compiler.TypeCache.System_Reflection_AssemblyCompanyAttribute)
        If att IsNot Nothing AndAlso att.Count > 0 Then company = CecilHelper.GetAttributeCtorString(att(0), 0)
        att = CecilHelper.GetCustomAttributes(custom_attributes, Compiler.TypeCache.System_Reflection_AssemblyCopyrightAttribute)
        If att IsNot Nothing AndAlso att.Count > 0 Then copyright = CecilHelper.GetAttributeCtorString(att(0), 0)
        att = CecilHelper.GetCustomAttributes(custom_attributes, Compiler.TypeCache.System_Reflection_AssemblyTrademarkAttribute)
        If att IsNot Nothing AndAlso att.Count > 0 Then trademark = CecilHelper.GetAttributeCtorString(att(0), 0)

        'Dim rdt As New Mono.Cecil.PE.ResourceDirectoryTable()
        'Dim r1 As New Mono.Cecil.PE.ResourceDirectoryEntry(16)
        'Dim r2 As New Mono.Cecil.PE.ResourceDirectoryEntry(1)
        'Dim r3 As New Mono.Cecil.PE.ResourceDirectoryEntry(0)
        'Dim data As New Mono.Cecil.PE.ResourceDataEntry()

        'Dim rsrc As Mono.Cecil.PE.Section = Nothing
        'If Compiler.AssemblyBuilderCecil.MainModule.Image Is Nothing Then
        '    Compiler.AssemblyBuilderCecil.MainModule.Image = New Mono.Cecil.PE.Image()
        '    Compiler.AssemblyBuilderCecil.MainModule.Image.Sections = New Mono.Cecil.PE.Section() {}
        'End If
        'rsrc = Compiler.AssemblyBuilderCecil.MainModule.Image.GetSection(".rsrc")
        'If rsrc Is Nothing Then
        '    rsrc = New Mono.Cecil.PE.Section()
        '    rsrc.Name = ".rsrc"
        '    Dim sections(Me.Compiler.AssemblyBuilderCecil.MainModule.Image.Sections.Length) As Mono.Cecil.PE.Section
        '    For i As Integer = 0 To sections.Length - 2
        '        sections(i) = Compiler.AssemblyBuilderCecil.MainModule.Image.Sections(i)
        '    Next
        '    sections(sections.Length - 1) = rsrc
        '    Me.Compiler.AssemblyBuilderCecil.MainModule.Image.Sections = sections
        'End If
        'Me.Compiler.AssemblyBuilderCecil.MainModule.Image.ResourceDirectoryRoot = New Mono.Cecil.PE.ResourceDirectoryTable()

        'Me.Compiler.AssemblyBuilderCecil.MainModule.Image.ResourceDirectoryRoot.Entries.Add(r1)
        'rdt = New Mono.Cecil.Binary.ResourceDirectoryTable()
        'rdt.Entries.Add(r2)
        'r1.Child = rdt

        'rdt = New Mono.Cecil.Binary.ResourceDirectoryTable()
        'rdt.Entries.Add(r3)
        'r2.Child = rdt

        'r3.Child = data
        'data.Size = 0
        'data.ResourceData = New Byte() {}

        Dim win32versionresources As Byte()
        Using ms As New IO.MemoryStream()
            Using w As New IO.BinaryWriter(ms, System.Text.Encoding.Unicode)
                Dim file_flags_mask As Integer = 63
                Dim file_flags As Integer = 0
                Dim file_os As Integer = 4 '/* VOS_WIN32 */
                Dim file_type As Integer = 2
                Dim file_subtype As Integer = 0
                Dim file_date As Long = 0
                Dim file_lang As Integer = 0
                Dim file_codepage As Integer = 1200
                Dim properties As New Generic.Dictionary(Of String, String)

                Dim file_version As Long
                Dim product_version As Long

                Dim WellKnownProperties As String() = New String() {"Assembly Version", "FileDescription", "FileVersion", "InternalName", "OriginalFilename", "ProductVersion"}

                For i As Integer = 0 To WellKnownProperties.Length - 1
                    properties.Add(WellKnownProperties(i), WellKnownProperties(i) & " ")
                Next

                If product <> String.Empty Then properties("ProductName") = product
                If productversion <> String.Empty Then
                    properties("ProductVersion") = productversion
                    properties("FileVersion") = productversion
                End If
                If company <> String.Empty Then properties("Company") = company
                If copyright <> String.Empty Then properties("LegalCopyright") = copyright
                If trademark <> String.Empty Then properties("LegalTrademark") = trademark

                'VS_VERSIONINFO
                w.Write(CShort(0))
                w.Write(CShort(&H34))
                w.Write(CShort(0))
                w.Write("VS_VERSION_INFO".ToCharArray())
                w.Write(CShort(0))
                emit_padding(w)

                '// VS_FIXEDFILEINFO
                w.Write(&HFEEF04BDUI)
                w.Write(CInt(1 << 16))
                w.Write(CInt(file_version >> 32)) 'TODO: FileVersion?
                w.Write(CInt(((file_version And &HFFFFFFFF)))) 'TODO: ?

                w.Write(CInt(product_version >> 32))
                w.Write(CInt(product_version And &HFFFFFFFF))
                w.Write(CInt(file_flags_mask))
                w.Write(CInt(file_flags))
                w.Write(CInt(file_os))
                w.Write(CInt(file_type))
                w.Write(CInt(file_subtype))
                w.Write(CInt((file_date >> 32)))
                w.Write(CInt((file_date And &HFFFFFFFF)))

                emit_padding(w)

                '// VarFileInfo
                Dim var_file_info_pos As Long = ms.Position
                w.Write(CShort(0))
                w.Write(CShort(0))
                w.Write(CShort(1))
                w.Write("VarFileInfo".ToCharArray())
                w.Write(CShort(0))

                If ((ms.Position Mod 4) <> 0) Then
                    w.Write(CShort(0))
                End If

                '// Var
                Dim var_pos As Long = ms.Position
                w.Write(CShort(0))
                w.Write(CShort(4))
                w.Write(CShort(0))
                w.Write("Translation".ToCharArray())
                w.Write(CShort(0))

                If ((ms.Position Mod 4) <> 0) Then
                    w.Write(CShort(0))
                End If

                w.Write(CShort(file_lang))
                w.Write(CShort(file_codepage))

                patch_length(w, var_pos)

                patch_length(w, var_file_info_pos)

                '// StringFileInfo
                Dim string_file_info_pos As Long = ms.Position
                w.Write(CShort(0))
                w.Write(CShort(0))
                w.Write(CShort(1))
                w.Write("StringFileInfo".ToCharArray())

                emit_padding(w)

                '// StringTable
                Dim string_table_pos As Long = ms.Position
                w.Write(CShort(0))
                w.Write(CShort(0))
                w.Write(CShort(1))
                w.Write(String.Format("{0:x4}{1:x4}", file_lang, file_codepage).ToCharArray())

                emit_padding(w)

                '// Strings
                For Each key As String In properties.Keys
                    Dim value As String = properties(key)

                    Dim string_pos As Long = ms.Position
                    w.Write(CShort(0))
                    w.Write(CShort((value.ToCharArray().Length + 1)))
                    w.Write(CShort(1))
                    w.Write(key.ToCharArray())
                    w.Write(CShort(0))

                    emit_padding(w)

                    w.Write(value.ToCharArray())
                    w.Write(CShort(0))

                    emit_padding(w)

                    patch_length(w, string_pos)
                Next

                patch_length(w, string_table_pos)

                patch_length(w, string_file_info_pos)

                patch_length(w, 0)
                win32versionresources = ms.ToArray()
            End Using
        End Using


        Using ms As New IO.MemoryStream()
            Using w As New IO.BinaryWriter(ms, System.Text.Encoding.Unicode)
                ' IMAGE_RESOURCE_DIRECTORY
                w.Write(0UI) 'characteristics
                w.Write(0UI) 'timedatestamp
                w.Write(0US) 'majorversion
                w.Write(0US) 'minorversion
                w.Write(0US) 'NumberOfNamedEntries
                w.Write(1US) 'NumberOfIdEntries

                '16 bytes

                ' IMAGE_RESOURCE_DIRECTORY_ENTRY
                w.Write(16UI) 'name
                w.Write(&H20UI + &H80000000UI) 'dataoffset
                w.Write(0UI) 'codepage
                w.Write(0UI) 'reserved

                ' 16 bytes, total 32 bytes

                ' IMAGE_RESOURCE_DIRECTORY
                w.Write(0UI) 'characteristics
                w.Write(0UI) 'timedatestamp
                w.Write(0US) 'majorversion
                w.Write(0US) 'minorversion
                w.Write(0US) 'NumberOfNamedEntries
                w.Write(1US) 'NumberOfIdEntries

                ' 16 bytes, total 48 bytes

                ' IMAGE_RESOURCE_DIRECTORY_ENTRY
                w.Write(1UI) 'name
                w.Write(&H40UI + &H80000000UI) 'dataoffset
                w.Write(0UI) 'codepage
                w.Write(0UI) 'reserved

                ' 16 bytes, total 64 bytes

                ' IMAGE_RESOURCE_DIRECTORY
                w.Write(0UI) 'characteristics
                w.Write(0UI) 'timedatestamp
                w.Write(0US) 'majorversion
                w.Write(0US) 'minorversion
                w.Write(0US) 'NumberOfNamedEntries
                w.Write(1US) 'NumberOfIdEntries

                ' 16 bytes, total 80 bytes

                ' IMAGE_RESOURCE_DIRECTORY_ENTRY
                w.Write(0UI) 'name
                w.Write(&H60UI) 'dataoffset
                w.Write(0UI) 'codepage
                w.Write(0UI) 'reserved

                ' 16 bytes, total 96 bytes

                ' IMAGE_RESOURCE_DATA_ENTRY
                w.Write(&H70UI) 'offsettodata
                w.Write(CUInt(win32versionresources.Length)) 'size
                w.Write(0UI) 'codepage
                w.Write(0UI) 'reserved

                w.Write(win32versionresources)

                Compiler.ModuleBuilderCecil.Win32Resources = ms.ToArray()
            End Using
        End Using
    End Sub

    Private Sub patch_length(ByVal w As IO.BinaryWriter, ByVal len_pos As Long)
        Dim ms As IO.Stream = w.BaseStream

        Dim pos As Long = ms.Position
        ms.Position = len_pos
        w.Write(CShort(pos - len_pos))
        ms.Position = pos
    End Sub

    Private Sub emit_padding(ByVal w As IO.BinaryWriter)
        Dim ms As IO.Stream = w.BaseStream

        If ((ms.Position Mod 4) <> 0) Then
            w.Write(CShort(0))
        End If
    End Sub

    Public Function SetCecilName(ByVal Name As Mono.Cecil.AssemblyNameDefinition) As Boolean
        Dim result As Boolean = True
        Dim keyfile As String = Nothing
        Dim keyname As String = Nothing
        Dim delaysign As Boolean = False

        Name.Name = IO.Path.GetFileNameWithoutExtension(Compiler.OutFileName)

        If Compiler.CommandLine.KeyFile <> String.Empty Then
            keyfile = Compiler.CommandLine.KeyFile
        End If

        For Each attri As Attribute In Me.Attributes
            Dim attribType As Mono.Cecil.TypeReference
            attribType = attri.ResolvedType

            If Helper.CompareType(attribType, Compiler.TypeCache.System_Reflection_AssemblyVersionAttribute) Then
                result = SetVersion(Name, attri, attri.Location) AndAlso result
            ElseIf Helper.CompareType(attribType, Compiler.TypeCache.System_Reflection_AssemblyKeyFileAttribute) Then
                If keyfile = String.Empty Then keyfile = TryCast(attri.Arguments()(0), String)
            ElseIf Helper.CompareType(attribType, Compiler.TypeCache.System_Reflection_AssemblyKeyNameAttribute) Then
                keyname = TryCast(attri.Arguments()(0), String)
            ElseIf Helper.CompareType(attribType, Compiler.TypeCache.System_Reflection_AssemblyDelaySignAttribute) Then
                delaysign = CBool(attri.Arguments()(0))
            End If
        Next

        If keyfile <> String.Empty Then
            If SignWithKeyFile(Name, keyfile, delaysign) = False Then
                Return result
            End If
        End If

        Return result
    End Function

    Private Function SignWithKeyFile(ByVal result As Mono.Cecil.AssemblyNameDefinition, ByVal KeyFile As String, ByVal DelaySign As Boolean) As Boolean
        Dim filename As String

        filename = IO.Path.GetFullPath(KeyFile)

#If DEBUG Then
        Compiler.Report.WriteLine("Signing with file: " & filename)
#End If

        If IO.File.Exists(filename) = False Then
            Helper.AddError(Me, "Can't find keyfile: " & filename)
            Return False
        End If

        Using stream As New IO.FileStream(filename, IO.FileMode.Open, IO.FileAccess.Read)
            Dim snkeypair() As Byte
            ReDim snkeypair(CInt(stream.Length - 1))
            stream.Read(snkeypair, 0, snkeypair.Length)

            If Helper.IsOnMono Then
                SignWithKeyFileMono(result, filename, DelaySign, snkeypair)
            Else
                If DelaySign Then
                    result.PublicKey = snkeypair
                Else
                    'FIXME: Check if this is correct, I suppose it's not
                    'result.KeyPair = New StrongNameKeyPair(snkeypair)
                    result.PublicKey = snkeypair
                End If
            End If

        End Using

        Compiler.ModuleBuilderCecil.Attributes = Compiler.ModuleBuilderCecil.Attributes Or ModuleAttributes.StrongNameSigned

        Return True
    End Function

    Private Sub SignWithKeyFileMono(ByVal result As Mono.Cecil.AssemblyNameDefinition, ByVal KeyFile As String, ByVal DelaySign As Boolean, ByVal blob As Byte())
        Dim CryptoConvert As Type
        Dim FromCapiKeyBlob As System.Reflection.MethodInfo
        Dim ToCapiPublicKeyBlob As System.Reflection.MethodInfo
        Dim FromCapiPrivateKeyBlob As System.Reflection.MethodInfo
        Dim RSA As Type
        Dim mscorlib As System.Reflection.Assembly = GetType(Integer).Assembly

#If DEBUG Then
        Compiler.Report.WriteLine("Signing on Mono")
#End If

        Try
            RSA = mscorlib.GetType("System.Security.Cryptography.RSA")
            CryptoConvert = mscorlib.GetType("Mono.Security.Cryptography.CryptoConvert")
            FromCapiKeyBlob = CryptoConvert.GetMethod("FromCapiKeyBlob", System.Reflection.BindingFlags.Public Or System.Reflection.BindingFlags.Static Or System.Reflection.BindingFlags.ExactBinding, Nothing, New Type() {GetType(Byte())}, Nothing)
            ToCapiPublicKeyBlob = CryptoConvert.GetMethod("ToCapiPublicKeyBlob", System.Reflection.BindingFlags.Static Or System.Reflection.BindingFlags.Public Or System.Reflection.BindingFlags.ExactBinding, Nothing, New Type() {RSA}, Nothing)
            FromCapiPrivateKeyBlob = CryptoConvert.GetMethod("FromCapiPrivateKeyBlob", System.Reflection.BindingFlags.Static Or System.Reflection.BindingFlags.Public Or System.Reflection.BindingFlags.ExactBinding, Nothing, New Type() {GetType(Byte())}, Nothing)

            If DelaySign Then
                If blob.Length = 16 Then
                    result.PublicKey = blob
#If DEBUG Then
                    Compiler.Report.WriteLine("Delay signed 1")
#End If
                Else
                    Dim publickey() As Byte
                    Dim fromCapiResult As Object
                    Dim publicKeyHeader As Byte() = New Byte() {&H0, &H24, &H0, &H0, &H4, &H80, &H0, &H0, &H94, &H0, &H0, &H0}
                    Dim encodedPublicKey() As Byte

                    fromCapiResult = FromCapiKeyBlob.Invoke(Nothing, New Object() {blob})
                    publickey = CType(ToCapiPublicKeyBlob.Invoke(Nothing, New Object() {fromCapiResult}), Byte())

                    ReDim encodedPublicKey(11 + publickey.Length)
                    Buffer.BlockCopy(publicKeyHeader, 0, encodedPublicKey, 0, 12)
                    Buffer.BlockCopy(publickey, 0, encodedPublicKey, 12, publickey.Length)
                    result.PublicKey = encodedPublicKey
#If DEBUG Then
                    Compiler.Report.WriteLine("Delay signed 2")
#End If
                End If
            Else
                FromCapiPrivateKeyBlob.Invoke(Nothing, New Object() {blob})
                'FIXME: Check if this is correct, I suppose it's not
                'result.KeyPair = New StrongNameKeyPair(blob)
                result.PublicKey = blob
            End If
        Catch ex As Exception
            Helper.AddError(Me, "Invalid key file: " & KeyFile & ", got error: " & ex.Message)
        End Try
    End Sub

    Private Function SetVersion(ByVal Name As Mono.Cecil.AssemblyNameDefinition, ByVal Attribute As Attribute, ByVal Location As Span) As Boolean
        Dim result As Version
        Dim version As String = ""

        If Attribute.Arguments IsNot Nothing AndAlso Attribute.Arguments.Length = 1 Then
            version = TryCast(Attribute.Arguments()(0), String)
        Else
            Return ShowInvalidVersionMessage(version, Location)
        End If

        Try
            Dim parts() As String
            Dim major, minor, build, revision As UShort
            parts = version.Split("."c)

            If parts.Length > 4 Then
                Return ShowInvalidVersionMessage(version, Location)
            End If

            If Not UShort.TryParse(parts(0), major) Then
                Return ShowInvalidVersionMessage(version, Location)
            End If

            If Not UShort.TryParse(parts(1), minor) Then
                Return ShowInvalidVersionMessage(version, Location)
            End If

            If parts.Length < 3 Then
                'Use 0
            ElseIf parts(2) = "*" Then
                build = CUShort((Date.Now - New Date(2000, 1, 1)).TotalDays)
                revision = CUShort((Date.Now.Hour * 3600 + Date.Now.Minute * 60 + Date.Now.Second) / 2)
            ElseIf Not UShort.TryParse(parts(2), build) Then
                Return ShowInvalidVersionMessage(version, Location)
            End If

            If parts.Length < 4 Then
                'Use 0
            ElseIf parts.Length > 3 Then
                If parts(3) = "*" Then
                    revision = CUShort((Date.Now.Hour * 3600 + Date.Now.Minute * 60 + Date.Now.Second) / 2)
                ElseIf Not UShort.TryParse(parts(3), revision) Then
                    Return ShowInvalidVersionMessage(version, Location)
                End If
            End If

            result = New Version(major, minor, build, revision)
        Catch ex As Exception
            Return ShowInvalidVersionMessage(version, Location)
        End Try

        Name.Version = result
        Return True
    End Function

    Private Function ShowInvalidVersionMessage(ByVal Version As String, ByVal Location As Span) As Boolean
        Compiler.Report.ShowMessage(Messages.VBNC30129, Location, "System.Reflection.AssemblyVersionAttribute", Version)
        Return False
    End Function

    ''' <summary>
    ''' Checks whether the specified Type is defined in the current compiling assembly
    ''' </summary>
    ''' <param name="Type"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function IsDefinedHere(ByVal Type As Mono.Cecil.TypeReference) As Boolean
        Helper.Assert(Type IsNot Nothing)
        If TypeOf Type Is Mono.Cecil.ArrayType Then
            Return IsDefinedHere(DirectCast(Type, Mono.Cecil.ArrayType).ElementType)
        ElseIf TypeOf Type Is Mono.Cecil.GenericParameter Then
            Dim gp As Mono.Cecil.GenericParameter = DirectCast(Type, Mono.Cecil.GenericParameter)
            If TypeOf gp.Owner Is Mono.Cecil.TypeDefinition Then
                Return IsDefinedHere(DirectCast(gp.Owner, Mono.Cecil.TypeDefinition))
            ElseIf TypeOf gp.Owner Is Mono.Cecil.MethodDefinition Then
                Return IsDefinedHere(DirectCast(gp.Owner, Mono.Cecil.MethodDefinition))
            ElseIf TypeOf gp.Owner Is Mono.Cecil.MethodReference Then
                Return IsDefinedHere(DirectCast(gp.Owner, Mono.Cecil.MethodReference))
            Else
                Throw New NotImplementedException
            End If
        ElseIf TypeOf Type Is ByReferenceType Then
            Dim tR As ByReferenceType = DirectCast(Type, ByReferenceType)
            Return IsDefinedHere(tR.ElementType)
        End If
        Return Type.Module.Assembly Is Compiler.AssemblyBuilderCecil
    End Function

    ''' <summary>
    ''' Checks whether the specified Type is defined in the current compiling assembly
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function IsDefinedHere(ByVal Member As Mono.Cecil.MemberReference) As Boolean
        Helper.Assert(Member IsNot Nothing)
        Return Member.DeclaringType.Module.Assembly Is Compiler.AssemblyBuilderCecil
    End Function

    Function FindType(ByVal FullName As String) As TypeDeclaration
        For Each type As TypeDeclaration In Me.Types
            If Helper.CompareName(type.FullName, FullName) Then Return type
        Next
        Return Nothing
    End Function

    Function FindTypeWithName(ByVal Name As String) As TypeDeclaration
        For i As Integer = 0 To m_Members.Count - 1
            Dim tD As TypeDeclaration = TryCast(m_Members(i), TypeDeclaration)
            If tD Is Nothing Then Continue For
            If Helper.CompareName(tD.Name, Name) Then Return tD
        Next
        Return Nothing
    End Function

    Function FindTypeWithFullname(ByVal Fullname As String) As TypeDeclaration
        For i As Integer = 0 To m_Members.Count - 1
            Dim tD As TypeDeclaration = TryCast(m_Members(i), TypeDeclaration)
            If tD Is Nothing Then Continue For
            If Helper.CompareName(tD.FullName, Fullname) Then Return tD
        Next
        Return Nothing
    End Function

    Property Name() As String
        Get
            Return m_Name
        End Get
        Set(ByVal value As String)
            m_Name = value
        End Set
    End Property

    ReadOnly Property Types() As TypeDeclaration()
        Get
            Return m_TypeDeclarations
        End Get
    End Property

    ReadOnly Property Attributes() As Attributes
        Get
            Return m_Attributes
        End Get
    End Property
End Class