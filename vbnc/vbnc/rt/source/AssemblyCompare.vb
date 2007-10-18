' 
' Visual Basic.Net COmpiler
' Copyright (C) 2004 - 2006 Rolf Bjarne Kvinge, rbjarnek at users.sourceforge.net
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

Imports System.Reflection
Imports System.Reflection.Emit

Public Class AssemblyCompare

    Private m_Assembly1 As String
    Private m_Assembly2 As String

    Private a1, a2 As Reflection.Assembly
    Private types1, types2 As Generic.List(Of Type)

    Private m_Differences As New Generic.List(Of String)

    Shared Function test() As String
        Dim a As New AssemblyCompare
        a.Compare("M:\Rolf\Proyectos\VB.NET\Compilers\vbnc\working trunk\trunk\public\vbnc\tests\CompileTime\testoutput\Class1_vbc.dll", "M:\Rolf\Proyectos\VB.NET\Compilers\vbnc\working trunk\trunk\public\vbnc\tests\CompileTime\testoutput\Class1.dll")
        Return a.Diff
    End Function

    ReadOnly Property Diff() As String
        Get
            Return Microsoft.VisualBasic.Join(m_Differences.ToArray, Microsoft.VisualBasic.vbNewLine)
        End Get
    End Property

    Public Sub Compare(ByVal Assembly1 As String, ByVal Assembly2 As String)
        Me.m_Assembly1 = Assembly1
        Me.m_Assembly2 = Assembly2
        Compare()
    End Sub

    Private Sub CompareMember(ByVal m1 As MemberInfo, ByVal m2 As MemberInfo)
        Debug.Assert(m1.MemberType = m2.MemberType)
        Select Case m1.MemberType
            Case MemberTypes.Constructor
                CompareConstructor(CType(m1, ConstructorInfo), CType(m2, ConstructorInfo))
            Case MemberTypes.Event
                CompareEvent(CType(m1, EventInfo), CType(m2, EventInfo))
            Case MemberTypes.Field
                CompareField(CType(m1, FieldInfo), CType(m2, FieldInfo))
            Case MemberTypes.Method
                CompareMethod(CType(m1, MethodInfo), CType(m2, MethodInfo))
            Case MemberTypes.NestedType, MemberTypes.TypeInfo
                CompareType(CType(m1, Type), CType(m2, Type))
            Case MemberTypes.Property
                CompareProperty(CType(m1, PropertyInfo), CType(m2, PropertyInfo))
            Case Else
                Stop
        End Select
    End Sub

    Private Function IsSameMember(ByVal m1 As MemberInfo, ByVal m2 As MemberInfo) As Boolean
        If m1.MemberType <> m2.MemberType Then Return False
        If m1.Name.Equals(m2.Name, StringComparison.OrdinalIgnoreCase) Then Return False
        Return True
    End Function

    Private Sub CompareEvent(ByVal e1 As EventInfo, ByVal e2 As EventInfo)

    End Sub

    Private Sub CompareConstructor(ByVal c1 As ConstructorInfo, ByVal c2 As ConstructorInfo)

    End Sub

    Private Sub CompareMethod(ByVal m1 As MethodInfo, ByVal m2 As MethodInfo)

    End Sub

    Private Sub CompareProperty(ByVal p1 As PropertyInfo, ByVal p2 As PropertyInfo)

    End Sub

    Private Sub CompareField(ByVal f1 As FieldInfo, ByVal f2 As FieldInfo)

    End Sub

    Private Function IsSameType(ByVal t1 As Type, ByVal t2 As Type) As Boolean
        Return t1.FullName.Equals(t2.FullName, StringComparison.OrdinalIgnoreCase)
    End Function

    Private Sub CompareType(ByVal t1 As Type, ByVal t2 As Type)
        If t1.Attributes <> t2.Attributes Then
            WriteDiff("t1 has attributes: " & t1.Attributes.ToString & " while t2 has attributes: " & t2.Attributes.ToString)
        End If
        If t1.BaseType.FullName.Equals(t2.FullName, StringComparison.OrdinalIgnoreCase) = False Then
            WriteDiff("t1.BaseType: " & t1.BaseType.FullName & " t2.BaseType: " & t2.BaseType.FullName)
        End If

        Dim members1, members2 As Generic.List(Of MemberInfo)
        Dim flags As BindingFlags = BindingFlags.CreateInstance Or BindingFlags.DeclaredOnly Or BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.Public Or BindingFlags.Static
        members1 = New Generic.List(Of MemberInfo)(t1.GetMembers(flags))
        members2 = New Generic.List(Of MemberInfo)(t2.GetMembers(flags))

        CheckList(Of MemberInfo)(members1, members2, New MemberComparer(Of MemberInfo)(AddressOf CompareMember), New IsSameMemberComparer(Of MemberInfo)(AddressOf IsSameMember))

    End Sub

    Delegate Sub MemberComparer(Of T)(ByVal m1 As T, ByVal m2 As T)
    Delegate Function IsSameMemberComparer(Of T)(ByVal m1 As T, ByVal m2 As T) As Boolean

    Private Sub CheckList(Of T As MemberInfo)(ByVal list1 As Generic.List(Of T), ByVal list2 As Generic.List(Of T), ByVal comparer As MemberComparer(Of T), ByVal IsSameMember As IsSameMemberComparer(Of T))

        Do While list1.Count > 0
            Dim t1, t2 As T
            t1 = list1(0)
            list1.RemoveAt(0)
            t2 = Nothing
            For i As Integer = 0 To list2.Count - 1
                If IsSameMember(t1, list2(i)) = False Then
                    t2 = list2(i)
                    list2.RemoveAt(i)
                End If
            Next
            If t2 IsNot Nothing Then
                comparer(t1, t2)
            Else
                WriteDiffMemberNotFound(t1, 2)
            End If
        Loop
        For Each t1 As T In list2
            WriteDiffMemberNotFound(t1, 1)
        Next
    End Sub

    Private Sub Compare()
        a1 = Reflection.Assembly.LoadFile(m_Assembly1)
        a2 = Reflection.Assembly.LoadFile(m_Assembly2)

        CompareMethod(a1.EntryPoint, a2.EntryPoint)

        types1 = New Generic.List(Of Type)(a1.GetTypes())
        types2 = New Generic.List(Of Type)(a2.GetTypes())

        CheckList(Of Type)(types1, types2, New MemberComparer(Of Type)(AddressOf CompareType), New IsSameMemberComparer(Of Type)(AddressOf IsSameType))

    End Sub

    Private Sub WriteDiffMemberNotFound(ByVal t As MemberInfo, ByVal AssemblyNumber As Integer)
        If t.MemberType = MemberTypes.TypeInfo Then
            WriteDiffTypeNotFound(DirectCast(t, Type), AssemblyNumber)
        Else
            WriteDiff("Member " & t.Name & " of type " & t.DeclaringType.FullName & " was not found in assembly #" & AssemblyNumber.ToString)
        End If
    End Sub

    Private Sub WriteDiffTypeNotFound(ByVal t As Type, ByVal AssemblyNumber As Integer)
        If t.FullName.StartsWith("My.My", StringComparison.Ordinal) = False Then
            WriteDiff("Type " & t.FullName & " was not found in assembly #" & AssemblyNumber.ToString)
        End If
    End Sub

    Private Sub WriteDiff(ByVal message As String)
        m_Differences.Add(message)
    End Sub
End Class
