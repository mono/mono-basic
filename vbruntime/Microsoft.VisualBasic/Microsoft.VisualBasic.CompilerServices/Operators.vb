'
' Operators.vb
'
' Author:
'   Mizrahi Rafael (rafim@mainsoft.com)
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

#If NET_2_0 Then
Imports System
Namespace Microsoft.VisualBasic.CompilerServices
    Public Class Operators
        Public Shared Function AddObject(ByVal Left As Object, ByVal Right As Object) As Object
            Throw New NotImplementedException
        End Function
        Public Shared Function AndObject(ByVal Left As Object, ByVal Right As Object) As Object
            Throw New NotImplementedException
        End Function
        Public Shared Function CompareObject(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Integer
            Throw New NotImplementedException
        End Function
        Public Shared Function CompareObjectEqual(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Object
            Throw New NotImplementedException
        End Function
        Public Shared Function CompareObjectGreater(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Object
            Throw New NotImplementedException
        End Function
        Public Shared Function CompareObjectGreaterEqual(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Object
            Throw New NotImplementedException
        End Function
        Public Shared Function CompareObjectLess(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Object
            Throw New NotImplementedException
        End Function
        Public Shared Function CompareObjectLessEqual(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Object
            Throw New NotImplementedException
        End Function
        Public Shared Function CompareObjectNotEqual(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Object
            Throw New NotImplementedException
        End Function
        Public Shared Function CompareString(ByVal Left As String, ByVal Right As String, ByVal TextCompare As Boolean) As Integer
            If TextCompare Then
                Return Left.CompareTo(Right)
            Else
                Return String.CompareOrdinal(Left, Right)
            End If
        End Function
        Public Shared Function ConcatenateObject(ByVal Left As Object, ByVal Right As Object) As Object
            Throw New NotImplementedException
        End Function
        Public Shared Function ConditionalCompareObjectEqual(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Boolean
            Throw New NotImplementedException
        End Function
        Public Shared Function ConditionalCompareObjectGreater(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Boolean
            Throw New NotImplementedException
        End Function
        Public Shared Function ConditionalCompareObjectGreaterEqual(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Boolean
            Throw New NotImplementedException
        End Function
        Public Shared Function ConditionalCompareObjectLess(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Boolean
            Throw New NotImplementedException
        End Function
        Public Shared Function ConditionalCompareObjectLessEqual(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Boolean
            Throw New NotImplementedException
        End Function
        Public Shared Function ConditionalCompareObjectNotEqual(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Boolean
            Throw New NotImplementedException
        End Function
        Public Shared Function DivideObject(ByVal Left As Object, ByVal Right As Object) As Object
            Throw New NotImplementedException
        End Function
        Public Shared Function ExponentObject(ByVal Left As Object, ByVal Right As Object) As Object
            Throw New NotImplementedException
        End Function
        Public Shared Function IntDivideObject(ByVal Left As Object, ByVal Right As Object) As Object
            Throw New NotImplementedException
        End Function
        Public Shared Function LeftShiftObject(ByVal Operand As Object, ByVal Amount As Object) As Object
            Throw New NotImplementedException
        End Function
        Public Shared Function LikeObject(ByVal Source As Object, ByVal Pattern As Object, ByVal CompareOption As CompareMethod) As Object
            Throw New NotImplementedException
        End Function
        Public Shared Function LikeString(ByVal Source As String, ByVal Pattern As String, ByVal CompareOption As CompareMethod) As Boolean
            Throw New NotImplementedException
        End Function
        Public Shared Function ModObject(ByVal Left As Object, ByVal Right As Object) As Object
            Throw New NotImplementedException
        End Function
        Public Shared Function MultiplyObject(ByVal Left As Object, ByVal Right As Object) As Object
            Throw New NotImplementedException
        End Function
        Public Shared Function NegateObject(ByVal Operand As Object) As Object
            Throw New NotImplementedException
        End Function
        Public Shared Function NotObject(ByVal Operand As Object) As Object
            Throw New NotImplementedException
        End Function
        Public Shared Function OrObject(ByVal Left As Object, ByVal Right As Object) As Object
            Throw New NotImplementedException
        End Function
        Public Shared Function PlusObject(ByVal Operand As Object) As Object
            Throw New NotImplementedException
        End Function
        Public Shared Function RightShiftObject(ByVal Operand As Object, ByVal Amount As Object) As Object
            Throw New NotImplementedException
        End Function
        Public Shared Function SubtractObject(ByVal Left As Object, ByVal Right As Object) As Object
            Throw New NotImplementedException
        End Function
        Public Shared Function XorObject(ByVal Left As Object, ByVal Right As Object) As Object
            Throw New NotImplementedException
        End Function
    End Class
End Namespace
#End If