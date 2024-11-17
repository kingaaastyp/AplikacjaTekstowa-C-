using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using AplikacjaTekstowa.Models;

namespace AplikacjaTekstowa
{
    public class BookRepository
    {
        private List<Book> books;
        private readonly string filePath;

        public BookRepository(string filePath)
        {
            this.filePath = filePath;
            books = LoadBooksFromFile();
        }

        private List<Book> LoadBooksFromFile()
        {
            try
            {
                if (!File.Exists(filePath)) return new List<Book>();
                string json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<List<Book>>(json) ?? new List<Book>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas ładowania pliku: {ex.Message}");
                return new List<Book>();
            }
        }

        public void SaveBooksToFile()
        {
            try
            {
                string json = JsonSerializer.Serialize(books, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, json);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas zapisu pliku: {ex.Message}");
            }

        }

        public List<Book> GetBooks() => books;

        public void AddBook(Book book)
        {
            books.Add(book);
            SaveBooksToFile();
        }

        public void UpdateBook(Book oldBook, Book updatedBook)
        {
            var index = books.IndexOf(oldBook);
            if (index != -1)
            {
                books[index] = updatedBook;
                SaveBooksToFile();
            }
        }

        public void RemoveBook(Book book)
        {
            books.Remove(book);
            SaveBooksToFile();
        }

        public List<Book> SearchByTitle(string title) =>
            books.Where(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase)).ToList();

        public List<Book> SearchByAuthor(string author) 
        {
            return books.Where(b => b.Author.Equals(author, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        public List<Book> FilterByAuthor(string author)
        {
            return books.Where(b => b.Author.Equals(author, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public void SortBooksByTitle() => books = books.OrderBy(b => b.Title).ToList();

        public void SortBooksByAuthor() => books = books.OrderBy(b => b.Author).ToList();
    }
}