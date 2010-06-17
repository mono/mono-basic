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
    'http://msdn2.microsoft.com/en-us/library/ms675318.aspx
    Friend Enum FileVariableType
        Empty = 0
        DBNull = 1
        [Short] = 2
        [Integer] = 3
        [Single] = 4
        [Double] = 5
        [Currency] = 6
        [Date] = 7
        [String] = 8
        [Error] = 10
        [Variant] = 12
        [Decimal] = 14
        [Boolean] = 11
        [Byte] = 17
        [Char] = 18 'UnsignedSmallInt in COM docs
        [Long] = 20
        [Structure] = 36

        Array = &H2000

        Binary = 128
        Chapter = 136
        ActualChar = 129
        DBDate = 133
        DBTime = 134
        DBTimeStamp = 135
        FileTime = 64
        Guid = 72
        IDispatch = 9
        IUnknown = 13
        LongVarBinary = 205
        LongVarChar = 201
        LongVarWChar = 203
        Numeric = 131
        PropVariant = 138
        TinyInt = 16
        UnsignedBigInt = 21
        UnsignedInt = 19
        UserDefind = 132
        VarBinary = 204
        VarChar = 200
        VarNumeric = 139
        VarWChar = 202
        WChar = 130

    End Enum

    Friend Class ArrayDescriptor
        Public Rank As Integer
        Public Sizes() As Integer
        Public LowerBounds() As Integer
        Public HasNonZeroLowerBounds As Boolean

        Sub Read(ByVal BinaryStream As BinaryReader)
            Rank = BinaryStream.ReadInt32
            ReDim Sizes(Rank - 1)
            ReDim LowerBounds(Rank - 1)
            For i As Integer = 0 To Rank - 1
                Sizes(i) = BinaryStream.ReadInt32
                LowerBounds(i) = BinaryStream.ReadInt32
                HasNonZeroLowerBounds = HasNonZeroLowerBounds OrElse LowerBounds(i) <> 0
            Next
        End Sub
    End Class

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

            If Me.RecordLength = -1 Then
                If Mode = OpenMode.Random Then
                    Me.RecordLength = 128
                End If
            End If

            CreateStream()
            Dim enc As System.Text.Encoding = System.Text.Encoding.Default

            If Me.Access = OpenAccess.Read OrElse Me.Access = OpenAccess.ReadWrite Then
                Reader = New StreamReader(Stream, enc)
                BinaryReader = New BinaryReader(Stream, enc)
            End If
            If Me.Access = OpenAccess.Write OrElse Me.Access = OpenAccess.ReadWrite Then
                Writer = New StreamWriter(Stream, enc)
                Writer.AutoFlush = True
                BinaryWriter = New BinaryWriter(Stream, enc)
            End If
        End Sub

        Public Sub CreateStream()
            If Access = OpenAccess.Default Then
                Select Case Mode
                    Case OpenMode.Input
                        Access = OpenAccess.Read
                    Case OpenMode.Binary, OpenMode.Random
                        Access = OpenAccess.ReadWrite
                    Case OpenMode.Output, OpenMode.Append
                        Access = OpenAccess.Write
                End Select
            ElseIf Mode = OpenMode.Append AndAlso Access = OpenAccess.ReadWrite Then
                Access = OpenAccess.Write
            End If

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
                    ioMode = FileMode.OpenOrCreate
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

            If Mode = OpenMode.Append Then
                Seek(Stream.Length + 1)
            End If
        End Sub

        Public Sub Close()
            If Not Stream Is Nothing Then
                Stream.Close()
                Stream = Nothing
            End If
        End Sub

        Public Sub VerifyFileModes(ByVal ParamArray Modes As OpenMode())
            For i As Integer = 0 To Modes.Length - 1
                If Modes(i) = Mode Then Return
            Next
            Throw Microsoft.VisualBasic.CompilerServices.ExceptionUtils.GetVBException(VBErrors.ERR54_Bad_file_mode)
        End Sub

        Public Sub VerifyWriteAccess()
            If Access <> OpenAccess.Write AndAlso Access <> OpenAccess.ReadWrite Then
                Select Case Mode
                    Case OpenMode.Output
                        Throw Microsoft.VisualBasic.CompilerServices.ExceptionUtils.GetVBException(VBErrors.ERR54_Bad_file_mode)
                    Case Else
                        Throw New IOException("File is not opened for write access.")
                End Select
            End If
        End Sub

        Public Sub VerifyReadAccessWeirdly()
            If Access <> OpenAccess.Read AndAlso Access <> OpenAccess.ReadWrite Then
                Throw New NullReferenceException((New NullReferenceException()).Message, New IOException("File is not opened for read access."))
            End If
        End Sub

        Public Sub VerifyReadAccess()
            If Access <> OpenAccess.Read AndAlso Access <> OpenAccess.ReadWrite Then
                Select Case Mode
                    Case OpenMode.Output
                        Throw Microsoft.VisualBasic.CompilerServices.ExceptionUtils.GetVBException(VBErrors.ERR54_Bad_file_mode)
                    Case Else
                        Throw New IOException("File is not opened for read access.")
                End Select
            End If
        End Sub

        Public Function EOF() As Boolean
            VerifyFileModes(OpenMode.Input, OpenMode.Random, OpenMode.Random)
            Return Stream.Length = Stream.Position
        End Function

        Public Sub FileGet(ByRef Value As Boolean, Optional ByVal RecordNumber As Long = -1)
            VerifyFileModes(OpenMode.Random, OpenMode.Binary)
            If RecordNumber >= 0 Then Seek(RecordNumber)
            Value = BinaryReader.ReadInt16 <> 0
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
            Value = Decimal.FromOACurrency(BinaryReader.ReadInt64)
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
            VerifyReadAccess()

            If RecordNumber >= 0 Then Seek(RecordNumber)
            Value = BinaryReader.ReadSingle
        End Sub

        Private Sub FileGetBinary(ByRef Value As String, Optional ByVal RecordNumber As Long = -1, Optional ByVal StringIsFixedLength As Boolean = False, Optional ByVal ReadDescriptor As Boolean = False)
            If RecordNumber >= 0 Then Seek(RecordNumber)
            If IsEOF Then
                Value = Nothing
                Return
            End If

            Dim length As Integer
            If ReadDescriptor Then
                length = BinaryReader.ReadInt16()
            ElseIf Not Value Is Nothing Then
                length = Value.Length
            Else
                Return
            End If

            Try
                Value = New String(BinaryReader.ReadChars(length))
            Catch ex As EndOfStreamException
                Value = String.Empty
            End Try
        End Sub

        Private Sub FileGetRandom(ByRef Value As String, Optional ByVal RecordNumber As Long = -1, Optional ByVal StringIsFixedLength As Boolean = False)
            If RecordNumber >= 0 Then Seek(RecordNumber)

            If IsEOF Then
                Value = String.Empty
                Return
            End If

            Dim length As Integer
            Dim prefix As Integer = 0
            If StringIsFixedLength = False Then
                length = BinaryReader.ReadInt16()
                prefix = 2
            ElseIf Not Value Is Nothing Then
                length = Value.Length
            Else
                length = 0
            End If

            Try

                If RecordLength < prefix + length Then Throw New IOException("Bad record length.")
                If length = 0 Then
                    Value = Nothing
                Else
                    Value = New String(BinaryReader.ReadChars(length))
                End If
            Catch ex As EndOfStreamException
                Value = String.Empty
            End Try
        End Sub

        Public Sub FileGet(ByRef Value As String, Optional ByVal RecordNumber As Long = -1, Optional ByVal StringIsFixedLength As Boolean = False)
            VerifyFileModes(OpenMode.Random, OpenMode.Binary)
            VerifyReadAccess()
            Select Case Mode
                Case OpenMode.Random
                    FileGetRandom(Value, RecordNumber, StringIsFixedLength)
                Case OpenMode.Binary
                    FileGetBinary(Value, RecordNumber, StringIsFixedLength)
                Case Else
                    Throw Microsoft.VisualBasic.CompilerServices.ExceptionUtils.GetVBException(VBErrors.ERR54_Bad_file_mode)
            End Select
        End Sub

        Public Sub FileGet(ByRef Value As System.Array, Optional ByVal RecordNumber As Long = -1, Optional ByVal ArrayIsDynamic As Boolean = False, Optional ByVal StringIsFixedLength As Boolean = False)
            VerifyFileModes(OpenMode.Random, OpenMode.Binary)
            VerifyReadAccess()
            If Value Is Nothing Then Throw New ArgumentException("Cannot determine array type because it is Nothing.")
            Throw New NotImplementedException
        End Sub

        Public Sub FileGet(ByRef Value As System.ValueType, Optional ByVal RecordNumber As Long = -1)
            VerifyFileModes(OpenMode.Random, OpenMode.Binary)
            VerifyReadAccess()
            If Value Is Nothing Then Throw New NullReferenceException
            Throw New NotImplementedException
        End Sub

        Public Sub FileGetObject(ByRef Value As Object, Optional ByVal RecordNumber As Long = -1)
            VerifyFileModes(OpenMode.Random, OpenMode.Binary)
            VerifyReadAccess()

            If Mode = OpenMode.Random Then
                FileGetObjectRandom(Value, RecordNumber)
            Else 'If Mode = OpenMode.Binary
                FileGetObjectBinary(Value, RecordNumber)
            End If
        End Sub

        Private Sub FileGetObjectBinary(ByRef Value As Object, ByVal RecordNumber As Long)
            If RecordNumber <> -1 Then Seek(RecordNumber)
            Dim descriptor As FileVariableType = CType(BinaryReader.ReadInt16, FileVariableType)
            If (descriptor And FileVariableType.Array) = FileVariableType.Array Then
                FileGetObjectArray(descriptor, Value)
            Else
                FileGetObjectVariable(descriptor, Value)
            End If
        End Sub

        Private Sub FileGetObjectRandom(ByRef Value As Object, ByVal RecordNumber As Long)
            If RecordNumber <> -1 Then Seek(RecordNumber)
            Dim descriptor As FileVariableType = CType(BinaryReader.ReadInt16, FileVariableType)
            If (descriptor And FileVariableType.Array) = FileVariableType.Array Then
                FileGetObjectArray(descriptor, Value)
            Else
                FileGetObjectVariable(descriptor, Value)
            End If
        End Sub

        Private Sub FileGetObjectVariable(ByVal Type As FileVariableType, ByRef Value As Object)
            Select Case Type
                Case FileVariableType.DBNull ' 1
                    Value = DBNull.Value
                Case FileVariableType.Boolean ' 11 
                    Value = CBool(BinaryReader.ReadInt16)
                Case FileVariableType.Byte ' 17 
                    Value = BinaryReader.ReadByte
                Case FileVariableType.Char ' 18
                    Value = Strings.Chr(BinaryReader.ReadByte)
                Case FileVariableType.Short ' 2
                    Value = BinaryReader.ReadInt16
                Case FileVariableType.Integer ' 3 
                    Value = BinaryReader.ReadInt32
                Case FileVariableType.Long '20 
                    Value = BinaryReader.ReadInt64
                Case FileVariableType.Single '4 
                    Value = BinaryReader.ReadSingle
                Case FileVariableType.Double '5
                    Value = BinaryReader.ReadDouble
                Case FileVariableType.Decimal '14 
                    Dim dummy As Integer = BinaryReader.ReadInt16
                    Dim Scale As Byte = BinaryReader.ReadByte
                    Dim IsNegative As Boolean = BinaryReader.ReadByte <> 0
                    Dim Hi As Integer = BinaryReader.ReadInt32
                    Dim Low As Integer = BinaryReader.ReadInt32
                    Dim Mid As Integer = BinaryReader.ReadInt32

                    Value = New Decimal(Low, Mid, Hi, IsNegative, Scale)
                Case FileVariableType.Date ' 7 
                    Value = Date.FromOADate(BinaryReader.ReadDouble)
                Case FileVariableType.String ' 8 
                    Value = BinaryReader.ReadString
                Case FileVariableType.Empty ' 0 
                    Value = Nothing
                Case Else
                    Throw New NullReferenceException()
            End Select
        End Sub

        Private Sub FileGetObjectArray(ByVal Type As FileVariableType, ByRef Value As Object)
            Dim elementType As FileVariableType = CType(Type And &H1FFF, FileVariableType)

            Select Case elementType
                Case FileVariableType.DBNull ' 1
                    Value = DBNull.Value
                Case FileVariableType.Boolean ' 11 
                    Value = CBool(BinaryReader.ReadInt16)
                Case FileVariableType.Byte ' 17 
                    Value = BinaryReader.ReadByte
                Case FileVariableType.Char ' 18
                    Value = Strings.Chr(BinaryReader.ReadByte)
                Case FileVariableType.Short ' 2
                    Value = BinaryReader.ReadInt16
                Case FileVariableType.Integer ' 3 
                    Value = BinaryReader.ReadInt32
                Case FileVariableType.Long '20 
                    Value = BinaryReader.ReadInt64
                Case FileVariableType.Single '4 
                    Value = BinaryReader.ReadSingle
                Case FileVariableType.Double '5
                    Value = BinaryReader.ReadDouble
                Case FileVariableType.Decimal '14 
                    Dim dummy As Integer = BinaryReader.ReadInt16
                    Dim Scale As Byte = BinaryReader.ReadByte
                    Dim IsNegative As Boolean = BinaryReader.ReadByte <> 0
                    Dim Hi As Integer = BinaryReader.ReadInt32
                    Dim Low As Integer = BinaryReader.ReadInt32
                    Dim Mid As Integer = BinaryReader.ReadInt32

                    Value = New Decimal(Low, Mid, Hi, IsNegative, Scale)
                Case FileVariableType.Date ' 7 
                    Value = Date.FromOADate(BinaryReader.ReadDouble)
                Case FileVariableType.String ' 8 
                    Value = BinaryReader.ReadString
                Case FileVariableType.Empty ' 0 
                    Value = Nothing
                Case Else
                    Throw New Exception("Variable uses an Automation type not supported in Visual Basic.")
            End Select
        End Sub

        Public Sub FilePutObjectVariable(ByVal Value As Object, ByVal Type As FileVariableType, ByVal PutDescriptor As Boolean, Optional ByVal StringIsFixedLength As Boolean = False)
            If PutDescriptor AndAlso Type <> FileVariableType.DBNull Then BinaryWriter.Write(CShort(Type))
            Select Case Type
                Case FileVariableType.DBNull
                    BinaryWriter.Write(1S)
                Case FileVariableType.Boolean
                    If Conversions.ToBoolean(Value) Then
                        BinaryWriter.Write(-1S)
                    Else
                        BinaryWriter.Write(0S)
                    End If
                Case FileVariableType.Byte
                    BinaryWriter.Write(DirectCast(Value, Byte))
                Case FileVariableType.Char
                    BinaryWriter.Write(DirectCast(Value, Char))
                Case FileVariableType.Short
                    BinaryWriter.Write(DirectCast(Value, Short))
                Case FileVariableType.Integer
                    BinaryWriter.Write(DirectCast(Value, Integer))
                Case FileVariableType.Long
                    BinaryWriter.Write(DirectCast(Value, Long))
                Case FileVariableType.Single
                    BinaryWriter.Write(DirectCast(Value, Single))
                Case FileVariableType.Double
                    BinaryWriter.Write(DirectCast(Value, Double))
                Case FileVariableType.Currency
                    BinaryWriter.Write(Decimal.ToOACurrency(DirectCast(Value, Decimal)))
                Case FileVariableType.Decimal
                    Dim dec As Decimal = DirectCast(Value, Decimal)
                    Dim bits() As Integer = Decimal.GetBits(dec)
                    Dim packed As Integer = bits(3)
                    Dim dummy As Short = 14
                    Dim Scale As Byte = CByte((packed >> 16) And &HFF)
                    Dim IsNegative As Byte
                    Dim Hi As Integer = bits(0)
                    Dim Low As Integer = bits(1)
                    Dim Mid As Integer = bits(2)

                    If (packed And (1 << 31)) <> 0 Then
                        IsNegative = 128
                    Else
                        IsNegative = 0
                    End If

                    BinaryWriter.Write(dummy)
                    BinaryWriter.Write(Scale)
                    BinaryWriter.Write(IsNegative)
                    BinaryWriter.Write(Hi)
                    BinaryWriter.Write(Low)
                    BinaryWriter.Write(Mid)
                    'Value = New Decimal(Low, Mid, Hi, IsNegative, Scale)
                Case FileVariableType.Date
                    BinaryWriter.Write(DirectCast(Value, Date).ToOADate)
                Case FileVariableType.String
                    Dim str As String = DirectCast(Value, String)
                    If StringIsFixedLength = False AndAlso (Mode <> OpenMode.Binary OrElse PutDescriptor) Then
                        Dim length As Short
                        If str Is Nothing Then
                            length = 0
                        ElseIf str.Length > Short.MaxValue Then
                            Throw New ArgumentException("String is too long") 'FIXME: Check error condition and message
                        Else
                            length = CShort(str.Length)
                        End If
                        BinaryWriter.Write(length)
                    End If
                        Writer.Write(str) 'Binary writer writes a length-prefixed string (and the length-prefix doesn't match VB's length prefix)
                Case FileVariableType.Empty
                        BinaryWriter.Write(0S)
                Case Else
                        Throw New Exception("Variable uses an Automation type not supported in Visual Basic.")
            End Select
        End Sub

        Public Sub FilePut(ByVal Value As Boolean, Optional ByVal RecordNumber As Long = -1)
            VerifyFileModes(OpenMode.Random, OpenMode.Binary)
            VerifyWriteAccess()
            If RecordNumber >= 0 Then Seek(RecordNumber)
            FilePutObjectVariable(Value, FileVariableType.Boolean, False)
        End Sub

        Public Sub FilePut(ByVal Value As Byte, Optional ByVal RecordNumber As Long = -1)
            VerifyFileModes(OpenMode.Random, OpenMode.Binary)
            VerifyWriteAccess()
            If RecordNumber >= 0 Then Seek(RecordNumber)
            FilePutObjectVariable(Value, FileVariableType.Byte, False)
        End Sub

        Public Sub FilePut(ByVal Value As Char, Optional ByVal RecordNumber As Long = -1)
            VerifyFileModes(OpenMode.Random, OpenMode.Binary)
            VerifyWriteAccess()
            If RecordNumber >= 0 Then Seek(RecordNumber)
            FilePutObjectVariable(Value, FileVariableType.Char, False)
        End Sub

        Public Sub FilePut(ByVal Value As Date, Optional ByVal RecordNumber As Long = -1)
            VerifyFileModes(OpenMode.Random, OpenMode.Binary)
            VerifyWriteAccess()
            If RecordNumber >= 0 Then Seek(RecordNumber)
            FilePutObjectVariable(Value, FileVariableType.Date, False)
        End Sub

        Public Sub FilePut(ByVal Value As Decimal, Optional ByVal RecordNumber As Long = -1)
            VerifyFileModes(OpenMode.Random, OpenMode.Binary)
            VerifyWriteAccess()
            If RecordNumber >= 0 Then Seek(RecordNumber)
            FilePutObjectVariable(Value, FileVariableType.Currency, False)
        End Sub

        Public Sub FilePut(ByVal Value As Double, Optional ByVal RecordNumber As Long = -1)
            VerifyFileModes(OpenMode.Random, OpenMode.Binary)
            VerifyWriteAccess()
            If RecordNumber >= 0 Then Seek(RecordNumber)
            FilePutObjectVariable(Value, FileVariableType.Double, False)
        End Sub

        Public Sub FilePut(ByVal Value As Integer, Optional ByVal RecordNumber As Long = -1)
            VerifyFileModes(OpenMode.Random, OpenMode.Binary)
            VerifyWriteAccess()
            If RecordNumber >= 0 Then Seek(RecordNumber)
            FilePutObjectVariable(Value, FileVariableType.Integer, False)
        End Sub

        Public Sub FilePut(ByVal Value As Long, Optional ByVal RecordNumber As Long = -1)
            VerifyFileModes(OpenMode.Random, OpenMode.Binary)
            VerifyWriteAccess()
            If RecordNumber >= 0 Then Seek(RecordNumber)
            FilePutObjectVariable(Value, FileVariableType.Long, False)
        End Sub

        Public Sub FilePut(ByVal Value As Short, Optional ByVal RecordNumber As Long = -1)
            VerifyFileModes(OpenMode.Random, OpenMode.Binary)
            VerifyWriteAccess()
            If RecordNumber >= 0 Then Seek(RecordNumber)
            FilePutObjectVariable(Value, FileVariableType.Short, False)
        End Sub

        Public Sub FilePut(ByVal Value As Single, Optional ByVal RecordNumber As Long = -1)
            VerifyFileModes(OpenMode.Random, OpenMode.Binary)
            VerifyWriteAccess()
            If RecordNumber >= 0 Then Seek(RecordNumber)
            FilePutObjectVariable(Value, FileVariableType.Single, False)
        End Sub

        Public Sub FilePut(ByVal Value As String, Optional ByVal RecordNumber As Long = -1, Optional ByVal StringIsFixedLength As Boolean = False)
            VerifyFileModes(OpenMode.Random, OpenMode.Binary)
            VerifyWriteAccess()

            If RecordNumber >= 0 Then Seek(RecordNumber)

            FilePutObjectVariable(Value, FileVariableType.String, False, StringIsFixedLength)
        End Sub

        Public Sub FilePut(ByVal Value As Array, Optional ByVal RecordNumber As Long = -1, Optional ByVal ArrayIsDynamic As Boolean = False, Optional ByVal StringIsFixedLength As Boolean = False)
            VerifyFileModes(OpenMode.Random, OpenMode.Binary)
            VerifyWriteAccess()

            If RecordNumber >= 0 Then Seek(RecordNumber)

            If ArrayIsDynamic Then
                FilePutDynamicArray(Value, StringIsFixedLength)
            Else
                FilePutStaticArray(Value, StringIsFixedLength)
            End If
        End Sub

        Private Sub FilePutDynamicArray(ByVal Value As Array, ByVal StringIsFixedLength As Boolean)
            FilePutArrayDescriptor(Value)
            FilePutStaticArray(Value, StringIsFixedLength)
        End Sub

        Private Sub FilePutArrayDescriptor(ByVal Value As Array)
            BinaryWriter.Write(CShort(Value.Rank))
            For i As Integer = 1 To Value.Rank
                BinaryWriter.Write(Value.GetLength(i - 1))
                BinaryWriter.Write(Value.GetLowerBound(i - 1))
            Next
        End Sub

        Private Function GetCOMTypeForValue(ByVal value As Object) As FileVariableType
            Dim tp As Type
            If value Is Nothing Then Return FileVariableType.Empty
            tp = value.GetType
            Select Case Type.GetTypeCode(tp)
                Case TypeCode.Boolean
                    Return FileVariableType.Boolean
                Case TypeCode.Byte
                    Return FileVariableType.Byte
                Case TypeCode.Char
                    Return FileVariableType.Char
                Case TypeCode.DateTime
                    Return FileVariableType.Date
                Case TypeCode.DBNull
                    Return FileVariableType.DBNull
                Case TypeCode.Decimal
                    Return FileVariableType.Decimal
                Case TypeCode.Double
                    Return FileVariableType.Double
                Case TypeCode.Empty
                    Return FileVariableType.Empty
                Case TypeCode.Int16
                    Return FileVariableType.Short
                Case TypeCode.Int32
                    Return FileVariableType.Integer
                Case TypeCode.Int64
                    Return FileVariableType.Long
                Case TypeCode.Object
                    If tp.IsArray Then
                        Return FileVariableType.Array Or GetCOMTypeForValue(tp.GetElementType)
                    Else
                        Throw New NotImplementedException
                    End If
                Case TypeCode.Single
                    Return FileVariableType.Single
                Case TypeCode.String
                    Return FileVariableType.String
                Case TypeCode.SByte
                    Throw New ArgumentException("'FilePutObject' of structure 'SByte' is not valid.")
                Case TypeCode.UInt16
                    Throw New ArgumentException("'FilePutObject' of structure 'UShort' is not valid.")
                Case TypeCode.UInt32
                    Throw New ArgumentException("'FilePutObject' of structure 'UInteger' is not valid.")
                Case TypeCode.UInt64
                    Throw New ArgumentException("'FilePutObject' of structure 'ULong' is not valid.")
                Case Else
                    Throw New NotImplementedException
            End Select
        End Function

        Private Sub FilePutStaticArray(ByVal Value As Array, ByVal StringIsFixedLength As Boolean)
            If Value.Rank = 1 Then
                For i As Integer = 0 To Value.Length - 1
                    Dim item As Object = Value.GetValue(i)
                    FilePutObjectVariable(item, GetCOMTypeForValue(item), False, StringIsFixedLength)
                Next
            Else
                Throw New NotImplementedException
            End If
        End Sub

        Public Sub FilePut(ByVal Value As System.ValueType, Optional ByVal RecordNumber As Long = -1)
            VerifyFileModes(OpenMode.Random, OpenMode.Binary)
            VerifyWriteAccess()
            If Value Is Nothing Then Throw New NullReferenceException
            Throw New NotImplementedException
        End Sub

        Private Sub FilePut(ByVal Value As Object, ByVal StringIsFixedLength As Boolean)
            If Value Is Nothing Then
                Throw New NotImplementedException
            Else
                Select Case Type.GetTypeCode(Value.GetType)
                    Case TypeCode.Boolean
                        FilePut(DirectCast(Value, Boolean))
                    Case TypeCode.Byte
                        FilePut(DirectCast(Value, Byte))
                    Case TypeCode.Char
                        FilePut(DirectCast(Value, Char))
                    Case TypeCode.DateTime
                        FilePut(DirectCast(Value, DateTime))
                    Case TypeCode.Decimal
                        FilePut(DirectCast(Value, Decimal))
                    Case TypeCode.Double
                        FilePut(DirectCast(Value, Double))
                    Case TypeCode.Int16
                        FilePut(DirectCast(Value, Short))
                    Case TypeCode.Int32
                        FilePut(DirectCast(Value, Integer))
                    Case TypeCode.Int64
                        FilePut(DirectCast(Value, Long))
                    Case TypeCode.Object
                        Throw New NotImplementedException
                    Case TypeCode.Single
                        FilePut(DirectCast(Value, Single))
                    Case TypeCode.String
                        FilePutObjectVariable(DirectCast(Value, String), FileVariableType.String, True, StringIsFixedLength)
                    Case TypeCode.DBNull
                        BinaryWriter.Write(1S)
                    Case TypeCode.SByte
                        Throw New ArgumentException("'FilePutObject' of structure 'SByte' is not valid.")
                    Case TypeCode.UInt16
                        Throw New ArgumentException("'FilePutObject' of structure 'UShort' is not valid.")
                    Case TypeCode.UInt32
                        Throw New ArgumentException("'FilePutObject' of structure 'UInteger' is not valid.")
                    Case TypeCode.UInt64
                        Throw New ArgumentException("'FilePutObject' of structure 'ULong' is not valid.")
                    Case Else
                        Throw New NotImplementedException
                End Select
            End If
        End Sub

        Public Sub FilePutObject(ByVal Value As Object, Optional ByVal RecordNumber As Long = -1)
            VerifyFileModes(OpenMode.Random, OpenMode.Binary)
            VerifyWriteAccess()
            If Value Is Nothing Then
                BinaryWriter.Write(0S)
            ElseIf TypeOf Value Is Array Then
                FilePut(CType(Value, Array), RecordNumber)
            Else
                If RecordNumber >= 0 Then Seek(RecordNumber)
                'FilePut(Value, False)
                FilePutObjectVariable(Value, GetCOMTypeForValue(Value), True)
            End If
        End Sub

        Public Sub FileWidth(ByVal RecordWidth As Integer)
            Me.RecordWidth = RecordWidth
        End Sub

        Private ReadOnly Property IsEOF() As Boolean
            Get
                Return Stream.Position = Stream.Length
            End Get
        End Property

        Private Function ReadStringRecord() As String
            Dim ch As Char
            Dim builder As New StringBuilder

            If IsEOF() Then Throw Microsoft.VisualBasic.CompilerServices.ExceptionUtils.GetVBException(VBErrors.ERR62_Input_past_end_of_file)

            Try
                ch = BinaryReader.ReadChar()
                While ch <> ","c AndAlso ch <> CChar(Constants.vbCr) AndAlso ch <> CChar(Constants.vbLf)
                    If ch <> Nothing Then builder.Append(ch)
                    ch = BinaryReader.ReadChar()
                End While

                If ch = Conversions.ToChar(Constants.vbCr) AndAlso Strings.Chr(BinaryReader.PeekChar) = Conversions.ToChar(Constants.vbLf) Then
                    BinaryReader.ReadChar()
                End If
            Catch ex As EndOfStreamException
            End Try

            Return builder.ToString
        End Function

        Private Function ParseRecord(ByVal record As String) As Object
            If record.Length = 0 Then
                Throw New IndexOutOfRangeException()
            ElseIf String.CompareOrdinal("#TRUE#", record) = 0 Then
                Return True
            ElseIf String.CompareOrdinal("#FALSE#", record) = 0 Then
                Return False
            ElseIf record.StartsWith("#ERROR") AndAlso record.EndsWith("#") Then
                Return Conversions.ToInteger(record.Substring(6, record.Length - 7))
            Else
                Return record
            End If
        End Function

        Private Function ReadRecord() As String
            Dim ch As Char
            Dim builder As New StringBuilder

            If IsEOF() Then Throw Microsoft.VisualBasic.CompilerServices.ExceptionUtils.GetVBException(VBErrors.ERR62_Input_past_end_of_file)

            Try
                ch = BinaryReader.ReadChar()
                If ch = "#"c Then
                    While ch <> ","c AndAlso ch <> CChar(Constants.vbCr) AndAlso ch <> CChar(Constants.vbLf) AndAlso ch <> " "c
                        If ch <> Nothing Then builder.Append(ch)
                        ch = BinaryReader.ReadChar()
                    End While
                Else
                    While ch <> ","c AndAlso ch <> CChar(Constants.vbCr) AndAlso ch <> CChar(Constants.vbLf)
                        If ch <> Nothing Then builder.Append(ch)
                        ch = BinaryReader.ReadChar
                    End While
                End If

                If ch = Conversions.ToChar(Constants.vbCr) AndAlso Strings.Chr(BinaryReader.PeekChar) = Conversions.ToChar(Constants.vbLf) Then
                    BinaryReader.ReadChar()
                End If

            Catch ex As EndOfStreamException
                'Do nothing
            End Try

            Return builder.ToString
        End Function

        Public Sub Input(ByRef Value As Boolean)
            VerifyFileModes(OpenMode.Input, OpenMode.Binary)
            VerifyReadAccess()
            If Mode = OpenMode.Input Then
                Value = Conversions.ToBoolean(ParseRecord(ReadStringRecord))
            Else 'If Mode = OpenMode.Binary Then
                Value = Conversions.ToBoolean(ReadStringRecord())
            End If
        End Sub

        Public Sub Input(ByRef Value As Byte)
            VerifyFileModes(OpenMode.Input, OpenMode.Binary)
            VerifyReadAccess()
            Value = Conversions.ToByte(ReadRecord())
        End Sub

        Public Sub Input(ByRef Value As Char)
            VerifyFileModes(OpenMode.Input, OpenMode.Binary)
            VerifyReadAccess()
            Value = Conversions.ToChar(ReadRecord())
        End Sub

        Public Sub Input(ByRef Value As Date)
            VerifyFileModes(OpenMode.Input, OpenMode.Binary)
            VerifyReadAccess()
            If Mode = OpenMode.Input Then
                Value = Conversions.ToDate(ParseRecord(ReadStringRecord()))
            Else 'If Mode = OpenMode.Binary Then
                Value = Conversions.ToDate(ReadStringRecord())
            End If
        End Sub

        Public Sub Input(ByRef Value As Decimal)
            VerifyFileModes(OpenMode.Input, OpenMode.Binary)
            VerifyReadAccess()
            Value = Conversions.ToDecimal(ReadRecord())
        End Sub

        Public Sub Input(ByRef Value As Double)
            VerifyFileModes(OpenMode.Input, OpenMode.Binary)
            VerifyReadAccess()
            Value = Conversions.ToDouble(ReadRecord())
        End Sub

        Public Sub Input(ByRef Value As Integer)
            VerifyFileModes(OpenMode.Input, OpenMode.Binary)
            VerifyReadAccess()
            Value = Conversions.ToInteger(ReadRecord())
        End Sub

        Public Sub Input(ByRef Value As Long)
            VerifyFileModes(OpenMode.Input, OpenMode.Binary)
            VerifyReadAccess()
            Value = Conversions.ToLong(ReadRecord())
        End Sub

        Public Sub Input(ByRef Value As Object)
            VerifyFileModes(OpenMode.Input, OpenMode.Binary)
            VerifyReadAccess()
            Dim record As String = ReadStringRecord()
            If record Is Nothing Then
                Value = Nothing
            ElseIf record Is Nothing OrElse record.Length = 0 Then
                If Mode = OpenMode.Input Then
                    Value = Nothing
                Else
                    Value = record
                End If
            ElseIf String.CompareOrdinal("#FALSE#", record) = 0 Then
                Value = False
            ElseIf String.CompareOrdinal("#TRUE#", record) = 0 Then
                Value = True
            ElseIf String.CompareOrdinal("#NULL#", record) = 0 Then
                Value = DBNull.Value
            ElseIf record.StartsWith("#ERROR ") AndAlso record.EndsWith("#") Then
                If Mode = OpenMode.Binary Then
                    Value = record
                Else
                    record = record.Substring(7, record.Length - 8)
                    Value = Integer.Parse(record)
                End If
            ElseIf record.StartsWith("#") AndAlso record.EndsWith("#") Then
                record = record.Substring(1, record.Length - 1)
                Value = Date.Parse(record)
            ElseIf record.StartsWith("""") AndAlso record.EndsWith("""") Then
                Value = record.Substring(1, record.Length - 1)
            Else
                'FIXME: Do some real parsing here
                ' Should probably return int/long values for non-floating point numbers
                Value = record
            End If
        End Sub

        Public Sub Input(ByRef Value As Short)
            VerifyFileModes(OpenMode.Input, OpenMode.Binary)
            VerifyReadAccess()
            Value = Conversions.ToShort(ReadRecord())
        End Sub

        Public Sub Input(ByRef Value As Single)
            VerifyFileModes(OpenMode.Input, OpenMode.Binary)
            VerifyReadAccess()
            Value = Conversions.ToSingle(ReadRecord())
        End Sub

        Public Sub Input(ByRef Value As String)
            VerifyFileModes(OpenMode.Input, OpenMode.Binary)
            VerifyReadAccess()
            Value = ReadStringRecord()
        End Sub

        Public Function InputString(ByVal CharCount As Integer) As String
            If CharCount <= 0 AndAlso Mode <> OpenMode.Input AndAlso Mode <> OpenMode.Binary Then
                Throw New NullReferenceException()
            End If

            VerifyReadAccessWeirdly()
            VerifyFileModes(OpenMode.Input, OpenMode.Binary)
            Dim chars(CharCount - 1) As Char
            If Reader.ReadBlock(chars, 0, CharCount) <> CharCount Then
                Throw New EndOfStreamException() 'TODO: Find exact message
            End If
            Dim str As New StringBuilder(CharCount)
            For i As Integer = 0 To chars.Length - 1
                If chars(i) <> Nothing Then str.Append(chars(i))
            Next
            Return str.ToString
        End Function

        Public Function LineInput() As String
            VerifyFileModes(OpenMode.Input, OpenMode.Binary)
            VerifyReadAccess()

            If IsEOF() Then Throw Microsoft.VisualBasic.CompilerServices.ExceptionUtils.GetVBException(VBErrors.ERR62_Input_past_end_of_file)

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
            VerifyFileModes(OpenMode.Append, OpenMode.Output)
            VerifyWriteAccess()
            For i As Integer = 0 To Output.Length - 1
                Dim item As Object = Output(i)

                If Not item Is Nothing Then
                    Dim iConv As IConvertible = Nothing
                    If TypeOf item Is IConvertible Then
                        iConv = DirectCast(item, IConvertible)
                    End If

                    Dim tc As TypeCode
                    If Not iConv Is Nothing Then
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
                        Case TypeCode.Single
                            Writer.Write(DirectCast(item, Single))
                        Case TypeCode.String
                            Writer.Write(DirectCast(item, String))
                        Case TypeCode.SByte
                            Writer.Write(DirectCast(item, SByte))
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
            VerifyFileModes(OpenMode.Append, OpenMode.Output, OpenMode.Random)
            VerifyWriteAccess()
            Print(Output)
            Writer.Write(Constants.vbCrLf)
        End Sub

        Public Function Seek() As Long
            If Stream.Position = Long.MaxValue Then
                Return Long.MaxValue
            Else
                Return Stream.Position + 1
            End If
        End Function

        Public Sub Seek(ByVal Position As Long)
            If Position < 1 Then Position = 1
            Stream.Position = Position - 1
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
            WriteInternal(",", Output)
        End Sub

        Private Sub WriteInternal(ByVal EndValue As String, ByVal ParamArray Output() As Object)
            VerifyFileModes(OpenMode.Append, OpenMode.Output)
            VerifyWriteAccess()

            For i As Integer = 0 To Output.Length - 1
                Dim item As Object = Output(i)

                If item Is Nothing Then
                    Writer.Write("#ERROR 448#")
                Else
                    Dim iConv As IConvertible = Nothing
                    If TypeOf iConv Is IConvertible Then
                        iConv = DirectCast(item, IConvertible)
                    End If
                    Dim tc As TypeCode
                    If Not iConv Is Nothing Then
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
                        Case TypeCode.Single
                            Writer.Write(DirectCast(item, Single))
                        Case TypeCode.String
                            Writer.Write("""")
                            Writer.Write(DirectCast(item, String))
                            Writer.Write("""")
                        Case TypeCode.SByte
                            Writer.Write(DirectCast(item, SByte))
                        Case TypeCode.UInt16
                            Writer.Write(DirectCast(item, UShort))
                        Case TypeCode.UInt32
                            Writer.Write(DirectCast(item, UInteger))
                        Case TypeCode.UInt64
                            Writer.Write(DirectCast(item, ULong))
                        Case TypeCode.Object
                            If TypeOf item Is ErrObject Then
                                Writer.Write("#ERROR " & DirectCast(item, ErrObject).Number.ToString() & "#")
                            Else
                                Throw New NotImplementedException("Can't write object of type: " & item.GetType().FullName)
                            End If
                    End Select
                    End If

                    If i < Output.Length - 1 Then
                        Writer.Write(","c)
                    End If
            Next
            Writer.Write(EndValue)
        End Sub

        Public Sub WriteLine(ByVal ParamArray Output() As Object)
            WriteInternal(Constants.vbCrLf, Output)
        End Sub
    End Class
End Namespace
