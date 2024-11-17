using System;
using System.Collections.Generic;
using AplikacjaTekstowa.Models;

namespace AplikacjaTekstowa.Views
{
    public class BookView
    {
        
        public void DisplayHeader()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"
   █████╗ ██╗     ██╗   ██╗██████╗ ███╗   ███╗      ██╗  ██╗███████╗██╗ ██████╗ ███████╗██╗  ██╗
  ██╔══██╗██║     ██║   ██║██╔══██╗████╗ ████║      ██║  ██║██╔════╝██║██╔════╝ ██╔════╝██║ ██╔╝
  ███████║██║     ██║   ██║██████╔╝██╔████╔██║█████╗███████║███████╗██║██║  ███╗█████╗  █████╔╝ 
  ██╔══██║██║     ██║   ██║██╔═══╝ ██║╚██╔╝██║╚════╝██╔══██║╚════██║██║██║   ██║██╔══╝  ██╔═██╗ 
  ██║  ██║███████╗╚██████╔╝██║     ██║ ╚═╝ ██║      ██║  ██║███████║██║╚██████╔╝███████╗██║  ██╗
  ╚═╝  ╚═╝╚══════╝ ╚═════╝ ╚═╝     ╚═╝     ╚═╝      ╚═╝  ╚═╝╚══════╝╚═╝ ╚═════╝ ╚══════╝╚═╝  ╚═╝
            ");
            Console.ResetColor();
        }

        public string GetInput(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        public void DisplayBooks(List<Book> books)
        {
            Console.Clear();
            if (books.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Brak książek.");
                Console.ResetColor();

                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n=== Lista książek ===");
            foreach (var book in books)
            {
                Console.WriteLine("-----------------------------");
                Console.WriteLine($"Tytuł: {book.Title}");
                Console.WriteLine($"Autor: {book.Author}");
                Console.WriteLine($"Gatunek: {book.Genre}");
                Console.WriteLine($"Liczba stron: {book.PageCount}");
                Console.WriteLine($"Rok wydania: {book.Year}");
                Console.WriteLine($"Opis: {book.Description}");
                Console.WriteLine("-----------------------------");
                
            }
            Console.ResetColor();

        }

        public void ShowReturningAnimation(string message)
        {
            Console.Write($"{message} ");
            for (int i = 0; i < 3; i++)
            {
                Console.Write(".");
                System.Threading.Thread.Sleep(300);
            }
            Console.WriteLine("\n");
        }

        public void LoadingAnimation(string message)
        {
            Console.Write(message);
            for (int i = 0; i < 3; i++)
            {
                Console.Write(".");
                System.Threading.Thread.Sleep(500);
            }
            Console.WriteLine();
        }
        public Book PromptForBookDetails()
        {
            string title = GetValidatedStringInput("Podaj tytuł: ");
            string author = GetValidatedStringInput("Podaj autora: ");
            string genre = GetValidatedStringInput("Podaj gatunek: ");
            int pageCount = GetValidatedIntInput("Podaj ilość stron: ");
            int year = GetValidatedIntInput("Podaj rok wydania: ");
            string description = GetValidatedStringInput("Podaj opis: ");

            return new Book
            {
                Title = title,
                Author = author,
                Genre = genre,
                PageCount = pageCount,
                Year = year,
                Description = description
            };
        }

        public int DisplayMenuWithNavigation()
        {
            string[] menuOptions = {
                "1. Wyświetl wszystkie książki",
                "2. Wyszukaj książkę po tytule",
                "3. Wyszukaj książkę po autorze",
                "4. Dodaj nową książkę",
                "5. Usuń książkę",
                "6. Edytuj książkę",
                "7. Wyjdź",
                "\n============================"
            };
            int selectedOption = 0;
            bool selecting = true;

            while (selecting)
            {
                Console.Clear();

                Console.WriteLine("\n============================");
                Console.WriteLine("         MENU");
                Console.WriteLine("============================\n");                for (int i = 0; i < menuOptions.Length; i++)
                {
                    if (i == selectedOption)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.WriteLine(menuOptions[i]);
                    Console.ResetColor();
                }

                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selectedOption = (selectedOption == 0) ? menuOptions.Length - 1 : selectedOption - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedOption = (selectedOption == menuOptions.Length - 1) ? 0 : selectedOption + 1;
                        break;
                    case ConsoleKey.Enter:
                        selecting = false;
                        break;
                }
            }

            return selectedOption + 1; // Zwraca wybór użytkownika, numerując opcje od 1
        }
        
        private readonly string[] filterOptions = {
            "Filtruj: brak",
            "Filtruj: autor",
            "Filtruj: gatunek"
        };

        private readonly string[] sortOptions = {
            "Sortuj: brak",
            "Sortuj: autor (rosnąco)",
            "Sortuj: autor (malejąco)",
            "Sortuj: tytuł (rosnąco)",
            "Sortuj: tytuł (malejąco)"
        };

        public int DisplayFilterOptions()
        {
            int selectedIndex = 0;
            bool selecting = true;

            while (selecting)
            {
                Console.Clear();
                Console.WriteLine("=== Opcje filtrowania ===\n");

                for (int i = 0; i < filterOptions.Length; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    Console.WriteLine(filterOptions[i]);
                    Console.ResetColor();
                }

                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = (selectedIndex == 0) ? filterOptions.Length - 1 : selectedIndex - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = (selectedIndex == filterOptions.Length - 1) ? 0 : selectedIndex + 1;
                        break;
                    case ConsoleKey.Enter:
                        selecting = false;
                        break;
                }
            }

            return selectedIndex;
        }

        
        public int DisplaySortOptions()
        {
            int selectedIndex = 0;
            bool selecting = true;

            while (selecting)
            {
                Console.Clear();
                Console.WriteLine("=== Opcje sortowania ===\n");

                for (int i = 0; i < sortOptions.Length; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    Console.WriteLine(sortOptions[i]);
                    Console.ResetColor();
                }

                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = (selectedIndex == 0) ? sortOptions.Length - 1 : selectedIndex - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = (selectedIndex == sortOptions.Length - 1) ? 0 : selectedIndex + 1;
                        break;
                    case ConsoleKey.Enter:
                        selecting = false;
                        break;
                }
            }

