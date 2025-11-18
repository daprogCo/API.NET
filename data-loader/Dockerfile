FROM ubuntu:22.04

ENV ACCEPT_EULA=Y
ENV DEBIAN_FRONTEND=noninteractive

# Install MSSQL tools and required packages
RUN apt-get update && \
    apt-get install -y curl gnupg software-properties-common unzip && \
    curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add - && \
    curl https://packages.microsoft.com/config/ubuntu/22.04/prod.list > /etc/apt/sources.list.d/mssql-release.list && \
    apt-get update && \
    ACCEPT_EULA=Y apt-get install -y mssql-tools unixodbc-dev python3 python3-pip && \
    ln -s /opt/mssql-tools/bin/sqlcmd /usr/bin/sqlcmd && \
    ln -s /opt/mssql-tools/bin/bcp /usr/bin/bcp && \
    pip install kaggle


# Set workdir and add optional test script
WORKDIR /app

# Kaggle auth token
COPY kaggle.json /root/.kaggle/kaggle.json
RUN chmod 600 /root/.kaggle/kaggle.json

# Copy the initialization scripts
COPY init.sql ./init.sql
COPY entrypoint.sh ./entrypoint.sh
COPY import_data.sql ./import_data.sql

# Make the scripts executable
RUN chmod +x /app/*.sh

# Set the entrypoint
ENTRYPOINT ["./entrypoint.sh"]