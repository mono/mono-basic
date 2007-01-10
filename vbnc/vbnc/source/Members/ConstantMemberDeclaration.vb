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

''' <summary>
''' ConstantMemberDeclaration  ::=	[  Attributes  ]  [  ConstantModifier+  ]  "Const"  ConstantDeclarators  StatementTerminator
''' </summary>
''' <remarks>
''' This is just a helper class, the objects put into the parse tree
''' is ConstantDeclaration
''' </remarks>
<Obsolete()> Public Class ConstantMemberDeclaration
    Inherits BaseObject
    Implements IAttributableDeclaration

    Private m_Attributes As Attributes
    Private m_Modifiers As Modifiers
    Private m_ConstantDeclarators As ConstantDeclarations

    Sub New(ByVal Parent As IBaseObject)
        MyBase.New(Parent)
    End Sub

    ReadOnly Property Modifiers() As Modifiers
        Get
            Return m_Modifiers
        End Get
    End Property


    ReadOnly Property ConstantModifiers() As Modifiers
        Get
            Return m_Modifiers
        End Get
    End Property

    ReadOnly Property ConstantDeclarations() As ConstantDeclarations
        Get
            Return m_ConstantDeclarators
        End Get
    End Property

#If DEBUG Then
    Public Overrides Sub Dump(ByVal Dumper As IndentedTextWriter)
        If m_Attributes IsNot Nothing Then m_Attributes.Dump(Dumper)
        m_Modifiers.Dump(Dumper)
        Dumper.Write("Const ")
        m_ConstantDeclarators.Dump(Dumper)
        Dumper.WriteLine("")
    End Sub
#End If

    Public ReadOnly Property Attributes() As Attributes Implements IAttributableDeclaration.Attributes
        Get
            Return m_Attributes
        End Get
    End Property

End Class