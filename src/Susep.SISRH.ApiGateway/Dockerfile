FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Susep.SISRH.ApiGateway/", "Susep.SISRH.ApiGateway/"]
RUN dotnet restore "Susep.SISRH.ApiGateway/Susep.SISRH.ApiGateway.csproj"
WORKDIR "/src/Susep.SISRH.ApiGateway"
RUN dotnet build "Susep.SISRH.ApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Susep.SISRH.ApiGateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Susep.SISRH.ApiGateway.dll"]
