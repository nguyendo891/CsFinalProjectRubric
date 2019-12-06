using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Engine.Configuration
{
    public class MailerElement : ProviderTypeElement 
    {
        [ConfigurationProperty("fromAddress",IsRequired = true)]
        public string FromAddress
        {
            get { return (string)base["fromAddress"]; }
            set { base["fromAddress"] = value; }
        }

        [ConfigurationProperty("smtpServer",IsRequired = true)]
        public string SmtpServer
        {
            get { return (string)base["smtpServer"]; }
            set { base["smtpServer"] = value; }
        }
    }
}
