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

Public Class PropertyGroupToPropertyAccessExpression
    Inherits Expression

    Private m_PropertyGroup As PropertyGroupClassification
    Private m_ExpressionType As Mono.Cecil.TypeReference

    Sub New(ByVal Parent As ParsedObject, ByVal PropertyGroupClassification As PropertyGroupClassification)
        MyBase.new(Parent)
        m_PropertyGroup = PropertyGroupClassification
    End Sub

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        Helper.Assert(m_PropertyGroup IsNot Nothing, "m_PropertyGroup Is Nothing")

        If m_PropertyGroup.IsResolved = False OrElse m_PropertyGroup.ResolvedProperty Is Nothing Then
            result = m_PropertyGroup.ResolveGroup(New ArgumentList(Me)) AndAlso result

            If result = False Then
                Compiler.Report.WriteLine("Property group resolution failed (unrecoverably), showing log")
                Helper.LOGMETHODRESOLUTION = True
                m_PropertyGroup.ResolveGroup(New ArgumentList(Me))
                Return Helper.AddError(Me, "Failed to resolve property group.")
            End If

            If m_PropertyGroup.InstanceExpression Is Nothing AndAlso CecilHelper.IsStatic(m_PropertyGroup.ResolvedProperty) = False Then
                Return Report.ShowMessage(Messages.VBNC30469, Me.Location)
            End If
        End If

        Helper.Assert(m_PropertyGroup.ResolvedProperty IsNot Nothing, "m_PropertyGroup.ResolvedProperty Is Nothing")
        m_ExpressionType = m_PropertyGroup.ResolvedProperty.PropertyType

        result = m_ExpressionType IsNot Nothing AndAlso result

        Me.Classification = New PropertyAccessClassification(Me, m_PropertyGroup.ResolvedProperty, m_PropertyGroup.InstanceExpression, m_PropertyGroup.Parameters)

        Return result
    End Function

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        result = Classification.GenerateCode(Info) AndAlso result

        Return result
    End Function

    Overrides ReadOnly Property ExpressionType() As Mono.Cecil.TypeReference
        Get
            Return m_ExpressionType
        End Get
    End Property
End Class

