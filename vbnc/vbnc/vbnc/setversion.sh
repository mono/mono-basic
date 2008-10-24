#!/bin/bash -ex

VERSION_VB=$1
VERSION_TMP=version.tmp

cat ../License\ FileHeader.txt > $VERSION_TMP
echo "" >> $VERSION_TMP
echo -n "<Assembly: AssemblyInformationalVersion (\"" >> $VERSION_TMP
echo -n `more ../../configure | grep VERSION | grep -v echo | grep -v sed | sed 's/VERSION=//'` >> $VERSION_TMP
echo -n " - r" >> $VERSION_TMP
echo -n `svn info . | grep Revision | sed 's/Revision: //'` >> $VERSION_TMP
echo -n "\")>" >> $VERSION_TMP

if ! diff $VERSION_TMP $VERSION_VB ; then
	cp $VERSION_TMP $VERSION_VB
fi

