using Bunit;

namespace VintageCashCowTechTestUI.Client.Tests.Unit
{
    public abstract class BunitTestContext : TestContextWrapper
    {
        [TestInitialize]
        public void Setup() => TestContext = new Bunit.TestContext();

        [TestCleanup]
        public void TearDown() => TestContext?.Dispose();

        public string GetTableRowSelector(int rowNumber)
        {
            return $"table > tbody > tr:nth-child({rowNumber})";
        }

        public string GetTableCellSelector(int rowNumber, int columnNumber)
        {
            return $"{GetTableRowSelector(rowNumber)} > td:nth-child({columnNumber})";
        }

        public string GetTableRowsSelector()
        {
            return "table > tbody > tr";
        }
    }
}
