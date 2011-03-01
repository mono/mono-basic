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

''' <summary>
''' Constraint  ::=  TypeName  |  "New"
''' LAMESPEC? Using the following:
''' Constraint  ::= TypeName | "New" | "Class" | "Structure"
''' </summary>
''' <remarks></remarks>
Public Class Constraint
    Inherits ParsedObject

    Private m_TypeName As TypeName
    Private m_Special As KS = KS.None

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal TypeName As TypeName, ByVal Special As KS)
        m_TypeName = TypeName
        m_Special = Special
    End Sub

    ReadOnly Property TypeName() As TypeName
        Get
            Return m_TypeName
        End Get
    End Property

    ReadOnly Property Special() As KS
        Get
            Return m_special
        End Get
    End Property

    ReadOnly Property SpecialConstraintAttribute() As Mono.Cecil.GenericParameterAttributes
        Get
            Select Case m_Special
                Case KS.[New]
                    Return Mono.Cecil.GenericParameterAttributes.DefaultConstructorConstraint
                Case KS.Class
                    Return Mono.Cecil.GenericParameterAttributes.ReferenceTypeConstraint
                Case KS.Structure
                    Return Mono.Cecil.GenericParameterAttributes.NotNullableValueTypeConstraint
                Case KS.None
                    Return Mono.Cecil.GenericParameterAttributes.NonVariant
                Case Else
                    Throw New InternalException(Me)
            End Select
        End Get
    End Property

    <Obsolete("No code to resolve here.")> Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Return True
    End Function

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        If m_TypeName IsNot Nothing Then result = m_TypeName.ResolveTypeReferences AndAlso result

        Return result
    End Function

End Class
