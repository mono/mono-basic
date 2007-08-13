# -*- makefile -*-

VBNC = MONO_PATH="$(topdir)/class/lib/$(PROFILE):$$MONO_PATH" $(RUNTIME) $(RUNTIME_FLAGS) --debug $(topdir)/class/lib/vbnc/vbnc.exe 
BOOTSTRAP_VBNC = MONO_PATH="$(topdir)/class/lib/bootstrap$(PLATFORM_PATH_SEPARATOR)$$MONO_PATH" $(RUNTIME) $(RUNTIME_FLAGS) --debug  $(topdir)/class/lib/bootstrap/vbnc.exe

# nuttzing!

profile-check:
	@:

PROFILE_VBNC_FLAGS =  -define:MOON_VERSION=1.1,MOONLIGHT=True
FRAMEWORK_VERSION = 2.0
