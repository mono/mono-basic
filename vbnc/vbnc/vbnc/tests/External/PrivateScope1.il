.assembly extern mscorlib
{
    .ver 2:0:0:0
    .publickeytoken = (B7 7A 5C 56 19 34 E0 89)
}

.assembly PrivateScope1
{
  .hash algorithm 0x00008004
  .ver 0:0:0:0
}

.module PrivateScope1.dll
.imagebase 0x00400000
.file alignment 0x00000200
.stackreserve 0x00100000
.subsystem 0x0003
.corflags 0x00000001

.class public auto ansi beforefieldinit PrivateScope1_External extends [mscorlib]System.Object
{
  .method public hidebysig specialname rtspecialname instance void  .ctor() cil managed
  {
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  call       instance void [mscorlib]System.Object::.ctor()
    IL_0006:  ret
  }

  .method privatescope instance void P() cil managed
  {
    .maxstack  8
    IL_0001:  ret
  }
}
