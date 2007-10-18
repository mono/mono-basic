' 
' Visual Basic.Net COmpiler
' Copyright (C) 2004 - 2006 Rolf Bjarne Kvinge, rbjarnek at users.sourceforge.net
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
Class CustomEvent2
    Delegate Sub a(ByVal param1 As String, ByRef param2 As Integer)
    Custom Event aa As a
        AddHandler(ByVal val As a)

        End AddHandler
        RemoveHandler(ByVal val As a)

        End RemoveHandler
        RaiseEvent(ByVal param As String, ByRef param2 As Integer)

        End RaiseEvent
    End Event
End Class
