````markdown
# SETUP.md

## 🚀 Project Setup — Local Development

This guide explains how to set up and run the project locally using Docker and Docker Compose.

---

## 🧱 Requirements

- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)
- A [Kaggle](https://www.kaggle.com/) account (used to download the dataset)

---

## 📁 Environment Variables

Create a `.env` file at the root of the project:

```bash
cp .env.example .env
```

Edit the `.env` file and add the following variable:

```env
SA_PASSWORD=YourStrong!Passw0rd
```

Use a strong password that meets SQL Server's complexity requirements.  
The `.env` file is ignored by Git through `.gitignore`, so it won't be committed.

---

## 🐳 Start the Containers

From the root directory, run:

```bash
docker-compose up --build -d
```

This command will:

- Start a SQL Server 2022 container.
- Build and start a dataloader container that:
  - Waits for SQL Server to be ready.
  - Initializes the database if it doesn’t exist.
  - Downloads the Kaggle dataset if the `Car` table is empty.
  - Loads the data into the `Car` table.

To follow the logs and see what's happening:

```bash
docker-compose logs -f dataloader
```

---

## 🗂 Directory Structure

```plaintext
.
├── docker-compose.yml
├── .env
├── data-loader/
│   ├── Dockerfile
│   ├── entrypoint.sh
│   ├── init.sql
│   └── kaggle.json
```

---

## 🔑 Kaggle API Token

To allow the project to download datasets from Kaggle:

1. Go to your Kaggle account settings.
2. Click "Create New API Token".
3. Save the downloaded file `kaggle.json`.
4. Move the file into the `data-loader/` directory.

Expected structure:

```plaintext
data-loader/
└── kaggle.json
```

Make sure the file has the correct permissions. The Dockerfile takes care of setting them automatically.

---

## 🧪 Verify the Setup

You can connect to SQL Server using:

- SQL Server Management Studio (SSMS)
- Azure Data Studio

Connection details:

- Server: `localhost`
- Port: `1433`
- Username: `sa`
- Password: the one you defined in your `.env` file

Verify the following:

- The `Cars` database exists.
- The `Car` table exists.
- Data from the Kaggle dataset has been loaded successfully.

---

## 🧹 Cleanup

To stop and remove all containers, networks, and volumes:

```bash
docker-compose down -v
```

---

## 🧾 Notes

The dataloader image is built from a custom Dockerfile based on `ubuntu:22.04`.

It includes:

- `sqlcmd` CLI for connecting to SQL Server.
- `kaggle` CLI for downloading datasets.
- Python 3 and pip.

To open a shell inside the dataloader container:

```bash
docker exec -it dataloader bash
```