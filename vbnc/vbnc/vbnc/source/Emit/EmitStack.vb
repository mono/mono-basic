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


#If DEBUG Then
#Const DEBUGSTACKTYPES = 0
#Const LOGDEBUGSTACKTYPES = 0
#End If

Public Class EmitStack
    Inherits Generic.Stack(Of Type)

    Private m_Compiler As Compiler

    Shadows Function Peek() As Type
        Return MyBase.Peek
    End Function

    '<Diagnostics.Conditional("DEBUG")> _
    Public Sub SwitchHead(ByVal FromType As Type, ByVal ToType As Type)
        Me.Pop(FromType)
        Me.Push(ToType)
    End Sub

    <Diagnostics.Conditional("DEBUG")> Sub CheckStackEmpty(ByVal MessageOnError As String)
        If Count > 0 Then
            Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, MessageOnError)
            Helper.StopIfDebugging(False)
            Clear()
        End If
    End Sub

    '<Diagnostics.Conditional("DEBUG")> _
    Shadows Sub Clear()
#If LOGDEBUGSTACKTYPES Then
        Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, "Clearing the stack!")
#End If
        MyBase.Clear()
    End Sub

    Sub New(ByVal Compiler As Compiler)
        MyBase.New()
        m_Compiler = Compiler
    End Sub

    ReadOnly Property Compiler() As Compiler
        Get
            Return m_Compiler
        End Get
    End Property

    '<Diagnostics.Conditional("DEBUG")> _
    Shadows Sub Push(ByVal Type As Type)
        MyBase.Push(Type)
#If LOGDEBUGSTACKTYPES Then
        Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, String.Format("Pushed stack type ({1} left on the stack): {0,-30} Stack now: {2}", Type.ToString, Count.ToString, StackState))
#End If
    End Sub

    Private Function StackState() As String
        Dim tmp() As Object = ToArray()
        Dim cnt As Integer
        Dim result As String = ""

        For i As Integer = 0 To tmp.GetUpperBound(0)
            result = String.Format("{0}", DirectCast(tmp(i), Type).ToString) & ", " & result
            cnt += 1
        Next
        If result.Length > 0 Then result = result.Substring(0, result.Length - 2)
        Return result
    End Function

    '<Diagnostics.Conditional("DEBUG")> _
    Shadows Sub Pop(ByVal Type As Type)
        Dim logStack As Boolean = False

#If LOGDEBUGSTACKTYPES Then
        logStack = True
#End If

#If DEBUGSTACKTYPES Then
        If Count = 0 Then
            Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, String.Format("Trying to pop type '{0}' off the stack, but stack is empty.", Type.ToString))
            'Helper.Stop()
            Return Nothing
        End If

        Dim tmp As Type
        tmp = MyBase.Pop()

        If Helper.CompareType(tmp, Type) = False AndAlso Helper.IsAssignable(Compiler, tmp, Type) = False AndAlso Helper.IsInterface(Compiler, Type) = False Then
            If Helper.IsEnum(tmp) = False Then 'OrElse Helper.CompareType(tmp.GetField("value__").FieldType, Type) = False Then
                If Type.ToString <> tmp.ToString Then
                    If Type.ContainsGenericParameters = False AndAlso tmp.ContainsGenericParameters Then
                        Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, String.Format("Trying to pop type '{0}' when type on the stack is '{1}'.", Type.ToString, tmp.ToString))

                        Helper.StopIfDebugging(False)
                    End If
                End If
            End If
        End If
        If logStack Then
            Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, String.Format("Popped stack type ({1} left on the stack): {0,-30} Stack now: {2}", tmp.ToString, Count.ToString, StackState))
        End If
        Return tmp
#Else
        If Count > 0 Then
            MyBase.Pop()
        Else
            'Return Nothing
        End If
#End If
    End Sub

    ''' <summary>
    ''' Are there any values left on the stack?
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    ReadOnly Property IsStackEmpty() As Boolean
        Get
            Return Count = 0
        End Get
    End Property
End Class
