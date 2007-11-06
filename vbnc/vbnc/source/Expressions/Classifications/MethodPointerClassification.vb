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

    Private m_ResolvedMethod As MethodBase
    Private m_DelegateType As Type

    Private m_Resolved As Boolean

    ReadOnly Property Resolved() As Boolean
        Get
            Return m_resolved
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

        If m_MethodGroup.InstanceExpression IsNot Nothing Then
            result = m_MethodGroup.InstanceExpression.GenerateCode(Info.Clone(Parent, True, False, m_MethodGroup.InstanceExpression.ExpressionType)) AndAlso result
            Emitter.EmitDup(Info)
        Else
            Emitter.EmitLoadNull(Info.Clone(Parent, True, False, Compiler.TypeCache.System_Object))
        End If

        Emitter.EmitLoadVftn(Info, m_ResolvedMethod)

        Dim ctor As ConstructorInfo
        ctor = m_DelegateType.GetConstructor(BindingFlags.Instance Or BindingFlags.Public Or BindingFlags.DeclaredOnly, Nothing, New Type() {Compiler.TypeCache.System_Object, Compiler.TypeCache.System_IntPtr}, Nothing)
        Emitter.EmitNew(Info, ctor)

        Return result
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Resolve(ByVal DelegateType As Type) As Boolean
        Dim result As Boolean = True

        Helper.Assert(DelegateType IsNot Nothing)

        If Helper.CompareType(DelegateType, Compiler.TypeCache.DelegateUnresolvedType) Then
            m_DelegateType = DelegateType
            Return True
        End If

        If Helper.IsDelegate(Compiler, DelegateType) = False Then
            result = Compiler.Report.ShowMessage(Messages.VBNC30581, Me.Parent.Location, DelegateType.FullName) AndAlso result
        End If

        If result = False Then Return result

        Dim params() As ParameterInfo = Helper.GetDelegateArguments(Compiler, DelegateType)
        Dim paramtypes() As Type = Helper.GetParameterTypes(params)

        m_ResolvedMethod = CType(Helper.ResolveGroupExact(Me.Parent, m_MethodGroup.Group, paramtypes), MethodBase)
        m_DelegateType = DelegateType

        result = m_ResolvedMethod IsNot Nothing AndAlso result

        m_Resolved = True

        Return result
    End Function

    ReadOnly Property DelegateType() As Type
        Get
            Return m_DelegateType
        End Get
    End Property

    ReadOnly Property MethodGroup() As MethodGroupClassification
        Get
            Return m_MethodGroup
        End Get
    End Property

    ReadOnly Property Type() As Type
        Get
            Return Compiler.TypeCache.System_IntPtr
        End Get
    End Property

    ReadOnly Property InstanceExpression() As Expression
        Get
            Return m_MethodGroup.InstanceExpression
        End Get
    End Property

    ReadOnly Property Method() As MethodInfo
        Get
            Return DirectCast(m_ResolvedMethod, MethodInfo)
        End Get
    End Property

    Sub New(ByVal Parent As AddressOfExpression, ByVal MethodGroup As MethodGroupClassification)
        MyBase.new(Classifications.MethodPointer, Parent)
        Helper.Assert(MethodGroup IsNot Nothing)
        m_MethodGroup = MethodGroup
    End Sub
End Class
