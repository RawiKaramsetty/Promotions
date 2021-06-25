namespace PromotionEngine.DTO.Models
{
    public class Promotion
    {
        public string SKUName { get; set; }
        public int PromotionQuantity { get; set; }
        public bool IsPromotionActive { get; set; }
        public int PromotionPrice { get; set; }
    }
}
