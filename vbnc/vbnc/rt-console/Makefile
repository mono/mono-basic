thisdir = vbnc/rt-console
SUBDIRS =
include ../../build/rules.make

NO_INSTALL = yes

PROGRAM = rt-console.exe
PROGRAM_COMPILE = $(BOOT_COMPILE) -r:System.Xml.dll,System.dll -imports:System,Microsoft.VisualBasic,System.Collections,System.Collections.Generic,System.Diagnostics

include ../../build/executable.make

DISTFILES = \
	Makefile		\
	Readme.txt		\
	rt-console.vbproj

rt-console.exe.sources: rt-console.vbproj
	MONO_PATH="../../class/lib/bootstrap$(PLATFORM_PATH_SEPARATOR)$$MONO_PATH" mono ../../tools/extract-source/extract-source.exe -s:rt-console.vbproj -d:rt-console.exe.sources -m:l -x:r
