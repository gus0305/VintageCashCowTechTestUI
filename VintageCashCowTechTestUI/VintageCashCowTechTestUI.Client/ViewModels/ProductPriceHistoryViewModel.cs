namespace VintageCashCowTechTestUI.Client.ViewModels
{
    public class ProductPriceHistoryViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<PriceHistoryViewModel> PriceHistory { get; set; }
    }
}
