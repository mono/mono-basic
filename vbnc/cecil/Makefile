thisdir := vbnc/cecil
SUBDIRS := symbols

include ../../build/rules.make

LIBRARY = Mono.Cecil.VB.dll
LIBRARY_COMPILE = mcs -keyfile:$(topdir)/$(thisdir)/mono.snk -d:CECIL -debug
#LIBRARY_SNK = $(topdir)/$(thisdir)/mono.snk

include ../../build/library.make

DISTFILES = \
	$(wildcard *.csproj)	\
	mono.snk	\
	Mono.Cecil.VB.dll.sources

Mono.Cecil.VB.dll.sources: Mono.Cecil.VB.csproj
	grep "Compile Include" Mono.Cecil.VB.csproj | sed 's_.*["]\(.*\)["].*_\1_' | sed 's_\\_/_' > $@
