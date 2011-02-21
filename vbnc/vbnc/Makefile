thisdir = vbnc/vbnc
SUBDIRS = tests
include ../../build/rules.make

LOCAL_VBNC_FLAGS = @vbnc.exe.rsp $(EXTERNAL_VBNC_FLAGS) /r:../../class/lib/$(PROFILE)/Mono.Cecil.VB.dll

PROGRAM = $(topdir)/class/lib/$(PROFILE)/vbnc.exe
PROGRAM_COMPILE = $(BOOT_COMPILE) /novbruntimeref /r:../../class/lib/bootstrap/Microsoft.VisualBasic.dll

include ../../build/executable.make

DISTFILES = \
	ChangeLog 		\
	Makefile  		\
	Readme.txt  		\
	vbnc.Errors.resources	\
	vbnc.exe.rsp  		\
	vbnc.exe.sources  	\
	vbnc.vbnc.rsp		\
	source/vbnc.vbproj	\
	source/Resources/Errors.resx \
	source/vbnc.rsp		\
	setversion.sh

CLEAN_FILES = \
	version.tmp 					\
	$(topdir)/vbnc/vbnc/source/General/Version.vb 	\
	../../class/lib/$(PROFILE)/vbnc.rsp

version.tmp: $(topdir)/configure setversion.sh
	-$(topdir)/vbnc/vbnc/setversion.sh $(topdir)/vbnc/vbnc/source/General/Version.vb

all-local: vbnc.Errors.resources vbnc.exe.sources version.tmp
	@cp -p vbnc.vbnc.rsp ../../class/lib/$(PROFILE)/vbnc.rsp
	
vbnc.Errors.resources: source/Resources/Errors.resx
	resgen2 source/Resources/Errors.resx vbnc.Errors.resources

vbnc.vbnc.rsp: source/vbnc.rsp
	cp source/vbnc.rsp vbnc.vbnc.rsp

vbnc.exe.sources: vbnc.vbnc.rsp source/vbnc.vbproj vbnc.Errors.resources
	MONO_PATH="../../class/lib/bootstrap$(PLATFORM_PATH_SEPARATOR)$$MONO_PATH" mono ../../tools/extract-source/extract-source.exe -s:source/vbnc.vbproj -d:vbnc.exe.sources -m:l -b:source/ -x:r

install-local: install-extras version.tmp
install-extras: 
	$(MKINSTALLDIRS) $(DESTDIR)$(PROGRAM_INSTALL_DIR)
	$(INSTALL_DATA) source/vbnc.rsp $(DESTDIR)$(PROGRAM_INSTALL_DIR)

uninstall-local: uninstall-extras
uninstall-extras:
	rm -f $(DESTDIR)$(PROGRAM_INSTALL_DIR)/vbnc.rsp

install-binary: default
	cp ../../class/lib/$(PROFILE)/vbnc.* ../../class/lib/bootstrap
	cp ../../class/lib/$(PROFILE)/Mono.Cecil.* ../../class/lib/bootstrap
	cp source/vbnc.rsp ../../class/lib/bootstrap/vbnc.rsp

boot-from-ms:
	cd tests/SelfTest/ && boot.sh && rerun.sh
	cp tests/SelfTest/vbnc.exe ../../class/lib/bootstrap
	
test-thorough:
	cd tests/SelfTest/ && updatesources.sh
	cd tests/SelfTest/ && rerun.sh && rerun.sh && rerun.sh
	cp tests/SelfTest/vbnc.exe* ../../class/lib/bootstrap
	cd ../../vbruntime && make install && cp ../class/lib/vbnc/Microsoft.VisualBasic.dll* ../class/lib/bootstrap/
	cd ../../ && make clean && make install
	
	
