
thisdir = vbruntime/Test
SUBDIRS =
include ../../build/rules.make

DISTFILES = \
	2005VB_test_CS.csproj		\
	2005VB_test_CS.dll.rsp		\
	2005VB_test_CS.dll.sources	\
	2005VB_test_CS.dll.sources.win	\
	2005VB_test_VB.dll.rsp		\
	2005VB_test_VB.dll.sources	\
	2005VB_test_VB.dll.sources.win	\
	2005VB_test_VB.vbproj		\
	ChangeLog			\
	Make.cmd

DISTFILES += $(wildcard *.vb) $(wildcard *.cs) $(wildcard */*.vb) $(wildcard */*.cs)
DISTFILES += $(wildcard bin/*)

MONO_VB_PATH="../../class/lib/bootstrap$(PLATFORM_PATH_SEPARATOR)$$MONO_PATH"

ifeq ("$(FXVERSION)", "")
NUNIT_VERSION=2
else
NUNIT_VERSION=$(FXVERSION)
endif

ifeq ("$(NUNIT_VERSION)", "1")
CSCOMPILER=mcs
CSDEFINES=-define:NET_1_0,NET_1_1
else
CSCOMPILER=gmcs
CSDEFINES=-define:NET_1_0,NET_1_1,NET_2_0
endif

init:
	cp bin/nunit-console$(NUNIT_VERSION).exe.config bin/nunit-console.exe.config

2005VB_test_VB.dll.sources: 2005VB_test_VB.vbproj 
	MONO_PATH=$(MONO_VB_PATH) mono ../../tools/extract-source/extract-source.exe -s:2005VB_test_VB.vbproj -d:2005VB_test_VB.dll.sources -m:l
	MONO_PATH=$(MONO_VB_PATH) mono ../../tools/extract-source/extract-source.exe -s:2005VB_test_VB.vbproj -d:2005VB_test_VB.dll.sources.win -m:w
	
2005VB_test_CS.dll.sources: 2005VB_test_CS.csproj
	MONO_PATH=$(MONO_VB_PATH) mono ../../tools/extract-source/extract-source.exe -s:2005VB_test_CS.csproj -d:2005VB_test_CS.dll.sources -m:l
	MONO_PATH=$(MONO_VB_PATH) mono ../../tools/extract-source/extract-source.exe -s:2005VB_test_CS.csproj -d:2005VB_test_CS.dll.sources.win -m:w

test-first:
	cd ../Microsoft.VisualBasic && make && cp ../../class/lib/vbnc/Microsoft.VisualBasic.dll ../Test/bin

test-cs: test-first 2005VB_test_CS.dll.sources
	$(CSCOMPILER) "-out:bin/2005VB_test_CS.dll" @2005VB_test_CS.dll.rsp @2005VB_test_CS.dll.sources -lib:bin $(CSDEFINES)
	
test-vb: test-first 2005VB_test_VB.dll.sources 
	$(VBNC) -out:bin/2005VB_test_VB.dll @2005VB_test_VB.dll.rsp @2005VB_test_VB.dll.sources /libpath:bin

test-local: init run-test-cs run-test-vb

clean-local:
	-rm -f bin/2005VB_test_VB.*
	-rm -f bin/2005VB_test_CS.*
	-rm -fR bin/data
	
run-test-cs: test-cs
	cd bin && mono --debug nunit-console.exe 2005VB_test_CS.dll /labels

run-test-vb: test-vb
	cd bin && mono --debug nunit-console.exe 2005VB_test_VB.dll /labels

run-test-local: run-test-cs run-test-vb
	
run-test-ondotnet-local all-local install-local uninstall-local:
	@:

dist-local: dist-default
