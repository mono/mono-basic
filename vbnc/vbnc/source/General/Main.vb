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

Public Class Main
    ''' <summary>
    ''' The main function!
    ''' </summary>
    ''' <param name="CmdArgs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function Main(ByVal CmdArgs() As String) As Integer
        Dim start As Date = Date.Now
        Try
            Dim result As Integer = -2
            Dim Compiler As Compiler

#If DEBUG Then
            Try
                System.Console.SetWindowSize(150, 50)
                System.Console.ForegroundColor = ConsoleColor.Green
                System.Console.WriteLine(VB.vbNewLine & New String(" "c, 50) & "DEBUG RUN" & VB.vbNewLine)
                System.Console.ResetColor()
                System.Console.SetBufferSize(150, 3000)
            Catch
                'Ignore all exceptions.
            End Try
#End If

            Compiler = New Compiler()

#If DEBUG Then
            Dim debugwriter As New IO.StringWriter()
            Compiler.Report.Listeners.Add(New System.Diagnostics.TextWriterTraceListener(debugwriter))
#End If

            result = Compiler.Compile(CmdArgs)

#If DEBUG Then
            Global.System.Diagnostics.Debug.WriteLine("")
            Global.System.Diagnostics.Debug.WriteLine( _
            "***************************************** Console output: ***************************************** ")
            Global.System.Diagnostics.Debug.WriteLine(debugwriter.ToString)
            Global.System.Diagnostics.Debug.WriteLine( _
            "*************************************************************************************************** ")
#End If

            Console.WriteLine("Compilation took " & (Date.Now.Subtract(start)).ToString())
            System.Diagnostics.Debug.WriteLine("Compilation took " & (Date.Now.Subtract(start)).ToString())
#If DEBUG Then
            'Console.WriteLine("With " & GC.CollectionCount(0) & " 0 gen collections")
            'Console.WriteLine("With " & GC.CollectionCount(1) & " 1 gen collections")
            'Console.WriteLine("With " & GC.CollectionCount(2) & " 2 gen collections")
            'System.Diagnostics.Debug.WriteLine("With " & GC.CollectionCount(0) & " 0 gen collections")
            'System.Diagnostics.Debug.WriteLine("With " & GC.CollectionCount(1) & " 1 gen collections")
            'System.Diagnostics.Debug.WriteLine("With " & GC.CollectionCount(2) & " 2 gen collections")
            '#End If
#End If
            Return result
        Catch ex As TooManyErrorsException
            Return 1 'An appropiate message has already been shown to the user
        Catch ex As vbncException
            Console.WriteLine(ex.Message & VB.vbNewLine & ex.StackTrace)
            Console.WriteLine("Failed compilation took " & (Date.Now.Subtract(start)).ToString())
            System.Diagnostics.Debug.WriteLine("Failed compilation took " & (Date.Now.Subtract(start)).ToString())
            'Console.WriteLine("With " & GC.CollectionCount(0) & " 0 gen collections")
            'Console.WriteLine("With " & GC.CollectionCount(1) & " 1 gen collections")
            'Console.WriteLine("With " & GC.CollectionCount(2) & " 2 gen collections")
            Return 1 'The exception has already been shown to the user.
        Catch ex As Exception
            Console.WriteLine(ex.Message & VB.vbNewLine & ex.StackTrace)
            Console.WriteLine("Failed compilation took " & (Date.Now.Subtract(start)).ToString())
            System.Diagnostics.Debug.WriteLine("Failed compilation took " & (Date.Now.Subtract(start)).ToString())
            'Console.WriteLine("With " & GC.CollectionCount(0) & " 0 gen collections")
            'Console.WriteLine("With " & GC.CollectionCount(1) & " 1 gen collections")
            'Console.WriteLine("With " & GC.CollectionCount(2) & " 2 gen collections")
            Return 1
        End Try
        Helper.Assert(False, "End of program reached!")
        Return 1
    End Function
End Class
