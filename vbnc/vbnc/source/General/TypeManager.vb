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
#Const EXTENDEDDEBUG = 0
#End If

''' <summary>
''' A helper class containing all information about the referenced assemblies,
''' loaded types, etc.
''' </summary>
''' <remarks></remarks>
Public Class TypeManager
    Private m_CecilAssemblies As New Generic.List(Of Mono.Cecil.AssemblyDefinition)
    Private m_CecilTypes As New TypeList
    Private m_CecilModuleTypes As New TypeList
    Private m_CecilTypesByNamespace As New NamespaceDictionary
    
    Private m_TypesByNamespaceAndName As New Generic.Dictionary(Of String, Mono.Collections.Generic.Collection(Of Mono.Cecil.MemberReference))

    ''' <summary>
    ''' All the modules indexed by namespace.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_CecilModulesByNamespace As New NamespaceDictionary

    ''' <summary>
    ''' All the namespaces available.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_Namespaces As New Namespaces

    ''' <summary>
    ''' All the types that are modules.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_ModuleTypes As New TypeList

    Private m_Compiler As Compiler

    Private Shared m_GenericTypeCache As New Generic.Dictionary(Of String, Mono.Cecil.GenericInstanceType)(vbnc.Helper.StringComparer)

    Public MemberCache As New Generic.Dictionary(Of Mono.Cecil.TypeReference, MemberCache)(New TypeComparer)

    ReadOnly Property Corlib As AssemblyDefinition
        Get
            For i As Integer = 0 To m_CecilAssemblies.Count - 1
                If Helper.CompareNameOrdinal(m_CecilAssemblies(i).Name.Name, "mscorlib") Then Return m_CecilAssemblies(i)
            Next
            Return Nothing
        End Get
    End Property

    Function FindAssemblyDefinition(ByVal Fullname As String) As Mono.Cecil.AssemblyDefinition
        For i As Integer = 0 To m_CecilAssemblies.Count - 1
            Dim a As Mono.Cecil.AssemblyDefinition
            a = m_CecilAssemblies(i)
            If Helper.CompareNameOrdinal(a.Name.FullName, Fullname) Then
                Return a
            End If
        Next
        Return Nothing
    End Function

    Function GetCache(ByVal Type As Mono.Cecil.TypeReference) As MemberCache
        Dim result As MemberCache = Nothing

        If MemberCache.TryGetValue(Type, result) Then
            Return result
        Else
            Return New MemberCache(Compiler, Type)
        End If
    End Function

    Sub ClearCache(ByVal Type As Mono.Cecil.TypeReference)
        Dim result As MemberCache = Nothing
        If MemberCache.TryGetValue(Type, result) Then
            result.ClearAll()
        End If
    End Sub

    Function ContainsCache(ByVal Type As Mono.Cecil.TypeReference) As Boolean
        Return MemberCache.ContainsKey(Type)
    End Function

    ReadOnly Property CecilAssemblies() As Generic.List(Of Mono.Cecil.AssemblyDefinition)
        Get
            Return m_CecilAssemblies
        End Get
    End Property

    ''' <summary>
    ''' All the non-nested types available.
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property Types() As TypeList
        Get
            Return m_CecilTypes
        End Get
    End Property

    ''' <summary>
    ''' All the non-nested  types indexed by namespace.
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property TypesByNamespace() As NamespaceDictionary
        Get
            Return m_CecilTypesByNamespace
        End Get
    End Property

    Function GetTypesByNamespaceAndName(ByVal [Namespace] As String, ByVal Name As String) As Mono.Collections.Generic.Collection(Of Mono.Cecil.MemberReference)
        Dim result As Mono.Collections.Generic.Collection(Of Mono.Cecil.MemberReference)
        Dim key As String = String.Concat([Namespace], "?", Name)
        If m_TypesByNamespaceAndName.ContainsKey(key) Then
            result = m_TypesByNamespaceAndName(key)
        Else
            result = New Mono.Collections.Generic.Collection(Of Mono.Cecil.MemberReference)
            Helper.FilterByName(TypesByNamespace([Namespace]), Name, result)
            m_TypesByNamespaceAndName.Add(key, result)
        End If
        Return result
    End Function

    ''' <summary>
    ''' All the namespaces available.
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property Namespaces() As Namespaces
        Get
            Return m_Namespaces
        End Get
    End Property

    ''' <summary>
    ''' All the non-nested types that are modules.
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property ModuleTypes() As TypeList
        Get
            Return m_ModuleTypes
        End Get
    End Property

    ReadOnly Property Compiler() As Compiler
        Get
            Return m_Compiler
        End Get
    End Property

    Sub New(ByVal Compiler As Compiler)
        MyBase.New()
        m_Compiler = Compiler
    End Sub


    ''' <summary>
    ''' Searches for the type with the specified name.
    ''' </summary>
    ''' <param name="Name">The type's name to search for. Not case-sensitive.</param>
    ''' <param name="OnlyCreatedTypes">Specifes whether to search in all types, or only in types compiled now.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Overloads Function [GetType](ByVal Name As String, ByVal OnlyCreatedTypes As Boolean) As Generic.List(Of Mono.Cecil.TypeReference)
        Dim result As New Generic.List(Of Mono.Cecil.TypeReference)
        result.AddRange(Me.GetType(Name, Types, OnlyCreatedTypes))
        Return result
    End Function

    Overloads Function [GetType](ByVal Name As String, ByVal InList As IEnumerable, ByVal OnlyCreatedTypes As Boolean) As Generic.List(Of Mono.Cecil.TypeReference)
        Dim result As New Generic.List(Of Mono.Cecil.TypeReference)
        For Each tp As Mono.Cecil.TypeReference In InList
            Dim tpD As Mono.Cecil.TypeDefinition = TryCast(tp, Mono.Cecil.TypeDefinition)
            If OnlyCreatedTypes AndAlso tpD Is Nothing Then Continue For
            If Helper.CompareName(tp.Name, Name) OrElse Helper.CompareName(tp.FullName, Name) Then
