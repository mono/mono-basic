thisdir = tools/moonlight
topdir = ../../

include ../../build/rules.make

DISTFILES = Makefile tuner.vb

DLL_TO_TUNE = $(topdir)/class/lib/moonlight/Microsoft.VisualBasic.dll
TUNED_DLL = $(topdir)/class/lib/2.1/Microsoft.VisualBasic.dll

clean-local:
	@rm -f tuner.exe*

test-local run-test-local run-test-ondotnet-local all-local install-local uninstall-local:
	@:

dist-local: dist-default

tuner.exe: tuner.vb
	mono --debug $(topdir)/class/lib/bootstrap/vbnc.exe tuner.vb -out:tuner.exe -r:`pkg-config --variable=Libraries cecil` -debug+ -r:$(topdir)/class/lib/bootstrap/Microsoft.VisualBasic.dll -noconfig -novbruntimeref

moonlight: tuner.exe
	mkdir -p $(topdir)/class/lib/moonlight
	$(MAKE) -C ../../vbruntime/Microsoft.VisualBasic PROFILE=moonlight
	
tune: moonlight tuner.exe
	mkdir -p $(topdir)/class/lib/2.1/
	mono --debug tuner.exe $(DLL_TO_TUNE) $(TUNED_DLL)
	-cp $(DLL_TO_TUNE).mdb $(TUNED_DLL).mdb
	
install-moonlight: tune
	cp $(TUNED_DLL)* $(prefix)/lib/moonlight/plugin

verify: tune
	MONO_PATH=$(prefix)/lib/moonlight/plugin pedump --verify error,code $(TUNED_DLL)