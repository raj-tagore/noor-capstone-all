# noor-capstone-all

My .NET 8 + SQL Server capstone and practice projects.

The projects were written for Windows with **SQL Server LocalDB** (`(localdb)\MSSQLLocalDB`). LocalDB does not run on macOS. On a Mac, I run **SQL Server 2022 in Docker** (through Colima) and point each project at it with a git-ignored `appsettings.Local.json`.

## Projects

| Folder | Solution | Type | Database | SQL script |
| --- | --- | --- | --- | --- |
| `LINQ-practice` | `Infosys.EmployeeManagement.csproj` | Console, LINQ only | none | none |
| `dal-project-and-soln` | `Infosys.QuickKartDBFirst` | DAL + console | QuickKartDB | `Infosys.DBFirstCore.DataAccessLayer/QuickKartDB.sql` |
| `capstone-1` | `PolyclinicApp/PolyclinicApp.sln` | DAL + console | PolyclinicDB | `Polyclinic_SQLScripts.sql` |
| `capstone-2` | `CureWell/CureWell.sln` | DAL + Web API | CureWellDB | `CureWellDataAccessLayer/CureWell.sql` |
| `capstone-4` | `MusicLibraryApp/MusicLibraryApp.sln` | DAL + console + Web API | MusicLibraryDB | `MusicLibrary_SQLScripts.sql` |
| `capstone-fa1-retest` | `CureHospital/CureHospital.sln` | DAL + test console + Web API | CureHospitalFA1DB | `CureHospitalDALCrossPlatform/CureHospitalDB.sql` |

`capstone-1` and `capstone-fa1-retest` already read `appsettings.Local.json` (see [Step 5](#5-point-a-project-at-docker)). The others need the same small change before they can run on a Mac.

---

## Mac setup (one time)

Tested on an Apple Silicon (M-series) MacBook Air with Homebrew.

### 1. Install the tools

```bash
brew install colima docker sqlcmd
```

| Tool | What it's for |
| --- | --- |
| `colima` | Lightweight Linux VM that runs Docker containers. A free, command-line alternative to Docker Desktop. |
| `docker` | Docker CLI, used to talk to Colima. |
| `sqlcmd` | Microsoft's cross-platform SQL command-line tool (go-sqlcmd). Runs the `.sql` scripts. |

### 2. Install the .NET 8 SDK

Microsoft's install script puts the SDK in `~/.dotnet` and needs no `sudo`:

```bash
curl -sSL https://dot.net/v1/dotnet-install.sh -o /tmp/dotnet-install.sh
bash /tmp/dotnet-install.sh --channel 8.0 --install-dir "$HOME/.dotnet"
```

Add it to your shell. Append this to `~/.zshrc`:

```bash
# .NET SDK
export DOTNET_ROOT="$HOME/.dotnet"
export PATH="$DOTNET_ROOT:$DOTNET_ROOT/tools:$PATH"
export DOTNET_CLI_TELEMETRY_OPTOUT=1
```

Open a **new** terminal and check it:

```bash
dotnet --list-sdks     # should show 8.0.x
```

### 3. Start Colima with Rosetta

SQL Server only ships an x86-64 (`amd64`) Linux image. On Apple Silicon, Colima runs it through Rosetta emulation:

```bash
colima start --vm-type=vz --vz-rosetta --arch aarch64 --cpu 2 --memory 4
```

SQL Server needs **at least 2 GB of RAM**. 4 GB is comfortable. Colima remembers these settings, so after the first time `colima start` is enough.

### 4. Run SQL Server 2022 in a container

```bash
docker run -d --name polyclinic-sql \
  --platform linux/amd64 \
  -e ACCEPT_EULA=Y \
  -e 'MSSQL_SA_PASSWORD=Polyclinic@Dev123' \
  -p 1433:1433 \
  --restart unless-stopped \
  mcr.microsoft.com/mssql/server:2022-latest
```

- The image is about 1.5 GB, so the first pull can take a while.
- `--restart unless-stopped` brings SQL Server back automatically every time Colima starts.
- One container serves **every** project. Each project uses its own database inside it.
- The password must be at least 8 characters with upper case, lower case, a digit and a symbol, or SQL Server won't start.

Wait about 15–30 seconds for it to boot, then check:

```bash
sqlcmd -S localhost,1433 -U sa -P 'Polyclinic@Dev123' -C -Q "SELECT @@VERSION"
```

`-C` trusts the container's self-signed certificate.

### 5. Point a project at Docker

The committed `appsettings.json` files keep their LocalDB connection strings, so the projects still work on Windows. On the Mac, each project's `DbContext` also loads an optional **`appsettings.Local.json`**, which overrides the connection string.

**a. Make the DbContext read the override.** In `OnConfiguring`, add one line to the `ConfigurationBuilder`:

```csharp
var builder = new ConfigurationBuilder()
              .SetBasePath(basePath)
              .AddJsonFile("appsettings.json")
              .AddJsonFile("appsettings.Local.json", optional: true);   // <- add this
```

`optional: true` means nothing changes on a machine without the file.

**b. Create `appsettings.Local.json`** next to the startup project's `appsettings.json`. That's the console or Web API project. Use the same connection-string **name** as that project's `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "PolyclinicDBConnectionString": "Server=localhost,1433;Database=PolyclinicDB;User Id=sa;Password=Polyclinic@Dev123;TrustServerCertificate=true;Encrypt=false"
  }
}
```

**c. Copy it to the build output.** In the startup project's `.csproj`:

```xml
<ItemGroup>
  <None Update="appsettings.Local.json">
    <CopyToOutputDirectory>Always</CopyToOutputDirectory>
  </None>
