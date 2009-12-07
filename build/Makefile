thisdir = build
SUBDIRS = 
include ../build/rules.make

all-local install-local test-local run-test-local run-test-ondotnet-local uninstall-local: $(BUILT_FILES)
	@:

clean-local:
	-rm -f $(BUILT_FILES) deps/*

PLATFORMS = linux win32

PROFILES = bootstrap vbnc moonlight

DISTFILES = \
	config-default.make		\
	executable.make			\
	library.make			\
	rules.make			\
	$(PLATFORMS:%=platforms/%.make) $(PROFILES:%=profiles/%.make)

dist-local: dist-default
	$(MKINSTALLDIRS) $(distdir)/deps
