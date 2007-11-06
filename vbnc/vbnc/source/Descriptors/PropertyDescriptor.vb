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
#Const DEBUGPROPERTYACCESS = 0
#End If

Public Class PropertyDescriptor
    Inherits PropertyInfo
    Implements IMemberDescriptor

    Private m_Declaration As PropertyDeclaration
    Private m_Parent As ParsedObject

    <Diagnostics.Conditional("DEBUGPROPERTYACCESS")> _
         Protected Sub DumpMethodInfo(Optional ByVal ReturnValue As Object = Nothing)
#If DEBUGPROPERTYACCESS Then
        Dim m As New Diagnostics.StackFrame(1)
        Dim str As String

        Dim name As String = m_Declaration.FullName
        str = " Called: (" & name & "): PropertyInfo."

        If ReturnValue IsNot Nothing Then
            m_Declaration.Compiler.Report.WriteLine(Report.ReportLevels.Debug, str & m.GetMethod.Name & " with return value: " & ReturnValue.ToString)
        Else
            m_Declaration.Compiler.Report.WriteLine(Report.ReportLevels.Debug, str & m.GetMethod.Name)
        End If
#End If
    End Sub

    Overridable ReadOnly Property IsDefault() As Boolean
        Get
            Return m_Declaration.Modifiers.Is(ModifierMasks.Default)
        End Get
    End Property

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

    Overridable ReadOnly Property PropertyInReflection() As PropertyInfo
        Get
            Return m_Declaration.PropertyBuilder
        End Get
    End Property

    ReadOnly Property PropertyDeclaration() As PropertyDeclaration
        Get
            Return DirectCast(m_Declaration, PropertyDeclaration)
        End Get
    End Property

    Public ReadOnly Property Declaration() As IMember Implements IMemberDescriptor.Declaration
        Get
            Return m_Declaration
        End Get
    End Property

    Sub New(ByVal Parent As ParsedObject)
        m_Declaration = TryCast(Parent, PropertyDeclaration)
        m_Parent = Parent
    End Sub

    Public Overrides ReadOnly Property Attributes() As System.Reflection.PropertyAttributes
        Get
            Dim result As PropertyAttributes = PropertyAttributes.None
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property CanRead() As Boolean
        Get
            Dim result As Boolean
            result = Not m_Declaration.Modifiers.Is(ModifierMasks.WriteOnly)
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property CanWrite() As Boolean
        Get
            Dim result As Boolean
            result = Not m_Declaration.Modifiers.Is(ModifierMasks.ReadOnly)
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property DeclaringType() As System.Type
        Get
            Dim result As Type = Nothing
            If m_Declaration Is Nothing Then Throw New InternalException(Me)
            result = DirectCast(m_Declaration, BaseObject).FindFirstParent(Of IType).TypeDescriptor
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overloads Overrides Function GetAccessors(ByVal nonPublic As Boolean) As System.Reflection.MethodInfo()
        Dim result As MethodInfo() = Nothing
        DumpMethodInfo(result)
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return result
    End Function

    Public Overloads Overrides Function GetCustomAttributes(ByVal inherit As Boolean) As Object()
        Dim result As Object() = Nothing
        DumpMethodInfo(result)
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return result
    End Function

    Public Overloads Overrides Function GetCustomAttributes(ByVal attributeType As System.Type, ByVal inherit As Boolean) As Object()
        Dim result As Object() = Nothing
        DumpMethodInfo(result)
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return result
    End Function

    Public Overloads Overrides Function GetGetMethod(ByVal nonPublic As Boolean) As System.Reflection.MethodInfo
        Dim result As MethodInfo

        result = m_Declaration.GetMethod
        DumpMethodInfo(result)

        Return result
    End Function

    Public Overrides Function GetIndexParameters() As System.Reflection.ParameterInfo()
        Dim result As ParameterInfo()

        result = m_Declaration.Signature.Parameters.AsParameterInfo

        DumpMethodInfo(result)
        Return result
    End Function

    Public Overloads Overrides Function GetSetMethod(ByVal nonPublic As Boolean) As System.Reflection.MethodInfo
        Dim result As MethodInfo

        result = m_Declaration.SetMethod
        DumpMethodInfo(result)

        Return result
    End Function

    Public Overloads Overrides Function GetValue(ByVal obj As Object, ByVal invokeAttr As System.Reflection.BindingFlags, ByVal binder As System.Reflection.Binder, ByVal index() As Object, ByVal culture As System.Globalization.CultureInfo) As Object
        Dim result As Object = Nothing
        DumpMethodInfo(result)
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return result
    End Function

    Public Overrides Function IsDefined(ByVal attributeType As System.Type, ByVal inherit As Boolean) As Boolean
        Dim result As Boolean
        DumpMethodInfo(result)
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return result
    End Function

    Public Overrides ReadOnly Property Name() As String
        Get
            Dim result As String = m_Declaration.Name
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property PropertyType() As System.Type
        Get
            Dim result As Type = m_Declaration.Signature.ReturnType
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property ReflectedType() As System.Type
        Get
            Dim result As Type = Nothing
            DumpMethodInfo(result)
            Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
            Return result
        End Get
    End Property

    Public Overloads Overrides Sub SetValue(ByVal obj As Object, ByVal value As Object, ByVal invokeAttr As System.Reflection.BindingFlags, ByVal binder As System.Reflection.Binder, ByVal index() As Object, ByVal culture As System.Globalization.CultureInfo)
        DumpMethodInfo()
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
    End Sub

    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        Dim result As Boolean = MyBase.Equals(obj)
        DumpMethodInfo(result)
        Return result
    End Function

    Public Overrides Function GetConstantValue() As Object
        Dim result As Object = Nothing
        DumpMethodInfo(result)
        Return result
    End Function

    Public Overrides Function GetHashCode() As Integer
        Dim result As Integer = MyBase.GetHashCode
        DumpMethodInfo(result)
        Return result
    End Function

    Public Overrides Function GetOptionalCustomModifiers() As System.Type()
        Dim result As Type() = MyBase.GetOptionalCustomModifiers
        DumpMethodInfo(result)
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return result
    End Function

    Public Overrides Function GetRawConstantValue() As Object
        Dim result As Object = MyBase.GetRawConstantValue
        DumpMethodInfo(result)
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return result
    End Function

    Public Overrides Function GetRequiredCustomModifiers() As System.Type()
        Dim result As Type() = MyBase.GetRequiredCustomModifiers
        DumpMethodInfo(result)
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return result
    End Function

    Public Overrides Function GetValue(ByVal obj As Object, ByVal index() As Object) As Object
        Dim result As Object = MyBase.GetValue(obj, index)
        DumpMethodInfo(result)
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return result
    End Function

    Public Overrides ReadOnly Property MemberType() As System.Reflection.MemberTypes
        Get
            Dim result As MemberTypes = MemberTypes.Property
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property MetadataToken() As Integer
        Get
            Dim result As Integer = MyBase.MetadataToken
            DumpMethodInfo(result)
            Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property [Module]() As System.Reflection.Module
        Get
            Dim result As System.Reflection.Module = MyBase.Module
            DumpMethodInfo(result)
            Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
            Return result
        End Get
    End Property

    Public Overrides Function ToString() As String
        Dim result As String = MyBase.ToString

        If m_Declaration Is Nothing Then Throw New InternalException(Me)

        If m_Declaration.PropertyBuilder IsNot Nothing Then
            result = m_Declaration.PropertyBuilder.ToString
        Else
            result = m_Declaration.FullName
        End If

        DumpMethodInfo(result)
        Return result
    End Function

    Public Overrides Sub SetValue(ByVal obj As Object, ByVal value As Object, ByVal index() As Object)
        'MyBase.SetValue(obj, value, index)
        DumpMethodInfo()
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
    End Sub
End Class
