thisdir := vbnc
SUBDIRS := cecil vbnc

rt_files = \
	rt/Makefile \
	rt/ChangeLog \
	rt/Readme.txt \
	rt/rt.exe.sources \
	$(wildcard rt/source/*)

rt_execute_files = \
	rt-execute/ChangeLog \
	rt-execute/Makefile \
	rt-execute/rt-execute.vb \
	rt-execute/rt-execute.vbproj \
	rt-execute/rt-execute.exe.sources

rt_console_files = \
	rt-console/ChangeLog \
	rt-console/Makefile \
	rt-console/Readme.txt \
	rt-console/rt-console.vbproj \
	rt-console/rt-console.exe.sources \
	$(wildcard rt-console/source/*)

	
EXTRA_DISTFILES = 			\
	all.sln				\
	changelog.txt			\
	CheckLicense.vbs		\
	clean.bat			\
	"License FileHeader.txt"	\
	License.txt			\
	progress.txt			\

DISTFILES =				\
	$(rt_files)			\
	$(rt_execute_files)		\
	$(rt_console_files)

include ../build/rules.make

clean-local test-local run-test-local run-test-ondotnet-local all-local install-local uninstall-local:
	@:

dist-local: dist-default
