Regression Tester
Copyright (C) 2004 - 2006 Rolf Bjarne Kvinge

This is the program used to test the output of the vbnc compiler.
 See the vbnc\test readme for details about the test structure.

It is licensed under the GPL-2 license.

Requirements:
- The .Net Framework v2.0 to compile the source code. 
The framework is also needed to run any executables. 

Instructions:
1 - Build vbnc
2 - Build rt, run it and configure the paths
    (the base path should be set to the vbnc\tests directory)
3 - Have some patience, since it will now load all the tests 
    (might take about a minute if you already generated all the tests since there are over 10.000 test in total)

Right click a test and you can:
- Run it (immediately)
- View code and debug test: this will open the test file in VS (or any other default viewer for vb files) 
  and change the vbnc\bin\debug.rsp so that if you run vbnc within VS you will be debugging this test.
