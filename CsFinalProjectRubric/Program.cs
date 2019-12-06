using Commerce.Common.Contracts;
using Commerce.Common.DataModels;
using Commerce.Engine;
using Commerce.Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace CsFinalProjectRubric
{
    class Program
    {
        static void Main(string[] args)
        {
            UnityContainer container = new UnityContainer();
            container.RegisterType<IStoreRepository, StoreRepository>()
                     .RegisterType<ICommerceManager, CommerceManager>()
                     .RegisterType<IConfigurationFactory, ConfigurationFactory>();

            OrderData orderData = new OrderData()
            {
                CustomerEmail = "dhhoang69@gmail.com",
                LineItems = new List<OrderLineItemData>()
                {
                    new OrderLineItemData() {Sku =102, PurchasePrice = 100, Quantity =1},
                    new OrderLineItemData() {Sku =101, PurchasePrice = 10, Quantity =1},
                    new OrderLineItemData() {Sku =103, PurchasePrice = 10, Quantity =1},
                },
                CreditCard = "1234567891234213",
                ExpirationDate = "1217"
            };

            ICommerceManager commerceEngine = container.Resolve<ICommerceManager>();
            commerceEngine.ProcessOrder(orderData);

            Console.WriteLine();
            Console.WriteLine("Press [Enter] to exit.");
            Console.ReadLine();
        }
    }
}
