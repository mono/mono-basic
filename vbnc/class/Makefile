thisdir = class

include ../build/rules.make

lib/bootstrap lib/vbnc:
	$(MKINSTALLDIRS) $@

DISTFILES = \
	mono.snk	\
	lib/bootstrap/Microsoft.VisualBasic.dll lib/bootstrap/vbnc.exe lib/bootstrap/vbnc.rsp	

dist-default: lib/bootstrap/Microsoft.VisualBasic.dll lib/bootstrap/vbnc.exe lib/bootstrap/vbnc.rsp

DIST_PROFILE_DIRS = bootstrap vbnc

dist-local: dist-default
	-$(MKINSTALLDIRS) $(DIST_PROFILE_DIRS:%=$(distdir)/lib/%) $(COMPAT_PROFILE_DIRS:%=$(distdir)/compat/%)

clean-local test-local run-test-local run-test-ondotnet-local all-local install-local uninstall-local:
	@:
