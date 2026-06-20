# Opis projektu

Aplikacja umożliwia tworzenie i wypełnianie ankiet online, zapewniając prosty sposób zbierania opinii.

Ankieter może definiować pytania, zarządzać aktywnymi ankietami oraz monitorować liczbę oddanych głosów. Respondenci natomiast wypełniają ankiety w intuicyjnym interfejsie, a następnie mogą przeglądać wyniki zaprezentowane w formie wykresów.

---

## Technologie

- .NET 9
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

## Zrzuty ekranu z aplikacji

<img width="1250" height="752" alt="image" src="https://github.com/user-attachments/assets/9004bdfd-1f38-490b-92a4-123b95b40805" />
<img width="1253" height="757" alt="image" src="https://github.com/user-attachments/assets/eeb769c4-496f-4d69-8455-815be632099e" />
<img width="1252" height="754" alt="image" src="https://github.com/user-attachments/assets/7867f9f5-2c59-4351-a12f-c9e7859b0c00" />
<img width="1254" height="759" alt="image" src="https://github.com/user-attachments/assets/4e86366f-55a1-460c-a52e-60ca732ed2f5" />
<img width="1252" height="754" alt="image" src="https://github.com/user-attachments/assets/c557b201-8217-45c7-a239-0b6b19b5612f" />
<img width="1252" height="751" alt="image" src="https://github.com/user-attachments/assets/efdec491-7467-45c9-9b39-7627914a9704" />
