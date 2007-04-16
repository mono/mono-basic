thisdir = vbruntime/Test
SUBDIRS =
include ../../build/rules.make

2005VB_test_VB.dll.sources: 2005VB_test_VB.vbproj 
	MONO_PATH="../../class/lib/bootstrap$$PLATFORM_PATH_SEPARATOR$$MONO_PATH" mono ../../tools/extract-source/extract-source.exe -s:2005VB_test_VB.vbproj -d:2005VB_test_VB.dll.sources -m:l
	
2005VB_test_CS.dll.sources: 2005VB_test_CS.csproj
	MONO_PATH="../../class/lib/bootstrap$$PLATFORM_PATH_SEPARATOR$$MONO_PATH" mono ../../tools/extract-source/extract-source.exe -s:2005VB_test_CS.csproj -d:2005VB_test_CS.dll.sources -m:l

test-local: 2005VB_test_CS.dll.sources  2005VB_test_VB.dll.sources 
	cp ../../class/lib/vbnc/Microsoft.VisualBasic.dll .
	gmcs -out:bin\2005VB_test_CS.dll @2005VB_test_CS.dll.rsp @2005VB_test_CS.dll.sources -lib:bin -define:NET_1_0,NET_1_1,NET_2_0
	$(VBNC) -out:bin\2005VB_test_VB.dll @2005VB_test_VB.dll.rsp @2005VB_test_VB.dll.sources /libpath:bin
	MONO_PATH="bin$$PLATFORM_PATH_SEPARATOR$$MONO_PATH" mono --debug bin/nunit-console.exe 2005_test_CS.dll
	MONO_PATH="bin$$PLATFORM_PATH_SEPARATOR$$MONO_PATH" mono --debug bin/nunit-console.exe 2005_test_VB.dll


clean-local run-test-local run-test-ondotnet-local all-local install-local uninstall-local:
	@:

dist-local: dist-default
