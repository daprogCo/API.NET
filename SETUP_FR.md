# 🚀 Mise en place du projet — Développement local

Ce guide explique comment configurer et exécuter le projet localement à l'aide de Docker et Docker Compose.

---

## 🧱 Prérequis

- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)
- Un compte [Kaggle](https://www.kaggle.com/) (utilisé pour télécharger le jeu de données)

---

## 📁 Variables d’environnement

Créez un fichier `.env` à la racine du projet :

```bash
cp .env.example .env
```

Éditez le fichier `.env` et ajoutez la variable suivante :

```env
SA_PASSWORD=YourStrong!Passw0rd
```

Utilisez un mot de passe robuste qui respecte les exigences de complexité de SQL Server.  
Le fichier `.env` est ignoré par Git via `.gitignore`, donc il ne sera pas versionné.

---

## 🐳 Démarrer les conteneurs

Depuis la racine du projet, exécutez :

```bash
docker-compose up --build -d
```

Cette commande va :

- Démarrer un conteneur **SQL Server 2022**.
- Construire et démarrer un conteneur **dataloader** qui :
  - Attend que SQL Server soit prêt.
  - Initialise la base de données si elle n’existe pas.
  - Télécharge le jeu de données Kaggle si la table `Car` est vide.
  - Charge les données dans la table `Car`.

Pour suivre les journaux et voir ce qu’il se passe :

```bash
docker-compose logs -f dataloader
```

---

## 🗂 Structure du répertoire

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

## 🔑 Jeton API Kaggle

Pour permettre le téléchargement des jeux de données depuis Kaggle :

1. Allez dans les paramètres de votre compte Kaggle.
2. Cliquez sur **"Create New API Token"**.
3. Enregistrez le fichier téléchargé `kaggle.json`.
4. Déplacez ce fichier dans le répertoire `data-loader/`.

Structure attendue :

```plaintext
data-loader/
└── kaggle.json
```

Assurez-vous que le fichier a les bonnes permissions (gérées automatiquement dans le Dockerfile).

---

## 🧪 Vérification de l’installation

Vous pouvez vous connecter à SQL Server en utilisant :

- SQL Server Management Studio (SSMS)
- Azure Data Studio

Détails de connexion :

- Serveur : `localhost`
- Port : `1433`
- Nom d’utilisateur : `sa`
- Mot de passe : celui défini dans votre fichier `.env`

Vérifiez les points suivants :

- La base de données `Cars` existe.
- La table `Car` existe.
- Les données du jeu Kaggle ont été correctement chargées.

---

## 🧹 Nettoyage

Pour arrêter et supprimer tous les conteneurs, réseaux et volumes :

```bash
docker-compose down -v
```

---

## 🧾 Remarques

L’image du conteneur `dataloader` est construite à partir d’un Dockerfile personnalisé basé sur `ubuntu:22.04`.

Elle inclut :

- L’outil en ligne `sqlcmd` pour se connecter à SQL Server.
- L’outil `kaggle` CLI pour télécharger les jeux de données.
- Python 3 et pip.

Pour ouvrir un terminal dans le conteneur `dataloader` :

```bash
docker exec -it dataloader bash
```