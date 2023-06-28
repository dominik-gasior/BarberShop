FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "Src/Bootstrapper/BarberShop.Bootstrapper/BarberShop.Bootstrapper.csproj" --disable-parallel
RUN dotnet publish "Src/Bootstrapper/BarberShop.Bootstrapper/BarberShop.Bootstrapper.csproj" -c release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0 as final

WORKDIR /app
EXPOSE 80
COPY --from=build /publish ./

ENTRYPOINT [ "dotnet", "BarberShop.Bootstrapper.dll" ]