            return selectedIndex;
        }
        
        
       public void DisplayBooksWithDetailsOption(List<Book> books)
{
    if (books.Count == 0)
    {
        Console.WriteLine("Brak książek do wyświetlenia.");
        Console.WriteLine("Naciśnij dowolny klawisz, aby powrócić do Menu...");
        Console.ReadKey();
        return;
    }

    // Wybierz filtr i zastosuj go
    int filterChoice = DisplayFilterOptions();
    books = ApplyFilter(filterChoice, books);

    // Wybierz sortowanie i zastosuj je
    int sortChoice = DisplaySortOptions();
    books = ApplySort(sortChoice, books);

    int selectedIndex = 0;
    bool selecting = true;

    while (selecting)
    {
        Console.Clear();
        Console.WriteLine("=== Lista Książek ===\n");

        // Wyświetlanie listy książek po filtrowaniu i sortowaniu
        for (int i = 0; i < books.Count; i++)
        {
            if (i == selectedIndex)
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine($"{books[i].Title} - {books[i].Author}");
            Console.ResetColor();
        }

        // Wyświetlanie opcji "Wróć" jako ostatni element listy
        if (selectedIndex == books.Count) // books.Count jest indeksem opcji "Wróć"
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
        }
        Console.WriteLine("Wróć");
        Console.ResetColor();

        Console.WriteLine("\nNaciśnij Enter, aby wyświetlić szczegóły wybranej książki, lub wybierz 'Wróć', aby powrócić do Menu.");

        var key = Console.ReadKey(true).Key;

        switch (key)
        {
            case ConsoleKey.UpArrow:
                selectedIndex = (selectedIndex == 0) ? books.Count : selectedIndex - 1;
                break;
            case ConsoleKey.DownArrow:
                selectedIndex = (selectedIndex == books.Count) ? 0 : selectedIndex + 1;
                break;
            case ConsoleKey.Enter:
                if (selectedIndex == books.Count)
                {
                    // Użytkownik wybrał opcję "Wróć"
                    selecting = false;
                }
                else
                {
                    // Wyświetla szczegóły wybranej książki
                    DisplayBookDetails(books[selectedIndex]);
                    Console.WriteLine("\nNaciśnij dowolny klawisz, aby wrócić do listy książek...");
                    Console.ReadKey(); 
                }
                break;
        }
    }
}

