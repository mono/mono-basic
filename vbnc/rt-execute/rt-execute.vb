Imports System.Reflection
Imports System
Imports System.Collections
Imports system.Collections.Generic

Module Main
    ''' <summary>
    ''' This is needed because silly windows shows a message box if an application
    ''' throws an unhandled exception, which is very bad for automated unit-testing.
    ''' It can be disabled in the registry (within HKEY_LOCAL_MACHINE), which is not
    ''' very user-friendly, and it changes the setting for the entire machine.
    ''' </summary>
    ''' <param name="args"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Main(ByVal args() As String) As Integer
        Try
            Dim result As Object
            Dim assembly As Assembly = System.Reflection.Assembly.LoadFrom(args(0))
            Dim entrypoint As MethodInfo = assembly.EntryPoint

            If entrypoint Is Nothing Then
                Console.WriteLine("Assembly '" & args(0) & "' does not have an entry point.")
                Return 1
            End If

            Dim argList As New List(Of String)
            argList.AddRange(args)
            argList.RemoveAt(0)
            If entrypoint.GetParameters.Length = 0 Then
                result = entrypoint.Invoke(Nothing, New Object() {})
            Else
                result = entrypoint.Invoke(Nothing, New Object() {argList.ToArray()})
            End If
            If result IsNot Nothing Then
                Return CInt(result)
            End If
        Catch ex As TargetInvocationException
            Console.WriteLine(ex.InnerException.Message)
            Console.WriteLine(ex.InnerException.StackTrace)
            Return 1
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Console.WriteLine(ex.StackTrace)
            Return 1
        End Try
    End Function

End Module
