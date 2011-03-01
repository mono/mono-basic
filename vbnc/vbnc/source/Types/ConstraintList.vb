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
''' ConstraintList  ::=	ConstraintList  ","  Constraint  |	Constraint
''' </summary>
''' <remarks></remarks>
Public Class ConstraintList
    Inherits BaseList(Of Constraint)

    Sub New(ByVal Parent As ParsedObject, ByVal ParamArray Objects() As Constraint)
        MyBase.New(Parent, Objects)
    End Sub

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        Dim hasClass, hasStructure, hasNew As Boolean

        For Each constraint As Constraint In Me
            result = constraint.ResolveTypeReferences AndAlso result
            Select Case constraint.Special
                Case KS.None
                Case KS.Class
                    If hasClass Then
                        result = Compiler.Report.ShowMessage(Messages.VBNC32101, Location) AndAlso result
                    End If
                    hasClass = True
                Case KS.Structure
                    If hasStructure Then
                        result = Compiler.Report.ShowMessage(Messages.VBNC32102, Location) AndAlso result
                    End If
                    hasStructure = True
                Case KS.[New]
                    hasNew = True
            End Select

        Next

        If hasNew AndAlso hasStructure Then
            result = Compiler.Report.ShowMessage(Messages.VBNC32103, Location) AndAlso result
        End If

        If hasStructure AndAlso hasClass Then
            result = Compiler.Report.ShowMessage(Messages.VBNC32104, Location) AndAlso result
        End If

        Return result
    End Function
End Class
