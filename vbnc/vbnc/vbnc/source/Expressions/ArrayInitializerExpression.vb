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

Public Class ArrayInitializerExpression
    Inherits Expression

    Private m_Initializers As New Expressions

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Initializers As Expressions)
        MyBase.New(Parent)
        m_Initializers = Initializers
    End Sub

    Sub Init(ByVal Initializers As Expressions)
        m_Initializers = Initializers
    End Sub

    Sub Init(ByVal Arguments As Generic.List(Of Argument))

        m_Initializers = Initializers
    End Sub
    ReadOnly Property Initializers() As Expressions
        Get
            Return m_Initializers
        End Get
    End Property

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        For Each exp As Expression In m_Initializers
            result = exp.ResolveExpression(Info) AndAlso result
        Next

        Return result
    End Function

#If DEBUG Then
    Public Overrides Sub Dump(ByVal Dumper As IndentedTextWriter)
        Dumper.Write("{")
        m_Initializers.Dump(Dumper)
        Dumper.Write("}")
    End Sub
#End If
End Class
