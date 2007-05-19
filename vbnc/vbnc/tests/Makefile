thisdir = vbnc/vbnc/tests
SUBDIRS =
include ../../../build/rules.make

DISTFILES = Readme.txt
DISTFILES += $(wildcard */*.vb) $(wildcard */*.rsp) $(wildcard */*.response)  \
             $(wildcard */*/*.vb) $(wildcard */*/*.rsp) $(wildcard */*/*.response)  \
             $(wildcard */*/*/*.vb) $(wildcard */*/*/*.rsp) $(wildcard */*/*/*.response) 

ifndef TEST_BASE_DIR
TEST_BASE_DIR=./
endif
ifndef TEST_RECURSE
TEST_RECURSE=on
endif
ifndef TEST_COMPILER
TEST_COMPILER=../../../class/lib/vbnc/vbnc.exe
endif

test-local:
	-cd ../ && $(MAKE)
	-cd ../../../vbruntime/Microsoft.VisualBasic/ && $(MAKE)
	-cd ../../rt-console && $(MAKE) && cd ../../$(thisdir)
	-cd ../../rt-execute && $(MAKE) && cd ../../$(thisdir)
	-MONO_PATH="../../../class/lib/vbnc$(PLATFORM_PATH_SEPARATOR)$$MONO_PATH" mono --debug ../../rt-console/rt-console.exe -f=on -c=$(TEST_COMPILER) -b=$(TEST_BASE_DIR) -r=$(TEST_RECURSE)


clean-local run-test-local run-test-ondotnet-local all-local install-local uninstall-local:
	@:

dist-local: dist-default
