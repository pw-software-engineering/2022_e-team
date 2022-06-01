# ECaterer

## Stan projektu na 05.04.2022

* Dodane mockup'y widoków aplikacji klienta, producenta oraz dostawcy
* Stworzony projekt do bazy danych ECaterer.Core z modelami oraz migracjami
* Stworzony projekt do front-end'u ECaterer.Web
* Stworzony project do testowania ECaterer.Web.Test z testami do api klienta (do tego zostały stworzone niezaimplementowane funkcje kontrolera klienta i klasy DTO)
* Stworzony bazowy pipeline

## Dostęp do aplikacji
Aplikacja dostępna jest pod adresem: http://eteam.06c45401340e40f5913f.westeurope.aksapp.io/

## Budowanie bazy i migracje

## Odpalenie docker composa
W głównym folderze projektu wywołać komendę `docker-compose up`

### Local development

Aktualizacja bazy do ostatniej migracji:

```
dotnet ef database update
```

Aktualizacja bazy do danej migracji (ewentualnie rollback):

```
dotnet ef database update [migration]
```

### Generacja skryptu SQL (dla "production database")

Od pustej bazy do ostatniej migracji:

```
dotnet ef migrations script
```

Od danej migracji do ostatniej migracji:

```
dotnet ef migrations script [migration]
```

Od migracji "from" bazy do migracji "to" (jeśli migracja "from" będzie nowsza od migracji "to", to zostanie wygenerowany skrypt rollback):

```
dotnet ef migrations script [migrationFrom] [migrationTo]
```
