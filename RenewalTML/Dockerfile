#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RenewalTML/RenewalTML.csproj", "RenewalTML/"]
COPY ["RenewalTML.EFCore/RenewalTML.EFCore.csproj", "RenewalTML.EFCore/"]
COPY ["RenewalTML.Data/RenewalTML.Data.csproj", "RenewalTML.Data/"]
RUN dotnet restore "RenewalTML/RenewalTML.csproj"
COPY . .
WORKDIR "/src/RenewalTML"
RUN dotnet build "RenewalTML.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RenewalTML.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RenewalTML.dll"]