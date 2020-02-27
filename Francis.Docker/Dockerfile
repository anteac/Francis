FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS sdk
WORKDIR src/

COPY . .

RUN apk add yarn

WORKDIR Francis/
RUN rm Reinforced.Typings.settings.xml
RUN dotnet publish -c Release -o /publish --runtime linux-x64

FROM lsiobase/ubuntu:bionic AS runtime
WORKDIR app/

COPY --from=sdk /publish .
COPY --from=sdk /src/Francis.Docker/lsiobase /

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true
ENV DOTNET_RUNNING_IN_CONTAINER=true

VOLUME /config
EXPOSE 4703
