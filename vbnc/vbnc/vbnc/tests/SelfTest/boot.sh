cp ../../bin/*.dll* .
cp ../../bin/vbnc.exe vbnc-boot.exe
mono --debug vbnc-boot.exe @SelfCompileLinux.response
