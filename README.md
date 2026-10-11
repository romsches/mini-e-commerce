# Mini E-Commerce вЂ“ Full-Stack Web Application

Eine containerisierte E-Commerce-Webanwendung mit **ASP.NET Core, C#, React, TypeScript und PostgreSQL**. Das Projekt demonstriert die Entwicklung einer REST-API, die persistente Speicherung relationaler Daten, die Verwaltung von Produkten sowie die Verarbeitung von WarenkГ¶rben und Bestellungen.

Der Schwerpunkt liegt auf einer nachvollziehbaren Backend-Struktur, der Integration von Frontend und Backend sowie einer reproduzierbaren lokalen Entwicklungsumgebung mit Docker Compose.

## Inhaltsverzeichnis

- [ProjektГјbersicht](#projektГјbersicht)
- [Screenshots und Funktionen](#screenshots-und-funktionen)
- [Technologie-Stack](#technologie-stack)
- [Architektur und Projektstruktur](#architektur-und-projektstruktur)
- [FunktionalitГ¤ten](#funktionalitГ¤ten)
- [Installation und Start](#installation-und-start)
- [REST-API und Swagger](#rest-api-und-swagger)
- [MГ¶gliche Erweiterungen](#mГ¶gliche-erweiterungen)

---

## ProjektГјbersicht

Die Anwendung bildet grundlegende AblГ¤ufe eines Online-Shops ab. Produkte werden Гјber eine WeboberflГ¤che angezeigt und verwaltet. Kunden kГ¶nnen Produkte in einen Warenkorb legen und eine Bestellung aufgeben. Die Backend-API verarbeitet die Anfragen und persistiert die Daten in einer PostgreSQL-Datenbank.

Das Projekt verbindet eine React-basierte BenutzeroberflГ¤che mit einem ASP.NET-Core-Backend und einer relationalen Datenbank. Die Komponenten werden Гјber Docker Compose gemeinsam gestartet.

**Projektziele:**

- Entwicklung einer REST-API mit C# und ASP.NET Core
- Trennung von fachlicher Logik, Anwendungslogik, Datenzugriff und API
- Datenpersistenz mit Entity Framework Core und PostgreSQL
- Umsetzung typischer E-Commerce-Prozesse
- Integration eines TypeScript-Frontends
- Containerisierung der Anwendung mit Docker Compose
- Dokumentation und ГњberprГјfung der API mit Swagger UI

## Screenshots und Funktionen

### 1. Produktkatalog

Der Produktkatalog bildet den Einstiegspunkt der Anwendung. Er zeigt die verfГјgbaren Produkte mit ihren wichtigsten Informationen und Preisen.

![Produktkatalog mit den verfГјgbaren Produkten](screenshots/01-product-catalog.jpg)

### 2. Produktverwaltung

Die VerwaltungsoberflГ¤che ermГ¶glicht die Гњbersicht Гјber die gespeicherten Produkte und stellt die entsprechenden Verwaltungsaktionen bereit.

![Produktverwaltung](screenshots/05-product-management.jpg)

### 3. Produktformular

Гњber das Produktformular kГ¶nnen Produktdaten erfasst oder bearbeitet werden. Die eingegebenen Daten werden an das Backend Гјbermittelt.

![Formular zum Erstellen oder Bearbeiten eines Produkts](screenshots/04-product-form.jpg)

### 4. Warenkorb

Im Warenkorb werden die ausgewГ¤hlten Produkte und deren Mengen zusammengefasst. Die Гњbersicht dient als Grundlage fГјr den Bestellvorgang.

![Warenkorb mit ausgewГ¤hlten Produkten](screenshots/02-cart-contents.jpg)

### 5. Bestellabschluss

Der Bestellvorgang zeigt den erfolgreichen Abschluss einer Bestellung in der Anwendung.

![Erfolgreich aufgegebene Bestellung](screenshots/03-order-placed.jpg)

### 6. REST-API mit Swagger UI

Die REST-API kann Гјber Swagger UI erkundet und direkt getestet werden. Die folgenden Screenshots dokumentieren die Produkt-Endpunkte und eine Beispielanfrage zum Anlegen eines Produkts.

**Swagger UI вЂ“ API-Гњbersicht**

![Гњbersicht der REST-API in Swagger UI](screenshots/06-swagger-api.jpg)

**GET-Endpunkt вЂ“ Produkte abrufen**

![GET-Endpunkt in Swagger UI](screenshots/07-swagger-api-get.jpg)

**POST-Endpunkt вЂ“ Produkt anlegen**

![POST-Endpunkt mit Request Body](screenshots/08-swagger-api-post.jpg)

**POST-Endpunkt вЂ“ Ergebnis der Anfrage**

![Antwort des POST-Endpunkts](screenshots/09-swagger-api-post-response.jpg)

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
- **Nginx** fГјr die Bereitstellung des Frontends

---

## Architektur und Projektstruktur

Das Projekt ist in mehrere Verantwortungsbereiche aufgeteilt. Die API verarbeitet HTTP-Anfragen, die Anwendungsschicht bГјndelt anwendungsbezogene AblГ¤ufe, die DomГ¤nenschicht enthГ¤lt fachliche Modelle und die Infrastruktur kГјmmert sich um technische Implementierungen wie den Datenzugriff.

Die Struktur orientiert sich an den Prinzipien einer geschichteten Architektur. Die konkrete Umsetzung und die AbhГ¤ngigkeiten zwischen den Schichten sind im Quellcode nachvollziehbar.

```text
EcommerceBackend/
в”‚
в”њв”Ђв”Ђ Ecommerce.Domain/
в”‚   в””в”Ђв”Ђ Fachliche Modelle und DomГ¤nenlogik
в”‚
в”њв”Ђв”Ђ Ecommerce.Application/
в”‚   в””в”Ђв”Ђ DTOs, Anwendungslogik und Interfaces
в”‚
в”њв”Ђв”Ђ Ecommerce.Infrastructure/
в”‚   в””в”Ђв”Ђ Datenzugriff und Persistenz
в”‚
в”њв”Ђв”Ђ Ecommerce.Api/
в”‚   в””в”Ђв”Ђ REST-Controller, Konfiguration und API-Einstiegspunkt
в”‚
в”њв”Ђв”Ђ ecommerce-frontend/
в”‚   в””в”Ђв”Ђ React-, TypeScript- und CSS-Anwendung
в”‚
в”њв”Ђв”Ђ docker-compose.yml
в”њв”Ђв”Ђ .env.example
в”њв”Ђв”Ђ .gitignore
в””в”Ђв”Ђ README.md
```

Die Verzeichnisbeschreibungen geben die vorgesehene Verantwortung der jeweiligen Bereiche wieder. Details kГ¶nnen je nach tatsГ¤chlicher Implementierung abweichen.

---

## FunktionalitГ¤ten

### Produktkatalog und Produktverwaltung

- Produkte abrufen und im Frontend darstellen
- Produktdaten anlegen und bearbeiten
- Produkte Гјber die VerwaltungsoberflГ¤che verwalten
- Produktdaten dauerhaft in der Datenbank speichern

### Warenkorb und Bestellungen

- Produkte in den Warenkorb legen
- AusgewГ¤hlte Produkte und Mengen anzeigen
- Bestellung Гјber den Checkout aufgeben
- Bestellinformationen im Backend verarbeiten

### Backend und Datenbank

- HTTP-Endpunkte fГјr die Kommunikation zwischen Frontend und Backend
- Datenbankzugriff Гјber Entity Framework Core
- Persistenz mit PostgreSQL
- API-Dokumentation und interaktive Tests Гјber Swagger UI

### Containerisierung

- Gemeinsamer Start der Anwendungskomponenten Гјber Docker Compose
- Separater Container fГјr die Datenbank
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

Г–ffne anschlieГџend `.env` und trage ein eigenes Datenbankpasswort ein:

```dotenv
POSTGRES_DB=EcommerceDb
POSTGRES_USER=postgres
POSTGRES_PASSWORD=CHANGE_ME
```

Ersetze `CHANGE_ME` durch dein gewГјnschtes Passwort. Die `.env`-Datei enthГ¤lt lokale Konfiguration und darf nicht in das Г¶ffentliche Repository hochgeladen werden.

**Hinweis:** Wenn bereits ein PostgreSQL-Datenbankvolume existiert, kann die Datenbank weiterhin das bei der erstmaligen Initialisierung verwendete Passwort erwarten.

### 3. Anwendung starten

Stelle sicher, dass Docker Desktop lГ¤uft. FГјhre anschlieГџend im Projektverzeichnis aus:

```bash
docker compose up --build
```

Docker Compose erstellt die benГ¶tigten Container und startet die konfigurierten Dienste.

### 4. Anwendung Г¶ffnen

| Komponente | Lokale Adresse |
|---|---|
| Frontend | http://localhost:5173 |
| Backend / Swagger UI | http://localhost:5083/swagger |

Die Ports entsprechen der lokalen Projektkonfiguration.

Zum Stoppen der Container:

```bash
docker compose down
```

Dieser Befehl stoppt die Container, ohne das Datenbankvolume ausdrГјcklich zu lГ¶schen.

---

## REST-API und Swagger

Swagger UI stellt die dokumentierten API-Endpunkte interaktiv dar. DarГјber lassen sich unterstГјtzte HTTP-Anfragen ausfГјhren und die Antworten des Backends ГјberprГјfen.

Г–ffne:

http://localhost:5083/swagger

Die verfГјgbaren Endpunkte und ihre Request- sowie Response-Modelle sind direkt in der Swagger-OberflГ¤che dokumentiert.

Die Screenshots im Abschnitt [Screenshots und Funktionen](#screenshots-und-funktionen) zeigen beispielhaft die API-Гњbersicht, das Abrufen von Produkten und das Anlegen eines Produkts.

---

## MГ¶gliche Erweiterungen

Folgende Funktionen kГ¶nnten das Projekt in Zukunft ergГ¤nzen:

- Automatisierte Unit- und Integrationstests
- Authentifizierung und rollenbasierte Autorisierung
- Erweiterte Such-, Filter- und Sortierfunktionen
- Erweiterte Bestellverwaltung
- CI/CD-Pipeline
- Deployment in einer Cloud-Umgebung
- ZusГ¤tzliche Sicherheits- und Performance-Tests

Diese Punkte sind mГ¶gliche Erweiterungen und keine Aussage Гјber bereits implementierte Funktionen.

---

## Repository

**GitHub:** https://github.com/romsches/mini-e-commerce

Das Repository enthГ¤lt den Quellcode der Webanwendung, die Container-Konfiguration und diese Projektdokumentation.
