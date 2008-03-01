'
' FileData.vb
'
' Author:
'   Rolf Bjarne Kvinge (RKvinge@novell.com)
'
' Copyright (C) 2007 Novell, Inc (http://www.novell.com)
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
Imports System.IO
Imports System.Text
Imports Microsoft.VisualBasic.CompilerServices
Imports System.Collections

Namespace Microsoft.VisualBasic
    Friend Class FileData
        Public FileNumber As Integer
        Public FileName As String
        Public Mode As Microsoft.VisualBasic.OpenMode
        Public Access As Microsoft.VisualBasic.OpenAccess
        Public Share As Microsoft.VisualBasic.OpenShare
        Public RecordLength As Integer = -1
        Public RecordWidth As Integer = 0
        Public Stream As FileStream
        Public Reader As StreamReader
        Public Writer As StreamWriter
        Public BinaryWriter As BinaryWriter
        Public BinaryReader As BinaryReader

        Public Sub New(ByVal FileNumber As Integer, ByVal FileName As String, ByVal Mode As Microsoft.VisualBasic.OpenMode, ByVal Access As Microsoft.VisualBasic.OpenAccess, ByVal Share As Microsoft.VisualBasic.OpenShare, ByVal RecordLength As Integer)
            Me.FileNumber = FileNumber
            Me.FileName = FileName
            Me.Mode = Mode
            Me.Access = Access
            Me.Share = Share
            Me.RecordLength = RecordLength
            If Me.Access = OpenAccess.Read OrElse Me.Access = OpenAccess.Write Then
                Reader = New StreamReader(Stream)
                BinaryReader = New BinaryReader(Stream)
            End If
            If Me.Access = OpenAccess.Write OrElse Me.Access = OpenAccess.ReadWrite Then
                Writer = New StreamWriter(Stream)
                Writer.AutoFlush = True
                BinaryWriter = New BinaryWriter(Stream)
            End If
        End Sub

        Public Sub CreateStream()
            If Access = OpenAccess.Default Then Access = OpenAccess.ReadWrite
            If Share = OpenShare.Default Then Share = OpenShare.LockReadWrite

            Dim ioAccess As System.IO.FileAccess
            Dim ioShare As System.IO.FileShare
            Dim ioMode As System.IO.FileMode

            Select Case Access
                Case OpenAccess.Read
                    ioAccess = FileAccess.Read
                Case OpenAccess.ReadWrite
                    ioAccess = FileAccess.ReadWrite
                Case OpenAccess.Write
                    ioAccess = FileAccess.Write
                Case Else
                    Throw Microsoft.VisualBasic.CompilerServices.ExceptionUtils.GetVBException(VBErrors.ERR5_Invalid_procedure_call)
            End Select

            Select Case Mode
                Case OpenMode.Append
                    ioMode = FileMode.Append
                Case OpenMode.Binary
                    ioMode = FileMode.OpenOrCreate
                Case OpenMode.Input
                    ioMode = FileMode.Open
                Case OpenMode.Output
                    ioMode = FileMode.OpenOrCreate
                Case OpenMode.Random
                    ioMode = FileMode.OpenOrCreate
                Case Else
                    Throw Microsoft.VisualBasic.CompilerServices.ExceptionUtils.GetVBException(VBErrors.ERR5_Invalid_procedure_call)
            End Select

            Select Case Share
                Case OpenShare.LockRead
                    ioShare = FileShare.Write
                Case OpenShare.LockReadWrite
                    ioShare = FileShare.None
                Case OpenShare.LockWrite
                    ioShare = FileShare.Read
                Case OpenShare.Shared
                    ioShare = FileShare.ReadWrite
                Case Else
                    Throw Microsoft.VisualBasic.CompilerServices.ExceptionUtils.GetVBException(VBErrors.ERR5_Invalid_procedure_call)
            End Select

            Stream = New IO.FileStream(FileName, ioMode, ioAccess, ioShare)
        End Sub

        Public Sub Close()
            Stream.Close()
            Stream = Nothing
        End Sub

        Public Sub VerifyFileModes(ByVal ParamArray Modes As OpenMode())
            For i As Integer = 0 To Modes.Length - 1
                If Modes(i) = Mode Then Return
            Next
            Throw Microsoft.VisualBasic.CompilerServices.ExceptionUtils.GetVBException(VBErrors.ERR54_Bad_file_mode)
        End Sub

        Public Sub VerifyFileAccess(ByVal ParamArray Accesses As OpenAccess())
            For i As Integer = 0 To Accesses.Length - 1
                If Accesses(i) = Access Then Return
            Next
            Throw New ArgumentException("Invalid access.") 'TODO: Find correct error message
        End Sub

        Public Function EOF() As Boolean
            VerifyFileModes(OpenMode.Input, OpenMode.Random, OpenMode.Random)
            Return Stream.Length = Stream.Position
        End Function

        Public Sub FileGet(ByRef Value As Boolean, Optional ByVal RecordNumber As Long = -1)
            VerifyFileModes(OpenMode.Random, OpenMode.Binary)
            If RecordNumber >= 0 Then Seek(RecordNumber)
            Value = BinaryReader.ReadBoolean
        End Sub

        Public Sub FileGet(ByRef Value As Byte, Optional ByVal RecordNumber As Long = -1)
            VerifyFileModes(OpenMode.Random, OpenMode.Binary)
            If RecordNumber >= 0 Then Seek(RecordNumber)
            Value = BinaryReader.ReadByte
        End Sub

        Public Sub FileGet(ByRef Value As Char, Optional ByVal RecordNumber As Long = -1)
            VerifyFileModes(OpenMode.Random, OpenMode.Binary)
            If RecordNumber >= 0 Then Seek(RecordNumber)
            Value = BinaryReader.ReadChar
        End Sub

        Public Sub FileGet(ByRef Value As Date, Optional ByVal RecordNumber As Long = -1)
            VerifyFileModes(OpenMode.Random, OpenMode.Binary)
            If RecordNumber >= 0 Then Seek(RecordNumber)
            Value = Date.FromOADate(BinaryReader.ReadDouble)
        End Sub

        Public Sub FileGet(ByRef Value As Decimal, Optional ByVal RecordNumber As Long = -1)
            VerifyFileModes(OpenMode.Random, OpenMode.Binary)
            If RecordNumber >= 0 Then Seek(RecordNumber)
            Value = BinaryReader.ReadDecimal
        End Sub

        Public Sub FileGet(ByRef Value As Double, Optional ByVal RecordNumber As Long = -1)
            VerifyFileModes(OpenMode.Random, OpenMode.Binary)
            If RecordNumber >= 0 Then Seek(RecordNumber)
            Value = BinaryReader.ReadDouble
        End Sub

        Public Sub FileGet(ByRef Value As Integer, Optional ByVal RecordNumber As Long = -1)
            VerifyFileModes(OpenMode.Random, OpenMode.Binary)
            If RecordNumber >= 0 Then Seek(RecordNumber)
            Value = BinaryReader.ReadInt32
        End Sub

        Public Sub FileGet(ByRef Value As Long, Optional ByVal RecordNumber As Long = -1)
            VerifyFileModes(OpenMode.Random, OpenMode.Binary)
            If RecordNumber >= 0 Then Seek(RecordNumber)
            Value = BinaryReader.ReadInt64
        End Sub

        Public Sub FileGet(ByRef Value As Short, Optional ByVal RecordNumber As Long = -1)
            VerifyFileModes(OpenMode.Random, OpenMode.Binary)
            If RecordNumber >= 0 Then Seek(RecordNumber)
            Value = BinaryReader.ReadInt16
        End Sub

        Public Sub FileGet(ByRef Value As Single, Optional ByVal RecordNumber As Long = -1)
            VerifyFileModes(OpenMode.Random, OpenMode.Binary)
            If RecordNumber >= 0 Then Seek(RecordNumber)
            Value = BinaryReader.ReadSingle
        End Sub

        Public Sub FileGet(ByRef Value As String, Optional ByVal RecordNumber As Long = -1, Optional ByVal StringIsFixedLength As Boolean = False)
            VerifyFileModes(OpenMode.Random, OpenMode.Binary)
            If RecordNumber >= 0 Then Seek(RecordNumber)

            Dim length As Integer
            If StringIsFixedLength = False Then
                length = BinaryReader.ReadInt16()
            ElseIf Not Value Is Nothing Then
                length = Value.Length
            Else
                length = 0
            End If
            Value = New String(BinaryReader.ReadChars(length))
        End Sub

        Public Sub FileGet(ByRef Value As System.Array, Optional ByVal RecordNumber As Long = -1, Optional ByVal ArrayIsDynamic As Boolean = False, Optional ByVal StringIsFixedLength As Boolean = False)
            Throw New NotImplementedException
        End Sub

        Public Sub FileGet(ByRef Value As System.ValueType, Optional ByVal RecordNumber As Long = -1)
            Throw New NotImplementedException
        End Sub

        Public Sub FileGetObject(ByRef Value As Object, Optional ByVal RecordNumber As Long = -1)
            Throw New NotImplementedException
        End Sub

        Public Sub FilePut(ByVal Value As Boolean, Optional ByVal RecordNumber As Long = -1)
            VerifyFileModes(OpenMode.Random, OpenMode.Binary)
            If RecordNumber >= 0 Then Seek(RecordNumber)
            BinaryWriter.Write(Value)
        End Sub

        Public Sub FilePut(ByVal Value As Byte, Optional ByVal RecordNumber As Long = -1)
            VerifyFileModes(OpenMode.Random, OpenMode.Binary)
            If RecordNumber >= 0 Then Seek(RecordNumber)
            BinaryWriter.Write(Value)
        End Sub

        Public Sub FilePut(ByVal Value As Char, Optional ByVal RecordNumber As Long = -1)
            VerifyFileModes(OpenMode.Random, OpenMode.Binary)
            If RecordNumber >= 0 Then Seek(RecordNumber)
            BinaryWriter.Write(Value)
        End Sub

        Public Sub FilePut(ByVal Value As Date, Optional ByVal RecordNumber As Long = -1)
            VerifyFileModes(OpenMode.Random, OpenMode.Binary)
            If RecordNumber >= 0 Then Seek(RecordNumber)
            BinaryWriter.Write(Value.ToOADate)
        End Sub

        Public Sub FilePut(ByVal Value As Decimal, Optional ByVal RecordNumber As Long = -1)
            VerifyFileModes(OpenMode.Random, OpenMode.Binary)
            If RecordNumber >= 0 Then Seek(RecordNumber)
            BinaryWriter.Write(Value)
        End Sub

        Public Sub FilePut(ByVal Value As Double, Optional ByVal RecordNumber As Long = -1)
            VerifyFileModes(OpenMode.Random, OpenMode.Binary)
            If RecordNumber >= 0 Then Seek(RecordNumber)
            BinaryWriter.Write(Value)
        End Sub

        Public Sub FilePut(ByVal Value As Integer, Optional ByVal RecordNumber As Long = -1)
            VerifyFileModes(OpenMode.Random, OpenMode.Binary)
            If RecordNumber >= 0 Then Seek(RecordNumber)
            BinaryWriter.Write(Value)
        End Sub

        Public Sub FilePut(ByVal Value As Long, Optional ByVal RecordNumber As Long = -1)
            VerifyFileModes(OpenMode.Random, OpenMode.Binary)
            If RecordNumber >= 0 Then Seek(RecordNumber)
            BinaryWriter.Write(Value)
        End Sub

        Public Sub FilePut(ByVal Value As Short, Optional ByVal RecordNumber As Long = -1)
            VerifyFileModes(OpenMode.Random, OpenMode.Binary)
            If RecordNumber >= 0 Then Seek(RecordNumber)
            BinaryWriter.Write(Value)
        End Sub

        Public Sub FilePut(ByVal Value As Single, Optional ByVal RecordNumber As Long = -1)
            VerifyFileModes(OpenMode.Random, OpenMode.Binary)
            If RecordNumber >= 0 Then Seek(RecordNumber)
            BinaryWriter.Write(Value)
        End Sub

        Public Sub FilePut(ByVal Value As String, Optional ByVal RecordNumber As Long = -1, Optional ByVal StringIsFixedLength As Boolean = False)
            VerifyFileModes(OpenMode.Random, OpenMode.Binary)
            If RecordNumber >= 0 Then Seek(RecordNumber)

            If StringIsFixedLength = False Then
                If Value.Length > Short.MaxValue Then
                    Throw New ArgumentException("String is too long") 'FIXME: Check error condition and message
                End If
                BinaryWriter.Write(CShort(Value.Length))
            End If
            Writer.Write(Value) 'Binary writer writes a length-prefixed string (and the length-prefix doesn't match VB's length prefix)
        End Sub

        Public Sub FilePut(ByVal Value As System.Array, Optional ByVal RecordNumber As Long = -1, Optional ByVal ArrayIsDynamic As Boolean = False, Optional ByVal StringIsFixedLength As Boolean = False)
            Throw New NotImplementedException
        End Sub

        Public Sub FilePut(ByVal Value As System.ValueType, Optional ByVal RecordNumber As Long = -1)
            Throw New NotImplementedException
        End Sub

        Public Sub FilePutObject(ByVal Value As Object, Optional ByVal RecordNumber As Long = -1)
            Throw New NotImplementedException
        End Sub

        Public Sub FileWidth(ByVal RecordWidth As Integer)
            Me.RecordWidth = RecordWidth
        End Sub

        Private Function ReadRecord() As String
            Dim ch As Char
            Dim builder As New StringBuilder

            ch = BinaryReader.ReadChar()
            While ch <> ","c AndAlso ch <> vbCr AndAlso ch <> vbLf
                builder.Append(ch)
                ch = BinaryReader.ReadChar()
            End While

            If ch = vbCr AndAlso Chr(BinaryReader.PeekChar) = vbLf Then
                BinaryReader.ReadChar()
            End If

            Return builder.ToString
        End Function

        Public Sub Input(ByRef Value As Boolean)
            VerifyFileAccess(OpenAccess.Read, OpenAccess.ReadWrite)
            Dim record As String = ReadRecord()
            If String.Equals("#TRUE#", record, StringComparison.Ordinal) Then
                Value = True
            ElseIf String.Equals("#FALSE#", record, StringComparison.Ordinal) Then
                Value = False
            Else
                Throw New ArgumentException("Invalid boolean value.")
            End If
        End Sub

        Public Sub Input(ByRef Value As Byte)
            VerifyFileAccess(OpenAccess.Read, OpenAccess.ReadWrite)
            Value = CByte(ReadRecord())
        End Sub

        Public Sub Input(ByRef Value As Char)
            VerifyFileAccess(OpenAccess.Read, OpenAccess.ReadWrite)
            Value = CChar(ReadRecord())
        End Sub

        Public Sub Input(ByRef Value As Date)
            VerifyFileAccess(OpenAccess.Read, OpenAccess.ReadWrite)
            Value = CDate(ReadRecord())
        End Sub

        Public Sub Input(ByRef Value As Decimal)
            VerifyFileAccess(OpenAccess.Read, OpenAccess.ReadWrite)
            Value = CDec(ReadRecord())
        End Sub

        Public Sub Input(ByRef Value As Double)
            VerifyFileAccess(OpenAccess.Read, OpenAccess.ReadWrite)
            Value = CDbl(ReadRecord())
        End Sub

        Public Sub Input(ByRef Value As Integer)
            VerifyFileAccess(OpenAccess.Read, OpenAccess.ReadWrite)
            Value = CInt(ReadRecord())
        End Sub

        Public Sub Input(ByRef Value As Long)
            VerifyFileAccess(OpenAccess.Read, OpenAccess.ReadWrite)
            Value = CLng(ReadRecord())
        End Sub

        Public Sub Input(ByRef Value As Object)
            VerifyFileAccess(OpenAccess.Read, OpenAccess.ReadWrite)
            Dim record As String = ReadRecord()
            If record Is Nothing Then
                Value = Nothing
            ElseIf String.Equals("#FALSE#", record, StringComparison.Ordinal) Then
                Value = False
            ElseIf String.Equals("#TRUE#", record, StringComparison.Ordinal) Then
                Value = True
            ElseIf String.Equals("#NULL#", record, StringComparison.Ordinal) Then
                Value = DBNull.Value
            ElseIf record.StartsWith("#ERROR ") AndAlso record.EndsWith("#") Then
                record = record.Substring(7, record.Length - 8)
                Dim errNumber As Integer = Integer.Parse(record)
                Dim errObj As New ErrObject
                errObj.Number = errNumber
                Value = errObj
            ElseIf record.StartsWith("#") AndAlso record.EndsWith("#") Then
                record = record.Substring(1, record.Length - 1)
                Value = Date.Parse(record)
            ElseIf record.StartsWith("""") AndAlso record.EndsWith("""") Then
                Value = record.Substring(1, record.Length - 1)
            Else
                'FIXME: Do some real parsing here
                ' Should probably return int/long values for non-floating point numbers
                Value = Double.Parse(record)
            End If
        End Sub

        Public Sub Input(ByRef Value As Short)
            VerifyFileAccess(OpenAccess.Read, OpenAccess.ReadWrite)
            Value = CShort(ReadRecord())
        End Sub

        Public Sub Input(ByRef Value As Single)
            VerifyFileAccess(OpenAccess.Read, OpenAccess.ReadWrite)
            Value = CSng(ReadRecord())
        End Sub

        Public Sub Input(ByRef Value As String)
            VerifyFileAccess(OpenAccess.Read, OpenAccess.ReadWrite)
            Value = ReadRecord()
        End Sub

        Public Function InputString(ByVal CharCount As Integer) As String
            VerifyFileAccess(OpenAccess.Read, OpenAccess.ReadWrite)
            Dim chars(CharCount - 1) As Char
            If Reader.ReadBlock(chars, 0, CharCount) <> CharCount Then
                Throw New EndOfStreamException() 'TODO: Find exact message
            End If
            Return New String(chars)
        End Function

        Public Function LineInput() As String
            VerifyFileAccess(OpenAccess.Read, OpenAccess.ReadWrite)
            Return Reader.ReadLine()
        End Function

        Public Function Loc() As Long
            Return Stream.Position
        End Function

        Public Sub Lock()
            Stream.Lock(0, Long.MaxValue)
        End Sub

        Public Sub Lock(ByVal Record As Long)
            Lock(Record, Record + 1)
        End Sub

        Public Sub Lock(ByVal FromRecord As Long, ByVal ToRecord As Long)
            If RecordLength = -1 Then
                Stream.Lock(FromRecord - 1, ToRecord - FromRecord)
            Else
                Stream.Lock((FromRecord - 1) * RecordLength, (ToRecord - FromRecord) * RecordLength)
            End If
        End Sub

        Public Function LOF() As Long
            Return Stream.Length
        End Function

        Public Sub Print(ByVal ParamArray Output() As Object)
            VerifyFileAccess(OpenAccess.Write, OpenAccess.ReadWrite)
            For i As Integer = 0 To Output.Length - 1
                Dim item As Object = Output(i)

                If item IsNot Nothing Then
                    Dim iConv As IConvertible = TryCast(item, IConvertible)
                    Dim tc As TypeCode
                    If iConv IsNot Nothing Then
                        tc = iConv.GetTypeCode
                    Else
                        tc = Type.GetTypeCode(item.GetType)
                    End If
                    Select Case tc
                        Case TypeCode.Boolean
                            If DirectCast(item, Boolean) Then
                                Writer.Write("True")
                            Else
                                Writer.Write("False")
                            End If
                        Case TypeCode.Byte
                            Writer.Write(DirectCast(item, Byte))
                        Case TypeCode.Char
                            Writer.Write("""")
                            Writer.Write(DirectCast(item, Char))
                            Writer.Write("""")
                        Case TypeCode.DateTime
                            Writer.Write(DirectCast(item, Date).ToString()) 'FIXME: Check date format
                        Case TypeCode.DBNull
                            Writer.Write("Null")
                        Case TypeCode.Decimal
                            Writer.Write(DirectCast(item, Decimal))
                        Case TypeCode.Double
                            Writer.Write(DirectCast(item, Double))
                        Case TypeCode.Int16
                            Writer.Write(DirectCast(item, Short))
                        Case TypeCode.Int32
                            Writer.Write(DirectCast(item, Integer))
                        Case TypeCode.Int64
                            Writer.Write(DirectCast(item, Long))
                        Case TypeCode.SByte
                            Writer.Write(DirectCast(item, SByte))
                        Case TypeCode.Single
                            Writer.Write(DirectCast(item, Single))
                        Case TypeCode.String
                            Writer.Write("""")
                            Writer.Write(DirectCast(item, String))
                            Writer.Write("""")
                        Case TypeCode.UInt16
                            Writer.Write(DirectCast(item, UShort))
                        Case TypeCode.UInt32
                            Writer.Write(DirectCast(item, UInteger))
                        Case TypeCode.UInt64
                            Writer.Write(DirectCast(item, ULong))
                        Case TypeCode.Object
                            If TypeOf item Is ErrObject Then
                                Writer.Write("Error ")
                                Writer.Write(DirectCast(item, ErrObject).Number)
                            Else
                                Throw New NotImplementedException("Can't write object of type: " & item.GetType().FullName)
                            End If
                    End Select
                End If

                If i < Output.Length - 1 Then
                    Writer.Write(","c)
                End If
            Next
        End Sub

        Public Sub PrintLine(ByVal ParamArray Output() As Object)
            Print(Output)
            Writer.Write(vbCrLf)
        End Sub

        Public Function Seek() As Long
            Return Stream.Position
        End Function

        Public Sub Seek(ByVal Position As Long)
            Stream.Position = Position
        End Sub

        Public Sub Unlock()
            Stream.Unlock(0, Long.MaxValue)
        End Sub

        Public Sub Unlock(ByVal Record As Long)
            Stream.Unlock(Record, Record + 1)
        End Sub

        Public Sub Unlock(ByVal FromRecord As Long, ByVal ToRecord As Long)
            If Me.RecordLength = -1 Then
                Stream.Unlock(FromRecord - 1, ToRecord - FromRecord)
            Else
                Stream.Unlock((FromRecord - 1) * RecordLength, (ToRecord - FromRecord) * RecordLength)
            End If
        End Sub

        Public Sub Write(ByVal ParamArray Output() As Object)
            For i As Integer = 0 To Output.Length - 1
                Dim item As Object = Output(i)

                If item IsNot Nothing Then
                    Dim iConv As IConvertible = TryCast(item, IConvertible)
                    Dim tc As TypeCode
                    If iConv IsNot Nothing Then
                        tc = iConv.GetTypeCode
                    Else
                        tc = Type.GetTypeCode(item.GetType)
                    End If
                    Select Case tc
                        Case TypeCode.Boolean
                            If DirectCast(item, Boolean) Then
                                Writer.Write("#TRUE#")
                            Else
                                Writer.Write("#FALSE")
                            End If
                        Case TypeCode.Byte
                            Writer.Write(DirectCast(item, Byte))
                        Case TypeCode.Char
                            Writer.Write("""")
                            Writer.Write(DirectCast(item, Char))
                            Writer.Write("""")
                        Case TypeCode.DateTime
                            Writer.Write(DirectCast(item, Date).ToString()) 'FIXME: Check date format
                        Case TypeCode.DBNull
                            Writer.Write("#NULL#")
                        Case TypeCode.Decimal
                            Writer.Write(DirectCast(item, Decimal))
                        Case TypeCode.Double
                            Writer.Write(DirectCast(item, Double))
                        Case TypeCode.Int16
                            Writer.Write(DirectCast(item, Short))
                        Case TypeCode.Int32
                            Writer.Write(DirectCast(item, Integer))
                        Case TypeCode.Int64
                            Writer.Write(DirectCast(item, Long))
                        Case TypeCode.SByte
                            Writer.Write(DirectCast(item, SByte))
                        Case TypeCode.Single
                            Writer.Write(DirectCast(item, Single))
                        Case TypeCode.String
                            Writer.Write("""")
                            Writer.Write(DirectCast(item, String))
                            Writer.Write("""")
                        Case TypeCode.UInt16
                            Writer.Write(DirectCast(item, UShort))
                        Case TypeCode.UInt32
                            Writer.Write(DirectCast(item, UInteger))
                        Case TypeCode.UInt64
                            Writer.Write(DirectCast(item, ULong))
                        Case TypeCode.Object
                            If TypeOf item Is ErrObject Then
                                Writer.Write("#ERROR " & DirectCast(item, ErrObject).Number & "#")
                            Else
                                Throw New NotImplementedException("Can't write object of type: " & item.GetType().FullName)
                            End If
                    End Select
                End If

                If i < Output.Length - 1 Then
                    Writer.Write(","c)
                End If
            Next
        End Sub

        Public Sub WriteLine(ByVal ParamArray Output() As Object)
            Write(Output)
            Writer.Write(vbCrLf)
        End Sub
    End Class
End Namespace
