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

Public Class MyGenerator
    Private m_Compiler As Compiler
    Private m_Code As System.Text.StringBuilder
    Private m_ProjectCode As System.Text.StringBuilder
    Private m_MyType As MyTypes
    Private m_GlobalMy As String

    Sub New(ByVal Compiler As Compiler)
        m_Compiler = Compiler
    End Sub

    ReadOnly Property Compiler() As Compiler
        Get
            Return m_Compiler
        End Get
    End Property

    ReadOnly Property Code() As System.Text.StringBuilder
        Get
            If m_Code Is Nothing Then m_Code = New System.Text.StringBuilder()
            Return m_Code
        End Get
    End Property

    ReadOnly Property ProjectCode() As System.Text.StringBuilder
        Get
            If m_ProjectCode Is Nothing Then m_ProjectCode = New System.Text.StringBuilder
            Return m_ProjectCode
        End Get
    End Property

    Function Generate() As Boolean
        Dim result As Boolean = True
        Dim _MyTypeDefine As Define
        Dim _MyType As String

        If String.IsNullOrEmpty(Compiler.CommandLine.VBRuntime) Then Return result

        _MyTypeDefine = Compiler.CommandLine.Define("_MYTYPE")
        If _MyTypeDefine Is Nothing Then
            _MyType = String.Empty
        Else
            _MyType = _MyTypeDefine.ValueAsString
        End If

        If _MyType = String.Empty Then
            m_MyType = MyTypes.Windows
        ElseIf [Enum].IsDefined(GetType(MyTypes), _MyType) Then
            m_MyType = CType([Enum].Parse(GetType(MyTypes), _MyType, False), MyTypes)
        Else
            m_MyType = MyTypes.Empty
        End If

        If m_MyType = MyTypes.Empty Then
            Return True
        End If

        If Compiler.CommandLine.RootNamespace <> "" Then
            m_GlobalMy = "Global." & Compiler.CommandLine.RootNamespace & ".My"
        Else
            m_GlobalMy = "Global" & ".My"
        End If

        result = GenerateMyApplication() AndAlso result
        result = GenerateMyComputer() AndAlso result
        result = GenerateMyForms() AndAlso result
        result = GenerateMyLog() AndAlso result
        result = GenerateMyRequest() AndAlso result
        result = GenerateMyResources() AndAlso result
        result = GenerateMyResponse() AndAlso result
        result = GenerateMySettings() AndAlso result
        result = GenerateMyUser() AndAlso result
        result = GenerateMyWebServices() AndAlso result

        If Code.Length > 0 OrElse m_MyType = MyTypes.Custom Then
            Dim projectPrepend As New System.Text.StringBuilder()
            projectPrepend.AppendLine("    <Global.System.CodeDom.Compiler.GeneratedCode(""MyTemplate"", ""11.0.0.0"")> _")
            projectPrepend.AppendLine("    <Global.Microsoft.VisualBasic.HideModuleName> _")
            projectPrepend.AppendLine("    Friend Module MyProject")
            projectPrepend.AppendLine("        <Global.System.ComponentModel.EditorBrowsable(Global.System.ComponentModel.EditorBrowsableState.Never)> _")
            projectPrepend.AppendLine("        <Global.System.Runtime.InteropServices.ComVisible(False)> _")
            projectPrepend.AppendLine("        Friend NotInheritable Class ThreadSafeObjectProvider(Of T As New)")
            projectPrepend.AppendLine("            ")
            projectPrepend.AppendLine("            <Global.System.ComponentModel.EditorBrowsable(Global.System.ComponentModel.EditorBrowsableState.Never)> _")
            projectPrepend.AppendLine("            <Global.System.Diagnostics.DebuggerHidden> _")
            projectPrepend.AppendLine("            Public Sub New()")
            projectPrepend.AppendLine("            End Sub")
            projectPrepend.AppendLine("            ")
            If Compiler.CommandLine.Target = CommandLine.Targets.Library Then
                projectPrepend.AppendLine("            Private ReadOnly m_Context As New Global.Microsoft.VisualBasic.MyServices.Internal.ContextValue(Of T)")
                projectPrepend.AppendLine("            Friend ReadOnly Property GetInstance As T")
                projectPrepend.AppendLine("                Get")
                projectPrepend.AppendLine("                    Dim tmp as T = m_Context.Value")
                projectPrepend.AppendLine("                    If tmp Is Nothing Then")
                projectPrepend.AppendLine("                        tmp = Global.System.Activator.CreateInstance(Of T)")
                projectPrepend.AppendLine("                        m_Context.Value = tmp")
                projectPrepend.AppendLine("                    End If")
                projectPrepend.AppendLine("                    Return tmp")
                projectPrepend.AppendLine("                End Get")
                projectPrepend.AppendLine("            End Property")
            Else
                projectPrepend.AppendLine("            <Global.System.Runtime.CompilerServices.CompilerGenerated> _")
                projectPrepend.AppendLine("            <Global.System.ThreadStatic> _")
                projectPrepend.AppendLine("            Private Shared m_ThreadStaticValue As T")
                projectPrepend.AppendLine("            ")
                projectPrepend.AppendLine("            Friend ReadOnly Property GetInstance As T")
                projectPrepend.AppendLine("                Get")
                projectPrepend.AppendLine("                    If (m_ThreadStaticValue Is Nothing) Then")
                projectPrepend.AppendLine("                        m_ThreadStaticValue = Global.System.Activator.CreateInstance(Of T)")
                projectPrepend.AppendLine("                    End If")
                projectPrepend.AppendLine("                    Return m_ThreadStaticValue")
                projectPrepend.AppendLine("                End Get")
                projectPrepend.AppendLine("            End Property")
            End If
            projectPrepend.AppendLine("        End Class")
            ProjectCode.Insert(0, projectPrepend)
            ProjectCode.AppendLine("    End Module")

            Code.Insert(0, "Namespace My" & Environment.NewLine)
            Code.Insert(0, "    Imports System" & Environment.NewLine)
            Code.Append(ProjectCode)
            Code.AppendLine("End Namespace")

            Code.Replace("$GLOBALMY$", m_GlobalMy)

