thisdir = tools/extract-source
SUBDIRS =
include ../../build/rules.make

NO_INSTALL = yes

PROGRAM = extract-source.exe
PROGRAM_COMPILE = $(BOOT_COMPILE) -r:System.Xml.dll -noconfig -define:_MYTYPE=\"Empty\"

include ../../build/executable.make
