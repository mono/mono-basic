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

    Shadows Sub Add(ByVal Item As IMember)
        Dim ptd As PartialTypeDeclaration = TryCast(Item, PartialTypeDeclaration)
        If ptd IsNot Nothing AndAlso Me.ContainsName(Item.Name) Then
            Dim mainDeclaration As PartialTypeDeclaration
            Dim items As Generic.List(Of INameable)

            items = Me.Index.Item(Item.Name)
            If items Is Nothing OrElse items.Count <> 1 Then
                Helper.AddError(Parent)
                Return
            End If

            mainDeclaration = TryCast(items(0), PartialTypeDeclaration)
            If mainDeclaration Is Nothing Then
                Helper.AddError(Parent)
                Return
            End If

            If Helper.CompareName(mainDeclaration.Namespace, ptd.Namespace) Then
                mainDeclaration.AddPartialDeclaration(ptd)
                Return
            End If
        End If

        MyBase.Add(Item)
    End Sub

    ReadOnly Property Declarations() As Nameables(Of IMember)
        Get
            Return Me
        End Get
    End Property

    ReadOnly Property MemberDeclarations() As Generic.List(Of MemberInfo)
        Get
            Dim result As New Generic.List(Of MemberInfo)
            For Each member As IMember In Me
                result.Add(member.MemberDescriptor)
            Next
            Return result
        End Get
    End Property

    Function GetMethod(ByVal Name As String, ByVal bindingAttr As BindingFlags, ByVal types As Type()) As MethodInfo
        Dim result As MethodInfo = Nothing
        Dim methods As Generic.List(Of MethodDeclaration)

        methods = GetSpecificMembers(Of MethodDeclaration)()
        For Each method As MethodDeclaration In methods
            If Helper.CompareName(method.Name, Name) Then
                If Helper.CompareTypes(method.Signature.Parameters.ToTypeArray, types) Then
                    result = New MethodDescriptor(method)
                    Exit For
                End If
            End If
        Next

        If result Is Nothing AndAlso CBool(bindingAttr And BindingFlags.FlattenHierarchy) Then
            result = parent.FindFirstParent(Of IType).BaseType.GetMethod(Name, bindingAttr, Nothing, types, Nothing)
        End If
        Return result
    End Function

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

    Sub New(ByVal Parent As ParsedObject)
        MyBase.new(Parent, New Index(Parent))
    End Sub
End Class