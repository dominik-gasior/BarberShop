moduless=("SystemReservation" "Users" "Warehouse")
for moduleName in "${moduless[@]}" ; do
/usr/local/share/dotnet/dotnet ef migrations add 
# shellcheck disable=SC2215
--project Src/Modules/"$moduleName"/BarberShop.Modules."$moduleName".Api/BarberShop.Modules."$moduleName".Api.csproj 
# shellcheck disable=SC2215
--startup-project Src/Bootstrapper/BarberShop.Bootstrapper/BarberShop.Bootstrapper.csproj 
# shellcheck disable=SC2215
--context BarberShop.Modules."$moduleName".Api.Persistence."$moduleName"DbContext 
# shellcheck disable=SC2215
--configuration Debug Initial --output-dir Persistence/Migrations

/usr/local/share/dotnet/dotnet ef database update 
# shellcheck disable=SC2215
--project Src/Modules/"$moduleName"/BarberShop.Modules."$moduleName".Api/BarberShop.Modules."$moduleName".Api.csproj 
# shellcheck disable=SC2215
--startup-project Src/Bootstrapper/BarberShop.Bootstrapper/BarberShop.Bootstrapper.csproj 
# shellcheck disable=SC2215
--context BarberShop.Modules."$moduleName".Api.Persistence."$moduleName"DbContext --configuration Debug Initial
done