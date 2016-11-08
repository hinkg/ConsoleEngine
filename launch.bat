
REM This batch file will launch the game as normal.

REM The reason this exists is because some computers/installations of Visual
REM Studio Code cannot use the .NET Core Debugger, such as 32-bit systems or no
REM access to the PATH variable.

REM In order to use this, replace DOTNET-DIRECTORY (keep the quotes) below with
REM the absolute path of the dotnet.exe executable, which lies at the root of
REM your .NET Core installation folder.

REM You can run this file by pressing Ctrl+Shift+C in Visual Studio Code, and
REM executing "launch" in the console window.

@echo off
cls
"DOTNET-DIRECTORY" run %~dp0src
