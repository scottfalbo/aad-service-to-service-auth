using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Identity.Client;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace AadAuthLocalTests
{
    public class ClientBuilder
    {
        public BuilderConfig _config { get; set; }

        public X509Certificate2 _certificate;
        public AuthenticationResult _accessToken;
        public HttpClient _client;

        public ClientBuilder()
        {
            _config = new BuilderConfig();
            _client = new HttpClient();
        }

        public async Task<HttpResponseMessage> AuthorizeClient()
        {
            _client.BaseAddress = new Uri("http://localhost:0000/api/");
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (_accessToken != null)
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken.AccessToken);

            var data = new StringContent("whatever");

            try
            {
                var response = await _client.PostAsync("MagicalWard", data);
                return response;
            }
            catch(Exception e)
            {
                throw new Exception($"womp womp: {e}");
            }

        }

        public async Task GetCertificateAsync(string subject)
        {
            var secretClient = new SecretClient(vaultUri: new Uri(_config.KeyVaultUri),
                credential: new DefaultAzureCredential());
            KeyVaultSecret secret = await secretClient.GetSecretAsync(subject);
            var cypher = Convert.FromBase64String(secret.Value);
            var certificate = new X509Certificate2(cypher, (string)null, X509KeyStorageFlags.MachineKeySet);
            _certificate = certificate;
        }

        public async Task GetAccessTokenAsync()
        {
            var scope = $"{_config.ClientId}/.default";
            var authority = $"{_config.Instance}{_config.TenantId}";

            IConfidentialClientApplication app = ConfidentialClientApplicationBuilder
                .Create(_config.ClientId)
                .WithCertificate(_certificate)
                .WithAuthority(authority)
                .Build();

            _accessToken = await app.AcquireTokenForClient(new[] { scope })
                .WithSendX5C(true)
                .ExecuteAsync();
        }

    }

    
}
