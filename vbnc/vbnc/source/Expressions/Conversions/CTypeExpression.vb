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
    Private m_ResolvedDestinationType As Type
    Private m_IsStringToCharArray As Boolean

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        If m_DestinationType IsNot Nothing Then
            result = m_DestinationType.ResolveTypeReferences AndAlso result
            m_ResolvedDestinationType = m_DestinationType.ResolvedType
            Helper.Assert(m_ResolvedDestinationType.IsByRef = False)
        End If

        result = MyBase.ResolveTypeReferences() AndAlso result

        Return result
    End Function

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Expression As Expression, ByVal DestinationType As Type)
        MyBase.New(Parent, Expression)
        m_ResolvedDestinationType = DestinationType
        Helper.Assert(m_ResolvedDestinationType.IsByRef = False, "Can't create TypeConversion to byref type (trying to convert from " & Expression.ExpressionType.FullName & " to " & DestinationType.FullName)
    End Sub

    Shadows Sub Init(ByVal Expression As Expression, ByVal DestinationType As TypeName)
        MyBase.Init(Expression)
        m_DestinationType = DestinationType
    End Sub

    Shadows Sub Init(ByVal Expression As Expression, ByVal DestinationType As Type)
        MyBase.Init(Expression)
        m_ResolvedDestinationType = DestinationType
    End Sub


    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        Dim expType As Type = Me.ExpressionType
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

    Public Shared Function GenerateUserDefinedConversionCode(ByVal Info As EmitInfo, ByVal Expression As Expression, ByVal DestinationType As Type) As Boolean
        Dim result As Boolean = True
        Dim exptype As Type = Expression.ExpressionType
        Dim ops As Generic.List(Of MethodInfo)
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

    Private Function GenerateCTypeCode(ByVal Info As EmitInfo, ByVal DestinationType As Type, ByVal SourceType As Type) As Boolean
        Dim result As Boolean = True

        If m_IsStringToCharArray Then
            Return EmitStringToCharArray(Info) AndAlso result
        End If

        result = Expression.Classification.GenerateCode(Info.Clone(Me, DestinationType)) AndAlso result

        If Helper.CompareType(Compiler.TypeCache.Nothing, SourceType) Then
            'There is nothing to do here
        ElseIf SourceType.IsGenericParameter Then
            If DestinationType.IsGenericParameter Then
                Return Compiler.Report.ShowMessage(Messages.VBNC99997, Location)
            ElseIf DestinationType.IsArray Then
                Return Compiler.Report.ShowMessage(Messages.VBNC99997, Location)
            ElseIf DestinationType.IsClass Then
                DestinationType = Helper.GetTypeOrTypeBuilder(DestinationType)
                If Helper.IsTypeConvertibleToAny(Helper.GetGenericParameterConstraints(Me, SourceType), DestinationType) Then
                    'Emitter.EmitUnbox_Any(Info, DestinationType)
                    Emitter.EmitBox(Info, SourceType)
                    Emitter.EmitCastClass(Info, SourceType, DestinationType)
                Else
                    Helper.AddError(Me)
                End If
            ElseIf DestinationType.IsValueType Then
                Return Compiler.Report.ShowMessage(Messages.VBNC99997, Location)
            ElseIf DestinationType.IsInterface Then
                Emitter.EmitBox(Info, SourceType)
                Emitter.EmitCastClass(Info, SourceType, DestinationType)
            Else
                Throw New InternalException(Me)
            End If
        ElseIf SourceType.IsArray Then
            If DestinationType.IsInterface Then
                If Helper.DoesTypeImplementInterface(Me, SourceType, DestinationType) Then
                    Emitter.EmitCastClass(Info, SourceType, DestinationType)
                ElseIf Helper.CompareType(SourceType, Compiler.TypeCache.System_Object_Array) Then
                    Emitter.EmitCastClass(Info, SourceType, DestinationType)
                ElseIf DestinationType.IsArray AndAlso Helper.DoesTypeImplementInterface(Me, SourceType.GetElementType, DestinationType.GetElementType) Then
                    Emitter.EmitCastClass(Info, SourceType, DestinationType)
                Else
                    Info.Compiler.Report.ShowMessage(Messages.VBNC30311, SourceType.Name, DestinationType.Name)
                    result = False
                End If
            ElseIf Helper.CompareType(DestinationType, Compiler.TypeCache.System_Array) Then
                Emitter.EmitCastClass(Info, SourceType, DestinationType)
            ElseIf DestinationType.IsArray = False Then
                Info.Compiler.Report.ShowMessage(Messages.VBNC30311, SourceType.Name, DestinationType.Name)
                result = False
            ElseIf SourceType.GetArrayRank <> DestinationType.GetArrayRank Then
                Info.Compiler.Report.ShowMessage(Messages.VBNC30311, SourceType.Name, DestinationType.Name)
                result = False
            Else
                Dim SourceElementType As Type = SourceType.GetElementType
                Dim DestinationElementType As Type = DestinationType.GetElementType
                'For any two reference types A and B, if A is a derived type of B or implements B, 
                'a conversion exists from an array of type A to a compatible array of type B.
                'A compatible array is an array of the same rank and type. 
                'This relationship is known as array covariance. 
                'Array covariance in particular means that an element of an array whose element type is B 
                'may actually be an element of an array whose element type is A, 
                'provided that both A and B are reference types and that B is a base type of A or is implemented by A. 
                If Helper.CompareType(Compiler.TypeCache.System_Object, SourceElementType) Then
                    Emitter.EmitCastClass(Info, SourceType, DestinationType)
                ElseIf Helper.CompareType(SourceElementType, DestinationElementType) OrElse DestinationElementType.IsSubclassOf(SourceElementType) OrElse SourceElementType.IsSubclassOf(DestinationElementType) Then
                    Emitter.EmitCastClass(Info, SourceType, DestinationType)
                ElseIf Helper.DoesTypeImplementInterface(Me, SourceElementType, DestinationElementType) Then
                    Emitter.EmitCastClass(Info, SourceType, DestinationType)
                ElseIf DestinationElementType.IsInterface AndAlso Helper.CompareType(Compiler.TypeCache.System_Object, SourceElementType) Then
                    Emitter.EmitCastClass(Info, SourceType, DestinationType)
                ElseIf SourceElementType.IsEnum AndAlso Helper.CompareType(Helper.GetEnumType(Compiler, SourceElementType), DestinationElementType) Then
                    'Conversions also exist between an array of an enumerated type and an array of the enumerated type's underlying type of the same rank.
                    Emitter.EmitCastClass(Info, SourceType, DestinationType)
                ElseIf TypeOf SourceElementType Is TypeParameterDescriptor AndAlso Helper.IsTypeConvertibleToAny(Helper.GetGenericParameterConstraints(Me, SourceElementType), DestinationElementType) Then
                    Emitter.EmitCastClass(Info, SourceType, DestinationType)
                Else
                    Info.Compiler.Report.ShowMessage(Messages.VBNC30311, SourceType.Name, DestinationType.Name)
                    result = False
                End If

            End If
        ElseIf SourceType.IsClass Then
            If DestinationType.IsGenericParameter Then
                Dim method As MethodInfo
                method = Compiler.TypeCache.MS_VB_CS_Conversions__ToGenericParameter_Object.MakeGenericMethod(DestinationType.UnderlyingSystemType)

                Emitter.EmitCall(Info, Compiler.TypeCache.System_Runtime_CompilerServices_RuntimeHelpers__GetObjectValue_Object)
                Emitter.EmitCall(Info, method)
            ElseIf DestinationType.IsClass Then
                Emitter.EmitCastClass(Info, SourceType, DestinationType)
            ElseIf DestinationType.IsInterface Then
                Emitter.EmitCastClass(Info, SourceType, DestinationType)
            ElseIf DestinationType.IsValueType Then
                Emitter.EmitUnbox(Info, DestinationType)
                Emitter.EmitLdobj(Info, DestinationType)
            ElseIf DestinationType.IsEnum Then
                Throw New InternalException(Me) 'This is an elemental conversion already covered.
            ElseIf DestinationType.IsArray Then
                Throw New InternalException(Me) 'This is an IsClass case.
            Else
                Throw New InternalException(Me)
            End If
        ElseIf SourceType.IsValueType Then
            'A value type value can be converted to one of its base reference types or an interface type that it implements through a process called boxing
            If Helper.CompareType(DestinationType, Compiler.TypeCache.System_Object) Then
                Throw New InternalException(Me) 'This is an elemental conversion already covered. 'Emitter.EmitBox(Info)
            ElseIf Helper.DoesTypeImplementInterface(Me, SourceType, DestinationType) Then
                Emitter.EmitBox(Info, SourceType)
                Emitter.EmitCastClass(Info, Compiler.TypeCache.System_Object, DestinationType)
            ElseIf Helper.CompareType(SourceType.BaseType, DestinationType) Then
                Emitter.EmitBox(Info, DestinationType)
            Else
                Throw New InternalException("Operator CType is not defined for types '" & SourceType.FullName & "' and '" & DestinationType.FullName & "'")
            End If
        ElseIf SourceType.IsInterface Then
            If DestinationType.IsGenericParameter Then
                Dim method As MethodInfo
                method = Compiler.TypeCache.MS_VB_CS_Conversions__ToGenericParameter_Object.MakeGenericMethod(DestinationType.UnderlyingSystemType)

                Emitter.EmitCall(Info, Compiler.TypeCache.System_Runtime_CompilerServices_RuntimeHelpers__GetObjectValue_Object)
                Emitter.EmitCall(Info, method)
            ElseIf Helper.DoesTypeImplementInterface(Me, DestinationType, SourceType) Then
                If DestinationType.IsValueType Then
                    Emitter.EmitUnbox(Info, DestinationType)
                    Emitter.EmitLdobj(Info, DestinationType)
                Else
                    Emitter.EmitCastClass(Info, SourceType, DestinationType)
                End If

            ElseIf DestinationType.IsClass OrElse DestinationType.IsInterface Then
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

    Overrides ReadOnly Property ExpressionType() As Type
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

#If DEBUG Then
    Public Overrides Sub Dump(ByVal Dumper As IndentedTextWriter)
        Dumper.Write(Enums.GetKSStringAttribute(GetKeyword).FriendlyValue)
        Dumper.Write("(")
        Expression.Dump(Dumper)
        Dumper.Write(" ,")
        Compiler.Dumper.Dump(m_DestinationType)
        Dumper.Write(")")
    End Sub
#End If
End Class
