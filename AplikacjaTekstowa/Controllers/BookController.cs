using AplikacjaTekstowa.Models;
using AplikacjaTekstowa.Views;
using System;
using System.Collections.Generic;

namespace AplikacjaTekstowa.Controllers
{
    public class BookController
    {
        private readonly BookRepository repository;
        private readonly BookView view;

        public BookController(BookRepository repository, BookView view)
        {
            this.repository = repository;
            this.view = view;
        }

        public void Start()
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                int choice = view.DisplayMenuWithNavigation();

                switch (choice)
                {
                    case 1:
                        List<Book> allBooks = repository.GetBooks();
                        view.DisplayBooksWithDetailsOption(allBooks); 
                        break;
                    case 2:
                        string title = view.GetInput("Podaj tytuł: ");
                        List<Book> booksByTitle = repository.SearchByTitle(title);
                        view.DisplayBooksWithDetailsOption(booksByTitle);
                        break;
                    case 3:
                        string author = view.GetInput("Podaj autora: ");
                        List<Book> booksByAuthor = repository.SearchByAuthor(author);
                        view.DisplayBooksWithDetailsOption(booksByAuthor);
                        break;
                    case 4:
                        var newBook = view.PromptForBookDetailsInteractive();
                        var existingBook = repository.GetBooks()
                            .Find(b => b.Title.Equals(newBook.Title, StringComparison.OrdinalIgnoreCase) &&
                                       b.Author.Equals(newBook.Author, StringComparison.OrdinalIgnoreCase) &&
                                       b.Year == newBook.Year);

                        if (existingBook != null)
                        {
                            if (view.ConfirmAction("Książka o podanym tytule, autorze i roku wydania już istnieje. \nCzy chcesz wyświetlić szczegóły tej książki?"))
                            {
                                view.DisplayBookDetails(existingBook);
                            }
                        }
                        else
                        {
                            repository.AddBook(newBook);
                            Console.WriteLine("Dodano książkę.");
                        }
                        break;
                    case 5:
                        string titleToRemove = view.GetInput("Podaj tytuł książki do usunięcia: ");
                        string authorToRemove = view.GetInput("Podaj autora książki do usunięcia: ");

                        var bookToRemove = repository.GetBooks().Find(b => b.Title.Equals(titleToRemove, StringComparison.OrdinalIgnoreCase)
                                                                           && b.Author.Equals(authorToRemove, StringComparison.OrdinalIgnoreCase));
                        if (bookToRemove != null)
                        {
                            bool confirmDeletion = view.ConfirmAction($"Czy na pewno chcesz usunąć książkę \"{bookToRemove.Title}\" autorstwa {bookToRemove.Author}?");
                            if (confirmDeletion)
                            {
                                repository.RemoveBook(bookToRemove);
                                Console.WriteLine("Usunięto książkę.");
                            }
                            else
                            {
                                Console.WriteLine("Anulowano usuwanie książki.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Nie znaleziono książki o podanym tytule i autorze.");
                        }
                        break;
                  
                    case 6: // Edytowanie książki
                        string titleToEdit = view.GetInput("Podaj tytuł książki do edycji: ");
                        var bookToEdit = repository.GetBooks()
                            .Find(b => b.Title.Equals(titleToEdit, StringComparison.OrdinalIgnoreCase));

                        if (bookToEdit != null)
                        {
                            // Wyświetlenie szczegółów książki i pytanie o edycję
                            if (view.DisplayBookDetails(bookToEdit))
                            {
                                var updatedBook = view.EditBookDetailsInteractive(bookToEdit);
                                repository.UpdateBook(bookToEdit, updatedBook);
                                Console.WriteLine($"\nKsiążka \"{updatedBook.Title}\" autorstwa {updatedBook.Author} została pomyślnie zaktualizowana i zapisana.");
                            }
                            else
                            {
                                Console.WriteLine("Edycja książki została anulowana.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Nie znaleziono książki o podanym tytule.");
                        }
                        break;

                    
                    case 7: // Wyjdź
                        Console.WriteLine("Wyjście z aplikacji.");
                        running = false;
                        return;
                    case 8:
                        Console.WriteLine("Sortowanie...");
                        int sortChoice = view.DisplaySortOptions();
                        List<Book> sortedBooks = ApplySort(sortChoice, repository.GetBooks());
                        view.DisplayBooks(sortedBooks);                        break;
                    case 9:
                        Console.WriteLine("Filtracja...");
                        int filterChoice = view.DisplayFilterOptions();
                        List<Book> filteredBooks = ApplyFilter(filterChoice);
                        view.DisplayBooks(filteredBooks);                        break;
                    case 10:
                        Console.WriteLine("Wyświetlanie szczegółów...");
                        // Dodaj logikę wyświetlania szczegółów, jeśli potrzebujesz
                        break;
                   
                    default:
                        Console.WriteLine("Nieprawidłowa opcja.");
                        break;
                }

                if (choice != 6)
                {
                    Console.WriteLine("Naciśnij dowolny klawisz, aby kontynuować...");
                    Console.ReadKey();
                }
            }
        }

        private void DisplayBooksWithFilterAndSort()
        {
            // Wybierz filtr
            int filterChoice = view.DisplayFilterOptions();
            List<Book> books = ApplyFilter(filterChoice);

            // Wybierz sortowanie
            int sortChoice = view.DisplaySortOptions();
            books = ApplySort(sortChoice, books);

            // Wyświetl książki po filtrowaniu i sortowaniu
            view.DisplayBooks(books);
        }

        private List<Book> ApplyFilter(int filterChoice)
        {
            switch (filterChoice)
            {
                case 1:
                    string author = view.GetInput("Podaj autora do filtrowania: ");
                    return repository.FilterByAuthor(author);
                case 2:
                    string genre = view.GetInput("Podaj gatunek do filtrowania: ");
                    return repository.GetBooks().Where(b => b.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                default:
                    return repository.GetBooks();
            }
        }

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
    }
}