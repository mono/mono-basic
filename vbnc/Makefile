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

EXTRA_DISTFILES = 			\
	all.sln				\
	changelog.txt			\
	CheckLicense.vbs		\
	clean.bat			\
	"License FileHeader.txt"	\
	License.txt			\
	progress.txt			\
	$(ac_files)

include ../build/rules.make

test-local run-test-local run-test-ondotnet-local all-local install-local uninstall-local:
        @:
