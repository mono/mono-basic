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
#Const DEBUGCONSTRUCTORACCESS = 0
#End If
Public Class ConstructorDescriptor
    Inherits ConstructorInfo
    Implements IMemberDescriptor

    Private m_Declaration As ConstructorDeclaration
    Private m_Parent As ParsedObject

    <Diagnostics.Conditional("DEBUGCONSTRUCTORACCESS")> _
        Protected Sub DumpMethodInfo(Optional ByVal ReturnValue As Object = Nothing)
#If DEBUGCONSTRUCTORACCESS Then
        Dim m As New Diagnostics.StackFrame(1)
        Dim str As String

        Dim i As INameable = m_Declaration
        Dim name As String = ""
        If i IsNot Nothing Then name = i.FullName
        str = " Called: (" & name & "): ConstructorInfo."

        If ReturnValue IsNot Nothing Then
            Compiler.Report.WriteLine(Report.ReportLevels.Debug, str & m.GetMethod.Name & " with return value: " & ReturnValue.ToString)
        Else
            Compiler.Report.WriteLine(Report.ReportLevels.Debug, str & m.GetMethod.Name)
        End If
#End If
    End Sub

    ReadOnly Property IsShared() As Boolean Implements IMemberDescriptor.IsShared
        Get
            Return m_Declaration.IsShared
        End Get
    End Property

    ReadOnly Property Parent() As ParsedObject
        Get
            Return m_Parent
        End Get
    End Property

    ReadOnly Property Compiler() As Compiler
        Get
            Return m_Parent.Compiler
        End Get
    End Property

    ReadOnly Property Declaration() As ConstructorDeclaration
        Get
            Return m_Declaration
        End Get
    End Property

    Private ReadOnly Property Declaration2() As IMember Implements IMemberDescriptor.Declaration
        Get
            Return m_Declaration
        End Get
    End Property

    Sub New(ByVal Declaration As ConstructorDeclaration)
        m_Declaration = Declaration
        m_Parent = m_Declaration
    End Sub

    Protected Sub New(ByVal Parent As ParsedObject)
        m_Parent = Parent
        m_Declaration = TryCast(Parent, ConstructorDeclaration)
    End Sub

    Public Overrides ReadOnly Property Attributes() As System.Reflection.MethodAttributes
        Get
            Dim result As MethodAttributes
            result = m_Declaration.Attributes
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property DeclaringType() As System.Type
        Get
            Dim result As Type

            result = m_Parent.FindTypeParent.TypeDescriptor

            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overloads Overrides Function GetCustomAttributes(ByVal inherit As Boolean) As Object()
        Dim result As Object() = Nothing
        DumpMethodInfo(result)
        Compiler.Report.ShowMessage(Messages.VBNC99997, m_Declaration.Location)
        Return result
    End Function

    Public Overloads Overrides Function GetCustomAttributes(ByVal attributeType As System.Type, ByVal inherit As Boolean) As Object()
        Dim result As Object() = Nothing
        DumpMethodInfo(result)
        Compiler.Report.ShowMessage(Messages.VBNC99997, m_Declaration.Location)
        Return result
    End Function

    Public Overrides Function GetMethodImplementationFlags() As System.Reflection.MethodImplAttributes
        Dim result As MethodImplAttributes
        DumpMethodInfo(result)
        Compiler.Report.ShowMessage(Messages.VBNC99997, m_Declaration.Location)
        Return result
    End Function

    Public Overrides Function GetParameters() As System.Reflection.ParameterInfo()
        Dim result As ParameterInfo()
        result = m_Declaration.GetParameters
        DumpMethodInfo(result)
        Return result
    End Function

    Public Overloads Overrides Function Invoke(ByVal obj As Object, ByVal invokeAttr As System.Reflection.BindingFlags, ByVal binder As System.Reflection.Binder, ByVal parameters() As Object, ByVal culture As System.Globalization.CultureInfo) As Object
        Throw New NotSupportedException
    End Function

    Public Overloads Overrides Function Invoke(ByVal invokeAttr As System.Reflection.BindingFlags, ByVal binder As System.Reflection.Binder, ByVal parameters() As Object, ByVal culture As System.Globalization.CultureInfo) As Object
        Throw New NotSupportedException
    End Function

    Public Overrides Function IsDefined(ByVal attributeType As System.Type, ByVal inherit As Boolean) As Boolean
        Dim result As Boolean
        Compiler.Report.ShowMessage(Messages.VBNC99997, m_Declaration.Location)
        DumpMethodInfo(result)
        Return result
    End Function

    Public Overrides ReadOnly Property MethodHandle() As System.RuntimeMethodHandle
        Get
            Dim result As RuntimeMethodHandle
            Compiler.Report.ShowMessage(Messages.VBNC99997, m_Declaration.Location)
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property Name() As String
        Get
            Dim result As String
            result = m_Declaration.Name
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property ReflectedType() As System.Type
        Get
            Dim result As Type
            result = DeclaringType
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property CallingConvention() As System.Reflection.CallingConventions
        Get
            Dim result As CallingConventions
            If Me.IsStatic = False Then
                result = CallingConventions.HasThis Or CallingConventions.Standard
            Else
                result = CallingConventions.Standard
            End If
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property ContainsGenericParameters() As Boolean
        Get
            Dim result As Boolean
            result = False
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        Dim result As Boolean

        result = obj Is Me
        DumpMethodInfo(result)

        Return result
    End Function

    Public Overrides Function GetGenericArguments() As System.Type()
        Dim result As Type() = Type.EmptyTypes
        DumpMethodInfo(result)
        Return result
    End Function

    Public Overrides Function GetHashCode() As Integer
        Dim result As Integer
        Compiler.Report.ShowMessage(Messages.VBNC99997, m_Declaration.Location)
        DumpMethodInfo(result)
        Return result
    End Function
    Public Overrides Function GetMethodBody() As System.Reflection.MethodBody
        Dim result As MethodBody = Nothing
        Compiler.Report.ShowMessage(Messages.VBNC99997, m_Declaration.Location)
        DumpMethodInfo(result)
        Return result
    End Function
    Public Overrides ReadOnly Property IsGenericMethod() As Boolean
        Get
            Dim result As Boolean
            If m_Declaration.ConstructorBuilder IsNot Nothing Then
                result = m_Declaration.ConstructorBuilder.IsGenericMethod
            Else
                result = m_Declaration.Signature.TypeParameters IsNot Nothing AndAlso m_Declaration.Signature.TypeParameters.Parameters.Length > 0
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
    Public Overrides ReadOnly Property MemberType() As System.Reflection.MemberTypes
        Get
            Dim result As MemberTypes
            result = MemberTypes.Constructor
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property MetadataToken() As Integer
        Get
            Dim result As Integer
            Compiler.Report.ShowMessage(Messages.VBNC99997, m_Declaration.Location)
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property [Module]() As System.Reflection.Module
        Get
            Dim result As System.Reflection.Module
            result = m_Declaration.Compiler.ModuleBuilder
            DumpMethodInfo(result)
            Return result
        End Get
    End Property
    Public Overrides Function ToString() As String
        Dim result As String
        result = m_Declaration.Name
        DumpMethodInfo(result)
        Return result
    End Function

    Overridable ReadOnly Property ConstructorInReflection() As ConstructorInfo
        Get
            Return m_Declaration.ConstructorBuilder
        End Get
    End Property
End Class
