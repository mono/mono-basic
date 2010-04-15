cp ../../bin/Mono.Cecil.VB.dll .
MONO_COUNT=0 mono --profile vbnc.exe @SelfCompileLinux.response -out:vbnc.mono.profile.exe


