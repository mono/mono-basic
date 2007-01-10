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

Public Class Modifiers
    Inherits ParsedObject

    Public Shared ReadOnly EmptySet As Modifiers = New Modifiers(Nothing)

    Private m_ValidModifiers() As KS
    'Private m_Modifiers As Generic.List(Of KS)
    Private m_Modifiers2(10) As KS
    Private m_Count As Integer
    ReadOnly Property Count() As Integer
        Get
            For i As Integer = 0 To 10
                If m_Modifiers2(i) = KS.None Then
                    Return i
                End If
            Next
            Helper.Stop()
            Return m_Count
            'If m_Modifiers Is Nothing Then Return 0
            'Return m_Modifiers.Count
        End Get
    End Property

    '<Obsolete()> ReadOnly Property Modifiers() As Generic.List(Of KS)
    '    Get
    '        'Return m_Modifiers
    '    End Get
    'End Property

    Sub New(ByVal Parent As ParsedObject, ByVal Modifiers As Generic.List(Of KS))
        MyBase.New(Parent)
        If Modifiers IsNot Nothing Then
            For i As Integer = 0 To Math.Min(10, Modifiers.Count - 1)
                m_Modifiers2(i) = Modifiers(i)
            Next
        End If
        'If Modifiers IsNot Nothing Then
        '    m_Modifiers = Modifiers
        'Else
        '    m_Modifiers = New Generic.List(Of KS)
        'End If
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal ParamArray Modifiers As KS())
        MyBase.New(Parent)
        'm_Modifiers = New Generic.List(Of KS)(Modifiers)
        If Modifiers IsNot Nothing Then
            Modifiers.CopyTo(m_Modifiers2, 0)
        End If
    End Sub

    ''' <summary>
    ''' Adds a modifier to the list if the modifier isn't there already.
    ''' </summary>
    ''' <param name="Modifier"></param>
    ''' <remarks></remarks>
    Public Sub AddModifier(ByVal Modifier As KS)
        If Modifier = KS.None Then Return
        If Me.Is(Modifier) = False Then
            For I As Integer = 0 To 10
                If m_Modifiers2(I) = KS.None Then
                    m_Modifiers2(I) = Modifier
                    Return
                End If
            Next
            'm_Modifiers.Add(Modifier)
        End If
    End Sub

    Public Sub AddModifiers(ByVal Modifiers As Modifiers, ByVal ParamArray Except() As KS)
        If Modifiers.Count > 0 Then
            For Each modifier As KS In Modifiers.m_Modifiers2
                If Array.IndexOf(Except, modifier) < 0 Then
                    AddModifier(modifier)
                End If
            Next
        End If
    End Sub

    ''' <summary>
    ''' Returns true if any of the specified modifiers are found.
    ''' </summary>
    ''' <param name="Modifiers"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ContainsAny(ByVal ParamArray Modifiers() As KS) As Boolean
        For Each modifier As KS In Modifiers
            If Me.Is(modifier) Then Return True
        Next
        Return False
    End Function

    ''' <summary>
    ''' Returns true if the modifier Publis is set, or any other modifiers (Private, Friend, Protected) 
    ''' is not set.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property IsPublic() As Boolean
        Get
            Return Me.Is(KS.Public) OrElse (Me.Is(KS.Private) = False AndAlso Me.Is(KS.Protected) = False AndAlso Me.Is(KS.Friend) = False)
        End Get
    End Property

    ReadOnly Property [Is](ByVal Modifier As KS) As Boolean
        Get
            For i As Integer = 0 To 10
                If m_Modifiers2(i) = Modifier Then Return True
                If m_Modifiers2(i) = KS.None Then Return False
            Next
            Helper.Stop()
            'Return m_Modifiers.Contains(Modifier)
        End Get
    End Property

    ReadOnly Property ModifiersAsArray() As KS()
        Get
            Return m_Modifiers2
        End Get
    End Property

    '<Obsolete()> ReadOnly Property ModifiersAsArrayList() As Generic.List(Of KS)
    '    Get
    '        Return New Generic.List(Of KS)(m_Modifiers2) 'Return m_Modifiers
    '    End Get
    'End Property

    Function GetMethodAttributeScope() As MethodAttributes
        If Me.Is(KS.Public) Then
            Return MethodAttributes.Public
        ElseIf Me.Is(KS.Friend) Then
            If Me.Is(KS.Protected) Then
                Return MethodAttributes.FamORAssem
            Else
                Return MethodAttributes.Assembly
            End If
        ElseIf Me.Is(KS.Protected) Then
            Return MethodAttributes.Family
        ElseIf Me.Is(KS.Private) Then
            Return MethodAttributes.Private
        Else
            Return MethodAttributes.Public
        End If
    End Function

    Function GetFieldAttributeScope() As Reflection.FieldAttributes
        If Me.Is(KS.Public) Then
            Return Reflection.FieldAttributes.Public
        ElseIf Me.Is(KS.Friend) Then
            If Me.Is(KS.Protected) Then
                Return Reflection.FieldAttributes.FamORAssem
            Else
                Return Reflection.FieldAttributes.Assembly
            End If
        ElseIf Me.Is(KS.Protected) Then
            Return Reflection.FieldAttributes.Family
        ElseIf Me.Is(KS.Private) Then
            Return Reflection.FieldAttributes.Private
        ElseIf Me.Is(KS.Dim) OrElse Me.Is(KS.Const) Then
            Dim tpParent As TypeDeclaration = Me.FindFirstParent(Of TypeDeclaration)()
            If TypeOf tpParent Is StructureDeclaration Then
                Return FieldAttributes.Public
            Else
                Return FieldAttributes.Private
            End If
        Else
            'Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, "Default scope set to public...")
            Return FieldAttributes.Private
        End If
    End Function
End Class
