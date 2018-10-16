FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY src/Web/Gap.Insurance.Web/Gap.Insurance.Web.csproj src/Web/Gap.Insurance.Web/
RUN dotnet restore src/Web/Gap.Insurance.Web/Gap.Insurance.Web.csproj
COPY . .
WORKDIR /src/src/Web/Gap.Insurance.Web
RUN dotnet build Gap.Insurance.Web.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Gap.Insurance.Web.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Gap.Insurance.Web.dll"]
