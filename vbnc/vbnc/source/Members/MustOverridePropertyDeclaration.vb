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

''' <summary>
''' MustOverridePropertyMemberDeclaration  ::=
'''	[  Attributes  ]  [  MustOverridePropertyModifier+  ]  "Property" FunctionSignature  [  ImplementsClause  ]
'''		StatementTerminator
''' </summary>
''' <remarks></remarks>
Public Class MustOverridePropertyDeclaration
    Inherits PropertyDeclaration

    Sub New(ByVal Parent As TypeDeclaration)
        MyBase.New(Parent)
    End Sub

    'Public Function DefineMember() As Boolean Implements IDefinableMember.DefineMember
    '    Dim result As Boolean = True

    '    m_PropertyBuilder = Me.FindFirstParent(Of IType).TypeBuilder.DefineProperty(Me.Name, m_Descriptor.Attributes, m_Signature.ReturnType, m_Signature.Parameters.ToTypeArray)

    '    Dim attribs As MethodAttributes
    '    attribs = MethodAttributes.SpecialName Or MethodAttributes.Abstract Or MethodAttributes.Virtual Or Modifiers.GetMethodAttributeScope Or MethodAttributes.NewSlot

    '    If m_Modifiers.Is(KS.WriteOnly) = False Then
    '        m_GetBuilder = Me.FindFirstParent(Of IType).TypeBuilder.DefineMethod("get_" & Name, attribs, m_Signature.ReturnType, m_Signature.Parameters.ToTypeArray)
    '        m_PropertyBuilder.SetGetMethod(m_GetBuilder)
    '        Compiler.Helper.DumpDefine(m_GetBuilder)
    '    End If

    '    If m_Modifiers.Is(KS.ReadOnly) = False Then
    '        Dim types As New Generic.List(Of Type)
    '        types.AddRange(m_Signature.Parameters.ToTypeArray)
    '        types.Add(m_Signature.ReturnType)

    '        m_SetBuilder = Me.FindFirstParent(Of IType).TypeBuilder.DefineMethod("set_" & Name, attribs, Nothing, types.ToArray)
    '        m_PropertyBuilder.SetSetMethod(m_SetBuilder)
    '        Compiler.Helper.DumpDefine(m_GetBuilder)
    '    End If

    '    Return result
    'End Function



    Shared Function IsMe(ByVal tm As tm) As Boolean
        Dim i As Integer
        While tm.PeekToken(i).Equals(Enums.MustOverridePropertyModifiers)
            i += 1
        End While
        Return tm.PeekToken(i).Equals(KS.Property)
    End Function

End Class
