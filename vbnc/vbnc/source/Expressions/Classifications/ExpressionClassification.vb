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
''' A late-bound access can be reclassified as a late-bound method
''' or late-bound property access. In a situation where late-bound access
''' can be reclassified both as a method acces and a property access,
''' reclassification to a property access is preferred.
''' A late-bound access can be reclassified as a value.
''' </summary>
''' <remarks></remarks>
Public Class ExpressionClassification
    Private m_Parent As ParsedObject

    Private m_Classification As Classifications

    ReadOnly Property Parent() As ParsedObject
        Get
            Return m_Parent
        End Get
    End Property

    ReadOnly Property Compiler() As Compiler
        Get
            Return m_Parent.Compiler
        End Get
    End Property

    Public Overridable Function GetConstant(ByRef value As Object, ByVal ShowError As Boolean) As Boolean
        If ShowError Then m_Parent.Show30059()
        Return False
    End Function

    Overloads Function [GetType](ByVal ThrowIfNoType As Boolean) As Mono.Cecil.TypeReference
        Select Case m_Classification
            Case Classifications.Value
                Return AsValueClassification.Type
            Case Classifications.Variable
                Return AsVariableClassification.Type
            Case Classifications.MethodGroup
                Return AsMethodGroupClassification.Type
            Case Classifications.MethodPointer
                Return AsMethodPointerClassification.Type
            Case Classifications.PropertyAccess
                Return AsPropertyAccess.Type
            Case Classifications.PropertyGroup
                Return AsPropertyGroup.Type
            Case Classifications.Void
                Return Compiler.TypeCache.System_Void
            Case Classifications.LateBoundAccess
                Return AsLateBoundAccess.Type
            Case Else
                If ThrowIfNoType Then
                    Throw New InternalException("No type was found")
                Else
                    Return Nothing
                End If
        End Select
    End Function

    ReadOnly Property AsLateBoundAccess() As LateBoundAccessClassification
        Get
            Return DirectCast(Me, LateBoundAccessClassification)
        End Get
    End Property

    ReadOnly Property AsEventAccess() As EventAccessClassification
        Get
            Return DirectCast(Me, EventAccessClassification)
        End Get
    End Property

    ReadOnly Property AsMethodGroupClassification() As MethodGroupClassification
        Get
            Return DirectCast(Me, MethodGroupClassification)
        End Get
    End Property

    ReadOnly Property AsMethodPointerClassification() As MethodPointerClassification
        Get
            Return DirectCast(Me, MethodPointerClassification)
        End Get
    End Property

    ReadOnly Property AsNamespaceClassification() As NamespaceClassification
        Get
            Return DirectCast(Me, NamespaceClassification)
        End Get
    End Property

    ReadOnly Property AsPropertyAccess() As PropertyAccessClassification
        Get
            Return DirectCast(Me, PropertyAccessClassification)
        End Get
    End Property

    ReadOnly Property AsPropertyGroup() As PropertyGroupClassification
        Get
            Return DirectCast(Me, PropertyGroupClassification)
        End Get
    End Property

    ReadOnly Property AsTypeClassification() As TypeClassification
        Get
            Return DirectCast(Me, TypeClassification)
        End Get
    End Property

    ReadOnly Property AsValueClassification() As ValueClassification
        Get
            Return DirectCast(Me, ValueClassification)
        End Get
    End Property

    ReadOnly Property AsVariableClassification() As VariableClassification
        Get
            Return DirectCast(Me, VariableClassification)
        End Get
    End Property

    ReadOnly Property AsVoidClassification() As VoidClassification
        Get
            Return DirectCast(Me, VoidClassification)
        End Get
    End Property

    ReadOnly Property IsLateBoundClassification() As Boolean
        Get
            Return m_Classification = Classifications.LateBoundAccess
        End Get
    End Property

    ReadOnly Property IsEventAccessClassification() As Boolean
        Get
            Return m_Classification = Classifications.EventAccess
        End Get
    End Property

    ReadOnly Property IsMethodGroupClassification() As Boolean
        Get
            Return m_Classification = Classifications.MethodGroup
        End Get
    End Property

    ReadOnly Property IsMethodPointerClassification() As Boolean
        Get
            Return m_Classification = Classifications.MethodPointer
        End Get
    End Property

    ReadOnly Property IsNamespaceClassification() As Boolean
        Get
            Return m_Classification = Classifications.Namespace
        End Get
    End Property

    ReadOnly Property IsPropertyAccessClassification() As Boolean
        Get
            Return m_Classification = Classifications.PropertyAccess
        End Get
    End Property

    ReadOnly Property IsPropertyGroupClassification() As Boolean
        Get
            Return m_Classification = Classifications.PropertyGroup
        End Get
    End Property

    ReadOnly Property IsTypeClassification() As Boolean
        Get
            Return m_Classification = Classifications.Type
        End Get
    End Property

    ReadOnly Property CanBeValueClassification() As Boolean
        Get
            Select Case m_Classification
                Case Classifications.Value, Classifications.Variable, _
                Classifications.LateBoundAccess, Classifications.MethodGroup, _
                Classifications.MethodPointer, Classifications.PropertyAccess, Classifications.PropertyGroup
                    Return True
                Case Classifications.Type
                    Dim tc As TypeClassification = AsTypeClassification
                    Return tc.CanBeExpression AndAlso tc.Expression.Classification.CanBeValueClassification
                Case Classifications.Void, Classifications.Namespace, Classifications.EventAccess
                    Return False
                Case Else
                    Compiler.Report.ShowMessage(Messages.VBNC99997, m_Parent.Location)
                    Return False
            End Select
        End Get
    End Property

    ReadOnly Property CanBePropertyAccessClassification() As Boolean
        Get
            Select Case m_Classification
                Case Classifications.PropertyGroup
                    Return True
                Case Classifications.LateBoundAccess
                    Return True
                Case Classifications.Type
                    Dim tc As TypeClassification = AsTypeClassification
                    Return tc.CanBeExpression AndAlso tc.Expression.Classification.CanBePropertyAccessClassification
                Case Classifications.Value, Classifications.Variable, Classifications.EventAccess, _
Classifications.LateBoundAccess, Classifications.MethodGroup, _
Classifications.MethodPointer, Classifications.PropertyAccess, Classifications.Void, Classifications.Namespace
                    Return False
                Case Else
                    Compiler.Report.ShowMessage(Messages.VBNC99997, m_Parent.Location)
                    Return False
            End Select
        End Get
    End Property

    ReadOnly Property IsValueClassification() As Boolean
        Get
            Return m_Classification = Classifications.Value
        End Get
    End Property

    ReadOnly Property IsVariableClassification() As Boolean
        Get
            Return m_Classification = Classifications.Variable
        End Get
    End Property

    ReadOnly Property IsVoidClassification() As Boolean
        Get
            Return m_Classification = Classifications.Void
        End Get
    End Property

    ReadOnly Property Classification() As Classifications
        Get
            Return m_Classification
        End Get
    End Property

    Protected Sub New(ByVal Classification As Classifications, ByVal Parent As ParsedObject)
        m_Parent = Parent 'MyBase.New(Parent)
        m_Classification = Classification
    End Sub

    Friend Overridable Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Return Compiler.Report.ShowMessage(Messages.VBNC99997, m_Parent.Location)
    End Function
End Class
