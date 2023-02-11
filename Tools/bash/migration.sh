
moduless=("SystemReservation" "Users" "Warehouse")
for moduleName in "${moduless[@]}" ; do
/usr/local/share/dotnet/dotnet ef migrations add --project Src/Modules/"$moduleName"/BarberShop.Modules."$moduleName".Api/BarberShop.Modules."$moduleName".Api.csproj --startup-project Src/Bootstrapper/BarberShop.Bootstrapper/BarberShop.Bootstrapper.csproj --context BarberShop.Modules."$moduleName".Api.Persistence."$moduleName"DbContext --configuration Debug Initial --output-dir Persistence/Migrations
#/usr/local/share/dotnet/dotnet ef migrations add --project Src/Modules/"$moduleName"/BarberShop.Modules."$moduleName".Api/BarberShop.Modules."$moduleName".Api.csproj --startup-project Src/Bootstrapper/BarberShop.Bootstrapper/BarberShop.Bootstrapper.csproj --context BarberShop.Modules."$moduleName".Api.Persistence."$moduleName"DbContext --configuration Debug Initial --output-dir Persistence/Migrations
#/usr/local/share/dotnet/dotnet ef migrations add --project Src/Modules/"$moduleName"/BarberShop.Modules."$moduleName".Api/BarberShop.Modules."$moduleName".Api.csproj --startup-project Src/Bootstrapper/BarberShop.Bootstrapper/BarberShop.Bootstrapper.csproj --context BarberShop.Modules."$moduleName".Api.Persistence."$moduleName"DbContext --configuration Debug Initial --output-dir Persistence/Migrations

/usr/local/share/dotnet/dotnet ef database update --project Src/Modules/"$moduleName"/BarberShop.Modules."$moduleName".Api/BarberShop.Modules."$moduleName".Api.csproj --startup-project Src/Bootstrapper/BarberShop.Bootstrapper/BarberShop.Bootstrapper.csproj --context BarberShop.Modules."$moduleName".Api.Persistence."$moduleName"DbContext --configuration Debug Initial
#/usr/local/share/dotnet/dotnet ef database update --project Src/Modules/Users/BarberShop.Modules.Users.Api/BarberShop.Modules.Users.Api.csproj --startup-project Src/Bootstrapper/BarberShop.Bootstrapper/BarberShop.Bootstrapper.csproj --context BarberShop.Modules.Users.Api.Persistence.UsersDbContext --configuration Debug Initial
#/usr/local/share/dotnet/dotnet ef database update --project Src/Modules/Warehouse/BarberShop.Modules.Warehouse.Api/BarberShop.Modules.Warehouse.Api.csproj --startup-project Src/Bootstrapper/BarberShop.Bootstrapper/BarberShop.Bootstrapper.csproj --context BarberShop.Modules.Warehouse.Api.Persistence.WarehouseDbContext --configuration Debug Initial
done