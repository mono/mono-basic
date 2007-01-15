'
' AssemblyInfo.vb
'
' Authors:
'   Miguel de Icaza (miguel@novell.com)
'   Mizrahi Rafael (rafim@mainsoft.com)

'
' Copyright (C) 2002-2006 Mainsoft Corporation.
' Copyright (C) 2006 Novell (http://www.novell.com)
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
Imports System.Reflection
Imports System.Runtime.InteropServices

' General Information about an assembly is controlled through the following 
' set of attributes. Change these attribute values to modify the information
' associated with an assembly.

' Review the values of the assembly attributes

<Assembly: AssemblyTitle("vbruntime")> 
<Assembly: AssemblyDescription("")> 
<Assembly: AssemblyCompany("Ximian")> 
<Assembly: AssemblyProduct("vbruntime")> 
<Assembly: AssemblyCopyright("Copyright © Ximian 2006")> 
<Assembly: AssemblyTrademark("")> 

<Assembly: ComVisible(False)>

#If NET_2_0 Then
'The following GUID is for the ID of the typelib if this project is exposed to COM
<Assembly: Guid("aa353322-85a4-4601-a6b7-e3b724e9350c")> 
#Else
<Assembly: Guid("d5e192e2-301b-49b5-9b05-da883f937c84")> 
<Assembly: CLSCompliant(True)> 
#End If

' Version information for an assembly consists of the following four values:
'
'      Major Version
'      Minor Version 
'      Build Number
'      Revision
'
' You can specify all the values or you can default the Build and Revision Numbers 
' by using the '*' as shown below:
' <Assembly: AssemblyVersion("1.0.*")> 

#If NET_2_0 Then
<Assembly: AssemblyVersion("8.0.0.1")>
#Else
<Assembly: AssemblyVersion("7.0.5000.0")> 
#End If

<Assembly: AssemblyFileVersion("1.0.0.0")> 
<Assembly: AssemblyDelaySign(True)> 
#If TARGET_JVM = False And INDEVENV = False Then
'<Assembly: AssemblyKeyFile("msfinal.pub")> 
#End If

