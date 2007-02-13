thisdir = vbnc/msg-generator
SUBDIRS =
include ../../build/rules.make


PROGRAM = msg-gen.exe
PROGRAM_COMPILE = $(BOOT_COMPILE) -r:System.Xml.dll,System.dll,System.Data.dll,System.Windows.Forms.dll -imports:System.Collections,System,System.Console,System.Data,System.Diagnostics,System.Collections.Generic,Microsoft.VisualBasic

msg-gen.exe.sources: msg-generator.vbproj
	MONO_PATH="../../class/lib/bootstrap:$MONO_PATH" mono ../../tools/extract-source/extract-source.exe -s:msg-generator.vbproj -d:msg-gen.exe.sources -m:l -x:r

include ../../build/executable.make
