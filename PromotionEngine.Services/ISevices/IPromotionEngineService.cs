using PromotionEngine.DTO.DTOs;
using PromotionEngine.DTO.Models;
using System.Collections.Generic;

namespace PromotionEngine.Services.ISevices
{
    public interface IPromotionEngineService
    {
        List<CartDTO> GetCartValue(List<CartItem> items);
    }
}
