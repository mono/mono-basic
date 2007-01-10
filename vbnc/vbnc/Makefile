thisdir = vbnc/vbnc
SUBDIRS =
include ../../build/rules.make

BOOTSTRAP_MCS = MONO_PATH="$(topdir)/class/lib/net_2_0_bootstrap$(PLATFORM_PATH_SEPARATOR)$$MONO_PATH" $(RUNTIME) $(RUNTIME_FLAGS) $(topdir)/class/lib/net_2_0_bootstrap/mcs.exe $(DEBUG_FLAGS)


LOCAL_VBNC_FLAGS = /res:source/vbnc.rsp /res:source/Resources/Errors.resx

PROGRAM = $(topdir)/class/lib/$(PROFILE)/vbnc.exe

include ../../build/executable.make
