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
''' AttributePositionalArgumentList  ::=
'''	    AttributeArgumentExpression  |
'''	    AttributePositionalArgumentList  ","  AttributeArgumentExpression
'''
''' </summary>
''' <remarks></remarks>
Public Class AttributePositionalArgumentList
    Inherits BaseList(Of AttributeArgumentExpression)

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Overloads Sub Add(ByVal Constant As Object)
        Dim exp As New AttributeArgumentExpression(Me)
        exp.Init(New ConstantExpression(exp, Constant, CecilHelper.GetType(Compiler, Constant)))
        Add(exp)
    End Sub

    Shared Function CanBeMe(ByVal tm As tm) As Boolean
        Return AttributeArgumentExpression.CanBeMe(tm)
    End Function

    ReadOnly Property AsExpressions() As Expression()
        Get
            Dim result(Me.Count - 1) As Expression
            For i As Integer = 0 To Me.Count - 1
                result(i) = Item(i).Expression
            Next
            Return result
        End Get
    End Property
End Class
