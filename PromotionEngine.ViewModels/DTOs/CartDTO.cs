namespace PromotionEngine.DTO.DTOs
{
    public class CartDTO
    {
        public string SKUName { get; set; }
        public int ActualQuantity { get; set; }
        public int ActualCost { get; set; }
        public int OfferedQuantity { get; set; }
        public int EligibleQuantityOfferedCost { get; set; }
        public int FinalCost { get; set; }
    }
}
