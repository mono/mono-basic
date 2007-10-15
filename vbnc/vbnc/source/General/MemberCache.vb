' 
' Visual Basic.Net Compiler
' Copyright (C) 2004 - 2007 Rolf Bjarne Kvinge (RKvinge@novell.com)
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
    [Public] = 1 'MethodAttributes.Public
    [Private] = 2 'MethodAttributes.Private
    [Friend] = 4 'MethodAttributes.Assembly
    [Protected] = 8 'MethodAttributes.Family
    ''' <summary>
    ''' Type inherits from another type in another assembly.
    ''' </summary>
    ''' <remarks></remarks>
    PublicProtected = [Public] Or [Protected]
    ''' <summary>
    ''' Type inherits from another type in the same assembly
    ''' </summary>
    ''' <remarks></remarks>
    PublicProtectedFriend = [Public] Or [Protected] Or [Friend]
    ''' <summary>
    ''' Type references another type in the same assembly
    ''' </summary>
    ''' <remarks></remarks>
    PublicFriend = [Public] Or [Friend]
    ''' <summary>
    ''' Both types are the same.
    ''' </summary>
    ''' <remarks></remarks>
    All = [Public] Or [Friend] Or [Protected] Or [Private]
End Enum

Public Class MemberCache
    Private m_Compiler As Compiler
    Private m_Cache As MemberCacheEntries
    Private m_CacheInsensitive As MemberCacheEntries
    Private m_FlattenedCache As MemberCacheEntries
    Private m_FlattenedCacheInsensitive As MemberCacheEntries

    Private m_Cache2 As MemberVisibilityEntries
    Private m_CacheInsensitive2 As MemberVisibilityEntries
    Private m_FlattenedCache2 As MemberVisibilityEntries
    Private m_FlattenedCacheInsensitive2 As MemberVisibilityEntries

    Private m_ShadowedInterfaceMembers As Generic.List(Of MemberInfo)

    Private m_Type As Type
    Private m_Base As MemberCache

    Sub New(ByVal Compiler As Compiler, ByVal Type As Type)
        m_Compiler = Compiler
        m_Type = Type
        Reload()
        Compiler.TypeManager.MemberCache.Add(Type, Me)
    End Sub

    Public Sub Reload()
        Load()
        Flatten()
    End Sub

    ReadOnly Property Compiler() As Compiler
        Get
            Return m_Compiler
        End Get
    End Property

    ReadOnly Property Cache() As MemberCacheEntries
        Get
            Return m_Cache
        End Get
    End Property

    ReadOnly Property FlattenedCache() As MemberCacheEntries
        Get
            Return m_FlattenedCache
        End Get
    End Property

    ReadOnly Property FlattenedCache2() As MemberVisibilityEntries
        Get
            Return m_FlattenedCache2
        End Get
    End Property

    Sub Load()
        Dim members() As MemberInfo

        Log("Caching type: " & m_Type.Name)
        'If m_Type.Name = "ParameterList" Then Helper.StopIfDebugging()
        members = m_Type.GetMembers(BindingFlags.Instance Or BindingFlags.Static Or BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.DeclaredOnly)

        m_Cache = New MemberCacheEntries()
        m_Cache2 = New MemberVisibilityEntries()

        m_CacheInsensitive = Nothing
        m_CacheInsensitive2 = Nothing
        m_FlattenedCacheInsensitive = Nothing
        m_FlattenedCacheInsensitive2 = Nothing
        m_FlattenedCache = Nothing
        m_FlattenedCache2 = Nothing

        Dim allEntries As MemberCacheEntries = m_Cache
        Dim publicEntries As New MemberCacheEntries()
        Dim publicFriendEntries As New MemberCacheEntries
        Dim publicProtectedEntries As New MemberCacheEntries
        Dim publicProtectedFriendEntries As New MemberCacheEntries

        Dim isDefinedHere As Boolean = Compiler.Assembly.IsDefinedHere(m_Type)

        Dim addTo(MemberVisibility.All) As Boolean
        Dim caches(addTo.Length - 1) As MemberCacheEntries


        caches(MemberVisibility.All) = allEntries
        caches(MemberVisibility.Public) = publicEntries
        caches(MemberVisibility.PublicFriend) = publicfriendEntries
        caches(MemberVisibility.PublicProtectedFriend) = publicprotectedfriendEntries
        caches(MemberVisibility.PublicProtected) = publicprotectedEntries

        m_Cache2.Add(MemberVisibility.All, allEntries)
        m_Cache2.Add(MemberVisibility.PublicProtectedFriend, publicProtectedFriendEntries)
        m_Cache2.Add(MemberVisibility.PublicProtected, publicProtectedEntries)
        m_Cache2.Add(MemberVisibility.PublicFriend, publicFriendEntries)
        m_Cache2.Add(MemberVisibility.Public, publicEntries)

        For Each member As MemberInfo In members
            Dim isPublic, isFriend, isProtected, isPrivate As Boolean

            isPublic = Helper.IsPublic(member)
            isPrivate = Helper.IsPrivate(member)
            isFriend = Helper.IsFriendOrProtectedFriend(member)
            isProtected = Helper.IsProtectedOrProtectedFriend(member)

            If isDefinedHere = False AndAlso isPublic = False AndAlso isProtected = False Then
                'Don't load private and friend members in other assemblies.
                Continue For
            End If

            addTo(MemberVisibility.All) = True
            addTo(MemberVisibility.PublicFriend) = isPublic OrElse isFriend
            addTo(MemberVisibility.PublicProtected) = isPublic OrElse isProtected
            addTo(MemberVisibility.PublicProtectedFriend) = isPublic OrElse isProtected OrElse isFriend
            addTo(MemberVisibility.Public) = isPublic

            For i As Integer = 0 To addTo.Length - 1
                If addTo(i) Then
                    Dim entries As MemberCacheEntries
                    Dim cache As MemberCacheEntry = Nothing
                    entries = caches(i)
                    If entries.TryGetValue(member.Name, cache) Then
                        cache.Members.Add(member)
                    Else
                        entries.Add(New MemberCacheEntry(member))
                    End If
                End If
            Next
        Next
    End Sub

    Sub Flatten()
        Dim base As MemberCache
        base = GetBaseCache()

        If base Is Nothing Then
            If m_Type.IsInterface AndAlso m_Type.IsGenericParameter = False Then
                Dim ifaces() As Type
                Dim icaches() As MemberCache

                ifaces = m_Type.GetInterfaces()

                ReDim icaches(ifaces.Length - 1)
                m_ShadowedInterfaceMembers = New Generic.List(Of MemberInfo)

                For i As Integer = 0 To ifaces.Length - 1
                    icaches(i) = m_Compiler.TypeManager.GetCache(ifaces(i))
                    m_ShadowedInterfaceMembers.AddRange(icaches(i).m_ShadowedInterfaceMembers)
                Next

                For i As Integer = 0 To ifaces.Length - 1
                    FlattenWith(icaches(i))
                Next
                FlattenWith(m_Compiler.TypeManager.GetCache(Compiler.TypeCache.System_Object))
            Else
                m_FlattenedCache = m_Cache
                m_FlattenedCache2 = m_Cache2
            End If

            Return
        End If

        If base.FlattenedCache Is Nothing Then
            m_FlattenedCache = m_Cache
            m_FlattenedCache2 = m_Cache2
            Return
        End If

        FlattenWith(base)
    End Sub

    Private Sub FlattenWith(ByVal MemberCache As MemberCache)
        If m_FlattenedCache Is Nothing Then
            m_FlattenedCache = New MemberCacheEntries(m_Cache)
        End If

        Dim allEntries As MemberCacheEntries = m_FlattenedCache
        Dim publicEntries As MemberCacheEntries
        Dim publicFriendEntries As MemberCacheEntries
        Dim publicProtectedEntries As MemberCacheEntries
        Dim publicProtectedFriendEntries As MemberCacheEntries

        If m_FlattenedCache2 Is Nothing Then
            m_FlattenedCache2 = New MemberVisibilityEntries()
            publicEntries = New MemberCacheEntries(m_Cache2(MemberVisibility.Public))
            publicFriendEntries = New MemberCacheEntries(m_Cache2(MemberVisibility.PublicFriend))
            publicProtectedEntries = New MemberCacheEntries(m_Cache2(MemberVisibility.PublicProtected))
            publicProtectedFriendEntries = New MemberCacheEntries(m_Cache2(MemberVisibility.PublicProtectedFriend))

            m_FlattenedCache2.Add(MemberVisibility.All, allEntries)
            m_FlattenedCache2.Add(MemberVisibility.PublicProtectedFriend, publicProtectedFriendEntries)
            m_FlattenedCache2.Add(MemberVisibility.PublicProtected, publicProtectedEntries)
            m_FlattenedCache2.Add(MemberVisibility.PublicFriend, publicFriendEntries)
            m_FlattenedCache2.Add(MemberVisibility.Public, publicEntries)
        Else
            publicEntries = m_FlattenedCache2(MemberVisibility.Public)
            publicFriendEntries = m_FlattenedCache2(MemberVisibility.PublicFriend)
            publicProtectedEntries = m_FlattenedCache2(MemberVisibility.PublicProtected)
            publicProtectedFriendEntries = m_FlattenedCache2(MemberVisibility.PublicProtectedFriend)
        End If

        Dim isFriendAccessible As Boolean = Compiler.Assembly.IsDefinedHere(m_Type)

        Dim addTo(MemberVisibility.All) As Boolean
        Dim caches(addTo.Length - 1) As MemberCacheEntries

        caches(MemberVisibility.All) = allEntries
        caches(MemberVisibility.Public) = publicEntries
        caches(MemberVisibility.PublicFriend) = publicFriendEntries
        caches(MemberVisibility.PublicProtectedFriend) = publicProtectedFriendEntries
        caches(MemberVisibility.PublicProtected) = publicProtectedEntries

        For Each entry As KeyValuePair(Of MemberVisibility, MemberCacheEntries) In MemberCache.FlattenedCache2
            For Each cache As MemberCacheEntry In entry.Value.Values
                For i As Integer = 0 To cache.Members.Count - 1
                    Dim member As MemberInfo = cache.Members(i)
                    Dim isHidden As Boolean
                    isHidden = False
                    If m_ShadowedInterfaceMembers IsNot Nothing AndAlso m_ShadowedInterfaceMembers.Contains(member) Then
                        isHidden = True
                    ElseIf Me.IsHidden(member, entry.Key) Then
                        isHidden = True
                        If m_ShadowedInterfaceMembers IsNot Nothing Then m_ShadowedInterfaceMembers.Add(member)
                    End If

                    If Not isHidden Then
                        Dim isPublic, isFriend, isProtected, isPrivate As Boolean
                        isPublic = Helper.IsPublic(member)
                        isPrivate = Helper.IsPrivate(member)
                        isFriend = Helper.IsFriendOrProtectedFriend(member)
                        isProtected = Helper.IsProtectedOrProtectedFriend(member)
                        addTo(MemberVisibility.All) = True AndAlso entry.Key = MemberVisibility.All
                        addTo(MemberVisibility.PublicFriend) = (isPublic OrElse isFriend) AndAlso entry.Key = MemberVisibility.PublicFriend
                        addTo(MemberVisibility.PublicProtected) = (isPublic OrElse isProtected) AndAlso entry.Key = MemberVisibility.PublicProtected
                        addTo(MemberVisibility.PublicProtectedFriend) = (isPublic OrElse isProtected OrElse isFriend) AndAlso entry.Key = MemberVisibility.PublicProtectedFriend
                        addTo(MemberVisibility.Public) = isPublic AndAlso entry.Key = MemberVisibility.Public

                        For j As Integer = 0 To addTo.Length - 1
                            If addTo(j) = False Then Continue For
                            Dim entries As MemberCacheEntries = caches(j)
                            If entries.ContainsKey(cache.Name) = False Then
                                entries.Add(New MemberCacheEntry(member))
                            ElseIf entries(cache.Name).Members.Contains(member) = False Then
                                entries(cache.Name).Members.Add(member)
                            End If
                        Next
                    End If
                Next
            Next
        Next
    End Sub

    Private Sub Log(ByVal Msg As String)
        'Compiler.Report.WriteLine(Msg)
    End Sub

    Private Sub LogExtended(ByVal Msg As String)
        Return
        Compiler.Report.WriteLine(Msg)
    End Sub

    Private Function IsHidden(ByVal baseMember As MemberInfo, ByVal Visibility As MemberVisibility) As Boolean
        Dim current As MemberCacheEntry
        Dim memberParameterTypes As Type() = Nothing

        current = Lookup(baseMember.Name, Visibility)

        If current Is Nothing Then
