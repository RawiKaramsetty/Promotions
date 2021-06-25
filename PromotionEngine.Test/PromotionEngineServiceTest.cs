using Microsoft.VisualStudio.TestTools.UnitTesting;
using PromotionEngine.DTO.Enums;
using PromotionEngine.DTO.Models;
using PromotionEngine.Services.ISevices;
using PromotionEngine.Services.Services;
using System.Collections.Generic;

namespace PromotionEngine.Test
{
    [TestClass]
    public class PromotionEngineServiceTest
    {
        IPromotionEngineService _promotionEngineService;

        public PromotionEngineServiceTest()
        {
            _promotionEngineService = new PromotionEngineService();
        }
        
        [TestMethod]
        public void Cart_with_all_SKU_values_test()
        {

            List<CartItem> cartItems = new List<CartItem>();
            cartItems.Add(new CartItem { SKUItem = SKU.A, SKUQuantity = 4 }); // Final Cost = 130 + 50 = 180
            cartItems.Add(new CartItem { SKUItem = SKU.B, SKUQuantity = 5 }); // Final Cost = 45 + 45 + 30 = 120
            cartItems.Add(new CartItem { SKUItem = SKU.C, SKUQuantity = 2 }); // Final Cost => C&D => 30 + 20 (another SKU-C) = 50
            cartItems.Add(new CartItem { SKUItem = SKU.D, SKUQuantity = 1 }); // Covered in above line. Total should be => 180 + 120 + 30 + 20 = 350

            var totalCartValue = 0;
            var cartResult = _promotionEngineService.GetCartValue(cartItems);
            foreach (var item in cartResult)
            {
                totalCartValue += item.FinalCost;
            }

            Assert.AreEqual(350, totalCartValue);

        }

        [TestMethod]
        public void Cart_with_some_SKU_values_as_zero_test()
        {
            List<CartItem> cartItems = new List<CartItem>();
            cartItems.Add(new CartItem { SKUItem = SKU.A, SKUQuantity = 4 }); // Final Cost = 130 + 50 = 180
            cartItems.Add(new CartItem { SKUItem = SKU.B, SKUQuantity = 0 }); // Final Cost = 0
            cartItems.Add(new CartItem { SKUItem = SKU.C, SKUQuantity = 2 }); // Final Cost = 20 + 20 = 40
            cartItems.Add(new CartItem { SKUItem = SKU.D, SKUQuantity = 0 }); // Final Cost = 0

            var totalCartValue = 0;
            var cartResult = _promotionEngineService.GetCartValue(cartItems);
            foreach (var item in cartResult)
            {
                totalCartValue += item.FinalCost;
            }
            Assert.AreEqual(220, totalCartValue);
        }
    }
}
