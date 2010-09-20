thisdir = vbruntime
include ../build/rules.make

SUBDIRS := Microsoft.VisualBasic Test

DISTFILES = Makefile VB.build.bat

clean-local all-local install-local uninstall-local:
	$(MAKE) $@ PROFILE=vbnc -C Microsoft.VisualBasic
	$(MAKE) $@ PROFILE=net_4_0 -C Microsoft.VisualBasic

test-local run-test-local run-test-ondotnet-local:
	@:

dist-local: dist-default
