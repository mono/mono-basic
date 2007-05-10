thisdir = vbruntime/Microsoft.VisualBasic
include ../../build/rules.make

LIBRARY = Microsoft.VisualBasic.dll

ifeq ("$(FXVERSION)", "1")
LOCAL_VBNC_FLAGS = @vbruntime1.rsp $(EXTRA_VBRUNTIME_FLAGS)
else
LOCAL_VBNC_FLAGS = @vbruntime2.rsp $(EXTRA_VBRUNTIME_FLAGS)
endif

NO_SIGN_ASSEMBLY = true

LIBRARY_COMPILE = $(BOOT_COMPILE)

BUILT_FILES = strings.resources

strings.resources: strings.txt
	resgen strings.txt

include ../../build/library.make

DISTFILES =	\
	AssemblyInfo.vb				\
	msfinal.pub				\
	strings.txt				\
	2005VB.vbproj				\
	strings.resources			\
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
	MONO_PATH="../../class/lib/bootstrap$$PLATFORM_PATH_SEPARATOR$$MONO_PATH" mono ../../tools/extract-source/extract-source.exe -s:2005VB.vbproj -d:Microsoft.VisualBasic.dll.sources -m:l
	MONO_PATH="../../class/lib/bootstrap$$PLATFORM_PATH_SEPARATOR$$MONO_PATH" mono ../../tools/extract-source/extract-source.exe -s:2005VB.vbproj -d:Microsoft.VisualBasic.dll.sources.win -m:w
