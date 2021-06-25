using PromotionEngine.DTO.DTOs;
using PromotionEngine.DTO.Models;
using System.Collections.Generic;

namespace PromotionEngine.Repositories.IRepositories
{
    public interface IPromotionEngineRepositery
    {
        List<CartDTO> ProcessPromotion(List<CartItem> skuItem);
    }
}
