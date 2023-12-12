using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class BookController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        private const string FilePath = "books.json";

        public IActionResult Index()
        {
            List<Book> books = ReadBooksFromFile();
            return View(books);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {

            List<Book> books = ReadBooksFromFile();
            books.Add(book);
            SaveBooksToFile(books);
            return RedirectToAction("Index");
        }

        private List<Book> ReadBooksFromFile()
        {
            if (System.IO.File.Exists(FilePath))
            {
                string json = System.IO.File.ReadAllText(FilePath);
                return JsonSerializer.Deserialize<List<Book>>(json);
            }
            return new List<Book>();
        }

        private void SaveBooksToFile(List<Book> books)
        {
            string json = JsonSerializer.Serialize(books);
            System.IO.File.WriteAllText(FilePath, json);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            List<Book> books = ReadBooksFromFile();
            Book book = books.FirstOrDefault(b => b.BookId == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [HttpPost]
        public IActionResult Edit(Book updatedBook)
        {
            List<Book> books = ReadBooksFromFile();
            Book existingBook = books.FirstOrDefault(b => b.BookId == updatedBook.BookId);
            if (existingBook == null)
            {
                return NotFound();
            }

            // Оновлюємо дані книги
            existingBook.Title = updatedBook.Title;
            existingBook.Author = updatedBook.Author;
            existingBook.Genre = updatedBook.Genre;
            existingBook.Year = updatedBook.Year;
            existingBook.Publisher = updatedBook.Publisher;
            existingBook.Price = updatedBook.Price;

            SaveBooksToFile(books);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            List<Book> books = ReadBooksFromFile();
            Book book = books.FirstOrDefault(b => b.BookId == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            List<Book> books = ReadBooksFromFile();
            Book bookToRemove = books.FirstOrDefault(b => b.BookId == id);
            if (bookToRemove == null)
            {
                return NotFound();
            }

            books.Remove(bookToRemove);
            SaveBooksToFile(books);
            return RedirectToAction("Index");
        }


        

    }
}
