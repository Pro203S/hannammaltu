@echo off
setlocal

if not exist debug mkdir debug

set PROJECT=.\Hannammaltu\Hannammaltu.csproj
set CONFIG=Debug
set FRAMEWORK=net10.0
set BUILD_DIR=.\Hannammaltu\bin\%CONFIG%\%FRAMEWORK%

echo ========================================
echo Building Hannammaltu (%CONFIG%)
echo ========================================

dotnet build "%PROJECT%" -c %CONFIG%
if errorlevel 1 (
    echo Build failed
    exit /b 1
)

echo Copying build outputs to project root...

for %%F in (
    Hannammaltu.exe
    Hannammaltu.dll
    Hannammaltu.deps.json
    Hannammaltu.runtimeconfig.json
    Hannammaltu.pdb
) do (
    if exist "%BUILD_DIR%\%%F" (
        copy /Y "%BUILD_DIR%\%%F" ".\debug\%%F" >nul
    )
)

echo ===========================

.\debug\Hannammaltu.exe %*