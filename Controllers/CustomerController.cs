using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using System.Text.Json;

namespace WebApplication3.Controllers
{
    public class CustomerController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        private const string FilePath = "customers.json";

        public IActionResult Index()
        {
            List<Customer> customers = ReadCustomersFromFile();
            return View(customers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            List<Customer> customers = ReadCustomersFromFile();
            customers.Add(customer);
            SaveCustomersToFile(customers);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            List<Customer> customers = ReadCustomersFromFile();
            Customer customer = customers.FirstOrDefault(c => c.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit(Customer updatedCustomer)
        {
            List<Customer> customers = ReadCustomersFromFile();
            Customer existingCustomer = customers.FirstOrDefault(c => c.CustomerId == updatedCustomer.CustomerId);
            if (existingCustomer == null)
            {
                return NotFound();
            }

            existingCustomer.FirstName = updatedCustomer.FirstName;
            existingCustomer.LastName = updatedCustomer.LastName;
            existingCustomer.Email = updatedCustomer.Email;
            existingCustomer.Address = updatedCustomer.Address;
            existingCustomer.PhoneNumber = updatedCustomer.PhoneNumber;

            SaveCustomersToFile(customers);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            List<Customer> customers = ReadCustomersFromFile();
            Customer customer = customers.FirstOrDefault(c => c.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            List<Customer> customers = ReadCustomersFromFile();
            Customer customerToRemove = customers.FirstOrDefault(c => c.CustomerId == id);
            if (customerToRemove == null)
            {
                return NotFound();
            }

            customers.Remove(customerToRemove);
            SaveCustomersToFile(customers);
            return RedirectToAction("Index");
        }

        private List<Customer> ReadCustomersFromFile()
        {
            if (System.IO.File.Exists(FilePath))
            {
                string json = System.IO.File.ReadAllText(FilePath);
                return JsonSerializer.Deserialize<List<Customer>>(json);
            }
            return new List<Customer>();
        }

        private void SaveCustomersToFile(List<Customer> customers)
        {
            string json = JsonSerializer.Serialize(customers);
            System.IO.File.WriteAllText(FilePath, json);
        }


    }
}
