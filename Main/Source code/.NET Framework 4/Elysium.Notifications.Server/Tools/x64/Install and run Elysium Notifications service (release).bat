@echo off
set OriginalPATH=%PATH%
set PATH=%OriginalPATH%;%windir%\Microsoft.NET\Framework64\v4.0.30319
chdir /d %~dp0
cd ..\..\..\..\
@echo on
installutil "%CD%\Binary\.NET Framework 4\Release\x64\Elysium.Notifications.Server.exe"
net start ElysiumNotifications-v2.0.35.0-v4.0
pause