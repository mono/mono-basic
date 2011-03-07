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
Public Class CTypeExpression
    Inherits ConversionExpression

    Private m_DestinationType As TypeName
    Private m_ResolvedDestinationType As Mono.Cecil.TypeReference
    Private m_IsStringToCharArray As Boolean
    Private m_ConversionType As CTypeConversionType

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

    Sub New(ByVal Parent As ParsedObject, ByVal IsExplicit As Boolean)
        MyBase.New(Parent, IsExplicit)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Expression As Expression, ByVal DestinationType As Mono.Cecil.TypeReference)
        MyBase.New(Parent, Expression)
        m_ResolvedDestinationType = DestinationType
        Helper.Assert(CecilHelper.IsByRef(m_ResolvedDestinationType) = False, "Can't create TypeConversion to byref type (trying to convert from " & Expression.ExpressionType.FullName & " to " & DestinationType.FullName)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Expression As Expression, ByVal DestinationType As TypeReference, ByVal ConversionType As CTypeConversionType)
        MyBase.New(Parent, Expression)
        m_ResolvedDestinationType = DestinationType
        m_ConversionType = ConversionType
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

        If m_ConversionType <> CTypeConversionType.Undetermined AndAlso m_ConversionType <> CTypeConversionType.Intrinsic Then
            result = Expression.Classification.GenerateCode(Info.Clone(Me, ExpressionType)) AndAlso result
        End If

        Select Case m_ConversionType
            Case CTypeConversionType.FromNullable
                Dim nullable_dst_type As GenericInstanceType
                Dim explicit_op As MethodReference
                Dim git As GenericInstanceType

                nullable_dst_type = New GenericInstanceType(Helper.GetTypeOrTypeReference(Compiler, Compiler.TypeCache.System_Nullable1))
                nullable_dst_type.GenericArguments.Add(Helper.GetTypeOrTypeReference(Compiler, CecilHelper.GetNulledType(Expression.ExpressionType)))
                git = New GenericInstanceType(Helper.GetTypeOrTypeReference(Compiler, Compiler.TypeCache.System_Nullable1))
                git.GenericArguments.Add(Compiler.TypeCache.System_Nullable1.GenericParameters(0))
                explicit_op = New MethodReference("op_Explicit", nullable_dst_type, ExpressionType, False, False, MethodCallingConvention.Default)
                explicit_op.Parameters.Add(New ParameterDefinition(git))
                Emitter.EmitCall(Info, explicit_op)
                Return True
            Case CTypeConversionType.ToNullable
                Dim nullable_dst_type As GenericInstanceType
                Dim implicit_op As MethodReference

                nullable_dst_type = New GenericInstanceType(Helper.GetTypeOrTypeReference(Compiler, Compiler.TypeCache.System_Nullable1))
                nullable_dst_type.GenericArguments.Add(Helper.GetTypeOrTypeReference(Compiler, CecilHelper.GetNulledType(ExpressionType)))
                implicit_op = New MethodReference("op_Implicit", nullable_dst_type, nullable_dst_type, False, False, MethodCallingConvention.Default)
                implicit_op.Parameters.Add(New ParameterDefinition(Compiler.TypeCache.System_Nullable1.GenericParameters(0)))
                Emitter.EmitCall(Info, implicit_op)
                Return True
            Case CTypeConversionType.Castclass
                Emitter.EmitCastClass(Info, ExpressionType)
                Return True
            Case CTypeConversionType.Identity
                'There is nothing to do here
                Return True
            Case CTypeConversionType.Box
                Emitter.EmitBox(Info, Expression.ExpressionType)
                Return True
            Case CTypeConversionType.Box_CastClass
                Emitter.EmitBox(Info, Expression.ExpressionType)
                Emitter.EmitCastClass(Info, ExpressionType)
                Return True
            Case CTypeConversionType.Unbox
                Emitter.EmitUnbox(Info, ExpressionType)
                Return True
            Case CTypeConversionType.Unbox_Ldobj
                Emitter.EmitUnbox(Info, ExpressionType)
                Emitter.EmitLdobj(Info, ExpressionType)
                Return True
            Case CTypeConversionType.Unbox_Any
                Emitter.EmitUnbox_Any(Info, ExpressionType)
                Return True
            Case CTypeConversionType.MS_VB_CS_Conversions_ToGenericParameter
                Dim gim As New GenericInstanceMethod(Compiler.TypeCache.MS_VB_CS_Conversions__ToGenericParameter_Object)
                gim.GenericArguments.Add(ExpressionType)
                Emitter.EmitCall(Info, gim)
                Return True
            Case CTypeConversionType.UserDefinedOperator
                Emitter.EmitCall(Info, ConversionMethod)
                Return True
            Case CTypeConversionType.NullableToNullable
                Dim SourceType As TypeReference = Expression.ExpressionType
                Dim DestinationType As TypeReference = ExpressionType
                Dim nullable_src_type As GenericInstanceType
                Dim nullable_dst_type As GenericInstanceType
                Dim get_value As MethodReference
                Dim has_value As MethodReference
                Dim ctor As MethodReference
                Dim localsrc, localdst As Mono.Cecil.Cil.VariableDefinition
                Dim falseLabel As Label = Emitter.DefineLabel(Info)
                Dim endLabel As Label = Emitter.DefineLabel(Info)
                Dim vose As ValueOnStackExpression
                Dim type_conversion As Expression

                nullable_src_type = New GenericInstanceType(Helper.GetTypeOrTypeReference(Compiler, Compiler.TypeCache.System_Nullable1))
                nullable_src_type.GenericArguments.Add(Helper.GetTypeOrTypeReference(Compiler, CecilHelper.GetNulledType(SourceType)))
                has_value = New MethodReference("get_HasValue", nullable_src_type, Helper.GetTypeOrTypeReference(Compiler, Compiler.TypeCache.System_Boolean), True, False, MethodCallingConvention.Default)
                get_value = New MethodReference("GetValueOrDefault", nullable_src_type, Compiler.TypeCache.System_Nullable1.GenericParameters(0), True, False, MethodCallingConvention.Default)

                nullable_dst_type = New GenericInstanceType(Helper.GetTypeOrTypeReference(Compiler, Compiler.TypeCache.System_Nullable1))
                nullable_dst_type.GenericArguments.Add(Helper.GetTypeOrTypeReference(Compiler, CecilHelper.GetNulledType(DestinationType)))
                ctor = New MethodReference(".ctor", nullable_dst_type, Helper.GetTypeOrTypeReference(Compiler, Compiler.TypeCache.System_Void), True, False, MethodCallingConvention.Default)
                ctor.Parameters.Add(New ParameterDefinition(Compiler.TypeCache.System_Nullable1.GenericParameters(0)))

                'store in local
                localsrc = Emitter.DeclareLocal(Info, SourceType)
                Emitter.EmitStoreVariable(Info, localsrc)

                'call Nullable`1.HasValue to check the condition
                Emitter.EmitLoadVariableLocation(Info, localsrc)
                Emitter.EmitCall(Info, has_value)
                Emitter.EmitBranchIfFalse(Info, falseLabel)

                localdst = Emitter.DeclareLocal(Info, DestinationType)

                'true branch: we have a value, get it to create a new nullable with the right value
                Emitter.EmitLoadVariableLocation(Info, localdst)
                Emitter.EmitLoadVariableLocation(Info, localsrc)
                Emitter.EmitCall(Info, get_value)

                'convert value
                vose = New ValueOnStackExpression(Me, CecilHelper.GetNulledType(SourceType))
                type_conversion = Helper.CreateTypeConversion(Me, vose, CecilHelper.GetNulledType(DestinationType), result)
                result = type_conversion.GenerateCode(Info) AndAlso result

                Emitter.EmitCall(Info, ctor)
                Emitter.EmitLoadVariable(Info, localdst)
                Emitter.EmitBranch(Info, endLabel)

                'false branch: no value
                Emitter.MarkLabel(Info, falseLabel)
                Emitter.EmitLoadVariableLocation(Info, localdst)
                Emitter.EmitInitObj(Info, localdst.VariableType)
                Emitter.EmitLoadVariable(Info, localdst)

                'end
                Emitter.MarkLabel(Info, endLabel)
                Return True
        End Select

        Select Case expTypeCode
            Case TypeCode.Boolean
                CBoolExpression.GenerateCode(Me, Info)
            Case TypeCode.Byte
                CByteExpression.GenerateCode(Me, Info)
            Case TypeCode.Char
                CCharExpression.GenerateCode(Me, Info)
            Case TypeCode.DateTime
                CDateExpression.GenerateCode(Me, Info)
            Case TypeCode.Decimal
                CDecExpression.GenerateCode(Me, Info)
            Case TypeCode.Double
                CDblExpression.GenerateCode(Me, Info)
            Case TypeCode.Int16
                CShortExpression.GenerateCode(Me, Info)
            Case TypeCode.Int32
                CIntExpression.GenerateCode(Me, Info)
            Case TypeCode.Int64
                CLngExpression.GenerateCode(Me, Info)
            Case TypeCode.SByte
                CSByteExpression.GenerateCode(Me, Info)
            Case TypeCode.Single
                CSngExpression.GenerateCode(Me, Info)
            Case TypeCode.String
                CStrExpression.GenerateCode(Me, Info)
            Case TypeCode.UInt16
                CUShortExpression.GenerateCode(Me, Info)
            Case TypeCode.UInt32
                CUIntExpression.GenerateCode(Me, Info)
            Case TypeCode.UInt64
                CULngExpression.GenerateCode(Me, Info)
            Case TypeCode.Object, TypeCode.DBNull
                If Helper.CompareType(expType, Compiler.TypeCache.System_Object) Then
                    CObjExpression.GenerateCode(Me.Expression, Info)
                Else
                    result = GenerateCTypeCode(Info, expType, Me.Expression.ExpressionType)
                End If
            Case Else
                Return Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Location)
        End Select

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
                    Emitter.EmitCastClass(Info, DestinationType)
                Else
                    Helper.AddError(Me)
                End If
            ElseIf CecilHelper.IsValueType(DestinationType) Then
                Return Compiler.Report.ShowMessage(Messages.VBNC99997, Location)
            ElseIf Helper.IsInterface(Compiler, DestinationType) Then
                Emitter.EmitBox(Info, SourceType)
                Emitter.EmitCastClass(Info, DestinationType)
            Else
                Throw New InternalException(Me)
            End If
        ElseIf CecilHelper.IsArray(SourceType) Then
            If CecilHelper.IsInterface(DestinationType) Then
                If Helper.DoesTypeImplementInterface(Me, SourceType, DestinationType) Then
                    Emitter.EmitCastClass(Info, DestinationType)
                ElseIf Helper.CompareType(SourceType, Compiler.TypeCache.System_Object_Array) Then
                    Emitter.EmitCastClass(Info, DestinationType)
                ElseIf CecilHelper.IsArray(DestinationType) AndAlso Helper.DoesTypeImplementInterface(Me, CecilHelper.GetElementType(SourceType), CecilHelper.GetElementType(DestinationType)) Then
                    Emitter.EmitCastClass(Info, DestinationType)
                Else
                    Info.Compiler.Report.ShowMessage(Messages.VBNC30311, Location, Helper.ToString(Expression, SourceType), Helper.ToString(Expression, DestinationType))
                    result = False
                End If
            ElseIf Helper.CompareType(DestinationType, Compiler.TypeCache.System_Array) Then
                Emitter.EmitCastClass(Info, DestinationType)
            ElseIf CecilHelper.IsArray(DestinationType) = False Then
                Info.Compiler.Report.ShowMessage(Messages.VBNC30311, Location, Helper.ToString(Expression, SourceType), Helper.ToString(Expression, DestinationType))
                result = False
            ElseIf CecilHelper.GetArrayRank(SourceType) <> CecilHelper.GetArrayRank(DestinationType) Then
                Info.Compiler.Report.ShowMessage(Messages.VBNC30311, Location, Helper.ToString(Expression, SourceType), Helper.ToString(Expression, DestinationType))
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
                    Emitter.EmitCastClass(Info, DestinationType)
                ElseIf Helper.CompareType(SourceElementType, DestinationElementType) OrElse Helper.IsSubclassOf(DestinationElementType, SourceElementType) OrElse Helper.IsSubclassOf(SourceElementType, DestinationElementType) Then
                    Emitter.EmitCastClass(Info, DestinationType)
                ElseIf Helper.DoesTypeImplementInterface(Me, SourceElementType, DestinationElementType) Then
                    Emitter.EmitCastClass(Info, DestinationType)
                ElseIf Helper.IsInterface(Info.Compiler, DestinationElementType) AndAlso Helper.CompareType(Compiler.TypeCache.System_Object, SourceElementType) Then
                    Emitter.EmitCastClass(Info, DestinationType)
                ElseIf Helper.IsEnum(Compiler, SourceElementType) AndAlso Helper.CompareType(Helper.GetEnumType(Compiler, SourceElementType), DestinationElementType) Then
                    'Conversions also exist between an array of an enumerated type and an array of the enumerated type's underlying type of the same rank.
                    Emitter.EmitCastClass(Info, DestinationType)
                ElseIf CecilHelper.IsGenericParameter(SourceElementType) AndAlso Helper.IsTypeConvertibleToAny(Helper.GetGenericParameterConstraints(Me, SourceElementType), DestinationElementType) Then
                    Emitter.EmitCastClass(Info, DestinationType)
                ElseIf CecilHelper.IsGenericParameter(DestinationElementType) AndAlso Helper.IsTypeConvertibleToAny(SourceElementType, Helper.GetGenericParameterConstraints(Me, DestinationElementType)) Then
                    Emitter.EmitCastClass(Info, DestinationType)
                Else
                    Info.Compiler.Report.ShowMessage(Messages.VBNC30311, Me.Location, SourceType.Name, DestinationType.Name)
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
                Emitter.EmitCastClass(Info, DestinationType)
            ElseIf CecilHelper.IsInterface(DestinationType) Then
                Emitter.EmitCastClass(Info, DestinationType)
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
        ElseIf CecilHelper.IsNullable(DestinationType) Then
            If CecilHelper.IsNullable(SourceType) Then
                Dim nullable_src_type As GenericInstanceType
                Dim nullable_dst_type As GenericInstanceType
                Dim get_value As MethodReference
                Dim has_value As MethodReference
                Dim ctor As MethodReference
                Dim localsrc, localdst As Mono.Cecil.Cil.VariableDefinition
                Dim falseLabel As Label = Emitter.DefineLabel(Info)
                Dim endLabel As Label = Emitter.DefineLabel(Info)
                Dim vose As ValueOnStackExpression
                Dim type_conversion As Expression

                nullable_src_type = New GenericInstanceType(Helper.GetTypeOrTypeReference(Compiler, Compiler.TypeCache.System_Nullable1))
                nullable_src_type.GenericArguments.Add(Helper.GetTypeOrTypeReference(Compiler, CecilHelper.GetNulledType(SourceType)))
                has_value = New MethodReference("get_HasValue", nullable_src_type, Helper.GetTypeOrTypeReference(Compiler, Compiler.TypeCache.System_Boolean), True, False, MethodCallingConvention.Default)
                get_value = New MethodReference("GetValueOrDefault", nullable_src_type, Compiler.TypeCache.System_Nullable1.GenericParameters(0), True, False, MethodCallingConvention.Default)

                nullable_dst_type = New GenericInstanceType(Helper.GetTypeOrTypeReference(Compiler, Compiler.TypeCache.System_Nullable1))
                nullable_dst_type.GenericArguments.Add(Helper.GetTypeOrTypeReference(Compiler, CecilHelper.GetNulledType(DestinationType)))
                ctor = New MethodReference(".ctor", nullable_dst_type, Helper.GetTypeOrTypeReference(Compiler, Compiler.TypeCache.System_Void), True, False, MethodCallingConvention.Default)
                ctor.Parameters.Add(New ParameterDefinition(Compiler.TypeCache.System_Nullable1.GenericParameters(0)))

                'store in local
                localsrc = Emitter.DeclareLocal(Info, SourceType)
                Emitter.EmitStoreVariable(Info, localsrc)

                'call Nullable`1.HasValue to check the condition
                Emitter.EmitLoadVariableLocation(Info, localsrc)
                Emitter.EmitCall(Info, has_value)
                Emitter.EmitBranchIfFalse(Info, falseLabel)

                localdst = Emitter.DeclareLocal(Info, DestinationType)

                'true branch: we have a value, get it to create a new nullable with the right value
                Emitter.EmitLoadVariableLocation(Info, localdst)
                Emitter.EmitLoadVariableLocation(Info, localsrc)
                Emitter.EmitCall(Info, get_value)

                'convert value
                vose = New ValueOnStackExpression(Me, CecilHelper.GetNulledType(SourceType))
                type_conversion = Helper.CreateTypeConversion(Me, vose, CecilHelper.GetNulledType(DestinationType), result)
                result = type_conversion.GenerateCode(Info) AndAlso result

                Emitter.EmitCall(Info, ctor)
                Emitter.EmitLoadVariable(Info, localdst)
                Emitter.EmitBranch(Info, endLabel)

                'false branch: no value
                Emitter.MarkLabel(Info, falseLabel)
                Emitter.EmitLoadVariableLocation(Info, localdst)
                Emitter.EmitInitObj(Info, localdst.VariableType)
                Emitter.EmitLoadVariable(Info, localdst)

                'end
                Emitter.MarkLabel(Info, endLabel)
            Else
                Dim nullable_dst_type As GenericInstanceType
                Dim ctor As MethodReference
                Dim localsrc, localdst As Mono.Cecil.Cil.VariableDefinition
                Dim falseLabel As Label = Emitter.DefineLabel(Info)
                Dim endLabel As Label = Emitter.DefineLabel(Info)
                Dim vose As ValueOnStackExpression
                Dim type_conversion As Expression

                nullable_dst_type = New GenericInstanceType(Helper.GetTypeOrTypeReference(Compiler, Compiler.TypeCache.System_Nullable1))
                nullable_dst_type.GenericArguments.Add(Helper.GetTypeOrTypeReference(Compiler, CecilHelper.GetNulledType(DestinationType)))
                ctor = New MethodReference(".ctor", nullable_dst_type, Helper.GetTypeOrTypeReference(Compiler, Compiler.TypeCache.System_Void), True, False, MethodCallingConvention.Default)
                ctor.Parameters.Add(New ParameterDefinition(Compiler.TypeCache.System_Nullable1.GenericParameters(0)))

                'store in local
                localsrc = Emitter.DeclareLocal(Info, SourceType)
                Emitter.EmitStoreVariable(Info, localsrc)

                localdst = Emitter.DeclareLocal(Info, DestinationType)

                Emitter.EmitLoadVariableLocation(Info, localdst)
                Emitter.EmitLoadVariable(Info, localsrc)

                'convert value
                vose = New ValueOnStackExpression(Me, SourceType)
                type_conversion = Helper.CreateTypeConversion(Me, vose, CecilHelper.GetNulledType(DestinationType), result)
                result = type_conversion.GenerateCode(Info) AndAlso result

                Emitter.EmitCall(Info, ctor)
                Emitter.EmitLoadVariable(Info, localdst)
            End If
        ElseIf CecilHelper.IsValueType(SourceType) Then
            'A value type value can be converted to one of its base reference types or an interface type that it implements through a process called boxing
            If Helper.CompareType(DestinationType, Compiler.TypeCache.System_Object) Then
                Throw New InternalException(Me) 'This is an elemental conversion already covered. 'Emitter.EmitBox(Info)
            ElseIf Helper.DoesTypeImplementInterface(Me, SourceType, DestinationType) Then
                Emitter.EmitBox(Info, SourceType)
                Emitter.EmitCastClass(Info, DestinationType)
            ElseIf Helper.CompareType(DestinationType, Compiler.TypeCache.System_ValueType) Then
                Emitter.EmitBox(Info, SourceType)
            ElseIf Helper.CompareType(CecilHelper.FindDefinition(SourceType).BaseType, DestinationType) Then
                Emitter.EmitBox(Info, DestinationType)
            Else
                Dim operators As Generic.List(Of Mono.Cecil.MethodReference)
                operators = Helper.GetWideningConversionOperators(Info.Compiler, SourceType, DestinationType)
                If operators Is Nothing OrElse operators.Count = 0 Then
                    Helper.AddWarning("using narrowing operators")
                    operators = Helper.GetNarrowingConversionOperators(Info.Compiler, SourceType, DestinationType)
                End If
                If operators IsNot Nothing AndAlso operators.Count > 0 Then
                    If operators.Count = 1 Then
                        Emitter.EmitCall(Info, operators(0))
                    Else
                        result = Compiler.Report.ShowMessage(Messages.VBNC30311, Me.Location, Expression.ExpressionType.FullName, ExpressionType.FullName) AndAlso result
                    End If
                Else
                    result = Compiler.Report.ShowMessage(Messages.VBNC30311, Me.Location, Helper.ToString(Compiler, Expression.ExpressionType), Helper.ToString(Compiler, ExpressionType)) AndAlso result
                End If
            End If
        ElseIf Helper.IsInterface(Compiler, SourceType) Then
            If CecilHelper.IsGenericParameter(DestinationType) Then
                Emitter.EmitUnbox_Any(Info, DestinationType)
            ElseIf Helper.DoesTypeImplementInterface(Me, DestinationType, SourceType) Then
                If CecilHelper.IsValueType(DestinationType) Then
                    Emitter.EmitUnbox(Info, DestinationType)
                    Emitter.EmitLdobj(Info, DestinationType)
                Else
                    Emitter.EmitCastClass(Info, DestinationType)
                End If

            ElseIf CecilHelper.IsClass(DestinationType) OrElse CecilHelper.IsInterface(DestinationType) Then
                Emitter.EmitCastClass(Info, DestinationType)
            Else
                'However, classes that represent COM classes may have interface implementations that are not known until run time. Consequently, a class type may also be converted to an interface type that it does not implement, an interface type may be converted to a class type that does not implement it, and an interface type may be converted to another interface type with which it has no inheritance relationship
                Return Compiler.Report.ShowMessage(Messages.VBNC99997, Location)
            End If
        Else
            Return Compiler.Report.ShowMessage(Messages.VBNC99997, Location)
        End If

        Return result
    End Function

    Public Overrides Function GetConstant(ByRef result As Object, ByVal ShowError As Boolean) As Boolean
        If Helper.CompareType(Compiler.TypeCache.Nothing, Expression.ExpressionType) Then
            Select Case Helper.GetTypeCode(Compiler, Me.ExpressionType)
                Case TypeCode.Boolean
                    result = CBool(Nothing)
                Case TypeCode.Byte
                    result = CByte(Nothing)
                Case TypeCode.Char
                    result = CChar(Nothing)
                Case TypeCode.DateTime
                    result = CDate(Nothing)
                Case TypeCode.Decimal
                    result = CDec(Nothing)
                Case TypeCode.Double
                    result = CDbl(Nothing)
                Case TypeCode.Int16
                    result = CShort(Nothing)
                Case TypeCode.Int32
                    result = CInt(Nothing)
                Case TypeCode.Int64
                    result = CLng(Nothing)
                Case TypeCode.SByte
                    result = CSByte(Nothing)
                Case TypeCode.Single
                    result = CSng(Nothing)
                Case TypeCode.UInt16
                    result = CUShort(Nothing)
                Case TypeCode.UInt32
                    result = CUInt(Nothing)
                Case TypeCode.UInt64
                    result = CULng(Nothing)
                Case Else
                    result = Nothing
            End Select
            Return True
        End If

        If Not Expression.GetConstant(result, ShowError) Then Return False

        Select Case Helper.GetTypeCode(Compiler, Me.ExpressionType)
            Case TypeCode.String
                Select Case Helper.GetTypeCode(Compiler, Me.Expression.ExpressionType)
                    Case TypeCode.Char
                        result = CStr(result)
                        Return True
                    Case TypeCode.String
                        Return True
                    Case Else
                        If ShowError Then Show30059()
                        Return False
                End Select
            Case TypeCode.Byte
                Return ConvertToByte(result, ShowError)
            Case TypeCode.SByte
                Return ConvertToSByte(result, ShowError)
            Case TypeCode.Int16
                Return ConvertToShort(result, ShowError)
            Case TypeCode.UInt16
                Return ConvertToUShort(result, ShowError)
            Case TypeCode.Int32
                Return ConvertToInt32(result, ShowError)
            Case TypeCode.UInt32
                Return ConvertToUInt32(result, ShowError)
            Case TypeCode.Int64
                Return ConvertToLong(result, ShowError)
            Case TypeCode.UInt64
                Return ConvertToULong(result, ShowError)
            Case TypeCode.Single
                Return ConvertToSingle(result, ShowError)
            Case TypeCode.Double
                Return ConvertToDouble(result, ShowError)
            Case TypeCode.Decimal
                Return ConvertToDecimal(result, ShowError)
            Case TypeCode.DateTime
                Return ConvertToDate(result, ShowError)
            Case TypeCode.Char
                Return ConvertToChar(result, ShowError)
            Case TypeCode.String
                Return ConvertToString(result, ShowError)
            Case TypeCode.Boolean
                Return ConvertToBoolean(result, ShowError)
            Case Else
                If ShowError Then Show30059()
                Return False
        End Select

        Return False
    End Function

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        If m_ResolvedDestinationType Is Nothing Then
            result = m_DestinationType.ResolveTypeReferences AndAlso result
            m_ResolvedDestinationType = m_DestinationType.ResolvedType
        End If

        result = MyBase.ResolveExpressionInternal(Info) AndAlso result

        If m_ConversionType <> CTypeConversionType.Undetermined Then Return result

        Select Case Helper.GetTypeCode(Compiler, Me.ExpressionType)
            Case TypeCode.Boolean
                result = CBoolExpression.Validate(Info, Me) AndAlso result
            Case TypeCode.Byte
                result = CByteExpression.Validate(Info, Me) AndAlso result
            Case TypeCode.Char
                result = CCharExpression.Validate(Info, Me) AndAlso result
            Case TypeCode.DateTime
                result = CDateExpression.Validate(Info, Me) AndAlso result
            Case TypeCode.Decimal
                result = CDecExpression.Validate(Info, Me) AndAlso result
            Case TypeCode.Double
                result = CDblExpression.Validate(Info, Me) AndAlso result
            Case TypeCode.Int16
                result = CShortExpression.Validate(Info, Me) AndAlso result
            Case TypeCode.Int32
                result = CIntExpression.Validate(Info, Me) AndAlso result
            Case TypeCode.Int64
                result = CLngExpression.Validate(Info, Me) AndAlso result
            Case TypeCode.SByte
                result = CSByteExpression.Validate(Info, Me) AndAlso result
            Case TypeCode.Single
                result = CSngExpression.Validate(Info, Me) AndAlso result
            Case TypeCode.String
                result = CStrExpression.Validate(Info, Me) AndAlso result
            Case TypeCode.UInt16
                result = CUShortExpression.Validate(Info, Me) AndAlso result
            Case TypeCode.UInt32
                result = CUIntExpression.Validate(Info, Me) AndAlso result
            Case TypeCode.UInt64
                result = CULngExpression.Validate(Info, Me) AndAlso result
            Case TypeCode.Object, TypeCode.DBNull
                If Helper.CompareType(Me.ExpressionType, Compiler.TypeCache.System_Object) Then
                    result = CObjExpression.Validate(Info, Expression) AndAlso result
                ElseIf Helper.CompareType(Me.ExpressionType, Compiler.TypeCache.System_Char_Array) AndAlso Helper.CompareType(Expression.ExpressionType, Compiler.TypeCache.System_String) Then
                    If Location.File(Compiler).IsOptionStrictOn Then
                        result = Compiler.Report.ShowMessage(Messages.VBNC30512, Location, Helper.ToString(Expression, Expression.ExpressionType), Helper.ToString(Expression, Me.ExpressionType))
                    Else
                        m_IsStringToCharArray = True
                    End If
                ElseIf Helper.CompareType(Me.Expression.ExpressionType, Compiler.TypeCache.Nothing) Then
                    'OK
                ElseIf Helper.CompareType(Me.Expression.ExpressionType, Compiler.TypeCache.System_Object) Then
                    'OK
                ElseIf Compiler.TypeResolver.IsImplicitlyConvertible(Me, Me.Expression.ExpressionType, Me.ExpressionType) Then
                    'OK
                ElseIf Helper.CompareType(Compiler.TypeCache.System_Array, Me.Expression.ExpressionType) AndAlso CecilHelper.IsArray(Me.ExpressionType) Then
                    'System.Array -> array type OK
                ElseIf CecilHelper.IsArray(Me.ExpressionType) AndAlso CecilHelper.IsArray(Me.Expression.ExpressionType) Then
                    'OKish
                ElseIf Location.File(Compiler).IsOptionStrictOn = False AndAlso CecilHelper.IsInterface(Me.Expression.ExpressionType) Then
                    'OKish
                Else
                    result = FindUserDefinedConversionOperator(Not IsExplicit) AndAlso result
                End If
            Case Else
                Throw New InternalException(Me)
        End Select

        Return result
    End Function

    Overrides ReadOnly Property ExpressionType() As Mono.Cecil.TypeReference
        Get
            Return m_ResolvedDestinationType
        End Get
    End Property

    Protected Overridable ReadOnly Property GetKeyword() As KS
        Get
            Return KS.CType
        End Get
    End Property
End Class

