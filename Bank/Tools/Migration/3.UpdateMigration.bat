@echo off
setlocal

REM 切換到專案根目錄
cd /d D:\WebLearn\Bank\Bank.Server

REM 設定 ASP.NET Core 環境
call %~dp0env.bat

REM 更新資料庫
echo Updating database...
dotnet ef database update
endlocal