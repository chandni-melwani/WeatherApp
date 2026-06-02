FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy everything from the repository root into the container
COPY . .

# Point explicitly through the middle WeatherApp folder to find the projects
RUN dotnet restore "WeatherApp/WeatherApp/WeatherApp.csproj"
RUN dotnet publish "WeatherApp/WeatherApp/WeatherApp.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "WeatherApp.dll"]