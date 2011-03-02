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

Public MustInherit Class MemberDeclaration
    Inherits ParsedObject
    Implements IMember

    Private m_DeclaringType As TypeDeclaration

    Private m_CustomAttributes As Attributes
    Private m_Modifiers As Modifiers
    Private m_Name As String
    Private m_GeneratedCode As Boolean

    ReadOnly Property GeneratedCode() As Boolean
        Get
            Return m_GeneratedCode
        End Get
    End Property

    Protected Sub New(ByVal Parent As ParsedObject)
        MyBase.new(Parent)
        m_DeclaringType = Me.FindFirstParent(Of TypeDeclaration)()
    End Sub

    Protected Sub New(ByVal Parent As AssemblyDeclaration)
        MyBase.new(Parent)
        m_DeclaringType = Nothing
    End Sub

    Protected Sub New(ByVal Parent As TypeDeclaration)
        MyBase.new(Parent)
        m_DeclaringType = Parent
    End Sub

    Protected Sub New(ByVal Parent As PropertyDeclaration)
        MyBase.new(Parent)
        m_DeclaringType = Parent.DeclaringType
        Helper.Assert(m_DeclaringType IsNot Nothing)
    End Sub

    Protected Sub New(ByVal Parent As EventDeclaration)
        MyBase.new(Parent)
        m_DeclaringType = Parent.DeclaringType
        Helper.Assert(m_DeclaringType IsNot Nothing)
    End Sub

    Sub Init(ByVal Modifiers As Modifiers, ByVal Name As String)
        m_Modifiers = Modifiers
        m_Name = Name

        If m_Name Is Nothing Then Throw New InternalException(Me.Location.ToString(Compiler))
    End Sub

    Public Overrides Function CreateDefinition() As Boolean Implements IMember.CreateDefinition
        Dim result As Boolean = True

        result = MyBase.CreateDefinition AndAlso result
        If m_CustomAttributes IsNot Nothing Then m_CustomAttributes.SetParent(Me)

        Return result
    End Function

    Protected Sub Rename(ByVal Name As String)
        m_Name = Name
        If MemberDescriptor IsNot Nothing Then
            MemberDescriptor.Name = m_Name
        End If
    End Sub

    Public Property CustomAttributes() As Attributes Implements IAttributableDeclaration.CustomAttributes
        Get
            Return m_CustomAttributes
        End Get
        Set(ByVal value As Attributes)
            m_CustomAttributes = value
        End Set
    End Property

    Public Function FindAttributes(ByVal Type As TypeReference) As Generic.List(Of Attribute)
        If m_CustomAttributes Is Nothing Then Return Nothing
        Return m_CustomAttributes.FindAttributes(Type)
    End Function

    Public Sub AddCustomAttribute(ByVal Attribute As Attribute)
        If m_CustomAttributes Is Nothing Then
            m_CustomAttributes = New Attributes(Me)
        End If
        m_CustomAttributes.Add(Attribute)
    End Sub

    Public Property DeclaringType() As TypeDeclaration Implements IMember.DeclaringType
        Get
            Return m_DeclaringType
        End Get
        Set(ByVal value As TypeDeclaration)
            m_DeclaringType = value
        End Set
    End Property

    Public Overridable ReadOnly Property IsShared() As Boolean Implements IMember.IsShared
        Get
            Return Me.Modifiers.Is(ModifierMasks.Shared) OrElse DeclaringType.IsShared OrElse Me.Modifiers.Is(ModifierMasks.Const)
        End Get
    End Property

    Public MustOverride ReadOnly Property MemberDescriptor() As Mono.Cecil.MemberReference Implements IMember.MemberDescriptor

    Public Property Modifiers() As Modifiers Implements IModifiable.Modifiers
        Get
            Return m_Modifiers
        End Get
        Set(ByVal value As Modifiers)
            m_Modifiers = value
        End Set
    End Property

    Public Property Name() As String Implements INameable.Name
        Get
            Return m_Name
        End Get
        Set(ByVal value As String)
            m_Name = value
        End Set
    End Property

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        If m_CustomAttributes IsNot Nothing Then result = m_CustomAttributes.ResolveTypeReferences AndAlso result

        Return result
    End Function

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        If m_CustomAttributes IsNot Nothing Then result = m_CustomAttributes.ResolveCode(Info) AndAlso result

        Return result
    End Function

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        If m_GeneratedCode = False Then
            If m_CustomAttributes IsNot Nothing Then result = m_CustomAttributes.GenerateCode(Info) AndAlso result

            m_GeneratedCode = True
        End If

        Return result
    End Function
End Class

