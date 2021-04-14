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

    }
}
