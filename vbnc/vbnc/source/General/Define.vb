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

''' <summary>
''' A define as specified on the command line.
''' </summary>
Public Class Define
    ''' <summary>
    ''' The symbol of this define.
    ''' </summary>
    Private m_Symbol As String

    ''' <summary>
    ''' The value of this define.
    ''' </summary>
    Private m_Value As String
    Private m_ObjectValue As Object

    Private m_Compiler As Compiler

    ReadOnly Property ObjectValue() As Object
        Get
            Return m_ObjectValue
        End Get
    End Property

    ReadOnly Property ValueAsDouble() As Double
        Get
            Return CDbl(m_ObjectValue)
        End Get
    End Property

    ReadOnly Property ValueAsString() As String
        Get
            Return CStr(m_ObjectValue)
        End Get
    End Property

    ReadOnly Property ValueAsDate() As Date
        Get
            Return CDate(m_ObjectValue)
        End Get
    End Property

    ReadOnly Property ValueAsBoolean() As Boolean
        Get
            Return CBool(m_ObjectValue)
        End Get
    End Property

    ''' <summary>
    ''' The symbol of this define.
    ''' </summary>
    ReadOnly Property Symbol() As String
        Get
            Return m_Symbol
        End Get
    End Property

    ''' <summary>
    ''' The value of this define.
    ''' </summary>
    ReadOnly Property Value() As String
        Get
            Return m_Value
        End Get
    End Property

    ''' <summary>
    ''' Create a new define with the specified values.
    ''' </summary>
    Sub New(ByVal Compiler As Compiler, ByVal Symbol As String, ByVal Value As String)
        Me.m_Symbol = Symbol
        Me.m_Value = Value
        Me.m_Compiler = Compiler

        Parse()
    End Sub

    ''' <summary>
    ''' Some decent parsing is needed here.
    ''' </summary>
    ''' <remarks></remarks>
    Sub Parse()
        If m_Value = String.Empty Then
            m_ObjectValue = Nothing
            Return
        End If

        If Helper.CompareName(m_Value, "Nothing") Then
            m_ObjectValue = Nothing
            Return
        ElseIf Helper.CompareName(m_Value, "True") Then
            m_ObjectValue = True
            Return
        ElseIf Helper.CompareName(m_Value, "False") Then
            m_ObjectValue = False
            Return
        End If

        If m_Value.StartsWith("#") Then
            If m_Value.EndsWith("#") Then
                m_ObjectValue = DateTime.Parse(m_Value.Substring(1, m_Value.Length - 2))
                Return
            Else
                Helper.AddError(Compiler, Span.CommandLineSpan, "Invalid date constant: " & m_Value)
            End If
        End If

        If m_Value.StartsWith("""") Then
            If m_Value.EndsWith("""") Then
                m_ObjectValue = m_Value.Substring(1, m_Value.Length - 2)
                Return
            Else
                Helper.AddError(Compiler, Span.CommandLineSpan, "Invalid string constant: " & m_Value)
            End If
        End If

        If True OrElse Microsoft.VisualBasic.IsNumeric(m_Value) Then
            m_ObjectValue = VB.Val(m_Value)
            Return
        End If

        Helper.AddError(Compiler, Span.CommandLineSpan, "Invalid constant: '" & m_Value & "' (Type=" & m_Value.GetType.FullName & ")")
    End Sub

    ReadOnly Property Compiler() As Compiler
        Get
            Return m_Compiler
        End Get
    End Property
End Class