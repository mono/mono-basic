#!/bin/bash -ex

make -C ../../../
cp ../../../../class/lib/vbnc/vbnc.* .
cp ../../../../class/lib/vbnc/Mono.Cecil* .

# make sure we're using an updated binary
rerun.sh && rerun.sh

rm output.mlpd
mono --profile=log:nocalls,zip vbnc.exe @SelfCompileLinux.response -out:vbnc.profile.exe
I=1
while test -e profile$I.log; do
	let I=I+1
done
mprof-report --reports=header,gc,alloc,exception --maxframes=8 --alloc-sort=count --traces output.mlpd > profile$I.log
biiip

