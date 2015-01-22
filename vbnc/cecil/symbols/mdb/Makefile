thisdir := vbnc/cecil/symbols/mdb
SUBDIRS := 

include ../../../../build/rules.make

LIBRARY = Mono.Cecil.VB.Mdb.dll
LIBRARY_COMPILE = mcs -keyfile:$(topdir)/vbnc/cecil/mono.snk -r:$(topdir)/class/lib/$(PROFILE)/Mono.Cecil.VB.dll -d:CECIL -debug -r:System.dll
BUILT_FILES = $(topdir)/class/lib/$(PROFILE)/Mono.Cecil.VB.dll

include ../../../../build/library.make

DISTFILES = \
	$(wildcard *.csproj)	\
	Mono.Cecil.VB.Mdb.dll.sources

Mono.Cecil.VB.Mdb.dll.sources: Mono.Cecil.Mdb.VB.csproj
	grep "Compile Include" Mono.Cecil.Mdb.VB.csproj | sed 's_.*["]\(.*\)["].*_\1_' | sed 's_\\_/_' > $@

$(topdir)/class/lib/$(PROFILE)/Mono.Cecil.VB.dll:
	$(MAKE) ../../class/lib/$(PROFILE)/Mono.Cecil.VB.dll PROFILE=$(PROFILE) -C ../..
