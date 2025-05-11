# Footlink Solution

Questa solution contiene tutti i progetti necessari al backend di **Footlink**, la piattaforma B2B per distretti calzaturieri. L’approccio è basato su architettura a strati (Clean Architecture / Onion Architecture) e OOP, con migrazioni EF Core e predisposizione all’integrazione con Hyperledger Fabric.

---

## 📂 Struttura della solution

```
Footlink.sln
│
├── src/
│   ├── Footlink.Api/             ← Progetto Web API
│   ├── Footlink.Application/     ← Application layer (Casi d’uso, servizi)
│   ├── Footlink.Domain/          ← Domain layer (Entità, VO, interfacce)
│   └── Footlink.Infrastructure/  ← Infrastructure layer (EF Core, repository, autenticazione)
│
└── tests/
    ├── Footlink.Api.Tests/       ← Test di integrazione per controller e middleware
    └── Footlink.Domain.Tests/    ← Unit test delle entità e regole di business
```

---

## 🔍 Dettaglio dei progetti

### 1. `Footlink.Api`
- **Template**: ASP.NET Core Web API (.NET 8)
- **Responsabilità**:
  - Espone gli endpoint RESTful sotto `/api/*`
  - Configura middleware: **JWT Authentication**, CORS, logging, exception handling
  - Carica e registra i servizi dal livello Application e Infrastructure
  - Documentazione API con **Swagger / OpenAPI**
- **File chiave**:
  - `Program.cs` / `Startup.cs` (pipeline, DI, configuration)
  - `Controllers/` (es. `AuthController`, `CompaniesController`, `OrdersController`, ecc.)
  - `appsettings.json` (connection string, JWT settings, log settings)
- **Dipendenze**:
  - `Footlink.Application`
  - `Footlink.Infrastructure`

### 2. `Footlink.Application`
- **Template**: Class Library
- **Responsabilità**:
  - Contiene la **logica di business** (casi d’uso, orchestrazione)
  - Definisce i **DTO** (Data Transfer Objects) per input/output
  - Regole di validazione e mapping tra DTO e Domain Entities
  - Interfacce dei servizi (es. `IOrderService`, `IUserService`)
- **File chiave**:
  - `Services/` (implementazioni di orchestrazione, notifiche, integrazioni esterne)
  - `DTOs/` (es. `CreateOrderDto`, `UserDto`)
  - `Mappers/` (es. AutoMapper profiles, se usato)
- **Dipendenze**:
  - `Footlink.Domain`
  - `Footlink.Infrastructure` (solo per interfacce, preferibilmente)

### 3. `Footlink.Domain`
- **Template**: Class Library
- **Responsabilità**:
  - Definizione delle **entità di dominio** (es. `Company`, `User`, `Order`)
  - **Value Objects** (es. `Money`, `Address`)
  - **Interfacce** repository e unit-of-work (es. `ICompanyRepository`, `IUnitOfWork`)
  - Eccezioni di dominio e policy
- **File chiave**:
  - `Entities/`
  - `ValueObjects/`
  - `Interfaces/Repositories/`
  - `Exceptions/`
- **Dipendenze**: nessuna (core puro)

### 4. `Footlink.Infrastructure`
- **Template**: Class Library
- **Responsabilità**:
  - **Entity Framework Core**:
    - `FootlinkDbContext` con `DbSet<>` per ogni entità
    - Configurazioni FluentAPI (`OnModelCreating`)
  - **Migrazioni EF Core** (`Migrations/`)
  - Implementazioni concrete dei repository (`CompanyRepository`, `OrderRepository`)
  - Gestione della sicurezza:
    - `JwtTokenGenerator`
    - `PasswordHasher`
  - Eventuale integrazione con Hyperledger Fabric SDK (Progetto futuro `Footlink.Blockchain`)
- **File chiave**:
  - `Data/FootlinkDbContext.cs`
  - `Repositories/`
  - `Services/Security/`
  - `Migrations/`
- **Dipendenze**:
  - `Microsoft.EntityFrameworkCore`
  - `Npgsql.EntityFrameworkCore.PostgreSQL`
  - `Microsoft.Extensions.Configuration`

---

## 🛠️ Convenzioni e best practice

- **Namespace**: `Footlink.[Layer].[Area]`, es. `Footlink.Api.Controllers`, `Footlink.Domain.Entities`
- **Configurazioni** in `appsettings.json` e variabili d’ambiente:
  - `ConnectionStrings:DefaultConnection`
  - `Jwt:Issuer`, `Jwt:Key`, `Jwt:ExpireMinutes`
- **Logging**: usa `ILogger<T>` in ogni servizio/controller
- **Error handling**: middleware globale per mappare eccezioni di dominio in `400 Bad Request`
- **Migrations**:
  - Per ogni modifica alle entità:
    ```bash
    cd src/Footlink.Infrastructure
    dotnet ef migrations add NomeMigrazione
    dotnet ef database update
    ```
- **Testing**:
  - `Footlink.Api.Tests`: avvia un `WebApplicationFactory<TEntryPoint>` e chiama gli endpoint
  - `Footlink.Domain.Tests`: istanzia direttamente le entità e verifica le regole di business

---

## 🚀 Come iniziare

1. Clona la solution:
   ```bash
   git clone https://github.com/cimitalk/Footlink.sln.git
   cd Footlink
   ```
2. Apri la solution in Visual Studio o VS Code:
   ```bash
   code .
   ```
3. Configura il database e la connection string (file `src/Footlink.Api/appsettings.json`)
4. Dalla radice della solution:
   ```bash
   dotnet restore
   dotnet build
   dotnet run --project src/Footlink.Api
   ```
5. Col connessione al DB pronta, esegui le migrazioni:
   ```bash
   dotnet ef database update --project src/Footlink.Infrastructure
   ```
6. Apri Swagger: `https://localhost:5001/swagger`

---

## 📄 Licenza

Il codice è rilasciato sotto licenza MIT.
