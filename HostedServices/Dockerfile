FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["HostedServices/HostedServices.csproj", "HostedServices/"]
RUN dotnet restore "HostedServices/HostedServices.csproj"
COPY . .
WORKDIR "/src/HostedServices"
RUN dotnet build "HostedServices.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HostedServices.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HostedServices.dll"]
