'
' LateBinding.vb
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
Imports System.Diagnostics

Namespace Microsoft.VisualBasic.CompilerServices
    <System.ComponentModel.EditorBrowsable(ComponentModel.EditorBrowsableState.Never)> _
    Public NotInheritable Class LateBinding

        Private Shared ReadOnly Property LBinder() As Microsoft.VisualBasic.CompilerServices.LateBinder
            Get
                Return New LateBinder
            End Get
        End Property

        <DebuggerStepThrough(), DebuggerHidden()> _
        Public Shared Sub LateCall(ByVal o As Object, ByVal objType As System.Type, ByVal name As String, ByVal args() As Object, ByVal paramnames() As String, ByVal CopyBack() As Boolean)
            LateGet(o, objType, name, args, paramnames, CopyBack)
        End Sub

        <DebuggerStepThrough(), DebuggerHidden()> _
        Public Shared Sub LateIndexSet(ByVal o As Object, ByVal args() As Object, ByVal paramnames() As String)
            Dim realType As System.Type = o.GetType()
            Dim flags As BindingFlags

            If realType.IsArray Then
                flags = BindingFlags.IgnoreCase Or BindingFlags.NonPublic Or BindingFlags.Public Or BindingFlags.Instance Or BindingFlags.InvokeMethod

#If TARGET_JVM Then
                Dim argsArrUpBound As Integer = args.Length - 1

                'FIXME There's a bug in LateBinder that given (Integer(), Object) args brings SetValue(Object,Long()) method,
                ' resulting in invocation failure. The current workaround is always to cast indices to long.
                Dim longIndicesArr(argsArrUpBound - 1) As Long 'args contains (arr.Length - 1) indices + 1 object (value to set)

                For i As Integer = 0 To argsArrUpBound - 1
                    longIndicesArr(i) = CType(args(i), Long)
                Next

                Dim newArgs(1) As Object        'array containing 2 elements - the array of [long] indices and the value to set.
                newArgs(0) = args(argsArrUpBound)
                newArgs(1) = longIndicesArr

                realType.InvokeMember("SetValue", flags, LBinder, o, newArgs)
#Else
                realType.InvokeMember("Set", flags, Nothing, o, args, Nothing, Nothing, Nothing)
