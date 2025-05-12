### EF Core Migrations

Per generare nuove migrazioni nel progetto `Footlink.Infrastructure`, è necessario:

1. Aver installato globalmente il tool `dotnet-ef`:
   ```bash
   dotnet tool install --global dotnet-ef
Eseguire il comando da dentro la cartella src/Footlink.Infrastructure:
dotnet ef migrations add NomeMigrazione
Verificare che esista il file DesignTimeDbContextFactory.cs che fornisce a EF Core il contesto FootlinkDbContext a design-time.
Importante: Il file appsettings.json viene caricato da ../Footlink.Api/ grazie a questo codice:
.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Footlink.Api"))
Per questo motivo, è fondamentale che appsettings.json si trovi in Footlink.Api e contenga la connection string DefaultConnection.