// Metoda pomocnicza do filtrowania
private List<Book> ApplyFilter(int filterChoice, List<Book> books)
{
    switch (filterChoice)
    {
        case 1:
            string author = GetInput("Podaj autora do filtrowania: ");
            return books.Where(b => b.Author.Equals(author, StringComparison.OrdinalIgnoreCase)).ToList();
        case 2:
            string genre = GetInput("Podaj gatunek do filtrowania: ");
            return books.Where(b => b.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase)).ToList();
        default:
            return books;
    }
}

// Metoda pomocnicza do sortowania
private List<Book> ApplySort(int sortChoice, List<Book> books)
{
    switch (sortChoice)
    {
        case 1:
            return books.OrderBy(b => b.Author).ToList();
        case 2:
            return books.OrderByDescending(b => b.Author).ToList();
        case 3:
            return books.OrderBy(b => b.Title).ToList();
        case 4:
            return books.OrderByDescending(b => b.Title).ToList();
        default:
            return books;
    }
    
    
}

public bool ConfirmAction(string message)
{
    string[] options = { "Tak", "Nie" };
    int selectedIndex = 0;

    while (true)
    {
        Console.Clear();
        Console.WriteLine(message);

        for (int i = 0; i < options.Length; i++)
        {
            if (i == selectedIndex)
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine(options[i]);
            Console.ResetColor();
        }

        var key = Console.ReadKey(true).Key;

        switch (key)
        {
            case ConsoleKey.UpArrow:
                selectedIndex = (selectedIndex == 0) ? options.Length - 1 : selectedIndex - 1;
                break;
            case ConsoleKey.DownArrow:
                selectedIndex = (selectedIndex == options.Length - 1) ? 0 : selectedIndex + 1;
                break;
            case ConsoleKey.Enter:
                return selectedIndex == 0; // Zwraca true dla "Tak", false dla "Nie".
        }
    }
}
public Book PromptForBookDetailsInteractive()
{
    string[] fields = { "Tytuł", "Autor", "Gatunek", "Liczba stron", "Rok wydania", "Opis" };
    string[] values = new string[fields.Length];
    int selectedIndex = 0;
    bool isOnOptions = false; // Flaga do przełączania między polami i opcjami

    while (true)
    {
        Console.Clear();
        Console.WriteLine("=== Dodawanie nowej książki ===");

        // Wyświetlanie pól do edycji
        for (int i = 0; i < fields.Length; i++)
        {
            if (i == selectedIndex && !isOnOptions)
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.White;
            }
            string displayValue = string.IsNullOrEmpty(values[i]) ? "(puste)" : values[i];
            Console.WriteLine($"{fields[i]}: {displayValue}");
            Console.ResetColor();
        }

        Console.WriteLine("\nNaciśniej tab, aby wybierać opcję poniżej (Użyj strzałek w lewo/prawo oraz Enter aby potwierdzić):");

        // Wyświetlanie opcji "Anuluj" i "Dodaj"
        if (isOnOptions && selectedIndex == fields.Length) // "Anuluj" zaznaczone
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[ Anuluj ]");
            Console.ResetColor();
            Console.Write("   Dodaj");
        }
        else if (isOnOptions && selectedIndex == fields.Length + 1) // "Dodaj" zaznaczone
        {
            Console.Write(" Anuluj   ");
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[ Dodaj ]");
            Console.ResetColor();
        }
        else
        {
            Console.Write(" Anuluj   Dodaj");
        }

        Console.WriteLine();

        var key = Console.ReadKey(true).Key;

        switch (key)
        {
            case ConsoleKey.UpArrow:
                if (!isOnOptions) // Przesuwanie między polami
                    selectedIndex = (selectedIndex == 0) ? fields.Length - 1 : selectedIndex - 1;
                break;

            case ConsoleKey.DownArrow:
                if (!isOnOptions) // Przesuwanie między polami
                    selectedIndex = (selectedIndex == fields.Length - 1) ? 0 : selectedIndex + 1;
                break;

            case ConsoleKey.LeftArrow:
                if (isOnOptions)
                    selectedIndex = fields.Length; // Zaznacz "Anuluj"
                break;

            case ConsoleKey.RightArrow:
                if (isOnOptions)
                    selectedIndex = fields.Length + 1; // Zaznacz "Dodaj"
                break;

            case ConsoleKey.Enter:
                if (isOnOptions)
                {
                    if (selectedIndex == fields.Length) // Wybrano "Anuluj"
                    {
                        Console.WriteLine("Dodawanie książki zostało anulowane.");
                        return null;
                    }
                    else if (selectedIndex == fields.Length + 1) // Wybrano "Dodaj"
                    {
                        // Sprawdzenie kompletności pól
                        if (values.Any(string.IsNullOrEmpty))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Nie wszystkie pola zostały uzupełnione. Uzupełnij je przed zakończeniem.");
                            Console.ResetColor();
                            Console.ReadKey();
                        }
                        else
                        {
                            return new Book
                            {
                                Title = values[0],
                                Author = values[1],
                                Genre = values[2],
                                PageCount = int.Parse(values[3]),
                                Year = int.Parse(values[4]),
                                Description = values[5]
                            };
                        }
                    }
                }
                else // Edytowanie pola
                {
                    Console.Clear();
                    Console.Write($"{fields[selectedIndex]}: ");
                    if (selectedIndex == 1 || selectedIndex == 2) // Autor i Gatunek
                    {
                        values[selectedIndex] = GetValidatedStringInput($"Podaj wartość dla {fields[selectedIndex]}: ", true);
                    }
                    else if (selectedIndex == 3 || selectedIndex == 4) // Liczba stron i Rok wydania
                    {
                        values[selectedIndex] = GetValidatedIntInput($"Podaj wartość dla {fields[selectedIndex]}: ").ToString();
                    }
                    else // Tytuł i Opis
                    {
                        values[selectedIndex] = GetValidatedStringInput($"Podaj wartość dla {fields[selectedIndex]}: ");
                    }
                }
                break;

            case ConsoleKey.Tab:
                isOnOptions = !isOnOptions; // Przełącz na opcje "Dodaj/Anuluj"
                selectedIndex = fields.Length; // Ustaw na "Anuluj"
                break;

            case ConsoleKey.Escape:
                isOnOptions = false; // Powrót do edycji pól
                break;
        }
    }
}

          
           


