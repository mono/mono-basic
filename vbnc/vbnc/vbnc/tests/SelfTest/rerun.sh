cp ../../bin/Mono.Cecil.VB.dll .
#valgrind --smc-check=all --log-file=valgrind.log mono --debug vbnc.exe @SelfCompileLinux.response $@
cp vbnc.exe vbnc-linux.exe
mono --debug vbnc-linux.exe @SelfCompileLinux.response $@
