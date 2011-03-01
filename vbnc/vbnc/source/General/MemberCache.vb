' 
' Visual Basic.Net Compiler
' Copyright (C) 2004 - 2010 Rolf Bjarne Kvinge (RKvinge@novell.com)
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

Imports System.Collections.Generic

Public Enum MemberVisibility
    ''' <summary>
    ''' Type references another type in another assembly
    ''' </summary>
    ''' <remarks></remarks>
    [Public] = 0
    ''' <summary>
    ''' Type inherits from another type in another assembly.
    ''' </summary>
    ''' <remarks></remarks>
    PublicProtected = 1
    ''' <summary>
    ''' Type inherits from another type in the same assembly
    ''' </summary>
    ''' <remarks></remarks>
    PublicProtectedFriend = 2
    ''' <summary>
    ''' Type references another type in the same assembly
    ''' </summary>
    ''' <remarks></remarks>
    PublicFriend = 3
    ''' <summary>
    ''' Both types are the same.
    ''' </summary>
    ''' <remarks></remarks>
    All = 4
End Enum

Public Class MemberCache
    Private m_Compiler As Compiler

    Public m_CacheInsensitive(MemberVisibility.All) As MemberCacheEntries
    Public m_FlattenedCacheInsensitive(MemberVisibility.All) As MemberCacheEntries
    Private m_ShadowedInterfaceMembers As Generic.List(Of Mono.Cecil.MemberReference)
    Private m_Type As Mono.Cecil.TypeReference
    Private m_Types As List(Of Mono.Cecil.TypeReference)
    Private m_Members As List(Of Mono.Collections.Generic.Collection(Of MemberReference))
    Private m_Bases As List(Of MemberCache)
    Private m_LoadedNames(MemberVisibility.All) As System.Collections.Generic.HashSet(Of String)
    Private m_LoadedAll(MemberVisibility.All) As Boolean

    Sub New(ByVal Compiler As Compiler, ByVal Type As Mono.Cecil.TypeReference)
        m_Compiler = Compiler
        m_Type = Type

        Compiler.TypeManager.MemberCache.Add(Type, Me)
        ClearAll()
    End Sub

#If DEBUG Then
    Sub DumpFlattenedCache()
        Console.WriteLine("Cache for: " & m_Type.FullName)
        For i As Integer = 0 To m_FlattenedCacheInsensitive.Length - 1
            Dim entries As MemberCacheEntries = m_FlattenedCacheInsensitive(i)
            Dim access As MemberVisibility = CType(i, MemberVisibility)
            If entries Is Nothing Then
                Console.WriteLine(" Access: " & access.ToString & " has 0 entries.")
            Else
                Console.WriteLine(" Access: " & access.ToString & " has " & entries.Count & " entries.")
            End If
            Dim keys As New Generic.List(Of String)(entries.Keys)
            keys.Sort()
            For Each str As String In keys
                Console.WriteLine("  {0}", str)
                Dim entry As MemberCacheEntry = entries(str)
                For Each member As Mono.Cecil.MemberReference In entry.Members
                    Console.WriteLine("   " & CecilHelper.GetMemberType(member).ToString & ": " & member.DeclaringType.FullName & "." & member.Name & Helper.ToString(Helper.GetParameterTypes(Nothing, member)))
                Next
            Next
        Next
    End Sub
