# SaveUp

SaveUp ist eine Blazor-Webanwendung zum Erfassen von Verzichtskäufen für ein persönliches Sparziel.

## Funktionen

- mindestens drei Seiten: Übersicht, Eintrag erfassen, Sparliste, plus Mock-up-Seite
- Erfassung von Kurzbeschreibung, Preis und Datum/Uhrzeit
- Aktionen zum Speichern und Öffnen der Listendarstellung
- Anzeige der Gesamteinsparung
- strukturierter Aufbau mit Models, ViewModels und Persistenz-Service
- lokale Persistenz in `App_Data/saveup-entries.json`
- eigenes App-Icon über `wwwroot/favicon.svg`

## Starten

```bash
dotnet run
```

Die Anwendung startet lokal und speichert neue Einträge serverseitig in der JSON-Datei unter `App_Data/`.