#!/bin/bash -ex

VERSION_VB=$1
VERSION_TMP=version.tmp

SVN_REVISION=`svn info . | grep Revision | sed 's/Revision: //'`
GIT_REVISION=`git log --no-color --first-parent --pretty=format:%b|grep -m1 git-svn-id|sed -e 's,git-svn-id: \(.*\)@\(.*\) .*,URL: \1 Revision: \2,'|awk -F" " '{print $4}'`

cat ../License\ FileHeader.txt > $VERSION_TMP
echo "" >> $VERSION_TMP
echo -n "<Assembly: AssemblyInformationalVersion (\"" >> $VERSION_TMP
echo -n `more ../../configure | grep VERSION | grep -v echo | grep -v sed | sed 's/VERSION=//'` >> $VERSION_TMP
echo -n " - " >> $VERSION_TMP
if [[ "x$SVN_REVISION" != "x" ]]; then
	echo -n r$SVN_REVISION  >> $VERSION_TMP
elif [[ "x$GIT_REVISION" != "x" ]]; then
	echo -n r$GIT_REVISION >> $VERSION_TMP
else
	echo -n tarball >> $VERSION_TMP
fi
echo "\")>" >> $VERSION_TMP

if ! diff $VERSION_TMP $VERSION_VB ; then
	cp $VERSION_TMP $VERSION_VB
fi

