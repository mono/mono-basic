To write tests and execute using Microsoft's Microsoft.VisualBasic.dll, use:

- 2008VB_test_MS solution
  - 2008VB_test_MS_CS C# project (tests written in C#)
  - 2008VB_test_MS_VB VB project (tests written in VB)
  - 2008VB_tester_MS  VB project to execute specific tests (instead of running with nunit)

To run tests with Mono's Microsoft.VisualBasic.dll, use:

- 2008VB_test solution
  - 2008VB_test_CS C# project (tests written in C#)
  - 2008VB_test_VB VB project (tests written in VB)
  - 2008VB_tester  VB project to execute specific tests (instead of running with nunit)
** there will be a lot of errors in the error list, but you can still execute the tests (just hit F5) **

Note that the project files may be slightly out of date (missing files)