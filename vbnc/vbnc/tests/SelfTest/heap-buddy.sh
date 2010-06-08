cp ../../bin/Mono.Cecil.VB.dll .
MONO_COUNT=0 mono --profile=heap-buddy:vbnc.profile.heap-buddy vbnc.exe @SelfCompileLinux.response
heap-buddy vbnc.profile.heap-buddy backtraces > vbnc.profile.heap-buddy.decoded
