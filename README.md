# Francis

Francis is a smart(ish) little bot that will help you and your Ombi users to request media directly from Telegram!

Development is still underway: keep in mind that everything *should* work, and that the feature set is far from complete!

Any bug report or contribution is very welcomed!

## Features

* Authentication using Plex OAuth2 (Plex and Ombi usernames have to match)
* Request a movie or a TV Show (with ability to select the season)
* Accept or deny a request directly from the chat
* Get notifications when a request is approved/denied/available
* Request remaining available storage on your media folder
* Web interface to let you configure the bot and see the logs (more to come)

## Technologies

* Back-end written in [.NET Core](https://docs.microsoft.com/en-us/aspnet/core/)
* Front-end developed using [Angular](https://angular.io/docs/) and [Angular Material](https://material.angular.io/)
* Dockerized using [lsiobase/ubuntu](https://hub.docker.com/r/lsiobase/ubuntu)

## Usage

As of now, the easiest way to deploy and use Francis is through [this docker image](https://hub.docker.com/r/namaneo/francis).
For the most aventurous (or those who already have the required development tools installed), head to the `Contributing` section!

Once Francis is up and running, open your browser on http://localhost:4703/ to fill the configuration.
For those who are deploying on an headless server, the WebUI listens on `http://*:4703`, so you'll be able to access it from your hostname or IP address.
Inside the container, all configurations and logs are stored in `/config`.


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
  Namaneo/francis
```


### docker-compose

```
---
version: "2"
services:
  francis:
    image: Namaneo/francis
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

The last step is to fill the Telegram and Ombi settings. Hit `Save & Test`, and if each result is green, Francis is ready to answer you messages!

## Contributing

You will need to install some softwares:

* Visual Studio 2019 or VSCode, with ASP.NET Core support
* Node.js and Yarn, in order to build the UI

Once you have installed those, fork this repository, clone it, and open the solution. You should be good to go!

## Credits

Big thanks to: 
* the creator of [Ombi](https://github.com/tidusjar/Ombi) for developing his awseome tool
* the guys from [TelegramBots](https://github.com/TelegramBots) for providing their Telegram [.NET wrapper](https://github.com/TelegramBots/Telegram.Bot)
* the guys from [linuxserver](https://github.com/linuxserver) for creating really handy docker images

## License

Under [GPLv3 license](https://github.com/Namaneo/Francis/blob/master/LICENSE.md).
