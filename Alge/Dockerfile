FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 44305
EXPOSE 44305

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Alge/Alge.csproj Alge/
COPY Alge.Domain/Alge.Domain.csproj Alge.Domain/
RUN dotnet restore Alge/Alge.csproj
COPY . .
WORKDIR /src/Alge
RUN dotnet build Alge.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Alge.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Alge.dll"]