#End If

    Public Sub ClearAll()
        Dim tG As Mono.Cecil.GenericParameter

        If m_Types Is Nothing Then
            m_Types = New List(Of Mono.Cecil.TypeReference)
        Else
            m_Types.Clear()
        End If

        tG = TryCast(m_Type, Mono.Cecil.GenericParameter)
        If tG IsNot Nothing Then
            If tG.Constraints.Count = 0 Then
                m_Types.Add(Compiler.TypeCache.System_Object)
            Else
                m_Types.AddRange(tG.Constraints)
            End If
        Else
            m_Types.Add(m_Type)
        End If

        If m_Members Is Nothing Then
            m_Members = New List(Of Mono.Collections.Generic.Collection(Of MemberReference))
        Else
            m_Members.Clear()
        End If
        For i As Integer = 0 To m_Types.Count - 1
            m_Members.Add(CecilHelper.GetMembers(m_Types(i)))
        Next

        For i As Integer = 0 To MemberVisibility.All - 1
            Clear(CType(i, MemberVisibility))
        Next
        m_Bases = Nothing
    End Sub

    Public Sub Clear(ByVal Visibility As MemberVisibility)
        m_CacheInsensitive(Visibility) = Nothing
        m_FlattenedCacheInsensitive(Visibility) = Nothing
        m_LoadedNames(Visibility) = Nothing
        m_LoadedAll(Visibility) = False
        m_ShadowedInterfaceMembers = Nothing
    End Sub

    ReadOnly Property Type() As Mono.Cecil.TypeReference
        Get
            Return m_Type
        End Get
    End Property

    ReadOnly Property Compiler() As Compiler
        Get
            Return m_Compiler
        End Get
    End Property

    Public Function GetAllMembers() As Mono.Collections.Generic.Collection(Of Mono.Cecil.MemberReference)
        Return m_Members(0)
    End Function

    Public Function GetAllFlattenedMembers(ByVal Visibility As MemberVisibility) As Generic.List(Of Mono.Cecil.MemberReference)
        If m_LoadedAll(Visibility) = False Then Load(Nothing, Visibility)
        Return m_FlattenedCacheInsensitive(Visibility).GetAllMembers()
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Name">Load every member if Name is nothing</param>
    ''' <remarks></remarks>
    Private Sub Load(ByVal Name As String, ByVal Visibility As MemberVisibility)
        If m_LoadedAll(Visibility) Then
            Return
        ElseIf m_LoadedNames(Visibility) Is Nothing Then
            m_LoadedNames(Visibility) = New HashSet(Of String)(Helper.StringComparer)
        ElseIf Name IsNot Nothing AndAlso m_LoadedNames(Visibility).Contains(Name) Then
            Return
        End If

        If Name Is Nothing Then Clear(Visibility)

        If Name IsNot Nothing Then m_LoadedNames(Visibility).Add(Name)

        For i As Integer = 0 To m_Types.Count - 1
            Load(m_Types(i), m_Members(i), Name, Visibility)
        Next

        Flatten(Name, Visibility)

        If Name Is Nothing Then m_LoadedAll(Visibility) = True
    End Sub

    Private Sub Load(ByVal Type As Mono.Cecil.TypeReference, ByVal Members As Mono.Collections.Generic.Collection(Of MemberReference), ByVal Name As String, ByVal Visibility As MemberVisibility)
        Dim entries As MemberCacheEntries = Nothing
        Dim isDefinedHere As Boolean = Compiler.Assembly.IsDefinedHere(Type)
        Dim addTo As Boolean

        entries = m_CacheInsensitive(Visibility)
        If entries Is Nothing Then
            entries = New MemberCacheEntries()
            m_CacheInsensitive(Visibility) = entries
        End If

        For m As Integer = 0 To Members.Count - 1
            Dim member As Mono.Cecil.MemberReference = Members(m)
            Dim cache As MemberCacheEntry = Nothing
            Dim isPublic, isFriend, isProtected, isPrivate As Boolean

            If Name IsNot Nothing AndAlso Not Helper.CompareName(Name, member.Name) Then
                Continue For
            End If

            isPublic = Helper.IsPublic(member)
            isPrivate = Helper.IsPrivate(member)
            isFriend = Helper.IsFriendOrProtectedFriend(member)
            isProtected = Helper.IsProtectedOrProtectedFriend(member)

            If isDefinedHere = False AndAlso isPublic = False AndAlso isProtected = False Then
                'Don't load private and friend members in other assemblies.
                Continue For
            End If

            addTo = False
            Select Case Visibility
                Case MemberVisibility.All
                    addTo = True
                Case MemberVisibility.Public
                    addTo = isPublic
                Case MemberVisibility.PublicFriend
                    addTo = isPublic OrElse isFriend
                Case MemberVisibility.PublicProtected
                    addTo = isPublic OrElse isProtected
                Case MemberVisibility.PublicProtectedFriend
                    addTo = isPublic OrElse isProtected OrElse isFriend
            End Select

            If addTo = False Then
                Continue For
            End If

            If entries.TryGetValue(member.Name, cache) Then
                cache.Members.Add(member)
            Else
                entries.Add(New MemberCacheEntry(member))
            End If
        Next
    End Sub

    Private Sub Flatten(ByVal Name As String, ByVal Visibility As MemberVisibility)
        Dim bases As List(Of MemberCache) = GetBaseCache()

        If bases.Count = 0 Then
            Flatten(Name, Nothing, Visibility)
        Else
            For i As Integer = 0 To bases.Count - 1
                Flatten(Name, bases(i), Visibility)
            Next
        End If
    End Sub

    Private Sub Flatten(ByVal Name As String, ByVal base As MemberCache, ByVal Visibility As MemberVisibility)
        If base Is Nothing Then
            If Helper.IsInterface(Compiler, m_Type) AndAlso CecilHelper.IsGenericParameter(m_Type) = False Then
                Dim ifaces As Mono.Collections.Generic.Collection(Of TypeReference)
                Dim icaches() As MemberCache

                ifaces = CecilHelper.GetInterfaces(m_Type, True)

                ReDim icaches(ifaces.Count - 1)
                m_ShadowedInterfaceMembers = New Generic.List(Of Mono.Cecil.MemberReference)

                For i As Integer = 0 To ifaces.Count - 1
                    icaches(i) = m_Compiler.TypeManager.GetCache(ifaces(i))
                    icaches(i).Load(Nothing, Visibility)
                    m_ShadowedInterfaceMembers.AddRange(icaches(i).m_ShadowedInterfaceMembers)
                Next

                For i As Integer = 0 To ifaces.Count - 1
                    FlattenWith(Name, icaches(i), Visibility)
                Next
                Dim system_object As MemberCache = m_Compiler.TypeManager.GetCache(Compiler.TypeCache.System_Object)
                system_object.Load(Name, Visibility)
                FlattenWith(Name, system_object, Visibility)
            Else
                m_FlattenedCacheInsensitive = m_CacheInsensitive
            End If

            Return
        End If

        base.Load(Name, Visibility)

        FlattenWith(Name, base, Visibility)
    End Sub

    Private Shared Sub AddToFlattenedCache(ByVal FlattenedCache As MemberCacheEntries, ByVal Name As String, ByVal MemberCache As MemberCache, ByVal Cache As MemberCacheEntries, ByVal Visibility As MemberVisibility)
        Dim cache_entry As MemberCacheEntry = Nothing
        If Name Is Nothing Then
            For Each obj As KeyValuePair(Of String, MemberCacheEntry) In Cache
                If Not FlattenedCache.TryGetValue(obj.Key, cache_entry) Then
                    cache_entry = New MemberCacheEntry(obj.Value.Name)
                    FlattenedCache.Add(cache_entry)
                End If
                For i As Integer = 0 To obj.Value.Members.Count - 1
                    Dim m As MemberReference = obj.Value.Members(i)
                    If cache_entry.Members.Contains(m) = False Then
                        cache_entry.Members.Add(m)
                    End If
                Next
            Next
        Else
            Dim value As MemberCacheEntry = Nothing
            If Not Cache.TryGetValue(Name, value) Then Return
            If Not FlattenedCache.TryGetValue(Name, cache_entry) Then
                cache_entry = New MemberCacheEntry(Name)
                FlattenedCache.Add(cache_entry)
            End If
            For i As Integer = 0 To value.Members.Count - 1
                Dim m As MemberReference = value.Members(i)
                If cache_entry.Members.Contains(m) = False Then
                    cache_entry.Members.Add(m)
                End If
            Next
        End If
    End Sub

    Private Sub AddToCache(ByVal cache As MemberCacheEntry, ByVal Visibility As MemberVisibility, ByVal cache_entries As MemberCacheEntries)
        For i As Integer = 0 To cache.Members.Count - 1
            Dim member As Mono.Cecil.MemberReference = cache.Members(i)
            Dim isPublic, isFriend, isProtected, isPrivate As Boolean
            Dim isHidden As Boolean
            Dim cacheentry As MemberCacheEntry = Nothing
            Dim method As Mono.Cecil.MethodReference

            isHidden = False
            If m_ShadowedInterfaceMembers IsNot Nothing AndAlso m_ShadowedInterfaceMembers.Contains(member) Then
                isHidden = True
            ElseIf Me.IsHidden(member, Visibility, cache.Members) Then
                isHidden = True
                If m_ShadowedInterfaceMembers IsNot Nothing Then m_ShadowedInterfaceMembers.Add(member)
            End If

            If isHidden Then Continue For

            isPublic = Helper.IsPublic(member)
            isPrivate = Helper.IsPrivate(member)
            isFriend = Helper.IsFriendOrProtectedFriend(member)
            isProtected = Helper.IsProtectedOrProtectedFriend(member)

            isHidden = True
            Select Case Visibility
                Case MemberVisibility.All
                    isHidden = False
                Case MemberVisibility.Public
                    isHidden = Not (isPublic)
                Case MemberVisibility.PublicFriend
                    isHidden = Not (isPublic OrElse isFriend)
                Case MemberVisibility.PublicProtected
                    isHidden = Not (isPublic OrElse isProtected)
                Case MemberVisibility.PublicProtectedFriend
                    isHidden = Not (isPublic OrElse isProtected OrElse isFriend)
            End Select

            If isHidden Then Continue For

            If cache_entries.TryGetValue(cache.Name, cacheentry) = False Then
                cache_entries.Add(New MemberCacheEntry(member))
            ElseIf cacheentry.Members.Contains(member) = False Then
                Dim found As Boolean = False
                For k As Integer = 0 To cacheentry.Members.Count - 1
                    If cacheentry.Members(k) Is member Then
                        found = True
                        Exit For
                    End If
                Next

                method = TryCast(member, Mono.Cecil.MethodReference)
                If Not found AndAlso method IsNot Nothing Then
                    For k As Integer = 0 To cacheentry.Members.Count - 1
                        If Helper.CompareMethod(TryCast(cacheentry.Members(k), Mono.Cecil.MethodReference), method) Then
                            found = True
                            Exit For
                        End If
                    Next
                End If
                If Not found Then
                    cacheentry.Members.Add(member)
                End If
            End If
        Next
    End Sub

    Private Sub FlattenWith(ByVal Name As String, ByVal MemberCache As MemberCache, ByVal Visibility As MemberVisibility)
        Dim cache_entries As MemberCacheEntries = Nothing

        'Console.WriteLine("{0} FlattenWith: ({1}, {2})", m_Type.FullName, Name, MemberCache.Type.FullName)

        cache_entries = m_FlattenedCacheInsensitive(Visibility)
        If cache_entries Is Nothing Then
            cache_entries = New MemberCacheEntries()
            m_FlattenedCacheInsensitive(Visibility) = cache_entries
        End If

        AddToFlattenedCache(cache_entries, Name, MemberCache, m_CacheInsensitive(Visibility), Visibility)

        Dim cache2 As MemberCacheEntries
        cache2 = MemberCache.m_FlattenedCacheInsensitive(Visibility)

        If Name Is Nothing Then
            For Each cache As MemberCacheEntry In cache2.Values
                AddToCache(cache, Visibility, cache_entries)
            Next
        Else
            Dim entry As MemberCacheEntry = Nothing
            If Not cache2.TryGetValue(Name, entry) Then Return
            AddToCache(cache2(Name), Visibility, cache_entries)
        End If
    End Sub

    Private Function IsHidden(ByVal baseMember As Mono.Cecil.MemberReference, ByVal Visibility As MemberVisibility, ByVal exclude As Mono.Collections.Generic.Collection(Of MemberReference)) As Boolean
        Dim current As MemberCacheEntry
        Dim memberParameterTypes As Mono.Cecil.TypeReference() = Nothing

        current = Lookup(baseMember.Name, Visibility, True)

        If current Is Nothing Then
            Return False
        End If

        For i As Integer = 0 To current.Members.Count - 1
            Dim thisMember As Mono.Cecil.MemberReference = current.Members(i)
            If CecilHelper.GetMemberType(thisMember) <> CecilHelper.GetMemberType(baseMember) Then
                Return True
            End If

            If exclude.Contains(thisMember) Then Continue For

            Select Case CecilHelper.GetMemberType(thisMember)
                Case MemberTypes.Constructor, MemberTypes.Event, MemberTypes.Field, MemberTypes.NestedType, MemberTypes.TypeInfo
                    Return True
                Case MemberTypes.Property, MemberTypes.Method
                    Dim methodAttributes As Mono.Cecil.MethodAttributes
                    Dim isHideBySig, isVirtual, isNewSlot As Boolean
                    Dim isOverrides As Boolean

                    methodAttributes = Helper.GetMethodAttributes(thisMember)
                    isHideBySig = CBool(methodAttributes And Reflection.MethodAttributes.HideBySig)
                    isVirtual = CBool(methodAttributes And Reflection.MethodAttributes.Virtual)
                    isNewSlot = CBool(methodAttributes And Reflection.MethodAttributes.NewSlot)
                    isOverrides = isVirtual AndAlso isNewSlot = False
                    If isHideBySig = False AndAlso isOverrides = False Then
                        Return True
                    End If
                    If memberParameterTypes Is Nothing Then memberParameterTypes = Helper.GetTypes(Helper.GetParameters(m_Compiler, baseMember))
                    If Helper.CompareTypes(Helper.GetTypes(Helper.GetParameters(m_Compiler, thisMember)), memberParameterTypes) Then
                        Return True
                    End If
                Case Else
                    Throw New InternalException("")
            End Select
        Next
        Return False
    End Function

    Private Function GetBaseCache() As List(Of MemberCache)
        Dim base As Mono.Cecil.TypeReference
        Dim cache As MemberCache

        If m_Bases IsNot Nothing Then Return m_Bases

        m_Bases = New List(Of MemberCache)

        For i As Integer = 0 To m_Types.Count - 1
            base = CecilHelper.FindDefinition(m_Types(i)).BaseType

            If base Is Nothing Then Continue For

            base = CecilHelper.InflateType(base, m_Type)

            If m_Compiler.TypeManager.MemberCache.ContainsKey(base) = False Then
                cache = New MemberCache(m_Compiler, base)
            Else
                cache = m_Compiler.TypeManager.MemberCache(base)
            End If
            m_Bases.Add(cache)
        Next

        Return m_Bases
    End Function

    ''' <summary>
    ''' Looks up the name in the flattened cache.
    ''' Looks case-insensitively
    ''' </summary>
    ''' <param name="Name"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LookupFlattened(ByVal Name As String, ByVal From As Mono.Cecil.TypeReference) As MemberCacheEntry
        Return LookupFlattened(Name, Helper.GetVisibility(Compiler, From, m_Type))
    End Function

    Public Function LookupFlattened(ByVal Name As String, ByVal Visibility As MemberVisibility) As MemberCacheEntry
        Dim result As MemberCacheEntry = Nothing

        Load(Name, Visibility)

        m_FlattenedCacheInsensitive(Visibility).TryGetValue(Name, result)
        Return result
    End Function

    ''' <summary>
    ''' Looks up the name in the flattened cache.
    ''' Looks case-insensitively
    ''' </summary>
    ''' <param name="Name"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LookupFlattened(ByVal Name As String) As MemberCacheEntry
        Return LookupFlattened(Name, MemberVisibility.All)
    End Function

    ''' <summary>
    ''' Looks up the name in the cache.
    ''' Looks case-insensitively
    ''' </summary>
    ''' <param name="Name"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Lookup(ByVal Name As String, ByVal Visibility As MemberVisibility) As MemberCacheEntry
        Return Lookup(Name, Visibility, False)
    End Function

    ''' <summary>
    ''' Looks up the name in the cache.
    ''' Looks case-insensitively
    ''' </summary>
    ''' <param name="Name"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Lookup(ByVal Name As String, ByVal Visibility As MemberVisibility, ByVal PreventLoad As Boolean) As MemberCacheEntry
        Dim cache_insensitive As MemberCacheEntries = Nothing
        Dim result As MemberCacheEntry = Nothing

        If Not PreventLoad Then Load(Name, Visibility)

        m_CacheInsensitive(Visibility).TryGetValue(Name, result)
        Return result
    End Function

    ''' <summary>
    ''' This function returns the members list in the cache, or nothing
    ''' </summary>
    ''' <param name="Name"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LookupFlattenedMembers(ByVal Name As String) As Mono.Collections.Generic.Collection(Of Mono.Cecil.MemberReference)
        Dim cache As MemberCacheEntry = LookupFlattened(Name)
        If cache Is Nothing Then Return Nothing
        Return cache.Members
    End Function

    ''' <summary>
    ''' This function returns a COPY of the members list in the cache.
    ''' To be avoided if possible.
    ''' </summary>
    ''' <param name="Name"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LookupMembersFlattened(ByVal Name As String) As Mono.Collections.Generic.Collection(Of Mono.Cecil.MemberReference)
        Dim result As New Mono.Collections.Generic.Collection(Of Mono.Cecil.MemberReference)
        Dim tmp As MemberCacheEntry

        tmp = LookupFlattened(Name)
        If tmp IsNot Nothing Then
            result.AddRange(tmp.Members)
        End If

        Return result
    End Function

End Class

Public Class MemberCacheEntries
    Inherits Generic.Dictionary(Of String, MemberCacheEntry)

    Overloads Sub Add(ByVal Entry As MemberCacheEntry)
        MyBase.Add(Entry.Name, Entry)
    End Sub

    Sub New()
        MyBase.New(Helper.StringComparer)
    End Sub

    Function GetAllMembers() As Generic.List(Of Mono.Cecil.MemberReference)
        Dim result As New Generic.List(Of Mono.Cecil.MemberReference)
        For Each item As MemberCacheEntry In Me.Values
            result.AddRange(item.Members)
        Next
        Return result
    End Function

End Class

Public Class MemberCacheEntry
    Public Name As String
    Public Members As New Mono.Collections.Generic.Collection(Of Mono.Cecil.MemberReference)

    Sub New(ByVal Name As String)
        Me.Name = Name
    End Sub

    Sub New(ByVal Member As Mono.Cecil.MemberReference)
        Me.Name = Member.Name
        Me.Members.Add(Member)
    End Sub
End Class