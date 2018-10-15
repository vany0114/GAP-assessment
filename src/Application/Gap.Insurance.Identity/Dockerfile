FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY src/Application/Gap.Insurance.Identity/Gap.Insurance.Identity.csproj src/Application/Gap.Insurance.Identity/
COPY src/Infrastructure/Gap.Infrastructure.WebHost/Gap.Infrastructure.WebHost.csproj src/Infrastructure/Gap.Infrastructure.WebHost/
RUN dotnet restore src/Application/Gap.Insurance.Identity/Gap.Insurance.Identity.csproj
COPY . .
WORKDIR /src/src/Application/Gap.Insurance.Identity
RUN dotnet build Gap.Insurance.Identity.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Gap.Insurance.Identity.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Gap.Insurance.Identity.dll"]
