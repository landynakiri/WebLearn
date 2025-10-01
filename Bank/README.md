# Bank 系統專案

## 專案簡介
本專案為一個銀行管理系統，採用 .NET 8 與 React 技術，前後端分離、容器化部署。

## 技術架構
- **後端**：ASP.NET Core (.NET 8)
- **前端**：React + TypeScript
- **資料庫**：Entity Framework Core
- **容器化**：Docker / Docker Compose
- **測試**：NUnit 單元測試

## 主要功能
- 使用者註冊、登入、權限管理
- RESTful API 設計
- 前後端分離架構
- 單元測試覆蓋服務層
- Docker 容器化部署
- API 文件與型別自動產生  
  前端 `generated` 目錄下自動產生 API 型別與呼叫介面，提升前後端協作效率。

## 工具腳本整合（Tools 資料夾）
本專案於 `Tools` 資料夾下提供三個主要批次腳本（.bat），協助開發者快速執行常用任務：

- **OpenAPIGenerator.bat**  
  自動根據後端 API 規格產生前端 `generated` 目錄下的 TypeScript 型別與 API 呼叫介面，確保前後端資料結構一致，提升協作效率。

- **DockerfilePublish.bat**  
  一鍵建置並發佈後端 ASP.NET Core 專案的 Docker 映像檔，簡化容器化流程，方便部署至各種環境。

- **DockerComposePublish.bat**  
  透過 Docker Compose 自動化多服務（前後端、資料庫等）容器的建置與啟動，確保本地或伺服器端環境一致性。
