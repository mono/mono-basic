# -*- makefile -*-
#
# The default 'bootstrap' profile -- builds so that we link against
# the libraries as we build them.
#
# We use the platform's native C# runtime and compiler if possible.

# Note that we have sort of confusing terminology here; BOOTSTRAP_MCS
# is what allows us to bootstrap ourselves, but when we are bootstrapping,
# we use INTERNAL_MCS.

# When bootstrapping, compile against our new assemblies.
# (MONO_PATH doesn't just affect what assemblies are loaded to
# run the compiler; /r: flags are by default loaded from whatever's
# in the MONO_PATH too).

VBNC = MONO_PATH="$(topdir)/class/lib/$(PROFILE):$$MONO_PATH" $(RUNTIME) $(RUNTIME_FLAGS) --debug $(topdir)/class/lib/bootstrap/vbnc.exe
BOOTSTRAP_VBNC = MONO_PATH="$(topdir)/class/lib/bootstrap$(PLATFORM_PATH_SEPARATOR)$$MONO_PATH" $(RUNTIME) $(RUNTIME_FLAGS) --debug  $(topdir)/class/lib/bootstrap/vbnc.exe


# nuttzing!

profile-check:
