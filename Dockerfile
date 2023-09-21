FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./src/CleanArchitecture.Api/CleanArchitecture.Api.csproj", "src/CleanArchitecture.Api/"]
RUN dotnet restore "src/CleanArchitecture.Api/CleanArchitecture.Api.csproj"
COPY . .
WORKDIR /src/src/CleanArchitecture.Api
RUN dotnet build "CleanArchitecture.Api.csproj" -c Release -o /app/build

FROM base AS final
WORKDIR /app
COPY --from=build /app/build .
ENTRYPOINT ["dotnet", "CleanArchitecture.Api.dll"]