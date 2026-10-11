# Enterprise-Grade E-Commerce Micro-Architecture & Web Application

Eine hochmoderne, containerisierte E-Commerce-Plattform, entwickelt nach den Prinzipien der **Clean Architecture** und des **Domain-Driven Designs (DDD)**. Das System demonstriert professionelles Software-Engineering im Full-Stack-Bereich mit besonderem Fokus auf Skalierbarkeit, Typsicherheit, strikte Schichtentrennung und produktionsreife Containerisierung.

---

## 🏗️ Technische Architektur & Tech-Stack

Das Projekt folgt einer strikten Schichtenarchitektur (`Clean Architecture`), um eine hohe Testbarkeit, lose Kopplung und langfristige Wartbarkeit sicherzustellen.

### Backend (.NET / C#)
* **Framework:** ASP.NET Core 10.0 Web API
* **Architekturmuster:** Clean Architecture (Domain, Application, Infrastructure, Presentation/API)
* **Datenbank-ORM:** Entity Framework Core mit PostgreSQL
* **Validierung:** FluentValidation (automatisiertes Pipeline-Behavior)
* **Logging:** Serilog (strukturierte Protokollierung mit Filtern)
* **API-Dokumentation:** OpenAPI / Swagger UI

### Frontend (React / TypeScript)
* **Framework:** React mit Vite
* **Typsicherheit:** TypeScript (strict mode)
* **HTTP-Client:** Axios (zentralisiertes Interceptor- und Error-Handling)
* **Styling & UI:** Modulare, responsive CSS-Struktur

### DevOps & Infrastructure
* **Containerisierung:** Docker & Docker Compose (Multi-Container-Setup)
* **Webserver / Reverse Proxy:** Nginx (für den produktiven Frontend-Build)
* **Datenbank:** PostgreSQL 16 (mit automatischem Migrationsmanagement zur Laufzeit)

---

## 📂 Projektstruktur

```text
EcommerceBackend/
│
├── Ecommerce.Domain/          # Kern-Entitäten, Value Objects & Domain-Logik
├── Ecommerce.Application/     # Use Cases, DTOs, Interfaces & FluentValidation
├── Ecommerce.Infrastructure/  # EF Core DbContext, Persistence & Repositories
├── Ecommerce.Api/             # REST-Controller, Middleware & Program.cs
├── ecommerce-frontend/        # React + TypeScript Single Page Application
├── docker-compose.yml         # Orchestrierung der Microservices
└── README.md