#If EXTENDEDDEBUG Then
                Compiler.Report.WriteLine("Found type: " & tp.Name)
#End If
                result.Add(tp)
            Else
#If EXTENDEDDEBUG Then
                Compiler.Report.WriteLine("Discarded type: " & tp.Name)
#End If
            End If
            result.AddRange(Me.GetType(Name, CecilHelper.GetNestedTypes(tp), OnlyCreatedTypes))
        Next
        Return result
    End Function

    ''' <summary>
    ''' Loads all the referenced assemblies.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function LoadReferencedAssemblies() As Boolean
        Dim result As Boolean = True
        Dim refAssembly As Mono.Cecil.AssemblyDefinition
        Dim loadedFiles As New Generic.List(Of String)
        Dim loaded As Boolean
        Dim fullPath As String = Nothing

        For Each strFile As String In Compiler.CommandLine.References
            If loadedFiles.Contains(strFile) Then Continue For
            loadedFiles.Add(strFile)

            refAssembly = LoadAssembly(strFile, fullPath)
            If refAssembly Is Nothing Then
                Compiler.Report.ShowMessage(Messages.VBNC2017, Span.CommandLineSpan, strFile)
                Return False
            End If

            loaded = False
            For a As Integer = 0 To CecilAssemblies.Count - 1
                If Helper.CompareNameOrdinal(CecilAssemblies(a).Name.FullName, refAssembly.Name.FullName) Then
                    loaded = True
                    Exit For
                End If
            Next
            If loaded Then Continue For

            If Compiler.CommandLine.Verbose Then
                Compiler.Report.WriteLine(String.Format("Loaded {0} => {1}", fullPath, refAssembly.Name))
            End If
            m_CecilAssemblies.Add(refAssembly)
            Compiler.AssemblyResolver.RegisterAssembly(refAssembly)
        Next

        Compiler.TypeCache.Init()

        Return result
    End Function

    ''' <summary>
    ''' Loads all the types (referenced and compiled) and all the namespaces as well.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function LoadReferenced() As Boolean
        Dim result As Boolean = True

        result = LoadReferencedAssemblies() AndAlso result
        If result = False Then Return result

        For Each ass As Mono.Cecil.AssemblyDefinition In CecilAssemblies
            If Helper.CompareNameOrdinal(ass.Name.Name, "Microsoft.VisualBasic") Then
                Compiler.TypeCache.InitInternalVB()
                Exit For
            End If
        Next

        result = LoadReferencedTypes() AndAlso result

#If EXTENDEDDEBUG Then
        Compiler.Report.WriteLine(String.Format("{0} assemblies were loaded.", Assemblies.Count.ToString))
        If Compiler.CommandLine.Verbose Then
            For i As Integer = 0 To Assemblies.Count - 1
                Compiler.Report.WriteLine("#" & (i + 1).ToString & ": " & Assemblies(i).FullName & " (location: " & Assemblies(i).Location & ")")
            Next
        End If
        Compiler.Report.WriteLine(String.Format("{0} namespaces were loaded.", Namespaces.Count))
        If Compiler.CommandLine.Verbose Then
            Dim ns As String() = Namespaces.NamespacesAsString
            For i As Integer = 0 To ns.Length - 1
                Compiler.Report.WriteLine("#" & (i + 1).ToString & ": " & ns(i))
            Next
        End If
        Compiler.Report.WriteLine(String.Format("{0} types were loaded.", Types.Count))
        If Compiler.CommandLine.Verbose Then
            For i As Integer = 0 To Types.Count - 1
                'Compiler.Report.WriteLine("#" & (i + 1).ToString & ": " & Types(i).FullName)
            Next
        End If
