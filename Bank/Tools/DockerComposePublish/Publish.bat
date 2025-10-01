@echo off
REM 批次檔：publish.bat
REM 用途：先停止舊容器，再依序執行 DockerBuild.bat 和 DockerRun.bat，完成建置與啟動容器

REM 設定共用的專案目錄變數
set PROJECT_DIR=D:\WebLearn\Bank\Tools\DockerComposePublish

REM 停止並清除先前執行的容器，避免埠口衝突
docker compose down --remove-orphans --volumes

REM 執行建置步驟
call "%PROJECT_DIR%\DockerBuild.bat"

REM 執行啟動步驟
call "%PROJECT_DIR%\DockerRun.bat"