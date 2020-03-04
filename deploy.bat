docker build -t legendary-weapons-store .

docker tag legendary-weapons-store registry.heroku.com/legendary-weapons-store/web

docker push registry.heroku.com/legendary-weapons-store/web

heroku container:release web -a legendary-weapons-store