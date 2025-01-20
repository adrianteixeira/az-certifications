# Comando para criar um novo projeto ASP.NET Core Web API com .NET 8
dotnet new webapi -n MyWebApiProject -f net8.0

# Navegar para o diretório do projeto
Set-Location MyWebApiProject

# Comando para criar manualmente o arquivo Dockerfile
@"
# Use the official .NET SDK image as a build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the project files and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the remaining files and build the application
COPY . ./
RUN dotnet publish -c Release -o out

# Use the official ASP.NET Core runtime image as a runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Expose the port and run the application
EXPOSE 80
ENTRYPOINT ["dotnet", "MyWebApiProject.dll"]
"@ | Out-File -FilePath Dockerfile -Encoding utf8

# Comando para restaurar as dependências do projeto
dotnet restore

# Comando para construir o projeto
dotnet build

# Comando para rodar o projeto localmente
dotnet run

# Comando para construir a imagem Docker
docker build -t mywebapiproject:latest .

# Comando para rodar o container Docker
docker run -d -p 8080:80 --name mywebapiprojectcontainer mywebapiproject:latest

# Comando para listar os containers Docker em execução
docker ps