#If DEBUG Then
            LogExtended("MemberCache.IsHidden (false, no current match), type=" & m_Type.Name & ", name=" & baseMember.Name)
#End If
            Return False
        End If

        For i As Integer = 0 To current.Members.Count - 1
            Dim thisMember As MemberInfo = current.Members(i)
            If thisMember.MemberType <> baseMember.MemberType Then
#If DEBUG Then
                LogExtended("MemberCache.IsHidden (true, different member types), type=" & m_Type.Name & ", name=" & thisMember.Name)
#End If
                Return True
            End If

            Select Case thisMember.MemberType
                Case MemberTypes.Constructor, MemberTypes.Event, MemberTypes.Field, MemberTypes.NestedType, MemberTypes.TypeInfo
#If DEBUG Then
                    LogExtended("MemberCache.IsHidden (true, non overloadable member type), type=" & m_Type.Name & ", name=" & thisMember.Name)
#End If
                    Return True
                Case MemberTypes.Property, MemberTypes.Method
                    Dim methodAttributes As MethodAttributes
                    Dim isHideBySig, isVirtual, isNewSlot As Boolean
                    Dim isOverrides As Boolean

                    methodAttributes = Helper.GetMethodAttributes(thisMember)
                    isHideBySig = CBool(methodAttributes And Reflection.MethodAttributes.HideBySig)
                    isVirtual = CBool(methodAttributes And Reflection.MethodAttributes.Virtual)
                    isNewSlot = CBool(methodAttributes And Reflection.MethodAttributes.NewSlot)
                    isOverrides = isVirtual AndAlso isNewSlot = False
                    If isHideBySig = False AndAlso isOverrides = False Then
