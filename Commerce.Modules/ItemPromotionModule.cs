﻿using Commerce.Common.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Modules
{
    public class ItemPromotionModule : ICommerceModule
    {
        public void Initialize(CommerceEvents events)
        {
            events.OrderItemProcessed += OnOrderItemProcessed;
        }

        private void OnOrderItemProcessed(OrderItemProcessedEventArgs e)
        {
            //This line is for update status of discount or something new about products.
            //if(e.OrderLineItemData.Sku == 102)
            //{
            //    e.OrderLineItemData.PurchasePrice -= 30.00;
            //}
        }
    }
}
