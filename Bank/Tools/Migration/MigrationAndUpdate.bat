@echo off
setlocal

echo Running 2.CreateMigration.bat...
call "%~dp02.CreateMigration.bat"
echo.

echo Running 3.UpdateMigration.bat...
call "%~dp03.UpdateMigration.bat"
echo.

echo All tasks completed.
endlocal
