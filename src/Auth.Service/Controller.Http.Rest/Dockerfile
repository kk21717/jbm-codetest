#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Controller.Http.Rest/Controller.Http.Rest.csproj", "Controller.Http.Rest/"]
RUN dotnet restore "Controller.Http.Rest/Controller.Http.Rest.csproj"
COPY . .
WORKDIR "/src/Controller.Http.Rest"
RUN dotnet build "Controller.Http.Rest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Controller.Http.Rest.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Controller.Http.Rest.dll"]