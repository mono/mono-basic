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

Imports System.Reflection.Emit
Imports System.Reflection

''' <summary>
''' ConstructorMemberDeclaration  ::=
''' [  Attributes  ]  [  ConstructorModifier+  ]  "Sub" "New" [  "("  [  ParameterList  ]  ")"  ]  LineTerminator
'''	[  Block  ]
'''	"End" "Sub" StatementTerminator
''' </summary>
''' <remarks></remarks>
Public Class ConstructorDeclaration
    Inherits MethodBaseDeclaration
    Implements IConstructorMember

    'Private m_Descriptor As New ConstructorDescriptor(Me)

    Public Const ConstructorName As String = ".ctor"
    Public Const SharedConstructorName As String = ".cctor"

    'Private m_ConstructorBuilder As ConstructorBuilder

    'Private m_CecilBuilder As Mono.Cecil.MethodDefinition
    Private m_DefaultBaseConstructorCecil As Mono.Cecil.MethodReference

    ''' <summary>
    ''' The default base constructor to call if no call is specified in the code.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_DefaultBaseConstructor As Mono.Cecil.MethodReference

    ''' <summary>
    ''' The base/self constructor call in the code.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_BaseCtorCall As Statement
    Private m_Added As Boolean

    Sub New(ByVal Parent As TypeDeclaration)
        MyBase.New(Parent)
    End Sub

    Shadows Sub Init(ByVal Code As CodeBlock)
        MyBase.Init(New Modifiers(), New SubSignature(Me, ConstructorName, New ParameterList(Me)), Code)
    End Sub

    Shadows Sub Init(ByVal Modifiers As Modifiers, ByVal Signature As SubSignature, ByVal Block As CodeBlock)

        'If vbnc.Modifiers.IsNothing(Modifiers) = False AndAlso Modifiers.Is(ModifierMasks.Shared) Then
        If Modifiers.Is(ModifierMasks.Shared) OrElse FindTypeParent.IsModule Then
            Signature.Init(New Identifier(Signature, SharedConstructorName, Signature.Location, TypeCharacters.Characters.None), Signature.TypeParameters, Signature.Parameters)
        Else
            Signature.Init(New Identifier(Signature, ConstructorName, Signature.Location, TypeCharacters.Characters.None), Signature.TypeParameters, Signature.Parameters)
        End If

        MyBase.Init(Modifiers, Signature, Block)
    End Sub

    Shared Function CreateTypeConstructor(ByVal Parent As TypeDeclaration) As ConstructorDeclaration
        Dim result As New ConstructorDeclaration(Parent)

        result.Init(New Modifiers(ModifierMasks.Shared), New SubSignature(result, SharedConstructorName, New ParameterList(result)), New CodeBlock(result))

        result.UpdateDefinition()
        If result.ResolveTypeReferences() = False Then
            Helper.ErrorRecoveryNotImplemented(Parent.Location)
        End If

        Return result
    End Function

    Shared Function CreateDefaultConstructor(ByVal Parent As TypeDeclaration) As ConstructorDeclaration
        Dim result As New ConstructorDeclaration(Parent)
        Dim modifiers As Modifiers

        If Parent.Modifiers.Is(ModifierMasks.MustInherit) Then
            modifiers.AddModifier(KS.Protected)
        End If
        result.Init(modifiers, New SubSignature(result, ConstructorName, New ParameterList(result)), New CodeBlock(result))

        If result.ResolveTypeReferences() = False Then
            Helper.ErrorRecoveryNotImplemented(Parent.Location)
        End If

        Return result
    End Function

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        result = MyBase.ResolveTypeReferences AndAlso result

        UpdateDefinition()

        Return result
    End Function

    Overrides Function ResolveMember(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = MyBase.ResolveMember(Info) AndAlso result

        UpdateDefinition()

        Return result
    End Function

    ReadOnly Property ExplicitCtorCall() As Mono.Cecil.MethodReference
        Get
            Dim firststatement As BaseObject
            Dim cs As CallStatement
            Dim ie As InvocationOrIndexExpression
            Dim ctor As Mono.Cecil.MethodReference

            firststatement = Code.FirstStatement
            If firststatement Is Nothing Then Return Nothing

            cs = TryCast(firststatement, CallStatement)
            If cs Is Nothing Then Return Nothing

            ie = TryCast(cs.Target, InvocationOrIndexExpression)
            If ie Is Nothing Then Return Nothing
            If ie.Expression.Classification.IsMethodGroupClassification = False Then Return Nothing

            ctor = ie.Expression.Classification.AsMethodGroupClassification.ResolvedConstructor
            If ctor Is Nothing Then Return Nothing

            If Helper.CompareNameOrdinal(ctor.Name, ConstructorDeclaration.ConstructorName) = False Then Return Nothing

            If Helper.CompareType(CecilHelper.FindDefinition(ctor.DeclaringType), CecilHelper.FindDefinition(Me.FindTypeParent.BaseType)) Then
                Return ctor
            ElseIf Helper.CompareType(CecilHelper.FindDefinition(ctor.DeclaringType), CecilHelper.FindDefinition(Me.FindTypeParent.CecilType)) Then
                Return ctor
            Else
                Return Nothing
            End If
        End Get
    End Property

    ReadOnly Property HasExplicitCtorCall() As Boolean
        Get
            Return ExplicitCtorCall IsNot Nothing
        End Get
    End Property

    Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = MyBase.ResolveCode(Info) AndAlso result

        If result = False Then Return result

        If Me.IsShared = False AndAlso Me.HasMethodBody AndAlso Me.HasExplicitCtorCall = False Then
            CreateDefaultCtorCall()
            CreateDefaultCtorCallCecil()
        ElseIf Code IsNot Nothing AndAlso Me.HasExplicitCtorCall Then
            m_BaseCtorCall = Code.FirstStatement
            If m_BaseCtorCall IsNot Nothing Then Code.RemoveStatement(m_BaseCtorCall)
        End If

        Return result
    End Function

    Public Overrides Function DefineMember() As Boolean
        Dim result As Boolean = True

        result = MyBase.DefineMember AndAlso result

        'Helper.SetTypeOrTypeBuilder(Compiler, ParameterTypes)

        UpdateDefinition()

        Return result
    End Function

    Overrides Sub UpdateDefinition()
        MyBase.UpdateDefinition()

        If Signature IsNot Nothing AndAlso Signature.Parameters IsNot Nothing Then
            For i As Integer = 0 To Signature.Parameters.Count - 1
                Signature.Parameters(i).UpdateDefinition()
            Next
        End If
        CecilBuilder.Name = Name

        MethodAttributes = Helper.GetAttributes(Me)
        MethodImplAttributes = Mono.Cecil.MethodImplAttributes.IL

        If DeclaringType IsNot Nothing AndAlso DeclaringType.CecilType IsNot Nothing AndAlso m_Added = False Then
            m_Added = True
            DeclaringType.CecilType.Methods.Add(CecilBuilder)
        End If
    End Sub

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        If CBool(MethodImplAttributes And Mono.Cecil.MethodImplAttributes.Runtime) Then
            Return result
        End If

        Helper.Assert(Info Is Nothing)
        Dim parent As IType = Me.FindTypeParent
        Info = New EmitInfo(Me)

#If DEBUG Then
        Info.ILGen.Emit(Mono.Cecil.Cil.OpCodes.Nop)
#End If

        Dim ParentType As Mono.Cecil.TypeReference
        ParentType = parent.CecilType
        If TypeOf parent Is StructureDeclaration AndAlso Me.IsShared = False Then
            Emitter.EmitLoadMe(Info, parent.CecilType)
            Emitter.EmitInitObj(Info, parent.CecilType)
        ElseIf m_DefaultBaseConstructor IsNot Nothing Then
            Dim params As Mono.Collections.Generic.Collection(Of ParameterDefinition) = m_DefaultBaseConstructor.Parameters
            Emitter.EmitLoadMe(Info, CecilHelper.FindDefinition(ParentType).BaseType)
            For i As Integer = 0 To params.Count - 1
                Helper.Assert(params(i).IsOptional)
                Emitter.EmitLoadValue(Info.Clone(Me, True, False, params(i).ParameterType), params(i).Constant)
            Next

            Emitter.EmitCall(Info, m_DefaultBaseConstructor)
        ElseIf m_BaseCtorCall IsNot Nothing Then
            result = m_BaseCtorCall.GenerateCode(Info)
        Else
            Helper.Assert(Me.IsShared)
        End If

        Dim exCtorCall As Mono.Cecil.MethodReference = ExplicitCtorCall
        If m_BaseCtorCall Is Nothing OrElse (exCtorCall IsNot Nothing AndAlso Helper.CompareType(exCtorCall.DeclaringType, Me.DeclaringType.CecilType) = False) Then
            result = EmitVariableInitialization(Info) AndAlso result

            For Each arhs As AddOrRemoveHandlerStatement In Me.DeclaringType.AddHandlers
                result = arhs.GenerateCode(Info) AndAlso result
            Next
        End If

        If Me.IsShared Then
            result = EmitConstantInitialization(Info) AndAlso result
        End If

#If DEBUG Then
        Info.ILGen.Emit(Mono.Cecil.Cil.OpCodes.Nop)
#End If

        result = MyBase.GenerateCode(Info) AndAlso result

        Return result
    End Function

    Private Function EmitVariableInitialization(ByVal Info As EmitInfo) As Boolean
        Dim variables As Generic.List(Of TypeVariableDeclaration)
        Dim parent As TypeDeclaration
        Dim result As Boolean = True

        parent = Me.DeclaringType
        variables = parent.Members.GetSpecificMembers(Of TypeVariableDeclaration)()

        For Each variable As TypeVariableDeclaration In variables
            If variable.HasInitializer AndAlso variable.IsShared = Me.IsShared Then
                result = variable.EmitVariableInitializer(Info) AndAlso result
            End If
        Next

        For Each variable As LocalVariableDeclaration In parent.StaticVariables
            If variable.HasInitializer AndAlso variable.DeclaringMethod.IsShared = Me.IsShared Then
                If Me.IsShared = False Then Emitter.EmitLoadMe(Info, Me.DeclaringType.CecilType)
                Emitter.EmitNew(Info, Compiler.TypeCache.MS_VB_CS_StaticLocalInitFlag__ctor)
                Emitter.EmitStoreField(Info, CecilHelper.GetCorrectMember(variable.StaticInitBuilder, variable.StaticInitBuilder.DeclaringType))
            End If
        Next

        Return result
    End Function

    Private Function EmitConstantInitialization(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True
        Dim parent As TypeDeclaration

        Parent = Me.DeclaringType

        For Each variable As ConstantDeclaration In Parent.Members.GetSpecificMembers(Of ConstantDeclaration)()
            If Helper.CompareType(variable.FieldType, Compiler.TypeCache.System_DateTime) Then
                Emitter.EmitLoadDateValue(Info, DirectCast(variable.ConstantValue, Date))
                Emitter.EmitStoreField(Info, variable.FieldBuilder)
            ElseIf Helper.CompareType(variable.FieldType, Compiler.TypeCache.System_Decimal) Then
                Emitter.EmitLoadDecimalValue(Info, DirectCast(variable.ConstantValue, Decimal))
                Emitter.EmitStoreField(Info, variable.FieldBuilder)
            End If
        Next

        Return result
    End Function

    Private Sub CreateDefaultCtorCall()
        Dim type As TypeDeclaration = Me.FindFirstParent(Of TypeDeclaration)()
        Dim classtype As ClassDeclaration = TryCast(type, ClassDeclaration)
        Dim defaultctor As Mono.Cecil.MethodReference
        If classtype IsNot Nothing Then
            defaultctor = classtype.GetBaseDefaultConstructor()
            If defaultctor IsNot Nothing AndAlso Helper.IsPrivate(defaultctor) = False Then
                If Helper.IsPrivate(defaultctor) OrElse (Helper.IsFriend(defaultctor) AndAlso Not Compiler.Assembly.IsDefinedHere(defaultctor.DeclaringType)) Then
                    Helper.AddError(Me, "Base class does not have an accessible default constructor")
                Else
                    m_DefaultBaseConstructor = defaultctor

#If DEBUG Then
                    Try
                        For Each param As Mono.Cecil.ParameterDefinition In m_DefaultBaseConstructor.Parameters
                            Helper.Assert(param.IsOptional)
                        Next
                    Catch ex As Exception
                        Helper.Assert(False)
                    End Try
#End If
                End If
            Else
                Helper.AddError(Me, "Base class does not have a default constructor")
            End If
        End If
    End Sub

    Private Sub CreateDefaultCtorCallCecil()
        Dim type As TypeDeclaration = Me.FindFirstParent(Of TypeDeclaration)()
        Dim classtype As ClassDeclaration = TryCast(type, ClassDeclaration)
        Dim defaultctor As Mono.Cecil.MethodReference
        If classtype IsNot Nothing Then
            defaultctor = classtype.GetBaseDefaultConstructorCecil()
            If defaultctor IsNot Nothing AndAlso Helper.IsPrivate(defaultctor) = False Then
                If Helper.IsPrivate(defaultctor) OrElse (Helper.IsFamilyOrAssembly(defaultctor) AndAlso defaultctor.DeclaringType.Module.Assembly IsNot Me.Compiler.AssemblyBuilderCecil) Then
                    Helper.AddError(Compiler, Location, "Base class does not have an accessible default constructor")
                Else
                    m_DefaultBaseConstructorCecil = defaultctor
                    m_DefaultBaseConstructorCecil = Helper.GetMethodOrMethodReference(Compiler, m_DefaultBaseConstructorCecil)

#If DEBUG Then
                    Try
                        For Each param As Mono.Cecil.ParameterDefinition In m_DefaultBaseConstructor.Parameters
                            Helper.Assert(param.IsOptional)
                        Next
                    Catch ex As Exception
                        Helper.Assert(False)
                    End Try
#End If
                End If
            Else
                Helper.AddError(Compiler, Location, "Base class does not have a default constructor")
            End If
        End If
    End Sub

    Shared Function IsMe(ByVal tm As tm) As Boolean
        Dim i As Integer
        While tm.PeekToken(i).Equals(ModifierMasks.ConstructorModifiers)
            i += 1
        End While
        If tm.PeekToken(i).Equals(KS.Sub) = False Then Return False
        Return tm.PeekToken(i + 1).Equals(KS.[New])
    End Function
End Class
