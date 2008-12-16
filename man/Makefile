thisdir = man
SUBDIRS =
include ../build/rules.make

DISTFILES = vbnc.1

MANDIR=$(DESTDIR)$(man1_dir)

install-local:
	test -z "$(MANDIR)" || mkdir -p "$(MANDIR)"
	/usr/bin/install -c -m 644 vbnc.1 "$(MANDIR)/vbnc.1"

clean-local test-local run-test-local run-test-ondotnet-local all-local uninstall-local:
	@:

dist-local: dist-default

	
	

