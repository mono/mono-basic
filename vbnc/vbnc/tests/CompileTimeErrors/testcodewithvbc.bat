rem echo off
cls
"%windir%\Microsoft.NET\Framework\v2.0.50727\vbc.exe" /nowarn /target:exe /out:fullcompilation.exe *.vb
echo off
IF ERRORLEVEL 1 (
echo                            COMPILATION FAILED
echo                            COMPILATION FAILED
echo                            COMPILATION FAILED
echo                            COMPILATION FAILED
echo                            COMPILATION FAILED
echo                            COMPILATION FAILED
echo                            COMPILATION FAILED
) ELSE (
del fullcompilation.exe
echo                                 SUCCESS
echo                                 SUCCESS
echo                                 SUCCESS
echo                                 SUCCESS
echo                                 SUCCESS
echo                                 SUCCESS
echo                                 SUCCESS
)
pause