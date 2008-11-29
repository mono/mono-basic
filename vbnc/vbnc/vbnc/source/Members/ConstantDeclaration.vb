' 
' Visual Basic.Net Compiler
' Copyright (C) 2004 - 2008 Rolf Bjarne Kvinge, RKvinge@novell.com
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

    'Private m_Descriptor As New FieldDescriptor(Me)

    Private m_Identifier As Identifier
    Private m_TypeName As TypeName
    Private m_ConstantExpression As Expression

    Private m_FieldBuilderCecil As Mono.Cecil.FieldDefinition

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
        UpdateDefinition()
    End Sub

    Shadows Sub Init(ByVal Modifiers As Modifiers, ByVal Identifier As Identifier, ByVal TypeName As TypeName, ByVal ConstantExpression As Expression)
        MyBase.Init(Modifiers, Identifier.Name)
        m_Identifier = Identifier
        m_TypeName = TypeName
        m_ConstantExpression = ConstantExpression
        UpdateDefinition()
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

        UpdateDefinition()

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

    Shared Function GetDecimalConstant(ByVal Compiler As Compiler, ByVal Field As Mono.Cecil.FieldDefinition, ByRef value As Decimal) As Boolean
        Dim decAttrs As Mono.Cecil.CustomAttributeCollection
        decAttrs = CecilHelper.GetCustomAttributes(Field.CustomAttributes, Compiler.TypeCache.System_Runtime_CompilerServices_DecimalConstantAttribute)
        If decAttrs IsNot Nothing AndAlso decAttrs.Count = 1 Then
            Dim attr As Mono.Cecil.CustomAttribute = decAttrs(0)
            Dim scale As Byte, sign As Byte
            Dim hi1 As Integer, mid1 As Integer, low1 As Integer
            Dim isUnsigned As Boolean

            If attr.ConstructorParameters.Count <> 5 Then Return False
            If TypeOf attr.ConstructorParameters(0) Is Byte = False Then Return False
            If TypeOf attr.ConstructorParameters(1) Is Byte = False Then Return False

            scale = DirectCast(attr.ConstructorParameters(0), Byte)
            sign = DirectCast(attr.ConstructorParameters(1), Byte)

            If TypeOf attr.ConstructorParameters(2) Is Integer Then
                hi1 = DirectCast(attr.ConstructorParameters(2), Integer)
                isUnsigned = False
            ElseIf TypeOf attr.ConstructorParameters(2) Is UInteger Then
                hi1 = BitConverter.ToInt32(BitConverter.GetBytes(DirectCast(attr.ConstructorParameters(2), UInteger)), 0)
                isUnsigned = True
            Else
                Return False
            End If

            If TypeOf attr.ConstructorParameters(3) Is Integer Then
                If isUnsigned Then Return False
                mid1 = DirectCast(attr.ConstructorParameters(3), Integer)
            ElseIf TypeOf attr.ConstructorParameters(3) Is UInteger Then
                If isUnsigned = False Then Return False
                mid1 = BitConverter.ToInt32(BitConverter.GetBytes(DirectCast(attr.ConstructorParameters(3), UInteger)), 0)
            Else
                Return False
            End If

            If TypeOf attr.ConstructorParameters(4) Is Integer Then
                If isUnsigned Then Return False
                low1 = DirectCast(attr.ConstructorParameters(4), Integer)
            ElseIf TypeOf attr.ConstructorParameters(4) Is UInteger Then
                If isUnsigned = False Then Return False
                low1 = BitConverter.ToInt32(BitConverter.GetBytes(DirectCast(attr.ConstructorParameters(4), UInteger)), 0)
            Else
                Return False
            End If

            value = New Decimal(low1, mid1, hi1, sign <> 0, scale)
            Return True
        End If
        Return False
    End Function

    Shared Function GetDateConstant(ByVal Compiler As Compiler, ByVal Field As Mono.Cecil.FieldDefinition, ByRef value As Date) As Boolean
        Dim dtAttrs As Mono.Cecil.CustomAttributeCollection
        dtAttrs = CecilHelper.GetCustomAttributes(Field.CustomAttributes, Compiler.TypeCache.System_Runtime_CompilerServices_DateTimeConstantAttribute)
        If dtAttrs IsNot Nothing AndAlso dtAttrs.Count = 1 Then
            value = DirectCast(dtAttrs(0).Properties("Value"), Date)
            Return True
        End If
        Return False
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
                UpdateDefinition()
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

        If m_TypeName Is Nothing AndAlso Location.File(Compiler).IsOptionStrictOn Then
            result = Compiler.Report.ShowMessage(Messages.VBNC30209, Me.Location) AndAlso result
        End If

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

        If m_ConstantValue Is Nothing OrElse TypeOf m_ConstantValue Is DBNull Then
            'm_FieldBuilder.SetConstant(Nothing)
        ElseIf Helper.CompareType(CecilHelper.GetType(Compiler, m_ConstantValue), Compiler.TypeCache.System_Decimal) Then
            Dim value As Decimal = DirectCast(m_ConstantValue, Decimal)
            Dim attrib As New Mono.Cecil.CustomAttribute(Compiler.TypeCache.System_Runtime_CompilerServices_DecimalConstantAttribute__ctor_Byte_Byte_Int32_Int32_Int32)
            Dim params As Object() = New Emitter.DecimalFields(value).AsByte_Byte_Int32_Int32_Int32()
            For i As Integer = 0 To params.Length - 1
                attrib.ConstructorParameters.Add(params(i))
            Next
            m_FieldBuilderCecil.CustomAttributes.Add(attrib) 
        ElseIf Helper.CompareType(CecilHelper.GetType(Compiler, m_ConstantValue), Compiler.TypeCache.System_DateTime) Then
            Dim attrib As New Mono.Cecil.CustomAttribute(Compiler.TypeCache.System_Runtime_CompilerServices_DateTimeConstantAttribute__ctor_Int64)
            attrib.ConstructorParameters.Add(DirectCast(m_ConstantValue, Date).Ticks)
            m_FieldBuilderCecil.CustomAttributes.Add(attrib)
        Else
            'If Helper.IsEnum(Compiler, m_FieldType) AndAlso Helper.CompareType(m_FieldType, m_ConstantValue.GetType) = False Then
            '    m_ConstantValue = System.Enum.ToObject(m_FieldType, m_ConstantValue)
            'End If

            'If Helper.IsOnMS Then
            '    Helper.Assert(Helper.CompareType(m_ConstantValue.GetType, m_FieldBuilder.FieldType), "Constant type and Field Type is not equal (Constant type = " & m_ConstantValue.GetType.Name & ", field type = " & m_FieldBuilder.FieldType.Name & ")")
            'End If
            'm_FieldBuilder.SetConstant(m_ConstantValue)
        End If

        UpdateDefinition()

        Return result
    End Function

    Public Overrides Sub UpdateDefinition()
        MyBase.UpdateDefinition()

        If m_FieldBuilderCecil Is Nothing Then
            m_FieldBuilderCecil = New Mono.Cecil.FieldDefinition(Name, Nothing, 0)
            m_FieldBuilderCecil.Annotations.Add(Compiler, Me)
            DeclaringType.CecilType.Fields.Add(m_FieldBuilderCecil)
        End If

        m_FieldBuilderCecil.Constant = m_ConstantValue
        m_FieldBuilderCecil.HasDefault = True
        m_FieldBuilderCecil.Name = Name
        If m_TypeName IsNot Nothing Then
            m_FieldBuilderCecil.FieldType = Helper.GetTypeOrTypeReference(Compiler, m_TypeName.ResolvedType)
        Else
            'Helper.StopIfDebugging()
        End If
        m_FieldBuilderCecil.Attributes = Helper.GetAttributes(Compiler, Me)

    End Sub

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = MyBase.ResolveCode(Info) AndAlso result

        Return result
    End Function


    Public ReadOnly Property FieldBuilder() As Mono.Cecil.FieldDefinition Implements IFieldMember.FieldBuilder
        Get
            Return m_FieldBuilderCecil
        End Get
    End Property

    Public ReadOnly Property FieldType() As Mono.Cecil.TypeReference Implements IFieldMember.FieldType
        Get
            If m_FieldBuilderCecil Is Nothing Then Return Nothing
            Return m_FieldBuilderCecil.FieldType
        End Get
    End Property

    Public Overrides ReadOnly Property MemberDescriptor() As Mono.Cecil.MemberReference
        Get
            Return m_FieldBuilderCecil
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

    Public Function ResolveAndGetConstantValue(ByRef value As Object) As Boolean Implements IFieldMember.ResolveAndGetConstantValue
        value = ConstantValue
        Return True
    End Function
End Class
