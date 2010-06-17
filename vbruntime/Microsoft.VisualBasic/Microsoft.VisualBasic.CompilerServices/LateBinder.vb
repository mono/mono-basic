'
' LateBinder.vb
'
' Author:
'   Boris Kirzner (borisk@mainsoft.com)
'

'
' Copyright (C) 2002-2006 Mainsoft Corporation.
' Copyright (C) 2004-2006 Novell, Inc (http://www.novell.com)
'
' Permission is hereby granted, free of charge, to any person obtaining
' a copy of this software and associated documentation files (the
' "Software"), to deal in the Software without restriction, including
' without limitation the rights to use, copy, modify, merge, publish,
' distribute, sublicense, and/or sell copies of the Software, and to
' permit persons to whom the Software is furnished to do so, subject to
' the following conditions:
' 
' The above copyright notice and this permission notice shall be
' included in all copies or substantial portions of the Software.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
' EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
' MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
' NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
' LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
' OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
' WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

Imports System
Imports System.Reflection
Imports System.Globalization
Imports System.ComponentModel
'Helper Class for LateBinding. Not public.
Namespace Microsoft.VisualBasic.CompilerServices
    Friend Class LateBinder
        Inherits Binder

        Private _wasIncompleteInvocation As Boolean
        Private _invokeNext As MethodInfo
        Private _invokeNextArgs() As Object

        Public ReadOnly Property WasIncompleteInvocation() As Boolean
            Get
                Return _wasIncompleteInvocation
            End Get
        End Property
        Public ReadOnly Property InvokeNext() As MethodInfo
            Get
                Return _invokeNext
            End Get
        End Property
        Public ReadOnly Property InvokeNextArgs() As Object()
            Get
                Return _invokeNextArgs
            End Get
        End Property
        
        Private Enum TypeConversion
            NotConvertible
            Equal
            [Widening]
            [Narrowing]
        End Enum

        Private Enum SignatureCompare
            Equal
            Left
            Right
            Ambiguity
        End Enum

        Public Sub New()
            MyBase.new()
        End Sub

        Class BState
            Public mapping() As Integer
            Public parameters() As ParameterInfo
            Public oargs() As Object
        End Class

        Public Shared Function GetStateInstance() As Object
            Return New BState
        End Function

        Public Overrides Function BindToField(ByVal bindingAttr As System.Reflection.BindingFlags, _
                                                ByVal match() As System.Reflection.FieldInfo, _
                                                ByVal value As Object, _
                                                ByVal culture As System.Globalization.CultureInfo) As System.Reflection.FieldInfo
            Throw New NotImplementedException
        End Function

#If Moonlight = False Then
        Friend Function Array_IndexOf(ByVal Array As Integer(), ByVal item As Integer) As Integer
            Return System.Array.IndexOf(Array, item)
        End Function

        Friend Function Array_IndexOf(ByVal Array As String(), ByVal item As String) As Integer
            Return System.Array.IndexOf(Array, item)
        End Function
#Else
        Friend Function Array_IndexOf(ByVal Array As Integer(), ByVal item As Integer) As Integer
            Return System.Array.IndexOf(Of Integer)(Array, item)
        End Function

        Friend Function Array_IndexOf(ByVal Array As String(), ByVal item As String) As Integer
            Return System.Array.IndexOf(Of String)(Array, item)
        End Function
