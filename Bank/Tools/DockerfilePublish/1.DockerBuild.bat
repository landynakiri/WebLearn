@echo off
echo ==============================
echo Building Docker image: bank
echo ==============================

REM 切換到 Bank 專案的目錄
cd /d D:\WebLearn\Bank

REM 執行 docker build，指定 Dockerfile
docker build -t bank -f .\Bank.Server\Dockerfile .

pause
