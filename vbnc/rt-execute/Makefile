thisdir = vbnc/rt-execute
SUBDIRS =
include ../../build/rules.make

NO_INSTALL = yes

PROGRAM = rt-execute.exe
PROGRAM_COMPILE = $(BOOT_COMPILE) -r:System.Xml.dll,System.dll -imports:System,Microsoft.VisualBasic,System.Collections,System.Collections.Generic,System.Diagnostics

include ../../build/executable.make

DISTFILES = \
	Makefile                \
	rt-execute.vbproj

rt-execute.exe.sources: rt-execute.vbproj
	MONO_PATH="../../class/lib/bootstrap$(PLATFORM_PATH_SEPARATOR)$$MONO_PATH" mono ../../tools/extract-source/extract-source.exe -s:rt-execute.vbproj -d:rt-execute.exe.sources -m:l -x:r
