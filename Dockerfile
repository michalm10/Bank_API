FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /App
COPY . ./
RUN dotnet restore "nbp_api/nbp_api.csproj"
RUN dotnet publish "nbp_api/nbp_api.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /App
COPY --from=build /App/out .

EXPOSE 5000

ENTRYPOINT ["dotnet", "nbp_api.dll"]