' 
' Visual Basic.Net Compiler
' Copyright (C) 2004 - 2010 Rolf Bjarne Kvinge, RKvinge@novell.com
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
''' ExitStatement  ::=  "Exit" ExitKind  StatementTerminator
''' ExitKind  ::=  "Do" | "For" | "While" | "Select" | "Sub" | "Function" | "Property" | "Try"
''' </summary>
''' <remarks></remarks>
Public Class ExitStatement
    Inherits Statement

    Private m_ExitWhat As KS

    Private m_Container As IBaseObject

    Public Overrides Function ResolveTypeReferences() As Boolean
        Return True
    End Function

    Sub New(ByVal Parent As ParsedObject, ByVal ExitWhat As KS)
        MyBase.New(Parent)
        m_ExitWhat = ExitWhat
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal ExitWhat As KS, Location As Span)
        MyBase.New(Parent, Location)
        m_ExitWhat = ExitWhat
    End Sub

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        Select Case m_ExitWhat
            Case KS.Sub
                Emitter.EmitRetOrLeave(Info, Me, False)
            Case KS.Function
                Dim func As FunctionDeclaration = TryCast(m_Container, FunctionDeclaration)
                If func IsNot Nothing Then
                    Emitter.EmitLoadVariable(Info, func.DefaultReturnVariable)
                    Emitter.EmitRetOrLeave(Info, Me, False)
                Else
                    Throw New InternalException(Me)
                End If
            Case KS.Property
                Dim propGet As PropertyGetDeclaration = TryCast(m_Container, PropertyGetDeclaration)
                Dim propSet As PropertySetDeclaration = TryCast(m_Container, PropertySetDeclaration)
                If propget IsNot Nothing Then
                    Emitter.EmitLoadVariable(Info, propGet.DefaultReturnVariable)
                    Emitter.EmitRetOrLeave(Info, Me, False)
                ElseIf propSet IsNot Nothing Then
                    Emitter.EmitRetOrLeave(Info, Me, False)
                Else
                    Throw New InternalException(Me)
                End If
            Case KS.Select
                Dim destinationStmt As SelectStatement
                destinationStmt = DirectCast(m_Container, SelectStatement)
                Emitter.EmitBranchOrLeave(Info, destinationStmt.EndLabel, Me, destinationStmt)
            Case KS.While, KS.Do, KS.For, KS.Try
                Dim destinationStmt As BlockStatement
                destinationStmt = TryCast(m_Container, BlockStatement)
                If destinationStmt IsNot Nothing Then
                    Dim lblStatement As Statement
                    lblStatement = destinationStmt.FindFirstParent(Of Statement)()
                    Emitter.EmitBranchOrLeave(Info, destinationStmt.EndLabel, Me, lblStatement)
                Else
                    Throw New InternalException(Me)
                End If
            Case Else
                Return Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Location)
        End Select

        Return result
    End Function

    Public Overrides Function ResolveStatement(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        Select Case m_ExitWhat
            Case KS.Sub
                m_Container = Me.FindFirstParent(Of IMethod)()
                If m_Container Is Nothing Then
                    result = Compiler.Report.ShowMessage(Messages.VBNC30065, Location) AndAlso result
                ElseIf (TypeOf m_Container Is SubDeclaration = False AndAlso TypeOf m_Container Is ConstructorDeclaration = False) OrElse TypeOf m_Container Is FunctionDeclaration = True Then
                    result = Compiler.Report.ShowMessage(Messages.VBNC30065, Location) AndAlso result
                End If
            Case KS.Property
                m_Container = Me.FindFirstParent(Of IMethod)()
                If m_Container Is Nothing Then
                    result = Compiler.Report.ShowMessage(Messages.VBNC30066, Location) AndAlso result
                ElseIf TypeOf m_Container Is PropertyDeclaration = False AndAlso TypeOf m_Container Is PropertyHandlerDeclaration = False Then
                    result = Compiler.Report.ShowMessage(Messages.VBNC30066, Location) AndAlso result
                End If
            Case KS.Function
                m_Container = Me.FindFirstParent(Of IMethod)()
                If m_Container Is Nothing Then
                    result = Compiler.Report.ShowMessage(Messages.VBNC30067, Location) AndAlso result
                ElseIf TypeOf m_Container Is FunctionDeclaration = False Then
                    result = Compiler.Report.ShowMessage(Messages.VBNC30067, Location) AndAlso result
                End If
            Case KS.Do
                m_Container = Me.FindFirstParent(Of DoStatement)()
                If m_Container Is Nothing Then Compiler.Report.ShowMessage(Messages.VBNC30089, Location)
            Case KS.Try
                m_Container = Me.FindFirstParent(Of TryStatement)()
                If m_Container Is Nothing Then Compiler.Report.ShowMessage(Messages.VBNC30393, Location)
            Case KS.For
                m_Container = Me.FindFirstParent(Of ForStatement, ForEachStatement)()
                If m_Container Is Nothing Then Compiler.Report.ShowMessage(Messages.VBNC30096, Location)
            Case KS.While
                m_Container = Me.FindFirstParent(Of WhileStatement)()
                If m_Container Is Nothing Then Compiler.Report.ShowMessage(Messages.VBNC30097, Location)
            Case KS.Select
                m_Container = Me.FindFirstParent(Of SelectStatement)()
                If m_Container Is Nothing Then Compiler.Report.ShowMessage(Messages.VBNC30099, Location)
            Case Else
                Throw New InternalException(Me)
        End Select

        result = m_Container IsNot Nothing AndAlso result

        Return result
    End Function

    ReadOnly Property ExitWhat() As KS
        Get
            Return m_ExitWhat
        End Get
    End Property
End Class
