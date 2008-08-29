cp ../../bin/Mono.Cecil.VB.dll .
MONO_COUNT=0 mono --debug vbnc.exe @SelfCompileLinux.response $@
