# CompanyManager With WebApi and AppSettings

**Lernziele:**

- Wie die AppSettings in den Projekten `CompanyManager.WebApi` und `CompanyManager.Console` verwendet werden.

**Hinweis:** Als Startpunkt wird die Vorlage [CompanyManagerWithWebApi](https://github.com/leoggehrer/CompanyManagerWithWebApiGeneric) verwendet.

## Vorbereitung

Bevor mit der Umsetzung begonnen wird, sollte die Vorlage heruntergeladen und die Funktionalität verstanden werden. Zusätzlich sollte die Präsentation zum Thema 'AppSettings' durchgearbeitet werden. Die Präsentation finden Sie [hier](https://github.com/leoggehrer/Slides/tree/main/DotnetAppSettings).

## Packages installieren

Das Laden der 'AppSettings' sollte von allen ausführbaren Projekten erfolgen können. Um diese Anforderung zu erfüllen, sollte das Laden der Einstellungen im Projekt `CompanyManager.Logic` erfolgen. Dieses Projekt wird von beiden Projekten `CompanyManager.WebApi` und `CompanyManager.Console` referenziert. Es müssen folgende Packages im Projekt `CompanyManager.Logic` installiert werden:

- Microsoft.Extensions.Configuration.Json
- Microsoft.Extensions.Configuration.Binder
- Microsoft.Extensions.Configuration.EnvironmentVariables

## Erstellen der Klasse `AppSettings`

Im Projekt `CompanyManager.Logic` erstellen wir einen Ordner **Modules** und einen Unterordner **Configuration**. Anschließend erstellen wir in diesem Ordner eine Klasse mit dem Namen `AppSettings`. Diese Klasse wird als `Singleton` konzipiert. Die Umsetzung der Klasse ist wie folgt:

```csharp
namespace CompanyManager.Logic.Modules.Configuration
{
    /// <summary>
    /// Singleton class to manage application settings.
    /// </summary>
    public sealed class AppSettings : ISettings
    {
        #region fields
        private static AppSettings _instance = new AppSettings();
        private static IConfigurationRoot _configurationRoot;
        #endregion fields

        /// <summary>
        /// Static constructor to initialize the configuration.
        /// </summary>
        static AppSettings()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{environmentName ?? "Development"}.json", optional: true)
                    .AddEnvironmentVariables();

            _configurationRoot = builder.Build();
        }

        #region properties
        /// <summary>
        /// Gets the singleton instance of the AppSettings class.
        /// </summary>
        public static AppSettings Instance => _instance;
        #endregion properties

        /// <summary>
        /// Private constructor to prevent instantiation.
        /// </summary>
        private AppSettings()
        {
        }

        /// <summary>
        /// Indexer to get the configuration value by key.
        /// </summary>
        /// <param name="key">The configuration key.</param>
        /// <returns>The configuration value.</returns>
        public string? this[string key] => _configurationRoot[key];
    }
}
```

Damit diese Klasse auch in einen **Dependency Injection (DI)-Container** registriert werden kann, erstellen wir zu dieser Klasse eine Schnittstelle. Diese Schnittstelle befindet sich im Ordner **Contracts** bei den anderen Schnittstellen. Der Aufbau der Schnittstelle ist wie folgt definiert:

```csharp
namespace CompanyManager.Logic.Contracts
{
    public interface ISettings
    {
        string? this[string key] { get; }
    }
}
```

## Auslesen von AppSettings-Daten

Im `DataContext` werden die `AppSettings`-Daten ausgewertet. Dabei wird der Database-Typ, 'SqlServer' oder 'Sqlite', ausgewertet und der entsprechende 'ConnectString' geladen. Die Auswertung der Daten erfolgt im statischen Konstruktor der `DataContext`-Klasse:

```csharp
namespace CompanyManager.Logic.DataContext
{
    internal class CompanyContext : DbContext, IContext
    {
        #region fields
        private static string DatabaseType = "Sqlite";
        private static string ConnectionString = "data source=CompanyManager.db";
        #endregion fields

        static CompanyContext()
        {
            var appSettings = Modules.Configuration.AppSettings.Instance;

            DatabaseType = appSettings["Database:Type"] ?? DatabaseType;
            ConnectionString = appSettings[$"ConnectionStrings:{DatabaseType}ConnectionString"] ?? ConnectionString;
        }
        ...
    }
}
```

## Testen des Systems

- Testen Sie die REST-API mit dem Programm **Postman**. Ein `GET`-Request sieht wie folgt aus:

```bash
// In dieser Anfrage werden alle `Company`-Einträge im json-Format aufgelistet.
GET: https://localhost:7074/api/companies

// In dieser Anfrage werden alle `Customer`-Einträge zum `Company`-Eintrag geladen und im json-Format aufgelistet.
GET: https://localhost:7074/api/companies/13
```

Diese Anfrage listed alle `Company`-Einträge im json-Format auf.

## Hilfsmittel

- keine

## Abgabe

- Termin: 1 Woche nach der Ausgabe
- Klasse:
- Name:

## Quellen

- keine Angabe

> **Viel Erfolg!**
