thisdir = vbruntime
include ../build/rules.make

SUBDIRS := Microsoft.VisualBasic Test

DISTFILES = Makefile VB.build.bat

clean-local all-local install-local uninstall-local:
	@for p in $(PROFILES); do echo Building Microsoft.VisualBasic.dll for \'$$p\'; $(MAKE) $@ PROFILE=$$p -C Microsoft.VisualBasic; done

test-local run-test-local run-test-ondotnet-local:
	@:

dist-local: dist-default
