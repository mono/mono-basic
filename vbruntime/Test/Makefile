
thisdir = vbruntime/Test
SUBDIRS =
include ../../build/rules.make

DISTFILES = \
	2008VB_test_CS.csproj		\
	2008VB_test_CS.dll.rsp		\
	2008VB_test_CS.dll.sources	\
	2008VB_test_CS.dll.sources.win	\
	2008VB_test_VB.dll.rsp		\
	2008VB_test_VB.dll.sources	\
	2008VB_test_VB.dll.sources.win	\
	2008VB_test_VB.vbproj		\
	ChangeLog

DISTFILES += $(wildcard *.vb) $(wildcard *.cs) $(wildcard */*.vb) $(wildcard */*.cs)
DISTFILES += $(wildcard bin/*config)

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

ifeq ($(PLATFORM), win32)
WINTEST=-cmd /C Make.cmd 1_1; cmd /C Make.cmd 2_0
else
WINTEST=-echo Linux environment.
endif

bin/nunit-console.exe:
	wget -O bin/nunit-console.exe http://anonsvn.mono-project.com/viewvc/trunk/mono-basic/vbruntime/Test/bin/nunit-console.exe?revision=76123
bin/nunit.framework.dll:
	wget -O bin/nunit.framework.dll http://anonsvn.mono-project.com/viewvc/trunk/mono-basic/vbruntime/Test/bin/nunit.framework.dll?revision=76123
bin/nunit.core.dll:
	wget -O bin/nunit.core.dll http://anonsvn.mono-project.com/viewvc/trunk/mono-basic/vbruntime/Test/bin/nunit.core.dll?revision=76123
bin/nunit.util.dll:
	wget -O bin/nunit.util.dll http://anonsvn.mono-project.com/viewvc/trunk/mono-basic/vbruntime/Test/bin/nunit.util.dll?revision=76123

init: bin/nunit-console.exe bin/nunit.framework.dll bin/nunit.core.dll bin/nunit.util.dll
	cp bin/nunit-console$(NUNIT_VERSION).exe.config bin/nunit-console.exe.config
	$(WINTEST)

2008VB_test_VB.dll.sources: 2008VB_test_VB.vbproj
	MONO_PATH=$(MONO_VB_PATH) mono ../../tools/extract-source/extract-source.exe -s:2008VB_test_VB.vbproj -d:2008VB_test_VB.dll.sources -m:l
	MONO_PATH=$(MONO_VB_PATH) mono ../../tools/extract-source/extract-source.exe -s:2008VB_test_VB.vbproj -d:2008VB_test_VB.dll.sources.win -m:w
	
2008VB_test_CS.dll.sources: 2008VB_test_CS.csproj
	MONO_PATH=$(MONO_VB_PATH) mono ../../tools/extract-source/extract-source.exe -s:2008VB_test_CS.csproj -d:2008VB_test_CS.dll.sources -m:l
	MONO_PATH=$(MONO_VB_PATH) mono ../../tools/extract-source/extract-source.exe -s:2008VB_test_CS.csproj -d:2008VB_test_CS.dll.sources.win -m:w

test-first:
	cd ../Microsoft.VisualBasic && make && cp ../../class/lib/vbnc/Microsoft.VisualBasic.dll* ../Test/bin

test-cs: test-first 2008VB_test_CS.dll.sources
	$(CSCOMPILER) "-out:bin/2008VB_test_CS.dll" @2008VB_test_CS.dll.rsp @2008VB_test_CS.dll.sources -lib:bin $(CSDEFINES)
	
test-vb: test-first 2008VB_test_VB.dll.sources
	$(VBNC) -out:bin/2008VB_test_VB.dll @2008VB_test_VB.dll.rsp /d:NET_VER=2.0 @2008VB_test_VB.dll.sources /libpath:bin

test-local: init run-test-cs run-test-vb

clean-local:
	-rm -f bin/2008VB_test_VB.*
	-rm -f bin/2008VB_test_CS.*
	-rm -fR bin/data
	
run-test-cs: test-cs
	-cd bin && mono --debug nunit-console.exe 2008VB_test_CS.dll /exclude:NotWorking /labels

run-test-vb: test-vb
	-cd bin && mono --debug nunit-console.exe 2008VB_test_VB.dll /exclude:NotWorking /labels

run-test-local: run-test-cs run-test-vb
	
run-test-ondotnet-local all-local install-local uninstall-local:
	@:

dist-local: dist-default
