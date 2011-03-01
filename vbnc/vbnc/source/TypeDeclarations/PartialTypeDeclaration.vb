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

Public MustInherit Class PartialTypeDeclaration
    Inherits GenericTypeDeclaration
    Public PartialModifierFound As Boolean
    Public IsPartial As Boolean

    Private m_TypeImplementsClauses As TypeImplementsClauses
    Private m_InterfacesImplemented As Boolean

    Sub New(ByVal Parent As ParsedObject, ByVal [Namespace] As String, ByVal Name As Identifier, ByVal TypeParameters As TypeParameters)
        MyBase.new(Parent, [Namespace], Name, TypeParameters)
    End Sub

    Property [Implements]() As TypeImplementsClauses
        Get
            Return m_TypeImplementsClauses
        End Get
        Set(ByVal value As TypeImplementsClauses)
            m_TypeImplementsClauses = value
        End Set
    End Property

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        If m_InterfacesImplemented = False AndAlso m_TypeImplementsClauses IsNot Nothing Then
            result = m_TypeImplementsClauses.ResolveTypeReferences AndAlso result
            For i As Integer = 0 To m_TypeImplementsClauses.Clauses.Count - 1
                AddInterface(m_TypeImplementsClauses.Clauses(i).ResolvedType)
            Next
            m_InterfacesImplemented = True
        End If

        result = MyBase.ResolveTypeReferences AndAlso result

        If Me.IsPartial Then
            If PartialModifierFound = False Then
                Dim first As PartialTypeDeclaration = Me
                Compiler.Report.ShowMessage(Messages.VBNC30179, Me.Location, Me.DescriptiveType, Me.Name, Me.DescriptiveType, Me.Name, Me.Namespace, Me.Namespace)
                result = False
            End If
            'TODO:
            'If TypeOf Me Is ClassDeclaration Then
            '    Dim inheritedTypes() As Mono.Cecil.TypeReference
            '    inheritedTypes = GetInheritedTypes()
            '    If inheritedTypes.Length > 0 Then
            '        Dim tmpType As Mono.Cecil.TypeReference
            '        tmpType = CheckUniqueType(inheritedTypes)
            '        If tmpType Is Nothing Then
            '            Return Helper.AddError(Me, "Partial classes must inherit from only one base class.")
            '        Else
            '            BaseType = tmpType
            '        End If
            '    Else
            '        Helper.Assert(BaseType IsNot Nothing) 'Should already be set to System.Object.
            '    End If
            'End If
        End If

        Return result
    End Function

    ''' <summary>
    ''' Checks that all types are equal.
    ''' Returns nothing if types are not equal.
    ''' </summary>
    ''' <param name="Types"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckUniqueType(ByVal Types() As Mono.Cecil.TypeReference) As Mono.Cecil.TypeReference
        Helper.Assert(Types.Length >= 1)
        For i As Integer = 1 To Types.Length - 1
            If Helper.CompareType(Types(0), Types(i)) = False Then Return Nothing
        Next
        Return Types(0)
    End Function
End Class
