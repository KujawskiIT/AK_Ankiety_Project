# Opis projektu

Aplikacja webowa do tworzenia i wypełniania ankiet online. Ankieter zakłada ankiety,
respondenci oddają głosy i przeglądają wyniki w formie wykresu słupkowego.

---

## Technologie

- .NET 8
- ASP.NET Core MVC
- Entity Framework Core + SQLite (baza tworzona automatycznie przy pierwszym uruchomieniu)
- Microsoft Identity (użytkownicy i role)

## Konta testowe (tworzone automatycznie)

Baza danych SQLite (ankiety.db) oraz role i konta testowe tworzą się automatycznie
przy pierwszym starcie aplikacji:
- ankieter@test.pl (password: Haslo123!)
- respondent@test.pl (password: Haslo123!)

Dodatkowo, można założyć konto przez funkcjonalność Rejestracji (domyślnie rola to respondent)

## Struktura projektu

- `Models/` – modele danych (Survey, Option, Vote, ApplicationUser)
- `Data/` – kontekst EF Core oraz inicjalizacja ról i kont testowych
- `Controllers/` – logika (konta, ankiety, strona główna)
- `Views/` – widoki Razor
- `wwwroot/css/site.css` – style i wykres słupkowy