# Chameleon Core API 🦎

A lightweight, scalable URL shortening API built with C# and .NET. Designed to serve as the backend for promotional and marketing campaigns, Chameleon Core allows you to generate short links and map them to long URLs with clean architecture principles.

## 🔧 Tech Stack

- **Language:** C# (.NET 9)
- **Architecture:** Clean Architecture
- **Database:** PostgreSQL (via EF Core)
- **Testing:** xUnit, Moq
- **Tooling:** Docker, GitHub Actions (WIP), Swagger, Redoc

## ✨ Features

- Generate short URLs mapped to long destination links
- Basic redirect logic
- Unit tests suite for core functionalities
- Clean, modular architecture following SOLID principles

## 🚀 Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download)
- [Docker](https://www.docker.com/) (optional)
- PostgreSQL instance (local or docker container)

### Run Locally

1. Clone the repo:

```bash
git clone https://github.com/scostadavid/chameleon-core-api.git
cd chameleon-core-api
```

2. Set up your environment variables (`appsettings.Development.json` or secrets).

3. Run the dev mode database container.
```
docker compose up
```

4. Run the project:

```bash
dotnet build
dotnet run --project API/Chameleon.API.csproj
```

## 📦 API Documentation

Test endpoint at:
```
http://localhost:5070/api/ping
```

Swagger is available at:

```
http://localhost:5070/docs/redoc
```

## 🧪 Tests

Run tests using the test project:

```bash
dotnet test
```

## 📈 Roadmap

- [x] Basic redirect and URL generation
- [x] Unit tests
- [ ] Full Docker support
- [ ] CI/CD pipeline (GitHub Actions)
- [ ] Authentication (JWT)
- [ ] URL Analytics (click count per short URL)
- [ ] Public deployment

## 📂 Project Structure (WIP)

```     
├── Chameleon.API  # Entry point (WebAPI and UI)
├── Chameleon.Application   # Use cases / Application logic
├── Chameleon.Domain        # Entities and core domain logic
├── Chameleon.Infrastructure # Data access, external services
├── Chameleon.Tests         # Test suite
```

## 🧑‍💻 Author

- **David S. Costa**  
  [Email](mailto:me@scostadavid.dev) • [GitHub](https://github.com/scostadavid) • [Website](https://scostadavid.dev) • [LinkedIn](https://linkedin.com/in/scostadavid)

---

> This project is actively evolving. Feedback and ideas are welcome.