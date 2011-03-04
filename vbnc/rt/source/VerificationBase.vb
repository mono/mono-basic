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

<System.ComponentModel.TypeConverter(GetType(System.ComponentModel.ExpandableObjectConverter))> _
Public MustInherit Class VerificationBase
    Private m_Result As Boolean
    Private m_Run As Boolean
    Private m_Test As Test
    Private m_DescriptiveMessage As String
    Private m_ExpectedExitCode As Integer
    Private m_ExpectedErrors As Generic.List(Of ErrorInfo)

    Private m_Name As String = "Verification"

    Property ExpectedExitCode() As Integer
        Get
            Return m_ExpectedExitCode
        End Get
        Set(ByVal value As Integer)
            m_ExpectedExitCode = value
        End Set
    End Property

    Property ExpectedErrors() As Generic.List(Of ErrorInfo)
        Get
            Return m_ExpectedErrors
        End Get
        Set(ByVal value As Generic.List(Of ErrorInfo))
            m_ExpectedErrors = value
        End Set
    End Property

    Property Name() As String
        Get
            Return m_Name
        End Get
        Set(ByVal value As String)
            m_Name = value
        End Set
    End Property

    ''' <summary>
    ''' A descriptive message of the verification, normally just StdOut.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DescriptiveMessage() As String
        Get
            Return m_DescriptiveMessage
        End Get
        Set(ByVal value As String)
            m_DescriptiveMessage = value
        End Set
    End Property


    ''' <summary>
    ''' Has this verification been done?
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Run() As Boolean
        Get
            Return m_Run
        End Get
        Set(ByVal value As Boolean)
            m_Run = value
        End Set
    End Property

    ''' <summary>
    ''' The result of the verification.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Result() As Boolean
        Get
            Return m_Result
        End Get
        Set(ByVal value As Boolean)
            m_Result = value
        End Set
    End Property

    ''' <summary>
    ''' The test to verify.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Test() As Test
        Get
            Return m_Test
        End Get
    End Property

    ''' <summary>
    ''' Creates a new verifier.
    ''' </summary>
    ''' <param name="Test">The test to verify.</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal Test As Test)
        m_Test = Test
    End Sub

    Protected MustOverride Function RunVerification() As Boolean

    Public Function Verify() As Boolean
        m_Result = RunVerification()
        m_Run = True
        Return m_Result
    End Function

    Public Overrides Function ToString() As String
        Return Name
    End Function
End Class
