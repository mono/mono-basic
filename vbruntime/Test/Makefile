
thisdir = vbruntime/Test
SUBDIRS =
include ../../build/rules.make

DISTFILES = \
	2010VB_test_CS.csproj		\
	2010VB_test_CS.dll.rsp		\
	2010VB_test_CS.dll.sources	\
	2010VB_test_CS.dll.sources.win	\
	2010VB_test_VB.dll.rsp		\
	2010VB_test_VB.dll.sources	\
	2010VB_test_VB.dll.sources.win	\
	2010VB_test_VB.vbproj		\
	ChangeLog

DISTFILES += $(wildcard *.vb) $(wildcard *.cs) $(wildcard */*.vb) $(wildcard */*.cs)
DISTFILES += $(wildcard bin/*config)

MONO_VB_PATH="../../class/lib/bootstrap$(PLATFORM_PATH_SEPARATOR)$$MONO_PATH"
CSCOMPILER=mcs

ifeq ("$(FXVERSION)", "")
NUNIT_VERSION=2
else
NUNIT_VERSION=$(FXVERSION)
endif

ifeq ("$(NUNIT_VERSION)", "1")
CSDEFINES=-define:NET_1_0,NET_1_1
else
CSDEFINES=-define:NET_1_0,NET_1_1,NET_2_0
endif

ifeq ($(PLATFORM), win32)
WINTEST=-cmd /C Make.cmd 1_1; cmd /C Make.cmd 2_0
else
WINTEST=-echo Linux environment.
endif

bin/nunit-console.exe:
	wget --no-check-certificate -O bin/nunit-console.exe "http://github.com/mono/mono-basic/raw/70f26b5b6f3c16999cb3e0b5864df88752fe683a/vbruntime/Test/bin/nunit-console.exe"
bin/nunit.framework.dll:
	wget --no-check-certificate -O bin/nunit.framework.dll "http://github.com/mono/mono-basic/raw/70f26b5b6f3c16999cb3e0b5864df88752fe683a/vbruntime/Test/bin/nunit.framework.dll"
bin/nunit.core.dll:
	wget --no-check-certificate -O bin/nunit.core.dll "http://github.com/mono/mono-basic/raw/70f26b5b6f3c16999cb3e0b5864df88752fe683a/vbruntime/Test/bin/nunit.core.dll"
bin/nunit.util.dll:
	wget --no-check-certificate -O bin/nunit.util.dll "http://github.com/mono/mono-basic/raw/70f26b5b6f3c16999cb3e0b5864df88752fe683a/vbruntime/Test/bin//nunit.util.dll"

init: bin/nunit-console.exe bin/nunit.framework.dll bin/nunit.core.dll bin/nunit.util.dll
	cp bin/nunit-console$(NUNIT_VERSION).exe.config bin/nunit-console.exe.config
	$(WINTEST)

2010VB_test_VB.dll.sources: 2010VB_test_VB.vbproj
	MONO_PATH=$(MONO_VB_PATH) mono ../../tools/extract-source/extract-source.exe -s:2010VB_test_VB.vbproj -d:2010VB_test_VB.dll.sources -m:l
	MONO_PATH=$(MONO_VB_PATH) mono ../../tools/extract-source/extract-source.exe -s:2010VB_test_VB.vbproj -d:2010VB_test_VB.dll.sources.win -m:w
	
2010VB_test_CS.dll.sources: 2010VB_test_CS.csproj
	MONO_PATH=$(MONO_VB_PATH) mono ../../tools/extract-source/extract-source.exe -s:2010VB_test_CS.csproj -d:2010VB_test_CS.dll.sources -m:l
	MONO_PATH=$(MONO_VB_PATH) mono ../../tools/extract-source/extract-source.exe -s:2010VB_test_CS.csproj -d:2010VB_test_CS.dll.sources.win -m:w

test-first:
	cd ../Microsoft.VisualBasic && make && cp ../../class/lib/$(PROFILE)/Microsoft.VisualBasic.dll* ../Test/bin

test-cs: test-first 2010VB_test_CS.dll.sources
	$(CSCOMPILER) "-out:bin/2010VB_test_CS.dll" @2010VB_test_CS.dll.rsp @2010VB_test_CS.dll.sources -lib:bin $(CSDEFINES)
	
test-vb: test-first 2010VB_test_VB.dll.sources
	$(VBNC) -out:bin/2010VB_test_VB.dll @2010VB_test_VB.dll.rsp /d:NET_VER=2.0 @2010VB_test_VB.dll.sources /libpath:bin

test-local: init run-test-cs run-test-vb

clean-local:
	-rm -f bin/2010VB_test_VB.*
	-rm -f bin/2010VB_test_CS.*
	-rm -fR bin/data
	-rm -f bin/nunit-console.exe bin/nunit.framework.dll bin/nunit.core.dll bin/nunit.util.dll bin/Microsoft.VisualBasic.dll*
	-rm -fR bin/%temp%
	-rm -f bin/TestResult.xml
	
run-test-cs: test-cs
	-cd bin && mono --debug nunit-console.exe 2010VB_test_CS.dll /exclude:NotWorking /labels

run-test-vb: test-vb
	-cd bin && mono --debug nunit-console.exe 2010VB_test_VB.dll /exclude:NotWorking /labels

run-test-local: run-test-cs run-test-vb
	
run-test-ondotnet-local all-local install-local uninstall-local:
	@:

dist-local: dist-default
