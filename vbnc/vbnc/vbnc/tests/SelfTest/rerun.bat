copy vbnc.exe vbnc-windows.exe
copy ..\..\bin\Mono.Cecil.VB.dll .
vbnc-windows.exe @SelfCompileWindows.response
del vbnc-windows.exe