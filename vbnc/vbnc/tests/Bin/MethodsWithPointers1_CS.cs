using System.Collections.Generic;

public class MethodsWithPointers1_CS
{
	public unsafe void A (int* i) {}
	public unsafe void A (int i) {}
	public unsafe void A (ref int* i) {}
	
	public unsafe void B (int*[] ii) {}
	public unsafe void B (int [] ii) {}
	public unsafe void B (out int* [] i) {}
	
	public unsafe void C (int* i) {}
	
	public unsafe void D (List<int> ii) {}
}