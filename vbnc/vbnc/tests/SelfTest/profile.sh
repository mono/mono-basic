#!/bin/bash -ex

make -C ../../../
cp ../../../../class/lib/net_4_0/vbnc.* .
cp ../../../../class/lib/net_4_0/Mono.Cecil* .

# make sure we're using an updated binary
rerun.sh && rerun.sh

rm -f output.mlpd
mono --profile=log:nocalls,zip vbnc.exe @SelfCompileLinux.response -out:vbnc.profile.exe
I=1
while test -e profile$I.log; do
	let I=I+1
done
mprof-report --reports=header,gc,alloc,exception --maxframes=8 --alloc-sort=count --traces output.mlpd > profile$I.log
biiip

