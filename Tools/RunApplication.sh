docker-compose -f docker-compose.yml down
docker-compose -f docker-compose.yml up -d sql-server-db
docker-compose -f docker-compose.yml up -d rabbitmq
sleep 8
cd bash/ || exit
sh migration.sh
cd .. || exit
cd .. || exit
cd Src/Bootstrapper/BarberShop.Bootstrapper/ || exit
dotnet run 