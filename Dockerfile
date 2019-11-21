FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS sdk
WORKDIR src/
COPY . .

RUN curl -sS https://dl.yarnpkg.com/debian/pubkey.gpg | apt-key add -
RUN echo "deb https://dl.yarnpkg.com/debian/ stable main" | tee /etc/apt/sources.list.d/yarn.list
RUN apt-get update && apt-get install -y yarn

RUN rm Francis/Reinforced.Typings.settings.xml

WORKDIR Francis/
RUN dotnet restore
RUN dotnet build -c Release --no-restore
RUN dotnet publish -c Release -o /publish --no-restore --no-build

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR app/
COPY --from=sdk /publish .
RUN mkdir /config

EXPOSE 4703
ENTRYPOINT dotnet Francis.dll
