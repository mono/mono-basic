' 
' Visual Basic.Net Compiler
' Copyright (C) 2004 - 2007 Rolf Bjarne Kvinge, RKvinge@novell.com
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

Option Compare Text

Public Class ConditionalConstant
    ''' <summary>
    ''' The name of the constant. The names are not case sensitive.
    ''' </summary>
    ''' <remarks></remarks>
    Public Name As String = ""
    ''' <summary>
    ''' The value of the constant. If a string value, it is case sensitive
    ''' Possible types: Boolean, Double (no integer values, nor Decimal), Date, String
    ''' </summary>
    ''' <remarks></remarks>
    Public Value As Object

    Private m_Compiler As Compiler

    ReadOnly Property Compiler() As Compiler
        Get
            Return m_Compiler
        End Get
    End Property

    Sub New(ByVal Compiler As Compiler)
        m_Compiler = Compiler
    End Sub

    ReadOnly Property ConstantType() As Type
        Get
            If Value Is Nothing Then
                Return Compiler.TypeCache.System_Object
            ElseIf TypeOf Value Is Date Then
                Return Compiler.TypeCache.System_DateTime
            ElseIf TypeOf Value Is String Then
                Return Compiler.TypeCache.System_String
            ElseIf TypeOf Value Is Boolean Then
                Return Compiler.TypeCache.System_Boolean
            Else
                Helper.Assert(VB.IsNumeric(Value))
                Return Compiler.TypeCache.System_Double
            End If
        End Get
    End Property

    ReadOnly Property IsDefined() As Boolean
        Get
            Return CBool(Value)
        End Get
    End Property

    Function AsBoolean() As Boolean
        Helper.Assert(ConstantType Is Compiler.TypeCache.System_Boolean)
        Return CBool(Value)
    End Function

    Function AsDouble() As Double
        Helper.Assert(ConstantType Is Compiler.TypeCache.System_Double)
        Return CDbl(Value)
    End Function

    Function AsString() As String
        Helper.Assert(ConstantType Is Compiler.TypeCache.System_String)
        Return CStr(Value)
    End Function

    Function AsObject() As Object
        Helper.Assert(ConstantType Is Compiler.TypeCache.System_Object)
        Helper.Assert(Value Is Nothing)
        Return Value
    End Function

    Function AsDate() As Date
        Helper.Assert(ConstantType Is Compiler.TypeCache.System_DateTime)
        Return CDate(Value)
    End Function

    Sub New(ByVal Name As String, ByVal Value As Object)
        Me.Name = Name
        Me.Value = Value
    End Sub

    Sub Dump(ByVal Dumper As IndentedTextWriter)
        Dumper.WriteLine(Name & " = " & Helper.ValueToCodeConstant(Value))
    End Sub

    ''' <summary>
    ''' Returns the name of the constant.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Overrides Function ToString() As String
        Return Name
    End Function

    'TODO: Conditional operators.
    ''' <summary>
    ''' Compares the values of the conditional constants
    ''' </summary>
    ''' <param name="Const1"></param>
    ''' <param name="Const2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Operator =(ByVal Const1 As ConditionalConstant, ByVal Const2 As ConditionalConstant) As Boolean
        Dim tp1 As TypeCode
        Dim tp2 As TypeCode
        tp1 = Helper.GetTypeCode(Const1.Compiler, Const1.ConstantType)
        tp2 = Helper.GetTypeCode(Const1.Compiler, Const2.ConstantType)
        Select Case tp1
            Case TypeCode.Object
                Select Case tp2
                    Case TypeCode.Object
                        Return True 'Nothing = Nothing
                    Case TypeCode.String
                        Return Const2.AsString = "" 'Nothing = ""
                    Case TypeCode.Double
                        Return Const2.AsDouble = 0 'Nothing = 0
                    Case TypeCode.DateTime
                        Throw New InternalException("")
                    Case TypeCode.Boolean
                        Return Const2.AsBoolean = False 'Nothing = False
                    Case Else
                        Throw New InternalException("Unhandled comparison!")
                End Select
            Case TypeCode.DateTime
                Select Case tp2
                    Case TypeCode.Object
                        Throw New InternalException("")
                    Case TypeCode.String, TypeCode.Double, TypeCode.Boolean
                        'Cannot convert from '{0}' to '{1}' in a constant expression.
                        Const1.Compiler.Report.ShowMessage(Messages.VBNC30060, tp1.ToString, tp2.ToString)
                        Return False
                    Case TypeCode.DateTime
                        Return Const1.AsDate = Const2.AsDate
                    Case Else
                        Throw New InternalException("Unhandled comparison!")
                End Select
            Case TypeCode.String
                Select Case tp2
                    Case TypeCode.Object
                        Return Const1.AsString = "" '"" = Nothing
                    Case TypeCode.String
                        Return String.Equals(Const1.AsString, Const2.AsString, StringComparison.InvariantCultureIgnoreCase) ' String = String
                    Case TypeCode.Double, TypeCode.DateTime, TypeCode.Boolean
                        'Cannot convert from '{0}' to '{1}' in a constant expression.
                        Const1.Compiler.Report.ShowMessage(Messages.VBNC30060, tp1.ToString, tp2.ToString)
                    Case Else
                        Throw New InternalException("Unhandled comparison!")
                End Select
            Case TypeCode.Boolean
                Select Case tp2
                    Case TypeCode.Object
                        Return Const1.AsBoolean = False 'False = Nothing
                    Case TypeCode.String, TypeCode.Double, TypeCode.DateTime
                        'Cannot convert from '{0}' to '{1}' in a constant expression.
                        Const1.Compiler.Report.ShowMessage(Messages.VBNC30060, tp1.ToString, tp2.ToString)
                    Case TypeCode.Boolean
                        Return Const1.AsBoolean = Const2.AsBoolean 'Boolean = Boolean
                    Case Else
                        Throw New InternalException("Unhandled comparison!")
                End Select
            Case Else
                Throw New InternalException("Wrong conditional type code.")
        End Select
        'Return Const1.Value = Const2.Value
    End Operator

    ''' <summary>
    ''' Compares the values of the conditional constants
    ''' </summary>
    ''' <param name="Const1"></param>
    ''' <param name="Const2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Operator <>(ByVal Const1 As ConditionalConstant, ByVal Const2 As ConditionalConstant) As Boolean
        Return Not Const1 = Const2
    End Operator

End Class
