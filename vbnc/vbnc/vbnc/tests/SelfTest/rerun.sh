cp ../../bin/Mono.Cecil.VB.dll .
#valgrind --smc-check=all --log-file=valgrind.log mono --debug vbnc.exe @SelfCompileLinux.response $@
mono --debug vbnc.exe @SelfCompileLinux.response $@
