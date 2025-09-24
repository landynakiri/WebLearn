cd ../../bank.server
dotnet build -c Debug
swagger tofile --output ./swagger/v1/swagger.json ./bin/Debug/net8.0/Bank.Server.dll v1