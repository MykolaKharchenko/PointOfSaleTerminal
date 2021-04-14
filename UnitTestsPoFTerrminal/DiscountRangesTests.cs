using Microsoft.VisualStudio.TestTools.UnitTesting;
using PointOfSaleTerminal;
using PointOfSaleTerminal.DataModels;
using PointOfSaleTerminal.Interfaces;
using PointOfSaleTerminal.Loaders;

namespace UnitTestsPoFTerminal
{
    [TestClass]
    class DiscountRangesTests
    {
        ICardLoader mCardsLoader;
        DiscountCard dCard;

        [TestInitialize]
        public void Init()
        {
            mCardsLoader = new MockCardsLoader();
            dCard = mCardsLoader.GetCard("1");
        }

        [DataTestMethod]
        [DataRow(400, 1)]
        [DataRow(2000, 3)]
        [DataRow(4000, 5)]
        [DataRow(9000, 7)]
        public void RaisingCardDiscountTest(double sum, int expectedPercent)
        {
            //  Act
            mCardsLoader.UpdateCard(dCard, sum);
            int resultPercent = new DiscountRanges().GetDiscount(dCard);

            //  Assert
            Assert.AreEqual(expectedPercent, resultPercent);
        }
    }
}
