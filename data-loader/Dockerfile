FROM ubuntu:22.04

ENV ACCEPT_EULA=Y
ENV DEBIAN_FRONTEND=noninteractive

# Install MSSQL tools
RUN apt-get update && \
    apt-get install -y curl gnupg software-properties-common && \
    curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add - && \
    curl https://packages.microsoft.com/config/ubuntu/22.04/prod.list > /etc/apt/sources.list.d/mssql-release.list && \
    apt-get update && \
    ACCEPT_EULA=Y apt-get install -y mssql-tools unixodbc-dev && \
    ln -s /opt/mssql-tools/bin/sqlcmd /usr/bin/sqlcmd && \
    ln -s /opt/mssql-tools/bin/bcp /usr/bin/bcp

# Set workdir and add optional test script
WORKDIR /app