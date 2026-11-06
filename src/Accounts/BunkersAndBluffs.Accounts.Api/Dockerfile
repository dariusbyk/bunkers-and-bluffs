FROM mcr.microsoft.com/dotnet/sdk:10.0.403 AS build
WORKDIR /src 
COPY . .
RUN dotnet publish "src/Accounts/BunkersAndBluffs.Accounts.Api/BunkersAndBluffs.Accounts.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0.14 AS runtime
WORKDIR /app 
COPY --from=build /app/publish .
RUN apt-get update && apt-get install curl -y
ENTRYPOINT ["dotnet", "BunkersAndBluffs.Accounts.Api.dll"]