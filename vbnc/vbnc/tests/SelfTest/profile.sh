#!/bin/bash -ex

make -C ../../../
cp ../../../../class/lib/vbnc/vbnc.* .
cp ../../../../class/lib/vbnc/Mono.Cecil* .
mono --profile=log:zip vbnc.exe @SelfCompileLinux.response -out:vbnc.profile.exe
I=1
while test -e profile$I.log; do
	let I=I+1
done
mprof-report --reports=header,gc,alloc,metadata,exception,monitor,thread,heapshot --traces output.mlpd > profile$I.log
biiip

