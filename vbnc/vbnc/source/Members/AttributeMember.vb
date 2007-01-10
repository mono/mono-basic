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

'Imports System.Reflection.Emit

'Public Class AttributeMember
'	Inherits BaseObject

'	Private m_IsAssembly As Boolean
'	Private m_IsModule As Boolean
'    Protected m_Type As NonArrayTypeName
'    Protected m_Arguments As InvocationOrIndexExpression
'    Protected m_Location As Span

'    Private m_Classification As MethodGroupClassification
'    Private m_Compiler As Compiler

'    Overridable ReadOnly Property Type() As Type
'        Get
'            Helper.Assert(m_Type.IsResolved)
'            Return m_Type.ResolvedType
'        End Get
'    End Property

'    Public Overrides ReadOnly Property Compiler() As Compiler
'        Get
'            Return m_Compiler
'        End Get
'    End Property

'    Public Overrides ReadOnly Property Assembly() As AssemblyType
'        Get
'            Return Compiler.theAss
'        End Get
'    End Property

'    'Sub SetParent(ByVal Parent As IAttributable)
'    '    Helper.Assert(TypeOf Parent Is NamedObject)
'    '    SetParent(DirectCast(Parent, NamedObject))
'    'End Sub

'    'Sub SetParent(ByVal Parent As NamedObject)
'    '    MyBase.Parent = Parent
'    'End Sub

'    Function GetAttributeBuilder() As Reflection.Emit.CustomAttributeBuilder
'        Return New CustomAttributeBuilder(Constructor, Parameters)
'    End Function

'    Overridable ReadOnly Property Constructor() As Reflection.ConstructorInfo
'        Get
'            Helper.Assert(m_Classification.Group.Length = 1)
'            Helper.Assert(TypeOf m_Classification.Group(0) Is Reflection.ConstructorInfo)
'            Return DirectCast(m_Classification.Group(0), Reflection.ConstructorInfo)
'        End Get
'    End Property

'    ReadOnly Property Parameters() As Object()
'        Get
'            Dim result(m_Arguments.ArgumentList.Length - 1) As Object
'            For i As Integer = 0 To m_Arguments.ArgumentList.Length - 1
'                Helper.Assert(m_Arguments.ArgumentList(i).Expression.IsConstant)
'                result(i) = m_Arguments.ArgumentList(i).Expression.ConstantValue
'            Next
'            Return result
'        End Get
'    End Property

'    ReadOnly Property ParentAsIAttributable() As IAttributable
'        Get
'            Helper.Assert(TypeOf MyBase.Parent Is IAttributable)
'            Return DirectCast(MyBase.Parent, IAttributable)
'        End Get
'    End Property

'    Overrides Function Resolve() As Boolean
'        Dim result As Boolean = True

'        result = m_Type.Resolve() AndAlso result
'        result = m_Arguments.DoResolve() AndAlso result

'        If result Then
'            m_Classification = New MethodGroupClassification(Me)
'            m_Classification.SetMethods(m_Type.ResolvedType.GetConstructors)
'            m_Classification.ResolveGroup(Me.ParentAsNamedObject, m_Arguments.ArgumentList)
'        End If
'        Return result
'    End Function

'    ReadOnly Property Location() As Span
'        Get
'            Return m_Location
'        End Get
'    End Property

'    Sub New(ByVal Parent As NamedObject, ByVal Compiler As Compiler)
'        MyBase.New(Parent)
'        m_Compiler = Compiler
'    End Sub

'    ''' <summary>
'    ''' Is this a module attribute?
'    ''' </summary>
'    ''' <value></value>
'    ''' <remarks></remarks>
'    Public ReadOnly Property IsModule() As Boolean
'        Get
'            Return m_IsModule
'        End Get
'    End Property

'    ''' <summary>
'    ''' Is this an assembly attribyte?
'    ''' </summary>
'    ''' <value></value>
'    ''' <remarks></remarks>
'    Public ReadOnly Property IsAssembly() As Boolean
'        Get
'            Return m_IsAssembly
'        End Get
'    End Property

'    Overrides Function Parse() As Boolean
'        Dim result As Boolean = True

'        m_Location = tm.CurrentToken.Location

'        If tm.Accept("Assembly") Then
'            m_IsAssembly = True
'            result = tm.AcceptIfNotError(KS.Colon) AndAlso result
'        ElseIf tm.Accept(KS.Module) Then
'            m_IsModule = True
'            result = tm.AcceptIfNotError(KS.Colon) AndAlso result
'        End If

'        m_Type = New NonArrayTypeName(Me)
'        result = m_Type.Parse() AndAlso result

'        If tm.CurrentToken = KS.LParenthesis Then
'            m_Arguments = New InvocationOrIndexExpression(Me)
'            Helper.NotImplemented() '    _______ nothing?
'            result = m_Arguments.Parse(Nothing) AndAlso result
'        End If

'        m_Location.SpanTo(tm.PeekToken(-1).Location)

'        Return result
'    End Function

'#If DEBUG Then
'	Public Sub Dump()
'		If m_IsAssembly Then
'			Debug.Write("Assembly: ")
'		ElseIf m_IsModule Then
'			Debug.Write("Module: ")
'		End If
'		m_Type.Dump()
'		If m_Arguments IsNot Nothing Then
'			m_Arguments.Dump()
'		End If
'    End Sub

'	Public Sub Dump(ByVal Xml As System.Xml.XmlWriter)
'		Xml.WriteStartElement(Me.GetType.Name)
'		Xml.WriteAttributeString("Assembly", m_IsAssembly.ToString)
'		Xml.WriteAttributeString("Module", m_IsModule.ToString)
'		m_Type.Dump(Xml)
'		If m_Arguments IsNot Nothing Then
'			m_Arguments.Dump(Xml)
'		End If
'		Xml.WriteEndElement()
'	End Sub
'#End If
'End Class
