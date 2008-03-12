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
''' OptionExplicitStatement  ::=  Option  Explicit  [  OnOff  ]  StatementTerminator
''' </summary>
''' <remarks></remarks>
Public Class OptionExplicitStatement
    Inherits BaseObject

    Private m_Off As Boolean

    Sub New(ByVal Parent As BaseObject)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal Off As Boolean)
        m_Off = Off
    End Sub

    ReadOnly Property Off() As Boolean
        Get
            Return m_Off
        End Get
    End Property

    ReadOnly Property IsOn() As Boolean
        Get
            Return Not m_Off
        End Get
    End Property

    Shared Function IsMe(ByVal tm As tm) As Boolean
        Return tm.PeekToken(0) = KS.Option AndAlso tm.PeekToken(1).Equals("Explicit")
    End Function

End Class
