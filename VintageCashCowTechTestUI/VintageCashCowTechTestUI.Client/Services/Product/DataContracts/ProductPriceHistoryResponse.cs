namespace VintageCashCowTechTestUI.Client.Services.Product.DataContracts
{
    public class ProductPriceHistoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<PriceHistory> PriceHistory { get; set; }
    }
}
