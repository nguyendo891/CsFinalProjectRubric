using Commerce.Common.Contracts;
using Commerce.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commerce.Engine
{
    public class StoreRepository : IStoreRepository
    {
        public StoreRepository()
        {
            Initialize();
        }
        private List<Product> _Products;
        List<Inventory> _ProductInventory = null;
        List<Customer> _Customers = null;

        public List<Product> Products
        {
            get { return _Products; }
        }

        public List<Inventory> ProductInventory
        {
            get { return _ProductInventory; }
        }

        public List<Customer> Customers
        {
            get { return _Customers; }
        }

        public void Initialize()
        {
            _Products = new List<Product>()
            {
                new Product() {Sku = 101, Description = "keyboard", UnitPrice = 10.00},
                new Product() {Sku = 102, Description = "monitor", UnitPrice = 100.00},
                new Product() {Sku = 103, Description = "headphones", UnitPrice = 10.00},
                new Product() {Sku = 104, Description = "mouse", UnitPrice = 5.00},
                new Product() {Sku = 105, Description = "router", UnitPrice = 50.00},
                new Product() {Sku = 106, Description = "speaker", UnitPrice = 15.00},
                new Product() {Sku = 107, Description = "cable", UnitPrice = 1.00},
                new Product() {Sku = 108, Description = "USB driver", UnitPrice = 5.00},
                new Product() {Sku = 109, Description = "motherboard", UnitPrice = 150.00},
                new Product() {Sku = 110, Description = "CDs", UnitPrice = 8.00},
                new Product() {Sku = 111, Description = "Hard drive", UnitPrice = 20.00},
                new Product() {Sku = 112, Description = "CPU", UnitPrice = 200.00},
                new Product() {Sku = 113, Description = "GPU", UnitPrice = 200.00},
                new Product() {Sku = 114, Description = "RAM", UnitPrice = 100.00},
                new Product() {Sku = 115, Description = "powersupply", UnitPrice = 80.00},
                new Product() {Sku = 116, Description = "laptop", UnitPrice = 700.00},
                new Product() {Sku = 117, Description = "Fans", UnitPrice = 150.00},
                new Product() {Sku = 118, Description = "PC case", UnitPrice = 30.00},
                new Product() {Sku = 119, Description = "Earphone", UnitPrice = 20.00},
                new Product() {Sku = 120, Description = "Battery", UnitPrice = 5.00}
            };

            _ProductInventory = new List<Inventory>()
            {
                new Inventory() { Sku = 101, QuantityInStock = 1 },
                new Inventory() { Sku = 102, QuantityInStock = 1 },
                new Inventory() { Sku = 103, QuantityInStock = 1 },
                new Inventory() { Sku = 104, QuantityInStock = 1 },
                new Inventory() { Sku = 105, QuantityInStock = 1 },
                new Inventory() { Sku = 106, QuantityInStock = 1 },
                new Inventory() { Sku = 107, QuantityInStock = 1 },
                new Inventory() { Sku = 108, QuantityInStock = 1 },
                new Inventory() { Sku = 109, QuantityInStock = 1 },
                new Inventory() { Sku = 110, QuantityInStock = 1 },
                new Inventory() { Sku = 111, QuantityInStock = 1 },
                new Inventory() { Sku = 112, QuantityInStock = 1 },
                new Inventory() { Sku = 113, QuantityInStock = 1 },
                new Inventory() { Sku = 114, QuantityInStock = 1 },
                new Inventory() { Sku = 115, QuantityInStock = 1 },
                new Inventory() { Sku = 116, QuantityInStock = 1 },
                new Inventory() { Sku = 117, QuantityInStock = 1 },
                new Inventory() { Sku = 118, QuantityInStock = 1 },
                new Inventory() { Sku = 119, QuantityInStock = 1 },
                new Inventory() { Sku = 120, QuantityInStock = 1 },
            };

            _Customers = new List<Customer>()
                {
                    new Customer() { Email = "nguyendo891@gmail.com", Name = "Dinh Nguyen Do", Purchases = new List<PurchasedItem>() },
                    new Customer() { Email = "dhhoang69@gmail.com", Name = "Jason Salmond", Purchases = new List<PurchasedItem>() },
                    new Customer() { Email = "aason.skonnard@pluralsight.com", Name = "Aaron Skonnard", Purchases = new List<PurchasedItem>() }
                };
        }

        public Customer GetCustomerByEmail(string email)
        {
            return _Customers.Where(item => item.Email == email).FirstOrDefault();
        }

    }
}

