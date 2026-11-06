FROM mcr.microsoft.com/dotnet/sdk:10.0.403 AS build
WORKDIR /src 
COPY . .
RUN dotnet publish "src/Web/BunkersAndBluffs.Gateway/BunkersAndBluffs.Gateway.csproj" -c Release -o /app/gateway
RUN dotnet publish "src/Web/BunkersAndBluffs.Client/BunkersAndBluffs.Client.csproj" -c Release -o /app/client

FROM mcr.microsoft.com/dotnet/aspnet:10.0.14 AS runtime
WORKDIR /app 
COPY --from=build /app/gateway .
COPY --from=build /app/client/wwwroot /app/wwwroot
RUN apt-get update && apt-get install curl -y
ENTRYPOINT ["dotnet", "BunkersAndBluffs.Gateway.dll"]