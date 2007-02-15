thisdir = vbnc/rt-console
SUBDIRS =
include ../../build/rules.make


PROGRAM = rt-console.exe
PROGRAM_COMPILE = $(VBNC) -r:System.Xml.dll,System.dll -imports:System,Microsoft.VisualBasic,System.Collections,System.Collections.Generic,System.Diagnostics

include ../../build/executable.make

rt-console.exe.sources: rt-console.vbproj
	MONO_PATH="../../class/lib/bootstrap:$MONO_PATH" mono ../../tools/extract-source/extract-source.exe -s:rt-console.vbproj -d:rt-console.exe.sources -m:l -x:r
