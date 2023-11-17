using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AddressesAPI.Boundary.Responses;
using AddressesAPI.v1.Infrastructure;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;

namespace AddressesAPI.Tests.v1.E2E
{
    [TestFixture]
    public class AddressesE2ETests
    {
        public HttpClient Client;

        [SetUp]
        public void SetUp()
        {
            Environment.SetEnvironmentVariable("CONNECTION_STRING", "Host=localhost;Port=5430;Database=testdb;Username=postgresuser;Password=postgresPass");
            var _factory = new TestFact();
            Client = _factory.CreateClient();
        }

        [TestCase(TestName = "get addresses by postcode endpoint returns bad request upon invalid postcode")]
        public async Task GetAddressesByPostcodeEndpointReturnsBadRequestWhenProvidedWithInvalidPostcode()
        {
            // arrange
            var url = new Uri($"/api/v1/addresses?postcode=GIR0AA", UriKind.Relative);

            // act
            var response = await Client.GetAsync(url);

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        }

        [TestCase(TestName = "get addresses by postcode endpoint returns ok response upon invalid postcode")]
        public async Task GetAddressesByPostcodeEndpointReturnsOkResponseUponValidPostcode()
        {
            // arrange
            var url = new Uri($"/api/v1/addresses?postcode=E81LL", UriKind.Relative);

            // act
            var response = await Client.GetAsync(url);

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var strData = await response.Content.ReadAsStringAsync();
            var responseContent = JsonConvert.DeserializeObject<List<AddressPresentation>>(strData);

            responseContent.Should().NotBeNull();
            responseContent.Should().HaveCount(1);
        }
    }
}
