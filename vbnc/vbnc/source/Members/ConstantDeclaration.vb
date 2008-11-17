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
''' ConstantDeclarator  ::=  Identifier  [  As  TypeName  ]  =  ConstantExpression  StatementTerminator
''' TODO: Is this a spec bug? ------------------------------------------------------^^^^^^^^^^^^^^^^^^^?
''' </summary>
''' <remarks></remarks>
Public Class ConstantDeclaration
    Inherits MemberDeclaration
    Implements IFieldMember

    Private m_Descriptor As New FieldDescriptor(Me)

    Private m_Identifier As Identifier
    Private m_TypeName As TypeName
    Private m_ConstantExpression As Expression

    Private m_FieldBuilder As FieldBuilder
    Private m_FieldType As Type

    Private m_Resolved As Boolean
    Private m_ConstantValue As Object
    Private m_RequiresSharedInitialization As Boolean

    ReadOnly Property RequiresSharedInitialization() As Boolean
        Get
            Return m_RequiresSharedInitialization
        End Get
    End Property

    ReadOnly Property Resolved() As Boolean
        Get
            Return m_Resolved
        End Get
    End Property

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Shadows Sub Init(ByVal Attributes As Attributes, ByVal Modifiers As Modifiers, ByVal Identifier As Identifier, ByVal TypeName As TypeName, ByVal ConstantExpression As Expression)
        MyBase.Init(Attributes, Modifiers, Identifier.Name)
        m_Identifier = Identifier
        m_TypeName = TypeName
        m_ConstantExpression = ConstantExpression
    End Sub

    ''' <summary>
    ''' Checks for the following grammar:
    ''' ConstantMemberDeclaration  ::=	[  Attributes  ]  [  ConstantModifier+  ]  "Const"  ConstantDeclarators  StatementTerminator
    ''' </summary>
    Shared Function IsMe(ByVal tm As tm) As Boolean
        Dim i As Integer
        While tm.PeekToken(i).Equals(ModifierMasks.ConstantModifiers)
            i += 1
        End While
        Return tm.PeekToken(i) = KS.Const
    End Function

    ReadOnly Property ConstantValue() As Object
        Get
            If m_Resolved = False Then
                Dim result As Boolean
                result = ResolveConstantValue(ResolveInfo.Default(Compiler))
                If result = False Then
                    Helper.AddError(Me, "")
                    Return Nothing
                End If
            End If
            Return m_ConstantValue
        End Get
    End Property

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        If m_TypeName IsNot Nothing Then
            result = m_TypeName.ResolveTypeReferences AndAlso result
            If m_Identifier.HasTypeCharacter Then
                Helper.AddError(Me)
            End If
        ElseIf m_Identifier.HasTypeCharacter Then
            m_TypeName = New TypeName(Me, TypeCharacters.TypeCharacterToType(Compiler, m_Identifier.TypeCharacter))
        End If

        If m_ConstantExpression IsNot Nothing Then result = m_ConstantExpression.ResolveTypeReferences AndAlso result

        If result AndAlso m_TypeName IsNot Nothing Then
            Helper.Assert(m_TypeName IsNot Nothing)
            If Helper.CompareType(m_TypeName.ResolvedType, Compiler.TypeCache.System_Decimal) Then
                m_RequiresSharedInitialization = True
            ElseIf Helper.CompareType(m_TypeName.ResolvedType, Compiler.TypeCache.System_DateTime) Then
                m_RequiresSharedInitialization = True
            End If
        End If

        Return result
    End Function

    Function ResolveConstantValue(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        Helper.Assert(m_ConstantExpression IsNot Nothing)
        If m_ConstantExpression.IsResolved = False Then
            result = m_ConstantExpression.ResolveExpression(Info) AndAlso result
            If m_ConstantExpression.IsConstant Then
                m_ConstantValue = m_ConstantExpression.ConstantValue
                Helper.Assert(m_ConstantValue IsNot Nothing)

                If m_TypeName Is Nothing Then
                    m_TypeName = New TypeName(Me, m_ConstantExpression.ExpressionType)
                Else
                    result = TypeConverter.ConvertTo(m_ConstantExpression, m_ConstantValue, m_TypeName.ResolvedType, m_ConstantValue) AndAlso result
                End If
                'If m_ConstantValue IsNot Nothing Then Compiler.Report.WriteLine("Converted to: " & m_ConstantValue.GetType.FullName)
            Else
                result = Compiler.Report.ShowMessage(Messages.VBNC30059, m_ConstantExpression.Location)
            End If
            m_Resolved = True
        End If

        Helper.Assert(m_Resolved)

        Return result
    End Function

    Function ResolveMember(ByVal Info As ResolveInfo) As Boolean Implements INonTypeMember.ResolveMember
        Dim result As Boolean = True

        If m_ConstantExpression Is Nothing Then
            Helper.AddError(Me, "No constant expression.")
            Return False
        End If

        result = ResolveConstantValue(Info) AndAlso result

        Return result
    End Function

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Return True
    End Function

    Public Function DefineMember() As Boolean Implements IDefinableMember.DefineMember
        Dim result As Boolean = True

        m_FieldType = Helper.GetTypeOrTypeBuilder(FieldType)
        m_FieldBuilder = Me.DeclaringType.TypeBuilder.DefineField(Name, m_FieldType, m_Descriptor.Attributes)
        Compiler.TypeManager.RegisterReflectionMember(m_FieldBuilder, Me.MemberDescriptor)

        If m_ConstantValue Is Nothing OrElse TypeOf m_ConstantValue Is DBNull Then
            m_FieldBuilder.SetConstant(Nothing)
        ElseIf Helper.CompareType(m_ConstantValue.GetType, Compiler.TypeCache.System_Decimal) Then
            'result = Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Location) AndAlso result
            'Helper.NotImplementedYet("Emit value of a decimal constant")
            Dim value As Decimal = DirectCast(m_ConstantValue, Decimal)
            m_FieldBuilder.SetCustomAttribute(New CustomAttributeBuilder(Compiler.TypeCache.System_Runtime_CompilerServices_DecimalConstantAttribute__ctor_Byte_Byte_Int32_Int32_Int32, New Emitter.DecimalFields(value).AsByte_Byte_Int32_Int32_Int32()))
        ElseIf Helper.CompareType(m_ConstantValue.GetType, Compiler.TypeCache.System_DateTime) Then
            m_FieldBuilder.SetCustomAttribute(New CustomAttributeBuilder(Compiler.TypeCache.System_Runtime_CompilerServices_DateTimeConstantAttribute__ctor_Int64, New Object() {DirectCast(m_ConstantValue, Date).Ticks}))
        Else
            If Helper.IsEnum(Compiler, m_FieldType) AndAlso Helper.CompareType(m_FieldType, m_ConstantValue.GetType) = False Then
                m_ConstantValue = System.Enum.ToObject(m_FieldType, m_ConstantValue)
            End If

            If Helper.IsOnMS Then
                Helper.Assert(Helper.CompareType(m_ConstantValue.GetType, m_FieldBuilder.FieldType), "Constant type and Field Type is not equal (Constant type = " & m_ConstantValue.GetType.Name & ", field type = " & m_FieldBuilder.FieldType.Name & ")")
            End If
            m_FieldBuilder.SetConstant(m_ConstantValue)
        End If

        Return result
    End Function

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = MyBase.ResolveCode(Info) AndAlso result

        Return result
    End Function


    Public ReadOnly Property FieldBuilder() As System.Reflection.Emit.FieldBuilder Implements IFieldMember.FieldBuilder
        Get
            Return m_FieldBuilder
        End Get
    End Property

    Public ReadOnly Property FieldType() As System.Type Implements IFieldMember.FieldType
        Get
            Return m_TypeName.ResolvedType
        End Get
    End Property

    Public Overrides ReadOnly Property MemberDescriptor() As System.Reflection.MemberInfo
        Get
            Return m_Descriptor
        End Get
    End Property

    ReadOnly Property Identifier() As Identifier
        Get
            Return m_Identifier
        End Get
    End Property

    ReadOnly Property TypeName() As TypeName
        Get
            Return m_TypeName
        End Get
    End Property

    ReadOnly Property ConstantExpression() As Expression
        Get
            Return m_ConstantExpression
        End Get
    End Property

    Public ReadOnly Property FieldDescriptor() As FieldDescriptor Implements IFieldMember.FieldDescriptor
        Get
            Return m_Descriptor
        End Get
    End Property
End Class
