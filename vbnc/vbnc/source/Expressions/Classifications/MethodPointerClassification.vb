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
''' Represents the location of a method. A method pointer may have
''' an associated instance expression and an associated type argument list.
''' 
''' Can be reclassified as a value. This reclassification can only occur in
''' assignment stastements or as a part of interpreting a parameter list,
''' where the target type is known. The method pointer expression is interpreted
''' as the argument to a delegate instantiation expression of the appropiate type
''' with the associated type argument list.
''' </summary>
''' <remarks></remarks>
Public Class MethodPointerClassification
    Inherits ExpressionClassification

    Private m_TypeArguments As TypeArgumentList

    Private m_MethodGroup As MethodGroupClassification

    Private m_ResolvedMethod As Mono.Cecil.MethodReference
    Private m_DelegateType As Mono.Cecil.TypeReference

    Private m_Resolved As Boolean

    ReadOnly Property Resolved() As Boolean
        Get
            Return m_Resolved
        End Get
    End Property

    ''' <summary>
    ''' Loads the method pointer onto the evalation stack.
    ''' Creates a new delegate of the specified type.
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        Helper.Assert(m_ResolvedMethod IsNot Nothing)
        Helper.Assert(m_DelegateType IsNot Nothing)

        If m_MethodGroup.InstanceExpression IsNot Nothing AndAlso CecilHelper.IsStatic(m_ResolvedMethod) = False Then
            result = m_MethodGroup.InstanceExpression.GenerateCode(Info.Clone(Parent, True, False, m_MethodGroup.InstanceExpression.ExpressionType)) AndAlso result
            Emitter.EmitDup(Info)
        Else
            Emitter.EmitLoadNull(Info.Clone(Parent, True, False, Compiler.TypeCache.System_Object))
        End If

        Emitter.EmitLoadVftn(Info, m_ResolvedMethod)

        Dim ctor As Mono.Cecil.MethodReference
        Dim dT As Mono.Cecil.TypeDefinition = CecilHelper.FindDefinition(m_DelegateType)
        ctor = CecilHelper.FindConstructor(dT.Methods, False, New Mono.Cecil.TypeReference() {Compiler.TypeCache.System_Object, Compiler.TypeCache.System_IntPtr})
        ctor = CecilHelper.GetCorrectMember(ctor, m_DelegateType)
        Emitter.EmitNew(Info, ctor)

        Return result
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Resolve(ByVal DelegateType As Mono.Cecil.TypeReference, ByVal ShowErrors As Boolean) As Boolean
        Dim result As Boolean = True

        Helper.Assert(DelegateType IsNot Nothing)

        If Helper.CompareType(DelegateType, Compiler.TypeCache.DelegateUnresolvedType) Then
            m_DelegateType = DelegateType
            Return True
        End If

        If Helper.IsDelegate(Compiler, DelegateType) = False Then
            If ShowErrors Then
                Compiler.Report.ShowMessage(Messages.VBNC30581, Me.Parent.Location, DelegateType.FullName)
            End If
            Return False
        End If

        Dim params As Mono.Collections.Generic.Collection(Of ParameterDefinition) = Helper.GetDelegateArguments(Compiler, DelegateType)
        Dim paramtypes() As Mono.Cecil.TypeReference = Helper.GetParameterTypes(params)

        m_ResolvedMethod = CType(Helper.ResolveGroupExact(Me.Parent, m_MethodGroup.Group, paramtypes), Mono.Cecil.MethodReference)
        m_DelegateType = DelegateType

        If m_ResolvedMethod Is Nothing Then
            If ShowErrors Then
                For i As Integer = 0 To m_MethodGroup.Group.Count - 1
                    Compiler.Report.ShowMessage(Messages.VBNC30408, Me.Parent.Location, Helper.ToString(Me.Parent, m_MethodGroup.Group(i)), Helper.ToString(Me.Parent, DelegateType))
                Next
            End If
            result = False
        Else
            If m_MethodGroup.InstanceExpression Is Nothing AndAlso CecilHelper.IsStatic(m_ResolvedMethod) = False Then
                If ShowErrors Then
                    Compiler.Report.ShowMessage(Messages.VBNC30469, Parent.Location)
                End If
                Return False
            End If
        End If

        m_Resolved = True

        Return result
    End Function

    ReadOnly Property DelegateType() As Mono.Cecil.TypeReference
        Get
            Return m_DelegateType
        End Get
    End Property

    ReadOnly Property MethodGroup() As MethodGroupClassification
        Get
            Return m_MethodGroup
        End Get
    End Property

    ReadOnly Property Type() As Mono.Cecil.TypeReference
        Get
            Return Compiler.TypeCache.System_IntPtr
        End Get
    End Property

    ReadOnly Property InstanceExpression() As Expression
        Get
            Return m_MethodGroup.InstanceExpression
        End Get
    End Property

    ReadOnly Property Method() As Mono.Cecil.MethodReference
        Get
            Return DirectCast(m_ResolvedMethod, Mono.Cecil.MethodReference)
        End Get
    End Property

    Sub New(ByVal Parent As AddressOfExpression, ByVal MethodGroup As MethodGroupClassification)
        MyBase.new(Classifications.MethodPointer, Parent)
        Helper.Assert(MethodGroup IsNot Nothing)
        m_MethodGroup = MethodGroup
    End Sub
End Class

