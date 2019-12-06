using Commerce.Common.Contracts;
using Commerce.Common.DataModels;
using Commerce.Common.Entities;
using Commerce.Common.Modules;
using Commerce.Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Commerce.Engine
{
    public class CommerceManager : ICommerceManager
    {
        readonly IStoreRepository _StoreRepository;
        readonly IPaymentProcessor _PaymentProcessor;
        readonly IMailer _Mailer;
        readonly CommerceEvents _CommerceEvents;
        public CommerceManager(IStoreRepository storeRepository,IConfigurationFactory configurationFactory)
        {
            _StoreRepository = storeRepository;
            _PaymentProcessor = configurationFactory.GetPaymentProcessor();
            _Mailer = configurationFactory.GetMailer();
            _CommerceEvents = configurationFactory.GetEvents();
        }

        #region ICommerceManager Members
        public void ProcessOrder(OrderData orderData)
        {
            try
            {
                using(TransactionScope scope = new TransactionScope())
                {
                    Customer customer = _StoreRepository.GetCustomerByEmail(orderData.CustomerEmail);
                    if(customer== null)
                    {
                        throw new ApplicationException(string.Format("No customer on file with email {0}.", orderData.CustomerEmail));
                    }

                    foreach(OrderLineItemData lineItem in orderData.LineItems)
                    {
                        if (_CommerceEvents.OrderItemProcessed != null)
                        {
                            OrderItemProcessedEventArgs args = new OrderItemProcessedEventArgs(customer, lineItem, _StoreRepository);
                            _CommerceEvents.OrderItemProcessed(args);
                            if (args.Cancel)
                                throw new ApplicationException(args.MessageText);
                        }

                        Product product = _StoreRepository.Products.Where(item => item.Sku == lineItem.Sku).FirstOrDefault();
                        if (product == null)
                            throw new ApplicationException(string.Format("Sku {0} not found in store inventory.", lineItem.Sku));

                        Inventory inventoryOnHand = _StoreRepository.ProductInventory.Where(item => item.Sku == lineItem.Sku).FirstOrDefault();
                        if (inventoryOnHand == null)
                            throw new ApplicationException(string.Format("Error attempting to determine on-hand inventory quantity for product {0}.", lineItem.Sku));

                        if (inventoryOnHand.QuantityInStock < lineItem.Quantity)
                            throw new ApplicationException(string.Format("Not enough quantity on-hand to satisfy product {0} purchase of {1} units.", lineItem.Sku, lineItem.Quantity));

                        inventoryOnHand.QuantityInStock -= lineItem.Quantity;
                        Console.WriteLine("Inventory for product {0}({2}) reduced by {1} units at {3}.", lineItem.Sku, lineItem.Quantity, product.Description, DateTime.Now.ToString());
                    }

                    // Update customer records with purchase
                    foreach (OrderLineItemData lineItem in orderData.LineItems)
                    {
                        for (int i = 0; i < lineItem.Quantity; i++)
                            customer.Purchases.Add(new PurchasedItem() { Sku = lineItem.Sku, PurchasePrice = lineItem.PurchasePrice, PurchasedOn = DateTime.Now });
                        Console.WriteLine("Added {0} unit(s) or product {1} to customer's purchase history.", lineItem.Quantity, lineItem.Sku);
                    }

                    // Process customer credit card
                    double amount = 0;
                    foreach (OrderLineItemData lineItem in orderData.LineItems)
                        amount += (lineItem.PurchasePrice * lineItem.Quantity);

                    bool paymentSuccess = _PaymentProcessor.ProcessCreditCard(customer.Name, orderData.CreditCard, orderData.ExpirationDate, amount);
                    if (!paymentSuccess)
                        throw new ApplicationException(string.Format("Credit card {0} could not be processed.", orderData.CreditCard));

                    // Send invoice email
                    _Mailer.SendInvoiceEmail(orderData);

                    scope.Complete();
                }
            }
            catch (Exception)
            {
                _Mailer.SendRejectionEmail(orderData);
                throw;
            }
        }
        #endregion
    }
}
