cd ../../bank.client
npx @openapitools/openapi-generator-cli generate -i ../bank.server/swagger/v1/swagger.json -g typescript-fetch -o ./src/generated