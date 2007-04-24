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


MONO_VB_PATH="../../class/lib/bootstrap$(PLATFORM_PATH_SEPARATOR)$MONO_PATH"

2005VB_test_VB.dll.sources: 2005VB_test_VB.vbproj 
	MONO_PATH=$(MONO_VB_PATH) mono ../../tools/extract-source/extract-source.exe -s:2005VB_test_VB.vbproj -d:2005VB_test_VB.dll.sources -m:l
	MONO_PATH=$(MONO_VB_PATH) mono ../../tools/extract-source/extract-source.exe -s:2005VB_test_VB.vbproj -d:2005VB_test_VB.dll.sources.win -m:w
	
2005VB_test_CS.dll.sources: 2005VB_test_CS.csproj
	MONO_PATH=$(MONO_VB_PATH) mono ../../tools/extract-source/extract-source.exe -s:2005VB_test_CS.csproj -d:2005VB_test_CS.dll.sources -m:l
	MONO_PATH=$(MONO_VB_PATH) mono ../../tools/extract-source/extract-source.exe -s:2005VB_test_CS.csproj -d:2005VB_test_CS.dll.sources.win -m:w

test-first:
	cd ../Microsoft.VisualBasic && make && cp ../../class/lib/vbnc/Microsoft.VisualBasic.dll ../Test/bin

test-cs: test-first 2005VB_test_CS.dll.sources
	gmcs "-out:bin/2005VB_test_CS.dll" @2005VB_test_CS.dll.rsp @2005VB_test_CS.dll.sources -lib:bin -define:NET_1_0,NET_1_1,NET_2_0
	
test-vb: test-first 2005VB_test_VB.dll.sources 
	$(VBNC) -out:bin/2005VB_test_VB.dll @2005VB_test_VB.dll.rsp @2005VB_test_VB.dll.sources /libpath:bin

test-local: test-cs test-vb

clean-local:
	-rm -f bin/2005VB_test_VB.*
	-rm -f bin/2005VB_test_CS.*
	-rm -fR bin/data
	
run-test-cs: test-cs
	cd bin && mono --debug nunit-console.exe 2005VB_test_CS.dll

run-test-vb: test-vb
	cd bin && mono --debug nunit-console.exe 2005VB_test_VB.dll

run-test-local: run-test-cs run-test-vb
	
run-test-ondotnet-local all-local install-local uninstall-local:
	@:

dist-local: dist-default
