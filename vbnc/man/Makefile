thisdir = man
SUBDIRS =
include ../build/rules.make

DISTFILES = vbnc.1

install-local:
	test "$(man1_dir)" || mkdir "$(man1_dir)"
	/usr/bin/install -c -m 644 vbnc.1 "$(man1_dir)/vbnc.1"

clean-local test-local run-test-local run-test-ondotnet-local all-local install-local uninstall-local:
	@:

dist-local: dist-default

	
	

