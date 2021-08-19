#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Susep.SISRH.Domain/", "Susep.SISRH.Domain/"]
COPY ["src/Susep.SISRH.Infrastructure/", "Susep.SISRH.Infrastructure/"]
COPY ["src/Susep.SISRH.Application/", "Susep.SISRH.Application/"]
COPY ["src/Susep.SISRH.WebApi/", "Susep.SISRH.WebApi/"]
COPY ["src/Susep.Libs/", "Susep.Libs/"]
RUN dotnet restore "Susep.SISRH.WebApi/Susep.SISRH.WebApi.csproj"
WORKDIR "/src/Susep.SISRH.WebApi"
RUN dotnet build "Susep.SISRH.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Susep.SISRH.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Susep.SISRH.WebApi.dll"]
