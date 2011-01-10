thisdir = scripts

include ../build/rules.make

PROGRAM_INSTALL_DIR = $(prefix)/bin

all-local: vbnc vbnc2

vbnc vbnc2: vbnc.in vbnc2.in Makefile
	sed -e s,@prefix@,$(prefix),g < $@.in > $@.tmp
	mv $@.tmp $@

install-local: all-local
	$(MKINSTALLDIRS) $(DESTDIR)$(PROGRAM_INSTALL_DIR)
	$(INSTALL_BIN) vbnc $(DESTDIR)$(PROGRAM_INSTALL_DIR)
	$(INSTALL_BIN) vbnc2 $(DESTDIR)$(PROGRAM_INSTALL_DIR)

uninstall-local: 
	-rm -f $(DESTDIR)$(PROGRAM_INSTALL_DIR)/vbnc
	-rm -f $(DESTDIR)$(PROGRAM_INSTALL_DIR)/vbnc2

DISTFILES = vbnc.in vbnc2.in Makefile

clean-local:
	rm -f vbnc vbnc2

test-local run-test-local run-test-ondotnet-local all-local:
	@:

dist-local: dist-default
