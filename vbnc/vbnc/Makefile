thisdir = vbnc/vbnc
SUBDIRS = tests
include ../../build/rules.make

LOCAL_VBNC_FLAGS = @vbnc.exe.rsp

PROGRAM = $(topdir)/class/lib/vbnc/vbnc.exe
PROGRAM_config = bin/vbnc.rsp
PROGRAM_COMPILE = $(BOOT_COMPILE)

include ../../build/executable.make
DISTFILES = \
	Makefile  		\
	Readme.txt  		\
	vbnc.Errors.resources	\
	vbnc.exe.rsp  		\
	vbnc.exe.sources  	\
	vbnc.sln  		\
	vbnc.vbnc.rsp		\
	source/vbnc.vbproj	\
	bin/vbnc.rsp


vbnc.exe.sources: vbnc.vbnc.rsp source/vbnc.vbproj
	MONO_PATH="../../class/lib/bootstrap:$(MONO_PATH)" mono ../../tools/extract-source/extract-source.exe -s:source/vbnc.vbproj -d:vbnc.exe.sources -m:l -b:source/ -x:r
