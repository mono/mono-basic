rem echo off
cls
"%windir%\Microsoft.NET\Framework\v2.0.50727\vbc.exe" /nowarn /target:exe /out:fullcompilation.exe *.vb /optionstrict+ /optionexplicit+ /imports:vb=microsoft.visualbasic
echo off
IF ERRORLEVEL 1 (
echo                            COMPILATION FAILED
echo                            COMPILATION FAILED
echo                            COMPILATION FAILED
echo                            COMPILATION FAILED
echo                            COMPILATION FAILED
echo                            COMPILATION FAILED
echo                            COMPILATION FAILED
pause
) ELSE (
echo                                 SUCCESS
echo                                 SUCCESS
echo                                 SUCCESS
echo                                 SUCCESS
echo                                 SUCCESS
echo                                 SUCCESS
echo                                 SUCCESS
pause
del fullcompilation.exe
)