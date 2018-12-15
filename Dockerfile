FROM microsoft/dotnet:sdk AS build-env

COPY . /app

WORKDIR /app/Common/DAL/
RUN ["dotnet", "restore"]

WORKDIR /app/Dark/
RUN ["dotnet", "restore"]

# Copy everything else and build
COPY . ./
FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app

# Build runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Dark.dll"]
