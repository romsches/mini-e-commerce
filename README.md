# Mini E-Commerce – Full-Stack Web Application

Eine containerisierte E-Commerce-Webanwendung mit **ASP.NET Core, C#, React, TypeScript und PostgreSQL**. Das Projekt demonstriert die Entwicklung einer REST-API, die persistente Speicherung relationaler Daten, die Verwaltung von Produkten sowie die Verarbeitung von Warenkörben und Bestellungen.

Der Schwerpunkt liegt auf einer nachvollziehbaren Backend-Struktur, der Integration von Frontend und Backend sowie einer reproduzierbaren lokalen Entwicklungsumgebung mit Docker Compose.

## Inhaltsverzeichnis

- [Projektübersicht](#projektübersicht)
- [Screenshots und Funktionen](#screenshots-und-funktionen)
- [Technologie-Stack](#technologie-stack)
- [Architektur und Projektstruktur](#architektur-und-projektstruktur)
- [Funktionalitäten](#funktionalitäten)
- [Installation und Start](#installation-und-start)
- [REST-API und Swagger](#rest-api-und-swagger)
- [Mögliche Erweiterungen](#mögliche-erweiterungen)

---

## Projektübersicht

Die Anwendung bildet grundlegende Abläufe eines Online-Shops ab. Produkte werden über eine Weboberfläche angezeigt und verwaltet. Kunden können Produkte in einen Warenkorb legen und eine Bestellung aufgeben. Die Backend-API verarbeitet die Anfragen und persistiert die Daten in einer PostgreSQL-Datenbank.

Das Projekt verbindet eine React-basierte Benutzeroberfläche mit einem ASP.NET-Core-Backend und einer relationalen Datenbank. Die Komponenten werden über Docker Compose gemeinsam gestartet.

**Projektziele:**

- Entwicklung einer REST-API mit C# und ASP.NET Core
- Trennung von fachlicher Logik, Anwendungslogik, Datenzugriff und API
- Datenpersistenz mit Entity Framework Core und PostgreSQL
- Umsetzung typischer E-Commerce-Prozesse
- Integration eines TypeScript-Frontends
- Containerisierung der Anwendung mit Docker Compose
- Dokumentation und Überprüfung der API mit Swagger UI

## Screenshots und Funktionen

### 1. Produktkatalog

Der Produktkatalog bildet den Einstiegspunkt der Anwendung. Er zeigt die verfügbaren Produkte mit ihren wichtigsten Informationen und Preisen.

![Produktkatalog mit den verfügbaren Produkten](screenshots/01-product-catalog.png)

### 2. Produktverwaltung

Die Verwaltungsoberfläche ermöglicht die Übersicht über die gespeicherten Produkte und stellt die entsprechenden Verwaltungsaktionen bereit.

![Produktverwaltung](screenshots/05-product-management.png)

### 3. Produktformular

Über das Produktformular können Produktdaten erfasst oder bearbeitet werden. Die eingegebenen Daten werden an das Backend übermittelt.

![Formular zum Erstellen oder Bearbeiten eines Produkts](screenshots/04-product-form.png)

### 4. Warenkorb

Im Warenkorb werden die ausgewählten Produkte und deren Mengen zusammengefasst. Die Übersicht dient als Grundlage für den Bestellvorgang.

![Warenkorb mit ausgewählten Produkten](screenshots/02-cart-contents.png)

### 5. Bestellabschluss

Der Bestellvorgang zeigt den erfolgreichen Abschluss einer Bestellung in der Anwendung.

![Erfolgreich aufgegebene Bestellung](screenshots/03-order-placed.png)

### 6. REST-API mit Swagger UI

Die REST-API kann über Swagger UI erkundet und direkt getestet werden. Die folgenden Screenshots dokumentieren die Produkt-Endpunkte und eine Beispielanfrage zum Anlegen eines Produkts.

**Swagger UI – API-Übersicht**

![Übersicht der REST-API in Swagger UI](screenshots/06-swagger-api.png)

**GET-Endpunkt – Produkte abrufen**

![GET-Endpunkt in Swagger UI](screenshots/07-swagger-api-get.png)

**POST-Endpunkt – Produkt anlegen**

![POST-Endpunkt mit Request Body](screenshots/08-swagger-api-post.png)

**POST-Endpunkt – Ergebnis der Anfrage**

![Antwort des POST-Endpunkts](screenshots/09-swagger-api-post-response.png)

---

## Technologie-Stack

### Backend

- **C#**
- **ASP.NET Core 10 Web API**
- **Entity Framework Core**
- **REST-API**
- **Swagger / OpenAPI**

### Frontend

- **React**
- **TypeScript**
- **Vite**
- **CSS**

### Datenbank und Infrastruktur

- **PostgreSQL 15**
- **Docker**
- **Docker Compose**
- **Nginx** für die Bereitstellung des Frontends

---

## Architektur und Projektstruktur

Das Projekt ist in mehrere Verantwortungsbereiche aufgeteilt. Die API verarbeitet HTTP-Anfragen, die Anwendungsschicht bündelt anwendungsbezogene Abläufe, die Domänenschicht enthält fachliche Modelle und die Infrastruktur kümmert sich um technische Implementierungen wie den Datenzugriff.

Die Struktur orientiert sich an den Prinzipien einer geschichteten Architektur. Die konkrete Umsetzung und die Abhängigkeiten zwischen den Schichten sind im Quellcode nachvollziehbar.

```text
EcommerceBackend/
│
├── Ecommerce.Domain/
│   └── Fachliche Modelle und Domänenlogik
│
├── Ecommerce.Application/
│   └── DTOs, Anwendungslogik und Interfaces
│
├── Ecommerce.Infrastructure/
│   └── Datenzugriff und Persistenz
│
├── Ecommerce.Api/
│   └── REST-Controller, Konfiguration und API-Einstiegspunkt
│
├── ecommerce-frontend/
│   └── React-, TypeScript- und CSS-Anwendung
│
├── docker-compose.yml
├── .env.example
├── .gitignore
└── README.md
```

Die Verzeichnisbeschreibungen geben die vorgesehene Verantwortung der jeweiligen Bereiche wieder. Details können je nach tatsächlicher Implementierung abweichen.

---

## Funktionalitäten

### Produktkatalog und Produktverwaltung

- Produkte abrufen und im Frontend darstellen
- Produktdaten anlegen und bearbeiten
- Produkte über die Verwaltungsoberfläche verwalten
- Produktdaten dauerhaft in der Datenbank speichern

### Warenkorb und Bestellungen

- Produkte in den Warenkorb legen
- Ausgewählte Produkte und Mengen anzeigen
- Bestellung über den Checkout aufgeben
- Bestellinformationen im Backend verarbeiten

### Backend und Datenbank

- HTTP-Endpunkte für die Kommunikation zwischen Frontend und Backend
- Datenbankzugriff über Entity Framework Core
- Persistenz mit PostgreSQL
- API-Dokumentation und interaktive Tests über Swagger UI

### Containerisierung

- Gemeinsamer Start der Anwendungskomponenten über Docker Compose
- Separater Container für die Datenbank
- Containerisiertes Backend und Frontend

---

## Installation und Start

### Voraussetzungen

- [Git](https://git-scm.com/)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- Docker Compose

### 1. Repository klonen

```bash
git clone https://github.com/romsches/mini-e-commerce.git
cd mini-e-commerce
```

### 2. Umgebungsvariablen konfigurieren

Erstelle im Projektverzeichnis eine lokale `.env`-Datei auf Grundlage der Beispieldatei.

Unter Windows PowerShell:

```powershell
Copy-Item .env.example .env
```

Öffne anschließend `.env` und trage ein eigenes Datenbankpasswort ein:

```dotenv
POSTGRES_DB=EcommerceDb
POSTGRES_USER=postgres
POSTGRES_PASSWORD=CHANGE_ME
```

Ersetze `CHANGE_ME` durch dein gewünschtes Passwort. Die `.env`-Datei enthält lokale Konfiguration und darf nicht in das öffentliche Repository hochgeladen werden.

**Hinweis:** Wenn bereits ein PostgreSQL-Datenbankvolume existiert, kann die Datenbank weiterhin das bei der erstmaligen Initialisierung verwendete Passwort erwarten.

### 3. Anwendung starten

Stelle sicher, dass Docker Desktop läuft. Führe anschließend im Projektverzeichnis aus:

```bash
docker compose up --build
```

Docker Compose erstellt die benötigten Container und startet die konfigurierten Dienste.

### 4. Anwendung öffnen

| Komponente | Lokale Adresse |
|---|---|
| Frontend | http://localhost:5173 |
| Backend / Swagger UI | http://localhost:5083/swagger |

Die Ports entsprechen der lokalen Projektkonfiguration.

Zum Stoppen der Container:

```bash
docker compose down
```

Dieser Befehl stoppt die Container, ohne das Datenbankvolume ausdrücklich zu löschen.

---

## REST-API und Swagger

Swagger UI stellt die dokumentierten API-Endpunkte interaktiv dar. Darüber lassen sich unterstützte HTTP-Anfragen ausführen und die Antworten des Backends überprüfen.

Öffne:

http://localhost:5083/swagger

Die verfügbaren Endpunkte und ihre Request- sowie Response-Modelle sind direkt in der Swagger-Oberfläche dokumentiert.

Die Screenshots im Abschnitt [Screenshots und Funktionen](#screenshots-und-funktionen) zeigen beispielhaft die API-Übersicht, das Abrufen von Produkten und das Anlegen eines Produkts.

---

## Mögliche Erweiterungen

Folgende Funktionen könnten das Projekt in Zukunft ergänzen:

- Automatisierte Unit- und Integrationstests
- Authentifizierung und rollenbasierte Autorisierung
- Erweiterte Such-, Filter- und Sortierfunktionen
- Erweiterte Bestellverwaltung
- CI/CD-Pipeline
- Deployment in einer Cloud-Umgebung
- Zusätzliche Sicherheits- und Performance-Tests

Diese Punkte sind mögliche Erweiterungen und keine Aussage über bereits implementierte Funktionen.

---

## Repository

**GitHub:** https://github.com/romsches/mini-e-commerce

Das Repository enthält den Quellcode der Webanwendung, die Container-Konfiguration und diese Projektdokumentation.