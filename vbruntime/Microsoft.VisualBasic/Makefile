thisdir = vbruntime/Microsoft.VisualBasic
include ../../build/rules.make

LIBRARY = Microsoft.VisualBasic.dll

LOCAL_VBNC_FLAGS = \
	@vbruntime.rsp

NO_SIGN_ASSEMBLY = true

LIBRARY_COMPILE = $(BOOT_COMPILE)

BUILT_FILES = strings.resources

strings.resources: strings.txt
	resgen strings.txt

include ../../build/library.make
