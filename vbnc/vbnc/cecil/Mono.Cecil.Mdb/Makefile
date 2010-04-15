thisdir = vbnc/cecil/Mono.Cecil.Mdb
include ../../../build/rules.make

MCS = gmcs
LIBRARY = Mono.Cecil.VB.Mdb.dll
LIBRARY_SNK = ../mono.snk
LIBRARY_PACKAGE = none

CECIL = $(topdir)/class/lib/$(PROFILE)/Mono.Cecil.VB.dll

LIB_MCS_FLAGS = /r:$(corlib) /r:$(CECIL) /d:CECIL -keyfile:$(LIBRARY_SNK)

NO_TEST = yes

ifneq (net_1_1, $(PROFILE))
NO_INSTALL = yes
endif

include ../../../build/library.make
