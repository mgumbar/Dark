FROM microsoft/dotnet:sdk AS build-env

COPY . /app

WORKDIR /app/Common/DAL/
RUN ["dotnet", "restore"]

WORKDIR /app/Common/Dark/
RUN ["dotnet", "restore"]

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Dark.dll"]
