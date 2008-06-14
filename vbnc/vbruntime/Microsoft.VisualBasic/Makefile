thisdir = vbruntime/Microsoft.VisualBasic
include ../../build/rules.make

LIBRARY = Microsoft.VisualBasic.dll

ifeq ("$(PROFILE)", "moonlight")
LOCAL_VBNC_FLAGS = @vbruntime-moonlight.rsp $(EXTRA_VBRUNTIME_FLAGS)
else
ifeq ("$(FXVERSION)", "1")
LOCAL_VBNC_FLAGS = @vbruntime1.rsp $(EXTRA_VBRUNTIME_FLAGS)
else
LOCAL_VBNC_FLAGS = @vbruntime2.rsp $(EXTRA_VBRUNTIME_FLAGS)
endif
endif

LIBRARY_COMPILE = $(BOOT_COMPILE)

BUILT_FILES = strings1.resources strings2.resources

strings1.resources: strings.txt strings-only1.txt
	cat strings.txt > strings1.txt
	cat strings-only1.txt >> strings1.txt
	resgen strings1.txt strings1.resources

strings2.resources: strings.txt strings-only2.txt
	cat strings.txt > strings2.txt
	cat strings-only2.txt >> strings2.txt
	resgen2 strings2.txt strings2.resources

include ../../build/library.make

DISTFILES =	\
	AssemblyInfo.vb				\
	msfinal.pub				\
	strings.txt				\
	strings-only1.txt			\
	strings-only2.txt			\
	2005VB.vbproj				\
	strings1.resources			\
	strings2.resources			\
	ChangeLog				\
	Makefile				\
	Microsoft.VisualBasic.dll.sources	\
	Microsoft.VisualBasic.dll.sources.win	\
	vbruntime.rsp				\
	vbruntime1.rsp				\
	vbruntime2.rsp				\
	Helper.vb				\
	2005VB.sln

Microsoft.VisualBasic.dll.sources: 2005VB.vbproj
	MONO_PATH="../../class/lib/bootstrap$$PLATFORM_PATH_SEPARATOR$$MONO_PATH" mono ../../tools/extract-source/extract-source.exe -x:r -s:2005VB.vbproj -d:Microsoft.VisualBasic.dll.sources -m:l
	MONO_PATH="../../class/lib/bootstrap$$PLATFORM_PATH_SEPARATOR$$MONO_PATH" mono ../../tools/extract-source/extract-source.exe -x:r -s:2005VB.vbproj -d:Microsoft.VisualBasic.dll.sources.win -m:w

install-binary: default
	cp ../../class/lib/vbnc/Microsoft.VisualBasic.* ../../class/lib/bootstrap
