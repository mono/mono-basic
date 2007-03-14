thisdir = scripts

include ../build/rules.make

PROGRAM_INSTALL_DIR = $(prefix)/bin

all-local: vbnc

vbnc: vbnc.in Makefile
	sed -e s,@prefix@,$(prefix),g < vbnc.in > $@.tmp
	mv $@.tmp $@

install-local: all-local
	$(MKINSTALLDIRS) $(DESTDIR)$(PROGRAM_INSTALL_DIR)
	$(INSTALL_BIN) vbnc $(DESTDIR)$(PROGRAM_INSTALL_DIR)

uninstall-local: 
	-rm -f $(DESTDIR)$(PROGRAM_INSTALL_DIR)/vbnc

DISTFILES = vbnc.in Makefile

clean-local:
	rm -f vbnc

test-local run-test-local run-test-ondotnet-local all-local:
	@:

dist-local: dist-default