#End If
        Return result
    End Function

    ''' <summary>
    ''' Tries to load the specified file as an assembly.
    ''' </summary>
    ''' <param name="Filename"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function LoadAssembly(ByVal Filename As String, ByRef FullPath As String) As Mono.Cecil.AssemblyDefinition
        Dim refAss As Mono.Cecil.AssemblyDefinition
        Dim readerParameters As New ReaderParameters(ReadingMode.Deferred)
        readerParameters.AssemblyResolver = Compiler.AssemblyResolver

        FullPath = Nothing

        '  Try
        If IO.File.Exists(Filename) Then
            refAss = Mono.Cecil.AssemblyDefinition.ReadAssembly(Filename, readerParameters)
            FullPath = Filename
            Return refAss
        End If

        '  Catch ex As IO.FileNotFoundException
        For Each strPath As String In Compiler.CommandLine.LibPath
            Dim strFullPath As String = IO.Path.Combine(strPath, Filename)
            Try
                If IO.File.Exists(strFullPath) Then
                    refAss = Mono.Cecil.AssemblyDefinition.ReadAssembly(strFullPath, readerParameters)
                    FullPath = strFullPath
                    Return refAss
                End If
            Catch ex2 As Exception
                'Do nothing, just keep on trying
            End Try
        Next
        '  End Try
        Return Nothing
    End Function

    ''' <summary>
    ''' Load the type into the various lists.
    ''' </summary>
    ''' <param name="Type"></param>
    ''' <remarks></remarks>
    Private Sub LoadType(ByVal Type As Mono.Cecil.TypeDefinition)
        'Add the type to the list of all types.
        Me.Types.Add(Type)

        'Add the namespace to the list of all namespaces.
        Me.Namespaces.AddAllNamespaces(Compiler, Type.Namespace, True)

        'Add the type to the list of types by namespace.
        m_CecilTypesByNamespace.AddType(Type)

        'If it is a module add it to the list of all modules and to the list of modules by namespace.
        If Helper.IsModule(Compiler, Type) Then
            m_CecilModuleTypes.Add(Type)
            m_CecilModulesByNamespace.AddType(Type)
        Else
            Helper.Assert(Type.Annotations(Compiler) Is Nothing OrElse Not TypeOf Type.Annotations(Compiler) Is ModuleDeclaration)
        End If

    End Sub

    ''' <summary>
    ''' Finds all the public non-nested types in the referenced assemblies and loads them into the lists.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function LoadReferencedTypes() As Boolean
        For Each ass As Mono.Cecil.AssemblyDefinition In CecilAssemblies
            Dim types As Mono.Collections.Generic.Collection(Of TypeDefinition) = ass.MainModule.Types
            For i As Integer = 0 To types.Count - 1
                Dim type As TypeDefinition = types(i)
                If Type.IsPublic Then
                    LoadType(Type)
                End If
            Next
        Next

        Return True
    End Function

    ''' <summary>
    ''' Finds all the non-nested types in the compiling code and loads them into the lists.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub LoadCompiledTypes()
        For Each t As TypeDeclaration In Compiler.theAss.Types
            LoadType(t.CecilType)
        Next
    End Sub

    ''' <summary>
    ''' Returns all the modules within the specified namespace.
    ''' Never returns nothing and never throws an exception.
    ''' </summary>
    ''' <param name="Namespace"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetModulesByNamespace(ByVal [Namespace] As String) As TypeDictionary
        Dim result As TypeDictionary = Nothing

        If [Namespace] Is Nothing Then [Namespace] = ""
        If [Namespace].StartsWith("Global.") Then [Namespace] = [Namespace].Substring(7)

        If m_CecilModulesByNamespace.TryGetValue([Namespace], result) Then
            Return result
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Returns all the types within the specified namespace.
    ''' Never returns nothing and never throws an exception.
    ''' </summary>
    ''' <param name="Namespace"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetTypesByNamespace(ByVal [Namespace] As String) As TypeDictionary
        If [Namespace] Is Nothing Then [Namespace] = ""
        If m_CecilTypesByNamespace.ContainsKey([Namespace]) Then
            Return m_CecilTypesByNamespace([Namespace])
        Else
            Return New TypeDictionary()
        End If
    End Function

    Function MakeGenericField(ByVal Parent As ParsedObject, ByVal OpenField As Mono.Cecil.FieldReference, ByVal TypeParameters As Mono.Cecil.TypeReference(), ByVal TypeArguments() As Mono.Cecil.TypeReference, ByVal ClosedType As Mono.Cecil.TypeReference) As Mono.Cecil.FieldReference
        Dim result As Mono.Cecil.FieldReference

        result = New Mono.Cecil.FieldReference(OpenField.Name, OpenField.FieldType, ClosedType)

        Return result
    End Function

    ''' <summary>
    ''' Creates a closed method of an open generic method.
    ''' </summary>
    ''' <param name="Parent"></param>
    ''' <param name="OpenMethod"></param>
    ''' <param name="TypeParameters"></param>
    ''' <param name="TypeArguments"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function MakeGenericMethod(ByVal Parent As ParsedObject, ByVal OpenMethod As Mono.Cecil.MethodReference, ByVal TypeParameters As Mono.Collections.Generic.Collection(Of TypeReference), ByVal TypeArguments As Mono.Collections.Generic.Collection(Of TypeReference)) As Mono.Cecil.MethodReference
        Dim result As Mono.Cecil.MethodReference
        Dim genM As Mono.Cecil.GenericInstanceMethod

        result = CecilHelper.GetCorrectMember(OpenMethod, TypeArguments)

        If OpenMethod.GenericParameters.Count = 0 Then Return result

        Helper.Assert(OpenMethod.GenericParameters.Count = TypeArguments.Count)

        genM = New Mono.Cecil.GenericInstanceMethod(result)
        genM.OriginalMethod = CecilHelper.FindDefinition(OpenMethod)
        For i As Integer = 0 To OpenMethod.GenericParameters.Count - 1
            genM.GenericArguments.Add(Helper.GetTypeOrTypeReference(Parent.Compiler, TypeArguments(i)))
        Next

        Return genM
    End Function

    Function MakeGenericParameter(ByVal Parent As ParsedObject, ByVal OpenParameter As Mono.Cecil.ParameterReference, ByVal ParameterType As Mono.Cecil.TypeReference) As Mono.Cecil.ParameterReference
        Dim result As Mono.Cecil.ParameterReference

        'result = New GenericParameterDescriptor(Parent, ParameterType, OpenParameter)
        result = Nothing : Helper.Stop()

        Return result
    End Function

    Function MakeGenericType(ByVal Parent As ParsedObject, ByVal OpenType As Mono.Cecil.TypeReference, ByVal GenericArguments As Mono.Collections.Generic.Collection(Of TypeReference)) As Mono.Cecil.TypeReference
        Dim result As Mono.Cecil.GenericInstanceType

        result = New Mono.Cecil.GenericInstanceType(Helper.GetTypeOrTypeReference(Parent.Compiler, OpenType))

        For i As Integer = 0 To GenericArguments.Count - 1
            result.GenericArguments.Add(Helper.GetTypeOrTypeReference(Parent.Compiler, GenericArguments(i)))
        Next

        'Needs to add this to a cache, otherwise two otherwise equal types might be created with two different 
        'type instances, which is not good as any type comparison would fail.
        Dim key As String = result.FullName
        Helper.Assert(key IsNot Nothing AndAlso key <> "")
        If m_GenericTypeCache.ContainsKey(key) Then
            'Revert to the cached type if it has already been created.
            result = m_GenericTypeCache(key)
        Else
            Dim addToCache As Boolean = True
            For Each item As Mono.Cecil.TypeReference In GenericArguments
                If CecilHelper.IsGenericParameter(item) Then addToCache = False : Exit For
            Next
            If addToCache Then m_GenericTypeCache.Add(key, result)
        End If

        Return result
    End Function

    Function MakeByRefType(ByVal Parent As ParsedObject, ByVal ElementType As Mono.Cecil.TypeReference) As Mono.Cecil.TypeReference
        Dim result As Mono.Cecil.TypeReference

        result = New ByReferenceType(ElementType)

        Return result
    End Function

    Function MakeArrayType(ByVal Parent As ParsedObject, ByVal ElementType As Mono.Cecil.TypeReference, ByVal Ranks As Integer) As Mono.Cecil.TypeReference
        Dim result As Mono.Cecil.ArrayType

        result = New Mono.Cecil.ArrayType(ElementType, Ranks)

        Return result
    End Function

    ReadOnly Property GenericTypeCache() As Generic.Dictionary(Of String, Mono.Cecil.GenericInstanceType)
        Get
            Return m_GenericTypeCache
        End Get
    End Property

    Class TypeComparer
        Implements Collections.Generic.IEqualityComparer(Of Mono.Cecil.TypeReference)

        Public Function Equals1(ByVal x As Mono.Cecil.TypeReference, ByVal y As Mono.Cecil.TypeReference) As Boolean Implements System.Collections.Generic.IEqualityComparer(Of Mono.Cecil.TypeReference).Equals
            Return Helper.CompareType(x, y)
        End Function

        Public Function GetHashCode1(ByVal obj As Mono.Cecil.TypeReference) As Integer Implements System.Collections.Generic.IEqualityComparer(Of Mono.Cecil.TypeReference).GetHashCode
            Return obj.GetHashCode
        End Function
    End Class
End Class
