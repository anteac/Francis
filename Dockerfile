FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS sdk
WORKDIR src/
COPY . .

RUN apk add yarn
RUN rm Francis/Reinforced.Typings.settings.xml

WORKDIR Francis/
RUN dotnet restore
RUN dotnet build -c Release --no-restore
RUN dotnet publish -c Release -o /publish --no-restore --no-build

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS runtime
WORKDIR app/
COPY --from=sdk /publish .
RUN mkdir /config

EXPOSE 4703
ENTRYPOINT dotnet Francis.dll
