@echo off
REM ********************************************************
REM This batch file receives the follwing parameters:
REM build/rebuild (optional): should the solution file be rebuilded 
REM                             or just builded before test run (default is rebuild)
REM example run-tests build 
REM will cause to build (and not rebuild) test solutions,
REM ********************************************************

call run-tests.vb.bat rebuild
call run-tests.cs.bat rebuild
