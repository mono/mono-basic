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
#Const DEBUGMETHODACCESS = 0
#End If

''' <summary>
''' Represents a member, either in the parse tree of the MethodInfo type.
''' </summary>
''' <remarks></remarks>
Public Class MethodDescriptor
    Inherits MethodInfo
    Implements IMemberDescriptor

    Private m_Declaration As MethodDeclaration
    Private m_Parent As ParsedObject

    <Diagnostics.Conditional("DEBUGMETHODACCESS")> _
        Protected Sub DumpMethodInfo(Optional ByVal ReturnValue As Object = Nothing)
#If DEBUGMETHODACCESS Then
        Dim m As New Diagnostics.StackFrame(1)
        Dim str As String

        Dim i As INameable = m_Declaration
        Dim name As String = i.FullName
        str = " Called: (" & name & "): MethodInfo."

        If ReturnValue IsNot Nothing Then
            i.Compiler.Report.WriteLine(Report.ReportLevels.Debug, str & m.GetMethod.Name & " with return value: " & ReturnValue.ToString)
        Else
            i.Compiler.Report.WriteLine(Report.ReportLevels.Debug, str & m.GetMethod.Name)
        End If
#End If
    End Sub

    Overridable ReadOnly Property IsShared() As Boolean Implements IMemberDescriptor.IsShared
        Get
            Return m_Declaration.IsShared
        End Get
    End Property

    ReadOnly Property Compiler() As Compiler
        Get
            Return m_Parent.Compiler
        End Get
    End Property

    ReadOnly Property Parent() As ParsedObject
        Get
            Return m_Parent
        End Get
    End Property

    Private ReadOnly Property Declaration2() As IMember Implements IMemberDescriptor.Declaration
        Get
            Return m_Declaration
        End Get
    End Property

    Public ReadOnly Property Declaration() As MethodDeclaration
        Get
            Return m_Declaration
        End Get
    End Property

    Overridable ReadOnly Property MethodInReflection() As MethodInfo
        Get
            Return m_Declaration.MethodBuilder
        End Get
    End Property

    Sub New(ByVal Parent As ParsedObject)
        m_Parent = Parent
        m_Declaration = TryCast(Parent, MethodDeclaration)
    End Sub

    Public Overrides ReadOnly Property Attributes() As System.Reflection.MethodAttributes
        Get
            Dim result As MethodAttributes

            result = m_Declaration.Modifiers.GetMethodAttributeScope

            'If Modifiers.IsNothing(m_Declaration.Modifiers) = False Then
            If m_Declaration.IsShared Then
                result = result Or MethodAttributes.Static
            End If
            If m_Declaration.Modifiers.Is(ModifierMasks.MustOverride) Then
                If m_Declaration.Modifiers.Is(ModifierMasks.Overrides) = False Then
                    result = result Or MethodAttributes.NewSlot
                End If
                result = result Or MethodAttributes.Abstract Or MethodAttributes.Virtual Or MethodAttributes.CheckAccessOnOverride
            End If
            If m_Declaration.Modifiers.Is(ModifierMasks.NotOverridable) Then
                result = result Or MethodAttributes.Final
            End If
            If m_Declaration.Modifiers.Is(ModifierMasks.Overridable) Then
                result = result Or MethodAttributes.NewSlot Or MethodAttributes.Virtual Or MethodAttributes.CheckAccessOnOverride
            End If
            If m_Declaration.Modifiers.Is(ModifierMasks.Overrides) Then
                result = result Or MethodAttributes.Virtual Or MethodAttributes.CheckAccessOnOverride
            End If
            If m_Declaration.Modifiers.Is(ModifierMasks.Overloads) Then
                result = result Or MethodAttributes.HideBySig
            End If
            'End If

            If TypeOf m_Declaration.Parent Is PropertyDeclaration Then
                result = result Or MethodAttributes.SpecialName
            End If

            If TypeOf m_Declaration Is ExternalSubDeclaration Then
                result = result Or MethodAttributes.Static
            End If

            If m_Declaration.HandlesOrImplements IsNot Nothing Then
                If m_Declaration.HandlesOrImplements.ImplementsClause IsNot Nothing Then
                    result = result Or MethodAttributes.Virtual Or MethodAttributes.CheckAccessOnOverride
                    If m_Declaration.Modifiers.Is(ModifierMasks.Overrides) = False Then
                        result = result Or MethodAttributes.NewSlot
                    End If
                    If m_Declaration.Modifiers.Is(ModifierMasks.Overridable) = False AndAlso m_Declaration.Modifiers.Is(ModifierMasks.MustOverride) = False AndAlso m_Declaration.Modifiers.Is(ModifierMasks.Overrides) = False Then
                        result = result Or MethodAttributes.Final
                    End If
                End If
            End If

            If TypeOf m_Declaration.Parent Is EventDeclaration Then
                If DirectCast(m_Declaration.Parent, EventDeclaration).ImplementsClause IsNot Nothing Then
                    result = result Or MethodAttributes.Virtual Or MethodAttributes.NewSlot Or MethodAttributes.CheckAccessOnOverride
                End If
            End If

            If m_Declaration.DeclaringType.IsInterface Then
                result = result Or MethodAttributes.Abstract Or MethodAttributes.Virtual Or MethodAttributes.CheckAccessOnOverride Or MethodAttributes.NewSlot
            End If
            If TypeOf m_Declaration Is OperatorDeclaration OrElse TypeOf m_Declaration Is ConversionOperatorDeclaration Then
                result = result Or MethodAttributes.SpecialName
            ElseIf TypeOf m_Declaration Is EventHandlerDeclaration Then
                result = result Or MethodAttributes.SpecialName
            End If


            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property DeclaringType() As System.Type
        Get
            Dim result As Type
            result = m_Declaration.FindFirstParent(Of IType).TypeDescriptor
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides Function GetBaseDefinition() As System.Reflection.MethodInfo
        DumpMethodInfo()
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return Nothing
    End Function

    Public Overloads Overrides Function GetCustomAttributes(ByVal inherit As Boolean) As Object()
        DumpMethodInfo()
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return Nothing
    End Function

    Public Overloads Overrides Function GetCustomAttributes(ByVal attributeType As System.Type, ByVal inherit As Boolean) As Object()
        Dim result As Object()

        result = Helper.FilterCustomAttributes(attributeType, inherit, m_Declaration)

        DumpMethodInfo(result)

        Return result
    End Function

    Public Overrides Function GetMethodImplementationFlags() As System.Reflection.MethodImplAttributes
        DumpMethodInfo()
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return Nothing
    End Function

    Public Overrides Function GetParameters() As System.Reflection.ParameterInfo()
        Dim result As ParameterInfo() = m_Declaration.GetParameters
        DumpMethodInfo(result)
        Return result
    End Function

    Public Overloads Overrides Function Invoke(ByVal obj As Object, ByVal invokeAttr As System.Reflection.BindingFlags, ByVal binder As System.Reflection.Binder, ByVal parameters() As Object, ByVal culture As System.Globalization.CultureInfo) As Object
        DumpMethodInfo()
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return Nothing
    End Function

    Public Overrides Function IsDefined(ByVal attributeType As System.Type, ByVal inherit As Boolean) As Boolean
        DumpMethodInfo()
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return Nothing
    End Function

    Public Overrides ReadOnly Property MethodHandle() As System.RuntimeMethodHandle
        Get
            DumpMethodInfo()
            Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
            Return Nothing
        End Get
    End Property

    Public Overrides ReadOnly Property Name() As String
        Get
            Dim result As String = m_Declaration.Name
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property ReflectedType() As System.Type
        Get
            Dim result As Type = DeclaringType
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property ReturnTypeCustomAttributes() As System.Reflection.ICustomAttributeProvider
        Get
            DumpMethodInfo()
            Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
            Return Nothing
        End Get
    End Property

    Public Overrides ReadOnly Property ReturnType() As System.Type
        Get
            Dim result As Type = m_Declaration.Signature.ReturnType
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property CallingConvention() As System.Reflection.CallingConventions
        Get
            Dim result As System.Reflection.CallingConventions
            If m_Declaration.IsShared Then
                result = CallingConventions.Standard
            Else
                result = CallingConventions.Standard Or CallingConventions.HasThis
            End If
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property ContainsGenericParameters() As Boolean
        Get
            Dim result As Boolean
            Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
            result = MyBase.ContainsGenericParameters
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        Dim result As Boolean

        If obj Is Me Then
            result = True
        Else
            result = False
        End If
        DumpMethodInfo(result)
        Return result
    End Function

    Public Overrides Function GetGenericArguments() As System.Type()
        Dim result As Type()

        If m_Declaration.Signature.TypeParameters IsNot Nothing Then
            result = m_Declaration.Signature.TypeParameters.Parameters.AsTypeArray
        Else
            result = Type.EmptyTypes
        End If

        DumpMethodInfo(result)
        Return result
    End Function
    Public Overrides Function GetGenericMethodDefinition() As System.Reflection.MethodInfo
        DumpMethodInfo()
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return MyBase.GetGenericMethodDefinition()
    End Function

    Public Overrides Function GetHashCode() As Integer
        Dim result As Integer

        result = MyBase.GetHashCode()
        DumpMethodInfo(result)

        Return result
    End Function

    Public Overrides Function GetMethodBody() As System.Reflection.MethodBody
        DumpMethodInfo()
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return MyBase.GetMethodBody()
    End Function

    Public Overrides ReadOnly Property IsGenericMethod() As Boolean
        Get
            Dim result As Boolean

            Helper.Assert(m_Declaration IsNot Nothing)
            Helper.Assert(m_Declaration.Signature IsNot Nothing)

            If m_Declaration.Signature.TypeParameters IsNot Nothing Then
                result = m_Declaration.Signature.TypeParameters.Parameters.Count > 0
            Else
                result = False
            End If
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property IsGenericMethodDefinition() As Boolean
        Get
            Dim result As Boolean
            result = False
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides Function MakeGenericMethod(ByVal ParamArray typeArguments() As System.Type) As System.Reflection.MethodInfo
        Dim result As MethodInfo

        result = Parent.Compiler.TypeManager.MakeGenericMethod(Me.Parent, Me, Me.GetGenericArguments, typeArguments)

        DumpMethodInfo(result)

        Return result
    End Function

    Public Overrides ReadOnly Property MemberType() As System.Reflection.MemberTypes
        Get
            Dim result As MemberTypes
            result = MemberTypes.Method
            DumpMethodInfo(result)
            Return result
        End Get
    End Property
    Public Overrides ReadOnly Property MetadataToken() As Integer
        Get
            DumpMethodInfo()
            Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
            Return MyBase.MetadataToken
        End Get
    End Property
    Public Overrides ReadOnly Property [Module]() As System.Reflection.Module
        Get
            DumpMethodInfo()
            Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
            Return MyBase.[Module]
        End Get
    End Property
    Public Overrides ReadOnly Property ReturnParameter() As System.Reflection.ParameterInfo
        Get
            Dim result As ParameterInfo = m_Declaration.Signature.ReturnParameter
            DumpMethodInfo(result)
            Return result
        End Get
    End Property
    Public Overrides Function ToString() As String
        DumpMethodInfo()
        Return MyBase.ToString()
    End Function
End Class
