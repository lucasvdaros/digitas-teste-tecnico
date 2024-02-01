using Digitas.Quotes.Domain.Enums;
using Digitas.Quotes.Domain.Interfaces.Repositories;
using Digitas.Quotes.Domain.Interfaces.Services;
using Digitas.Quotes.Domain.Services;
using Digitas.Quotes.Domain.UnitTest.Mocks;
using Digitas.Quotes.Domain.ValueObjects;
using FluentAssertions;
using Moq;

namespace Digitas.Quotes.Domain.UnitTest.Services;

public class BtcOperationServiceTest
{
    private readonly Mock<IBtcAskRepository> mockBtcAskRepository;
    private readonly Mock<IBtcBidRepository> mockBtcBidRepository;
    private readonly Mock<IQuoteService> mockQuoteRepository;

    public BtcOperationServiceTest()
    {
        mockBtcAskRepository = new Mock<IBtcAskRepository>();
        mockBtcBidRepository = new Mock<IBtcBidRepository>();
        mockQuoteRepository = new Mock<IQuoteService>();
    }

    [Theory]
    [InlineData(OperationChoice.Buy, 0.05)]
    public async Task BtcOperationService_CalculateBtcBuyOperation_Given_Operation_Amount_When_Valid_ShouldReturnSimulation(OperationChoice operation, decimal amount)
    {
        //Arrange
        mockBtcAskRepository.Setup(btc => btc.GetLastUpdatedQuoutes()).ReturnsAsync(BtcAsks.BuildBtcAskList());
        mockQuoteRepository.Setup(btc => btc.SelectQuoutesByAmountRequested(It.IsAny<IEnumerable<ValueObjects.Quote>>(), amount)).Returns(BtcAsks.BuildQuoteList());
        mockQuoteRepository.Setup(btc => btc.CalculateFinalPriceOffer(It.IsAny<List<Quote>>(), amount)).Returns(1);

        var service = new BtcOperationService(mockQuoteRepository.Object, mockBtcAskRepository.Object, mockBtcBidRepository.Object);

        //Act
        var simulation = await service.CalculateBtcBuyOperation(operation, amount);

        //Assert
        simulation.Operation.Should().Be(operation);
        simulation.Quotes.Should().NotBeNullOrEmpty();
        simulation.Coin.Should().Be(Coin.BTC);
        simulation.hashIdentificacao.Should().NotBeNullOrEmpty();
        simulation.RequestAmount.Should().Be(amount);
        simulation.Result.Should().Be(1);
    }

    [Theory]
    [InlineData(OperationChoice.Sell, 0.05)]
    public async Task BtcOperationService_CalculateBtcSellOperation_Given_Operation_Amount_When_Valid_ShouldReturnSimulation(OperationChoice operation, decimal amount)
    {

    }

}
