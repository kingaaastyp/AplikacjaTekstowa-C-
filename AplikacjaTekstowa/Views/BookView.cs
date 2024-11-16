using System;
using System.Collections.Generic;
using AplikacjaTekstowa.Models;

namespace AplikacjaTekstowa.Views
{
    public class BookView
    {
        public void DisplayMenu()
        {
            Console.WriteLine("\n=== Menu ===");
            Console.WriteLine("1. Wyświetl wszystkie książki");
            Console.WriteLine("2. Wyszukaj książkę po tytule");
            Console.WriteLine("3. Wyszukaj książkę po autorze");
            Console.WriteLine("4. Dodaj nową książkę");
            Console.WriteLine("5. Usuń książkę");
            Console.WriteLine("6. Edytuj książkę");
            Console.WriteLine("7. Wyjdź");
        }

        public string GetInput(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        public void DisplayBooks(List<Book> books)
        {
            if (books.Count == 0)
            {
                Console.WriteLine("Brak książek.");
                return;
            }

            foreach (var book in books)
            {
                Console.WriteLine($"Tytuł: {book.Title}, Autor: {book.Author}");
            }
        }

        public Book PromptForBookDetails()
        {
            string title = GetInput("Podaj tytuł: ");
            string author = GetInput("Podaj autora: ");
            string genre = GetInput("Podaj gatunek: ");
            int pageCount = int.Parse(GetInput("Podaj ilość stron: "));
            int year = int.Parse(GetInput("Podaj rok wydania: "));
            string description = GetInput("Podaj opis: ");
            return new Book { Title = title, Author = author, Genre = genre, PageCount = pageCount, Year = year, Description = description };
        }
        public int DisplayMenuWithNavigation()
        {
            string[] menuOptions = {
                "1. Wyświetl wszystkie książki",
                "2. Wyszukaj książki po tytule",
                "3. Wyszukaj książki po autorze",
                "4. Dodaj nową książkę",
                "5. Usuń książkę",
                "6. Edytuj książkę",
                "7. Wyjdź"
            };
            int selectedOption = 0;
            bool selecting = true;

            while (selecting)
            {
                Console.Clear();

                Console.WriteLine("=== Menu ===");
                for (int i = 0; i < menuOptions.Length; i++)
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

    while (true)
    {
        Console.Clear();
        Console.WriteLine("=== Dodawanie nowej książki ===");
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

        Console.WriteLine("\nUżyj strzałek do poruszania się po polach, Enter, aby edytować, lub F2, aby zakończyć.");

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
                Console.Write($"{fields[selectedIndex]}: ");
                values[selectedIndex] = Console.ReadLine();
                break;

            case ConsoleKey.F2:
                // Sprawdzenie, czy wszystkie pola są uzupełnione
                if (values.Any(string.IsNullOrEmpty))
                {
                    Console.WriteLine("Nie wszystkie pola zostały uzupełnione. Uzupełnij je przed zakończeniem.");
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
                Console.Write($"{fields[selectedIndex]}: ");
                values[selectedIndex] = Console.ReadLine();
                break;

            case ConsoleKey.F2:
                // Sprawdzenie, czy wszystkie pola są poprawne
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
            Console.WriteLine("=== Szczegóły Książki ===\n");
            Console.WriteLine($"Tytuł:        {book.Title}");
            Console.WriteLine($"Autor:        {book.Author}");
            Console.WriteLine($"Gatunek:      {book.Genre}");
            Console.WriteLine($"Liczba stron: {book.PageCount}");
            Console.WriteLine($"Rok wydania:  {book.Year}");
            Console.WriteLine($"Opis:         {book.Description}\n");
            Console.WriteLine("=========================");
            return ConfirmAction("Czy chcesz edytować tę książkę?");

            Console.ReadKey();
        }

    }
}
