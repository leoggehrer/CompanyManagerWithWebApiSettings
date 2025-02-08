# CompanyManager With WebApi and AppSettings

**Lernziele:**

- Wie die AppSettings in den Projekten `CompanyManager.WebApi` und `CompanyManager.Console` verwendet werden.

**Hinweis:** Als Startpunkt wird die Vorlage [CompanyManagerWithWebApi](https://github.com/leoggehrer/CompanyManagerWithWebApiGeneric) verwendet.

## Vorbereitung

Bevor mit der Umsetzung begonnen wird, sollte die Vorlage heruntergeladen und die Funktionalität verstanden werden.

### Testen des Systems

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
