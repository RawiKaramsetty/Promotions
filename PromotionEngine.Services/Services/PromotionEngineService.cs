using PromotionEngine.DTO.DTOs;
using PromotionEngine.DTO.Models;
using PromotionEngine.Repositories.IRepositories;
using PromotionEngine.Repositories.Repositories;
using PromotionEngine.Services.ISevices;
using System.Collections.Generic;

namespace PromotionEngine.Services.Services
{
    public class PromotionEngineService : IPromotionEngineService
    {
        public List<CartDTO> GetCartValue(List<CartItem> items)
        {
            IPromotionEngineRepositery repository = new PromotionEngineRepositery();
            return repository.ProcessPromotion(items);
        }
    }
}
