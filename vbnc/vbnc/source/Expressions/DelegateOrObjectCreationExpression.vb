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

    Private m_ResolvedType As Type
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

    Sub New(ByVal Parent As ParsedObject, ByVal DelegateType As Type, ByVal AddressOfExpression As AddressOfExpression)
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

    Sub Init(ByVal Type As Type, ByVal ArgumentList As ArgumentList)
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
            If Info.DesiredType.IsByRef Then
                Dim type As Type = Helper.GetTypeOrTypeBuilder(m_ResolvedType)
                Emitter.EmitInitObj(Info, type)
            Else
                Dim type As Type = Helper.GetTypeOrTypeBuilder(m_ResolvedType)
                Dim local As LocalBuilder = Info.ILGen.DeclareLocal(type)
                Emitter.EmitLoadVariableLocation(Info, local)
                Emitter.EmitInitObj(Info, type)
                Emitter.EmitLoadVariable(Info, local)
            End If
        ElseIf m_IsGenericConstructor Then
            Dim method As MethodInfo
            Dim args As Type() = New Type() {Helper.GetTypeOrTypeBuilder(ExpressionType)}
            method = Compiler.TypeCache.System_Activator__CreateInstance.MakeGenericMethod(args)
            Emitter.EmitCall(Info, method)
        Else
            Dim ctor As ConstructorInfo
            ctor = m_MethodClassification.ResolvedConstructor

            result = m_ArgumentList.GenerateCode(Info, ctor.GetParameters) AndAlso result

            Emitter.EmitNew(Info, ctor)
        End If

        Return result
    End Function

    Overrides ReadOnly Property ExpressionType() As Type
        Get
            Return Classification.AsValueClassification.Type
        End Get
    End Property

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        Helper.Assert(m_ResolvedType IsNot Nothing)
        If m_IsDelegateCreationExpression = False Then
            m_IsDelegateCreationExpression = Helper.CompareType(m_ResolvedType.BaseType, Compiler.TypeCache.System_MulticastDelegate)
        End If

        If m_ArgumentList IsNot Nothing Then
            result = m_ArgumentList.ResolveCode(Info) AndAlso result
        Else
            m_ArgumentList = New ArgumentList(Me)
        End If

        If result = False Then Return result

        If m_IsDelegateCreationExpression Then
            Dim type As Type = m_ResolvedType
            If m_ArgumentList.Count <> 1 Then
                result = Compiler.Report.ShowMessage(Messages.VBNC32008) AndAlso result
            End If
            If result AndAlso m_ArgumentList(0).Expression.Classification.IsMethodPointerClassification = False Then
                result = Compiler.Report.ShowMessage(Messages.VBNC32008) AndAlso result
            End If
            If result Then
                result = m_ArgumentList(0).Expression.ResolveAddressOfExpression(type) AndAlso result
                Classification = New ValueClassification(Me, type)
            End If
        Else
            Dim resolvedType As Type = m_ResolvedType
            If resolvedType.IsValueType AndAlso m_ArgumentList.Count = 0 Then
                'Nothing to resolve. A structure with no parameters can always be created.
                m_IsValueTypeInitializer = True
            ElseIf resolvedType.IsGenericParameter Then
                If m_ArgumentList.Count > 0 Then
                    Return Compiler.Report.ShowMessage(Messages.VBNC32085, Me.Location)
                End If
                If (resolvedType.GenericParameterAttributes And GenericParameterAttributes.DefaultConstructorConstraint) = 0 AndAlso (resolvedType.GenericParameterAttributes And GenericParameterAttributes.NotNullableValueTypeConstraint) = 0 Then
                    Return Compiler.Report.ShowMessage(Messages.VBNC32046, Me.Location)
                End If
                m_IsGenericConstructor = True
            ElseIf resolvedType.IsClass OrElse resolvedType.IsValueType Then
                Dim ctors As ConstructorInfo()
                Dim finalArguments As Generic.List(Of Argument) = Nothing
                ctors = resolvedType.GetConstructors(BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Instance Or BindingFlags.DeclaredOnly)
                m_MethodClassification = New MethodGroupClassification(Me, Nothing, Nothing, ctors)
                result = m_MethodClassification.AsMethodGroupClassification.ResolveGroup(m_ArgumentList, finalArguments) AndAlso result
                If result = False Then
                    Helper.AddError(Me, "Delegate problems 3, " & Me.Location.ToString(Compiler) & ">" & Me.Parent.Location.ToString(Compiler))
                Else
                    result = m_ArgumentList.ReplaceAndVerifyArguments(finalArguments, m_MethodClassification.ResolvedMethod) AndAlso result
                End If
            Else
                Helper.AddError(Me, "Delegate problems 4, " & Me.Location.ToString(Compiler))
            End If

            Classification = New ValueClassification(Me, resolvedType)
        End If

        Return result
    End Function

#If DEBUG Then
    Public Overrides Sub Dump(ByVal Dumper As IndentedTextWriter)
        Dumper.Write("New ")
        Compiler.Dumper.Dump(m_NonArrayTypeName)
        Dumper.Write("(")
        Compiler.Dumper.Dump(m_ArgumentList)
        Dumper.Write(")")
    End Sub
#End If

End Class
