# Francis

Francis is a smart(ish) little bot that will help you and your [Ombi](https://github.com/tidusjar/Ombi) users to request media directly from Telegram!

Development is still underway: keep in mind that everything *should* work and that the feature set is far from complete!

Any bug report or contribution is very welcome!

## Features

### User

* Authenticate using Plex's OAuth2 (Plex and Ombi usernames must match)
* Request a movie or TV Show (choosing a specific season is possible)
* Get a notification when a request is approved/denied/available
* Get remaining available storage
* ... More to come!

### Administrator

* Accept or deny requests directly from the chat
* Web interface allowing you to
  * Easily configure the bot
  * See the request history
  * Dig into the application logs

## Used technologies

* Back-end written in [.NET Core](https://docs.microsoft.com/en-us/aspnet/core/)
* Front-end developed using [Angular](https://angular.io/docs/) and [Angular Material](https://material.angular.io/)
* Dockerized using [lsiobase/ubuntu](https://hub.docker.com/r/lsiobase/ubuntu)

## Usage

As of now, the easiest way to deploy and use Francis is through [this docker image](https://hub.docker.com/r/namaneo/francis).
For the most aventurous (or those who already have the required development tools installed), head to the `Contributing` section!

Once Francis is up and running, open your browser and hit http://localhost:4703/ to fill in the configuration.
For those deploying on a headless server, the WebUI listens on `http://*:4703`, so you'll be able to access it from your hostname or IP address.
Configuration and logs are stored in `/config` inside the container.


### docker

```
docker create \
  --name=francis \
  -e PUID=1000 \
  -e PGID=1000 \
  -e TZ=Europe/London \
  -p 4703:4703 \
  -v <host config path>:/config \
  --restart unless-stopped \
  namaneo/francis
```


### docker-compose

```
---
version: "2"
services:
  francis:
    image: namaneo/francis
    container_name: francis
    environment:
      - PUID=1000
      - PGID=1000
      - TZ=Europe/London
    volumes:
      - <host config path>:/config
    ports:
      - 4703:4703
    restart: unless-stopped
```

The last step is to fill the Telegram and Ombi settings. Hit `Save & Test`, and if each result is green, Francis is ready to answer your messages!

## Contributing

You will need to install some software:

* Visual Studio 2019 or VSCode, with ASP.NET Core support
* Node.js and Yarn, to build the UI

Once you have installed those, fork this repository, clone it, and open the solution. You should be good to go!

## Credits

Big thanks to: 
* The creator of [Ombi](https://github.com/tidusjar/Ombi) for developing this awseome tool
* The guys from [TelegramBots](https://github.com/TelegramBots) for their Telegram [.NET wrapper](https://github.com/TelegramBots/Telegram.Bot)
* The guys from [linuxserver](https://github.com/linuxserver) for creating really handy docker images

## License

Under [GPLv3 license](https://github.com/Namaneo/Francis/blob/master/LICENSE.md).
