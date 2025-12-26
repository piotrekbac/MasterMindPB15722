#  Gra MasterMind - Projekt Dodatkowy


Projekt realizujący grę logiczną **MasterMind** wraz z zaawansowaną analizą kombinatoryczną i sztuczną inteligencją (solverem). Aplikacja została napisana w języku C#. 

**Autor:** Piotr Bacior  
**Nr indeksu:** 15 722  
**Uczelnia:** WSEI Kraków

---

## Realizacja Zadań 

Projekt spełnia wszystkie wymagania postawione w treści zadania (PDF), w tym zadania dodatkowe o podwyższonym stopniu trudności.

### Zadanie 1: Logika Gry i Interfejs
*   Pełna implementacja silnika gry w oddzielnej bibliotece klas (`MasterMind.Engine`).
*   Algorytm oceny kodu zwracający liczbę trafień dokładnych (czarne szpilki) i niedokładnych (białe szpilki).
*   Interaktywna konsola (CLI) wykorzystująca kolory do wizualizacji rozgrywki.

### Zadanie 2: Sztuczna Inteligencja (Solver)
*   Implementacja algorytmu zgadującego kod użytkownika.
*   Komputer wykorzystuje **strategię redukcji zbioru potencjalnych rozwiązań** (zgodnie z analizą matematyczną gry).
*   Wykrywanie niespójności/oszustwa (gdy użytkownik poda sprzeczne oceny).

### Zadanie 3: Parametryzacja (N i K)
*   Możliwość konfiguracji gry w menu:
    *   **Liczba kolorów ($n$):** Zakres 6-8.
    *   **Długość kodu ($k$):** Zakres 3-6.
*   Algorytm Solvera generuje drzewo rozwiązań **rekurencyjnie**, dzięki czemu działa dla dowolnych poprawnych $n$ i $k$.

### Zadanie 4: Tolerancja Błędów (Advanced Solver)
*   Zaimplementowano zaawansowany tryb, w którym **użytkownik może się pomylić (lub skłamać)** przy ocenie ruchu komputera.
*   Komputer analizuje historię gry i szuka kodu, który posiada **minimalną liczbę sprzeczności**.
*   Użytkownik może zdefiniować maksymalną liczbę dopuszczalnych pomyłek.

### Zadanie 5: Wariant Cyfrowy
*   Rozszerzenie gry o tryb cyfrowy ($n=10$, cyfry `0-9`).
*   Dynamiczne przełączanie alfabetu gry między kolorami (`r, y, g...`) a cyframi.

---

## Architektura Projektu

Rozwiązanie (Solution) składa się z następujących projektów:

1.  **`MasterMind.Engine`** (Class Library)
    *   Serce aplikacji. Zawiera klasy `Game` (logika zasad) oraz `ComputerSolver` (algorytmy SI - komputera).
    *   Kod jest niezależny od interfejsu użytkownika (może być użyty w Console, WPF).

2.  **`MasterMind.CLI`** (Console App)
    *   Warstwa prezentacji. Odpowiada za interakcję z użytkownikiem, menu główne oraz wizualizację wyników.

---

## Jak uruchomić projekt?

1.  Sklonuj repozytorium:
    ```bash
    git clone https://github.com/piotrekbac/MasterMindPB15722.git
    ```
2.  Otwórz plik `MasterMindPB15722.sln` w programie **Visual Studio 2022**.
3.  Ustaw projekt `MasterMind.CLI` jako projekt startowy (Prawy przycisk myszy -> *Set as Startup Project*).
4.  Uruchom aplikację klawiszem `F5`.

---

## Instrukcja obsługi

Po uruchomieniu programu zobaczysz menu główne. Sterowanie odbywa się poprzez wybór odpowiednich cyfr na klawiaturze:

*   **1. Nowa gra (Człowiek vs Komputer):** Ty zgadujesz kod wylosowany przez komputer.
*   **2. Nowa gra (Komputer vs Człowiek):** Wymyślasz kod, a komputer próbuje go odgadnąć.
*   **3. Ustawienia gry:** Tutaj możesz zmienić liczbę kolorów/cyfr, długość kodu oraz włączyć **tryb cyfrowy** i **tryb tolerancji błędów**.

---
