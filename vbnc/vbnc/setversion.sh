#!/bin/bash -ex

VERSION_VB=$1
VERSION_TMP=version.tmp

GIT_BRANCH=`git branch | grep '^\*' | cut -d ' ' -f 2; true`
GIT_REVISION=`git log --no-color --first-parent -n1 --pretty=format:%h; true`

cat ../LicenseFileHeader.txt > $VERSION_TMP
echo "" >> $VERSION_TMP
echo -n "<Assembly: AssemblyInformationalVersion (\"" >> $VERSION_TMP
echo -n `grep VERSION ../../configure | grep -v echo | grep -v sed | sed 's/VERSION=//'` >> $VERSION_TMP
echo -n " - " >> $VERSION_TMP
if [[ "x$GIT_REVISION" != "x" ]]; then
	echo -n $GIT_BRANCH/$GIT_REVISION >> $VERSION_TMP
else
	echo -n tarball >> $VERSION_TMP
fi
echo "\")>" >> $VERSION_TMP

if ! diff $VERSION_TMP $VERSION_VB ; then
	cp $VERSION_TMP $VERSION_VB
fi

