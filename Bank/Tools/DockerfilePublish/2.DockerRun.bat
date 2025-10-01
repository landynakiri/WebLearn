@echo off
REM 切換到專案根目錄
cd /d D:\WebLearn\Bank

REM 執行容器，預設使用 Production 環境
docker run -it --rm -p 5000:8080 ^
    --name bank_publish ^
    -e ASPNETCORE_ENVIRONMENT=Production ^
    bank
