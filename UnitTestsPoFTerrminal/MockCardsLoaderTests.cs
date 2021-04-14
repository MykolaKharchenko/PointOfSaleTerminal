using Microsoft.VisualStudio.TestTools.UnitTesting;
using PointOfSaleTerminal.Interfaces;
using PointOfSaleTerminal.Loaders;

namespace UnitTestsPoFTerminal
{
    [TestClass]
    public class MockCardsLoaderTests
    {
        ICardLoader mCardsLoader;

        [TestInitialize]
        public void Init()
        {
            mCardsLoader = new MockCardsLoader();
        }

        [DataTestMethod]
        [DataRow("none")]
        [DataRow("")]
        public void PurchaseWithoutDiscountCard(string inputedCardId)
        {
            // Act
            var card = mCardsLoader.GetCard(inputedCardId);

            Assert.ThrowsException<System.NullReferenceException>(() => card.TotalSum);
        }
    }
}
