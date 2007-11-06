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


#If DEBUG Then
#Const DEBUGTYPEACCESS = 0
#End If

Public Class TypeParameterDescriptor
    Inherits TypeDescriptor

    Private m_TypeParameter As TypeParameter

    Sub New(ByVal TypeParameter As TypeParameter)
        MyBase.new(TypeParameter)
        m_TypeParameter = TypeParameter
    End Sub

    ''' <summary>
    ''' Gets the Reflection.Emit created type for this descriptor.
    ''' It is a TypeParameterBuilder
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides ReadOnly Property TypeInReflection() As System.Type
        Get
            Dim result As Type

            Helper.Assert(m_TypeParameter IsNot Nothing)
            result = m_TypeParameter.TypeParameterBuilder

            Return result
        End Get
    End Property

    Public Overrides Function Equals(ByVal o As System.Type) As Boolean
        If o Is Nothing Then Return False

        If m_TypeParameter.TypeParameterBuilder IsNot Nothing AndAlso m_TypeParameter.TypeParameterBuilder Is o Then Return True

        Dim oD As TypeParameterDescriptor = TryCast(o, TypeParameterDescriptor)

        If oD Is Nothing Then Return False
        If Helper.CompareNameOrdinal(oD.Name, Name) = False Then Return False
        If oD.DeclaringMethod Is Nothing Xor DeclaringMethod Is Nothing Then Return False
        If oD.DeclaringType Is Nothing Xor DeclaringType Is Nothing Then Return False
        If DeclaringMethod IsNot Nothing Then
            Return DeclaringMethod Is oD.DeclaringMethod
        ElseIf DeclaringType IsNot Nothing Then
            Return Helper.CompareType(DeclaringType, oD.DeclaringType)
        Else
            Throw New InternalException
        End If
    End Function

    Public Overrides Function GetMembers(ByVal bindingAttr As System.Reflection.BindingFlags) As System.Reflection.MemberInfo()
        Dim result As MemberInfo() = Nothing
        Dim tmpResult As New Generic.List(Of MemberInfo)

        tmpResult.AddRange(Compiler.TypeCache.System_Object.GetMembers(bindingAttr))
        If m_TypeParameter.TypeParameterConstraints Is Nothing OrElse m_TypeParameter.TypeParameterConstraints.Constraints.Count = 0 Then
            'tmpResult.AddRange(Compiler.TypeCache.Object.GetMembers(bindingAttr))
        Else
            For Each constraint As Constraint In m_TypeParameter.TypeParameterConstraints.Constraints
                Select Case constraint.SpecialConstraintAttribute
                    Case Reflection.GenericParameterAttributes.None
                        tmpResult.AddRange(constraint.TypeName.ResolvedType.GetMembers(bindingAttr))
                    Case Reflection.GenericParameterAttributes.DefaultConstructorConstraint
                        'Nothing to do
                    Case Reflection.GenericParameterAttributes.NotNullableValueTypeConstraint
                        'Nothing to do
                    Case Reflection.GenericParameterAttributes.ReferenceTypeConstraint
                        'Nothing to do
                    Case Else
                        Throw New InternalException(Me.Declaration)
                End Select
            Next
        End If

        result = tmpResult.ToArray

        DumpMethodInfo(result)
        Return result
    End Function

    Public Overrides Function GetInterfaces() As System.Type()
        Return Type.EmptyTypes
    End Function

    ReadOnly Property TypeParameter() As TypeParameter
        Get
            Return m_TypeParameter
        End Get
    End Property

    Public Overrides ReadOnly Property GenericParameterAttributes() As System.Reflection.GenericParameterAttributes
        Get
            Dim result As GenericParameterAttributes
            result = m_TypeParameter.GenericParameterAttributes
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides Function GetGenericParameterConstraints() As System.Type()
        Dim result() As Type
        result = m_TypeParameter.GetGenericParameterConstraints()
        DumpMethodInfo(result)
        Return result
    End Function

    Public Overrides ReadOnly Property GenericParameterPosition() As Integer
        Get
            Dim result As Integer
            result = m_TypeParameter.GenericParameterPosition
            DumpMethodInfo(result)
            Return result
        End Get
    End Property


    Public Overrides ReadOnly Property IsGenericParameter() As Boolean
        Get
            Dim result As Boolean
            result = True
            DumpMethodInfo(result)
            Return result
        End Get
    End Property


    Public Overrides ReadOnly Property IsGenericType() As Boolean
        Get
            Dim result As Boolean

            result = False

            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    ''' <summary>
    ''' A hack to prevent the debugger to crash when inspecting descriptors.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shadows ReadOnly Property IsVisible() As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides ReadOnly Property IsGenericTypeDefinition() As Boolean
        Get
            Dim result As Boolean

            result = False

            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property DeclaringType() As System.Type
        Get
            Dim result As Type

            result = m_TypeParameter.FindFirstParent(Of IType).TypeDescriptor

            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property FullName() As String
        Get
            Dim result As String
            'DOC: Gets a null reference (Nothing in Visual Basic) in all cases.
            result = Nothing

            DumpMethodInfo(result)

            Return result
        End Get
    End Property

    Protected Overrides Function GetAttributeFlagsImpl() As System.Reflection.TypeAttributes
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return TypeAttributes.AnsiClass
        Throw New NotSupportedException
        Return TypeAttributes.AnsiClass
    End Function

    Public Overrides Function MakeArrayType(ByVal rank As Integer) As System.Type
        Dim result As Type = Nothing
        If m_TypeParameter IsNot Nothing Then
            result = New ArrayTypeDescriptor(Me, rank)
        Else
            Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        End If
        'Needs to add this to a cache, otherwise two otherwise equal types might be created with two different 
        'type instances, which is not good is any type comparison would fail.
        Static cache As New Generic.Dictionary(Of String, Type)(Helper.StringComparer)
        If cache.ContainsKey(result.Name) Then
            result = cache.Item(result.Name)
        Else
            cache.Add(result.Name, result)
        End If
        DumpMethodInfo(result)
        Return result
    End Function

    Overrides ReadOnly Property Name() As String
        Get
            Dim result As String
            result = m_TypeParameter.Name
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property Assembly() As System.Reflection.Assembly
        Get
            Dim result As System.Reflection.Assembly
            result = m_TypeParameter.Compiler.AssemblyBuilder
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property BaseType() As System.Type
        Get
            Dim result As Type = Nothing

            Helper.Assert(m_TypeParameter IsNot Nothing)
            ' Helper.Assert(m_TypeParameter.TypeParameterConstraints IsNot Nothing)

            If m_TypeParameter.TypeParameterConstraints IsNot Nothing Then
                result = m_TypeParameter.TypeParameterConstraints.ClassConstraint
            End If

            If result Is Nothing Then
                result = Compiler.TypeCache.System_Object
            End If

            DumpMethodInfo(result)
            Return result
        End Get
    End Property


    Public Overrides ReadOnly Property [Module]() As System.Reflection.Module
        Get
            Dim result As Reflection.Module

            result = m_TypeParameter.Compiler.ModuleBuilder

            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property [Namespace]() As String
        Get
            Dim result As String
            'DOC: Gets a null reference (Nothing in Visual Basic) in all cases.
            result = Nothing
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property UnderlyingSystemType() As System.Type
        Get
            Dim result As Type
            Helper.Assert(m_TypeParameter.TypeParameterBuilder IsNot Nothing)
            result = m_TypeParameter.TypeParameterBuilder.UnderlyingSystemType
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property DeclaringMethod() As System.Reflection.MethodBase
        Get
            Dim result As MethodBase

            Dim method As IMethod = m_TypeParameter.FindFirstParent(Of IMethod)()
            If method Is Nothing Then
                result = Nothing
            Else
                result = method.MethodDescriptor
            End If

            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="c"></param>
    ''' <returns>Return Value
    ''' true if the c parameter and the current Type represent the same type, or if the current Type is in 
    ''' the inheritance hierarchy of c, or if the current Type is an interface that c supports. false if 
    ''' none of  these conditions are the case, or if c is a null reference (Nothing in Visual Basic).
    ''' </returns>
    ''' <remarks></remarks>
    Public Overrides Function IsAssignableFrom(ByVal c As System.Type) As Boolean
        Dim result As Boolean

        Dim dtpb As GenericTypeParameterBuilder = TryCast(c, GenericTypeParameterBuilder)
        If dtpb IsNot Nothing Then
            result = dtpb Is m_TypeParameter.TypeParameterBuilder
        Else
            Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        End If

        DumpMethodInfo(result)
        Return result
    End Function

    Protected Overrides Function HasElementTypeImpl() As Boolean
        Dim result As Boolean

        result = False
        DumpMethodInfo(result)
        Return result
    End Function

End Class
