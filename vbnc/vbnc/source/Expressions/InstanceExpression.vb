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
''' Classification: Value
''' 
''' InstanceExpression  ::=  "Me" | "MyClass" | "MyBase"
''' </summary>
''' <remarks></remarks>
Public MustInherit Class InstanceExpression
    Inherits Expression

    Protected m_ExpressionType As Mono.Cecil.TypeReference

    Public Overrides Function ResolveTypeReferences() As Boolean
        Return True
    End Function

    Shared Function IsMe(ByVal tm As tm) As Boolean
        Return tm.CurrentToken.Equals(KS.Me, KS.MyBase, KS.MyClass)
    End Function

    Overrides ReadOnly Property ExpressionType() As Mono.Cecil.TypeReference
        Get
            Helper.Assert(m_ExpressionType IsNot Nothing)
            Return m_ExpressionType
        End Get
    End Property

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Dim tp As TypeDeclaration
        tp = Me.FindFirstParent(Of TypeDeclaration)()
        m_ExpressionType = tp.CecilType

        '        Classification = New ValueClassification(Me, m_ExpressionType)
        'SPECBUG: instance expressions should be variable classifications?
        Classification = New VariableClassification(Me, Me, m_ExpressionType)

        Return True
    End Function

    ReadOnly Property Name() As String
        Get
            If TypeOf Me Is MeExpression Then
                Return KS.Me.ToString
            ElseIf TypeOf Me Is MyBaseExpression Then
                Return KS.MyBase.ToString
            ElseIf TypeOf Me Is MyClassExpression Then
                Return KS.MyClass.ToString
            Else
                Throw New InternalException("Invalid instance expression.")
            End If
        End Get
    End Property

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

End Class
