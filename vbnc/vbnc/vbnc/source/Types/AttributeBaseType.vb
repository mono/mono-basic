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
'''' Every type that can have attribute must inherit from this class.
'''' </summary>
'''' <remarks></remarks>
'MustInherit Public Class AttributeBaseType
'	Inherits BaseType

'    ''' <summary>
'    ''' A list of Attributes
'    ''' </summary>
'    ''' <remarks></remarks>
'    Private m_lstAttributes As New ArrayList

'    Public ReadOnly Property Attributes() As ArrayList
'        Get
'            Return m_lstAttributes
'        End Get
'    End Property

'    'Protected Sub New()
'    '	MyBase.New()
'    '	Helper.Assert(TypeOf Me Is AssemblyType)
'    'End Sub

'    Protected Sub New(ByVal Parent As IBaseObject)
'        MyBase.New(Parent)
'    End Sub

'#If DEBUG Then
'    Sub DumpAttributes(ByVal xml As Xml.XmlWriter) 'Implements IAttributable.DumpAttributes
'        Helper.DumpCollection(m_lstAttributes, xml, "Attribute", Me.GetType.ToString)
'        'If m_lstAttributes IsNot Nothing Then m_lstAttributes.Dump(xml)
'    End Sub
'    Sub DumpAttributes() 'Implements IAttributable.DumpAttributes
'        Helper.DumpCollection(m_lstAttributes)
'        'If m_lstAttributes IsNot Nothing Then m_lstAttributes.Dump()
'    End Sub
'#End If
'End Class

'Interface IAttributable
'    Property Attributes() As AttributeMembers
'#If DEBUG Then
'    Sub DumpAttributes()
'    Sub DumpAttributes(ByVal Xml As Xml.XmlWriter)
'#End If
'End Interface