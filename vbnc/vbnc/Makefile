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
	bin/vbnc.rsp            \
	source/Resources/Errors.resx \
	source/vbnc.rsp

VERSION_VB=source/General/Version.vb

version: source/Version.vb source/VersionAttribute.vb ../../configure
	-cat ../License\ FileHeader.txt > $(VERSION_VB)
	-echo -n "<Assembly: AssemblyInformationalVersion (\"" >> $(VERSION_VB)
	-echo -n `more ../../configure | grep VERSION | grep -v echo | sed 's/VERSION=//'` >> $(VERSION_VB)
	-echo -n " - r" >> $(VERSION_VB)
	-echo -n `svn info ../../ | grep Revision | sed 's/Revision: //'` >> $(VERSION_VB)
	-echo -n "\")>" >> $(VERSION_VB)

all-local: vbnc.Errors.resources vbnc.exe.sources version

vbnc.Errors.resources: source/Resources/Errors.resx
	resgen2 source/Resources/Errors.resx vbnc.Errors.resources

vbnc.vbnc.rsp: source/vbnc.rsp
	cp source/vbnc.rsp vbnc.vbnc.rsp

vbnc.exe.sources: vbnc.vbnc.rsp source/vbnc.vbproj vbnc.Errors.resources
	MONO_PATH="../../class/lib/bootstrap$(PLATFORM_PATH_SEPARATOR)$$MONO_PATH" mono ../../tools/extract-source/extract-source.exe -s:source/vbnc.vbproj -d:vbnc.exe.sources -m:l -b:source/ -x:r

install-local: install-extras
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
	
	
