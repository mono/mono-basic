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

'#Const DEBUGRESOLVE = True

'''' <summary>
'''' Base class for all members
'''' </summary>
'''' <remarks></remarks>
'<Obsolete()> MustInherit Public Class BaseMember
'    Inherits NamedObject
'    Implements IAttributable

'    Private m_Attributes As New AttributeMembers(Compiler)

'    Protected Sub New(ByVal Parent As IBaseObject)
'        MyBase.New(Parent)
'    End Sub

'    Overridable Function DefineMember() As Boolean
'        Return True
'    End Function

'    ''' <summary>
'    ''' Defined in BaseMember
'    ''' </summary>
'    ''' <value></value>
'    ''' <remarks></remarks>
'    ReadOnly Property ParentAsBaseType() As BaseType
'        Get
'            Helper.Assert(MyBase.ParentAsNamedObject IsNot Nothing)
'            Helper.Assert(TypeOf MyBase.ParentAsNamedObject Is BaseType)
'            Return DirectCast(MyBase.ParentAsNamedObject, BaseType)
'        End Get
'    End Property

'    ''' <summary>
'    ''' Writes the start of an element. 
'    ''' The end must later be written as well.
'    ''' </summary>
'    ''' <param name="xml"></param>
'    ''' <remarks></remarks>
'    Sub DumpMember(ByVal xml As Xml.XmlWriter)
'        Dim member As String
'        member = Me.GetType.ToString.Replace("vbnc.", "").Replace("Member", "")
'        xml.WriteStartElement(member)
'    End Sub

'    Public Property Attributes() As AttributeMembers Implements IAttributable.Attributes
'        Get
'            Return m_Attributes
'        End Get
'        Set(ByVal value As AttributeMembers)
'            m_Attributes = value
'            m_Attributes.SetParent(DirectCast(Me, IAttributable))
'        End Set
'    End Property
'#If DEBUG Then
'    Public Sub DumpAttributes() Implements IAttributable.DumpAttributes
'        If m_Attributes IsNot Nothing Then m_Attributes.Dump()
'    End Sub

'    Public Sub DumpAttributes(ByVal Xml As System.Xml.XmlWriter) Implements IAttributable.DumpAttributes
'        If m_Attributes IsNot Nothing Then m_Attributes.Dump(Xml)
'    End Sub
'#End If


'#Region "Resolution region"
'    'This code is shared between Expression and BaseMember, should be the same code.

'    ''' <summary>
'    ''' Has this expression been resolved?
'    ''' </summary>
'    ''' <remarks></remarks>
'    Private m_Resolved As Boolean

'    ''' <summary>
'    ''' Is this expression beeing resolved (in Resolve / DoResolve)
'    ''' </summary>
'    ''' <remarks></remarks>
'    Private m_Resolving As Boolean

'    ''' <summary>
'    ''' Calling this function in stead of Resolve enables 
'    ''' </summary>
'    ''' <returns></returns>
'    ''' <remarks></remarks>
'    Function DoResolve() As Boolean
'        Dim result As Boolean
'        StartResolve()
'        result = Resolve()
'        EndResolve(result)
'#If DEBUGRESOLVE Then
'        If result = False Then Helper.Stop()
'#End If
'        Return result
'    End Function

'    ''' <summary>
'    ''' Call StartResolve to enable check for recursive resolving.
'    ''' Call EndResolve when finished resolving.
'    ''' </summary>
'    ''' <remarks></remarks>
'    Private Sub StartResolve()
'        If m_Resolving Then
'            'Recursive resolution.
'            'TODO: Find a meaningful error message
'            Throw New InternalException(Me)
'        End If
'        m_Resolving = True
'    End Sub

'    ''' <summary>
'    ''' Is this expression beeing resolved (in Resolve)?
'    ''' </summary>
'    ''' <value></value>
'    ''' <remarks></remarks>
'    ReadOnly Property IsResolving() As Boolean
'        Get
'            Return m_Resolving
'        End Get
'    End Property

'    ''' <summary>
'    ''' Is this constant resolved?
'    ''' </summary>
'    ''' <value></value>
'    ''' <remarks></remarks>
'    ReadOnly Property IsResolved() As Boolean
'        Get
'            Return m_Resolved
'        End Get
'    End Property

'    ''' <summary>
'    ''' Call StartResolve to enable check for recursive resolving.
'    ''' Call EndResolve when finished resolving.
'    ''' </summary>
'    ''' <remarks></remarks>
'    Private Sub EndResolve(ByVal result As Boolean)
'        If Not m_Resolving Then Throw New InternalException(Me)
'        m_Resolving = False
'        m_Resolved = result
'    End Sub

'    Protected Overridable Function Resolve() As Boolean
'        Helper.Stop()
'        Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, "Called Resolve() of type " & Me.GetType.FullName)
'    End Function
'#End Region

'End Class