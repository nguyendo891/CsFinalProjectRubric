using Commerce.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Providers
{
    public class AcmePaymentProcessor: IPaymentProcessor
    {
        public bool ProcessCreditCard(string customerName, string creditCard, string expirationDate, double amount)
        {
            Console.WriteLine("Credit card processed with Acme Payment Gateway for {0:c}.", amount);

            return true;
        }
    }
}
