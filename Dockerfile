FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy the entire solution and all project folders into the container
COPY . .

# Restore and publish pointing specifically to the subfolder path
RUN dotnet restore "WeatherApp/WeatherApp.csproj"
RUN dotnet publish "WeatherApp/WeatherApp.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "WeatherApp.dll"]