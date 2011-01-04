thisdir = vbnc/vbnc/tests
SUBDIRS =
include ../../../build/rules.make

DISTFILES = ChangeLog Makefile Readme.txt Resources/Resources3.binary tests.xml
DISTFILES += \
	$(wildcard $(topdir)/vbnc/vbnc/tests/*/*.vb) \
	$(wildcard $(topdir)/vbnc/vbnc/tests/*/*.rsp) \
	$(wildcard $(topdir)/vbnc/vbnc/tests/*/*.response) \
	$(wildcard $(topdir)/vbnc/vbnc/tests/*/*.resources) \
	$(wildcard $(topdir)/vbnc/vbnc/tests/*/*.resx)  \
	$(wildcard $(topdir)/vbnc/vbnc/tests/*/*/*.vb) \
	$(wildcard $(topdir)/vbnc/vbnc/tests/*/*/*.rsp) \
	$(wildcard $(topdir)/vbnc/vbnc/tests/*/*/*.response) \
	$(wildcard $(topdir)/vbnc/vbnc/tests/*/*/*.resources) \
	$(wildcard $(topdir)/vbnc/vbnc/tests/*/*/*.resx)  \
	$(wildcard $(topdir)/vbnc/vbnc/tests/*/*/*/*.vb) \
	$(wildcard $(topdir)/vbnc/vbnc/tests/*/*/*/*.rsp) \
	$(wildcard $(topdir)/vbnc/vbnc/tests/*/*/*/*.response) \
	$(wildcard $(topdir)/vbnc/vbnc/tests/*/*/*/*.resources) \
	$(wildcard $(topdir)/vbnc/vbnc/tests/*/*/*/*.resx) 

make-dependencies:
	$(MAKE) -C ..
	$(MAKE) -C ../../../vbruntime/Microsoft.VisualBasic
	$(MAKE) -C ../../rt rt.exe

test-local: make-dependencies
	mono --debug $(PWD)/../../../class/lib/$(PROFILE)/rt.exe -console

clean-local run-test-local run-test-ondotnet-local all-local install-local uninstall-local:
	@:

dist-local: dist-default