#End If
        Public Overrides Function BindToMethod(ByVal bindingAttr As System.Reflection.BindingFlags, _
                                                ByVal match() As System.Reflection.MethodBase, _
                                                ByRef args() As Object, _
                                                ByVal modifiers() As System.Reflection.ParameterModifier, _
                                                ByVal culture As System.Globalization.CultureInfo, _
                                                ByVal names() As String, _
                                                ByRef state As Object) As System.Reflection.MethodBase

			If match Is Nothing OrElse match.Length = 0 Then
				return Nothing
			End If

            state = GetStateInstance()
            CType(state, BState).oargs = CType(args.Clone(), Object())

            Dim matchExists As Boolean = False

            'FIXME : add filtering of methods hiding by name and by signature

            Dim nameMatches() As MethodBase = FilterMethodsByParameterName(match, names)

            If nameMatches Is Nothing Then
                Throw New MissingMemberException
            End If
            Dim potentialMatches() As MethodBase = FilterMethods(nameMatches, args, names)

            If potentialMatches Is Nothing Then
                'FIXME more elegant care of different failure cases
                If match.Length = 1 Then
                    If Not names Is Nothing Then
                        Dim parameters As ParameterInfo() = match(0).GetParameters()
                        If IsParamArray(parameters, parameters.Length - 1) Then
                            If Array_IndexOf(names, parameters(parameters.Length - 1).Name) <> -1 Then
                                Throw New ArgumentException("Named arguments cannot match ParamArray parameters.")
                            End If
                        End If
                    End If
                Else
                    If Not names Is Nothing Then ' there are name matches but not parameter matches
                        Throw New AmbiguousMatchException
                    End If
                End If
                Throw New MissingMemberException
            End If

            For j As Integer = 0 To potentialMatches.Length - 1
                Dim matches(potentialMatches.Length - j) As MethodBase
                Dim matchesCount As Integer = 0
                Dim fj As MethodBase = potentialMatches(j)

                matches(matchesCount) = fj
                matchesCount += 1

                For k As Integer = 0 To potentialMatches.Length - 1
                    If k <> j Then
                        Dim fk As MethodBase = potentialMatches(k)
                        Dim order As SignatureCompare = CompareMethods(fj, fk, args)

                        Select Case order
                            Case SignatureCompare.Ambiguity
                                matches(matchesCount) = fk
                                matchesCount += 1
                            Case SignatureCompare.Left
                                ' fk < fj , continue checking
                            Case SignatureCompare.Right
                                'fk > fj , fj does not match
                                matchesCount = 0
                                Exit For
                                ' Case SignatureCompare.Equal impossible case
                        End Select
                    End If
                Next

                If matchesCount = 1 Then 'fj suits better that any other fk

                    PrepareParameters(args, fj, names, CType(state, BState))
                    CType(state, BState).parameters = fj.GetParameters()
                    Return fj
                ElseIf matchesCount > 1 Then ' there are ambigous fj and fk
                    Throw New AmbiguousMatchException
                End If
            Next

            Return Nothing
        End Function

        Public Overrides Function ChangeType(ByVal value As Object, ByVal type1 As System.Type, _
                                    ByVal culture As System.Globalization.CultureInfo) As Object
            Return ConvertValue(value, type1)
        End Function

        Public Overrides Sub ReorderArgumentArray(ByRef args() As Object, ByVal state As Object)
			If state Is Nothing Or args Is Nothing Then
				Return
			End If
            Dim bstate As BState = CType(state, BState)
            Dim mapping() As Integer = bstate.mapping
            Dim parameters() As ParameterInfo = bstate.parameters
            Dim clone() As Object = CType(args.Clone(), Object())
            Dim oargs() As Object = bstate.oargs
            Dim cpos As Integer = 0

            If args.Length <> oargs.Length Then
                ReDim args(oargs.Length - 1)
            End If

            For i As Integer = 0 To args.Length - 1
                If IsParamArray(parameters, i) Then
                    If args.Length - 1 >= i Then
                        If oargs(i).GetType().IsArray Then
                            args(i) = clone(cpos) ' param array was passed as array
                        Else
                            Dim arr As Array = CType(clone(cpos), Array)
                            For j As Integer = 0 To arr.Length - 1
                                args(i) = arr.GetValue(j)
                                i += 1
                            Next
                        End If
                    Else
                        'FIXME: impossible case
                    End If
                    Exit For
                Else
                    If mapping Is Nothing Then
                        args(i) = clone(cpos) 'FIXME: is it possible that method was invoked after the mapping failed? 
                    Else
                        Dim reverseMapping As Integer = Array_IndexOf(mapping, i)
                        If reverseMapping > -1 Then
                            If parameters(i).ParameterType.IsByRef Then
                                'ByRef parameters
                                If Not oargs(i) Is Nothing Then
                                    If oargs(i).GetType().IsArray Then
                                        'array that was passed by ref
                                        Dim arr As Array = CType(clone(reverseMapping), Array)
                                        Array.Copy(arr, CType(oargs(i), Array), arr.Length)
                                        args(i) = oargs(i)
                                    ElseIf oargs(i).GetType().IsPrimitive Then
                                        'primitives passed by ref should be updated
                                        args(i) = clone(reverseMapping)
                                    End If
                                End If
                            Else
                                'ByVal parameters
                                args(i) = clone(reverseMapping)
                            End If
                        End If
                    End If
                End If

                If Not IsParamArray(parameters, i) Then
                    cpos += 1
                End If
            Next

        End Sub

        Public Overrides Function SelectMethod(ByVal bindingAttr As System.Reflection.BindingFlags, _
                                                ByVal match() As System.Reflection.MethodBase, _
                                                ByVal types() As System.Type, _
                                                ByVal modifiers() As System.Reflection.ParameterModifier) As System.Reflection.MethodBase
            Throw New NotImplementedException
        End Function
        Public Overrides Function SelectProperty(ByVal bindingAttr As System.Reflection.BindingFlags, _
                                                ByVal match() As System.Reflection.PropertyInfo, _
                                                ByVal returnType As System.Type, _
                                                ByVal indexes() As System.Type, _
                                                ByVal modifiers() As System.Reflection.ParameterModifier) As System.Reflection.PropertyInfo

            Throw New NotImplementedException
        End Function

        Private Function PrepareArguments(ByVal args() As Object, ByVal parameters() As ParameterInfo, ByVal names() As String, ByVal bstate As BState) As Object()
            If parameters.Length = 0 Then 'no parameters
                Return args
            End If

            Dim mapping() As Integer = GetArgumentsMapping(parameters, names, args)

            If Not bstate Is Nothing Then
                bstate.mapping = mapping
            End If

            If mapping Is Nothing Then
                Return Nothing 'name mapping failed
            End If

            Dim preparedArguments() As Object = CType(args.Clone(), Object())

            ' optional parameters case (optional parameters are always at the end)
            If parameters(parameters.Length - 1).IsOptional Then
                If args.Length > parameters.Length Then
                    Return Nothing 'there are more arguments than parameters
                End If

                ReDim preparedArguments(parameters.Length - 1)
                For i As Integer = 0 To parameters.Length - 1
                    Dim mappedArgument As Integer = Array_IndexOf(mapping, i)
                    If mappedArgument = -1 Or mappedArgument > args.Length - 1 Then
                        If parameters(i).IsOptional Then
                            preparedArguments(i) = parameters(i).DefaultValue 'initialize optional parameter
                        Else
                            Return Nothing 'not-optional parameter missing
                        End If
                    ElseIf args(mappedArgument) Is Missing.Value AndAlso parameters(i).IsOptional Then
                        preparedArguments(i) = parameters(i).DefaultValue
                    Else
                        preparedArguments(i) = args(mappedArgument) 'initialize parameter from argument
                    End If
                Next
                Return preparedArguments
            End If

            ' param array case
            If IsParamArray(parameters, parameters.Length - 1) Then
                If args.Length > 0 Then
                    If Not names Is Nothing Then ' for simplicity, care about named prameters case separately
                        For i As Integer = 0 To args.Length - 1
                            Dim mappedArgument As Integer = Array_IndexOf(mapping, i)
                            If mappedArgument = -1 Then
                                If i = args.Length - 1 Then ' i.e. there is no mapping for param array
                                    Exit For ' its ok, just finish preparing arguments
                                End If
                            Else
                                If i = parameters.Length - 1 Then
                                    Return Nothing ' param array can not be mapped by name
                                End If
                                preparedArguments(i) = args(mappedArgument) 'initialize parameter from argument
                            End If
                        Next
                    Else
                        If args(args.Length - 1).GetType().IsArray Then
                            If args.Length > parameters.Length Then  'last arg matches param array, but there are too much args
                                Return Nothing
                            End If
                            ' param array passed as array argument
                            Dim index As Integer = args.Length - 1
                            Dim pcount As Integer = CType(args(args.Length - 1), Array).Length
                            Dim arr As Object = args(index)
                            preparedArguments = DirectCast (Utils.CopyArray (preparedArguments, new Object (index + pcount - 1) {}), Object ())
                            For i As Integer = 0 To pcount - 1
                                preparedArguments(index + i) = CType(arr, Array).GetValue(i)
                            Next
                        End If
                    End If
                End If
                Return preparedArguments
            End If

            For i As Integer = 0 To args.Length - 1
                Dim mappedArgument As Integer = Array_IndexOf(mapping, i)
                If mappedArgument = -1 Or mappedArgument > args.Length - 1 Then
                    Return Nothing 'not-optional parameter missing
                Else
                    preparedArguments(i) = args(mappedArgument) 'initialize parameter from argument
                End If
            Next

            Return preparedArguments
        End Function

        Private Function GetArgumentsMapping(ByVal parameters() As ParameterInfo, ByVal names() As String, ByVal args() As Object) As Integer()
            Dim mapping(parameters.Length - 1) As Integer
            For i As Integer = 0 To mapping.Length - 1
                mapping(i) = i
            Next

            If names Is Nothing Then
                Return mapping
            End If

            Dim parameterNames(parameters.Length - 1) As String
            For i As Integer = 0 To parameters.Length - 1
                parameterNames(i) = parameters(i).Name
            Next

            Dim namedArgumentsStart As Integer = args.Length - names.Length

            For i As Integer = 0 To mapping.Length - 1
                If i < names.Length Then
                    Dim m As Integer = Array_IndexOf(parameterNames, names(i))
                    If m = -1 Then
                        Return Nothing ' mapping failed
                    End If
                    mapping(i + namedArgumentsStart) = m
                End If
            Next

            Return mapping
        End Function

        Private Sub PrepareParameters(ByRef args() As Object, ByVal f As MethodBase, ByVal names() As String, ByVal bstate As BState)

            Dim parameters() As ParameterInfo = f.GetParameters()

            Dim vals() As Object
            Dim i As Integer

            If parameters.Length = 0 Then
                Return
            End If

            vals = PrepareArguments(args, parameters, names, bstate)

            If parameters.Length <> args.Length Then
                ReDim args(parameters.Length - 1) ' args must be the same size as parameters
            End If

            Dim hasParamArray As Boolean = False
            For i = 0 To args.Length - 1 ' fit args to parameters until param array
                If IsParamArray(parameters, i) Then
                    hasParamArray = True
                    Exit For
                End If
                args(i) = ConvertValue(vals(i), GetParameterType(parameters, i))
            Next

            If hasParamArray Then
                ' special case when param array args are passed as one argument that is actually an array
                If (vals.Length - i) = 1 Then
                    If vals(i).GetType().IsArray Then
                        args(i) = vals(i)
                        Return
                    End If
                    ' if the last value remained is not array, put it in args array as usual
                End If

                ' store rest of args (if any) in param array
                ' if args count is the same as params count excluding param array
                ' we pass empty param array
                Dim arrType As Type = GetParameterType(parameters, i)
                Dim pcount As Integer = vals.Length - i
                Dim parr As Array = Array.CreateInstance(arrType, pcount)

                For j As Integer = 0 To pcount - 1
                    parr.SetValue(ConvertValue(vals(i + j), arrType), j)
                Next

                args(parameters.Length - 1) = parr
            End If
        End Sub

        Private Function ConvertValue(ByVal value As Object, ByVal type2 As Type) As Object

            If value Is Nothing Then
                Return Nothing
            End If

            Dim type1 As Type = value.GetType()

            If type1 Is type2 Then
                Return value
            End If

            If type1.IsByRef Then
                type1 = type1.GetElementType()
            End If

            If type1.IsPrimitive And type2.IsPrimitive Then
                Select Case Type.GetTypeCode(type2)
                    Case TypeCode.Boolean
                        Return Conversions.ToBoolean(value)
                    Case TypeCode.Byte
                        Return Conversions.ToByte(value)
                    Case TypeCode.SByte
                        Return Conversions.ToSByte(value)
                    Case TypeCode.Char
                        Return Conversions.ToChar(value)
                    Case TypeCode.DateTime
                        Return Conversions.ToDate(value)
                    Case TypeCode.Double
                        Return Conversions.ToDouble(value)
                    Case TypeCode.Decimal
                        Return Conversions.ToDecimal(value)
                    Case TypeCode.Int32
                        Return Conversions.ToInteger(value)
                    Case TypeCode.Int16
                        Return Conversions.ToShort(value)
                    Case TypeCode.Int64
                        Return Conversions.ToLong(value)
                    Case TypeCode.UInt32
                        Return Conversions.ToUInteger(value)
                    Case TypeCode.UInt16
                        Return Conversions.ToUShort(value)
                    Case TypeCode.UInt64
                        Return Conversions.ToULong(value)
                    Case TypeCode.Single
                        Return Conversions.ToSingle(value)
                End Select
                Return Convert.ChangeType(value, type2, CultureInfo.CurrentCulture)
            Else
                If type2 Is GetType(String) Then
                    Return StringType.FromObject(value)
                End If

                If type1 Is GetType(String) And type2.IsPrimitive Then
                    Select Case Type.GetTypeCode(type2)
                        Case TypeCode.Boolean
                            Return Conversions.ToBoolean(value)
                        Case TypeCode.Byte
                            Return Conversions.ToByte(value)
                        Case TypeCode.SByte
                            Return Conversions.ToSByte(value)
                        Case TypeCode.Char
                            Return Conversions.ToChar(value)
                        Case TypeCode.DateTime
                            Return Conversions.ToDate(value)
                        Case TypeCode.Double
                            Return Conversions.ToDouble(value)
                        Case TypeCode.Decimal
                            Return Conversions.ToDecimal(value)
                        Case TypeCode.Int32
                            Return Conversions.ToInteger(value)
                        Case TypeCode.Int16
                            Return Conversions.ToShort(value)
                        Case TypeCode.Int64
                            Return Conversions.ToLong(value)
                        Case TypeCode.UInt32
                            Return Conversions.ToUInteger(value)
                        Case TypeCode.UInt16
                            Return Conversions.ToUShort(value)
                        Case TypeCode.UInt64
                            Return Conversions.ToULong(value)
                        Case TypeCode.Single
                            Return Conversions.ToSingle(value)
                    End Select
                End If
            End If

            Return value
        End Function

        Private Function CompareMethods(ByVal f1 As MethodBase, ByVal f2 As MethodBase, ByVal args() As Object) As SignatureCompare
            Dim params1() As ParameterInfo = f1.GetParameters()
            Dim params2() As ParameterInfo = f2.GetParameters()
            Dim previous As SignatureCompare = SignatureCompare.Equal
            Dim current As SignatureCompare
            Dim paramArrayDecision As Boolean = False

            For i As Integer = 0 To Math.Max(args.Length, Math.Min(params1.Length, params2.Length)) - 1 'args.Length - 1
                Dim f1iType As Type = GetParameterType(params1, i)
                Dim f2iType As Type = GetParameterType(params2, i)
                Dim ptype As Type = GetArgumentType(args, i)

                Dim f1i As TypeConversion = CompareTypes(ptype, f1iType)
                Dim f2i As TypeConversion = CompareTypes(ptype, f2iType)

                If f1i = TypeConversion.Widening And f2i = TypeConversion.Widening Then
                    'current = CompareWideningTypes(f1iType, f2iType, IsParamArray(params1, i), IsParamArray(params2, i))
                    If Not ptype Is Nothing Then
                        current = CompareWideningTypes(f1iType, f2iType)
                    Else
                        current = CompareWideningTypesWithNothing(f1iType, f2iType) ' for explicit null arguments
                    End If

                    If current = SignatureCompare.Equal Then
                        Dim type1isParamArray As Boolean = IsParamArray(params1, i)
                        Dim type2isParamArray As Boolean = IsParamArray(params2, i)

                        'If paramArrayDecision Then
                        If previous = SignatureCompare.Equal Or paramArrayDecision Then
                            If type1isParamArray And type2isParamArray Then
                                'if occurs, it will be always the case until we reach the args end
                                If paramArrayDecision Then
                                    current = SignatureCompare.Ambiguity
                                End If
                            ElseIf type1isParamArray And Not type2isParamArray Then
                                current = SignatureCompare.Right
                                paramArrayDecision = True
                            ElseIf type2isParamArray And Not type1isParamArray Then
                                current = SignatureCompare.Left
                                paramArrayDecision = True
                            End If
                        End If
                        'End If
                    End If
                Else
                    If f1i = TypeConversion.Equal And f2i = TypeConversion.Widening Then
                        current = SignatureCompare.Left
                    ElseIf f1i = TypeConversion.Widening And f2i = TypeConversion.Equal Then
                        current = SignatureCompare.Right
                    Else
                        'if we get here that means 
                        ' f1i = TypeConversion.Equal And  f2i = TypeConversion.Equal
                        current = SignatureCompare.Equal
                    End If
                End If

                If current = SignatureCompare.Ambiguity Then 'no need to check anymore
                    Return SignatureCompare.Ambiguity
                End If

                ' now collate previous comparison result with current
                Select Case previous
                    Case SignatureCompare.Equal
                        previous = current
                    Case SignatureCompare.Left
                        If current = SignatureCompare.Right Then ' previous was f1 and now we get f2
                            Return SignatureCompare.Ambiguity
                        End If
                        'result stays f1 and we continue
                    Case SignatureCompare.Right
                        If current = SignatureCompare.Left Then ' previous was f2 and neow we get f1
                            Return SignatureCompare.Ambiguity
                        End If
                        ' result stays f2 and we continue
                End Select
            Next

            If previous = SignatureCompare.Equal Or paramArrayDecision Then
                If params1.Length <> args.Length Or params2.Length <> args.Length Then
                    ' occurs only if we're dealing with param arrays
                    Dim isParamArray1 As Boolean = IsParamArray(params1, params1.Length - 1)
                    Dim isParamArray2 As Boolean = IsParamArray(params2, params2.Length - 1)
                    If isParamArray1 And isParamArray2 Then
                        ' if both methods have param array - its ambiguity
                        Return SignatureCompare.Ambiguity
                    Else
                        If isParamArray1 Then ' first is param array - choose second
                            Return SignatureCompare.Right
                        ElseIf isParamArray2 Then ' second is param array - choose first
                            Return SignatureCompare.Left
                        End If
                    End If
                End If
            End If
            Return previous
        End Function

        Private Function CompareWideningTypes(ByVal f1 As Type, ByVal f2 As Type, ByVal type1isParamArray As Boolean, ByVal type2isParamArray As Boolean) As SignatureCompare
            If f1 Is f2 Then
                If type1isParamArray And type2isParamArray Then
                    Return SignatureCompare.Ambiguity
                ElseIf type1isParamArray And Not type2isParamArray Then
                    Return SignatureCompare.Right
                ElseIf type2isParamArray And Not type1isParamArray Then
                    Return SignatureCompare.Left
                End If
            End If
            Return CompareWideningTypes(f1, f2)
        End Function

        Private Function CompareWideningTypesWithNothing(ByVal f1 As Type, ByVal f2 As Type) As SignatureCompare
            If f1 Is f2 Then
                Return SignatureCompare.Equal
            End If

            Dim typeCode1 As TypeCode = Type.GetTypeCode(f1)
            Dim typeCode2 As TypeCode = Type.GetTypeCode(f2)

            If f1.IsPrimitive And f2.IsPrimitive Then
                If typeCode1 >= TypeCode.Byte And typeCode1 <= TypeCode.UInt64 Then
                    If typeCode2 >= TypeCode.Byte And typeCode2 <= TypeCode.UInt64 Then
                        'primitives between byte and ulong are compared by size, the smaller is better
                        If typeCode1 < typeCode2 Then
                            Return SignatureCompare.Left
                        Else
                            Return SignatureCompare.Right
                        End If
                    End If
                End If
            Else
                If f1.IsPrimitive Or f2.IsPrimitive Then
                    If f1.IsPrimitive Then
                        If f2 Is GetType(Object) Then 'primitive is better than object
                            Return SignatureCompare.Left
                        End If

                        If typeCode1 = TypeCode.Char And typeCode2 = TypeCode.String Then 'char is better that string
                            Return SignatureCompare.Left
                        End If
                    ElseIf f2.IsPrimitive Then
                        If f1 Is GetType(Object) Then 'primitive is better than object
                            Return SignatureCompare.Right
                        End If

                        If typeCode2 = TypeCode.Char And typeCode1 = TypeCode.String Then 'char is better that string
                            Return SignatureCompare.Right
                        End If
                    End If
                Else
                    'both f1 and f2 are not primitives - subclass is better that superclass
                    If f1.IsSubclassOf(f2) Then    'f1 is a subclass of f2
                        Return SignatureCompare.Left
                    ElseIf f2.IsSubclassOf(f1) Then 'f2 is a subclass of f1
                        Return SignatureCompare.Right
                    End If
                End If
            End If
            ' the rest of the cases are ambiguos
            Return SignatureCompare.Ambiguity
        End Function
        Private Function CompareWideningTypes(ByVal f1 As Type, ByVal f2 As Type) As SignatureCompare

            If f1 Is f2 Then
                Return SignatureCompare.Equal
            End If

            Dim typeCode1 As TypeCode = Type.GetTypeCode(f1)
            Dim typeCode2 As TypeCode = Type.GetTypeCode(f2)

            If f1.IsPrimitive And f2.IsPrimitive Then
                If typeCode1 < typeCode2 Then
                    Return SignatureCompare.Left
                Else
                    Return SignatureCompare.Right
                End If
            Else

                If typeCode1 = TypeCode.String Then
                    Return SignatureCompare.Left
                ElseIf typeCode2 = TypeCode.String Then
                    Return SignatureCompare.Right
                End If


                If f1.IsPrimitive Then ' and f2 is not primitive
                    Return SignatureCompare.Left
                End If

                If f2.IsPrimitive Then  ' and f1 is not primitive
                    Return SignatureCompare.Right
                End If

                'f1 and f2 are not primitives => f1, f2 have some kind of inheritance relation

                ' object is preferred over a string - inspite of inheritance relation
                If f1 Is GetType(Object) And f2 Is GetType(String) Then
                    Return SignatureCompare.Left
                ElseIf f2 Is GetType(Object) And f1 Is GetType(String) Then
                    Return SignatureCompare.Right
                End If

                ' "pure" inheritance logic : prefer subclass over supeclass
                If f1.IsSubclassOf(f2) Then    'f1 is a subclass of f2
                    Return SignatureCompare.Left
                ElseIf f2.IsSubclassOf(f1) Then 'f2 is a subclass of f1
                    Return SignatureCompare.Right
                End If

            End If
        End Function

        Private Function GetArgumentType(ByVal args() As Object, ByVal number As Integer) As Type
            If number <= args.Length - 1 Then
                If Not args(number) Is Nothing Then
                    Return args(number).GetType()
                End If
            End If
            Return Nothing
        End Function
        Private Function GetParameterType(ByVal parameters() As ParameterInfo, ByVal number As Integer) As Type
            Dim ptype As Type
            If number < parameters.Length - 1 Then
                ptype = parameters(number).ParameterType
            ElseIf number = parameters.Length - 1 Then
                If IsParamArray(parameters, number) Then
                    ptype = parameters(number).ParameterType.GetElementType()
                Else
                    ptype = parameters(number).ParameterType
                End If
            Else
                If IsParamArray(parameters, parameters.Length - 1) Then
                    ptype = parameters(parameters.Length - 1).ParameterType.GetElementType()
                Else
                    Return Nothing
                End If
            End If

            If ptype.IsByRef Then
                ptype = ptype.GetElementType()
            End If

            Return ptype
        End Function

        Private Function IsParamArray(ByVal parameters() As ParameterInfo, ByVal index As Integer) As Boolean
            If parameters.Length = 0 Then
                Return False
            End If

            If index >= parameters.Length Then
                index = parameters.Length - 1
            End If

            Dim attrs() As Object = parameters(index).GetCustomAttributes(True)
            For i As Integer = 0 To attrs.Length - 1
                If TypeOf attrs(i) Is ParamArrayAttribute Then
                    Return True
                End If
            Next

            Return False
        End Function

        Private Function FilterMethodsByParameterName(ByVal methods() As MethodBase, ByVal names() As String) As MethodBase()
            If names Is Nothing Then
                Return methods
            End If

            Dim match(methods.Length - 1) As MethodBase
            Dim matchCount As Integer = 0
            For i As Integer = 0 To methods.Length - 1
                Dim parameters() As ParameterInfo = methods(i).GetParameters()
                Dim paramNames(parameters.Length) As String
                For j As Integer = 0 To parameters.Length - 1
                    paramNames(j) = parameters(j).Name
                Next

                Dim nameMatch As Boolean = True
                For j As Integer = 0 To names.Length - 1
                    If Array_IndexOf(paramNames, names(j)) = -1 Then
                        nameMatch = False
                        Exit For
                    End If
                Next

                If nameMatch Then
                    match(matchCount) = methods(i)
                    matchCount += 1
                End If
            Next

            If matchCount = 0 Then
                Return Nothing
            End If

            match = DirectCast (Utils.CopyArray(match, new MethodBase (matchCount - 1) {}), MethodBase ())
            Return match
        End Function

        Private Function IsGetterThatReturnObjectWithIndexer(ByVal method As MethodBase, ByRef args() As Object) As Boolean
            If (method.Name.StartsWith("get_")) Then
                Dim minfo As MethodInfo = CType(method, MethodInfo)
                Dim retType As Type = minfo.ReturnType
                If (retType.IsArray) Then
                    _invokeNext = retType.GetMethod("get_Item")
                    _invokeNextArgs = args
                    _wasIncompleteInvocation = True
                    args = Nothing
                    Return True
                End If

                Dim newMethod As MethodInfo = retType.GetMethod("get_Blubber")
                If (Not newMethod Is Nothing) Then
                    _invokeNext = newMethod
                    _invokeNextArgs = args
                    _wasIncompleteInvocation = True
                    args = Nothing
                    Return True
                End If
            End If
            Return False
        End Function


        Private Function FilterMethods(ByVal methods() As MethodBase, ByRef args() As Object, ByVal names() As String) As MethodBase()
            Dim filteredMethods(methods.Length) As MethodBase
            Dim methodsCount As Integer = 0
            Dim narrowingAmbiguity As Boolean = False

            Dim previous As TypeConversion
            Dim current As TypeConversion

            For i As Integer = 0 To methods.Length - 1
                Dim parameters() As ParameterInfo = methods(i).GetParameters()

                If i = 0 Then
                    previous = CompareSignature(args, parameters, names)
                    If Not previous = TypeConversion.NotConvertible Then
                        filteredMethods(methodsCount) = methods(i)
                        methodsCount += 1
                        If previous = TypeConversion.Equal Then
                            Exit For
                        End If
                    ElseIf (IsGetterThatReturnObjectWithIndexer(methods(i), args)) Then
                        previous = TypeConversion.Equal
                        filteredMethods(methodsCount) = methods(i)
                        methodsCount += 1
                        Exit For
                    End If

                Else
                    current = CompareSignature(args, parameters, names)

                    Select Case current
                        Case TypeConversion.Equal
                            previous = current
                            ReDim filteredMethods(0)
                            filteredMethods(0) = methods(i)
                            methodsCount = 1
                            Exit For
                        Case TypeConversion.Narrowing
                            If previous = TypeConversion.Narrowing Then
                                narrowingAmbiguity = True
                                'Throw New AmbiguousMatchException 
                                'do no thow since we still can find Equal or Widening
                            End If
                        Case TypeConversion.Widening
                            Select Case previous
                                Case TypeConversion.Equal
                                    ' do nothing
                                Case TypeConversion.Narrowing
                                    previous = TypeConversion.Widening
                                    ' we've already stored method with Narrowing - remove it
                                    ' this call is redundant sine there is only 1 method stored
                                    ' Array.Clear(filteredMethods,0,filteredMethods.Length)
                                    ' zerowing counter is enouph
                                    methodsCount = 0
                                    ' now we can store a current method
                                    filteredMethods(methodsCount) = methods(i)
                                    methodsCount += 1
                                Case TypeConversion.Widening
                                    filteredMethods(methodsCount) = methods(i)
                                    methodsCount += 1
                                Case TypeConversion.NotConvertible
                                    filteredMethods(methodsCount) = methods(i)
                                    methodsCount += 1
                            End Select
                    End Select
                End If
            Next

            If methodsCount = 0 Then
                Return Nothing
            Else
                If previous = TypeConversion.Narrowing And narrowingAmbiguity Then
                    ' thre are more that 1 narrowing methods and this is the best
                    Throw New AmbiguousMatchException
                End If
                filteredMethods = DirectCast(Utils.CopyArray(filteredMethods, New MethodBase(methodsCount - 1) {}), MethodBase())
                Return filteredMethods
            End If
        End Function

        Private Function CompareSignature(ByVal args() As Object, ByVal parameters() As ParameterInfo, ByVal names() As String) As TypeConversion
            Dim current As TypeConversion
            Dim previous As TypeConversion = TypeConversion.NotConvertible

            If args.Length = 0 And parameters.Length = 0 Then  ' private case to make the things easy
                Return TypeConversion.Equal
            End If

            'basic parameters check
            If args.Length > parameters.Length And Not IsParamArray(parameters, parameters.Length - 1) Then
                ' args do not suit even parameters count
                Return TypeConversion.NotConvertible
            End If

            If parameters(parameters.Length - 1).IsOptional Then   ' check the args fill at least not optional parameters
                Dim optionalParametersCount As Integer = 0
                For i As Integer = 0 To parameters.Length - 1
                    If parameters(i).IsOptional Then
                        optionalParametersCount += 1
                    End If
                Next
                'if args does not fill non optional prameters
                If args.Length < parameters.Length - optionalParametersCount Then
                    Return TypeConversion.NotConvertible
                End If
            End If

            Dim margs() As Object = PrepareArguments(args, parameters, names, Nothing)

            If margs Is Nothing Then
                Return TypeConversion.NotConvertible 'failed to map parameters
            End If

            'special case of no arguments passed
            If margs.Length = 0 Then
                If IsParamArray(parameters, 0) Then 'method receives only param array, empty param array suits
                    Return TypeConversion.Widening
                Else
                    Return TypeConversion.NotConvertible 'method receives additional parameters
                End If
            End If

            For i As Integer = 0 To margs.Length - 1
                Dim type1 As Type
                Dim type2 As Type = GetParameterType(parameters, i)

                If margs(i) Is Nothing Then
                    current = TypeConversion.Widening
                Else
                    type1 = margs(i).GetType()
                    current = CompareTypes(type1, type2, IsParamArray(parameters, i))
                End If

                If current = TypeConversion.NotConvertible Then
                    Return current
                End If

                If i = 0 Then
                    previous = current
                Else
                    Select Case previous
                        Case TypeConversion.Equal
                            previous = current
                        Case TypeConversion.Narrowing
                            previous = TypeConversion.Narrowing
                        Case TypeConversion.Widening
                            If current = TypeConversion.Narrowing Then
                                previous = TypeConversion.Narrowing
                            End If
                    End Select
                End If
            Next

            ' args suit parameters checked, but there can exist more parameters
            If previous <> TypeConversion.NotConvertible And parameters.Length > margs.Length Then
                If IsParamArray(parameters, margs.Length) Then
                    'only if next (and the last parameter) is param array, its ok
                    If previous = TypeConversion.Equal Then
                        previous = TypeConversion.Widening
                    End If
                Else
                    Return TypeConversion.NotConvertible ' if there are more params, args does not suit
                End If
            End If

            Return previous
        End Function

        Private Function CompareTypes(ByVal type1 As Type, ByVal type2 As Type, ByVal type2isParamArray As Boolean) As TypeConversion
            Dim basicConversion As TypeConversion = CompareTypes(type1, type2)

            If type2isParamArray And basicConversion = TypeConversion.Equal Then
                Return TypeConversion.Widening
            End If

            Return basicConversion
        End Function

        Private Function CompareTypes(ByVal type1 As Type, ByVal type2 As Type) As TypeConversion
            If type1 Is type2 Then
                Return TypeConversion.Equal
            End If

            If type1 Is Nothing Then
                Return TypeConversion.Widening
            ElseIf type2 Is Nothing Then
                Return TypeConversion.NotConvertible
            End If

            Dim typeCode1 As TypeCode = Type.GetTypeCode(type1)
            Dim typeCode2 As TypeCode = Type.GetTypeCode(type2)

            If type1.IsPrimitive And type2.IsEnum Then
                Return TypeConversion.Narrowing
            ElseIf type1.IsEnum And type2.IsPrimitive Then
                Return TypeConversion.Widening
            End If

            If type1.IsPrimitive And type2.IsPrimitive Then

                If typeCode1 = TypeCode.Char Or typeCode2 = TypeCode.Char Then
                    Return TypeConversion.Narrowing
                End If

                If typeCode1 = TypeCode.Boolean Or typeCode2 = TypeCode.Boolean Then
                    Return TypeConversion.Narrowing
                End If

                If typeCode1 > TypeCode.Boolean And typeCode1 <= TypeCode.Decimal And _
                    typeCode2 > TypeCode.Boolean And typeCode2 <= TypeCode.Decimal Then

                    If typeCode1 < typeCode2 Then
                        Return TypeConversion.Widening
                    Else
                        Return TypeConversion.Narrowing
                    End If

                End If
                'FIXME additional cases
            Else
                If type1.IsPrimitive Then
                    ' primitive can be converted to string or object
                    'FIXME: is char to string convert widens or narrows ?
                    If typeCode1 = TypeCode.Char And typeCode2 = TypeCode.String Then
                        Return TypeConversion.Widening
                    End If

                    If typeCode2 = TypeCode.String Then
                        Return TypeConversion.Narrowing
                    ElseIf type2 Is GetType(Object) Then
                        Return TypeConversion.Widening
                    Else
                        Return TypeConversion.NotConvertible
                    End If
                End If

                If type2.IsPrimitive Then
                    ' string can be converted to primitive
                    If typeCode1 = TypeCode.String Then
                        Return TypeConversion.Narrowing
                    End If

                    'FIXME: is this should be valid for IConvertible only?
                    Return TypeConversion.Narrowing
                End If

                ' objects can be converted to strings
                If typeCode2 = TypeCode.String Then
                    If type1.IsArray Then
                        Return TypeConversion.NotConvertible
                    Else
                        Return TypeConversion.Narrowing
                    End If
                End If

                If type1.IsSubclassOf(type2) Then
                    Return TypeConversion.Widening
                ElseIf type2.IsSubclassOf(type1) Then
                    Return TypeConversion.Narrowing
                End If
            End If

            Return TypeConversion.NotConvertible
        End Function

    End Class
End Namespace
