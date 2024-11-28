# HangmanGame

Ett spel där man får gissa för att hitta ett hemligt ord.

## Innehåll & Funktioner

- **Spelarstatistik:** Spelet håller koll på statistik som spelade och vunna matcher.
- **Leaderboard:** En funktion för att visa en rangordning av spelare baserat på vinstprocent.
- **Lägg till nya spelare:** Möjlighet att lägga till nya spelare till systemet.
- **Sparar data:** Ord och spelare samt deras statistik sparas och laddas till och från en JSON-fil.

## Användning

Välj ett alternativ i huvudmenyn:
    - **Spela** – Starta ett nytt spel.
    - **Visa spelarstatistik** – Se en rangordning av spelare baserat på vinstprocent.
    - **Avsluta** – Stäng programmet.

När spelet startar får du välja en spelare eller skapa en ny. Sedan gäller det att gissa ordet innan antalet felaktiga gissningar når sitt maximum.

## Teknologi

- **Programmeringsspråk:** C#
- **Bibliotek:** [Spectre.Console](https://spectre.console) för terminalbaserad UI.
- **Datahantering:** JSON-filer för att spara och läs
