@echo off
setlocal EnableDelayedExpansion

REM 切換到專案根目錄
cd /d D:\WebLearn\Bank\Bank.Server

REM 使用 PowerShell 取得時間（格式：yyyyMMddHHmm）
for /f %%i in ('powershell -NoProfile -Command "Get-Date -Format yyyyMMddHHmm"') do set "timestamp=%%i"

REM 設定 ASP.NET Core 環境
call %~dp0env.bat

REM 組合 Migration 名稱與資料夾路徑
set "migrationName=AddMigration_%timestamp%"
set "migrationFolder=Migrations\%migrationName%"

REM 建立資料夾（如果不存在）
if not exist "%migrationFolder%" (
    mkdir "%migrationFolder%"
)

REM 執行 EF Core Migration 並指定輸出資料夾為 Migrations 根目錄
dotnet ef migrations add %migrationName% --output-dir "Migrations"

REM 搬移 migration 主檔與 designer 檔案到子資料夾（根據檔名包含 migrationName）
for %%f in (Migrations\*%migrationName%*.cs) do (
    if /I not "%%~nxf"=="BankContextModelSnapshot.cs" (
        move "%%f" "%migrationFolder%\"
    )
)

endlocal