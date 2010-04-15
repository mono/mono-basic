cp ../../bin/Mono.Cecil.VB.dll .
mono --optimize=-deadce --debug vbnc.exe @SelfCompileLinux.response $@
