using Microsoft.VisualStudio.TestTools.UnitTesting;
using PointOfSaleTerminal;
using System.Collections.Generic;

namespace UnitTestsPoFTerminal
{
    [TestClass]
    public class TerminalTests
    {
        Terminal terminal;

        [TestInitialize]
        public void Init()
        {
            terminal = new Terminal();
            terminal.SetPricing("TestData\\Products.json");
        }

        [DataTestMethod]
        [DataRow("Not found1")]
        [DataRow("Not found2")]
        public void TestNotFoundProduct(string inputData)
        {
            Assert.ThrowsException<System.Exception>(() => terminal.Scan(inputData));
        }

        [TestMethod]
        public void AddDeclaredProductToOrder()
        {
            //  Arrange
            var expectedColl = new Dictionary<string, int> { { "A", 2 } };
            var returnedColl = terminal.order;

            //  Act
            terminal.Scan("A");
            terminal.Scan("A");

            //  Assert
            CollectionAssert.AreEqual(expectedColl, returnedColl);
        }

        [TestMethod]
        public void AddUndeclaredProductToOrder()
        {
            //  Arrange
            var expectedColl = new Dictionary<string, int> { { "B", 1 } };
            var returnedColl = terminal.order;

            //  Act
            terminal.Scan("B");

            //  Assert
            CollectionAssert.AreEqual(expectedColl, returnedColl);
        }

        [DataTestMethod]
        [DataRow(new string[] { }, 0)]
        [DataRow(new string[] { "A" }, 1.25)]
        [DataRow(new string[] { "B" }, 4.25)]
        [DataRow(new string[] { "C" }, 1)]
        [DataRow(new string[] { "D" }, 0.75)]
        [DataRow(new string[] { "A", "A", "B", "C", "C", "C", "C", "C", "D" }, 12.5)]
        [DataRow(new string[] { "A", "B", "C", "C", "C", "D", "D", "D", "D", "A", "D" }, 13.5)]
        [DataRow(new string[] { "A", "B", "C", "D" }, 7.25)]
        [DataRow(new string[] { "A", "A", "A", "A", "A", "A" }, 6.75)]
        [DataRow(new string[] { "A", "A", "A", "A", "A", "A", "A" }, 8)]
        [DataRow(new string[] { "A", "A", "A", "A", "A", "A", "A", "A", "A" }, 10.5)]
        [DataRow(new string[] { "C", "C", "C", "C", "C", "C", "C", "C", "C" }, 8)]
        [DataRow(new string[] { "C", "C", "C", "C", "C", "C", "C", "C", "C", "C", "C", "C" }, 11)]
        [DataRow(new string[] { "A", "A", "A", "A", "A", "A" }, 6.75)]
        [DataRow(new string[] { "A", "A", "A", "C", "C", "C", "C", "C", "C" }, 8)]
        [DataRow(new string[] { "A", "A", "A", "B", "B", "B", "B", "C", "C", "C", "C", "C", "C", "D", "D", "D", "D" }, 28)]

        public void TestCalculation(string[] inputData, double expectedResult)
        {
            // Arrange
            foreach (var item in inputData)
                terminal.Scan(item);

            // Act
            var result = terminal.CalculateTotal();

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}
