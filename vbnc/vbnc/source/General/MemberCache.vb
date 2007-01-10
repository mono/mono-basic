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

Public Class MemberCache
    Private m_Compiler As Compiler
    Private m_Cache As New MemberCacheEntries
    Private m_CacheInsensitive As MemberCacheEntries
    Private m_FlattenedCache As MemberCacheEntries
    Private m_FlattenedCacheInsensitive As MemberCacheEntries
    Private m_Type As Type
    Private m_Base As MemberCache

    Sub New(ByVal Compiler As Compiler, ByVal Type As Type)
        m_Compiler = Compiler
        m_Type = Type
        Load()
        Flatten()
        Compiler.TypeManager.MemberCache.Add(Type, Me)
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

    Sub Load()
        Dim members() As MemberInfo

        Log("Caching type: " & m_Type.Name)
        'If m_Type.Name = "ParameterList" Then Helper.StopIfDebugging()
        members = m_Type.GetMembers(BindingFlags.Instance Or BindingFlags.Static Or BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.DeclaredOnly)

        For Each member As MemberInfo In members
            If m_Cache.ContainsKey(member.Name) = False Then
                m_Cache.Add(New MemberCacheEntry(member))
            Else
                m_Cache(member.Name).Members.Add(member)
            End If
        Next
    End Sub

    Sub Flatten()
        Dim base As MemberCache
        base = GetBaseCache()

        If base Is Nothing Then

            If m_Type.IsInterface AndAlso m_Type.IsGenericParameter = False Then
                Dim ifaces() As Type
                Dim icache As MemberCache
                ifaces = m_Type.GetInterfaces()
                For Each iface As Type In ifaces
                    icache = m_Compiler.TypeManager.GetCache(iface)
                    FlattenWith(icache)
                Next
                FlattenWith(m_Compiler.TypeManager.GetCache(Compiler.TypeCache.Object))
            Else
                m_FlattenedCache = m_Cache
            End If

            Return
        End If

        If base.FlattenedCache Is Nothing Then
            m_FlattenedCache = m_Cache
            Return
        End If

        FlattenWith(base)
    End Sub

    Private Sub FlattenWith(ByVal MemberCache As MemberCache)
        If m_FlattenedCache Is Nothing Then
            m_FlattenedCache = New MemberCacheEntries(m_Cache)
        End If
        For Each cache As MemberCacheEntry In MemberCache.FlattenedCache.Values
            For Each member As MemberInfo In cache.Members
                If Not IsHidden(member) Then
                    If m_FlattenedCache.ContainsKey(cache.Name) = False Then
                        m_FlattenedCache.Add(New MemberCacheEntry(member))
                    ElseIf m_FlattenedCache(cache.Name).Members.Contains(member) = False Then
                        m_FlattenedCache(cache.Name).Members.Add(member)
                    End If
                End If
            Next
        Next
    End Sub

    Private Sub Log(ByVal Msg As String)
        'Compiler.Report.WriteLine(Msg)
    End Sub

    Private Sub LogExtended(ByVal Msg As String)
        'If False OrElse m_Type.Name = "Form" OrElse m_Type.Name = "Control" Then
        '    Compiler.Report.WriteLine(Msg)
        'End If
    End Sub

    Function IsHidden(ByVal member As MemberInfo) As Boolean
        Dim current As MemberCacheEntry
        Dim memberParameterTypes As Type() = Nothing

        current = Lookup(member.Name)

        If current Is Nothing Then
#If DEBUG Then
            LogExtended("MemberCache.IsHidden (false, no current match), type=" & m_Type.Name & ", name=" & member.Name)
#End If
            Return False
        End If

        For Each m As MemberInfo In current.Members
            If m.MemberType <> member.MemberType Then
#If DEBUG Then
                LogExtended("MemberCache.IsHidden (true, different member types), type=" & m_Type.Name & ", name=" & m.Name)
#End If
                Return True
            End If

            Select Case m.MemberType
                Case MemberTypes.Constructor, MemberTypes.Event, MemberTypes.Field, MemberTypes.NestedType, MemberTypes.TypeInfo
#If DEBUG Then
                    LogExtended("MemberCache.IsHidden (true, non overloadable member type), type=" & m_Type.Name & ", name=" & m.Name)
#End If
                    Return True
                Case MemberTypes.Property, MemberTypes.Method
                    If CBool(Helper.GetMethodAttributes(m) And MethodAttributes.HideBySig) = False Then
#If DEBUG Then
                        LogExtended("MemberCache.IsHidden (true, shadowed member), type=" & m_Type.Name & ", name=" & m.Name)
#End If
                        Return True
                    End If
                    If memberParameterTypes Is Nothing Then memberParameterTypes = Helper.GetTypes(Helper.GetParameters(m_Compiler, member))
                    If Helper.CompareTypes(Helper.GetTypes(Helper.GetParameters(m_Compiler, m)), memberParameterTypes) Then
#If DEBUG Then
                        LogExtended("MemberCache.IsHidden (true, exact signature), type=" & m_Type.Name & ", name=" & m.Name)
#End If
                        Return True
                    End If
                Case Else
                    Throw New InternalException("")
            End Select
        Next
#If DEBUG Then
        LogExtended("MemberCache.IsHidden (false, no match at all), type=" & m_Type.Name & ", name=" & member.Name)
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
    Function LookupFlattened(ByVal Name As String) As MemberCacheEntry
        If m_FlattenedCacheInsensitive Is Nothing Then
            m_FlattenedCacheInsensitive = New MemberCacheEntries(StringComparer.InvariantCultureIgnoreCase)
            For Each item As KeyValuePair(Of String, MemberCacheEntry) In m_FlattenedCache
                Dim current As MemberCacheEntry
                If m_FlattenedCacheInsensitive.ContainsKey(item.Key) = False Then
                    current = New MemberCacheEntry(item.Key)
                    m_FlattenedCacheInsensitive.Add(current)
                Else
                    current = m_FlattenedCacheInsensitive(item.Key)
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

    Function LookupMembersFlattened(ByVal Name As String) As Generic.List(Of MemberInfo)
        Dim result As New Generic.List(Of MemberInfo)

        Dim tmp As MemberCacheEntry
        tmp = LookupFlattened(Name)
        If tmp IsNot Nothing Then
            result.AddRange(tmp.members)
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
            m_CacheInsensitive = New MemberCacheEntries(StringComparer.InvariantCultureIgnoreCase)
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