#If DEBUG Then
                        LogExtended("MemberCache.IsHidden (true, shadowed member), type=" & m_Type.Name & ", name=" & thisMember.Name)
#End If
                        Return True
                    End If
                    If memberParameterTypes Is Nothing Then memberParameterTypes = Helper.GetTypes(Helper.GetParameters(m_Compiler, baseMember))
                    If Helper.CompareTypes(Helper.GetTypes(Helper.GetParameters(m_Compiler, thisMember)), memberParameterTypes) Then
#If DEBUG Then
                        LogExtended("MemberCache.IsHidden (true, exact signature), type=" & m_Type.Name & ", name=" & thisMember.Name)
#End If
                        Return True
                    End If
                Case Else
                    Throw New InternalException("")
            End Select
        Next
#If DEBUG Then
        LogExtended("MemberCache.IsHidden (false, no match at all), type=" & m_Type.Name & ", name=" & baseMember.Name)
#End If
        Return False
    End Function

    Function GetBaseCache() As MemberCache
        If m_Base IsNot Nothing Then Return m_Base

        Dim base As Type
        base = m_Type.BaseType
        If base Is Nothing Then Return Nothing

        If m_Compiler.TypeManager.MemberCache.ContainsKey(base) = False Then
            m_Base = New MemberCache(m_Compiler, base)
        Else
            m_Base = m_Compiler.TypeManager.MemberCache(base)
        End If
        Return m_Base
    End Function

    ''' <summary>
    ''' Looks up the name in the flattened cache.
    ''' Looks case-insensitively
    ''' </summary>
    ''' <param name="Name"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function LookupFlattened(ByVal Name As String, ByVal From As Type) As MemberCacheEntry
        Return LookupFlattened(Name, Helper.GetVisibility(Compiler, From, m_Type))
    End Function

    Function LookupFlattened(ByVal Name As String, ByVal Visibility As MemberVisibility) As MemberCacheEntry
        If m_FlattenedCacheInsensitive2 Is Nothing Then
            m_FlattenedCacheInsensitive2 = New MemberVisibilityEntries()

            For Each item As KeyValuePair(Of MemberVisibility, MemberCacheEntries) In m_FlattenedCache2
                Dim cacheinsensitive As New MemberCacheEntries(item.Value.Count, Helper.StringComparer)
                m_FlattenedCacheInsensitive2.Add(item.Key, cacheinsensitive)
                For Each item2 As KeyValuePair(Of String, MemberCacheEntry) In item.Value
                    Dim current As MemberCacheEntry = Nothing
                    If cacheinsensitive.TryGetValue(item2.Key, current) = False Then
                        current = New MemberCacheEntry(item2.Key)
                        cacheinsensitive.Add(current)
                    End If
                    current.Members.AddRange(item2.Value.Members)
                Next
            Next
        End If

        Dim entries As MemberCacheEntries = Nothing
        Dim value As MemberCacheEntry = Nothing
        If m_FlattenedCacheInsensitive2.TryGetValue(Visibility, entries) Then
            If entries.TryGetValue(Name, value) Then
                Return value
            End If
        End If

        Return Nothing
    End Function
    ''' <summary>
    ''' Looks up the name in the flattened cache.
    ''' Looks case-insensitively
    ''' </summary>
    ''' <param name="Name"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function LookupFlattened(ByVal Name As String) As MemberCacheEntry
        If m_FlattenedCacheInsensitive Is Nothing Then
            m_FlattenedCacheInsensitive = New MemberCacheEntries(m_FlattenedCache.Count, Helper.StringComparer)
            For Each item As KeyValuePair(Of String, MemberCacheEntry) In m_FlattenedCache
                Dim current As MemberCacheEntry = Nothing
                If m_FlattenedCacheInsensitive.TryGetValue(item.Key, current) = False Then
                    current = New MemberCacheEntry(item.Key)
                    m_FlattenedCacheInsensitive.Add(current)
                End If
                current.Members.AddRange(item.Value.Members)
            Next
        End If
        If m_FlattenedCacheInsensitive.ContainsKey(Name) Then
            Return m_FlattenedCacheInsensitive(Name)
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' This function returns the members list in the cache, or nothing
    ''' </summary>
    ''' <param name="Name"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function LookupFlattenedMembers(ByVal Name As String) As Generic.List(Of MemberInfo)
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
    Function LookupMembersFlattened(ByVal Name As String) As Generic.List(Of MemberInfo)
        Dim result As New Generic.List(Of MemberInfo)

        Dim tmp As MemberCacheEntry
        tmp = LookupFlattened(Name)
        If tmp IsNot Nothing Then
            result.AddRange(tmp.Members)
        End If
        Return result
    End Function

    ''' <summary>
    ''' Looks up the name in the cache.
    ''' Looks case-insensitively
    ''' </summary>
    ''' <param name="Name"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Lookup(ByVal Name As String) As MemberCacheEntry
        If m_CacheInsensitive Is Nothing Then
            m_CacheInsensitive = New MemberCacheEntries(Helper.StringComparer)
            For Each item As KeyValuePair(Of String, MemberCacheEntry) In m_Cache
                Dim current As MemberCacheEntry
                If m_CacheInsensitive.ContainsKey(item.Key) = False Then
                    current = New MemberCacheEntry(item.Key)
                    m_CacheInsensitive.Add(current)
                Else
                    current = m_CacheInsensitive(item.Key)
                End If
                current.Members.AddRange(item.Value.Members)
            Next
        End If
        If m_CacheInsensitive.ContainsKey(Name) Then
            Return m_CacheInsensitive(Name)
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Looks up the name in the cache.
    ''' Looks case-insensitively
    ''' </summary>
    ''' <param name="Name"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Lookup(ByVal Name As String, ByVal Visibility As MemberVisibility) As MemberCacheEntry
        If m_CacheInsensitive2 Is Nothing Then
            m_CacheInsensitive2 = New MemberVisibilityEntries()
            For Each item2 As KeyValuePair(Of MemberVisibility, MemberCacheEntries) In m_Cache2
                Dim cache As New MemberCacheEntries(Helper.StringComparer)
                m_CacheInsensitive2.Add(item2.Key, cache)

                For Each item As KeyValuePair(Of String, MemberCacheEntry) In item2.Value
                    Dim current As MemberCacheEntry
                    If cache.ContainsKey(item.Key) = False Then
                        current = New MemberCacheEntry(item.Key)
                        cache.Add(current)
                    Else
                        current = cache(item.Key)
                    End If
                    current.Members.AddRange(item.Value.Members)
                Next
            Next
        End If

        Dim result As MemberCacheEntry = Nothing
        If m_CacheInsensitive2(Visibility).TryGetValue(Name, result) Then
            Return result
        Else
            Return Nothing
        End If

        'If m_CacheInsensitive.ContainsKey(Name) Then
        '    Return m_CacheInsensitive(Name)
        'Else
        '    Return Nothing
        'End If
    End Function

