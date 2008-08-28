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
Public Class CTypeExpression
    Inherits ConversionExpression

    Private m_DestinationType As TypeName
    Private m_ResolvedDestinationType As Mono.Cecil.TypeReference
    Private m_IsStringToCharArray As Boolean

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        If m_DestinationType IsNot Nothing Then
            result = m_DestinationType.ResolveTypeReferences AndAlso result
            m_ResolvedDestinationType = m_DestinationType.ResolvedType
            Helper.Assert(CecilHelper.IsByRef(m_ResolvedDestinationType) = False)
        End If

        result = MyBase.ResolveTypeReferences() AndAlso result

        Return result
    End Function

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Expression As Expression, ByVal DestinationType As Mono.Cecil.TypeReference)
        MyBase.New(Parent, Expression)
        m_ResolvedDestinationType = DestinationType
        Helper.Assert(CecilHelper.IsByRef(m_ResolvedDestinationType) = False, "Can't create TypeConversion to byref type (trying to convert from " & Expression.ExpressionType.FullName & " to " & DestinationType.FullName)
    End Sub

    Shadows Sub Init(ByVal Expression As Expression, ByVal DestinationType As TypeName)
        MyBase.Init(Expression)
        m_DestinationType = DestinationType
    End Sub

    Shadows Sub Init(ByVal Expression As Expression, ByVal DestinationType As Mono.Cecil.TypeReference)
        MyBase.Init(Expression)
        m_ResolvedDestinationType = DestinationType
    End Sub


    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        Dim expType As Mono.Cecil.TypeReference = Me.ExpressionType
        Dim expTypeCode As TypeCode = Helper.GetTypeCode(Compiler, expType)

        Select Case expTypeCode
            Case TypeCode.Boolean
                CBoolExpression.GenerateCode(Me.Expression, Info)
            Case TypeCode.Byte
                CByteExpression.GenerateCode(Me.Expression, Info)
            Case TypeCode.Char
                CCharExpression.GenerateCode(Me.Expression, Info)
            Case TypeCode.DateTime
                CDateExpression.GenerateCode(Me.Expression, Info)
            Case TypeCode.Decimal
                CDecExpression.GenerateCode(Me.Expression, Info)
            Case TypeCode.Double
                CDblExpression.GenerateCode(Me.Expression, Info)
            Case TypeCode.Int16
                CShortExpression.GenerateCode(Me.Expression, Info)
            Case TypeCode.Int32
                CIntExpression.GenerateCode(Me.Expression, Info)
            Case TypeCode.Int64
                CLngExpression.GenerateCode(Me.Expression, Info)
            Case TypeCode.SByte
                CSByteExpression.GenerateCode(Me.Expression, Info)
            Case TypeCode.Single
                CSngExpression.GenerateCode(Me.Expression, Info)
            Case TypeCode.String
                CStrExpression.GenerateCode(Me.Expression, Info)
            Case TypeCode.UInt16
                CUShortExpression.GenerateCode(Me.Expression, Info)
            Case TypeCode.UInt32
                CUIntExpression.GenerateCode(Me.Expression, Info)
            Case TypeCode.UInt64
                CULngExpression.GenerateCode(Me.Expression, Info)
            Case TypeCode.Object
                If Helper.CompareType(expType, Compiler.TypeCache.System_Object) Then
                    CObjExpression.GenerateCode(Me.Expression, Info)
                Else
                    result = GenerateCTypeCode(Info, expType, Me.Expression.ExpressionType)
                End If
            Case Else
                Throw New InternalException(Me)
        End Select

        Return result
    End Function

    Public Shared Function GenerateUserDefinedConversionCode(ByVal Info As EmitInfo, ByVal Expression As Expression, ByVal DestinationType As Mono.Cecil.TypeReference) As Boolean
        Dim result As Boolean = True
        Dim exptype As Mono.Cecil.TypeReference = Expression.ExpressionType
        Dim ops As Generic.List(Of Mono.Cecil.MethodReference)
        ops = Helper.GetWideningConversionOperators(Info.Compiler, exptype, DestinationType)
        If ops.Count = 0 Then
            ops = Helper.GetNarrowingConversionOperators(Info.Compiler, exptype, DestinationType)
            Helper.AddWarning("narrowing operator")
        End If

        If ops.Count = 0 Then
            Helper.AddError(Expression, "Cannot convert from '" & exptype.Name & "' to '" & DestinationType.Name & "'")
            result = False
        ElseIf ops.Count > 1 Then
            Helper.AddError(Expression, "Cannot convert from '" & exptype.Name & "' to '" & DestinationType.Name & "'")
            result = False
        Else
            Emitter.EmitCall(Info, ops(0))
        End If
        Return result
    End Function

    Private Function EmitStringToCharArray(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        result = Expression.GenerateCode(Info.Clone(Info.Context, Compiler.TypeCache.System_String)) AndAlso result
        Emitter.EmitCall(Info, Info.Compiler.TypeCache.MS_VB_CS_Conversions__ToCharArrayRankOne_String)

        Return result
    End Function

    Private Function GenerateCTypeCode(ByVal Info As EmitInfo, ByVal DestinationType As Mono.Cecil.TypeReference, ByVal SourceType As Mono.Cecil.TypeReference) As Boolean
        Dim result As Boolean = True

        If m_IsStringToCharArray Then
            Return EmitStringToCharArray(Info) AndAlso result
        End If

        result = Expression.Classification.GenerateCode(Info.Clone(Me, DestinationType)) AndAlso result

        If Helper.CompareType(Compiler.TypeCache.Nothing, SourceType) Then
            'There is nothing to do here
        ElseIf CecilHelper.IsGenericParameter(SourceType) Then
            If CecilHelper.IsGenericParameter(DestinationType) Then
                Return Compiler.Report.ShowMessage(Messages.VBNC99997, Location)
            ElseIf CecilHelper.IsArray(DestinationType) Then
                Return Compiler.Report.ShowMessage(Messages.VBNC99997, Location)
            ElseIf CecilHelper.IsClass(DestinationType) Then
                DestinationType = Helper.GetTypeOrTypeBuilder(Compiler, DestinationType)
                If Helper.IsTypeConvertibleToAny(Helper.GetGenericParameterConstraints(Me, SourceType), DestinationType) Then
                    'Emitter.EmitUnbox_Any(Info, DestinationType)
                    Emitter.EmitBox(Info, SourceType)
                    Emitter.EmitCastClass(Info, SourceType, DestinationType)
                Else
                    Helper.AddError(Me)
                End If
            ElseIf CecilHelper.IsValueType(DestinationType) Then
                Return Compiler.Report.ShowMessage(Messages.VBNC99997, Location)
            ElseIf Helper.IsInterface(Compiler, DestinationType) Then
                Emitter.EmitBox(Info, SourceType)
                Emitter.EmitCastClass(Info, SourceType, DestinationType)
            Else
                Throw New InternalException(Me)
            End If
        ElseIf CecilHelper.IsArray(SourceType) Then
            If CecilHelper.IsInterface(DestinationType) Then
                If Helper.DoesTypeImplementInterface(Me, SourceType, DestinationType) Then
                    Emitter.EmitCastClass(Info, SourceType, DestinationType)
                ElseIf Helper.CompareType(SourceType, Compiler.TypeCache.System_Object_Array) Then
                    Emitter.EmitCastClass(Info, SourceType, DestinationType)
                ElseIf CecilHelper.IsArray(DestinationType) AndAlso Helper.DoesTypeImplementInterface(Me, CecilHelper.GetElementType(SourceType), CecilHelper.GetElementType(DestinationType)) Then
                    Emitter.EmitCastClass(Info, SourceType, DestinationType)
                Else
                    Info.Compiler.Report.ShowMessage(Messages.VBNC30311, SourceType.Name, DestinationType.Name)
                    result = False
                End If
            ElseIf Helper.CompareType(DestinationType, Compiler.TypeCache.System_Array) Then
                Emitter.EmitCastClass(Info, SourceType, DestinationType)
            ElseIf CecilHelper.IsArray(DestinationType) = False Then
                Info.Compiler.Report.ShowMessage(Messages.VBNC30311, SourceType.Name, DestinationType.Name)
                result = False
            ElseIf CecilHelper.GetArrayRank(SourceType) <> CecilHelper.GetArrayRank(DestinationType) Then
                Info.Compiler.Report.ShowMessage(Messages.VBNC30311, SourceType.Name, DestinationType.Name)
                result = False
            Else
                Dim SourceElementType As Mono.Cecil.TypeReference = CecilHelper.GetElementType(SourceType)
                Dim DestinationElementType As Mono.Cecil.TypeReference = CecilHelper.GetElementType(DestinationType)
                'For any two reference types A and B, if A is a derived type of B or implements B, 
                'a conversion exists from an array of type A to a compatible array of type B.
                'A compatible array is an array of the same rank and type. 
                'This relationship is known as array covariance. 
                'Array covariance in particular means that an element of an array whose element type is B 
                'may actually be an element of an array whose element type is A, 
                'provided that both A and B are reference types and that B is a base type of A or is implemented by A. 
                If Helper.CompareType(Compiler.TypeCache.System_Object, SourceElementType) Then
                    Emitter.EmitCastClass(Info, SourceType, DestinationType)
                ElseIf Helper.CompareType(SourceElementType, DestinationElementType) OrElse Helper.IsSubclassOf(DestinationElementType, SourceElementType) OrElse Helper.IsSubclassOf(SourceElementType, DestinationElementType) Then
                    Emitter.EmitCastClass(Info, SourceType, DestinationType)
                ElseIf Helper.DoesTypeImplementInterface(Me, SourceElementType, DestinationElementType) Then
                    Emitter.EmitCastClass(Info, SourceType, DestinationType)
                ElseIf Helper.IsInterface(Info.Compiler, DestinationElementType) AndAlso Helper.CompareType(Compiler.TypeCache.System_Object, SourceElementType) Then
                    Emitter.EmitCastClass(Info, SourceType, DestinationType)
                ElseIf helper.IsEnum(compiler, sourceelementtype)AndAlso Helper.CompareType(Helper.GetEnumType(Compiler, SourceElementType), DestinationElementType) Then
                    'Conversions also exist between an array of an enumerated type and an array of the enumerated type's underlying type of the same rank.
                    Emitter.EmitCastClass(Info, SourceType, DestinationType)
                ElseIf CecilHelper.IsGenericParameter(SourceElementType) AndAlso Helper.IsTypeConvertibleToAny(Helper.GetGenericParameterConstraints(Me, SourceElementType), DestinationElementType) Then
                    Emitter.EmitCastClass(Info, SourceType, DestinationType)
                Else
                    Info.Compiler.Report.ShowMessage(Messages.VBNC30311, SourceType.Name, DestinationType.Name)
                    result = False
                End If

            End If
        ElseIf CecilHelper.IsClass(SourceType) Then
            If CecilHelper.IsGenericParameter(DestinationType) Then
                Dim methodD As New Mono.Cecil.GenericInstanceMethod(Helper.GetMethodOrMethodReference(Compiler, Compiler.TypeCache.MS_VB_CS_Conversions__ToGenericParameter_Object))
                methodD.GenericParameters.Add(DirectCast(CecilHelper.GetGenericArguments(Compiler.TypeCache.MS_VB_CS_Conversions__ToGenericParameter_Object)(0), Mono.Cecil.GenericParameter))
                methodD.GenericArguments.Add(DestinationType)

                Emitter.EmitCall(Info, Compiler.TypeCache.System_Runtime_CompilerServices_RuntimeHelpers__GetObjectValue_Object)
                Emitter.EmitCall(Info, methodD)
            ElseIf CecilHelper.IsClass(DestinationType) Then
                Emitter.EmitCastClass(Info, SourceType, DestinationType)
            ElseIf CecilHelper.IsInterface(DestinationType) Then
                Emitter.EmitCastClass(Info, SourceType, DestinationType)
            ElseIf CecilHelper.IsValueType(DestinationType) Then
                Emitter.EmitUnbox(Info, DestinationType)
                Emitter.EmitLdobj(Info, DestinationType)
            ElseIf Helper.IsEnum(Compiler, DestinationType) Then
                Throw New InternalException(Me) 'This is an elemental conversion already covered.
            ElseIf CecilHelper.IsArray(DestinationType) Then
                Throw New InternalException(Me) 'This is an IsClass case.
            Else
                Throw New InternalException(Me)
            End If
        ElseIf CecilHelper.IsValueType(SourceType) Then
            'A value type value can be converted to one of its base reference types or an interface type that it implements through a process called boxing
            If Helper.CompareType(DestinationType, Compiler.TypeCache.System_Object) Then
                Throw New InternalException(Me) 'This is an elemental conversion already covered. 'Emitter.EmitBox(Info)
            ElseIf Helper.DoesTypeImplementInterface(Me, SourceType, DestinationType) Then
                Emitter.EmitBox(Info, SourceType)
                Emitter.EmitCastClass(Info, Compiler.TypeCache.System_Object, DestinationType)
            ElseIf Helper.CompareType(CecilHelper.FindDefinition(SourceType).BaseType, DestinationType) Then
                Emitter.EmitBox(Info, DestinationType)
            Else
                Throw New InternalException("Operator CType is not defined for types '" & SourceType.FullName & "' and '" & DestinationType.FullName & "'")
            End If
        ElseIf Helper.IsInterface(Compiler, SourceType) Then
            If CecilHelper.IsGenericParameter(DestinationType) Then
                'Dim method As MethodInfo
                Throw New NotImplementedException
                'Dim methodD As New GenericMethodDescriptor(Me, Compiler.TypeCache.MS_VB_CS_Conversions__ToGenericParameter_Object, Compiler.TypeCache.MS_VB_CS_Conversions__ToGenericParameter_Object.GetGenericArguments(), New Type() {DestinationType.UnderlyingSystemType})

                'Emitter.EmitCall(Info, Compiler.TypeCache.System_Runtime_CompilerServices_RuntimeHelpers__GetObjectValue_Object)
                'Emitter.EmitCall(Info, methodD)
            ElseIf Helper.DoesTypeImplementInterface(Me, DestinationType, SourceType) Then
                If CecilHelper.IsValueType(DestinationType) Then
                    Emitter.EmitUnbox(Info, DestinationType)
                    Emitter.EmitLdobj(Info, DestinationType)
                Else
                    Emitter.EmitCastClass(Info, SourceType, DestinationType)
                End If

            ElseIf CecilHelper.IsClass(DestinationType) OrElse CecilHelper.IsInterface(DestinationType) Then
                Emitter.EmitCastClass(Info, SourceType, DestinationType)
            Else
                'However, classes that represent COM classes may have interface implementations that are not known until run time. Consequently, a class type may also be converted to an interface type that it does not implement, an interface type may be converted to a class type that does not implement it, and an interface type may be converted to another interface type with which it has no inheritance relationship
                Return Compiler.Report.ShowMessage(Messages.VBNC99997, Location)
            End If
        Else
            Return Compiler.Report.ShowMessage(Messages.VBNC99997, Location)
        End If

        Return result
    End Function

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        If m_ResolvedDestinationType Is Nothing Then
            result = m_DestinationType.ResolveTypeReferences AndAlso result
            m_ResolvedDestinationType = m_DestinationType.ResolvedType
        End If

        result = MyBase.ResolveExpressionInternal(Info) AndAlso result

        Select Case Helper.GetTypeCode(Compiler, Me.ExpressionType)
            Case TypeCode.Boolean
                result = CBoolExpression.Validate(Info, Expression.ExpressionType) AndAlso result
            Case TypeCode.Byte
                result = CByteExpression.Validate(Info, Expression.ExpressionType) AndAlso result
            Case TypeCode.Char
                result = CCharExpression.Validate(Info, Expression.ExpressionType) AndAlso result
            Case TypeCode.DateTime
                result = CDateExpression.Validate(Info, Expression.ExpressionType) AndAlso result
            Case TypeCode.Decimal
                result = CDecExpression.Validate(Info, Expression.ExpressionType) AndAlso result
            Case TypeCode.Double
                result = CDblExpression.Validate(Info, Expression.ExpressionType) AndAlso result
            Case TypeCode.Int16
                result = CShortExpression.Validate(Info, Expression.ExpressionType) AndAlso result
            Case TypeCode.Int32
                result = CIntExpression.Validate(Info, Expression.ExpressionType) AndAlso result
            Case TypeCode.Int64
                result = CLngExpression.Validate(Info, Expression.ExpressionType) AndAlso result
            Case TypeCode.SByte
                result = CSByteExpression.Validate(Info, Expression.ExpressionType) AndAlso result
            Case TypeCode.Single
                result = CSngExpression.Validate(Info, Expression.ExpressionType) AndAlso result
            Case TypeCode.String
                result = CStrExpression.Validate(Info, Expression.ExpressionType) AndAlso result
            Case TypeCode.UInt16
                result = CUShortExpression.Validate(Info, Expression.ExpressionType) AndAlso result
            Case TypeCode.UInt32
                result = CUIntExpression.Validate(Info, Expression.ExpressionType) AndAlso result
            Case TypeCode.UInt64
                result = CULngExpression.Validate(Info, Expression.ExpressionType) AndAlso result
            Case TypeCode.Object, TypeCode.DBNull
                If Helper.CompareType(Me.ExpressionType, Compiler.TypeCache.System_Object) Then
                    result = CObjExpression.Validate(Info, Expression.ExpressionType) AndAlso result
                ElseIf Helper.CompareType(Me.ExpressionType, Compiler.TypeCache.System_Char_Array) AndAlso Helper.CompareType(Expression.ExpressionType, Compiler.TypeCache.System_String) Then
                    If Location.File(Compiler).IsOptionStrictOn Then
                        result = Compiler.Report.ShowMessage(Messages.VBNC30512, Location, Expression.ExpressionType.FullName, Me.ExpressionType.FullName)
                    Else
                        m_IsStringToCharArray = True
                    End If
                Else
                    'Helper.NotImplementedYet("") Anything to do here?
                End If
            Case Else
                Throw New InternalException(Me)
        End Select

        Return result


        Return result
    End Function

    Overrides ReadOnly Property ExpressionType() As Mono.Cecil.TypeReference
        Get
            Return m_ResolvedDestinationType
        End Get
    End Property

    Public Overrides ReadOnly Property ConstantValue() As Object
        Get
            Select Case Helper.GetTypeCode(Compiler, Me.ExpressionType)
                Case TypeCode.String
                    Select Case Helper.GetTypeCode(Compiler, Me.Expression.ExpressionType)
                        Case TypeCode.Char
                            Return CStr(Expression.ConstantValue)
                        Case Else
                            Return Compiler.Report.ShowMessage(Messages.VBNC99997, Location)
                    End Select
                Case Else
                    Return Compiler.Report.ShowMessage(Messages.VBNC99997, Location)
            End Select
        End Get
    End Property

    Public Overrides ReadOnly Property IsConstant() As Boolean
        Get
            If Expression.IsConstant Then
                If m_ResolvedDestinationType IsNot Nothing AndAlso Helper.CompareType(m_ResolvedDestinationType, Compiler.TypeCache.System_String) AndAlso Helper.CompareType(Expression.ExpressionType, Compiler.TypeCache.System_Char) Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
            Return False 'TODO: This isn't true.
        End Get
    End Property

    Protected Overridable ReadOnly Property GetKeyword() As KS
        Get
            Return KS.CType
        End Get
    End Property

End Class
