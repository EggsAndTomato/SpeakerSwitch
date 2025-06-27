@echo off
echo ========================================
echo       SpeakerSwitch Publishing Tool
echo ========================================
echo.
echo Please select publishing type:
echo 1. Framework-dependent (Small file, requires .NET runtime)
echo 2. Self-contained (Large file, includes runtime)
echo 3. Single file (Recommended, easy to distribute)
echo.
set /p choice=Please enter your choice (1-3): 

if "%choice%"=="1" goto framework
if "%choice%"=="2" goto standalone
if "%choice%"=="3" goto singlefile
echo Invalid choice, using single file version by default
goto singlefile

:framework
echo.
echo Publishing framework-dependent version...
dotnet publish -c Release -r win-x64 --self-contained false -o publish
echo Publishing completed! File location: publish\SpeakerSwitch.exe
echo Note: Target machine requires .NET 9.0 runtime installed
goto end

:standalone
echo.
echo Publishing self-contained version...
dotnet publish -c Release -r win-x64 --self-contained true -o publish-standalone
echo Publishing completed! File location: publish-standalone\SpeakerSwitch.exe
echo Note: Larger file size but runs on any Windows machine
goto end

:singlefile
echo.
echo Publishing single file version...
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -o publish-single
echo Publishing completed! File location: publish-single\SpeakerSwitch.exe
echo Recommended: Single exe file, easy to distribute and use
goto end

:end
echo.
echo ========================================
echo Publishing completed! You can copy the exe file to any location.
echo Suggestion: Create desktop shortcut or set hotkeys.
echo ========================================
pause