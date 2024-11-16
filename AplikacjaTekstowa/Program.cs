using System;
using System.Collections.Generic;
using System.IO;
using AplikacjaTekstowa.Controllers;
using AplikacjaTekstowa.Views;

namespace AplikacjaTekstowa
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\kstyp\RiderProjects\AplikacjaTekstowa\AplikacjaTekstowa\books.json";
            BookRepository repository = new BookRepository(filePath);
            BookView view = new BookView();
            BookController controller = new BookController(repository, view);
            controller.Start();
        }
        
    }
}