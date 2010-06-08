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
''' RaiseEventStatement  ::= "RaiseEvent" IdentifierOrKeyword [ "(" [ ArgumentList ] ")" ] StatementTerminator
''' 
''' The simple name expression in a RaiseEvent statement is interpreted as a member lookup on Me. 
''' Thus, RaiseEvent x is interpreted as if it were RaiseEvent Me.x. The result of the expression must be 
''' classified as an event access for an event defined in the class itself; events defined on 
''' base types cannot be used in a RaiseEvent statement.
''' </summary>
''' <remarks></remarks>
Public Class RaiseEventStatement
    Inherits Statement

    Private m_Event As SimpleNameExpression
    Private m_Arguments As ArgumentList

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        If m_Event IsNot Nothing Then result = m_Event.ResolveTypeReferences AndAlso result
        If m_Arguments IsNot Nothing Then result = m_Arguments.ResolveTypeReferences AndAlso result

        Return result
    End Function

    ReadOnly Property [Event]() As SimpleNameExpression
        Get
            Return m_Event
        End Get
    End Property

    ReadOnly Property Arguments() As ArgumentList
        Get
            Return m_Arguments
        End Get
    End Property

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal [Event] As SimpleNameExpression, ByVal Arguments As ArgumentList)
        m_Event = [Event]
        m_Arguments = Arguments
    End Sub

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        Helper.Assert(m_Event.Classification.IsEventAccessClassification)

        Dim cl As EventAccessClassification = m_Event.Classification.AsEventAccess
        Dim eventtp As Mono.Cecil.TypeReference = cl.EventType
        Dim delegatetp As Mono.Cecil.TypeDefinition = CecilHelper.FindDefinition(cl.Type)

        Helper.Assert(delegatetp IsNot Nothing)

        Dim raiseMethod As Mono.Cecil.MethodReference
        raiseMethod = CecilHelper.FindDefinition(cl.EventInfo).InvokeMethod
        If raiseMethod IsNot Nothing Then
            'Call the raise method
            Return Compiler.Report.ShowMessage(Messages.VBNC99997, Location)
        Else
            'Manually raise the event
            Dim delegateVar As Mono.Cecil.Cil.VariableDefinition
            Dim endIfLabel As Label
            Dim eventDeclaration As RegularEventDeclaration
            Dim eventdesc As Mono.Cecil.EventDefinition
            Dim invokemethod As Mono.Cecil.MethodReference

            delegateVar = Emitter.DeclareLocal(Info, Helper.GetTypeOrTypeBuilder(Compiler, delegatetp))
            endIfLabel = Emitter.DefineLabel(Info)
            eventdesc = CecilHelper.FindDefinition(cl.EventInfo)
            eventDeclaration = DirectCast(eventdesc.Annotations(Compiler), RegularEventDeclaration)
            invokemethod = Helper.GetInvokeMethod(Compiler, delegatetp)

            'Helper.Assert(eventDeclaration IsNot Nothing)
            'Helper.Assert(TypeOf cl.EventInfo Is EventDescriptor)
            'Helper.Assert(Helper.CompareType(cl.EventType, Me.FindFirstParent(Of IType).CecilType))
            Helper.Assert(invokemethod IsNot Nothing)

            'Load the field of the variable
            If eventDeclaration.EventField.IsStatic = False Then
                Emitter.EmitLoadMe(Info, cl.EventType)
            End If
            Emitter.EmitLoadVariable(Info, eventDeclaration.EventField)

            'Test if the field is nothing
            Emitter.EmitStoreVariable(Info, delegateVar)
            Emitter.EmitLoadVariable(Info, delegateVar)
            'If the field is nothing, don't invoke anything.
            Emitter.EmitBranchIfFalse(Info, endIfLabel)

            'Load the field again
            Emitter.EmitLoadVariable(Info, delegateVar)
            'Load the invoke arguments
            result = m_Arguments.GenerateCode(Info.Clone(Me, True), invokemethod.Parameters) AndAlso result
            'Call the invoke method.
            Emitter.EmitCallOrCallVirt(Info, invokemethod)

            'End of the RaiseEvent statement.
            Emitter.MarkLabel(Info, endIfLabel)
        End If

        Return result
    End Function

    Public Overrides Function ResolveStatement(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True


        result = m_Event.ResolveExpression(Info) AndAlso result

        result = m_Arguments.ResolveCode(Info) AndAlso result

        Compiler.Helper.AddCheck("The result of the expression must be classified as an event access for an event defined in the class itself; ")
        Return result
    End Function

End Class
