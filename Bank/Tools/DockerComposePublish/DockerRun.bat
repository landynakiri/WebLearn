@echo off
REM 批次檔：DockerRun.bat
REM 用途：啟動 Docker Compose 所定義的所有服務

REM 切換到 Docker Compose 專案所在的目錄
cd /d D:\WebLearn\Bank

REM 執行 docker compose up，啟動所有在 docker-compose.yml 中定義的容器服務
docker compose up