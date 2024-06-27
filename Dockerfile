#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0-preview AS build
ARG TARGETARCH
WORKDIR /src
COPY ["SimpleBankAPI.csproj", "."]
RUN dotnet restore "./SimpleBankAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "SimpleBankAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SimpleBankAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false -a $TARGETARCH

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimpleBankAPI.dll"]