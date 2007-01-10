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
''' A late-bound access, which represents a method or property access deferred until run-time. 
''' A late-bound access may have an associated instance expression and an associated type argument list. 
''' The type of a late-bound access is always Object.
''' 
''' 	A late-bound access can be reclassified as a value.
''' 	A late-bound access can be reclassified as a late-bound method or late-bound property access. In a situation where a late-bound access can be reclassified both as a method access and as a property access, reclassification to a property access is preferred.
''' </summary>
''' <remarks></remarks>
Public Class LateBoundAccessClassification
    Inherits ExpressionClassification

    ReadOnly Property Type() As Type
        Get
            Helper.NotImplemented() : Return Nothing
        End Get
    End Property

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Classifications.LateBoundAccess, Parent)
    End Sub
End Class
