thisdir = vbnc/vbnc
SUBDIRS = tests
include ../../build/rules.make

LOCAL_VBNC_FLAGS = @vbnc.exe.rsp $(EXTERNAL_VBNC_FLAGS)

PROGRAM = $(topdir)/class/lib/vbnc/vbnc.exe
PROGRAM_COMPILE = $(BOOT_COMPILE)

include ../../build/executable.make
DISTFILES = \
	Makefile  		\
	Readme.txt  		\
	vbnc.Errors.resources	\
	vbnc.exe.rsp  		\
	vbnc.exe.sources  	\
	vbnc.sln  		\
	vbnc.vbnc.rsp		\
	source/vbnc.vbproj	\
	source/vbnc9.vbproj	\
	bin/vbnc.rsp            \
	source/Resources/Errors.resx \
	source/vbnc.rsp		\
	setversion.sh

version.tmp: $(topdir)/configure setversion.sh
	-$(topdir)/vbnc/vbnc/setversion.sh $(topdir)/vbnc/vbnc/source/General/Version.vb

all-local: vbnc.Errors.resources vbnc.exe.sources version.tmp

vbnc.Errors.resources: source/Resources/Errors.resx
	resgen2 source/Resources/Errors.resx vbnc.Errors.resources

vbnc.vbnc.rsp: source/vbnc.rsp
	cp source/vbnc.rsp vbnc.vbnc.rsp

vbnc.exe.sources: vbnc.vbnc.rsp source/vbnc9.vbproj vbnc.Errors.resources
	MONO_PATH="../../class/lib/bootstrap$(PLATFORM_PATH_SEPARATOR)$$MONO_PATH" mono ../../tools/extract-source/extract-source.exe -s:source/vbnc9.vbproj -d:vbnc.exe.sources -m:l -b:source/ -x:r

install-local: install-extras version.tmp
install-extras: 
	$(MKINSTALLDIRS) $(DESTDIR)$(PROGRAM_INSTALL_DIR)
	$(INSTALL_DATA) bin/vbnc.rsp $(DESTDIR)$(PROGRAM_INSTALL_DIR)

uninstall-local: uninstall-extras
uninstall-extras:
	rm -f $(DESTDIR)$(PROGRAM_INSTALL_DIR)/vbnc.rsp

install-binary: default
	cp ../../class/lib/vbnc/vbnc.* ../../class/lib/bootstrap

boot-from-ms:
	cd tests/SelfTest/ && boot.sh && rerun.sh
	cp tests/SelfTest/vbnc.exe ../../class/lib/bootstrap
	
test-thorough:
	cd tests/SelfTest/ && updatesources.sh
	cd tests/SelfTest/ && rerun.sh && rerun.sh && rerun.sh
	cp tests/SelfTest/vbnc.exe* ../../class/lib/bootstrap
	cd ../../vbruntime && make install && cp ../class/lib/vbnc/Microsoft.VisualBasic.dll* ../class/lib/bootstrap/
	cd ../../ && make clean && make install
	
	
