#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["05WebApi/MM.Suppliers.API.Web/MM.Suppliers.API.Web.csproj", "05WebApi/MM.Suppliers.API.Web/"]
COPY ["01Common/MM.Suppliers.API.Common/MM.Suppliers.API.Common.csproj", "01Common/MM.Suppliers.API.Common/"]
COPY ["02Domain/MM.Suppliers.API.Domain/MM.Suppliers.API.Domain.csproj", "02Domain/MM.Suppliers.API.Domain/"]
COPY ["03Infra/MM.Suppliers.API.AzureServices/MM.Suppliers.API.AzureServices.csproj", "03Infra/MM.Suppliers.API.AzureServices/"]
COPY ["04Services/MM.Suppliers.API.Services/MM.Suppliers.API.Services.csproj", "04Services/MM.Suppliers.API.Services/"]
COPY ["03Infra/MM.Suppliers.API.Repositories/MM.Suppliers.API.Repositories.csproj", "03Infra/MM.Suppliers.API.Repositories/"]
RUN dotnet restore "05WebApi/MM.Suppliers.API.Web/MM.Suppliers.API.Web.csproj"
COPY . .
WORKDIR "/src/05WebApi/MM.Suppliers.API.Web"
RUN dotnet build "MM.Suppliers.API.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MM.Suppliers.API.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MM.Suppliers.API.Web.dll"]