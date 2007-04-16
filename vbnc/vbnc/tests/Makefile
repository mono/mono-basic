thisdir = vbnc/vbnc/tests
SUBDIRS =
include ../../../build/rules.make

DISTFILES = Readme.txt
DISTFILES += $(wildcard */*.vb) $(wildcard */*.rsp) $(wildcard */*.response)  \
             $(wildcard */*/*.vb) $(wildcard */*/*.rsp) $(wildcard */*/*.response)  \
             $(wildcard */*/*/*.vb) $(wildcard */*/*/*.rsp) $(wildcard */*/*/*.response) 

test-local:
	cd ../../rt-console && $(MAKE) && cd ../../$(thisdir)
	MONO_PATH="../../../class/lib/vbnc$(PLATFORM_PATH_SEPARATOR)$$MONO_PATH" mono --debug ../../rt-console/rt-console.exe -f=on -c=../../../class/lib/vbnc/vbnc.exe -b=./ -r=on


clean-local run-test-local run-test-ondotnet-local all-local install-local uninstall-local:
	@:

dist-local: dist-default
