The structure of the test system is the following:

Compiler tests:
-  Should be placed in the CompileTime\ directory - these are tests that can be verified with compiler output (compile time errors, warnings, or successful compilation tests).

Runtime tests:
- Should be placed in the RunTime\ directory - these are tests that requires the execution of the compiled program to be verified.


Single file tests:
- just put a test file in the corresponding folder.
- the name of the test is the filename without its extension.

Multiple file tests (multiple code files):
- just put a subfolder in the corresponding folder where all the tests files will go.
- the name of the test is the name subfolder.


Every test will output one or more xml files with it's "name.(testtype).output.verified.xml". Current test types are:
- tokens (All the tokens after scanning the file. Should always exist, even though no tokens were found.)
- tokensAfterConditionalParsing (Needs explanation? Should always exist, even though no tokens are left after conditional compilation.)
- typetree (The parsed type tree. Exists if the parsing step was successful.)
- errors (Compile time errors and warnings. Inexistent if successful compile with no errors nor warnings.)
- exceptions (If any unhandled exception occurs this file will be created. If this file exist, the compiler has regressed.)
More test types will be added in the future. Note that a compile that is supposed to fail can be tested with this structure.

The default command line is the file(s) + a default response file for all test in a directory called all.rsp.
A test can specify a response file that will replace this commandline by using the format name.rsp (then this is the only argument that will be sent to the compiler, this response file should include the filename(s) as well). A test can also specify a response file that will be appended to the commandline by using the format name.response (this will be the last argument on the commandline).
You may specify other source files not included in the test directory, as well as any other compiler argument needed. (Often additional references, such as System.dll)
In any case the option "/out:" is specified by rt, but if will be the first option on the command line, to override it just specify it in any response file.

For every xml file that a test outputs, there should be a corresponding file named "name.(testtype).verified.xml". The outputted xml files will be compared to the verified output, and if any differences are detected, the test is marked as failed. The test is also marked as failed if a verified file exists and the compile doesn't produce it, or a compile produces a file with no corresponding verified file. TODO: currently the files are compared in binary mode, but xml files can be identical even though the exact binary content doesn't match - such a compare should be implemented.


Naming of the test files:
- Tests supposed to fail compile should start with "VBNC_" + the error code + a number indicating this error's test sequence number. For instance: the first test file for the error VBNC30XXXX should be named VBNC30XXXX_1.vb, the second VBNC30XXXX_2.vb, etc. Since warnings also has "error" numbers, they are included here.
- Tests supposed to succeed compile can be named in any way (shouldn't start with VBNC though...), though meaningful names is appreciated. For instance: testing an interface declaration, call the file Interface1.vb
