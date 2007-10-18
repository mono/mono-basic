VBNC=mono --debug vbnc.exe
VBNC_BOOTSTRAP=mono --debug bootstrap-vbnc.exe

default:
	$(VBNC) @../tests/SelfTest/SelfCompileLinux.response

runtime:
	$(VBNC) @../tests/VBRunTime/Microsoft.VisualBasic.linux.response

bootstrap:
	$(VBNC_BOOTSTRAP) @../tests/SelfTest/SelfCompileLinux.response

getl:
	scp quack.boston.ximian.com:/cygdrive/c/mono-basic/vbruntime/bin/*.dll
	gacutil -i Microsoft.VisualBasic.dll

get getc:
	scp quack.boston.ximian.com:/cygdrive/c/miguel/vbnc/bin/vbnc.exe .
