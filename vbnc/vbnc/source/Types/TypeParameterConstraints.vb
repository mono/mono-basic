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
''' TypeParameterConstraints  ::= 	As  Constraint  |	As  {  ConstraintList  }
''' </summary>
''' <remarks></remarks>
Public Class TypeParameterConstraints
    Inherits ParsedObject

    Private m_ConstraintList As ConstraintList

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal ConstraintList As ConstraintList)
        m_ConstraintList = ConstraintList
    End Sub

    ''' <summary>
    ''' Might very well be nothing.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ClassConstraint() As Mono.Cecil.TypeReference
        Dim result As Mono.Cecil.TypeReference = Nothing
        For Each constraint As Constraint In m_ConstraintList
            If constraint.Special = KS.None Then
                If CecilHelper.IsClass(constraint.TypeName.ResolvedType) Then
                    Helper.Assert(result Is Nothing)
                    result = constraint.TypeName.ResolvedType
                End If
            End If
        Next
        Return result
    End Function

    ReadOnly Property Constraints() As ConstraintList
        Get
            Return m_Constraintlist
        End Get
    End Property

    <Obsolete("No code to resolve here.")> Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Return True
    End Function

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        result = m_ConstraintList.ResolveTypeReferences AndAlso result

        Return result
    End Function

    Shared Function CanBeMe(ByVal tm As tm) As Boolean
        Return tm.CurrentToken = KS.As
    End Function


End Class
