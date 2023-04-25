using Moq;
using FluentAssertions;
using nbp_api.Controllers;
using nbp_api.Services;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using nbp_api.Models.Output;

namespace Tests.Systems.Controllers
{
    public class TestExchangesController
    {
        [Fact]
        public async Task GetAvgRateReturns400WhenWrongDateFormat()
        {
            var mockBankService = new Mock<IBankService>();
            var sut = new ExchangesController(mockBankService.Object);

            var result = (BadRequestObjectResult)await sut.GetAvgRate("GBP", "20weddf2");

            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task InvokesBankService()
        {
            var mockBankService = new Mock<IBankService>();
            var sut = new ExchangesController(mockBankService.Object);

            await sut.GetAvgRate("GBP", "2022-12-12");

            mockBankService.Verify(service => service.getRateOnDate("GBP", DateOnly.Parse("2022-12-12")), Times.Once());
        }

        [Fact]
        public async Task GetAvgRateReturnsCorrectly()
        {
            var mockBankService = new Mock<IBankService>();
            mockBankService
                .Setup(service => service.getRateOnDate("GBP", DateOnly.Parse("2022-11-17")))
                .ReturnsAsync("6");
            var sut = new ExchangesController(mockBankService.Object);

            var result = await sut.GetAvgRate("GBP", "2022-11-17");
            result.Should().BeOfType<OkObjectResult>();
            var objectresult = (OkObjectResult)result;
            objectresult.Value.Should().BeOfType<string>();
            objectresult.Value.Should().Be("6");
        }

        [Fact]
        public async Task GetAvgRateReturns404()
        {
            var mockBankService = new Mock<IBankService>();
            mockBankService
                .Setup(service => service.getRateOnDate("GBP", DateOnly.Parse("2022-11-17")))
                .ReturnsAsync((string)null);
            var sut = new ExchangesController(mockBankService.Object);

            var result = await sut.GetAvgRate("GBP", "2022-11-17");
            result.Should().BeOfType<NotFoundResult>();
            var objectResult = (NotFoundResult)result;
            objectResult.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task GetMinMaxReturns400WhenNTooBig()
        {
            var mockBankService = new Mock<IBankService>();
            var sut = new ExchangesController(mockBankService.Object);

            var result = (BadRequestObjectResult)await sut.GetMinMax("GBP", 2341);

            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task GetMinMaxReturnsCorrectly()
        {
            var fakeResult = new MinMaxModel(new ValueModel(12, "12-12-12"), new ValueModel(14, "12-12-14"));
            var mockBankService = new Mock<IBankService>();
            mockBankService
                .Setup(service => service.getMinMax("GBP", 5))
                .ReturnsAsync(fakeResult);
            var sut = new ExchangesController(mockBankService.Object);

            var result = await sut.GetMinMax("GBP", 5);
            result.Should().BeOfType<OkObjectResult>();
            var objectresult = (OkObjectResult)result;
            objectresult.Value.Should().BeOfType<MinMaxModel>();
            objectresult.Value.Should().Be(fakeResult);
        }

        [Fact]
        public async Task GetMinMaxReturns404()
        {
            var mockBankService = new Mock<IBankService>();
            mockBankService
                .Setup(service => service.getMinMax("GBP", 5))
                .ReturnsAsync((MinMaxModel)null);
            var sut = new ExchangesController(mockBankService.Object);

            var result = await sut.GetMinMax("GBP", 5);
            result.Should().BeOfType<NotFoundResult>();
            var objectResult = (NotFoundResult)result;
            objectResult.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task GetDiffReturns400WhenNTooBig()
        {
            var mockBankService = new Mock<IBankService>();
            var sut = new ExchangesController(mockBankService.Object);

            var result = (BadRequestObjectResult)await sut.GetDiff("GBP", 2341);

            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task GetDiffReturnsCorrectly()
        {
            var fakeResult = new ValueModel(12, "12-12-12");
            var mockBankService = new Mock<IBankService>();
            mockBankService
                .Setup(service => service.getDiff("GBP", 5))
                .ReturnsAsync(fakeResult);
            var sut = new ExchangesController(mockBankService.Object);

            var result = await sut.GetDiff("GBP", 5);
            result.Should().BeOfType<OkObjectResult>();
            var objectresult = (OkObjectResult)result;
            objectresult.Value.Should().BeOfType<ValueModel>();
            objectresult.Value.Should().Be(fakeResult);
        }

        [Fact]
        public async Task GetDiffReturns404()
        {
            var mockBankService = new Mock<IBankService>();
            mockBankService
                .Setup(service => service.getDiff("GBP", 5))
                .ReturnsAsync((ValueModel)null);
            var sut = new ExchangesController(mockBankService.Object);

            var result = await sut.GetDiff("GBP", 5);
            result.Should().BeOfType<NotFoundResult>();
            var objectResult = (NotFoundResult)result;
            objectResult.StatusCode.Should().Be(404);
        }
    }
}