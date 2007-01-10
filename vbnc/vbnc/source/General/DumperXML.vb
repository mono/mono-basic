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

Public Class DumperXML
    Public Shared Sub Dump(ByVal Obj As BaseObject, ByVal Xml As Xml.XmlWriter)
        Xml.WriteStartElement(Obj.GetType.ToString)
        Dim fields() As FieldInfo = Obj.GetType.GetFields(BindingFlags.Instance Or BindingFlags.Public Or BindingFlags.NonPublic)
        For Each field As FieldInfo In fields
            If field.Name <> "m_Parent" AndAlso field.Name <> "Parent" Then
                Dim fieldtypename As String = field.FieldType.ToString
                Dim fieldvalue As String = "Nothing"

                If Obj.GetType.IsAssignableFrom(field.FieldType) Then
                    Dim bofield As BaseObject = CType(field.GetValue(Obj), BaseObject)
                    If bofield IsNot Nothing Then
                        Dump(bofield, Xml)
                    End If
                Else
                    Dim vofield As Object = field.GetValue(Obj)
                    If vofield IsNot Nothing Then
                        fieldvalue = vofield.ToString
                    End If
                End If
                Xml.WriteStartElement(field.Name)
                Xml.WriteAttributeString("Type", fieldtypename)
                Xml.WriteString(fieldvalue)
                Xml.WriteEndElement()
            End If
        Next
        Xml.WriteEndElement()
    End Sub
End Class
