using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace AadAuthLocalTests
{
    public class AuthTests
    {
        private readonly ITestOutputHelper _output;
        public AuthTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task CanAuthenticate()
        {
            var builder = new ClientBuilder();
            await builder.GetCertificateAsync("");
            await builder.GetAccessTokenAsync();
            var result = await builder.AuthorizeClient();

            string expectedIssuer = "";
            string expectedSubject = "";
            string expectedResponse = "OK";

            _output.WriteLine($"{builder._certificate}");
            _output.WriteLine($"Token: {builder._accessToken}");
            _output.WriteLine($"Response: {result}");

            Assert.Equal(expectedIssuer, builder._certificate.Issuer);
            Assert.Equal(expectedSubject, builder._certificate.Subject);
            Assert.NotNull(builder._accessToken);
            Assert.Equal(expectedResponse, result.ReasonPhrase);
        }
    }
}
