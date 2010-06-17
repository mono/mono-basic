'
' ErrObject.vb
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
'
Imports System
'Imports System.Runtime.InteropServices
'Imports Microsoft.VisualBasic.CompilerServices
'Imports System.Diagnostics

Namespace Microsoft.VisualBasic
    Public NotInheritable Class ErrObject

        ' 
        ' ErrObject is stored in a thread safe singelton ProjectData.
        '
        Private m_Number As Integer
        Private m_Exception As Exception
        Private m_Description As String
        '
        Private m_HelpContext As System.Int32
        Private m_HelpFile As String
        Private m_Source As String

        ' MappedExceptionToNumber
        ' False: If we don't have a Number, then create a Number from the exception.
        ' True: If we do have a Number, preserve it, and don't create a Number from the exception.
        Private IsMappedExceptionToNumber As Boolean


        '
        ' A Friend constructor. only VB dll can use this class.
        '
        Friend Sub New()
            Clear()
        End Sub

        <System.Runtime.ConstrainedExecution.ReliabilityContract(System.Runtime.ConstrainedExecution.Consistency.WillNotCorruptState, System.Runtime.ConstrainedExecution.Cer.Success)> _
        Public Sub Clear()
            ' storage
            m_Number = 0
            m_Exception = Nothing
            m_Description = ""
            m_Description = ""
            m_HelpContext = 0
            m_HelpFile = ""
            m_Source = ""

            'logic
            IsMappedExceptionToNumber = False
        End Sub

        ' store values in ErrObject
        ' create and throw a new exception object
#If Moonlight = False Then
        Public Sub Raise(ByVal Number As Integer, _
                        Optional ByVal Source As System.Object = Nothing, _
                        Optional ByVal Description As System.Object = Nothing, _
                        Optional ByVal HelpFile As System.Object = Nothing, _
                        Optional ByVal HelpContext As System.Object = Nothing)
#Else
        Public Sub Raise(ByVal Number As Integer, _
                        Optional ByVal Description As System.Object = Nothing)
#End If
            If Number > 65535 Or Number = 0 Then
                Throw New ArgumentException("Argument 'Number' is not a valid value.")
            End If

            Me.Clear()

            ' Raise is enforcing the Number.
            IsMappedExceptionToNumber = True

            ' create an exception and its message
            If Not (Description Is Nothing) Then
                m_Description = Convert.ToString(Description)
            Else
                m_Description = Conversion.ErrorToString(Number)
            End If
            SetExceptionFromNumber(Number, m_Description)

#If Moonlight = False Then
            If Not (Source Is Nothing) Then
                m_Source = Convert.ToString(Source)
                m_Exception.Source = m_Source
            End If

            If Not (HelpFile Is Nothing) Then
                m_HelpFile = Convert.ToString(HelpFile)
                'TODO: implement me: HelpFile
            End If

            If Not (HelpContext Is Nothing) Then
                m_HelpContext = Convert.ToInt32(HelpContext)
                'TODO: implement me: HelpContext
            End If
#End If

            Throw m_Exception

        End Sub
        Public Function GetException() As System.Exception
            Return m_Exception
        End Function
        Public Property Description() As System.String
            Get
                Return m_Description
            End Get
            Set(ByVal Value As System.String)
                m_Description = Value
            End Set
        End Property
        Public ReadOnly Property Erl() As System.Int32
            Get
                'TODO: implement me
                Throw New NotImplementedException
            End Get
        End Property
        Public Property HelpContext() As System.Int32
            Get
                Return m_HelpContext
            End Get
            Set(ByVal Value As System.Int32)
                m_HelpContext = Value
            End Set
        End Property
        Public Property HelpFile() As System.String
            Get
                Return m_HelpFile
            End Get
            Set(ByVal Value As System.String)
                m_HelpFile = Value
            End Set
        End Property
        Public ReadOnly Property LastDllError() As System.Int32
            Get
                'TODO: go native and get the last dll error
                Throw New NotImplementedException
            End Get
        End Property
        Public Property Number() As System.Int32
            Get
#If TRACE Then
                Console.WriteLine("TRACE:ErrObject.Number:get" + m_Number.ToString())
#End If
                Return m_Number
            End Get
            Set(ByVal Value As System.Int32)
                m_Number = Value
            End Set
        End Property
        Public Property Source() As System.String
            Get
                Return m_Source
            End Get
            Set(ByVal Value As System.String)
                m_Source = Value
            End Set
        End Property
        '
        ' helper friend functions
        '
        Friend Sub SetException(ByVal ex As System.Exception)
            m_Exception = ex

            If Not IsMappedExceptionToNumber Then
                m_Number = GetNumberFromException(m_Exception)
                IsMappedExceptionToNumber = True
            End If
        End Sub
        Friend Sub SetExceptionFromNumber(ByVal Number As System.Int32, ByVal Message As String)
            m_Number = Number
            IsMappedExceptionToNumber = True
            m_Exception = GetExceptionFromNumber(Number, Message)
        End Sub
        'according to the number, return an exception
        Friend Shared Function GetExceptionFromNumber(ByVal Number As System.Int32, ByVal Message As String) As Exception

            Select Case Number
                Case 3, 20, 94, 100
                    Return New InvalidOperationException(Message)
                Case 5, 446, 448
                    Return New ArgumentException(Message)
                Case 6
                    Return New OverflowException(Message)
                Case 7, 14
                    Return New OutOfMemoryException(Message)
                Case 9
                    Return New IndexOutOfRangeException(Message)
                Case 11
                    Return New DivideByZeroException(Message)
                Case 13
                    Return New InvalidCastException(Message)
                Case 28
                    Return New StackOverflowException(Message)
                Case 48
                    Return New TypeLoadException(Message)
                Case 52, 54, 55, 57, 58, 59, 61, 63, 67, 68, 70, 71, 74, 75
                    Return New System.IO.IOException(Message)
                Case 53, 76, 432
                    Return New System.IO.FileNotFoundException(Message)
                Case 62
                    Return New System.IO.EndOfStreamException(Message)
                Case 91
                    Return New NullReferenceException(Message)
                Case 422
                    Return New MissingFieldException(Message)
                Case 438
                    Return New MissingMemberException(Message)
                Case Else


                    '57=Device I/O 'or.
                    '58=File already exists.
                    '59=Bad record length.

                    'FIXME: the following numbers has not map to exception. (why is that ?)
                    '10,16,17,18,35,47,49,51
                    Return New Exception(Message)
            End Select

        End Function
        ' according to exception, return a number.
        ' since GetExceptionFromNumber supports many-to-one maps,
        ' this function is not mapping one-to-one as with GetExceptionFromNumber.
        ' If the ErrObject already contain a Number, we should not use this function.

        ' FIXME: Map all the exceptions from GetExceptionFromNumber back to number.
        ' TODO: add tests which generates all these exceptions and check their number.
        Private Function GetNumberFromException(ByVal ex As Exception) As System.Int32

            If TypeOf ex Is ArgumentException Then
                Return 5
            ElseIf TypeOf ex Is OverflowException Then
                Return 6
            ElseIf TypeOf ex Is IndexOutOfRangeException Then
                Return 9
            Else
                'something went wrong. return ArgumentException
                Return 5
            End If

        End Function

    End Class
End Namespace

