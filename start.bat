@echo off
ROCKET_PORT=3721 ./your_application
cd .\Microservices\random_number_service
start cmd /k cargo run
cd .\Microservices\lucky_user_service
start cmd /k cargo run
cd .\Microservices\recmended_movie_service
start cmd /k cargo run
echo "All services started"
