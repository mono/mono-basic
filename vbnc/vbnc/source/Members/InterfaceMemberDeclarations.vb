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

'''' <summary>
'''' InterfaceMemberDeclaration  ::=
''''	 NonModuleDeclaration  |
''''	 InterfaceEventMemberDeclaration  |
''''	 InterfaceMethodMemberDeclaration  |
''''	 InterfacePropertyMemberDeclaration
'''' </summary>
'''' <remarks></remarks>
'Public Class InterfaceMemberDeclarations
'    Inherits BaseObject

'    Private m_Declarations As Nameables

'    Public Overrides Function Resolve() As Boolean
'        Return m_Declarations.Resolve
'    End Function
'#If DEBUG Then
'    Public Sub Dump()
'        m_Declarations.Dump()
'    End Sub
'    Public Sub Dump(ByVal Xml As System.Xml.XmlWriter)
'        Xml.WriteStartElement(Me.GetType.ToString)
'        m_Declarations.Dump(Xml)
'        Xml.WriteEndElement()
'    End Sub
'#End If
'    Sub New(ByVal Parent As IBaseObject)
'        MyBase.New(Parent)
'        m_Declarations = New Nameables(Me.Compiler, New Index(Me.Compiler))
'    End Sub

'    Public Overrides Function Parse() As Boolean
'        Dim result As Boolean = True
'        Dim parsing As Boolean = True
'        Dim attributes As Attributes = Nothing

'        While parsing
'            parsing = True
'            If vbnc.Attributes.IsMe(tm) Then
'                attributes = New Attributes(Me)
'                result = attributes.Parse AndAlso result
'            End If
'            Dim newMember As IAttributableNamedDeclaration = Nothing
'            If NonModuleDeclaration.IsMe(tm) Then
'                newMember = New NonModuleDeclaration(Me)
'            ElseIf InterfaceEventMemberDeclaration.IsMe(tm) Then
'                newMember = New InterfaceEventMemberDeclaration(Me)
'            ElseIf InterfaceFunctionDeclaration.isme(tm) Then
'                newMember = New InterfaceFunctionDeclaration(Me)
'            ElseIf InterfaceSubDeclaration.isme(tm) Then
'                newMember = New InterfaceSubDeclaration(Me)
'            ElseIf InterfacePropertyMemberDeclaration.IsMe(tm) Then
'                newMember = New InterfacePropertyMemberDeclaration(Me)
'            Else
'                parsing = False
'            End If

'            If parsing Then
'                result = newMember.Parse(attributes) AndAlso result
'                m_Declarations.Add(newMember)
'            End If
'        End While

'        Return result
'    End Function
'End Class
