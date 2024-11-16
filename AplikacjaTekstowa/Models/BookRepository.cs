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
            if (!File.Exists(filePath)) return new List<Book>();
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Book>>(json) ?? new List<Book>();
        }

        public void SaveBooksToFile()
        {
            try
            {
                Console.WriteLine($"Zapisuję książki do pliku: {Path.GetFullPath(filePath)}"); // Ścieżka pełna

                string json = JsonSerializer.Serialize(books, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, json);

                Console.WriteLine("Zapisano książki do pliku.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
            books.Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();

        public List<Book> SearchByAuthor(string author) =>
            books.Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase)).ToList();

        public List<Book> FilterByAuthor(string author)
        {
            return books.Where(b => b.Author.Equals(author, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public void SortBooksByTitle() => books = books.OrderBy(b => b.Title).ToList();

        public void SortBooksByAuthor() => books = books.OrderBy(b => b.Author).ToList();
    }
}