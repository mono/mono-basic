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
'''' A list of attributes.
'''' </summary>
'''' <remarks></remarks>
'<Obsolete()> Public Class AttributeMembers
'    Inherits BaseObjects ' Members

'    Private m_AttributeParent As NamedObject

'    Sub New(ByVal Parent As NamedObject)
'        MyBase.New(Parent.Compiler)
'        Helper.Assert(Parent.Compiler IsNot Nothing)
'        m_AttributeParent = Parent
'    End Sub

'    ''' <summary>
'    ''' Parse the attributes. Can be called several times, every time will add new attributes to this list of attributes.
'    ''' </summary>
'    ''' <returns></returns>
'    ''' <remarks></remarks>
'    Public Function Parse(Optional ByVal Parent As NamedObject = Nothing) As Boolean
'        Dim result As Boolean = True

'        If Not Compiler.tm.Accept(KS.LT) Then Throw New InternalException(Me)

'        If Compiler.tm.CurrentToken = KS.GT Then
'            Compiler.Report.ShowMessage(Messages.VBNC30035)
'            Return False
'        End If

'        If Parent IsNot Nothing Then m_AttributeParent = Parent

'        Do
'            Dim newAttribute As New AttributeMember(m_AttributeParent, Compiler)
'            result = newAttribute.Parse AndAlso result
'            Add(newAttribute)
'        Loop While Compiler.tm.Accept(KS.Comma)

'        result = Compiler.tm.AcceptIfNotError(KS.GT) AndAlso result

'        Return result
'    End Function

'    'Sub SetParent(ByVal Parent As NamedObject)
'    '	Helper.Assert(TypeOf Parent Is IAttributable)
'    '	SetParent(DirectCast(Parent, IAttributable))
'    'End Sub

'    'Sub SetParent(ByVal Parent As IAttributable)
'    '	For Each attrib As AttributeMember In Me
'    '		attrib.SetParent(Parent)
'    '	Next
'    'End Sub

'    Sub New(ByVal Compiler As Compiler)
'        MyBase.New(Compiler)
'        Helper.Assert(Compiler IsNot Nothing)
'    End Sub

'    Shadows Sub Add(ByVal Base As AttributeMember)
'        MyBase.Add(Base)
'    End Sub

'    Shadows Sub Add(ByVal Attributes As AttributeMembers)
'        MyBase.AddRange(Attributes.ToArray)
'    End Sub

'    Shadows Function ToArray() As AttributeMember()
'        Return DirectCast(MyBase.ToArray(GetType(AttributeMember)), AttributeMember())
'    End Function

'    Default Shadows ReadOnly Property Item(ByVal Index As Integer) As AttributeMember
'        Get
'            Return DirectCast(MyBase.Item(Index), AttributeMember)
'        End Get
'    End Property

'    Sub Emit(ByVal Type As MemberParameter)
'        For Each attrib As AttributeMember In Me
'            Type.ParameterBuilder.SetCustomAttribute(attrib.GetAttributeBuilder)
'        Next
'    End Sub

'    Sub Emit(ByVal Type As ContainerType)
'        For Each attrib As AttributeMember In Me
'            Type.TypeBuilder.SetCustomAttribute(attrib.GetAttributeBuilder)
'        Next
'    End Sub

'    Sub Emit(ByVal Method As MethodMember)
'        Helper.NotImplemented()
'        'For Each attrib As AttributeMember In Me
'        '    Method.MethodBuilder.SetCustomAttribute(attrib.GetAttributeBuilder)
'        'Next
'    End Sub

'    Sub EmitAsAssembly()
'        For Each attrib As AttributeMember In Me
'            Helper.Assert(attrib.IsAssembly)
'            Helper.Assert(Compiler IsNot Nothing)
'            Compiler.AssemblyBuilder.SetCustomAttribute(attrib.GetAttributeBuilder)
'        Next
'    End Sub

'    Function IsDefined(ByVal Type As Type) As Boolean
'        For Each attrib As AttributeMember In Me
'            If attrib.Type.Equals(Type) Then
'                Return True
'            End If
'        Next
'        Return False
'    End Function

'    Function Resolve() As Boolean
'        Dim result As Boolean = True

'        For Each attrib As AttributeMember In Me
'            result = attrib.Resolve AndAlso result
'        Next

'        Return result
'    End Function

'#If DEBUG Then
'    Sub DumpAttributes(ByVal xml As Xml.XmlWriter)
'        For Each attrib As AttributeMember In Me
'            attrib.Dump(xml)
'        Next
'    End Sub
'    Sub DumpAttributes()
'        If Count > 0 Then
'            Debug.Write("<")
'            Dump(", ")
'            Debug.Write(">")
'        End If
'    End Sub
'    Public Sub Dump()
'        DumpAttributes()
'    End Sub
'    Public Sub Dump(ByVal Xml As System.Xml.XmlWriter)
'        DumpAttributes(Xml)
'    End Sub
'#End If
'End Class