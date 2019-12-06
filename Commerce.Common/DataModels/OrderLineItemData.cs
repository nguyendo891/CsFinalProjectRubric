using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Common.DataModels
{
    public class OrderLineItemData
    {
        public int Sku { get; set; }
        public double PurchasePrice { get; set; }
        public  int Quantity { get; set; }
    }
}
