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
''' Used to produce a method pointer.
''' Classification: MethodPointer
''' 
''' AddressOfExpression  ::= "AddressOf" Expression
''' 
''' An AddressOf expression is used to produce a method pointer. The expression consists of 
''' the AddressOf keyword and an expression that must be classified as a method group. The 
''' method group cannot be late-bound and it cannot refer to constructors.
''' 
''' The result is classified as a method pointer and the associated instance expression and type
''' argument list (if any) is the same as the associated instance expression and type argument 
''' list of the method group.
''' </summary>
''' <remarks></remarks>
Public Class AddressOfExpression
    Inherits Expression

    ''' <summary>
    ''' This expression must be classified as MethodGroup.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_Expression As Expression
    Private m_ExpressionType As Type

    Public Overrides Function ResolveTypeReferences() As Boolean
        Return m_Expression.ResolveTypeReferences
    End Function

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Expression As Expression)
        MyBase.New(Parent)
        m_Expression = Expression
    End Sub

    Sub Init(ByVal Expression As Expression)
        m_Expression = Expression
    End Sub

    Shared Function IsMe(ByVal tm As tm) As Boolean
        Return tm.CurrentToken.Equals(KS.AddressOf)
    End Function

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = m_Expression.ResolveExpression(New ResolveInfo(Info.Compiler, True)) AndAlso result

        If m_Expression.Classification.IsMethodGroupClassification Then
            Dim mpClassification As MethodPointerClassification
            mpClassification = New MethodPointerClassification(Me, m_Expression.Classification.AsMethodGroupClassification)
            Classification = mpClassification

            'Try to find the desired type of the addressof expression.
            Dim dri As DelegateResolveInfo = TryCast(Info, DelegateResolveInfo)
            If dri IsNot Nothing Then
                m_ExpressionType = dri.DelegateType
            Else
                Dim assign As AssignmentStatement
                assign = TryCast(Me.Parent, AssignmentStatement)
                If assign IsNot Nothing Then
                    If assign.RSide Is Me = False Then Throw New InternalException(Me)
                    m_ExpressionType = assign.LSide.ExpressionType
                Else
                    Dim aor As AddOrRemoveHandlerStatement = TryCast(Me.Parent, AddOrRemoveHandlerStatement)
                    Dim al As ArgumentList = TryCast(Me.Parent.Parent, ArgumentList)
                    If aor IsNot Nothing Then
                        If aor.EventHandler Is Me = False Then Throw New InternalException(Me)
                        If aor.Event.Classification.IsEventAccessClassification Then
                            m_ExpressionType = aor.Event.Classification.AsEventAccess.EventInfo.EventHandlerType
                        Else
                            Helper.AddError("(This message should probably be ignored, this is a compile time error to get to this situation, but the error should already have been shown)")
                        End If
                    ElseIf al IsNot Nothing Then
                        Dim doc As DelegateOrObjectCreationExpression = TryCast(al.Parent, DelegateOrObjectCreationExpression)
                        If doc.IsDelegateCreationExpression Then
                            Dim deltp As Type = doc.NonArrayTypeName.ResolvedType
                            m_ExpressionType = deltp
                        Else
                            Helper.AddError()
                        End If
                    Else
                        Helper.NotImplemented()
                    End If
                End If
            End If

            result = mpClassification.Resolve(m_ExpressionType) AndAlso result
        Else
            Helper.AddError()
        End If

        Return result
    End Function

    Overrides ReadOnly Property ExpressionType() As Type
        Get
            If MyBase.IsResolved Then
                Return m_ExpressionType
            Else
                Throw New InternalException(Me)
            End If
        End Get
    End Property

#If DEBUG Then
    Public Overrides Sub Dump(ByVal Dumper As IndentedTextWriter)
        Dumper.Write("AddressOf ")
        m_Expression.Dump(Dumper)
    End Sub
#End If

End Class
