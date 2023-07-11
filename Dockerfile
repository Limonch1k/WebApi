# https://hub.docker.com/_/microsoft-dotnet
#FROM mcr.microsoft.com/dotnet/nightly/sdk:8.0-preview as build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source

ENV http_proxy=http://user:12345@192.168.211.209:8080
ENV https_proxy=http://user:12345@192.168.211.209:8080

#RUN apt-get update && apt-get -y install libpq-dev gcc g++ unixodbc-dev libaio-dev wget unzip curl gnupg2 default-jdk



# copy csproj and restore as distinct layers
COPY PresentationLayer/*.csproj PresentationLayer/
COPY BusinessLayer/*.csproj BusinessLayer/
COPY DatabaseLayer/*.csproj DatabaseLayer/
COPY GeneralObject/*.csproj GeneralObject/



RUN dotnet restore "PresentationLayer/api_fact_weather_by_city.csproj"

# copy everything else and build app
COPY PresentationLayer/ PresentationLayer/
COPY BusinessLayer/ BusinessLayer/
COPY DatabaseLayer/ DatabaseLayer/
COPY GeneralObject/ GeneralObject/


WORKDIR /source/PresentationLayer
#RUN dotnet build 
RUN dotnet publish -c release -o /app  /restore





# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 5000



WORKDIR /app
COPY --from=build /app .

RUN apt-get update
#RUN apt-get install  -y unzip wget
#RUN wget https://download.oracle.com/otn_software/linux/instantclient/195000/instantclient-basic-linux.x64-19.5.0.0.0dbru.zip

#RUN unzip instantclient-basic-linux.x64-19.5.0.0.0dbru.zip
#RUN mkdir -p /opt/oracle
#RUN mv instantclient_19_5 /opt/oracle

#ENV LD_LIBRARY_PATH /opt/oracle/instantclient_19_5:$LD_LIBRARY_PATH
ENV ASPNETCORE_URLS="http://*:5000"
ENV TZ=Etc/GMT+3
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone
ENTRYPOINT ["dotnet", "api_fact_weather_by_city.dll"]
