default: */*.cs */*/*.cs
	gmcs @Mono.Cecil.VB.dll.sources /r:System.dll /target:library /out:Mono.Cecil.VB.dll

install: default
	mv Mono.Cecil.VB.dll ../../class/vbnc
