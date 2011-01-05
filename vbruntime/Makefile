thisdir = vbruntime
include ../build/rules.make

SUBDIRS := Microsoft.VisualBasic Test

DISTFILES = Makefile VB.build.bat

clean-local all-local install-local uninstall-local:
	@set . $$MAKEFLAGS; final_exit=:; \
	for p in $(PROFILES) ; do \
	    (cd Microsoft.VisualBasic && $(MAKE) profile-check PROFILE=$$p $@) || { final_exit="exit 1"; exit 1; } ; \
	done; \

test-local run-test-local run-test-ondotnet-local:
	@:

dist-local: dist-default
