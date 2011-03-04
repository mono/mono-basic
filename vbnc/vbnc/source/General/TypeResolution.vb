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

#If DEBUG Then
#Const EXTENDEDDEBUG = 0
#End If

Public Class TypeResolution
    Inherits Helper

    Friend BuiltInTypes As New Generic.List(Of Mono.Cecil.TypeReference)(New Mono.Cecil.TypeReference() {Compiler.TypeCache.System_Boolean, Compiler.TypeCache.System_Byte, Compiler.TypeCache.System_Char, Compiler.TypeCache.System_DateTime, Compiler.TypeCache.System_Double, Compiler.TypeCache.System_Int32, Compiler.TypeCache.System_int64, Compiler.TypeCache.System_Object, Compiler.TypeCache.System_int16, Compiler.TypeCache.System_Single, Compiler.TypeCache.System_String, Compiler.TypeCache.System_SByte, Compiler.TypeCache.System_UInt16, Compiler.TypeCache.System_UInt32, Compiler.TypeCache.System_Uint64})
    Friend NumericTypes As New Generic.List(Of Mono.Cecil.TypeReference)(New Mono.Cecil.TypeReference() {Compiler.TypeCache.System_Byte, Compiler.TypeCache.System_SByte, Compiler.TypeCache.System_Decimal, Compiler.TypeCache.System_Double, Compiler.TypeCache.System_Single, Compiler.TypeCache.System_int16, Compiler.TypeCache.System_UInt16, Compiler.TypeCache.System_Int32, Compiler.TypeCache.System_UInt32, Compiler.TypeCache.System_int64, Compiler.TypeCache.System_Uint64})
    Friend IntegralTypes As New Generic.List(Of Mono.Cecil.TypeReference)(New Mono.Cecil.TypeReference() {Compiler.TypeCache.System_Byte, Compiler.TypeCache.System_SByte, Compiler.TypeCache.System_int16, Compiler.TypeCache.System_Uint16, Compiler.TypeCache.System_Int32, Compiler.TypeCache.System_UInt32, Compiler.TypeCache.System_int64, Compiler.TypeCache.System_Uint64})

    Private valCanBeContainBy(15)() As Mono.Cecil.TypeReference

    Public Shared Conversion As TypeConversionInfo(,)

    Private Shared m_ImplicitlyConvertedIntrinsicTypes As New Generic.Dictionary(Of Mono.Cecil.TypeReference, TypeCode())

    Shared Function GetIntrinsicTypesImplicitlyConvertibleFrom(ByVal Compiler As Compiler, ByVal Type As Mono.Cecil.TypeReference) As TypeCode()
        Dim result As TypeCode() = Nothing

        If m_ImplicitlyConvertedIntrinsicTypes.TryGetValue(Type, result) Then
            Return result
        End If

        If Helper.CompareType(Type, Compiler.TypeCache.System_Char_Array) Then
            result = New TypeCode() {TypeCode.String}
        End If

        If result IsNot Nothing Then
            m_ImplicitlyConvertedIntrinsicTypes.Add(Type, result)
        End If

        Return result
    End Function

    Shared Sub New()
        Dim highest As Integer

        Dim tmp As Array = System.Enum.GetValues(GetType(TypeCode))
        highest = CInt(tmp.GetValue(tmp.GetUpperBound(0)))

        ReDim Conversion(highest, highest)
        For i As Integer = 0 To highest
            For j As Integer = 0 To highest
                Conversion(i, j) = New TypeConversionInfo
                If j = TypeCode.Object OrElse j = i Then
                    Conversion(i, j).Conversion = ConversionType.Implicit
                ElseIf i = TypeCode.Char OrElse j = TypeCode.Char Then
                    Conversion(i, j).Conversion = ConversionType.None
                ElseIf i = TypeCode.DateTime OrElse j = TypeCode.DateTime Then
                    Conversion(i, j).Conversion = ConversionType.None
                ElseIf i = TypeCode.DBNull OrElse j = TypeCode.DBNull Then
                    Conversion(i, j).Conversion = ConversionType.None
                Else
                    Conversion(i, j).Conversion = ConversionType.Explicit
                End If
            Next
        Next

        setImplicit(TypeCode.SByte, New TypeCode() {TypeCode.Int16, TypeCode.Int32, TypeCode.Int64, TypeCode.Decimal, TypeCode.Single, TypeCode.Double})
        setImplicit(TypeCode.Byte, New TypeCode() {TypeCode.Int16, TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt64, TypeCode.Decimal, TypeCode.Single, TypeCode.Double})
        setImplicit(TypeCode.Int16, New TypeCode() {TypeCode.Int32, TypeCode.Int64, TypeCode.Decimal, TypeCode.Single, TypeCode.Double})
        setImplicit(TypeCode.UInt16, New TypeCode() {TypeCode.Int32, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt64, TypeCode.Decimal, TypeCode.Single, TypeCode.Double})
        setImplicit(TypeCode.Int32, New TypeCode() {TypeCode.Int64, TypeCode.Decimal, TypeCode.Single, TypeCode.Double})
        setImplicit(TypeCode.UInt32, New TypeCode() {TypeCode.Int64, TypeCode.UInt64, TypeCode.Decimal, TypeCode.Single, TypeCode.Double})
        setImplicit(TypeCode.Int64, New TypeCode() {TypeCode.Decimal, TypeCode.Single, TypeCode.Double})
        setImplicit(TypeCode.UInt64, New TypeCode() {TypeCode.Decimal, TypeCode.Single, TypeCode.Double})
        setImplicit(TypeCode.Decimal, New TypeCode() {TypeCode.Single, TypeCode.Double})
        setImplicit(TypeCode.Single, New TypeCode() {TypeCode.Double})
        setImplicit(TypeCode.Double, New TypeCode() {})
        setImplicit(TypeCode.Char, New TypeCode() {TypeCode.String})
        setImplicit(TypeCode.DBNull, New TypeCode() {TypeCode.String})

        Conversion(TypeCode.DateTime, TypeCode.String).Conversion = ConversionType.Explicit
        Conversion(TypeCode.Byte, TypeCode.Byte).BinaryAddResult = TypeCode.Byte
        Conversion(TypeCode.Boolean, TypeCode.Boolean).BinaryAddResult = TypeCode.SByte
    End Sub

    Sub New(ByVal Compiler As Compiler)
        MyBase.New(Compiler)

        valCanBeContainBy(getTypeIndex(BuiltInDataTypes.Boolean)) = Nothing
        valCanBeContainBy(getTypeIndex(BuiltInDataTypes.Byte)) = New Mono.Cecil.TypeReference() {Compiler.TypeCache.System_Byte, Compiler.TypeCache.System_Int16, Compiler.TypeCache.System_UInt16, Compiler.TypeCache.System_Int32, Compiler.TypeCache.System_UInt32, Compiler.TypeCache.System_Int64, Compiler.TypeCache.System_UInt64, Compiler.TypeCache.System_Decimal, Compiler.TypeCache.System_Double, Compiler.TypeCache.System_Single}
        valCanBeContainBy(getTypeIndex(BuiltInDataTypes.Char)) = Nothing
        valCanBeContainBy(getTypeIndex(BuiltInDataTypes.Date)) = Nothing
        valCanBeContainBy(getTypeIndex(BuiltInDataTypes.Decimal)) = New Mono.Cecil.TypeReference() {Compiler.TypeCache.System_Decimal, Compiler.TypeCache.System_Double, Compiler.TypeCache.System_Single}
        valCanBeContainBy(getTypeIndex(BuiltInDataTypes.Double)) = New Mono.Cecil.TypeReference() {Compiler.TypeCache.System_Double}
        valCanBeContainBy(getTypeIndex(BuiltInDataTypes.Integer)) = New Mono.Cecil.TypeReference() {Compiler.TypeCache.System_Int32, Compiler.TypeCache.System_Int64, Compiler.TypeCache.System_Decimal, Compiler.TypeCache.System_Double, Compiler.TypeCache.System_Single}
        valCanBeContainBy(getTypeIndex(BuiltInDataTypes.Long)) = New Mono.Cecil.TypeReference() {Compiler.TypeCache.System_Int64, Compiler.TypeCache.System_Decimal, Compiler.TypeCache.System_Double, Compiler.TypeCache.System_Single}
        valCanBeContainBy(getTypeIndex(BuiltInDataTypes.Object)) = Nothing
        valCanBeContainBy(getTypeIndex(BuiltInDataTypes.[SByte])) = New Mono.Cecil.TypeReference() {Compiler.TypeCache.System_SByte, Compiler.TypeCache.System_Int16, Compiler.TypeCache.System_Int32, Compiler.TypeCache.System_Int64, Compiler.TypeCache.System_Decimal, Compiler.TypeCache.System_Double, Compiler.TypeCache.System_Single}
        valCanBeContainBy(getTypeIndex(BuiltInDataTypes.Short)) = New Mono.Cecil.TypeReference() {Compiler.TypeCache.System_Int16, Compiler.TypeCache.System_Int32, Compiler.TypeCache.System_Int64, Compiler.TypeCache.System_Decimal, Compiler.TypeCache.System_Double, Compiler.TypeCache.System_Single}
        valCanBeContainBy(getTypeIndex(BuiltInDataTypes.Single)) = New Mono.Cecil.TypeReference() {Compiler.TypeCache.System_Double, Compiler.TypeCache.System_Single}
        valCanBeContainBy(getTypeIndex(BuiltInDataTypes.String)) = Nothing
        valCanBeContainBy(getTypeIndex(BuiltInDataTypes.[UInteger])) = New Mono.Cecil.TypeReference() {Compiler.TypeCache.System_UInt32, Compiler.TypeCache.System_Int64, Compiler.TypeCache.System_UInt64, Compiler.TypeCache.System_Decimal, Compiler.TypeCache.System_Double, Compiler.TypeCache.System_Single}
        valCanBeContainBy(getTypeIndex(BuiltInDataTypes.[ULong])) = New Mono.Cecil.TypeReference() {Compiler.TypeCache.System_UInt64, Compiler.TypeCache.System_Decimal, Compiler.TypeCache.System_Double, Compiler.TypeCache.System_Single}
        valCanBeContainBy(getTypeIndex(BuiltInDataTypes.[UShort])) = New Mono.Cecil.TypeReference() {Compiler.TypeCache.System_UInt16, Compiler.TypeCache.System_Int32, Compiler.TypeCache.System_UInt32, Compiler.TypeCache.System_Int64, Compiler.TypeCache.System_UInt64, Compiler.TypeCache.System_Decimal, Compiler.TypeCache.System_Double, Compiler.TypeCache.System_Single}

    End Sub

    ''' <summary>
    ''' Returns the type of the builtin type.
    ''' If it isn't a builtin type, then it returns nothing,
    ''' </summary>
    ''' <param name="tp"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function BuiltInTypeToType(ByVal tp As BuiltInDataTypes) As Mono.Cecil.TypeReference
        Return KeywordToType(CType(tp, KS))
    End Function

    Function IsBuiltInType(ByVal Type As Mono.Cecil.TypeReference) As Boolean
        Return BuiltInTypes.Contains(Type)
    End Function

    Shared Function TypeCodeToBuiltInType(ByVal tp As TypeCode) As BuiltInDataTypes
        Select Case tp
            Case TypeCode.Boolean
                Return BuiltInDataTypes.Boolean
            Case TypeCode.Byte
                Return BuiltInDataTypes.Byte
            Case TypeCode.Char
                Return BuiltInDataTypes.Char
            Case TypeCode.DateTime
                Return BuiltInDataTypes.Date
            Case TypeCode.DBNull
                Throw New InternalException("")
            Case TypeCode.Decimal
                Return BuiltInDataTypes.Decimal
            Case TypeCode.Double
                Return BuiltInDataTypes.Double
            Case TypeCode.Empty
                Throw New InternalException("")
            Case TypeCode.Int16
                Return BuiltInDataTypes.Short
            Case TypeCode.Int32
                Return BuiltInDataTypes.Integer
            Case TypeCode.Int64
                Return BuiltInDataTypes.Long
            Case TypeCode.Object
                Return BuiltInDataTypes.Object
            Case TypeCode.SByte
                Return BuiltInDataTypes.SByte
            Case TypeCode.Single
                Return BuiltInDataTypes.Single
            Case TypeCode.String
                Return BuiltInDataTypes.String
            Case TypeCode.UInt16
                Return BuiltInDataTypes.UShort
            Case TypeCode.UInt32
                Return BuiltInDataTypes.UInteger
            Case TypeCode.UInt64
                Return BuiltInDataTypes.ULong
            Case Else
                Throw New InternalException("")
        End Select
    End Function

    Shared Function BuiltInTypeToTypeCode(ByVal Type As BuiltInDataTypes) As TypeCode
        Select Case Type
            Case BuiltInDataTypes.Boolean
                Return TypeCode.Boolean
            Case BuiltInDataTypes.Byte
                Return TypeCode.Byte
            Case BuiltInDataTypes.Char
                Return TypeCode.Char
            Case BuiltInDataTypes.Date
                Return TypeCode.DateTime
            Case BuiltInDataTypes.Decimal
                Return TypeCode.Decimal
            Case BuiltInDataTypes.Double
                Return TypeCode.Double
            Case BuiltInDataTypes.Integer
                Return TypeCode.Int32
            Case BuiltInDataTypes.Long
                Return TypeCode.Int64
            Case BuiltInDataTypes.Object
                Return TypeCode.Object
            Case BuiltInDataTypes.SByte
                Return TypeCode.SByte
            Case BuiltInDataTypes.Short
                Return TypeCode.Int16
            Case BuiltInDataTypes.Single
                Return TypeCode.Single
            Case BuiltInDataTypes.String
                Return TypeCode.String
            Case BuiltInDataTypes.UInteger
                Return TypeCode.UInt32
            Case BuiltInDataTypes.ULong
                Return TypeCode.UInt64
            Case BuiltInDataTypes.UShort
                Return TypeCode.UInt16
            Case Else
                Throw New InternalException("")
        End Select
    End Function

    Function TypeCodeToTypeDescriptor(ByVal Code As TypeCode) As Mono.Cecil.TypeReference
        Return TypeCodeToType(Code)
    End Function

    Function TypeCodeToType(ByVal Code As TypeCode) As Mono.Cecil.TypeReference
        Select Case Code
            Case TypeCode.Boolean
                Return Compiler.TypeCache.System_Boolean
            Case TypeCode.Byte
                Return Compiler.TypeCache.System_Byte
            Case TypeCode.Char
                Return Compiler.TypeCache.System_Char
            Case TypeCode.DateTime
                Return Compiler.TypeCache.System_DateTime
            Case TypeCode.DBNull
                Throw New InternalException("")
            Case TypeCode.Decimal
                Return Compiler.TypeCache.System_Decimal
            Case TypeCode.Double
                Return Compiler.TypeCache.System_Double
            Case TypeCode.Empty
                Throw New InternalException("")
            Case TypeCode.Int16
                Return Compiler.TypeCache.System_Int16
            Case TypeCode.Int32
                Return Compiler.TypeCache.System_Int32
            Case TypeCode.Int64
                Return Compiler.TypeCache.System_Int64
            Case TypeCode.Object
                Return Compiler.TypeCache.System_Object
            Case TypeCode.SByte
                Return Compiler.TypeCache.System_SByte
            Case TypeCode.Single
                Return Compiler.TypeCache.System_Single
            Case TypeCode.String
                Return Compiler.TypeCache.System_String
            Case TypeCode.UInt16
                Return Compiler.TypeCache.System_UInt16
            Case TypeCode.UInt32
                Return Compiler.TypeCache.System_UInt32
            Case TypeCode.UInt64
                Return Compiler.TypeCache.System_UInt64
            Case Else
                Throw New InternalException("")
        End Select
    End Function

    Private Shared Function getTypeIndex(ByVal special As BuiltInDataTypes) As Integer
        Select Case special
            Case BuiltInDataTypes.Boolean
                Return 0
            Case BuiltInDataTypes.Byte
                Return 1
            Case BuiltInDataTypes.Char
                Return 2
            Case BuiltInDataTypes.Date
                Return 3
            Case BuiltInDataTypes.Decimal
                Return 4
            Case BuiltInDataTypes.Double
                Return 5
            Case BuiltInDataTypes.Integer
                Return 6
            Case BuiltInDataTypes.Long
                Return 7
            Case BuiltInDataTypes.Object
                Return 8
            Case BuiltInDataTypes.[SByte]
                Return 9
            Case BuiltInDataTypes.Short
                Return 10
            Case BuiltInDataTypes.Single
                Return 11
            Case BuiltInDataTypes.String
                Return 12
            Case BuiltInDataTypes.[UInteger]
                Return 13
            Case BuiltInDataTypes.[ULong]
                Return 14
            Case BuiltInDataTypes.[UShort]
                Return 15
            Case Else
                Throw New InternalException("")
        End Select
    End Function

    Shared Function KeywordToTypeCode(ByVal Keyword As KS) As TypeCode
        Select Case Keyword
            Case KS.Boolean
                Return TypeCode.Boolean
            Case KS.Byte
                Return TypeCode.Byte
            Case KS.Char
                Return TypeCode.Char
            Case KS.Date
                Return TypeCode.DateTime
            Case KS.Decimal
                Return TypeCode.Decimal
            Case KS.Double
                Return TypeCode.Double
            Case KS.Integer
                Return TypeCode.Int32
            Case KS.Long
                Return TypeCode.Int64
            Case KS.Object
                Return TypeCode.Object
            Case KS.Single
                Return TypeCode.Single
            Case KS.Short
                Return TypeCode.Int16
            Case KS.String
                Return TypeCode.String
            Case KS.SByte
                Return TypeCode.SByte
            Case KS.UShort
                Return TypeCode.UInt16
            Case KS.UInteger
                Return TypeCode.UInt32
            Case KS.ULong
                Return TypeCode.UInt64
            Case Else
                Throw New InternalException("")
        End Select
    End Function

    ''' <summary>
    ''' Returns the type of the specified keyword. Throws an internalexception if the keyword isn't a type.
    ''' </summary>
    ''' <param name="Keyword"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 
    Function KeywordToType(ByVal Keyword As KS) As Mono.Cecil.TypeReference
        Select Case Keyword
            Case KS.Boolean
                Return Compiler.TypeCache.System_Boolean
            Case KS.Byte
                Return Compiler.TypeCache.System_Byte
            Case KS.Char
                Return Compiler.TypeCache.System_Char
            Case KS.Date
                Return Compiler.TypeCache.System_DateTime
            Case KS.Decimal
                Return Compiler.TypeCache.System_Decimal
            Case KS.Double
                Return Compiler.TypeCache.System_Double
            Case KS.Integer
                Return Compiler.TypeCache.System_Int32
            Case KS.Long
                Return Compiler.TypeCache.System_Int64
            Case KS.Object
                Return Compiler.TypeCache.System_Object
            Case KS.Single
                Return Compiler.TypeCache.System_Single
            Case KS.Short
                Return Compiler.TypeCache.System_Int16
            Case KS.String
                Return Compiler.TypeCache.System_String
            Case KS.[SByte]
                Return Compiler.TypeCache.System_SByte
            Case KS.[UShort]
                Return Compiler.TypeCache.System_UInt16
            Case KS.[UInteger]
                Return Compiler.TypeCache.System_UInt32
            Case KS.[ULong]
                Return Compiler.TypeCache.System_UInt64
            Case Else
                'Throw New InternalException("Don't know if this can actually happen, though. KS = " & Keyword.ToString)
                Return Nothing
        End Select
    End Function

    Function TypeToKeyword(ByVal Type As Mono.Cecil.TypeReference) As KS
        If Helper.CompareType(Type, Compiler.TypeCache.System_Boolean) Then
            Return KS.Boolean
        ElseIf Helper.CompareType(Type, Compiler.TypeCache.System_Byte) Then
            Return KS.Byte
        ElseIf Helper.CompareType(Type, Compiler.TypeCache.System_Char) Then
            Return KS.Char
        ElseIf Helper.CompareType(Type, Compiler.TypeCache.System_DateTime) Then
            Return KS.Date
        ElseIf Helper.CompareType(Type, Compiler.TypeCache.System_Decimal) Then
            Return KS.Decimal
        ElseIf Helper.CompareType(Type, Compiler.TypeCache.System_Double) Then
            Return KS.Double
        ElseIf Helper.CompareType(Type, Compiler.TypeCache.System_Int32) Then
            Return KS.Integer
        ElseIf Helper.CompareType(Type, Compiler.TypeCache.System_Int64) Then
            Return KS.Long
        ElseIf Helper.CompareType(Type, Compiler.TypeCache.System_Object) Then
            Return KS.Object
        ElseIf Helper.CompareType(Type, Compiler.TypeCache.System_Int16) Then
            Return KS.Short
        ElseIf Helper.CompareType(Type, Compiler.TypeCache.System_Single) Then
            Return KS.Single
        ElseIf Helper.CompareType(Type, Compiler.TypeCache.System_String) Then
            Return KS.String
        ElseIf Helper.CompareType(Type, Compiler.TypeCache.System_SByte) Then
            Return KS.[SByte]
        ElseIf Helper.CompareType(Type, Compiler.TypeCache.System_UInt16) Then
            Return KS.[UShort]
        ElseIf Helper.CompareType(Type, Compiler.TypeCache.System_UInt32) Then
            Return KS.[UInteger]
        ElseIf Helper.CompareType(Type, Compiler.TypeCache.System_UInt64) Then
            Return KS.[ULong]
        Else
            Return KS.None
        End If
    End Function

    Function IsImplicitlyConvertible(ByVal Context As BaseObject, ByVal FromType As Mono.Cecil.TypeReference, ByVal ToType As Mono.Cecil.TypeReference) As Boolean
        Dim Compiler As Compiler = Context.Compiler
        Dim tpFrom, tpTo As TypeCode
        If Helper.CompareType(Compiler.TypeCache.Nothing, FromType) Then Return True
        If CecilHelper.IsByRef(FromType) Then FromType = CecilHelper.GetElementType(FromType)
        If CecilHelper.IsByRef(ToType) Then ToType = CecilHelper.GetElementType(ToType)
        If CecilHelper.IsNullable(FromType) Then FromType = CecilHelper.GetNulledType(FromType)
        If CecilHelper.IsNullable(ToType) Then ToType = CecilHelper.GetNulledType(ToType)
        tpFrom = Helper.GetTypeCode(Compiler, FromType)
        tpTo = Helper.GetTypeCode(Compiler, ToType)
        If tpTo = TypeCode.Object Then
            Return Helper.IsAssignable(Context, FromType, ToType) ' ToType.IsAssignableFrom(FromType)
        ElseIf Helper.IsEnum(Compiler, ToType) AndAlso Helper.IsEnum(Compiler, FromType) = False Then
            Return False
        ElseIf Helper.IsEnum(Compiler, ToType) AndAlso Helper.IsEnum(Compiler, FromType) Then
            Return Helper.CompareType(ToType, FromType)
        Else
            Return IsImplicitlyConvertible(Compiler, tpFrom, tpTo)
        End If
    End Function

    Function IsImplicitlyConvertible(ByVal Compiler As Compiler, ByVal tpFrom As TypeCode, ByVal tpTo As TypeCode) As Boolean
        Dim result As Boolean
        result = Conversion(tpFrom, tpTo).Conversion = ConversionType.Implicit
        Return result
    End Function

    ''' <summary>
    ''' If explicitly or implicitly convertible.
    ''' </summary>
    ''' <param name="Compiler"></param>
    ''' <param name="tpFrom"></param>
    ''' <param name="tpTo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function IsExplicitlyConvertible(ByVal Compiler As Compiler, ByVal tpFrom As TypeCode, ByVal tpTo As TypeCode) As Boolean
        Dim result As Boolean
        Dim ct As ConversionType

        ct = Conversion(tpFrom, tpTo).Conversion
        result = ct = ConversionType.Implicit OrElse ct = ConversionType.Explicit

        Return result
    End Function

    Function IsNumericType(ByVal Type As Mono.Cecil.TypeReference) As Boolean
        For Each t As Mono.Cecil.TypeReference In NumericTypes
            If Helper.CompareType(t, Type) Then Return True
        Next
        Return False
    End Function

    Function IsIntegralType(ByVal Type As Mono.Cecil.TypeReference) As Boolean
        For Each t As Mono.Cecil.TypeReference In IntegralTypes
            If Helper.CompareType(t, Type) Then Return True
        Next
        Return False
    End Function

    Function IsIntegralType(ByVal Type As TypeCode) As Boolean
        For Each t As Mono.Cecil.TypeReference In IntegralTypes
            If Helper.GetTypeCode(Compiler, t) = Type Then Return True
        Next
        Return False
    End Function

    Function IsIntegralType(ByVal Type As BuiltInDataTypes) As Boolean
        Return IsIntegralType(BuiltInTypeToTypeCode(Type))
    End Function

    Function IsSignedIntegralType(ByVal Type As Mono.Cecil.TypeReference) As Boolean
        Return Helper.CompareType(Type, Compiler.TypeCache.System_SByte) OrElse _
         Helper.CompareType(Type, Compiler.TypeCache.System_Int16) OrElse _
         Helper.CompareType(Type, Compiler.TypeCache.System_Int32) OrElse _
         Helper.CompareType(Type, Compiler.TypeCache.System_Int64)
    End Function

    Function IsUnsignedIntegralType(ByVal Type As Mono.Cecil.TypeReference) As Boolean
        Return Helper.CompareType(Type, Compiler.TypeCache.System_Byte) OrElse _
         Helper.CompareType(Type, Compiler.TypeCache.System_UInt16) OrElse _
         Helper.CompareType(Type, Compiler.TypeCache.System_UInt32) OrElse _
         Helper.CompareType(Type, Compiler.TypeCache.System_UInt64)
    End Function

    ''' <summary>
    ''' If the type is an enum type returns the base (integral type),
    ''' otherwise returns the same type.
    ''' </summary>
    ''' <param name="tp"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetIntegralType(ByVal Compiler As Compiler, ByVal tp As Mono.Cecil.TypeReference) As Mono.Cecil.TypeReference
        Helper.Assert(tp IsNot Nothing, "tp Is Nothing")
        If Helper.IsEnum(Compiler, tp) Then
            Dim field As Mono.Cecil.FieldDefinition
            field = CecilHelper.FindField(CecilHelper.FindDefinition(tp).Fields, EnumDeclaration.EnumTypeMemberName)
            Helper.Assert(field IsNot Nothing, "field '" & EnumDeclaration.EnumTypeMemberName & "' Is Nothing of Type '" & tp.FullName & "'")
            Return field.FieldType
        Else
            Helper.Assert(IsIntegralType(tp))
            Return tp
        End If
    End Function

    ''' <summary>
    ''' Finds the smallest type that can hold both specified types.
    ''' If tp1 = Integer and tp2 = Long would return Long
    ''' </summary>
    ''' <param name="tp1"></param>
    ''' <param name="tp2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSmallestIntegralType(ByVal tp1 As Mono.Cecil.TypeReference, ByVal tp2 As Mono.Cecil.TypeReference) As Mono.Cecil.TypeReference
        Dim cont1(), cont2() As Mono.Cecil.TypeReference

        Helper.Assert(tp1 IsNot Nothing, "tp1 Is Nothing")
        Helper.Assert(tp2 IsNot Nothing, "tp2 Is Nothing")

        Dim itp1, itp2 As Mono.Cecil.TypeReference
        itp1 = GetIntegralType(Compiler, tp1)
        itp2 = GetIntegralType(Compiler, tp2)
        cont1 = valCanBeContainBy(getTypeIndex(CType(TypeToKeyword(itp1), BuiltInDataTypes)))
        cont2 = valCanBeContainBy(getTypeIndex(CType(TypeToKeyword(itp2), BuiltInDataTypes)))

        If cont1 Is Nothing Or cont2 Is Nothing Then Return Nothing

        Dim found As New ArrayList
        For Each t1 As Mono.Cecil.TypeReference In cont1
            For Each t2 As Mono.Cecil.TypeReference In cont2
                If Helper.CompareType(t1, t2) Then
                    found.Add(t1)
                End If
            Next
        Next

        If found.Count = 0 Then
            Return Nothing
        Else
            Return CType(found(0), Mono.Cecil.TypeReference)
        End If
    End Function

    Public Function GetWidestType(ByVal tp1 As TypeReference, ByVal tp2 As TypeReference, ByVal tp3 As TypeReference) As TypeReference
        Dim cont1(), cont2(), cont3() As Mono.Cecil.TypeReference

        Helper.Assert(tp1 IsNot Nothing, "tp1 Is Nothing")
        Helper.Assert(tp2 IsNot Nothing, "tp2 Is Nothing")

        If tp1 Is tp2 Then
            If tp3 Is Nothing Then Return tp1
            If tp1 Is tp3 Then Return tp2
        End If

        Dim itp1, itp2, itp3 As Mono.Cecil.TypeReference
        itp1 = GetIntegralType(Compiler, tp1)
        itp2 = GetIntegralType(Compiler, tp2)

        cont1 = valCanBeContainBy(getTypeIndex(CType(TypeToKeyword(itp1), BuiltInDataTypes)))
        cont2 = valCanBeContainBy(getTypeIndex(CType(TypeToKeyword(itp2), BuiltInDataTypes)))

        If tp3 Is Nothing Then
            itp3 = Nothing
            cont3 = Nothing
        Else
            itp3 = GetIntegralType(Compiler, tp3)
            cont3 = valCanBeContainBy(getTypeIndex(CType(TypeToKeyword(itp3), BuiltInDataTypes)))
        End If

        If cont1 Is Nothing Or cont2 Is Nothing Then Return Nothing

        For i As Integer = 0 To cont1.Length - 1
            For j As Integer = 0 To cont2.Length - 1
                If Not cont2(j) Is cont1(i) Then Continue For

                If itp3 Is Nothing Then
                    'We've found a type that can contain both input types
                    If cont2(j) Is itp1 Then Return tp1
                    If cont2(j) Is itp2 Then Return tp2
                    'Continue looking, the type we want is neither of the two input types
                Else
                    For k As Integer = 0 To cont3.Length - 1
                        If Not cont3(k) Is cont2(j) Then Continue For
                        'We've found a type that can contain all three input types
                        If cont3(k) Is itp1 Then Return tp1
                        If cont3(k) Is itp2 Then Return tp2
                        If cont3(k) Is itp3 Then Return tp3
                        'Continue looking, the type we want is neither of the three input types
                    Next
                End If
            Next
        Next

        Return Nothing
    End Function

    Private Shared Sub setImplicit(ByVal type As TypeCode, ByVal implicit() As TypeCode)
        For i As Integer = 0 To VB.UBound(implicit)
            Conversion(type, implicit(i)).Conversion = ConversionType.Implicit
        Next
    End Sub

    Private Shared Sub setNone(ByVal type As TypeCode, ByVal explicit() As TypeCode)
        For i As Integer = 0 To explicit.Length - 1
            Conversion(type, explicit(i)).Conversion = ConversionType.None
        Next
    End Sub

    ''' <summary>
    ''' Tries to convert the value into the desired type. Returns true if successful, 
    ''' returns false otherwise. 
    ''' </summary>
    ''' <param name="value"></param>
    ''' <param name="result"></param>
    ''' <param name="desiredType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckNumericRange(ByVal value As Object, ByRef result As Object, ByVal desiredType As Mono.Cecil.TypeReference) As Boolean
        Dim builtInType As BuiltInDataTypes = TypeResolution.TypeCodeToBuiltInType(Helper.GetTypeCode(Compiler, desiredType))

        If value Is Nothing Then 'Nothing can be converted into anything.
            result = Nothing
            Return True
        End If

        If IsNumericType(desiredType) = False Then Return False

        If IsIntegralType(builtInType) AndAlso IsIntegralType(Helper.GetTypeCode(Compiler, CecilHelper.GetType(Compiler, value))) Then
            Return CheckIntegralRange(value, result, builtInType)
        Else
            Dim tpValue As TypeCode = Helper.GetTypeCode(Compiler, CecilHelper.GetType(Compiler, value))
            Dim desiredCode As TypeCode = Helper.GetTypeCode(Compiler, desiredType)

            If Helper.CompareType(CecilHelper.GetType(Compiler, value), desiredType) Then
                result = value
                Return True
            End If

            If tpValue = TypeCode.DBNull Then
                Select Case desiredCode
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
                    Case TypeCode.String
                        result = Nothing
                    Case TypeCode.UInt16
                        result = CUShort(Nothing)
                    Case TypeCode.UInt32
                        result = CUInt(Nothing)
                    Case TypeCode.UInt64
                        result = CULng(Nothing)
                    Case Else
                        Helper.Stop()
                        Throw New InternalException("")
                End Select
                Return True
            End If

            If IsNumericType(CecilHelper.GetType(Compiler, value)) = False Then Return False

            Select Case desiredCode
                Case TypeCode.Double
                    Select Case tpValue
                        Case TypeCode.Byte, TypeCode.SByte, TypeCode.Int16, TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt64, TypeCode.Single, TypeCode.Double, TypeCode.Decimal
                            result = CDbl(value)
                            Return True
                    End Select
                Case TypeCode.Decimal
                    Select Case tpValue
                        Case TypeCode.Byte, TypeCode.SByte, TypeCode.Int16, TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt64, TypeCode.Decimal
                            result = CDec(value)
                            Return True
                        Case TypeCode.Single, TypeCode.Double
                            Dim tmp As Double = CDbl(value)
                            If tmp <= Decimal.MaxValue AndAlso tmp >= Decimal.MinValue Then
                                result = CDec(tmp) 'This should be CDec(value), but vbc.exe seems to do it like this.
                                Return True
                            Else
                                Return False
                            End If
                    End Select
                Case TypeCode.Single
                    Select Case tpValue
                        Case TypeCode.Byte, TypeCode.SByte, TypeCode.Int16, TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt64, TypeCode.Single, TypeCode.Decimal
                            result = CSng(value)
                            Return True
                        Case TypeCode.Double
                            If CDbl(value) >= Single.MinValue AndAlso CDbl(value) <= Single.MaxValue Then
                                result = CSng(value)
                                Return True
                            Else
                                Return False
                            End If
                    End Select
                Case TypeCode.Byte
                    Select Case tpValue
                        Case TypeCode.Single, TypeCode.Double
                            Dim tmp As Double = CDbl(value)
                            If tmp <= Byte.MaxValue AndAlso tmp >= Byte.MinValue Then
                                result = CByte(value)
                                Return True
                            Else
                                Return False
                            End If
                        Case TypeCode.Decimal
                            Dim tmp As Decimal = CDec(value)
                            If tmp <= Byte.MaxValue AndAlso tmp >= Byte.MinValue Then
                                result = CByte(value)
                                Return True
                            Else
                                Return False
                            End If
                    End Select
                Case TypeCode.SByte
                    Select Case tpValue
                        Case TypeCode.Single, TypeCode.Double
                            Dim tmp As Double = CDbl(value)
                            If tmp <= SByte.MaxValue AndAlso tmp >= SByte.MinValue Then
                                result = CSByte(value)
                                Return True
                            Else
                                Return False
                            End If
                        Case TypeCode.Decimal
                            Dim tmp As Decimal = CDec(value)
                            If tmp <= SByte.MaxValue AndAlso tmp >= SByte.MinValue Then
                                result = CSByte(value)
                                Return True
                            Else
                                Return False
                            End If
                    End Select
                Case TypeCode.Int16
                    Select Case tpValue
                        Case TypeCode.Single, TypeCode.Double
                            Dim tmp As Double = CDbl(value)
                            If tmp <= Int16.MaxValue AndAlso tmp >= Int16.MinValue Then
                                result = CShort(value)
                                Return True
                            Else
                                Return False
                            End If
                        Case TypeCode.Decimal
                            Dim tmp As Decimal = CDec(value)
                            If tmp <= Int16.MaxValue AndAlso tmp >= Int16.MinValue Then
                                result = CShort(value)
                                Return True
                            Else
                                Return False
                            End If
                    End Select
                Case TypeCode.UInt16
                    Select Case tpValue
                        Case TypeCode.Single, TypeCode.Double
                            Dim tmp As Double = CDbl(value)
                            If tmp <= UInt16.MaxValue AndAlso tmp >= UInt16.MinValue Then
                                result = CUShort(value)
                                Return True
                            Else
                                Return False
                            End If
                        Case TypeCode.Decimal
                            Dim tmp As Decimal = CDec(value)
                            If tmp <= UInt16.MaxValue AndAlso tmp >= UInt16.MinValue Then
                                result = CUShort(value)
                                Return True
                            Else
                                Return False
                            End If
                    End Select
                Case TypeCode.Int32
                    Select Case tpValue
                        Case TypeCode.Byte, TypeCode.SByte, TypeCode.Int16, TypeCode.UInt16, TypeCode.Int32, TypeCode.Boolean
                            result = CInt(value)
                            Return True
                        Case TypeCode.Single, TypeCode.Double
                            Dim tmp As Double = CDbl(value)
                            If tmp <= Int32.MaxValue AndAlso tmp >= Int32.MinValue Then
                                result = CInt(value)
                                Return True
                            Else
                                Return False
                            End If
                        Case TypeCode.Decimal
                            Dim tmp As Decimal = CDec(value)
                            If tmp <= Int32.MaxValue AndAlso tmp >= Int32.MinValue Then
                                result = CInt(value)
                                Return True
                            Else
                                Return False
                            End If
                    End Select
                Case TypeCode.UInt32
                    Select Case tpValue
                        Case TypeCode.Single, TypeCode.Double
                            Dim tmp As Double = CDbl(value)
                            If tmp <= UInt32.MaxValue AndAlso tmp >= UInt32.MinValue Then
                                result = CUInt(value)
                                Return True
                            Else
                                Return False
                            End If
                        Case TypeCode.Decimal
                            Dim tmp As Decimal = CDec(value)
                            If tmp <= UInt32.MaxValue AndAlso tmp >= UInt32.MinValue Then
                                result = CUInt(value)
                                Return True
                            Else
                                Return False
                            End If
                    End Select
                Case TypeCode.Int64
                    Select Case tpValue
                        Case TypeCode.Byte, TypeCode.SByte, TypeCode.Int16, TypeCode.UInt16, TypeCode.Int32, TypeCode.Boolean
                            result = CInt(value)
                            Return True
                        Case TypeCode.Single, TypeCode.Double
                            Dim tmp As Double = CDbl(value)
                            If tmp <= Int64.MaxValue AndAlso tmp >= Int64.MinValue Then
                                result = CLng(value)
                                Return True
                            Else
                                Return False
                            End If
                        Case TypeCode.Decimal
                            Dim tmp As Decimal = CDec(value)
                            If tmp <= Int64.MaxValue AndAlso tmp >= Int64.MinValue Then
                                result = CLng(value)
                                Return True
                            Else
                                Return False
                            End If
                    End Select
                Case TypeCode.UInt64
                    Select Case tpValue
                        Case TypeCode.Single, TypeCode.Double
                            Dim tmp As Double = CDbl(value)
                            If tmp <= UInt64.MaxValue AndAlso tmp >= UInt64.MinValue Then
                                result = CULng(value)
                                Return True
                            Else
                                Return False
                            End If
                        Case TypeCode.Decimal
                            Dim tmp As Decimal = CDec(value)
                            If tmp <= UInt64.MaxValue AndAlso tmp >= UInt64.MinValue Then
                                result = CULng(value)
                                Return True
                            Else
                                Return False
                            End If
                    End Select
            End Select

            Select Case tpValue
                Case TypeCode.Byte, TypeCode.UInt16, TypeCode.UInt32, TypeCode.UInt64
                    Dim tmpValue As ULong = CULng(value)
                    Helper.Stop()
                Case TypeCode.SByte, TypeCode.Int16, TypeCode.Int32, TypeCode.Int64
                    Dim tmpValue As Long = CLng(value)
                    'Dim t As Type
                    Helper.Stop()
                Case TypeCode.Char
                    Helper.Stop()
                Case TypeCode.Boolean
                    Helper.Stop()
                Case TypeCode.DateTime
                    Helper.Stop()
                Case TypeCode.Decimal
                    Helper.Stop()
                Case TypeCode.Double
                    Helper.Stop()
                Case TypeCode.Single
                    Helper.Stop()
                Case TypeCode.String
                    Helper.Stop()
                Case Else
                    Helper.Stop()
            End Select
            Helper.Stop()
        End If

        Return False
    End Function

    ''' <summary>
    ''' Tries to convert the value into the desired type. Returns true if successful, 
    ''' returns false otherwise. Can only convert if value is already an integral type 
    ''' (does only do range-checking, not type conversion)
    ''' </summary>
    ''' <param name="value"></param>
    ''' <param name="result"></param>
    ''' <param name="desiredType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckIntegralRange(ByVal value As Object, ByRef result As Object, ByVal desiredType As BuiltInDataTypes) As Boolean
        Helper.Assert(value IsNot Nothing)
        Helper.Assert(IsIntegralType(desiredType))
        Dim tpValue As TypeCode = Helper.GetTypeCode(Compiler, CecilHelper.GetType(Compiler, value))
        Helper.Assert(IsIntegralType(tpValue))
        Select Case tpValue
            Case TypeCode.Byte, TypeCode.UInt16, TypeCode.UInt32, TypeCode.UInt64
                Dim tmpValue As ULong = CULng(value)
                Select Case desiredType
                    Case BuiltInDataTypes.Byte
                        If tmpValue <= Byte.MaxValue Then
                            result = CByte(tmpValue)
                            Return True
                        Else
                            Return False
                        End If
                    Case BuiltInDataTypes.UShort
                        If tmpValue <= UShort.MaxValue Then
                            result = CUShort(tmpValue)
                            Return True
                        Else
                            Return False
                        End If
                    Case BuiltInDataTypes.UInteger
                        If tmpValue <= UInteger.MaxValue Then
                            result = CUInt(tmpValue)
                            Return True
                        Else
                            Return False
                        End If
                    Case BuiltInDataTypes.ULong
                        result = tmpValue
                        Return True
                    Case BuiltInDataTypes.SByte
                        If tmpValue <= SByte.MaxValue Then
                            result = CSByte(tmpValue)
                            Return True
                        Else
                            Return False
                        End If
                    Case BuiltInDataTypes.Short
                        If tmpValue <= Short.MaxValue Then
                            result = CShort(tmpValue)
                            Return True
                        Else
                            Return False
                        End If
                    Case BuiltInDataTypes.Integer
                        If tmpValue <= Integer.MaxValue Then
                            result = CInt(tmpValue)
                            Return True
                        Else
                            Return False
                        End If
                    Case BuiltInDataTypes.Long
                        If tmpValue <= Long.MaxValue Then
                            result = CLng(tmpValue)
                            Return True
                        Else
                            Return False
                        End If
                    Case Else
                        Throw New InternalException("")
                End Select
            Case TypeCode.SByte, TypeCode.Int16, TypeCode.Int32, TypeCode.Int64
                Dim tmpValue As Long = CLng(value)
                Select Case desiredType
                    Case BuiltInDataTypes.Byte
                        If tmpValue >= Byte.MinValue AndAlso tmpValue <= Byte.MaxValue Then
                            result = CByte(tmpValue)
                            Return True
                        Else
                            Return False
                        End If
                    Case BuiltInDataTypes.UShort
                        If tmpValue >= UShort.MinValue AndAlso tmpValue <= UShort.MaxValue Then
                            result = CUShort(tmpValue)
                            Return True
                        Else
                            Return False
                        End If
                    Case BuiltInDataTypes.UInteger
                        If tmpValue >= UInteger.MinValue AndAlso tmpValue <= UInteger.MaxValue Then
                            result = CUInt(tmpValue)
                            Return True
                        Else
                            Return False
                        End If
                    Case BuiltInDataTypes.ULong
                        If tmpValue >= ULong.MinValue AndAlso tmpValue <= ULong.MaxValue Then
                            result = CULng(tmpValue)
                            Return True
                        Else
                            Return False
                        End If
                    Case BuiltInDataTypes.SByte
                        If tmpValue >= SByte.MinValue AndAlso tmpValue <= SByte.MaxValue Then
                            result = CSByte(tmpValue)
                            Return True
                        Else
                            Return False
                        End If
                    Case BuiltInDataTypes.Short
                        If tmpValue >= Short.MinValue AndAlso tmpValue <= Short.MaxValue Then
                            result = CShort(tmpValue)
                            Return True
                        Else
                            Return False
                        End If
                    Case BuiltInDataTypes.Integer
                        If tmpValue >= Integer.MinValue AndAlso tmpValue <= Integer.MaxValue Then
                            result = CInt(tmpValue)
                            Return True
                        Else
                            Return False
                        End If
                    Case BuiltInDataTypes.Long
                        If tmpValue >= Long.MinValue AndAlso tmpValue <= Long.MaxValue Then
                            result = CLng(tmpValue)
                            Return True
                        Else
                            Return False
                        End If
                    Case Else
                        Throw New InternalException("")
                End Select
            Case Else
                Throw New InternalException("")
        End Select
    End Function
End Class

Public Enum ConversionType
    Implicit
    Explicit
    None
End Enum

Public Class TypeConversionInfo
    Public Conversion As ConversionType
    Public hasPrecisionLoss As Boolean
    Public BinaryAddResult As TypeCode
End Class
