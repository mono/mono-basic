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
Imports System.Runtime.CompilerServices
Imports System.Resources
Imports System.Security

' General Information about an assembly is controlled through the following 
' set of attributes. Change these attribute values to modify the information
' associated with an assembly.

' Review the values of the assembly attributes

<Assembly: AssemblyTitle("Microsoft.VisualBasic.dll")> 
<Assembly: AssemblyDescription("Microsoft.VisualBasic.dll")> 
<Assembly: AssemblyCompany("Ximian")> 
<Assembly: AssemblyProduct("vbruntime")> 
<Assembly: AssemblyCopyright("Copyright © Ximian 2006")> 

<Assembly: NeutralResourcesLanguage("en-US")> 
#If Not MOONLIGHT Then
<Assembly: AllowPartiallyTrustedCallers()> 
#End If


#If MOONLIGHT Then
<Assembly: AssemblyVersion("2.0.5.0")> 
<Assembly: ComVisible(True)> 
<Assembly: CLSCompliant(True)> 
<Assembly: Debuggable(DebuggableAttribute.DebuggingModes.Default Or DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)> 
<Assembly: CompilationRelaxations(CompilationRelaxations.NoStringInterning)> 
<Assembly: RuntimeCompatibility(WrapNonExceptionThrows:=True)> 
<Assembly: SatelliteContractVersion("2.1.0.0")> 
<Assembly: AssemblyInformationalVersion("8.0.50727.42")> 
<Assembly: AssemblyFileVersion("1.1.20806.0")> 
<Assembly: AssemblyDefaultAlias("Microsoft.VisualBasic.dll")> 
#Else
#If NET_VER >= 4.0 Then
<Assembly: AssemblyVersion("10.0.0.0")> 
<Assembly: ComVisible(True)> 
'<Assembly: Guid("aa353322-85a4-4601-a6b7-e3b724e9350c")> 
<Assembly: CLSCompliant(True)> 
<Assembly: Debuggable(DebuggableAttribute.DebuggingModes.Default Or DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)> 
<Assembly: CompilationRelaxations(CompilationRelaxations.NoStringInterning)> 
<Assembly: RuntimeCompatibility(WrapNonExceptionThrows:=True)> 
<Assembly: SatelliteContractVersion("10.0.0.0")> 
<Assembly: AssemblyInformationalVersion("10.0.30319.1")> 
<Assembly: AssemblyFileVersion("10.0.30319.1")> 
<Assembly: AssemblyDefaultAlias("Microsoft.VisualBasic.dll")> 
#Else 'If NET_VER >= 2.0 Then
<Assembly: AssemblyVersion("8.0.0.0")> 
<Assembly: ComVisible(True)> 
'<Assembly: Guid("aa353322-85a4-4601-a6b7-e3b724e9350c")> 
<Assembly: CLSCompliant(True)> 
<Assembly: Debuggable(DebuggableAttribute.DebuggingModes.Default Or DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)> 
<Assembly: CompilationRelaxations(CompilationRelaxations.NoStringInterning)> 
<Assembly: RuntimeCompatibility(WrapNonExceptionThrows:=True)> 
<Assembly: SatelliteContractVersion("8.0.0.0")> 
<Assembly: AssemblyInformationalVersion("8.0.50727.42")> 
<Assembly: AssemblyFileVersion("8.0.50727.42")> 
<Assembly: AssemblyDefaultAlias("Microsoft.VisualBasic.dll")> 
#End If
#End If


#If DONTSIGN = False Then
<Assembly: AssemblyDelaySign(True)> 
#If TARGET_JVM = False And INDEVENV = False Then
#If MOONLIGHT Then
<Assembly: AssemblyKeyFile("winfx3.pub")> 
#Else
<Assembly: AssemblyKeyFile("msfinal.pub")> 
#End If
#End If
#End If
