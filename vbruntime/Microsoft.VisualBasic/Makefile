thisdir = vbruntime/Microsoft.VisualBasic
include ../../build/rules.make

LIBRARY = Microsoft.VisualBasic.dll

rspfile = vbruntime.rsp

# If the directory contains the per profile rsp file, use it
PROFILE_rspfile = vbruntime-$(PROFILE).rsp
ifeq ($(wildcard $(PROFILE_rspfile)), $(PROFILE_rspfile))
rspfile = $(PROFILE_rspfile)
endif

LIBRARY_COMPILE = $(BOOT_COMPILE) @$(rspfile)

BUILT_FILES = strings2.resources

strings2.resources: strings.txt strings-only2.txt
	cat strings.txt > strings2.txt
	cat strings-only2.txt >> strings2.txt
	resgen2 strings2.txt strings2.resources

include ../../build/library.make

DISTFILES =	\
	AssemblyInfo.vb				\
	msfinal.pub				\
	winfx3.pub				\
	mono.snk				\
	strings.txt				\
	strings-only2.txt			\
	strings2.resources			\
	ChangeLog				\
	Makefile				\
	$(wildcard Microsoft.VisualBasic.dll*.sources)	\
	Microsoft.VisualBasic.dll.sources.win	\
	$(wildcard vbruntime*.rsp)		\
	Helper.vb				\
	$(wildcard *.sln)			\
	$(wildcard *.vbproj)

Microsoft.VisualBasic.dll.sources: 2010VB.vbproj
	MONO_PATH="../../class/lib/bootstrap$(PLATFORM_PATH_SEPARATOR)$$MONO_PATH" mono ../../tools/extract-source/extract-source.exe -x:r -s:2010VB.vbproj -d:Microsoft.VisualBasic.dll.sources -m:l
	MONO_PATH="../../class/lib/bootstrap$(PLATFORM_PATH_SEPARATOR)$$MONO_PATH" mono ../../tools/extract-source/extract-source.exe -x:r -s:2010VB.vbproj -d:Microsoft.VisualBasic.dll.sources.win -m:w

install-binary: default
	cp ../../class/lib/net_4_0/Microsoft.VisualBasic.* ../../class/lib/bootstrap
