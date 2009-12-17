thisdir := .

SUBDIRS := build man class tools vbnc vbruntime scripts

include build/rules.make

all-recursive $(STD_TARGETS:=-recursive): platform-check profile-check

.PHONY: all-local $(STD_TARGETS:=-local)
all-local $(STD_TARGETS:=-local):
	@:

DISTFILES = README configure mkinstalldirs install-sh LICENSE

# fun specialty targets

PROFILES = default net_2_0

.PHONY: all-profiles $(STD_TARGETS:=-profiles)
all-profiles $(STD_TARGETS:=-profiles): %-profiles: profiles-do--%
	@:

profiles-do--%:
	$(MAKE) $(PROFILES:%=profile-do--%--$*)

# The % below looks like profile-name--target-name
profile-do--%:
	$(MAKE) PROFILE=$(subst --, ,$*)

# We don't want to run the tests in parallel.  We want behaviour like -k.
profiles-do--run-test:
	ret=:; $(foreach p,$(PROFILES), { $(MAKE) PROFILE=$(p) run-test || ret=false; }; ) $$ret

# Orchestrate the bootstrap here.
_boot_ = all clean install
$(_boot_:%=profile-do--net_2_0--%):           profile-do--net_2_0--%:           profile-do--net_2_0_bootstrap--%
$(_boot_:%=profile-do--net_2_0_bootstrap--%): profile-do--net_2_0_bootstrap--%: profile-do--default--%
$(_boot_:%=profile-do--default--%):           profile-do--default--%:           profile-do--net_1_1_bootstrap--%
$(_boot_:%=profile-do--net_1_1_bootstrap--%): profile-do--net_1_1_bootstrap--%: profile-do--basic--%

testcorlib:
	@cd class/corlib && $(MAKE) test run-test

compiler-tests:
	$(MAKE) TEST_SUBDIRS="tests errors" run-test-profiles

test-installed-compiler:
	$(MAKE) TEST_SUBDIRS="tests errors" PROFILE=default TEST_RUNTIME=mono MCS=mcs run-test
	$(MAKE) TEST_SUBDIRS="tests errors" PROFILE=net_2_0 TEST_RUNTIME=mono MCS=gmcs run-test

package := mono-basic-$(VERSION)

dist-local: dist-default

dist-pre:
	rm -rf $(package)
	mkdir $(package)

dist-tarball: dist-pre
	$(MAKE) distdir='$(package)' dist-recursive
	tar cvjf $(package).tar.bz2 $(package)

dist: dist-tarball
	rm -rf $(package)

# the egrep -v is kind of a hack (to get rid of the makefrags)
# but otherwise we have to make dist then make clean which
# is sort of not kosher. And it breaks with DIST_ONLY_SUBDIRS.
#
# We need to set prefix on make so class/System/Makefile can find
# the installed System.Xml to build properly

distcheck: dist-tarball
	rm -rf InstallTest Distcheck-MONOBASIC ; \
	mkdir InstallTest ; \
	destdir=`cd InstallTest && pwd` ; \
	mv $(package) Distcheck-MONOBASIC ; \
	(cd Distcheck-MONOBASIC && \
	    ./configure --prefix=$(prefix) && \
	    $(MAKE) prefix=$(prefix) && $(MAKE) test && $(MAKE) install DESTDIR="$$destdir" && \
	    $(MAKE) clean && $(MAKE) dist || exit 1) || exit 1 ; \
	mv Distcheck-MONOBASIC $(package) ; \
	tar tjf $(package)/$(package).tar.bz2 |sed -e 's,/$$,,' |sort >distdist.list ; \
	rm $(package)/$(package).tar.bz2 ; \
	tar tjf $(package).tar.bz2 |sed -e 's,/$$,,' |sort >before.list ; \
	find $(package) |egrep -v '(makefrag|response|config.make)' |sed -e 's,/$$,,' |sort >after.list ; \
	cmp before.list after.list || exit 1 ; \
	cmp before.list distdist.list || exit 1 ; \
	rm -f before.list after.list distdist.list ; \
	rm -rf $(package) InstallTest

vbnc: vbnc.in Makefile
	sed -e s,@prefix@,$(prefix),g < vbnc.in > $@.tmp
	mv $@.tmp $@

