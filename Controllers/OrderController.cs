using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using WebApplication3.Models;


namespace WebApplication3.Controllers
{
    public class OrderController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        private const string FilePath = "orders.json";

        public IActionResult Index()
        {
            List<Order> orders = ReadOrdersFromFile();
            return View(orders);
        }

        private List<Customer> ReadCustomersFromFile()
        {
            if (System.IO.File.Exists("customers.json"))
            {
                string json = System.IO.File.ReadAllText("customers.json");
                return JsonSerializer.Deserialize<List<Customer>>(json);
            }
            return new List<Customer>();
        }

        private List<Book> ReadBooksFromFile()
        {
            if (System.IO.File.Exists("books.json"))
            {
                string json = System.IO.File.ReadAllText("books.json");
                return JsonSerializer.Deserialize<List<Book>>(json);
            }
            return new List<Book>();
        }

        [HttpGet]
        public IActionResult Create()
        {
            // Вам також може знадобитися передача списку клієнтів та книг для вибору у формі
            List<Customer> customers = ReadCustomersFromFile();
            List<Book> books = ReadBooksFromFile();

            ViewBag.Customers = new SelectList(customers, "CustomerId", "FirstName");
            ViewBag.Books = new SelectList(books, "BookId", "Title");

            return View();
        }

        [HttpPost]
        public IActionResult Create(Order order)
        {
            List<Order> orders = ReadOrdersFromFile();
            orders.Add(order);
            SaveOrdersToFile(orders);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            List<Order> orders = ReadOrdersFromFile();
            Order order = orders.FirstOrDefault(o => o.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            // Передача списків клієнтів та книг для вибору у форму редагування
            List<Customer> customers = ReadCustomersFromFile();
            List<Book> books = ReadBooksFromFile();

            ViewBag.Customers = new SelectList(customers, "CustomerId", "FirstName", order.CustomerId);
            ViewBag.Books = new SelectList(books, "BookId", "Title", order.BookId);

            return View(order);
        }

        [HttpPost]
        public IActionResult Edit(Order updatedOrder)
        {
            List<Order> orders = ReadOrdersFromFile();
            Order existingOrder = orders.FirstOrDefault(o => o.OrderId == updatedOrder.OrderId);
            if (existingOrder == null)
            {
                return NotFound();
            }

            existingOrder.CustomerId = updatedOrder.CustomerId;
            existingOrder.BookId = updatedOrder.BookId;
            existingOrder.Date = updatedOrder.Date;
            existingOrder.Status = updatedOrder.Status;
            existingOrder.TotalAmount = updatedOrder.TotalAmount;
            existingOrder.Price = updatedOrder.Price;

            SaveOrdersToFile(orders);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            List<Order> orders = ReadOrdersFromFile();
            Order order = orders.FirstOrDefault(o => o.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            List<Order> orders = ReadOrdersFromFile();
            Order orderToRemove = orders.FirstOrDefault(o => o.OrderId == id);
            if (orderToRemove == null)
            {
                return NotFound();
            }

            orders.Remove(orderToRemove);
            SaveOrdersToFile(orders);
            return RedirectToAction("Index");
        }

        private List<Order> ReadOrdersFromFile()
        {
            if (System.IO.File.Exists(FilePath))
            {
                string json = System.IO.File.ReadAllText(FilePath);
                return JsonSerializer.Deserialize<List<Order>>(json);
            }
            return new List<Order>();
        }

        private void SaveOrdersToFile(List<Order> orders)
        {
            string json = JsonSerializer.Serialize(orders);
            System.IO.File.WriteAllText(FilePath, json);
        }
    }
}
