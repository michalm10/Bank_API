using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using nbp_api.Config;
using nbp_api.Models;
using nbp_api.Models.Output;
using nbp_api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Fixtures;
using Tests.Helpers;
using Xunit;

namespace Tests.Systems.Services
{
    public class TestBankService
    {
        [Fact]
        public async Task GetRateOnDateInvokesConfiguredUrl()
        {
            var response = TablesFixture.GetAOne();
            var url = "http://api.nbp.pl/api/exchangerates/rates/c/chf/last/6?format=json";
            var handler = MockHttpMessageHandler<ExchangesAModel>.SetupResponse(response, url);
            var httpClient = new HttpClient(handler.Object);
            var config = Options.Create(new BankAPI() { URL = url });

            var sut = new BankService(httpClient, config);

            var result = await sut.getRateOnDate("GBP", DateOnly.Parse("2022-12-12"));

            handler
                .Protected()
                .Verify("SendAsync",
                    Times.Exactly(1),
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri.ToString() == url),
                    ItExpr.IsAny<CancellationToken>()
            );
        }

        [Fact]
        public async Task InvokesHttpGetRequest()
        {
            var response = TablesFixture.GetAOne();
            var url = "https://whatever";
            var handler = MockHttpMessageHandler<ExchangesAModel>.SetupResponse(response);
            var httpClient = new HttpClient(handler.Object);
            var config = Options.Create(new BankAPI() { URL = url });

            var sut = new BankService(httpClient, config);
            
            await sut.getRateOnDate("GBP", DateOnly.Parse("2022-12-12"));

            handler
                .Protected()
                .Verify("SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
            );
        }

        [Fact]
        public async Task GetRateOnDateReturnsCorrectRate()
        {
            var response = TablesFixture.GetAOne();
            var url = "https://whatever";
            var handler = MockHttpMessageHandler<ExchangesAModel>.SetupResponse(response);
            var httpClient = new HttpClient(handler.Object);
            var config = Options.Create(new BankAPI() { URL = url });

            var sut = new BankService(httpClient, config);

            var result = await sut.getRateOnDate("GBP", DateOnly.Parse("2022-12-12"));

            result.Should().BeOfType<string>();
            result.Should().Be("5,5");
        }

        [Fact]
        public async Task GetRateOnDateReturnsNullWhenNotFound()
        {
            var url = "https://whatever";
            var handler = MockHttpMessageHandler<ExchangesAModel>.SetupReturn404();
            var httpClient = new HttpClient(handler.Object);
            var config = Options.Create(new BankAPI() { URL = url });

            var sut = new BankService(httpClient, config);

            var result = await sut.getRateOnDate("GBP", DateOnly.Parse("2022-12-12"));

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetMinMaxReturnsCorrectMinMax()
        {
            var response = TablesFixture.GetAMultiple();
            var url = "https://whatever";
            var handler = MockHttpMessageHandler<ExchangesAModel>.SetupResponse(response);
            var httpClient = new HttpClient(handler.Object);
            var config = Options.Create(new BankAPI() { URL = url });

            var sut = new BankService(httpClient, config);

            var result = await sut.getMinMax("GBP", 5);

            result.Should().BeOfType<MinMaxModel>();
            result.Min.Value.Should().Be(5.5);
            result.Min.Date.Should().Be("2022-04-14");
            result.Max.Value.Should().Be(11);
            result.Max.Date.Should().Be("2022-04-16");
        }

        [Fact]
        public async Task GetMinMaxReturnsNullWhenNotFound()
        {
            var url = "https://whatever";
            var handler = MockHttpMessageHandler<ExchangesAModel>.SetupReturn404();
            var httpClient = new HttpClient(handler.Object);
            var config = Options.Create(new BankAPI() { URL = url });

            var sut = new BankService(httpClient, config);

            var result = await sut.getMinMax("GBP", 5);

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetDiffReturnsCorrectDiff()
        {
            var response = TablesFixture.GetCMultiple();
            var url = "https://whatever";
            var handler = MockHttpMessageHandler<ExchangesCModel>.SetupResponse(response);
            var httpClient = new HttpClient(handler.Object);
            var config = Options.Create(new BankAPI() { URL = url });

            var sut = new BankService(httpClient, config);

            var result = await sut.getDiff("GBP", 5);

            result.Should().BeOfType<ValueModel>();
            result.Value.Should().Be(5.27);
            result.Date.Should().Be("2022-04-15");
        }

        [Fact]
        public async Task GetDiffReturnsNullWhenNotFound()
        {
            var url = "https://whatever";
            var handler = MockHttpMessageHandler<ExchangesAModel>.SetupReturn404();
            var httpClient = new HttpClient(handler.Object);
            var config = Options.Create(new BankAPI() { URL = url });

            var sut = new BankService(httpClient, config);

            var result = await sut.getDiff("GBP", 5);

            result.Should().BeNull();
        }
    }
}
