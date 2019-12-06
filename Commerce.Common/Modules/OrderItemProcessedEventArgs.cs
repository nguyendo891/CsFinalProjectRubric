using Commerce.Common.Contracts;
using Commerce.Common.DataModels;
using Commerce.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Common.Modules
{
    public class OrderItemProcessedEventArgs: CancelEventArgs
    {
        public OrderItemProcessedEventArgs(Customer customer, OrderLineItemData orderLineItemData,IStoreRepository storeRepository)
        {
            Customer = customer;
            OrderLineItemData = orderLineItemData;
            MessageText = string.Empty;
            StoreRepository = storeRepository;
        }
        public Customer Customer { get; set; }
        public OrderLineItemData OrderLineItemData { get; set; }
        public string MessageText { get; set; }
        public IStoreRepository StoreRepository { get; set; }
    }
}
