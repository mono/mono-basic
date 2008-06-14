thisdir = vbnc/vbnc/tests
SUBDIRS =
include ../../../build/rules.make

DISTFILES = Readme.txt KnownFailures.txt Resources/Resources3.binary
DISTFILES += $(wildcard */*.vb) $(wildcard */*.rsp) $(wildcard */*.response) $(wildcard */*.resources) $(wildcard */*.resx)  \
             $(wildcard */*/*.vb) $(wildcard */*/*.rsp) $(wildcard */*/*.response) $(wildcard */*/*.resources) $(wildcard */*/*.resx)  \
             $(wildcard */*/*/*.vb) $(wildcard */*/*/*.rsp) $(wildcard */*/*/*.response) $(wildcard */*/*/*.resources) $(wildcard */*/*/*.resx)  \

ifndef TEST_BASE_DIR
TEST_BASE_DIR=./
endif
ifndef TEST_RECURSE
TEST_RECURSE=on
endif
ifndef TEST_COMPILER
TEST_COMPILER=../../../class/lib/vbnc/vbnc.exe
endif

ifeq ($(PLATFORM), win32)
# hack for cygwin, MONO_PATH seems to work only with full paths (Starting with C:/...).
# so we copy the only dependency to the executable location.
# this path is relative to ../../rt-console/
DEPENDENCIES=cp ../../class/lib/vbnc/Microsoft.VisualBasic.* .
else
DEPENDENCIES=:
endif

make-dependencies:
	-cd ../ && $(MAKE)
	-cd ../../../vbruntime/Microsoft.VisualBasic/ && $(MAKE)
	-cd ../../rt-console && $(MAKE) && $(DEPENDENCIES)
	-cd ../../rt-execute && $(MAKE) 
	

test-local: make-dependencies
	-MONO_PATH="$(PWD)/../../../class/lib/vbnc$(PLATFORM_PATH_SEPARATOR)$$MONO_PATH" mono --debug ../../rt-console/rt-console.exe -f=on -c=$(TEST_COMPILER) -b=$(TEST_BASE_DIR) -r=$(TEST_RECURSE) -verbosity:failed $(TEST_FLAGS)

show-failed: make-dependencies
	-MONO_PATH="$(PWD)/../../../class/lib/vbnc$(PLATFORM_PATH_SEPARATOR)$$MONO_PATH" mono --debug ../../rt-console/rt-console.exe -f=on -c=$(TEST_COMPILER) -b=$(TEST_BASE_DIR) -r=$(TEST_RECURSE) -printstatus=on -printstatusskip=Success,NotRun,Skipped $(TEST_FLAGS)

clean-local run-test-local run-test-ondotnet-local all-local install-local uninstall-local:
	@:

dist-local: dist-default
