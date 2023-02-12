cd .. || exit 
cd .. || exit 
rm -r Src/Modules/SystemReservation/BarberShop.Modules.SystemReservation.Api/Persistence/Migrations
rm -r Src/Modules/Users/BarberShop.Modules.Users.Api/Persistence/Migrations
rm -r Src/Modules/Warehouse/BarberShop.Modules.Warehouse.Api/Persistence/Migrations


moduless=("SystemReservation" "Users" "Warehouse")
for moduleName in "${moduless[@]}" ; do
/usr/local/share/dotnet/dotnet ef migrations add --project Src/Modules/"$moduleName"/BarberShop.Modules."$moduleName".Api/BarberShop.Modules."$moduleName".Api.csproj --startup-project Src/Bootstrapper/BarberShop.Bootstrapper/BarberShop.Bootstrapper.csproj --context BarberShop.Modules."$moduleName".Api.Persistence."$moduleName"DbContext --configuration Debug Initial --output-dir Persistence/Migrations
/usr/local/share/dotnet/dotnet ef database update --project Src/Modules/"$moduleName"/BarberShop.Modules."$moduleName".Api/BarberShop.Modules."$moduleName".Api.csproj --startup-project Src/Bootstrapper/BarberShop.Bootstrapper/BarberShop.Bootstrapper.csproj --context BarberShop.Modules."$moduleName".Api.Persistence."$moduleName"DbContext --configuration Debug Initial
done