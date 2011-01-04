thisdir = vbnc/rt
SUBDIRS =
include ../../build/rules.make

NO_INSTALL = yes

REFERENCES = \
	-r:System.Xml.dll \
	-r:System.dll \
	-r:System.Windows.Forms.dll \
	-r:System.Drawing.dll \
	-r:../../class/lib/$(PROFILE)/Mono.Cecil.VB.dll

IMPORTS = \
	-imports:System,Microsoft.VisualBasic \
	-imports:System.Collections \
	-imports:System.Collections.Generic \
	-imports:System.Diagnostics \
	-imports:System.Windows.Forms \
	-imports:System.Drawing \
	-imports:System.Xml

TARGET = ../../class/lib/$(PROFILE)/rt.exe

DISTFILES = \
	Makefile	\
	ChangeLog	\
	Readme.txt	\
	source/rt.vbproj	\
	source/rt.sln	\
	$(wildcard source/*.vb)	\
	$(wildcard source/*.resx)	\
	source/ChangeLog	\

rt.exe.sources: source/rt.vbproj
	MONO_PATH="../../class/lib/bootstrap$(PLATFORM_PATH_SEPARATOR)$$MONO_PATH" mono ../../tools/extract-source/extract-source.exe -s:source/rt.vbproj -b:source/ -d:rt.exe.sources -m:l -x:r

$(TARGET): rt.exe.sources
	$(BOOT_COMPILE) -rootnamespace:rt -out:$(TARGET) $(REFERENCES) $(IMPORTS) @rt.exe.sources -debug:full -define:_MYTYPE=\"Empty\"

rt.exe: $(TARGET)

clean-local:
	rm -f $(TARGET)
	rm -f $(TARGET).mdb
	rm -f rt.exe.sources

test-local run-test-local run-test-ondotnet-local all-local install-local uninstall-local:
	@:

dist-local: dist-default
