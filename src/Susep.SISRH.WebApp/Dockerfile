FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build

RUN apt-get update && apt-get install -y nodejs npm --no-install-recommends && npm i npm@latest -g

WORKDIR /src
COPY ["src/Susep.SISRH.WebApp/", "Susep.SISRH.WebApp/"]
RUN dotnet restore "Susep.SISRH.WebApp/Susep.SISRH.WebApp.csproj"
WORKDIR "/src/Susep.SISRH.WebApp"
RUN dotnet build "Susep.SISRH.WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Susep.SISRH.WebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Susep.SISRH.WebApp.dll"]