#End If
            Else
                Dim defaultMembers() As MemberInfo = realType.GetDefaultMembers()
                flags = BindingFlags.IgnoreCase Or BindingFlags.NonPublic Or BindingFlags.Public Or BindingFlags.Instance Or BindingFlags.SetProperty
                realType.InvokeMember(defaultMembers(0).Name, flags, Nothing, o, args, Nothing, Nothing, Nothing)
            End If
        End Sub

        <DebuggerStepThrough(), DebuggerHidden()> _
        Public Shared Sub LateIndexSetComplex(ByVal o As Object, ByVal args() As Object, ByVal paramnames() As String, ByVal OptimisticSet As Boolean, ByVal RValueBase As Boolean)
            'FIXME
            LateIndexSet(o, args, paramnames)
        End Sub

        <DebuggerStepThrough(), DebuggerHidden()> _
        Public Shared Sub LateSet(ByVal o As Object, ByVal objType As System.Type, ByVal name As String, ByVal args() As Object, ByVal paramnames() As String)
            Dim realType As System.Type = objType
            If realType Is Nothing Then
                realType = o.GetType()
            End If

            Dim flags As BindingFlags
            Try 'first try property set
                flags = BindingFlags.FlattenHierarchy Or _
                                           BindingFlags.SetField Or _
                                           BindingFlags.SetProperty Or _
                                           BindingFlags.IgnoreCase Or _
                                           BindingFlags.Instance Or _
                                           BindingFlags.NonPublic Or _
                                           BindingFlags.OptionalParamBinding Or _
                                           BindingFlags.Public Or _
                                           BindingFlags.Static

                realType.InvokeMember(name, flags, LBinder, o, args, Nothing, Nothing, paramnames)
                Return
            Catch e As MissingMemberException
            End Try

            'if failed, try method call
            flags = BindingFlags.FlattenHierarchy Or _
                    BindingFlags.GetField Or _
                    BindingFlags.GetProperty Or _
                    BindingFlags.IgnoreCase Or _
                    BindingFlags.Instance Or _
                    BindingFlags.InvokeMethod Or _
                    BindingFlags.NonPublic Or _
                    BindingFlags.OptionalParamBinding Or _
                    BindingFlags.Public Or _
                    BindingFlags.Static

            realType.InvokeMember(name, flags, LBinder, o, args, Nothing, Nothing, paramnames)
        End Sub

        <DebuggerStepThrough(), DebuggerHidden()> _
        Public Shared Sub LateSetComplex(ByVal o As Object, ByVal objType As System.Type, ByVal name As String, ByVal args() As Object, ByVal paramnames() As String, ByVal OptimisticSet As Boolean, ByVal RValueBase As Boolean)
            'FIXME
            Try
                LateSet(o, objType, name, args, paramnames)
            Catch e As Exception
                'suppress
            End Try
        End Sub

        <DebuggerStepThrough(), DebuggerHidden()> _
        Public Shared Function LateGet(ByVal o As Object, ByVal objType As System.Type, ByVal name As String, ByVal args() As Object, ByVal paramnames() As String, ByVal CopyBack() As Boolean) As Object
            Dim realType As System.Type = objType
            If realType Is Nothing Then
                realType = o.GetType()
            End If

            Dim flags As BindingFlags = BindingFlags.FlattenHierarchy Or _
                                        BindingFlags.GetField Or _
                                        BindingFlags.GetProperty Or _
                                        BindingFlags.IgnoreCase Or _
                                        BindingFlags.Instance Or _
                                        BindingFlags.InvokeMethod Or _
                                        BindingFlags.NonPublic Or _
                                        BindingFlags.OptionalParamBinding Or _
                                        BindingFlags.Public Or _
                                        BindingFlags.Static

            Try
                Dim lb As LateBinder = New LateBinder
                Dim result As Object = realType.InvokeMember(name, flags, lb, o, args, Nothing, CultureInfo.CurrentCulture, paramnames)

                If (lb.WasIncompleteInvocation) Then
                    result = lb.InvokeNext.Invoke(result, lb.InvokeNextArgs)
                End If
                Return result
            Catch e As MissingMethodException
                'FIXME: should the InvokeMember always call a binder instead of throwing an exceptions
                Throw New MissingMemberException
            End Try
        End Function

        <DebuggerStepThrough(), DebuggerHidden()> _
        Public Shared Function LateIndexGet(ByVal o As Object, ByVal args() As Object, ByVal paramnames() As String) As Object
            Dim realType As System.Type = o.GetType()
            Dim flags As BindingFlags
            If realType.IsArray Then
                flags = BindingFlags.IgnoreCase Or BindingFlags.NonPublic Or BindingFlags.Public Or BindingFlags.Instance Or BindingFlags.InvokeMethod
#If TARGET_JVM Then

                Dim indicesUpBound As Integer = args.Length - 1

                Dim longIndicesArr(indicesUpBound) As Long

                For i As Integer = 0 To indicesUpBound
                    longIndicesArr(i) = CType(args(i), Long)
                Next

                Dim newArgs(0) As Object            'array containing a single element - the array of [long] indices
                newArgs(0) = longIndicesArr

                Return realType.InvokeMember("GetValue", flags, LBinder, o, newArgs)
#Else
                Return realType.InvokeMember("Get", flags, New LateBinder, o, args, Nothing, Nothing, Nothing)
#End If
            Else
                Dim defaultMembers() As MemberInfo = realType.GetDefaultMembers()
                flags = BindingFlags.IgnoreCase Or BindingFlags.NonPublic Or BindingFlags.Public Or BindingFlags.Instance Or BindingFlags.InvokeMethod Or BindingFlags.GetProperty
                Return realType.InvokeMember(defaultMembers(0).Name, flags, New LateBinder, o, args, Nothing, Nothing, Nothing)
            End If
        End Function

        Private Sub New()

        End Sub
    End Class
End Namespace
