using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AadAuthLocalTests
{
    public class BuilderConfig
    {
        public string ClientId { get; set; }
        public string TenantId { get; set; }
        public string Instance { get; set; }
        public string KeyVaultSubject { get; set; }
        public string KeyVaultUri { get; set; }

        public BuilderConfig()
        {
            ClientId = "client id";
            TenantId = "tenant id";
            Instance = "instance";
            KeyVaultSubject = "subject";
            KeyVaultUri = "uri";
        }
    }
}
