'
' Utils.vb
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

Imports System
Imports System.Runtime.InteropServices
Imports System.Resources

Namespace Microsoft.VisualBasic.CompilerServices
    <System.ComponentModel.EditorBrowsable(ComponentModel.EditorBrowsableState.Never)> _
    Public NotInheritable Class Utils

        Private Shared m_Resources As ResourceManager

        Private Sub New()
            'Nobody should see constructor
        End Sub

#If Moonlight = False Then
        Friend Shared Function Array_GetLength(ByVal array As System.Array) As Long
            Return array.LongLength
        End Function

        Friend Shared Function Array_GetLength(ByVal array As System.Array, ByVal dimension As Integer) As Long
            Return array.GetLongLength(dimension)
        End Function

        Friend Shared Sub Array_Copy(ByVal sourceArray As System.Array, ByVal sourceIndex As Long, ByVal destinationArray As System.Array, ByVal destinationIndex As Long, ByVal length As Long)
            System.Array.Copy(sourceArray, sourceIndex, destinationArray, destinationIndex, length)
        End Sub

#Else
        Friend Shared Function Array_GetLength (array As System.Array) As Integer
            Return array.Length
        End Function

        Friend Shared Function Array_GetLength(ByVal array As System.Array, ByVal dimension As Integer) As Integer
            Return array.GetLength(dimension)
        End Function

        Friend Shared Sub Array_Copy(ByVal sourceArray As System.Array, ByVal sourceIndex As Integer, ByVal destinationArray As System.Array, ByVal destinationIndex As Integer, ByVal length As Integer)
            System.Array.Copy(sourceArray, sourceIndex, destinationArray, destinationIndex, length)
        End Sub
#End If

        Public Shared Function CopyArray(ByVal arySrc As System.Array, ByVal aryDest As System.Array) As System.Array

            If arySrc Is Nothing Then
#If TRACE Then
                Console.WriteLine("TRACE:Utils.CopyArray:arySrc is Nothing")
#End If
                Return aryDest
            End If

            If arySrc.Rank <> aryDest.Rank Then
                Throw New InvalidCastException("'ReDim' cannot change the number of dimensions.")
            End If

            Dim lastRank As Integer
            Dim destLength As Integer
            Dim srcLength As Integer
            Dim lastLength As Integer
            Dim copies As Long

            lastRank = arySrc.Rank - 1
            destLength = aryDest.GetUpperBound(lastRank) + 1
            srcLength = arySrc.GetUpperBound(lastRank) + 1

            'Check that all but the last dimension have the same length
            For i As Integer = 0 To lastRank - 1
                If Array_GetLength(arySrc, i) <> Array_GetLength(aryDest, i) Then
                    Throw New InvalidCastException("'ReDim' can only change the rightmost dimension.")
                End If
            Next

            If destLength = srcLength Then
                'All dimensions have the same size, copy the entire array
                Array.Copy(arySrc, aryDest, Array_GetLength(arySrc))
                Return aryDest
            End If

            lastLength = Math.Min(destLength, srcLength)

            If lastRank = 0 Then
                'There's only one dimension, copy the length
                Array.Copy(arySrc, aryDest, lastLength)
                Return aryDest
            End If

            copies = Array_GetLength(arySrc) \ srcLength

            For i As Long = 0 To copies - 1
                Array_Copy(arySrc, i * srcLength, aryDest, i * destLength, lastLength)
            Next

            Return aryDest
        End Function
        Public Shared Function MethodToString(ByVal Method As System.Reflection.MethodBase) As String
            Throw New NotImplementedException
        End Function
        Public Shared Function SetCultureInfo(ByVal Culture As System.Globalization.CultureInfo) As Object
            Throw New NotImplementedException
        End Function
        Public Shared Sub ThrowException(ByVal hr As Integer)
            Throw New NotImplementedException
        End Sub

        Public Shared Function GetResourceString(ByVal ResourceKey As String, ByVal ParamArray Args As String()) As String
            Dim result As String

            Try
                result = String.Format(GetResourceString(ResourceKey), Args)
            Catch ex As Exception
                result = ResourceKey
            End Try

            Return result
        End Function

        Friend Shared Function GetResourceString(ByVal Name As String) As String
            Try
                If m_Resources Is Nothing Then
                    m_Resources = New Resources.ResourceManager("strings", System.Reflection.Assembly.GetExecutingAssembly())
                End If
                Return m_Resources.GetString(Name)
            Catch ex As Exception
                Return "Error message not available."
            End Try
        End Function
    End Class

End Namespace
