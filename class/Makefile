thisdir = class

include ../build/rules.make

lib/bootstrap lib/vbnc:
	$(MKINSTALLDIRS) $@

DISTFILES = \
	mono.snk	\
	lib/bootstrap/Microsoft.VisualBasic.dll \
	lib/bootstrap/Microsoft.VisualBasic.dll.mdb \
	lib/bootstrap/vbnc.exe \
	lib/bootstrap/vbnc.exe.mdb \
	lib/bootstrap/vbnc.rsp \
	lib/bootstrap/Mono.Cecil.VB.dll \
	lib/bootstrap/Mono.Cecil.VB.dll.mdb \
	lib/bootstrap/Mono.Cecil.VB.Mdb.dll \
	lib/bootstrap/Mono.Cecil.VB.Mdb.dll.mdb \
	lib/bootstrap/Mono.Cecil.VB.Pdb.dll \
	lib/bootstrap/Mono.Cecil.VB.Pdb.dll.mdb

dist-default: lib/bootstrap/Microsoft.VisualBasic.dll lib/bootstrap/vbnc.exe lib/bootstrap/vbnc.rsp

DIST_PROFILE_DIRS = bootstrap net_2_0 net_4_0 monotouch monodroid moonlight

dist-local: dist-default
	-$(MKINSTALLDIRS) $(DIST_PROFILE_DIRS:%=$(distdir)/lib/%) $(COMPAT_PROFILE_DIRS:%=$(distdir)/compat/%)

clean-local test-local run-test-local run-test-ondotnet-local all-local install-local uninstall-local:
	@:
