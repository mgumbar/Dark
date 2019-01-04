FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

COPY .. ./

WORKDIR /app/Common/DAL/
RUN ["dotnet", "restore"]

WORKDIR /app/Dark/
RUN ["dotnet", "restore"]

# Copy everything else and build
COPY . /app
WORKDIR /app
RUN dotnet publish -c Release -o /app/out

# Build runtime image
FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Dark.dll"]
