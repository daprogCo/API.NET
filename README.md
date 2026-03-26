# API.NET — Learning C# by Building Real!

> *"Turning curiosity into code, one endpoint at a time..."*

---

## 👤 About Me

👋 Hi, I'm **David Savard** — a **Power Platform Developer** at **CGI**, with **over two years** of professional experience building business applications, automations, and custom solutions.

🎓 I also completed a **Data Engineer** formation at **Collège Bois-de-Boulogne**, where I deepened my understanding of data infrastructure, ETL pipelines, and cloud technologies.

🔗 [![LinkedIn](https://img.shields.io/badge/LinkedIn-Connect-blue?style=flat&logo=linkedin)](https://www.linkedin.com/in/david-savard-1b3a96274/)

This project is my personal challenge to break into **C#** and **.NET** development—because growth happens outside the comfort zone. 💪

---

## 🎯 Project Goal

Create an **API** using **C#** and **.NET**, inspired by the book:  
📘 [*Programming APIs with C# and .NET*](https://www.packtpub.com/en-us/product/programming-apis-with-c-and-net-9781803231099) by *Packt*.

The current functional goal is to build a **Car API** backed by SQL Server, exposing the car dataset through clear REST endpoints and preparing the project for production-oriented practices such as CI/CD, containerization, and VPS deployment.

---

## 🚗 Car Resource Scope

The `Car` resource is the core domain of this project.

### Exposure Rules

- All columns of the `Car` resource will be exposed through the API
- The only exception is the `is_deleted` column, which will remain internal and will not be exposed publicly

### Initial Read Endpoints

The first implementation phase focuses on `GET` endpoints:

- `GET /cars`
  - Returns the full list of cars
- `GET /cars/{id}`
  - Returns a single car by its unique identifier
- `GET /cars/search`
  - Supports filtering by:
    - `name` → contains a keyword such as `dodge`
    - `origin` → exact match such as `usa`
    - `mpg` → numeric filter such as `<= 20`

### Planned Write Endpoints

After the initial read endpoints, the API will also support data modification:

- `POST /cars`
  - Adds a new entry to the database
- `PUT /cars/{id}`
  - Updates an existing entry by its ID
- `DELETE /cars/{id}`
  - Deletes an existing entry by its ID

---

## 🔥 Adding Some Spice

To raise the stakes, I'm not just building this API locally.  
I'm also planning to:

- 🛠️ Set up a **CI/CD pipeline**
- 📦 **Containerize** the API  
- 🚀 **Deploy it to my Hostinger VPS**  
- 🌐 Make it publicly accessible at:  
  **[`https://api.daprog.co`](https://api.daprog.co)**

---

## 🤓 The Catch?

I **barely know** C#, .NET and GitHub Actions right now...  
But that’s what makes this challenge *awesome*.

---

## 🧠 Why This Matters

This isn’t just a project—it’s a **learning journey**.  
From zero to deploying an API in production, while mastering:

- C# syntax and best practices  
- .NET Core fundamentals  
- API design and architecture  
- DevOps workflows and automation  
- Docker containerization  

---

## 📌 Current Direction

The project is moving toward a practical, data-driven API centered around a real `Car` resource, with progressively richer querying and full CRUD support.

---

## 📄 Deployment Instructions

Want to run this API locally?  
Please refer to the [`SETUP.md`](./SETUP.md) file for a complete step-by-step guide.

> 🛠️ Includes environment setup, dependencies, configuration, and local execution steps.

---

# API.NET — Apprendre C# en construisant un vrai projet !

> *« Transformer la curiosité en code, une requête à la fois… »*
---

## 👤 À propos de moi

👋 Bonjour, je suis **David Savard**, **Développeur Power Platform** chez **CGI**, avec **plus de deux ans d'expérience** professionnelle dans la création d'applications métiers, d'automatisations et de solutions sur mesure.

🎓 J'ai également complété une formation en **Ingénierie des données** au **Collège Bois-de-Boulogne**, où j'ai approfondi mes compétences en infrastructure de données, pipelines ETL et technologies cloud.

🔗 [![LinkedIn](https://img.shields.io/badge/LinkedIn-Connecter-blue?style=flat&logo=linkedin)](https://www.linkedin.com/in/david-savard-1b3a96274/)

Ce projet est mon défi personnel pour me lancer dans le développement **C#** et **.NET** — parce que c'est en sortant de sa zone de confort qu'on progresse. 💪

---

## 🎯 Objectif du projet

Créer une **API fonctionnelle** avec **C#** et **.NET**, en suivant le livre :  
📘 [*Programming APIs with C# and .NET*](https://www.packtpub.com/en-us/product/programming-apis-with-c-and-net-9781803231099) publié par *Packt*.

L’objectif fonctionnel actuel est de construire une **API Car** appuyée sur SQL Server, qui expose le jeu de données des voitures à travers des endpoints REST clairs, tout en préparant le projet à des pratiques orientées production comme la CI/CD, la conteneurisation et le déploiement sur VPS.

---

## 🚗 Portée de la ressource Car

La ressource `Car` est le domaine central de ce projet.

### Règles d’exposition

- Toutes les colonnes de la ressource `Car` seront exposées par l’API
- La seule exception est la colonne `is_deleted`, qui restera interne et ne sera pas exposée publiquement

### Premiers endpoints de lecture

La première phase d’implantation se concentre sur des endpoints `GET` :

- `GET /cars`
  - Retourne la liste complète des voitures
- `GET /cars/{id}`
  - Retourne une voiture précise selon son identifiant unique
- `GET /cars/search`
  - Permet de filtrer par :
    - `name` → contient un mot-clé comme `dodge`
    - `origin` → correspondance exacte comme `usa`
    - `mpg` → filtre numérique comme `<= 20`

### Endpoints d’écriture prévus

Après les premiers endpoints de lecture, l’API prendra aussi en charge la modification des données :

- `POST /cars`
  - Ajoute une nouvelle entrée dans la base de données
- `PUT /cars/{id}`
  - Met à jour une entrée existante selon son ID
- `DELETE /cars/{id}`
  - Supprime une entrée existante selon son ID

---

## 🔥 Pour rendre ça plus corsé

Pour augmenter la difficulté, je ne vais pas seulement développer localement.  
Je prévois aussi de :

- 🛠️ Mettre en place une **pipeline CI/CD**
- 📦 **Conteneuriser** l'API  
- 🚀 La **déployer sur mon VPS Hostinger**  
- 🌐 La rendre accessible publiquement à l'adresse :  
  **[`https://api.daprog.co`](https://api.daprog.co)**

---

## 🤓 Le défi ?

Je connais à peine C# ou .NET en ce moment…  
Mais c'est justement ce qui rend ce projet excitant !

---

## 🧠 Pourquoi ce projet est important

Ce n’est pas juste un projet, c’est un **parcours d’apprentissage**.  
Passer de zéro à une API en production, tout en maîtrisant :

- La syntaxe et les bonnes pratiques C#  
- Les bases de .NET Core  
- La conception et l’architecture d’API  
- Les workflows DevOps et l’automatisation  
- La conteneurisation avec Docker  

---

## 📌 Direction actuelle

Le projet évolue vers une API pratique et orientée données, centrée sur une vraie ressource `Car`, avec des capacités de recherche plus riches et un support CRUD complet.

---

## 📄 Instructions de déploiement

Vous souhaitez exécuter cette API en local ?  
Consultez le fichier [`SETUP_FR.md`](./SETUP_FR.md) pour un guide complet étape par étape.

> 🛠️ Inclut la configuration de l’environnement, les dépendances, et les étapes d'exécution en local.
