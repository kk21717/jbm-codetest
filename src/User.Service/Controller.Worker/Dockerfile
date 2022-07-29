#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Controller.Worker/Controller.Worker.csproj", "Controller.Worker/"]
RUN dotnet restore "Controller.Worker/Controller.Worker.csproj"
COPY . .
WORKDIR "/src/Controller.Worker"
RUN dotnet build "Controller.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Controller.Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Controller.Http.Rest.dll"]