cd .\Microservices\random_number_service
start cmd /k cargo run
cd ..\lucky_user_service
start cmd /k cargo run
cd ..\recmended_movie_service
start cmd /k cargo run

start microsoft-edge:http://127.0.0.1:8000/random_number/1/100
start microsoft-edge:http://127.0.0.1:8001/recommend_movie
start microsoft-edge:http://127.0.0.1:8002/lucky_user
