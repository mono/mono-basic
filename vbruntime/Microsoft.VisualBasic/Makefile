thisdir = vbruntime/Microsoft.VisualBasic
include ../../build/rules.make

LIBRARY = Microsoft.VisualBasic.dll

ifeq ("$(PROFILE)", "moonlight")
LOCAL_VBNC_FLAGS = @vbruntime-moonlight.rsp $(EXTRA_VBRUNTIME_FLAGS)
LIBRARY_SNK = $(topdir)/$(thisdir)/mono.snk
else
LOCAL_VBNC_FLAGS = @vbruntime2.rsp $(EXTRA_VBRUNTIME_FLAGS)
endif

LIBRARY_COMPILE = $(BOOT_COMPILE)

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
	2005VB.vbproj				\
	strings2.resources			\
	ChangeLog				\
	Makefile				\
	Microsoft.VisualBasic.dll.sources	\
	Microsoft.VisualBasic.dll.sources.win	\
	Microsoft.VisualBasic.dll.moonlight.sources\
	vbruntime.rsp				\
	vbruntime2.rsp				\
	vbruntime-moonlight.rsp			\
	Helper.vb				\
	2005VB.sln

Microsoft.VisualBasic.dll.sources: 2005VB.vbproj
	MONO_PATH="../../class/lib/bootstrap$(PLATFORM_PATH_SEPARATOR)$$MONO_PATH" mono ../../tools/extract-source/extract-source.exe -x:r -s:2005VB.vbproj -d:Microsoft.VisualBasic.dll.sources -m:l
	MONO_PATH="../../class/lib/bootstrap$(PLATFORM_PATH_SEPARATOR)$$MONO_PATH" mono ../../tools/extract-source/extract-source.exe -x:r -s:2005VB.vbproj -d:Microsoft.VisualBasic.dll.sources.win -m:w

install-binary: default
	cp ../../class/lib/vbnc/Microsoft.VisualBasic.* ../../class/lib/bootstrap
