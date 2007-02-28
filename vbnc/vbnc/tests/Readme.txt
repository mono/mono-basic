The structure of the test system is the following:

Compiler tests:
-  Should be placed in the CompileTime\ (for tests that compile as libraries) or CompileTime2\ (for tests that compile as executables) directory - these are tests that can be verified with compiler output (compile time errors, warnings, or successful compilation tests).

Runtime tests:
- Should be placed in the RunTime\ directory - these are tests that requires the execution of the compiled program to be verified.

Single file tests:
- just put a test file in the corresponding folder.
- the name of the test is the filename without its extension.

Multiple file tests (multiple code files):
- just put a subfolder in the corresponding folder where all the tests files will go.
- the name of the test is the name subfolder.

The default command line is the file(s) + a default response file for all test in a directory called all.rsp.
A test can specify a response file that will replace this commandline by using the format name.rsp (then this is the only argument that will be sent to the compiler, this response file should include the filename(s) as well). A test can also specify a response file that will be appended to the commandline by using the format name.response (this will be the last argument on the commandline).
You may specify other source files not included in the test directory, as well as any other compiler argument needed. (Often additional references, such as System.dll)
In any case the option "/out:" is specified by rt, but if will be the first option on the command line, to override it just specify it in any response file.

Naming of the test files:
- Tests supposed to fail compile should start with a number (the error number) + a descriptive name. Since warnings also has "error" numbers, they are included here.
- Tests supposed to succeed compile should NOT start with a number, though meaningful names is appreciated. For instance: testing an interface declaration, call the file Interface1.vb
Do not include spaces in the file names, as the build system will get confused.
