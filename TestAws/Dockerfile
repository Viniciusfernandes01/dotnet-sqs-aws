#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["TestAws/TestAws.csproj", "TestAws/"]
RUN dotnet restore "TestAws/TestAws.csproj"
COPY . .
WORKDIR "/src/TestAws"
RUN dotnet build "TestAws.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestAws.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestAws.dll"]