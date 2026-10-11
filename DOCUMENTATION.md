# Technische Projektdokumentation: Enterprise E-Commerce System

Diese Dokumentation beschreibt die tiefgreifende Architektur, die Design-Entscheidungen, die Komponenten und die Betriebsabläufe des containerisierten E-Commerce-Systems.

---

## 1. Architektur & Design-Prinzipien

Das System wurde von Grund auf nach den Paradigmen der **Clean Architecture** (Robert C. Martin / Uncle Bob) und des **Domain-Driven Designs (DDD)** konzipiert. Ziel ist es, die Geschäftslogik (Domain) komplett unabhängig von externen Frameworks, UI-Technologien und Datenbanken zu halten.

### Die 4 Schichten im Detail:

1. **`Ecommerce.Domain` (Kernschicht):**
   * Enthält die puren Geschäftsmodelle (Entities wie `Product`, `Cart`, `CartItem`, `Order`, `OrderItem`) und Value Objects.
   * Keine Abhängigkeiten zu anderen Projekten. Implementiert fachliche Validierungen und invariants Verhalten.

2. **`Ecommerce.Application` (Anwendungsschicht):**
   * Enthält die Use Cases, Data Transfer Objects (DTOs), Service-Interfaces und Validierungsregeln.
   * Nutzt **FluentValidation** für typsichere und automatisierte Eingabeprüfungen (Pipelines).

3. **`Ecommerce.Infrastructure` (Infrastrukturschicht):**
   * Zuständig für Persistence (Datenbankzugriff) mittels **Entity Framework Core**.
   * Beherbergt den `EcommerceDbContext`, Konfigurationen, Fluent API Mappings (z. B. präzise Dezimaldefinitionen für Währungen mit `.HasPrecision(18, 2)`) sowie die EF Core Migrations.

4. **`Ecommerce.Api` (Präsentationsschicht):**
   * ASP.NET Core 10.0 Web API Controller.
   * Verarbeitet HTTP-Requests, CORS-Richtlinien, Swagger/OpenAPI-Dokumentation und das **Global Exception Handling** über eine dedizierte Middleware.

---

## 2. Datenbank & Datenfluss

* **Datenbank-System:** PostgreSQL 16 (isoliert in einem Docker-Volume für persistente Datenspeicherung).
* **Automatisches Migrationsmanagement:** 
  Beim Start der ASP.NET Core Anwendung prüft das System den Migrationsstatus und wendet ausstehende Schema-Änderungen (`dbContext.Database.MigrateAsync()`) vollautomatisch zur Laufzeit an. Das verhindert manuelle Konsolenschritte im Produktivbetrieb.
* **Transaktionssicherheit:** Wichtige Operationen (wie Bestellabschluss oder Warenkorb-Manipulationen) nutzen die ACID-Garantien von PostgreSQL und die Change-Tracking-Mechanismen von EF Core.

---

## 3. Frontend-Architektur (SPA)

* **Technologie:** React, TypeScript, Vite.
* **Struktur:** 
  * Komponentenbasiertes Design mit klarer Trennung zwischen Shop-Ansicht (Katalog, Warenkorb, Checkout) und Admin-Panel (CRUD-Operationen für Produkte, Bestellübersicht).
  * **Axios** als zentraler HTTP-Client für die Kommunikation mit der REST-API.
* **UX-Optimierungen:** Spezielle Event-Listener (`onFocus={e => e.target.select()}`) in numerischen Formularen verhindern Usability-Frustrationen bei der Produkteingabe (Vermeidung störender Standard-Nullen).

---

## 4. Containerisierung & DevOps (Docker)

Das gesamte System ist über **Docker Compose** in ein orchestriertes Netzwerk (`ecommerce-net`) eingebunden:

* **`ecommerce_postgres`:** Offizielles PostgreSQL-Image mit persistentem Docker-Volume.
* **`ecommerce_backend`:** Multi-Stage Dockerfile für das .NET-Backend, optimiert für minimale Image-Größen und maximale Sicherheit.
* **`ecommerce_frontend`:** Node.js-Build-Umgebung, deren statische Artefakte in einen performanten **Nginx Alpine** Webserver eingespeist werden.

---

## 5. Fehlerbehandlung & Logging

* **Global Exception Handling (`GlobalExceptionHandlerMiddleware`):** Fängt unvorhergesehene Laufzeitfehler im Backend ab, loggt sie strukturiert und gibt standardisierte JSON-Fehlerantworten an den Client zurück.
* **Structured Logging (Serilog):** Protokolliert Anfragen, Systemzustände und Fehler direkt in der Konsole mit konfigurierbaren Filtern zur Reduzierung von Rauschen durch Drittanbieter-Bibliotheken.