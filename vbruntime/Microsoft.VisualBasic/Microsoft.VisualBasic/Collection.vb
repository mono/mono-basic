'
' Collection.vb
'
' Author:
'   Chris J Breisch (cjbreisch@altavista.net) 
'   Mizrahi Rafael (rafim@mainsoft.com)
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
'
Imports System
Imports System.Runtime.InteropServices
Imports System.Collections
Imports System.ComponentModel
Imports System.Runtime.Serialization
Imports System.Reflection

Namespace Microsoft.VisualBasic
    <DebuggerTypeProxy(GetType(Collection.CollectionDebugView))> _
    <Serializable()> _
    <DebuggerDisplay("Count = {Count}")> _
    Public NotInheritable Class Collection
        Implements ICollection
        Implements IList
        Implements ISerializable
        Implements IDeserializationCallback

#If Moonlight Then
        Implements IEnumerable
#End If
        ' Declarations
        Private m_Hashtable As Hashtable = New Hashtable
        Private m_HashIndexers As ArrayList = New ArrayList
        Private m_KeysCount As Integer = Integer.MinValue
        Friend Modified As Boolean = False

        Private Class ColEnumerator
            Implements IEnumerator

            Private currentKey As Object
            Private afterLast As Boolean = False
            Private m_col As Collection
            Private m_Current As Object

            Public Sub New(ByRef coll As Collection)
                m_col = coll
                currentKey = Nothing
            End Sub

            Public Sub Reset() Implements System.Collections.IEnumerator.Reset
                If (m_col.Modified) Then
                    'LAMESPEC: spec says throw exception, MS doesn't
                    'throw new InvalidOperationException();
                End If
                currentKey = Nothing
                afterLast = False
            End Sub

            Public Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext
                If (m_col.Modified) Then
                    'LAMESPEC: spec says throw exception, MS doesn't
                    'throw new InvalidOperationException();
                End If

                If currentKey Is Nothing And m_col.Count > 0 Then
                    currentKey = m_col.m_HashIndexers(0)
                    m_Current = CurrentInternal
                    Return True
                End If

                If afterLast Then
                    m_Current = Nothing
                    Return False
                End If

                Dim index As Integer = m_col.m_HashIndexers.IndexOf(currentKey)
                If index >= m_col.Count - 1 Then
                    afterLast = True
                    m_Current = Nothing
                    Return False
                End If

                currentKey = m_col.m_HashIndexers(index + 1)
                afterLast = False

                m_Current = CurrentInternal
                Return True
            End Function

            Public ReadOnly Property Current() As Object Implements System.Collections.IEnumerator.Current
                Get
                    Return m_Current
                End Get
            End Property

            Private ReadOnly Property CurrentInternal() As Object
                Get
                    Dim index As Integer = m_col.m_HashIndexers.IndexOf(currentKey)
                    If index > m_col.Count - 1 Then
                        Return Nothing
                    Else
                        If afterLast Then
                            If MoveNext() Then
                                Dim tmo As ColEnumerator = Me
                                Return tmo.Current()
                            Else
                                Return Nothing
                            End If
                        End If
                        Return m_col(index + 1)
                    End If
                End Get
            End Property

        End Class
        ' Constructors
        ' Properties
        Private ReadOnly Property IsReadOnly() As Boolean Implements System.Collections.IList.IsReadOnly
            Get
                Return False
            End Get
        End Property
        Private ReadOnly Property IsSynchronized() As Boolean Implements System.Collections.ICollection.IsSynchronized
            Get
                Return m_Hashtable.IsSynchronized
            End Get
        End Property

        Private ReadOnly Property SyncRoot() As Object Implements System.Collections.ICollection.SyncRoot
            Get
                Return m_Hashtable.SyncRoot
            End Get
        End Property

        Private ReadOnly Property IsFixedSize() As Boolean Implements System.Collections.IList.IsFixedSize
            Get
                Return False
            End Get
        End Property

        Public ReadOnly Property Count() As Integer
            Get
                Return IList_Count
            End Get
        End Property

        Private ReadOnly Property IList_Count() As Integer Implements System.Collections.IList.Count
            Get
                Return m_HashIndexers.Count
            End Get
        End Property

        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Default Public Overloads ReadOnly Property Item(ByVal Index As Object) As Object
            Get
                If Index Is Nothing Then Throw New IndexOutOfRangeException("Argument 'Index' is not a valid index.")

                If TypeOf Index Is Integer Then
                    Return Item(DirectCast(Index, Integer))
                Else
                    Dim idx As Integer = m_HashIndexers.IndexOf(Index)
                    If idx = -1 Then
                        Throw New ArgumentException("Argument 'Index' is not a valid value.")
                    End If
                    Return Item(idx + 1)
                End If
            End Get
        End Property

        Default Public Overloads ReadOnly Property Item(ByVal Index As Integer) As Object
            Get
                'The behaviour of Collection.Item is NOT the same as the IList.Item interface implementation.
                Index = Index - 1

                If Index > Count - 1 Or Index < 0 Then
                    Throw New IndexOutOfRangeException("Collection1 index must be in the range 1 to the size of the collection.")
                End If

                Return m_Hashtable(m_HashIndexers(Index))
            End Get
        End Property

        Default Public Overloads ReadOnly Property Item(ByVal Key As String) As Object
            Get
                Return Item(CObj(Key))
            End Get
        End Property

        Private Property IList_Item(ByVal index As Integer) As Object Implements System.Collections.IList.Item
            Get
                If index < 0 AndAlso Count > 0 Then
                    'Oh man this behaviour is weird...
                    index = 0
                End If

                If index > Count - 1 Or index < 0 Then
                    Throw New ArgumentOutOfRangeException("Collection1 index must be in the range 1 to the size of the collection.")
                End If

                Return m_Hashtable(m_HashIndexers(index))

            End Get
            Set(ByVal Value As Object)
                If index < 0 AndAlso Count > 0 Then
                    'Oh man this behaviour is weird...
                    index = 0
                End If

                If index > Count Or index < 0 Then
                    Throw New ArgumentOutOfRangeException("Index")
                End If

                If index = -1 Then
                    m_Hashtable(m_HashIndexers(0)) = Value
                Else
                    m_Hashtable(m_HashIndexers(index)) = Value
                End If
            End Set
        End Property

        Friend Function IndexOf(ByVal value As Object) As Integer Implements System.Collections.IList.IndexOf

            Dim index As Integer = -1

            Dim enTry As DictionaryEntry
            For Each enTry In m_Hashtable
                If enTry.Value Is value Then
                    index = m_HashIndexers.IndexOf(enTry.Key)
                    Exit For
                End If

                ' also allow value comparison to work for types that do not 
                ' override equality operator
                If enTry.Value.GetType() Is value.GetType() Then
                    If Object.Equals(enTry.Value, value) Then
                        index = m_HashIndexers.IndexOf(enTry.Key)
                        Exit For
                    End If
                End If
            Next

            Return index

        End Function

        Public Function Contains(ByVal Key As String) As Boolean
            Return m_Hashtable.ContainsKey(Key)
        End Function

        Private Function IListContains(ByVal value As Object) As Boolean Implements System.Collections.IList.Contains
            Return (CType(Me, IList)).IndexOf(value) <> -1
        End Function

        Public Sub Clear()
            m_Hashtable.Clear()
            m_HashIndexers.Clear()
            m_KeysCount = Integer.MinValue
        End Sub

        Private Sub IList_Clear() Implements System.Collections.IList.Clear
            Clear()
        End Sub

        Public Overloads Sub Remove(ByVal Key As String)

            If m_Hashtable.ContainsKey(Key) Then
                m_Hashtable.Remove(Key)
                m_HashIndexers.Remove(Key)
                Modified = True
            Else
                Throw New ArgumentException("Argument 'Key' is not a valid value.")
            End If

        End Sub

        Public Overloads Sub Remove(ByVal Index As Integer)

            Try
                ' Collections are 1-based
                m_Hashtable.Remove(m_HashIndexers(Index - 1))
                m_HashIndexers.RemoveAt(Index - 1)
                Modified = True
            Catch e As ArgumentOutOfRangeException
                Throw New IndexOutOfRangeException("Collection1 index must be in the range 1 to the size of the collection.")
            End Try
        End Sub

        Private Overloads Sub Remove(ByVal value As Object) Implements System.Collections.IList.Remove
            'FIXME: .Net behaviour is unstable
            Dim index As Integer = (CType(Me, IList)).IndexOf(value)
            If index <> -1 Then
                Remove(index + 1)
            End If
        End Sub

        Private Sub RemoveAt(ByVal index As Integer) Implements System.Collections.IList.RemoveAt
            If index + 1 > Count Or (index = -1 And Count = 0) Then
                Throw New ArgumentOutOfRangeException("Index")
            End If

            If index = -1 Then
                Remove(1)
            Else
                Remove(index + 1)
            End If
        End Sub

        Private Sub Insert(ByVal index As Integer, ByVal value As Object) Implements System.Collections.IList.Insert
            If index < 0 Then
                Throw New ArgumentOutOfRangeException
            End If

            If index + 1 > Count + 2 Then
                Throw New ArgumentOutOfRangeException
            End If

            If index + 2 >= Count Then
                Add(value)
            Else
                Insert(index + 2, value, GetNextKey(value))
            End If

        End Sub

        Private Sub Insert(ByVal index As Integer, ByVal value As Object, ByVal Key As String)
            m_HashIndexers.Insert(index - 1, Key)
            m_Hashtable.Add(Key, value)
            Modified = True
        End Sub

        Private Function IList_Add(ByVal value As Object) As Integer Implements System.Collections.IList.Add
            Return AddByKey(value, GetNextKey(value))
        End Function

        Private Function AddByKey(ByVal Item As Object, ByVal Key As String) As Integer
            m_Hashtable.Add(Key, Item)
            Modified = True

            Return m_HashIndexers.Add(Key)
        End Function

        Public Sub Add(ByVal Item As Object, _
                        Optional ByVal Key As String = Nothing, _
                        Optional ByVal Before As Object = Nothing, _
                        Optional ByVal After As Object = Nothing)

            Dim Position As Integer = Integer.MinValue

            ' check for valid args
            If (Not Before Is Nothing) And (Not After Is Nothing) Then
                Throw New ArgumentException("'Before' and 'After' arguments cannot be combined.")
            End If
            If Not Key Is Nothing And m_HashIndexers.IndexOf(Key) <> -1 Then
                Throw New ArgumentException
            End If
            If Not Before Is Nothing Then
                ' Looks like its an implementation bug in .NET
                ' Not very satisfied with the fix, but did it
                ' just to bring the similar behaviour on mono
                ' as well.
                If TypeOf Before Is Integer Then
                    Position = Convert.ToInt32(Before)
                    If Position <> (m_HashIndexers.Count + 1) Then
                        Position = GetIndexPosition(Before)
                    End If
                Else
                    Position = GetIndexPosition(Before)
                End If
            End If
            If Not After Is Nothing Then
                Position = GetIndexPosition(After) + 1
            End If
            If Key Is Nothing Then
                Key = GetNextKey(Item)
            End If

            If Position > (m_HashIndexers.Count + 1) Or Position = Integer.MinValue Then
                AddByKey(Item, Key)
            Else
                Insert(Position, Item, Key)
            End If
        End Sub

        Private Function GetNextKey(ByVal value As Object) As String
            m_KeysCount = m_KeysCount + 1
            Dim key As String
            If value Is Nothing Then
                key = "Nothing"
            Else
                key = value.ToString()
            End If
            Return (key + m_KeysCount.ToString())
        End Function

        Private Function GetIndexPosition(ByVal Item As Object) As Integer
            Dim Position As Integer = Integer.MinValue

            If TypeOf Item Is String Then
                Position = m_HashIndexers.IndexOf(Item) + 1
            ElseIf TypeOf Item Is Integer Then
                Position = Convert.ToInt32(Item)
            Else
                Throw New InvalidCastException
            End If
            If Position < 0 Then
                Throw New ArgumentOutOfRangeException("Specified argument was out of the range of valid values.")
            End If

            'Position must be from 1 to value of collections Count 
            If Position > m_HashIndexers.Count Then
                Throw New ArgumentOutOfRangeException("Specified argument was out of the range of valid values.")
            End If

            Return Position

        End Function

        Private Sub CopyTo(ByVal array As System.Array, ByVal index As Integer) Implements System.Collections.ICollection.CopyTo

            If array Is Nothing Then
                Throw New ArgumentNullException
            End If

            If index < 0 Then
                Throw New ArgumentOutOfRangeException
            End If

            If array.Rank > 1 Or index >= array.Length Or Count > (array.Length - index) Then
                Throw New ArgumentException
            End If

            'Dim NewArray As System.Array = array.CreateInstance(Type.GetType("System.Object"), m_HashIndexers.Count - index)

            ' Collections are 1-based
            For i As Integer = 0 To m_HashIndexers.Count - 1
                array.SetValue(m_Hashtable(m_HashIndexers(i)), i + index)
            Next
        End Sub

        Private Function IEnumerable_GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return New ColEnumerator(Me)
        End Function

        Public Function GetEnumerator() As System.Collections.IEnumerator
            Return IEnumerable_GetEnumerator()
        End Function

        Private Sub GetObjectData(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext) Implements System.Runtime.Serialization.ISerializable.GetObjectData
            Throw New NotImplementedException
        End Sub

        Private Sub OnDeserialization(ByVal sender As Object) Implements System.Runtime.Serialization.IDeserializationCallback.OnDeserialization
            Throw New NotImplementedException
        End Sub

        Friend Class CollectionDebugView
            'If you want to view Collection classes in VS, implement me
        End Class
    End Class
End Namespace