End Class

Public Class MemberVisibilityEntries
    Inherits Generic.Dictionary(Of MemberVisibility, MemberCacheEntries)

End Class

Public Class MemberCacheEntries
    Inherits Generic.Dictionary(Of String, MemberCacheEntry)

    Shadows Sub Add(ByVal Entry As MemberCacheEntry)
        MyBase.Add(Entry.Name, Entry)
    End Sub

    Sub New()

    End Sub

    Sub New(ByVal compare As IEqualityComparer(Of String))
        MyBase.New(compare)
    End Sub

    Sub New(ByVal Capacity As Integer, ByVal compare As IEqualityComparer(Of String))
        MyBase.New(Capacity, compare)
    End Sub

    Sub New(ByVal Dictionary As MemberCacheEntries)
        MyBase.New(Dictionary)
    End Sub

    Function GetAllMembers() As Generic.List(Of MemberInfo)
        Dim result As New Generic.List(Of MemberInfo)
        For Each item As MemberCacheEntry In Me.Values
            result.AddRange(item.Members)
        Next
        Return result
    End Function

End Class

Public Class MemberCacheEntry
    Public Name As String
    Public Members As New Generic.List(Of MemberInfo)

    Sub New(ByVal Name As String)
        Me.Name = Name
    End Sub

    Sub New(ByVal Name As String, ByVal ParamArray Members As MemberInfo())
        Me.Name = Name
        Me.Members.AddRange(Members)
    End Sub

    Sub New(ByVal Member As MemberInfo)
        Me.Name = Member.Name
        Me.Members.Add(Member)
    End Sub
End Class