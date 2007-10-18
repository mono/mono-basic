Visual Basic.Net Compiler
Copyright (C) 2004 - 2006 Rolf Bjarne Kvinge

This is a compiler for the Visual Basic language, aimed at the specifications 
/ features of the Visual Basic 2005 compiler soon to be released. 
The source code is released under the GPL-2 license.

Requirements:
- The .Net Framework v2.0 to compile the source code. 
  The framework is also needed to run any executables.

Features:
- I'm trying to be as compatible as possible with the .Net v2.0 compiler,
  but for the moment the following features are not beeing implemented:
  * XML comments
  * My namespace
  These features are scheduled for a later timeframe however.

Why?
- There is no open-source compiler for the Visual Basic language written entirely in Visual Basic. 
  And I wanted to try to make a compiler... Don't come complaining about the source code, 
  I do this entirely on my spare time, and all I have ever read about compilers is 1 (yes, one) book 
  (partially actually), and some information found on the internet. I never thought about 
  speed / eficiency / whatever source code is supposed to be while writing the compiler, 
  since I was basically busy figuring out how to make things working... Any constructive 
  comments are of course welcome! Or better: contribute writing source code!
  Though be aware: the structure of the code is currently changing rapidly, 
  absolutely nothing has been fixed yet.

Instructions:
- Load the vbnc.sln in VS2005 and build it. You will now have the vbnc compiler in the bin directory.