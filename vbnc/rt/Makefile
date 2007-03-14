thisdir = vbnc/rt
SUBDIRS =
include ../../build/rules.make


PROGRAM = rt.exe
PROGRAM_COMPILE = $(BOOT_COMPILE) -r:System.Xml.dll,System.dll,System.Windows.Forms.dll,System.Drawing.dll -imports:System,Microsoft.VisualBasic,System.Collections,System.Collections.Generic,System.Diagnostics,System.Windows.Forms,System.Drawing -rootnamespace:rt

include ../../build/executable.make

DISTFILES = \
	Makefile                \
	source/rt.vbproj

rt.exe.sources: source/rt.vbproj
	MONO_PATH="../../class/lib/bootstrap:$$MONO_PATH" mono ../../tools/extract-source/extract-source.exe -s:source/rt.vbproj -b:source/ -d:rt.exe.sources -m:l -x:r