public string GetValidatedStringInput(string prompt, bool allowOnlyLetters = false)
{
    while (true)
    {
        Console.Write(prompt);
        string input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Pole nie może być puste. Spróbuj ponownie.");
            continue;
        }

        if (allowOnlyLetters && !input.All(char.IsLetter))
        {
            Console.WriteLine("Dozwolone są tylko litery. Spróbuj ponownie.");
            continue;
        }

        return input;
    }
}

public int GetValidatedIntInput(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        string input = Console.ReadLine();

        if (int.TryParse(input, out int value) && value > 0)
        {
            return value;
        }

        Console.WriteLine("Podano nieprawidłową wartość. Wprowadź liczbę całkowitą większą od 0.");
    }
}



public Book EditBookDetailsInteractive(Book bookToEdit)
{
    string[] fields = { "Tytuł", "Autor", "Gatunek", "Liczba stron", "Rok wydania", "Opis" };
    string[] values = {
        bookToEdit.Title,
        bookToEdit.Author,
        bookToEdit.Genre,
        bookToEdit.PageCount.ToString(),
        bookToEdit.Year.ToString(),
        bookToEdit.Description
    };

    int selectedIndex = 0;

    while (true)
    {
        Console.Clear();
        Console.WriteLine("=== Edytowanie Książki ===");
        for (int i = 0; i < fields.Length; i++)
        {
            if (i == selectedIndex)
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.White;
            }

            string displayValue = string.IsNullOrEmpty(values[i]) ? "(puste)" : values[i];
            Console.WriteLine($"{fields[i]}: {displayValue}");
            Console.ResetColor();
        }

        Console.WriteLine("\nUżyj strzałek do poruszania się po polach, Enter, aby edytować, lub F2, aby zakończyć edycję.");

        var key = Console.ReadKey(true).Key;

        switch (key)
        {
            case ConsoleKey.UpArrow:
                selectedIndex = (selectedIndex == 0) ? fields.Length - 1 : selectedIndex - 1;
                break;

            case ConsoleKey.DownArrow:
                selectedIndex = (selectedIndex == fields.Length - 1) ? 0 : selectedIndex + 1;
                break;

            case ConsoleKey.Enter:
                Console.Clear();
                if (selectedIndex == 1 || selectedIndex == 2) // Autor i Gatunek
                {
                    values[selectedIndex] = GetValidatedStringInput($"Podaj wartość dla {fields[selectedIndex]}: ", true);
                }
                else if (selectedIndex == 3 || selectedIndex == 4) // Liczba stron i Rok wydania
                {
                    values[selectedIndex] = GetValidatedIntInput($"Podaj wartość dla {fields[selectedIndex]}: ").ToString();
                }
                else // Tytuł i Opis
                {
                    values[selectedIndex] = GetValidatedStringInput($"Podaj wartość dla {fields[selectedIndex]}: ");
                }
                break;

            case ConsoleKey.F2: // zakonczenie edytowania
                if (values.Any(string.IsNullOrEmpty))
                {
                    Console.WriteLine("Nie wszystkie pola zostały uzupełnione. Uzupełnij je przed zakończeniem edycji.");
                    Console.ReadKey();
                }
                else
                {
                    return new Book
                    {
                        Title = values[0],
                        Author = values[1],
                        Genre = values[2],
                        PageCount = int.Parse(values[3]),
                        Year = int.Parse(values[4]),
                        Description = values[5]
                    };
                }
                break;
        }
    }
}

        
        public void DisplayBookDetails(Book book)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("═══════════════════════════════════════════════════════════════════");
            Console.WriteLine($"                       Szczegóły Książki                        ");
            Console.WriteLine("═══════════════════════════════════════════════════════════════════");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Tytuł: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(book.Title);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Autor: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(book.Author);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Gatunek: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(book.Genre);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Liczba stron: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(book.PageCount);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Rok wydania: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(book.Year);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Opis: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(book.Description);

            // Stopka
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("═══════════════════════════════════════════════════════════════════");
            Console.ResetColor();

            // Informacja o powrocie
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\nNaciśnij dowolny klawisz, aby wrócić do listy książek...");
            Console.ResetColor();
            Console.ReadKey();
        }

        public bool AskToEditBook()
        {
            return ConfirmAction(("Czy chcesz edytować tę książkę?"));
        }

    }
}
