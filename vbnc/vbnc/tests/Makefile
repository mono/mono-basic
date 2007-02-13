thisdir = vbnc/vbnc/tests
SUBDIRS = 
include ../../../build/rules.make

test-local:
	MONO_PATH="../../class/lib/bootstrap:$MONO_PATH" mono --debug ../../rt-console/rt-console.exe -f=on -c=../bin/vbnc.exe -b=./ -r=on

clean-local run-test-local run-test-ondotnet-local all-local install-local uninstall-local:
        @:
