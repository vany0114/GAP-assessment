FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY src/Application/Gap.Insurance.API/Gap.Insurance.API.csproj src/Application/Gap.Insurance.API/
RUN dotnet restore src/Application/Gap.Insurance.API/Gap.Insurance.API.csproj
COPY . .
WORKDIR /src/src/Application/Gap.Insurance.API
RUN dotnet build Gap.Insurance.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Gap.Insurance.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Gap.Insurance.API.dll"]