#If DEBUG AndAlso False Then
            Dim counter As Integer = 1
            For Each line As String In VB.Split(Code.ToString, VB.vbNewLine)
                Compiler.Report.WriteLine(counter & ": " & line)
                counter += 1
            Next
#End If
            Compiler.CommandLine.Files.Add(New CodeFile("<MyGenerator>", "<MyGenerator>", Compiler, Code.ToString))
        End If
        Return result
    End Function

    Function GenerateMyApplication() As Boolean
        Dim result As Boolean = True
        Dim _MyApplicationDefine As Define
        Dim _MyApplication As String

        _MyApplicationDefine = Compiler.CommandLine.Define("_MYAPPLICATIONTYPE")
        If _MyApplicationDefine Is Nothing Then
            Select Case m_MyType
                Case MyTypes.Console, MyTypes.WindowsFormsWithCustomSubMain
                    _MyApplication = "Console"
                Case MyTypes.Windows
                    _MyApplication = "Windows"
                Case MyTypes.WindowsForms
                    _MyApplication = "WindowsForms"
                Case Else
                    _MyApplication = String.Empty
            End Select
        Else
            _MyApplication = _MyApplicationDefine.Value
        End If

        Dim baseClass As String
        Select Case _MyApplication
            Case "Console"
                baseClass = "Global.Microsoft.VisualBasic.ApplicationServices.ConsoleApplicationBase"
            Case "Windows"
                baseClass = "Global.Microsoft.VisualBasic.ApplicationServices.ApplicationBase"
            Case "WindowsForms"
                baseClass = "Global.Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase"
            Case Else
                Return True
        End Select

        Code.AppendLine("    <Global.System.ComponentModel.EditorBrowsable(Global.System.ComponentModel.EditorBrowsableState.Never)> _")
        Code.AppendLine("    <Global.System.CodeDom.Compiler.GeneratedCode(""MyTemplate"", ""11.0.0.0"")> _")
        Code.AppendLine("    Friend Class MyApplication")
        Code.Append("        Inherits ") : Code.AppendLine(baseClass)
        'Code.AppendLine("        Public Sub New()")
        'Code.AppendLine("        End Sub")
        If Compiler.CommandLine.Target = CommandLine.Targets.Winexe AndAlso baseClass = "Global.Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase" Then
            Code.AppendLine("        <Global.System.ComponentModel.EditorBrowsable(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _")
            Code.AppendLine("        <Global.System.Diagnostics.DebuggerHidden()> _")
            Code.AppendLine("        <Global.System.STAThread()> _")
            Code.AppendLine("        Friend Shared Sub Main(ByVal Args As String())")
            Code.AppendLine("            Global.System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(Global.Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase.UseCompatibleTextRendering)")
            Code.AppendLine("            $GLOBALMY$.MyProject.Application.Run(Args)")
            Code.AppendLine("        End Sub")
        End If
        Code.AppendLine("    End Class")

        ProjectCode.AppendLine("        Private Shared ReadOnly m_AppObjectProvider As ThreadSafeObjectProvider(Of $GLOBALMY$.MyApplication) = New ThreadSafeObjectProvider(Of $GLOBALMY$.MyApplication)")
        ProjectCode.AppendLine("        <Global.System.ComponentModel.Design.HelpKeyword(""My.Application"")> _")
        ProjectCode.AppendLine("        Friend Shared ReadOnly Property Application As $GLOBALMY$.MyApplication")
        ProjectCode.AppendLine("            <Global.System.Diagnostics.DebuggerHidden()> _")
        ProjectCode.AppendLine("            Get")
        ProjectCode.AppendLine("                Return m_AppObjectProvider.GetInstance")
        ProjectCode.AppendLine("            End Get")
        ProjectCode.AppendLine("        End Property")

        Return result
    End Function

    Function GenerateMyUser() As Boolean
        Dim result As Boolean = True
        Dim _MyUserDefine As Define
        Dim _MyUser As String

        _MyUserDefine = Compiler.CommandLine.Define("_MYUSERTYPE")
        If _MyUserDefine Is Nothing Then
            Select Case m_MyType
                Case MyTypes.Console, MyTypes.WindowsFormsWithCustomSubMain, MyTypes.Windows, MyTypes.WindowsForms
                    _MyUser = "Windows"
                Case MyTypes.Web, MyTypes.WebControl
                    _MyUser = "Web"
                Case Else
                    _MyUser = String.Empty
            End Select
        Else
            _MyUser = _MyUserDefine.Value
        End If

        Dim baseClass As String
        Select Case _MyUser
            Case "Web"
                baseClass = "Global.Microsoft.VisualBasic.ApplicationServices.WebUser"
            Case "Windows"
                baseClass = "Global.Microsoft.VisualBasic.ApplicationServices.User"
            Case Else
                Return True
        End Select

        ProjectCode.AppendLine("        Private Shared ReadOnly m_UserObjectProvider As ThreadSafeObjectProvider(Of Z) = New ThreadSafeObjectProvider(Of Z)".Replace("Z", baseClass))
        ProjectCode.AppendLine("        <Global.System.ComponentModel.Design.HelpKeyword(""My.User"")> _")
        ProjectCode.Append("        Friend Shared ReadOnly Property User As ") : ProjectCode.AppendLine(baseClass)
        ProjectCode.AppendLine("            <Global.System.Diagnostics.DebuggerHidden()> _")
        ProjectCode.AppendLine("            Get")
        ProjectCode.AppendLine("                Return m_UserObjectProvider.GetInstance")
        ProjectCode.AppendLine("            End Get")
        ProjectCode.AppendLine("        End Property")
        Return result
    End Function

    Function GenerateMyForms() As Boolean
        Dim result As Boolean = True
        Dim _MyFormsDefine As Define
        Dim _MyForms As Boolean

        _MyFormsDefine = Compiler.CommandLine.Define("_MYFORMS")
        If _MyFormsDefine Is Nothing Then
            Select Case m_MyType
                Case MyTypes.WindowsFormsWithCustomSubMain, MyTypes.WindowsForms
                    _MyForms = True
                Case Else
                    _MyForms = False
            End Select
        Else
            _MyForms = CBool(_MyFormsDefine.Value)
        End If

        If Not _MyForms Then Return True

        Compiler.CommandLine.References.Add("System.Windows.Forms.dll")

        Dim code As String = VB.vbNewLine & _
       "        <Global.System.ComponentModel.EditorBrowsable(Global.System.ComponentModel.EditorBrowsableState.Never)> _" & VB.vbNewLine & _
       "        <Global.Microsoft.VisualBasic.MyGroupCollection(""System.Windows.Forms.Form"", ""Create__Instance__"", ""Dispose__Instance__"", ""My.MyProject.Forms"")> _" & VB.vbNewLine & _
       "        Friend NotInheritable Class MyForms" & VB.vbNewLine & _
       "            <Global.System.ThreadStatic> _" & VB.vbNewLine & _
       "            Private Shared m_FormBeingCreated As Global.System.Collections.Hashtable" & VB.vbNewLine & _
       "            " & VB.vbNewLine & _
       "            <Global.System.ComponentModel.EditorBrowsable(Global.System.ComponentModel.EditorBrowsableState.Never)> _" & VB.vbNewLine & _
       "            <Global.System.Diagnostics.DebuggerHidden> _" & VB.vbNewLine & _
       "            Public Sub New()" & VB.vbNewLine & _
       "            End Sub" & VB.vbNewLine & _
       "            " & VB.vbNewLine & _
       "            <Global.System.Diagnostics.DebuggerHidden> _" & VB.vbNewLine & _
       "            Private Shared Function Create__Instance__(Of T As {Global.System.Windows.Forms.Form, New})(ByVal Instance As T) As T" & VB.vbNewLine & _
       "                If Instance IsNot Nothing AndAlso Instance.IsDisposed = False" & VB.vbNewLine & _
       "                    Return Instance" & VB.vbNewLine & _
       "                End If" & VB.vbNewLine & _
       "                " & VB.vbNewLine & _
       "                Dim TType As Type = GetType(T)" & VB.vbNewLine & _
       "                If m_FormBeingCreated Is Nothing Then" & VB.vbNewLine & _
       "                    m_FormBeingCreated = New Global.System.Collections.Hashtable()" & VB.vbNewLine & _
       "                ElseIf m_FormBeingCreated.ContainsKey(TType) Then" & VB.vbNewLine & _
       "                    Throw New InvalidOperationException(""There is a reference to a default instance from the constructor of a form, which leads to infinite recursion. Please refer to the form itself using 'Me' from within the constructor."")" & VB.vbNewLine & _
       "                End If" & VB.vbNewLine & _
       "                " & VB.vbNewLine & _
       "                m_FormBeingCreated.Add(TType, Nothing)" & VB.vbNewLine & _
       "                Try" & VB.vbNewLine & _
       "                    Return Global.System.Activator.CreateInstance(Of T)()" & VB.vbNewLine & _
       "                Catch ex As Global.System.Reflection.TargetInvocationException" & VB.vbNewLine & _
       "                    Throw New Global.System.InvalidOperationException(""See inner exception"", ex.InnerException)" & VB.vbNewLine & _
       "                Finally" & VB.vbNewLine & _
       "                    m_FormBeingCreated.Remove(TType)" & VB.vbNewLine & _
       "                End Try" & VB.vbNewLine & _
       "                Return Nothing" & VB.vbNewLine & _
       "            End Function" & VB.vbNewLine & _
       "            " & VB.vbNewLine & _
       "            <Global.System.Diagnostics.DebuggerHidden> _" & VB.vbNewLine & _
       "            Private Sub Dispose__Instance__(Of T As Global.System.Windows.Forms.Form)(ByRef instance As T)" & VB.vbNewLine & _
       "                instance.Dispose()" & VB.vbNewLine & _
       "                instance = CType(Nothing, T)" & VB.vbNewLine & _
       "            End Sub" & VB.vbNewLine & _
       "            " & VB.vbNewLine & _
       "            <Global.System.ComponentModel.EditorBrowsable(Global.System.ComponentModel.EditorBrowsableState.Never)> _" & VB.vbNewLine & _
       "            Public Overrides Function Equals(ByVal o As Object) As Boolean" & VB.vbNewLine & _
       "                Return MyBase.Equals(Global.System.Runtime.CompilerServices.RuntimeHelpers.GetObjectValue(o))" & VB.vbNewLine & _
       "            End Function" & VB.vbNewLine & _
       "            " & VB.vbNewLine & _
       "            <Global.System.ComponentModel.EditorBrowsable(Global.System.ComponentModel.EditorBrowsableState.Never)> _" & VB.vbNewLine & _
       "            Public Overrides Function GetHashCode() As Integer" & VB.vbNewLine & _
       "             Return MyBase.GetHashCode" & VB.vbNewLine & _
       "            End Function" & VB.vbNewLine & _
       "            " & VB.vbNewLine & _
       "            <Global.System.ComponentModel.EditorBrowsable(Global.System.ComponentModel.EditorBrowsableState.Never)> _" & VB.vbNewLine & _
       "            Friend Overloads Function [GetType]() As Type" & VB.vbNewLine & _
       "                Return GetType(MyForms)" & VB.vbNewLine & _
       "            End Function" & VB.vbNewLine & _
       "            " & VB.vbNewLine & _
       "            <Global.System.ComponentModel.EditorBrowsable(Global.System.ComponentModel.EditorBrowsableState.Never)> _" & VB.vbNewLine & _
       "            Public Overrides Function ToString() As String" & VB.vbNewLine & _
       "                Return MyBase.ToString" & VB.vbNewLine & _
       "            End Function" & VB.vbNewLine & _
       "        End Class" & VB.vbNewLine & _
       ""
        ProjectCode.Append(code)

        ProjectCode.AppendLine("        Private Shared m_MyFormsObjectProvider As ThreadSafeObjectProvider(Of MyForms) = New ThreadSafeObjectProvider(Of MyForms)")
        ProjectCode.AppendLine("        <Global.System.ComponentModel.Design.HelpKeyword(""My.Forms"")> _")
        ProjectCode.AppendLine("        Friend Shared ReadOnly Property Forms As MyForms")
        ProjectCode.AppendLine("            <Global.System.Diagnostics.DebuggerHidden()> _")
        ProjectCode.AppendLine("            Get")
        ProjectCode.AppendLine("                Return m_MyFormsObjectProvider.GetInstance")
        ProjectCode.AppendLine("            End Get")
        ProjectCode.AppendLine("        End Property")

        Return result
    End Function

    Function GenerateMyWebServices() As Boolean
        Dim result As Boolean = True
        Dim _MyWebServicesDefine As Define
        Dim _MyWebServices As Boolean

        _MyWebServicesDefine = Compiler.CommandLine.Define("_MYWEBSERVICES")
        If _MyWebServicesDefine Is Nothing Then
            Select Case m_MyType
                Case MyTypes.Console, MyTypes.WindowsFormsWithCustomSubMain, MyTypes.Windows, MyTypes.WindowsForms, MyTypes.WebControl
                    _MyWebServices = True
                Case Else
                    _MyWebServices = False
            End Select
        Else
            _MyWebServices = CBool(_MyWebServicesDefine.Value)
        End If

        If Not _MyWebServices Then Return True

        Dim code As String = VB.vbNewLine & _
       "        <Global.System.ComponentModel.EditorBrowsable(Global.System.ComponentModel.EditorBrowsableState.Never)> _" & VB.vbNewLine & _
       "        <Global.Microsoft.VisualBasic.MyGroupCollection(""System.Web.Services.Protocols.SoapHttpClientProtocol"", ""Create__Instance__"", ""Dispose__Instance__"", """")> _" & VB.vbNewLine & _
       "        Friend NotInheritable Class MyWebServices" & VB.vbNewLine & _
       "            <Global.System.ComponentModel.EditorBrowsable(Global.System.ComponentModel.EditorBrowsableState.Never)> _" & VB.vbNewLine & _
       "            <Global.System.Diagnostics.DebuggerHidden> _" & VB.vbNewLine & _
       "            Public Sub New()" & VB.vbNewLine & _
       "            End Sub" & VB.vbNewLine & _
       "            " & VB.vbNewLine & _
       "            <Global.System.Diagnostics.DebuggerHidden> _" & VB.vbNewLine & _
       "            Private Shared Function Create__Instance__(Of T As New)(ByVal instance As T) As T" & VB.vbNewLine & _
       "                If (instance Is Nothing) Then" & VB.vbNewLine & _
       "                    Return Global.System.Activator.CreateInstance(Of T)" & VB.vbNewLine & _
       "                End If" & VB.vbNewLine & _
       "                Return instance" & VB.vbNewLine & _
       "            End Function" & VB.vbNewLine & _
       "            " & VB.vbNewLine & _
       "            <Global.System.Diagnostics.DebuggerHidden> _" & VB.vbNewLine & _
       "            Private Sub Dispose__Instance__(Of T)(ByRef instance As T)" & VB.vbNewLine & _
       "                instance = CType(Nothing, T)" & VB.vbNewLine & _
       "            End Sub" & VB.vbNewLine & _
       "            " & VB.vbNewLine & _
       "            <Global.System.Diagnostics.DebuggerHidden> _" & VB.vbNewLine & _
       "            <Global.System.ComponentModel.EditorBrowsable(Global.System.ComponentModel.EditorBrowsableState.Never)> _" & VB.vbNewLine & _
       "            Public Overrides Function Equals(ByVal o As Object) As Boolean" & VB.vbNewLine & _
       "                Return MyBase.Equals(Global.System.Runtime.CompilerServices.RuntimeHelpers.GetObjectValue(o))" & VB.vbNewLine & _
       "            End Function" & VB.vbNewLine & _
       "            " & VB.vbNewLine & _
       "            <Global.System.Diagnostics.DebuggerHidden> _" & VB.vbNewLine & _
       "            <Global.System.ComponentModel.EditorBrowsable(Global.System.ComponentModel.EditorBrowsableState.Never)> _" & VB.vbNewLine & _
       "            Public Overrides Function GetHashCode() As Integer" & VB.vbNewLine & _
       "             Return MyBase.GetHashCode" & VB.vbNewLine & _
       "            End Function" & VB.vbNewLine & _
       "            " & VB.vbNewLine & _
       "            <Global.System.Diagnostics.DebuggerHidden> _" & VB.vbNewLine & _
       "            <Global.System.ComponentModel.EditorBrowsable(Global.System.ComponentModel.EditorBrowsableState.Never)> _" & VB.vbNewLine & _
       "            Friend Overloads Function [GetType]() As Type" & VB.vbNewLine & _
       "                Return GetType(MyWebServices)" & VB.vbNewLine & _
       "            End Function" & VB.vbNewLine & _
       "            " & VB.vbNewLine & _
       "            <Global.System.Diagnostics.DebuggerHidden> _" & VB.vbNewLine & _
       "            <Global.System.ComponentModel.EditorBrowsable(Global.System.ComponentModel.EditorBrowsableState.Never)> _" & VB.vbNewLine & _
       "            Public Overrides Function ToString() As String" & VB.vbNewLine & _
       "                Return MyBase.ToString" & VB.vbNewLine & _
       "            End Function" & VB.vbNewLine & _
       "        End Class" & VB.vbNewLine & _
       ""
        ProjectCode.Append(code)

        ProjectCode.AppendLine("        Private Shared ReadOnly m_MyWebServicesObjectProvider As ThreadSafeObjectProvider(Of MyWebServices) = New ThreadSafeObjectProvider(Of MyWebServices)")
        ProjectCode.AppendLine("        <Global.System.ComponentModel.Design.HelpKeyword(""My.WebServices"")> _")
        ProjectCode.AppendLine("        Friend Shared ReadOnly Property WebServices As MyWebServices")
        ProjectCode.AppendLine("            <Global.System.Diagnostics.DebuggerHidden()> _")
        ProjectCode.AppendLine("            Get")
        ProjectCode.AppendLine("                Return m_MyWebServicesObjectProvider.GetInstance")
        ProjectCode.AppendLine("            End Get")
        ProjectCode.AppendLine("        End Property")

        Return result
    End Function

    Sub GenerateGroupCollectionClass(ByVal TypeName As String, ByVal TypeToCollect As String)
        Dim code As String = VB.vbNewLine & _
        "        <Global.System.ComponentModel.EditorBrowsable(Global.System.ComponentModel.EditorBrowsableState.Never)> _" & VB.vbNewLine & _
        "        <Global.Microsoft.VisualBasic.MyGroupCollection(""{1}"", ""Create__Instance__"", ""Dispose__Instance__"", """")> _" & VB.vbNewLine & _
        "        Friend NotInheritable Class {0}" & VB.vbNewLine & _
        "            <Global.System.ComponentModel.EditorBrowsable(Global.System.ComponentModel.EditorBrowsableState.Never)> _" & VB.vbNewLine & _
        "            <Global.System.Diagnostics.DebuggerHidden> _" & VB.vbNewLine & _
        "            Public Sub New()" & VB.vbNewLine & _
        "            End Sub" & VB.vbNewLine & _
        "            " & VB.vbNewLine & _
        "            <Global.System.Diagnostics.DebuggerHidden> _" & VB.vbNewLine & _
        "            Private Shared Function Create__Instance__(Of T As New)(ByVal instance As T) As T" & VB.vbNewLine & _
        "                If (instance Is Nothing) Then" & VB.vbNewLine & _
        "                    Return Global.System.Activator.CreateInstance(Of T)" & VB.vbNewLine & _
        "                End If" & VB.vbNewLine & _
        "                Return instance" & VB.vbNewLine & _
        "            End Function" & VB.vbNewLine & _
        "            " & VB.vbNewLine & _
        "            <Global.System.Diagnostics.DebuggerHidden> _" & VB.vbNewLine & _
        "            Private Sub Dispose__Instance__(Of T)(ByRef instance As T)" & VB.vbNewLine & _
        "                instance = CType(Nothing, T)" & VB.vbNewLine & _
        "            End Sub" & VB.vbNewLine & _
        "            " & VB.vbNewLine & _
        "            <Global.System.Diagnostics.DebuggerHidden> _" & VB.vbNewLine & _
        "            <Global.System.ComponentModel.EditorBrowsable(Global.System.ComponentModel.EditorBrowsableState.Never)> _" & VB.vbNewLine & _
        "            Public Overrides Function Equals(ByVal o As Object) As Boolean" & VB.vbNewLine & _
        "                Return MyBase.Equals(Global.System.Runtime.CompilerServices.RuntimeHelpers.GetObjectValue(o))" & VB.vbNewLine & _
        "            End Function" & VB.vbNewLine & _
        "            " & VB.vbNewLine & _
        "            <Global.System.Diagnostics.DebuggerHidden> _" & VB.vbNewLine & _
        "            <Global.System.ComponentModel.EditorBrowsable(Global.System.ComponentModel.EditorBrowsableState.Never)> _" & VB.vbNewLine & _
        "            Public Overrides Function GetHashCode() As Integer" & VB.vbNewLine & _
        "             Return MyBase.GetHashCode" & VB.vbNewLine & _
        "            End Function" & VB.vbNewLine & _
        "            " & VB.vbNewLine & _
        "            <Global.System.Diagnostics.DebuggerHidden> _" & VB.vbNewLine & _
        "            <Global.System.ComponentModel.EditorBrowsable(Global.System.ComponentModel.EditorBrowsableState.Never)> _" & VB.vbNewLine & _
        "            Friend Overloads Function [GetType]() As Type" & VB.vbNewLine & _
        "                Return GetType(MyWebServices)" & VB.vbNewLine & _
        "            End Function" & VB.vbNewLine & _
        "            " & VB.vbNewLine & _
        "            <Global.System.Diagnostics.DebuggerHidden> _" & VB.vbNewLine & _
        "            <Global.System.ComponentModel.EditorBrowsable(Global.System.ComponentModel.EditorBrowsableState.Never)> _" & VB.vbNewLine & _
        "            Public Overrides Function ToString() As String" & VB.vbNewLine & _
        "                Return MyBase.ToString" & VB.vbNewLine & _
        "            End Function" & VB.vbNewLine & _
        "        End Class" & VB.vbNewLine & _
        ""
        ProjectCode.Append(String.Format(code, TypeName, TypeToCollect))
    End Sub

    Function GenerateMyLog() As Boolean
        Dim result As Boolean = True

        If m_MyType <> MyTypes.Web Then Return True

        Dim code As String
        code = "" & _
        "        Private Shared ReadOnly m_LogObjectProvider As ThreadSafeObjectProvider(Of Global.Microsoft.VisualBasic.Logging.AspLog) = New ThreadSafeObjectProvider(Of Global.Microsoft.VisualBasic.Logging.AspLog)" & VB.vbNewLine & _
        "        <Global.System.ComponentModel.Design.HelpKeyword(""My.Application.Log"")> _" & VB.vbNewLine & _
        "        Friend Shared ReadOnly Property Log As Global.Microsoft.VisualBasic.Logging.AspLog" & VB.vbNewLine & _
        "            Get" & VB.vbNewLine & _
        "                Return MyProject.m_LogObjectProvider.GetInstance" & VB.vbNewLine & _
        "            End Get" & VB.vbNewLine & _
        "        End Property" & VB.vbNewLine

        ProjectCode.AppendLine(code)

        'ProjectCodeCctor.AppendLine("            m_LogObjectProvider = New ThreadSafeObjectProvider(Of Global.Microsoft.VisualBasic.Logging.AspLog)")

        Return result
    End Function

    Function GenerateMyComputer() As Boolean
        Dim result As Boolean = True
        Dim _MyComputerDefine As Define
        Dim _MyComputer As String

        _MyComputerDefine = Compiler.CommandLine.Define("_MYCOMPUTERTYPE")
        If _MyComputerDefine Is Nothing Then
            Select Case m_MyType
                Case MyTypes.Console, MyTypes.WindowsFormsWithCustomSubMain, MyTypes.Windows, MyTypes.WindowsForms
                    _MyComputer = "Windows"
                Case MyTypes.Web, MyTypes.WebControl
                    _MyComputer = "Web"
                Case Else
                    _MyComputer = String.Empty
            End Select
        Else
            _MyComputer = _MyComputerDefine.Value
        End If

        Dim baseClass As String
        Select Case _MyComputer
            Case "Web"
                baseClass = "Global.Microsoft.VisualBasic.Devices.ServerComputer"
            Case "Windows"
                baseClass = "Global.Microsoft.VisualBasic.Devices.Computer"
            Case Else
                Return True
        End Select

        Code.AppendLine("    <Global.System.ComponentModel.EditorBrowsable(Global.System.ComponentModel.EditorBrowsableState.Never)> _")
        Code.AppendLine("    <Global.System.CodeDom.Compiler.GeneratedCode(""MyTemplate"", ""11.0.0.0"")> _")
        Code.AppendLine("    Friend Class MyComputer")
        Code.Append("        Inherits ") : Code.AppendLine(baseClass)
        Code.AppendLine("        <Global.System.Diagnostics.DebuggerHidden()> _")
        Code.AppendLine("        <Global.System.ComponentModel.EditorBrowsable(Global.System.ComponentModel.EditorBrowsableState.Never)> _")
        Code.AppendLine("        Public Sub New()")
        Code.AppendLine("        End Sub")
        Code.AppendLine("    End Class")

        ProjectCode.AppendLine("        Private Shared ReadOnly m_ComputerObjectProvider As ThreadSafeObjectProvider(Of $GLOBALMY$.MyComputer) = New ThreadSafeObjectProvider(Of $GLOBALMY$.MyComputer)")
        ProjectCode.AppendLine("        <Global.System.ComponentModel.Design.HelpKeyword(""My.Computer"")> _")
        ProjectCode.AppendLine("        Friend Shared ReadOnly Property Computer As $GLOBALMY$.MyComputer")
        ProjectCode.AppendLine("            <Global.System.Diagnostics.DebuggerHidden()> _")
        ProjectCode.AppendLine("            Get")
        ProjectCode.AppendLine("                Return m_ComputerObjectProvider.GetInstance")
        ProjectCode.AppendLine("            End Get")
        ProjectCode.AppendLine("        End Property")

        Return result
    End Function

    Function GenerateMyRequest() As Boolean
        Dim result As Boolean = True

        If m_MyType <> MyTypes.Web Then Return True

        Dim code As String
        code = "" & _
        "        <Global.System.ComponentModel.Design.HelpKeyword(""My.Request"")> _" & VB.vbNewLine & _
        "        Friend Shared ReadOnly Property Request As Global.System.Web.HttpRequest" & VB.vbNewLine & _
        "            Get" & VB.vbNewLine & _
        "                Dim current As Global.System.Web.HttpContext" & VB.vbNewLine & _
        "                current = Global.System.Web.HttpContext.Current" & VB.vbNewLine & _
        "                If current IsNot Nothing Then" & VB.vbNewLine & _
        "                    Return current.Request" & VB.vbNewLine & _
        "                Else" & VB.vbNewLine & _
        "                    Return Nothing" & VB.vbNewLine & _
        "                End If" & VB.vbNewLine & _
        "            End Get" & VB.vbNewLine & _
        "        End Property" & VB.vbNewLine

        ProjectCode.AppendLine(code)

        Return result
    End Function

    Function GenerateMyResources() As Boolean
        Dim result As Boolean = True

        Return result
    End Function

    Function GenerateMyResponse() As Boolean
        Dim result As Boolean = True

        If m_MyType <> MyTypes.Web Then Return True

        Dim code As String
        code = "" & _
        "        <Global.System.ComponentModel.Design.HelpKeyword(""My.Response"")> _" & VB.vbNewLine & _
        "        Friend Shared ReadOnly Property Response As Global.System.Web.HttpResponse" & VB.vbNewLine & _
        "            Get" & VB.vbNewLine & _
        "                Dim current As Global.System.Web.HttpContext" & VB.vbNewLine & _
        "                current = Global.System.Web.HttpContext.Current" & VB.vbNewLine & _
        "                If current IsNot Nothing Then" & VB.vbNewLine & _
        "                    Return current.Response    " & VB.vbNewLine & _
        "                Else" & VB.vbNewLine & _
        "                    Return Nothing" & VB.vbNewLine & _
        "                End If" & VB.vbNewLine & _
        "            End Get" & VB.vbNewLine & _
        "        End Property" & VB.vbNewLine

        ProjectCode.AppendLine(code)

        Return result
    End Function

    Function GenerateMySettings() As Boolean
        Dim result As Boolean = True

        Return result
    End Function


    Enum MyTypes
        Console
        Custom
        Empty
        Web
        WebControl
        Windows
        WindowsForms
        WindowsFormsWithCustomSubMain
    End Enum
End Class
