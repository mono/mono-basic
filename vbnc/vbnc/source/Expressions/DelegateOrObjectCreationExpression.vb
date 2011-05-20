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
''' DelegateCreationExpression ::= "New" NonArrayTypeName "(" Expression ")"
''' ObjectCreationExpression   ::= "New" NonArrayTypeName [ "(" [ ArgumentList ] ")" ]
''' 
''' A New expression is classified as a value and the result is the new instance of the type.
''' </summary>
''' <remarks></remarks>
Public Class DelegateOrObjectCreationExpression
    Inherits Expression

    Private m_NonArrayTypeName As NonArrayTypeName
    Private m_ArgumentList As ArgumentList

    Private m_ResolvedType As Mono.Cecil.TypeReference
    Private m_MethodClassification As MethodGroupClassification
    Private m_IsDelegateCreationExpression As Boolean
    ''' <summary>
    ''' If this is a value type creation expression with no parameters.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_IsValueTypeInitializer As Boolean
    Private m_IsGenericConstructor As Boolean

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        If m_NonArrayTypeName IsNot Nothing Then
            result = m_NonArrayTypeName.ResolveTypeReferences AndAlso result
            m_ResolvedType = m_NonArrayTypeName.ResolvedType
        End If
        If m_ArgumentList IsNot Nothing Then result = m_ArgumentList.ResolveTypeReferences AndAlso result

        Return result
    End Function

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal DelegateType As Mono.Cecil.TypeReference, ByVal AddressOfExpression As AddressOfExpression)
        MyBase.New(Parent)

        m_IsDelegateCreationExpression = True
        m_ResolvedType = DelegateType
        m_ArgumentList = New ArgumentList(Me, AddressOfExpression)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal TypeName As NonArrayTypeName, ByVal ArgumentList As ArgumentList)
        MyBase.new(Parent)
        Me.Init(TypeName, ArgumentList)
    End Sub

    Sub Init(ByVal TypeName As NonArrayTypeName, ByVal ArgumentList As ArgumentList)
        m_NonArrayTypeName = TypeName
        m_ArgumentList = ArgumentList
    End Sub

    Sub Init(ByVal Type As Mono.Cecil.TypeReference, ByVal ArgumentList As ArgumentList)
        m_ResolvedType = Type
        m_ArgumentList = ArgumentList
    End Sub

    ReadOnly Property NonArrayTypeName() As NonArrayTypeName
        Get
            Return m_NonArrayTypeName
        End Get
    End Property

    ReadOnly Property IsDelegateCreationExpression() As Boolean
        Get
            Return m_IsDelegateCreationExpression
        End Get
    End Property

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        If m_IsDelegateCreationExpression Then
            result = m_ArgumentList(0).Expression.Classification.AsMethodPointerClassification.GenerateCode(Info) AndAlso result
        ElseIf m_IsValueTypeInitializer Then
            If CecilHelper.IsByRef(Info.DesiredType) Then
                Dim type As Mono.Cecil.TypeReference = Helper.GetTypeOrTypeBuilder(Compiler, m_ResolvedType)
                Emitter.EmitInitObj(Info, type)
            Else
                Dim type As Mono.Cecil.TypeReference = Helper.GetTypeOrTypeBuilder(Compiler, m_ResolvedType)
                Dim local As Mono.Cecil.Cil.VariableDefinition = Emitter.DeclareLocal(Info, type)
                Emitter.EmitLoadVariableLocation(Info, local)
                Emitter.EmitInitObj(Info, type)
                Emitter.EmitLoadVariable(Info, local)
            End If
        ElseIf m_IsGenericConstructor Then
            Dim method As Mono.Cecil.MethodReference
            Dim args As Mono.Cecil.TypeReference() = New Mono.Cecil.TypeReference() {Helper.GetTypeOrTypeBuilder(Compiler, ExpressionType)}
            method = CecilHelper.MakeGenericMethod(Compiler.TypeCache.System_Activator__CreateInstance, args)
            Emitter.EmitCall(Info, method)
        Else
            Dim ctor As Mono.Cecil.MethodReference
            ctor = m_MethodClassification.ResolvedConstructor

            result = m_ArgumentList.GenerateCode(Info, ctor.Parameters) AndAlso result

            Emitter.EmitNew(Info, ctor)
        End If

        Return result
    End Function

    Overrides ReadOnly Property ExpressionType() As Mono.Cecil.TypeReference
        Get
            Return Classification.AsValueClassification.Type
        End Get
    End Property

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        If m_NonArrayTypeName IsNot Nothing Then result = m_NonArrayTypeName.ResolveCode(Info) AndAlso result
        If m_ArgumentList IsNot Nothing Then result = m_ArgumentList.ResolveCode(Info) AndAlso result

        Helper.Assert(m_ResolvedType IsNot Nothing)
        If m_IsDelegateCreationExpression = False Then
            If TypeOf m_ResolvedType Is Mono.Cecil.GenericParameter = False Then
                m_IsDelegateCreationExpression = Helper.CompareType(CecilHelper.FindDefinition(m_ResolvedType).BaseType, Compiler.TypeCache.System_MulticastDelegate)
            End If
        End If

        If m_ArgumentList IsNot Nothing Then
            result = m_ArgumentList.ResolveCode(Info) AndAlso result
        Else
            m_ArgumentList = New ArgumentList(Me)
        End If

        If result = False Then Return result

        If m_IsDelegateCreationExpression Then
            Dim type As Mono.Cecil.TypeReference = m_ResolvedType
            If m_ArgumentList.Count <> 1 Then
                result = Compiler.Report.ShowMessage(Messages.VBNC32008, Me.Location, type.FullName) AndAlso result
            End If
            If result AndAlso m_ArgumentList(0).Expression.Classification.IsMethodPointerClassification = False Then
                result = Compiler.Report.ShowMessage(Messages.VBNC32008, Me.Location, type.FullName) AndAlso result
            End If
            If result Then
                result = m_ArgumentList(0).Expression.ResolveAddressOfExpression(type, True) AndAlso result
                Classification = New ValueClassification(Me, type)
            End If
        Else
            Dim resolvedType As Mono.Cecil.TypeReference = m_ResolvedType
            Dim isCoClass As Boolean = False

            If CecilHelper.IsInterface(resolvedType) Then
                Dim coClass As TypeReference = Helper.GetCoClassType(Compiler, resolvedType)
                If coClass Is Nothing Then
                    Return Compiler.Report.ShowMessage(Messages.VBNC30375, Me.Location)
                Else
                    m_ResolvedType = coClass
                    resolvedType = m_ResolvedType

                    If CecilHelper.IsValueType(resolvedType) = False AndAlso CecilHelper.IsClass(resolvedType) = False Then
                        Return Compiler.Report.ShowMessage(Messages.VBNC31094, Me.Location, resolvedType.Name)
                    End If

                    Dim ctors As Mono.Collections.Generic.Collection(Of MethodReference)
                    ctors = CecilHelper.GetConstructors(resolvedType)
                    If ctors Is Nothing OrElse ctors.Count = 0 Then
                        Return Compiler.Report.ShowMessage(Messages.VBNC30251, Me.Location, resolvedType.Name)
                    End If
                    isCoClass = True
                End If
            End If

            If isCoClass = False AndAlso CecilHelper.IsValueType(resolvedType) AndAlso m_ArgumentList.Count = 0 Then
                'Nothing to resolve. A structure with no parameters can always be created.
                m_IsValueTypeInitializer = True
            ElseIf CecilHelper.IsGenericParameter(resolvedType) Then
                If m_ArgumentList.Count > 0 Then
                    Return Compiler.Report.ShowMessage(Messages.VBNC32085, Me.Location)
                End If
                If (CecilHelper.GetGenericParameterAttributes(resolvedType) And Mono.Cecil.GenericParameterAttributes.DefaultConstructorConstraint) = 0 AndAlso (CecilHelper.GetGenericParameterAttributes(resolvedType) And Mono.Cecil.GenericParameterAttributes.NotNullableValueTypeConstraint) = 0 Then
                    Return Compiler.Report.ShowMessage(Messages.VBNC32046, Me.Location)
                End If
                m_IsGenericConstructor = True
            ElseIf CecilHelper.IsClass(resolvedType) OrElse CecilHelper.IsValueType(resolvedType) Then
                Dim ctors As Mono.Collections.Generic.Collection(Of Mono.Cecil.MethodReference)

                ctors = CecilHelper.GetConstructors(resolvedType)
                m_MethodClassification = New MethodGroupClassification(Me, Nothing, Nothing, Nothing, ctors)
                result = m_MethodClassification.AsMethodGroupClassification.ResolveGroup(m_ArgumentList, , False) AndAlso result
                If result = False Then
                    'Show the error
                    result = m_MethodClassification.AsMethodGroupClassification.ResolveGroup(m_ArgumentList, True, False) AndAlso result
                Else
                    result = m_ArgumentList.ReplaceAndVerifyArguments(m_MethodClassification.FinalArguments, m_MethodClassification.ResolvedMethod, True) AndAlso result
                End If
            Else
                Compiler.Report.ShowMessage(Messages.VBNC99999, Me.Location, String.Format("Delegate type {0} is neither ValueType, GenericParameter nor Class. This is a problem in the compiler, please file a bug report here: http://bugzilla.novell.com", resolvedType.FullName))
            End If

            Classification = New ValueClassification(Me, resolvedType)
        End If

        Return result
    End Function
End Class

