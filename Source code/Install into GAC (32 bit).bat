@echo off
set PATH=%ProgramFiles%\Microsoft SDKs\Windows\v7.0A\Bin\NETFX 4.0 Tools
chdir /d %~dp0
cd ..\
@echo on
gacutil /i "%ProgramFiles%\Microsoft SDKs\Expression\Blend\.NETFramework\v4.0\Libraries\Microsoft.Expression.Drawing.dll"
gacutil /i "%ProgramFiles%\Microsoft SDKs\Expression\Blend\.NETFramework\v4.0\Libraries\Microsoft.Expression.Interactions.dll"
gacutil /i "%ProgramFiles%\Microsoft SDKs\Expression\Blend\.NETFramework\v4.0\Libraries\System.Windows.Interactivity.dll"
gacutil /i "%CD%\Tools and Resources\Assembly dependencies\Microsoft.Windows.Shell.dll"
gacutil /i "%CD%\Tools and Resources\Assembly dependencies\PipelineHints.dll"
pause