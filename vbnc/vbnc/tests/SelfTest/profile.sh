cp ../../bin/Mono.Cecil.VB.dll .
MONO_COUNT=0 mono --profile=logging:a,ts,o=vbnc.profile.log vbnc.exe @SelfCompileLinux.response -out:vbnc.profile.exe
mprof-decoder vbnc.profile.log > vbnc.profile.decoded

