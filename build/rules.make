# -*- makefile -*-
#
# This is the makefile fragment with default rules
# for building things in MCS
#
# To customize the build, you should edit config.make.
# If you need to edit this file, that's a bug; email
# peter@newton.cx about it.

# Some more variables. The leading period in the sed expression prevents
# thisdir = . from being changed into '..' for the toplevel directory.

dots := $(shell echo $(thisdir) |sed -e 's,[^./][^/]*,..,g')
topdir := $(dots)

VERSION = 0.0

USE_MCS_FLAGS = $(LOCAL_MCS_FLAGS) $(PLATFORM_MCS_FLAGS) $(PROFILE_MCS_FLAGS) $(MCS_FLAGS)
USE_VBNC_FLAGS = $(LOCAL_VBNC_FLAGS) $(PLATFORM_VBNC_FLAGS) $(PROFILE_VBNC_FLAGS) $(VBNC_FLAGS)
USE_CFLAGS = $(LOCAL_CFLAGS) $(CFLAGS)
CSCOMPILE = $(MCS) $(USE_MCS_FLAGS)
CCOMPILE = $(CC) $(USE_CFLAGS)
BOOTSTRAP_VBNC = MONO_PATH="$(topdir)/class/lib/bootstrap$(PLATFORM_PATH_SEPARATOR)$$MONO_PATH" $(RUNTIME) $(RUNTIME_FLAGS) --debug $(topdir)/class/lib/bootstrap/vbnc.exe
BOOT_COMPILE = $(BOOTSTRAP_VBNC) $(USE_VBNC_FLAGS)
VBNC = MONO_PATH="$(topdir)/class/lib/net_4_0$(PLATFORM_PATH_SEPARATOR)$$MONO_PATH" $(RUNTIME) $(RUNTIME_FLAGS) --debug $(topdir)/class/lib/net_4_0/vbnc.exe
INSTALL = $(SHELL) $(topdir)/install-sh
INSTALL_DATA = $(INSTALL) -c -m 644
INSTALL_BIN = $(INSTALL) -c -m 755
INSTALL_LIB = $(INSTALL_BIN)
MKINSTALLDIRS = $(SHELL) $(topdir)/mkinstalldirs
INTERNAL_MCS = mcs
INTERNAL_VBNC = $(RUNTIME) $(RUNTIME_FLAGS) $(topdir)/class/lib/$(PROFILE)/vbnc.exe
INTERNAL_ILASM = ilasm
INTERNAL_RESGEN = resgen
corlib = mscorlib.dll

depsdir = $(topdir)/build/deps

# Make sure these propagate if set manually

export PLATFORM
export PROFILE
export MCS
export VBNC
export VBNC_FLAGS
export MCS_FLAGS
export CC
export CFLAGS
export INSTALL
export MKINSTALLDIRS
export TEST_HARNESS
export BOOTSTRAP_MCS
export BOOTSTRAP_VBNC
export DESTDIR
export RESGEN

# Get this so the platform.make platform-check rule doesn't become the
# default target

.DEFAULT: all
default: all

# Get initial configuration. pre-config is so that the builder can
# override PLATFORM or PROFILE

include $(topdir)/build/config-default.make
-include $(topdir)/build/pre-config.make

# Default PLATFORM and PROFILE if they're not already defined.

ifndef PLATFORM
ifeq ($(OS),Windows_NT)
PLATFORM = win32
else
PLATFORM = linux
endif
endif

# Platform config

include $(topdir)/build/platforms/$(PLATFORM).make

ifdef PLATFORM_CORLIB
corlib = $(PLATFORM_CORLIB)
endif
# Useful

ifeq ($(PLATFORM_RUNTIME),$(RUNTIME))
PLATFORM_MONO_NATIVE = yes
endif

# Rest of the configuration

ifndef PROFILE
PROFILE = net_4_5
endif

include $(topdir)/build/profiles/$(PROFILE).make
-include $(topdir)/build/config.make


# vbnc is built in one the profiles
PROFILES = $(CONFIGURED_PROFILES)
PLATFORMS = linux win32



ifdef OVERRIDE_TARGET_ALL
all: all.override
else
all: do-all
endif

STD_TARGETS = test run-test run-test-ondotnet clean install uninstall

$(STD_TARGETS): %: do-%

do-run-test:
	ok=:; $(MAKE) run-test-recursive || ok=false; $(MAKE) run-test-local || ok=false; $$ok

do-%: %-recursive
	$(MAKE) $*-local

# The way this is set up, any profile-specific subdirs list should
# be listed _before_ including rules.make.  However, the default
# SUBDIRS list can come after, so don't use the eager := syntax when
# using the defaults.
PROFILE_SUBDIRS := $($(PROFILE)_SUBDIRS)
ifndef PROFILE_SUBDIRS
PROFILE_SUBDIRS = $(SUBDIRS)
endif

%-recursive:
	@set . $$MAKEFLAGS; final_exit=:; \
	case $$2 in --unix) shift ;; esac; \
	case $$2 in *=*) dk="exit 1" ;; *k*) dk=: ;; *) dk="exit 1" ;; esac; \
	list='$(PROFILE_SUBDIRS)'; for d in $$list ; do \
	    (cd $$d && $(MAKE) $*) || { final_exit="exit 1"; $$dk; } ; \
	done; \
	$$final_exit

ifndef DIST_SUBDIRS
DIST_SUBDIRS = $(SUBDIRS) $(DIST_ONLY_SUBDIRS)
endif
dist-recursive: dist-local
	@case '$(distdir)' in [\\/$$]* | ?:[\\/]* ) reldir='$(distdir)' ;; *) reldir='../$(distdir)' ;; esac ; \
	list='$(DIST_SUBDIRS)'; for d in $$list ; do \
	    (cd $$d && $(MAKE) distdir=$$reldir/$$d $@) || exit 1 ; \
	done

# The following target can be used like
#
#   dist-local: dist-default
#	... additional commands ...
#
# Notes:
#  1. we invert the test here to not end in an error if ChangeLog doesn't exist.
#  2. we error out if we try to dist a nonexistant file.
#  3. we pick up Makefile, makefile, or GNUmakefile.
dist-default:
	-mkdir -p $(distdir)
	test '!' -f ChangeLog || cp ChangeLog $(distdir)
	if test -f Makefile; then m=M; fi; \
	if test -f makefile; then m=m; fi; \
	if test -f GNUmakefile; then m=GNUm; fi; \
	for f in $${m}akefile $(DISTFILES) ; do \
	    dest=`dirname $(distdir)/$$f` ; \
	    $(MKINSTALLDIRS) $$dest && cp -p $$f $$dest || exit 1 ; \
	done

# Useful

withmcs:
	$(MAKE) MCS='$(INTERNAL_MCS)' BOOTSTRAP_MCS='$(INTERNAL_MCS)' all

dll-sources:
	echo "../../build/common/Consts.cs" > $(LIBRARY).sources
	echo "../../build/common/MonoTODOAttribute.cs" >> $(LIBRARY).sources
	ls */*.cs >> $(LIBRARY).sources
	cd Test; ls */*.cs > ../$(LIBRARY:.dll=_test.dll).sources; cd ..

$(depsdir):
	mkdir -p $(depsdir)
