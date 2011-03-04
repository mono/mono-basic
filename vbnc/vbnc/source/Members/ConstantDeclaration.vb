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
''' ConstantDeclarator  ::=  Identifier  [  As  TypeName  ]  =  ConstantExpression  StatementTerminator
''' TODO: Is this a spec bug? ------------------------------------------------------^^^^^^^^^^^^^^^^^^^?
''' </summary>
''' <remarks></remarks>
Public Class ConstantDeclaration
    Inherits MemberDeclaration
    Implements IFieldMember

    Private m_Identifier As Identifier
    Private m_TypeName As TypeName
    Private m_ConstantExpression As Expression

    Private m_FieldBuilderCecil As Mono.Cecil.FieldDefinition

    Private m_RequiresSharedInitialization As Boolean

    ReadOnly Property RequiresSharedInitialization() As Boolean
        Get
            Return m_RequiresSharedInitialization
        End Get
    End Property

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Shadows Sub Init(ByVal Modifiers As Modifiers, ByVal Identifier As Identifier, ByVal TypeName As TypeName, ByVal ConstantExpression As Expression)
        MyBase.Init(Modifiers, Identifier.Name)
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

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        If m_ConstantExpression IsNot Nothing Then result = m_ConstantExpression.ResolveTypeReferences AndAlso result

        If m_TypeName IsNot Nothing Then
            result = m_TypeName.ResolveTypeReferences AndAlso result
            If m_Identifier.HasTypeCharacter Then
                result = Helper.AddError(Me, "Type character and type name is not allowed")
            End If
        ElseIf m_Identifier.HasTypeCharacter Then
            m_TypeName = New TypeName(Me, TypeCharacters.TypeCharacterToType(Compiler, m_Identifier.TypeCharacter))
        Else
            m_TypeName = New TypeName(Me, m_ConstantExpression.ExpressionType)
        End If

        m_FieldBuilderCecil.FieldType = Helper.GetTypeOrTypeReference(Compiler, m_TypeName.ResolvedType)

        If Helper.CompareType(m_TypeName.ResolvedType, Compiler.TypeCache.System_Decimal) Then
            m_RequiresSharedInitialization = True
        ElseIf Helper.CompareType(m_TypeName.ResolvedType, Compiler.TypeCache.System_DateTime) Then
            m_RequiresSharedInitialization = True
        End If

        Return result
    End Function

    Shared Function GetDecimalConstant(ByVal Compiler As Compiler, ByVal Field As Mono.Cecil.FieldDefinition, ByRef value As Decimal) As Boolean
        Dim decAttrs As Mono.Collections.Generic.Collection(Of CustomAttribute)
        decAttrs = CecilHelper.GetCustomAttributes(Field.CustomAttributes, Compiler.TypeCache.System_Runtime_CompilerServices_DecimalConstantAttribute)
        If decAttrs IsNot Nothing AndAlso decAttrs.Count = 1 Then
            Dim attr As Mono.Cecil.CustomAttribute = decAttrs(0)
            Dim scale As Byte, sign As Byte
            Dim hi1 As Integer, mid1 As Integer, low1 As Integer
            Dim isUnsigned As Boolean

            If attr.ConstructorArguments.Count <> 5 Then Return False
            If TypeOf attr.ConstructorArguments(0).Value Is Byte = False Then Return False
            If TypeOf attr.ConstructorArguments(1).Value Is Byte = False Then Return False

            scale = DirectCast(attr.ConstructorArguments(0).Value, Byte)
            sign = DirectCast(attr.ConstructorArguments(1).Value, Byte)

            If TypeOf attr.ConstructorArguments(2).Value Is Integer Then
                hi1 = DirectCast(attr.ConstructorArguments(2).Value, Integer)
                isUnsigned = False
            ElseIf TypeOf attr.ConstructorArguments(2).Value Is UInteger Then
                hi1 = BitConverter.ToInt32(BitConverter.GetBytes(DirectCast(attr.ConstructorArguments(2).Value, UInteger)), 0)
                isUnsigned = True
            Else
                Return False
            End If

            If TypeOf attr.ConstructorArguments(3).Value Is Integer Then
                If isUnsigned Then Return False
                mid1 = DirectCast(attr.ConstructorArguments(3).Value, Integer)
            ElseIf TypeOf attr.ConstructorArguments(3).Value Is UInteger Then
                If isUnsigned = False Then Return False
                mid1 = BitConverter.ToInt32(BitConverter.GetBytes(DirectCast(attr.ConstructorArguments(3).Value, UInteger)), 0)
            Else
                Return False
            End If

            If TypeOf attr.ConstructorArguments(4).Value Is Integer Then
                If isUnsigned Then Return False
                low1 = DirectCast(attr.ConstructorArguments(4).Value, Integer)
            ElseIf TypeOf attr.ConstructorArguments(4).Value Is UInteger Then
                If isUnsigned = False Then Return False
                low1 = BitConverter.ToInt32(BitConverter.GetBytes(DirectCast(attr.ConstructorArguments(4).Value, UInteger)), 0)
            Else
                Return False
            End If

            value = New Decimal(low1, mid1, hi1, sign <> 0, scale)
            Return True
        End If
        Return False
    End Function

    Shared Function GetDateConstant(ByVal Compiler As Compiler, ByVal Field As Mono.Cecil.FieldDefinition, ByRef value As Date) As Boolean
        Dim dtAttrs As Mono.Collections.Generic.Collection(Of CustomAttribute)
        dtAttrs = CecilHelper.GetCustomAttributes(Field.CustomAttributes, Compiler.TypeCache.System_Runtime_CompilerServices_DateTimeConstantAttribute)
        If dtAttrs IsNot Nothing AndAlso dtAttrs.Count = 1 Then
            value = DirectCast(dtAttrs(0).Properties(0).Argument.Value, Date)
            Return True
        End If
        Return False
    End Function

    Function GetConstant(ByRef result As Object, ByVal ShowErrors As Boolean) As Boolean
        If m_ConstantExpression Is Nothing Then
            If ShowErrors Then Show30059()
            Return False
        End If

        If m_ConstantExpression.IsResolved = False Then
            If Not m_ConstantExpression.ResolveExpression(ResolveInfo.Default(Compiler)) Then
                If ShowErrors Then Show30059()
                Return False
            End If
        End If

        If m_ConstantExpression.GetConstant(result, ShowErrors) = False Then Return False

        If m_TypeName Is Nothing Then
            m_TypeName = New TypeName(Me, m_ConstantExpression.ExpressionType)
        Else
            Return TypeConverter.ConvertTo(m_ConstantExpression, result, m_TypeName.ResolvedType, result, ShowErrors)
        End If

        Return True
    End Function

    Function ResolveMember(ByVal Info As ResolveInfo) As Boolean Implements INonTypeMember.ResolveMember
        Dim result As Boolean = True

        If m_TypeName Is Nothing AndAlso Location.File(Compiler).IsOptionStrictOn Then
            result = Compiler.Report.ShowMessage(Messages.VBNC30209, Me.Location) AndAlso result
        End If

        result = m_ConstantExpression.ResolveExpression(Info) AndAlso result

        Return result
    End Function

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Return True
    End Function

    Public Function DefineConstant() As Boolean
        Dim result As Boolean = True
        Dim constant As Object = Nothing

        Helper.Assert(m_TypeName IsNot Nothing)

        If m_ConstantExpression Is Nothing Then
            Helper.AddError(Me, "No constant expression.")
            Return False
        End If

        result = m_ConstantExpression.ResolveExpression(ResolveInfo.Default(Parent.Compiler)) AndAlso result

        If Not GetConstant(constant, True) Then Return False

        If m_RequiresSharedInitialization Then
            m_FieldBuilderCecil.Constant = Nothing
        ElseIf constant Is DBNull.Value Then
            m_FieldBuilderCecil.Constant = Nothing
        Else
            m_FieldBuilderCecil.Constant = constant
        End If
        m_FieldBuilderCecil.HasConstant = Not m_RequiresSharedInitialization

        If constant IsNot Nothing AndAlso constant IsNot DBNull.Value Then
            CreateConstantAttribute(Compiler, constant, m_FieldBuilderCecil.CustomAttributes)
        End If

        m_FieldBuilderCecil.Attributes = Helper.GetAttributes(Compiler, Me)

        Return result
    End Function

    Public Shared Sub CreateConstantAttribute(ByVal Compiler As Compiler, ByVal constant As Object, ByVal CustomAttributes As Mono.Collections.Generic.Collection(Of CustomAttribute))
        If TypeOf constant Is Decimal Then
            Dim value As Decimal = DirectCast(constant, Decimal)
            Dim ctor As MethodDefinition = Compiler.TypeCache.System_Runtime_CompilerServices_DecimalConstantAttribute__ctor_Byte_Byte_UInt32_UInt32_UInt32
            Dim attrib As New Mono.Cecil.CustomAttribute(Helper.GetMethodOrMethodReference(Compiler, ctor))
            Dim params As Object() = New Emitter.DecimalFields(value).AsByte_Byte_UInt32_UInt32_UInt32()
            For i As Integer = 0 To params.Length - 1
                attrib.ConstructorArguments.Add(New CustomAttributeArgument(ctor.Parameters(i).ParameterType, params(i)))
            Next
            CustomAttributes.Add(attrib)
        ElseIf TypeOf constant Is Date Then
            Dim attrib As New Mono.Cecil.CustomAttribute(Helper.GetMethodOrMethodReference(Compiler, Compiler.TypeCache.System_Runtime_CompilerServices_DateTimeConstantAttribute__ctor_Int64))
            attrib.ConstructorArguments.Add(New CustomAttributeArgument(Helper.GetTypeOrTypeReference(Compiler, Compiler.TypeCache.System_Int64), DirectCast(constant, Date).Ticks))
            CustomAttributes.Add(attrib)
        End If
    End Sub

    Public Overrides Function CreateDefinition() As Boolean
        Dim result As Boolean = True

        result = MyBase.CreateDefinition AndAlso result

        Helper.Assert(m_FieldBuilderCecil Is Nothing)
        m_FieldBuilderCecil = New Mono.Cecil.FieldDefinition(Name, 0, Nothing)
        m_FieldBuilderCecil.Annotations.Add(Compiler, Me)
        DeclaringType.CecilType.Fields.Add(m_FieldBuilderCecil)

        m_FieldBuilderCecil.HasDefault = True
        m_FieldBuilderCecil.Name = Name
        m_FieldBuilderCecil.Attributes = Helper.GetAttributes(Compiler, Me)

        Return result
    End Function

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
End Class

