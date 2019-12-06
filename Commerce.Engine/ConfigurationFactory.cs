using Commerce.Common.Contracts;
using Commerce.Common.Modules;
using Commerce.Engine.Configuration;
using Commerce.Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Engine
{
    public class ConfigurationFactory : IConfigurationFactory
    {
        readonly IPaymentProcessor _PaymentProcessor;
        readonly IMailer _Mailer;
        readonly CommerceEvents _Events;

        public ConfigurationFactory()
        {
            CommerceEngineConfigurationSection config = ConfigurationManager.GetSection("commerceEngine") as CommerceEngineConfigurationSection;
            if(config != null)
            {
                IPaymentProcessor paymentProcessor = Activator.CreateInstance(Type.GetType(config.PaymentProcessor.Type)) as IPaymentProcessor;
                IMailer mailer = Activator.CreateInstance(Type.GetType(config.Mailer.Type)) as IMailer;
                mailer.FromAddress = config.Mailer.FromAddress;
                mailer.SmtpServer = config.Mailer.SmtpServer;
                _PaymentProcessor = paymentProcessor;
                _Mailer = mailer;

                //handle modules
                _Events = new CommerceEvents();
                foreach(ModuleElement moduleElement in config.Modules)
                {
                    ICommerceModule module =
                        Activator.CreateInstance(Type.GetType(moduleElement.Type)) as ICommerceModule;
                    module.Initialize(_Events);
                }
            }
        }

        public CommerceEvents GetEvents()
        {
            return _Events;
        }

        public IMailer GetMailer()
        {
            return _Mailer;
        }

        public IPaymentProcessor GetPaymentProcessor()
        {
            return _PaymentProcessor;
        }
    }
}
