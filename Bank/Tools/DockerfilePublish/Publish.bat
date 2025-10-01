@echo off
setlocal

REM 專案目錄
set PROJECT_DIR=D:\WebLearn\Tools

call "%PROJECT_DIR%\DockerBuild.bat"
call "%PROJECT_DIR%\DockerRun.bat"
