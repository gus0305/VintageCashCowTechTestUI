﻿namespace VintageCashCowTechTestUI.Client.Services.Product.DataContracts
{
    public class DiscountResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal DiscountedPrice { get; set; }
    }
}
