using System;
using System.Threading.Tasks;
using Xunit;

namespace AadAuthLocalTests
{
    public class AuthTests
    {
        [Fact]
        public async Task CanAuthenticate()
        {
            var builder = new ClientBuilder();
            await builder.GetCertificateAsync("");
            await builder.GetAccessTokenAsync();
            var result = await builder.AuthorizeClient();
            string expected = "OK";
            Assert.Equal(result.ReasonPhrase, expected);
        }
    }
}
