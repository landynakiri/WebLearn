@echo off
REM 批次檔：DockerBuild.bat
REM 用途：建置 Docker Compose 專案

REM 切換到 Docker Compose 專案所在的目錄
cd /d D:\WebLearn\Bank

REM 執行 docker compose build，建置所有在 docker-compose.yml 中定義的服務
docker compose build
