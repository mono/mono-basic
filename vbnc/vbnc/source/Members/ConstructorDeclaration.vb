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

    Private m_Descriptor As New ConstructorDescriptor(Me)

    Public Const ConstructorName As String = ".ctor"
    Public Const SharedConstructorName As String = ".cctor"

    Private m_ConstructorBuilder As ConstructorBuilder

    ''' <summary>
    ''' The default base constructor to call if no call is specified in the code.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_DefaultBaseConstructor As ConstructorInfo

    ''' <summary>
    ''' The base/self constructor call in the code.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_BaseCtorCall As Statement

    Sub New(ByVal Parent As TypeDeclaration)
        MyBase.New(Parent)
    End Sub

    Shadows Sub Init(ByVal Code As CodeBlock)
        MyBase.Init(New Attributes(Me), New Modifiers(), New SubSignature(Me, ConstructorName, New ParameterList(Me)), Code)
    End Sub

    Shadows Sub Init(ByVal Attributes As Attributes, ByVal Modifiers As Modifiers, ByVal Signature As SubSignature, ByVal Block As CodeBlock)

        'If vbnc.Modifiers.IsNothing(Modifiers) = False AndAlso Modifiers.Is(ModifierMasks.Shared) Then
        If Modifiers.Is(ModifierMasks.Shared) Then
            Signature.Init(New Identifier(Signature, SharedConstructorName, Signature.Location, TypeCharacters.Characters.None), Signature.TypeParameters, Signature.Parameters)
        Else
            Signature.Init(New Identifier(Signature, ConstructorName, Signature.Location, TypeCharacters.Characters.None), Signature.TypeParameters, Signature.Parameters)
        End If

        MyBase.Init(Attributes, Modifiers, Signature, Block)
    End Sub

    Shared Function CreateTypeConstructor(ByVal Parent As TypeDeclaration) As ConstructorDeclaration
        Dim result As New ConstructorDeclaration(Parent)

        result.Init(New Attributes(result), New Modifiers(ModifierMasks.Shared), New SubSignature(result, SharedConstructorName, New ParameterList(result)), New CodeBlock(result))

        If result.ResolveTypeReferences() = False Then
            Helper.ErrorRecoveryNotImplemented()
        End If

        Return result
    End Function

    Shared Function CreateDefaultConstructor(ByVal Parent As TypeDeclaration) As ConstructorDeclaration
        Dim result As New ConstructorDeclaration(Parent)

        result.Init(New Attributes(result), New Modifiers(), New SubSignature(result, ConstructorName, New ParameterList(result)), New CodeBlock(result))

        If result.ResolveTypeReferences() = False Then
            Helper.ErrorRecoveryNotImplemented()
        End If

        Return result
    End Function

    ReadOnly Property ConstructorDescriptor() As ConstructorDescriptor Implements IConstructorMember.ConstructorDescriptor
        Get
            Return m_Descriptor
        End Get
    End Property

    Public Overrides ReadOnly Property MethodDescriptor() As MethodBase
        Get
            Return m_Descriptor
        End Get
    End Property

    Overrides ReadOnly Property MemberDescriptor() As MemberInfo
        Get
            Return m_Descriptor
        End Get
    End Property

    ReadOnly Property ConstructorBuilder() As ConstructorBuilder Implements IConstructorMember.ConstructorBuilder
        Get
            Return m_ConstructorBuilder
        End Get
    End Property


    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        ResolveFlags()

        result = MyBase.ResolveTypeReferences AndAlso result


        Return result
    End Function

    Sub ResolveFlags()
        If MethodAttributes.HasValue = False Then
            Dim flags As MethodAttributes
            flags = Reflection.MethodAttributes.SpecialName Or Reflection.MethodAttributes.RTSpecialName

            'LAMESPEC: shared constructors have implicit public access.
            'VBC: shared constructors defaults to private.
            If Modifiers.IsAny(ModifierMasks.AccessModifiers) = False AndAlso Me.IsShared Then
                flags = flags Or Reflection.MethodAttributes.Private
            Else
                flags = flags Or Me.Modifiers.GetMethodAttributeScope
            End If

            If Me.IsShared Then
                flags = flags Or Reflection.MethodAttributes.Static
            End If

            Attributes = flags
        End If

        If MethodImplAttributes.HasValue = False Then
            Me.SetImplementationFlags(Reflection.MethodImplAttributes.IL)
        End If
    End Sub

    Overrides Function ResolveMember(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = MyBase.ResolveMember(Info) AndAlso result

        Return result
    End Function

    ReadOnly Property ExplicitCtorCall() As ConstructorInfo
        Get
            Dim firststatement As BaseObject
            Dim cs As CallStatement
            Dim ie As InvocationOrIndexExpression
            Dim ctor As ConstructorInfo

            firststatement = Code.FirstStatement
            If firststatement Is Nothing Then Return Nothing

            cs = TryCast(firststatement, CallStatement)
            If cs Is Nothing Then Return Nothing

            ie = TryCast(cs.Target, InvocationOrIndexExpression)
            If ie Is Nothing Then Return Nothing
            If ie.Expression.Classification.IsMethodGroupClassification = False Then Return Nothing

            ctor = ie.Expression.Classification.AsMethodGroupClassification.ResolvedConstructor
            If ctor Is Nothing Then Return Nothing

            If Helper.CompareType(ctor.DeclaringType, Me.FindTypeParent.BaseType) Then
                Return ctor
            ElseIf Helper.CompareType(ctor.DeclaringType, Me.FindTypeParent.TypeDescriptor) Then
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

        Me.CheckCodeNotResolved()

        result = MyBase.ResolveCode(Info) AndAlso result

        If result = False Then Return result

        If Me.IsShared = False AndAlso Me.HasMethodBody AndAlso Me.HasExplicitCtorCall = False Then
            CreateDefaultCtorCall()
        ElseIf Code IsNot Nothing AndAlso Me.HasExplicitCtorCall Then
            m_BaseCtorCall = Code.FirstStatement
            If m_BaseCtorCall IsNot Nothing Then Code.RemoveStatement(m_BaseCtorCall)
        End If

        Return result
    End Function

    Public Overrides Function DefineMember() As Boolean
        Dim result As Boolean = True

        result = MyBase.DefineMember AndAlso result

        Dim declaringType As TypeDeclaration = Me.FindFirstParent(Of TypeDeclaration)()

        Helper.SetTypeOrTypeBuilder(ParameterTypes)

        m_ConstructorBuilder = declaringType.TypeBuilder.DefineConstructor(Me.Attributes, CallingConventions.Standard, ParameterTypes)
        m_ConstructorBuilder.SetImplementationFlags(Me.GetMethodImplementationFlags)
        Compiler.TypeManager.RegisterReflectionMember(m_ConstructorBuilder, Me.MemberDescriptor)

#If DEBUGREFLECTION Then
        Helper.DebugReflection_AppendLine("{0} = {1}.DefineConstructor(CType({2}, System.Reflection.MethodAttributes), System.Reflection.CallingConventions.Standard, Nothing)", m_ConstructorBuilder, declaringType.TypeBuilder, CInt(Me.Attributes).ToString)
        Helper.DebugReflection_AppendLine("{0}.SetImplementationFlags(CType({1}, System.Reflection.MethodImplAttributes))", m_ConstructorBuilder, CInt(Me.GetMethodImplementationFlags).ToString)
#End If

        For i As Integer = 0 To Signature.Parameters.Count - 1
            result = Signature.Parameters(i).Define(Me.ConstructorBuilder) AndAlso result
        Next

        Compiler.Helper.DumpDefine(Me, m_ConstructorBuilder)

        Return result
    End Function

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        If CBool(MethodImplAttributes.Value And Reflection.MethodImplAttributes.Runtime) Then
            Return result
        End If

        Helper.Assert(Info Is Nothing)
        Dim parent As IType = Me.FindTypeParent
        Info = New EmitInfo(Me)

#If DEBUG Then
        Info.ILGen.Emit(OpCodes.Nop)
#End If

        Dim ParentType As Type
        ParentType = parent.TypeBuilder
        If TypeOf parent Is StructureDeclaration AndAlso Me.IsShared = False Then
            Emitter.EmitLoadMe(Info, parent.TypeBuilder)
            Emitter.EmitInitObj(Info, parent.TypeBuilder)
        ElseIf m_DefaultBaseConstructor IsNot Nothing Then
            Dim params() As ParameterInfo = m_DefaultBaseConstructor.GetParameters
            Emitter.EmitLoadMe(Info, ParentType.BaseType)
            For i As Integer = 0 To params.Length - 1
                Helper.Assert(params(i).IsOptional)
                Emitter.EmitLoadValue(Info.Clone(Me, True, False, params(i).ParameterType), params(i).DefaultValue)
            Next

            Emitter.EmitCall(Info, m_DefaultBaseConstructor)
        ElseIf m_BaseCtorCall IsNot Nothing Then
            result = m_BaseCtorCall.GenerateCode(Info)
        Else
            Helper.Assert(Me.IsShared)
        End If

        Dim exCtorCall As ConstructorInfo = ExplicitCtorCall
        If m_BaseCtorCall Is Nothing OrElse (exCtorCall IsNot Nothing AndAlso Helper.CompareType(exCtorCall.DeclaringType, Me.DeclaringType.TypeDescriptor) = False) Then
            result = EmitVariableInitialization(Info) AndAlso result

            For Each arhs As AddOrRemoveHandlerStatement In Me.DeclaringType.AddHandlers
                result = arhs.GenerateCode(Info) AndAlso result
            Next
        End If

        If Me.IsShared Then
            result = EmitConstantInitialization(Info) AndAlso result
        End If

#If DEBUG Then
        Info.ILGen.Emit(OpCodes.Nop)
#End If

        result = MyBase.GenerateCode(Info) AndAlso result

        Return result
    End Function

    Private Function EmitVariableInitialization(ByVal Info As EmitInfo) As Boolean
        Dim variables As Generic.List(Of VariableDeclaration)
        Dim parent As TypeDeclaration
        Dim result As Boolean = True

        parent = Me.DeclaringType
        variables = parent.Members.GetSpecificMembers(Of VariableDeclaration)()

        For Each variable As VariableDeclaration In variables
            If variable.HasInitializer AndAlso variable.IsShared = Me.IsShared Then
                result = variable.EmitVariableInitializer(Info) AndAlso result
            End If
        Next

        For Each variable As VariableDeclaration In parent.StaticVariables
            If variable.HasInitializer AndAlso variable.DeclaringMethod.IsShared = Me.IsShared Then
                If Me.IsShared = False Then Emitter.EmitLoadMe(Info, Me.DeclaringType.TypeDescriptor)
                Emitter.EmitNew(Info, Compiler.TypeCache.MS_VB_CS_StaticLocalInitFlag__ctor)
                Emitter.EmitStoreField(Info, variable.StaticInitBuilder)
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
        Dim defaultctor As ConstructorInfo
        If classtype IsNot Nothing Then
            defaultctor = classtype.GetBaseDefaultConstructor()
            If defaultctor IsNot Nothing AndAlso defaultctor.IsPrivate = False Then
                If defaultctor.IsPrivate OrElse (defaultctor.IsFamilyOrAssembly AndAlso defaultctor.DeclaringType.Assembly IsNot Me.Compiler.AssemblyBuilder) Then
                    Helper.AddError(Me, "Base class does not have an accessible default constructor")
                Else
                    m_DefaultBaseConstructor = defaultctor

#If DEBUG Then
                    Try
                        For Each param As ParameterInfo In m_DefaultBaseConstructor.GetParameters
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

    Shared Function IsMe(ByVal tm As tm) As Boolean
        Dim i As Integer
        While tm.PeekToken(i).Equals(ModifierMasks.ConstructorModifiers)
            i += 1
        End While
        If tm.PeekToken(i).Equals(KS.Sub) = False Then Return False
        Return tm.PeekToken(i + 1).Equals(KS.[New])
    End Function

    Public Overrides ReadOnly Property MethodBuilder() As System.Reflection.Emit.MethodBuilder
        Get
            Throw New InternalException(Me)
        End Get
    End Property

    Public Overrides ReadOnly Property ILGenerator() As System.Reflection.Emit.ILGenerator
        Get
            Return m_ConstructorBuilder.GetILGenerator
        End Get
    End Property
End Class