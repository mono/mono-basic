thisdir := vbnc/cecil/symbols/pdb
SUBDIRS := 

include ../../../../build/rules.make

LIBRARY = Mono.Cecil.VB.Pdb.dll
LIBRARY_COMPILE = mcs -keyfile:$(topdir)/vbnc/cecil/mono.snk -r:$(topdir)/class/lib/$(PROFILE)/Mono.Cecil.VB.dll -d:CECIL -debug

BUILT_FILES = $(topdir)/class/lib/$(PROFILE)/Mono.Cecil.VB.dll

include ../../../../build/library.make

DISTFILES = \
	$(wildcard *.csproj)		\
	Mono.Cecil.VB.Pdb.dll.sources

Mono.Cecil.VB.Pdb.dll.sources: Mono.Cecil.Pdb.VB.csproj
	grep "Compile Include" Mono.Cecil.Pdb.VB.csproj | sed 's_.*["]\(.*\)["].*_\1_' | sed 's_\\_/_' > $@

$(topdir)/class/lib/$(PROFILE)/Mono.Cecil.VB.dll:
	$(MAKE) ../../class/lib/$(PROFILE)/Mono.Cecil.VB.dll PROFILE=$(PROFILE) -C ../..
