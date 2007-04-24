thisdir := vbnc
SUBDIRS := vbnc

ac_files =	\
	"ac/My Project/Resources.Designer.vb"	\
	"ac/My Project/Settings.settings"	\
	"ac/My Project/AssemblyInfo.vb"		\
	"ac/My Project/Settings.Designer.vb"	\
	"ac/My Project/Application.Designer.vb"	\
	"ac/My Project/Application.myapp"	\
	"ac/My Project/Resources.resx"		\
	ac/License.txt				\
	ac/ac.vbproj				\
	ac/Startup.vb				\
	ac/ac.sln				\
	ac/AssemblyComparer.vb

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
	$(ac_files)			

DISTFILES =				\
	$(rt_files)			\
	$(rt_execute_files)		\
	$(rt_console_files)

include ../build/rules.make

clean-local test-local run-test-local run-test-ondotnet-local all-local install-local uninstall-local:
	@:

dist-local: dist-default