</ItemGroup>
```

**d. Keep it out of git.** Add this to that solution's `.gitignore`:

```gitignore
appsettings.Local.json
```

> [!NOTE]
> Integrated Security (Windows login) doesn't work against a Linux container. The Mac connection string uses SQL authentication (`User Id=sa;Password=...`).

### 6. Create a project's database

Run that project's script against the container. For example, capstone-1:

```bash
sqlcmd -S localhost,1433 -U sa -P 'Polyclinic@Dev123' -C -i capstone-1/Polyclinic_SQLScripts.sql
```

Every script drops and recreates its own database, so running it again **resets the data**. Do that whenever a test run leaves rows behind (for example, a duplicate-key error on an insert test).

---

## Everyday use

```bash
colima start                                  # after a reboot. SQL Server starts with it.

# capstone-1 (console)
dotnet run --project capstone-1/PolyclinicApp/PolyclinicApp.ConsoleApp

# capstone-fa1-retest (test console + Web API)
dotnet run --project capstone-fa1-retest/CureHospital/CureHospitalTest
dotnet run --project capstone-fa1-retest/CureHospital/CureHospitalWebService --launch-profile http
#   Swagger: http://localhost:5111/swagger

colima stop                                   # frees the VM's RAM when you're done
```

### Useful commands

```bash
docker ps                                     # is the SQL container running?
docker logs polyclinic-sql --tail 30          # SQL Server startup / error log
sqlcmd -S localhost,1433 -U sa -P 'Polyclinic@Dev123' -C -Q "SELECT name FROM sys.databases"
dotnet build-server shutdown                  # stop background MSBuild / compiler servers
```

To browse the databases in a GUI, connect **Azure Data Studio**, **VS Code's SQL Server (mssql) extension** or **DBeaver** to `localhost,1433` with user `sa`.

---

## Troubleshooting

| Symptom | Fix |
| --- | --- |
| `zsh: command not found: dotnet` | Open a new terminal, or run `source ~/.zshrc`. |
| `A network-related or instance-specific error` / `LocalDB is not supported on this platform` | The app is still using the LocalDB string. Check that `appsettings.Local.json` exists, is in `bin/Debug/net8.0/`, and uses the same connection-string name. |
| `Cannot connect to the Docker daemon` | Colima isn't running. Run `colima start`. |
| `Login failed for user 'sa'` right after starting | SQL Server is still booting. Wait 20 seconds and retry. |
| Container exits immediately | Run `docker logs polyclinic-sql`. Usually the password is too weak or Colima has under 2 GB of RAM. |
| `Cannot open database "XyzDB"` | That project's SQL script hasn't been run yet (Step 6). |
| A test passed once but fails on the second run | Its data changed the database. Re-run the project's SQL script. |

> [!WARNING]
> `sa` / `Polyclinic@Dev123` is a **local development password only**. Port 1433 is published on your machine. Don't reuse this password anywhere else.
