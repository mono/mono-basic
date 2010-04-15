thisdir = vbnc/rt-console
SUBDIRS =
include ../../build/rules.make

NO_INSTALL = yes

REFERENCES = \
	-r:System.Xml.dll				\
	-r:System.dll					\
	-r:System.Drawing.dll				\
	-r:../../class/lib/vbnc/Mono.Cecil.VB.dll

IMPORTS = \
	-imports:System,Microsoft.VisualBasic	\
	-imports:System.Collections		\
	-imports:System.Collections.Generic	\
	-imports:System.Diagnostics,System.Xml

PROGRAM = rt-console.exe
PROGRAM_COMPILE = $(BOOT_COMPILE) $(REFERENCES) $(IMPORTS)

include ../../build/executable.make

DISTFILES = \
	Makefile		\
	Readme.txt		\
	rt-console.vbproj

rt-console.exe.sources: rt-console.vbproj
	MONO_PATH="../../class/lib/bootstrap$(PLATFORM_PATH_SEPARATOR)$$MONO_PATH" mono ../../tools/extract-source/extract-source.exe -s:rt-console.vbproj -d:rt-console.exe.sources -m:l -x:r
