thisdir = vbnc/cecil/Mono.Cecil
SUBDIRS = 

include ../../../build/rules.make

MCS = gmcs
LIBRARY = Mono.Cecil.VB.dll
LIBRARY_SNK = ../mono.snk
LIBRARY_PACKAGE = none

LIB_MCS_FLAGS = /r:$(corlib) /r:System.dll -keyfile:$(LIBRARY_SNK)

NO_TEST = yes

ifneq (net_2_0, $(PROFILE))
NO_INSTALL = yes
endif

include ../../../build/library.make
