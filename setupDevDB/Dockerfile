FROM mcr.microsoft.com/mssql/server:2017-latest-ubuntu

RUN mkdir -p /usr/src/app
WORKDIR /usr/src/app

COPY ./setupDevDB /usr/src/app
COPY ./src/NewsApi.Services/Repositories/Scripts/*.sql /usr/src/app/scripts/
RUN chmod +x /usr/src/app/run-initialization.sh

CMD /bin/bash ./entrypoint.sh