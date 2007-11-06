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
#Const DEBUGFIELDACCESS = 0
#End If

Public Class FieldDescriptor
    Inherits FieldInfo
    Implements IMemberDescriptor

    Private m_Declaration As IFieldMember
    Private m_Parent As ParsedObject

    <Diagnostics.Conditional("DEBUGFIELDACCESS")> _
        Protected Sub DumpMethodInfo(Optional ByVal ReturnValue As Object = Nothing)
#If DEBUGFIELDACCESS Then
        Dim m As New Diagnostics.StackFrame(1)
        Dim str As String

        Dim i As INameable = m_Declaration
        Dim name As String = i.FullName
        str = " Called: (" & name & "): Field."

        If ReturnValue IsNot Nothing Then
            i.Compiler.Report.WriteLine(Report.ReportLevels.Debug, str & m.GetMethod.Name & " with return value: " & ReturnValue.ToString)
        Else
            i.Compiler.Report.WriteLine(Report.ReportLevels.Debug, str & m.GetMethod.Name)
        End If
#End If
    End Sub

    ReadOnly Property IsShared() As Boolean Implements IMemberDescriptor.IsShared
        Get
            Return m_Declaration.IsShared
        End Get
    End Property

    Overridable ReadOnly Property FieldInReflection() As FieldInfo
        Get
            Return m_Declaration.FieldBuilder
        End Get
    End Property

    Private ReadOnly Property Declaration2() As IMember Implements IMemberDescriptor.Declaration
        Get
            Return m_Declaration
        End Get
    End Property

    ReadOnly Property Declaration() As IFieldMember
        Get
            Return m_Declaration
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

    Sub New(ByVal Parent As ParsedObject)
        m_Declaration = TryCast(Parent, IFieldMember)
        m_Parent = Parent
    End Sub

    Public Overrides ReadOnly Property Attributes() As System.Reflection.FieldAttributes
        Get
            Dim result As FieldAttributes
            result = m_Declaration.Modifiers.GetFieldAttributeScope(DirectCast(m_Declaration, BaseObject).FindFirstParent(Of TypeDeclaration))
            If m_Declaration.Modifiers.Is(ModifierMasks.Static) Then
                result = result Or FieldAttributes.SpecialName
                If DirectCast(m_Declaration, BaseObject).FindFirstParent(Of IMethod).IsShared Then
                    result = result Or FieldAttributes.Static
                End If
            End If
            If m_Declaration.Modifiers.Is(ModifierMasks.Shared) OrElse m_Declaration.IsShared Then
                result = result Or FieldAttributes.Static
            End If
            If TypeOf m_Declaration Is EnumMemberDeclaration Then
                result = result Or FieldAttributes.Static Or FieldAttributes.Literal
            End If
            If TypeOf m_Declaration Is ConstantDeclaration Then
                result = result Or FieldAttributes.Static
                Helper.Assert(m_Declaration.FieldType IsNot Nothing)
                If Helper.CompareType(m_Declaration.FieldType, Compiler.TypeCache.System_Decimal) Then
                    result = result Or FieldAttributes.InitOnly
                ElseIf Helper.CompareType(m_Declaration.FieldType, Compiler.TypeCache.System_DateTime) Then
                    result = result Or FieldAttributes.InitOnly
                Else
                    result = result Or FieldAttributes.Literal
                End If
            End If
            If m_Declaration.Modifiers.Is(ModifierMasks.ReadOnly) Then
                result = result Or FieldAttributes.InitOnly
            End If

            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Shared ticks As Long
    Shared counter As Long

    Public Overrides ReadOnly Property DeclaringType() As System.Type
        Get
            Dim result As Type
            If m_Declaration.FieldBuilder IsNot Nothing Then
                result = a() 'm_Declaration.FieldBuilder.DeclaringType
            Else
                Dim startticks As Long = DateTime.Now.Ticks
                Dim tickcount As Long
                Dim endticks As Long

                result = b() 'm_Declaration.FindFirstParent(Of IType).TypeDescriptor

                endticks = DateTime.Now.Ticks
                tickcount = endticks - startticks
                ticks += tickcount
                counter += 1
                If counter Mod 1000 = 0 Then
                    'Compiler.Report.WriteLine(String.Format("FieldDescriptor.DeclaringType, {0} ticks, {1} ms, {2} total ticks, {3} total ms, {4} total s", tickcount, TimeSpan.FromTicks(tickcount).TotalMilliseconds, ticks, TimeSpan.FromTicks(ticks).TotalMilliseconds, TimeSpan.FromTicks(ticks).TotalSeconds))
                End If
            End If

            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Private Function a() As Type
        Return m_Declaration.FieldBuilder.DeclaringType
    End Function

    Private Function b() As Type
        Dim obj As BaseObject
        obj = DirectCast(m_Declaration, BaseObject)
        Return obj.FindFirstParent_IType.TypeDescriptor
    End Function


    Public Overrides ReadOnly Property FieldHandle() As System.RuntimeFieldHandle
        Get
            DumpMethodInfo()

            Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
            Return Nothing
        End Get
    End Property

    Public Overrides ReadOnly Property FieldType() As System.Type
        Get
            Dim result As Type = m_Declaration.FieldType
            DumpMethodInfo(result)
            Helper.Assert(result IsNot Nothing)
            Return result
        End Get
    End Property

    Public Overloads Overrides Function GetCustomAttributes(ByVal inherit As Boolean) As Object()
        DumpMethodInfo()
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return Nothing
    End Function

    Public Overloads Overrides Function GetCustomAttributes(ByVal attributeType As System.Type, ByVal inherit As Boolean) As Object()
        Dim result As Object() = Nothing
        DumpMethodInfo(result)
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return result
    End Function

    Public Overrides Function GetValue(ByVal obj As Object) As Object
        DumpMethodInfo()

        If obj IsNot Nothing Then Throw New InternalException(Me.Declaration)
        If m_Declaration Is Nothing Then Throw New InternalException(Me.Declaration)

        Dim emd As EnumMemberDeclaration = TryCast(m_Declaration, EnumMemberDeclaration)
        If emd IsNot Nothing Then
            Helper.Assert(emd.ConstantValue IsNot Nothing)
            Return emd.ConstantValue
        End If
        Dim cd As ConstantDeclaration = TryCast(m_Declaration, ConstantDeclaration)
        If cd IsNot Nothing Then
            Helper.Assert(cd.ConstantValue IsNot Nothing)
            Return cd.ConstantValue
        End If

        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return Nothing
    End Function

    Public Overrides Function IsDefined(ByVal attributeType As System.Type, ByVal inherit As Boolean) As Boolean
        DumpMethodInfo()
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return Nothing
    End Function

    Public Overrides ReadOnly Property Name() As String
        Get
            DumpMethodInfo()
            Return m_Declaration.Name
        End Get
    End Property

    Public Overrides ReadOnly Property ReflectedType() As System.Type
        Get
            Dim result As Type = DeclaringType
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overloads Overrides Sub SetValue(ByVal obj As Object, ByVal value As Object, ByVal invokeAttr As System.Reflection.BindingFlags, ByVal binder As System.Reflection.Binder, ByVal culture As System.Globalization.CultureInfo)
        DumpMethodInfo()
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
    End Sub

    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        Dim result As Boolean

        result = obj Is Me
        DumpMethodInfo(result)
        Return result
    End Function

    Public Overrides Function GetHashCode() As Integer
        DumpMethodInfo()
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return MyBase.GetHashCode()
    End Function
    Public Overrides Function GetOptionalCustomModifiers() As System.Type()
        DumpMethodInfo(Type.EmptyTypes)
        Return Type.EmptyTypes
    End Function
    Public Overrides Function GetRawConstantValue() As Object
        DumpMethodInfo()
        Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
        Return MyBase.GetRawConstantValue()
    End Function
    Public Overrides Function GetRequiredCustomModifiers() As System.Type()
        DumpMethodInfo(Type.EmptyTypes)
        Return Type.EmptyTypes
    End Function

    Public Overrides ReadOnly Property MemberType() As System.Reflection.MemberTypes
        Get
            Dim result As MemberTypes
            result = MemberTypes.Field

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
    Public Overrides Function ToString() As String
        DumpMethodInfo()
        If m_Declaration.FieldBuilder IsNot Nothing Then
            Return m_Declaration.FieldBuilder.ToString
        Else
            Return MyBase.ToString
        End If
    End Function

End Class
