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
#Const DEBUGPARSERESULT = 1

''' <summary>
''' ClassMemberDeclaration  ::= 
''' StructureMemberDeclaration ::=
'''  NonModuleDeclaration          |
'''	 EventMemberDeclaration        |
'''  VariableMemberDeclaration     |
'''  ConstantMemberDeclaration     |
'''  MethodMemberDeclaration       |
'''  PropertyMemberDeclaration     |
'''  ConstructorMemberDeclaration  |
'''  OperatorDeclaration
'''
''' ModuleMemberDeclaration  ::=
'''  NonModuleDeclaration       |
'''  VariableMemberDeclaration  |
'''  ConstantMemberDeclaration  |
'''	 EventMemberDeclaration     |
'''  MethodMemberDeclaration    |
'''  PropertyMemberDeclaration  |
'''  ConstructorMemberDeclaration
'''
''' NamespaceMemberDeclaration  ::= NamespaceDeclaration  | TypeDeclaration
''' TypeDeclaration             ::= ModuleDeclaration     |	NonModuleDeclaration
''' 
''' InterfaceMemberDeclaration  ::=
'''	 NonModuleDeclaration  |
'''	 InterfaceEventMemberDeclaration  |
'''	 InterfaceMethodMemberDeclaration  |
'''	 InterfacePropertyMemberDeclaration
''' 
''' NonModuleDeclaration  ::=
'''  EnumDeclaration  |
'''	 StructureDeclaration  |
'''	 InterfaceDeclaration  |
'''	 ClassDeclaration  |
'''	 DelegateDeclaration
'''
''' </summary>
''' <remarks></remarks>
Public Class MemberDeclarations
    Inherits Nameables(Of IMember)

    ReadOnly Property Declarations() As Nameables(Of IMember)
        Get
            Return Me
        End Get
    End Property

    ReadOnly Property MemberDeclarations() As Generic.List(Of Mono.Cecil.MemberReference)
        Get
            Dim result As New Generic.List(Of Mono.Cecil.MemberReference)
            For Each member As IMember In Me
                result.Add(member.MemberDescriptor)
            Next
            Return result
        End Get
    End Property

    Function GetSpecificMembers(Of T)() As Generic.List(Of T)
        Dim result As New Generic.List(Of T)

        For i As Integer = 0 To Count - 1
            Dim obj As IMember = Me.Item(i)
            If TypeOf obj Is T Then
                result.Add(CType(CObj(obj), T))
            End If
        Next

        Return result
    End Function

    Function HasSpecificMember(Of T)() As Boolean
        For i As Integer = 0 To Count - 1
            If TypeOf Me.Item(i) Is T Then Return True
        Next
        Return False
    End Function

    Sub New(ByVal Parent As ParsedObject)
        MyBase.new(Parent)
    End Sub
End Class