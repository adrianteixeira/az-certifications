# Comando para fazer login no Azure
az login

# Comando para iniciar um novo projeto de funções do Azure
func init MyFunctionProj --worker-runtime dotnet

# Comando para criar uma nova função HTTP
func new --name validateCpf --template "HTTP trigger"

# Comando para instalar a biblioteca System.Text.Json
dotnet add package System.Text.Json

# Comando para iniciar a função do Azure localmente
func start

# Comando para publicar a função do Azure
FUNCTION_APP_NAME=<YourFunctionAppName>
func azure functionapp publish $FUNCTION_APP_NAME
