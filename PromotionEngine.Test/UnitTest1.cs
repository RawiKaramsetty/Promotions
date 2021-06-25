using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PromotionEngine.DTO.DTOs;
using PromotionEngine.DTO.Models;
using PromotionEngine.Repositories.Repositories;
using System.Collections.Generic;
namespace PromotionEngine.Test
{
    [TestClass]
    public class PromotionEngineRepositeryTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            PromotionEngineRepositery promotionEngineRepositery = new PromotionEngineRepositery();

            List<CartItem> cartItems = new List<CartItem>();
            List<Promotion> promotions = new List<Promotion>();
            promotions.Add(new Promotion
            {
                IsPromotionActive = true,
                PromotionPrice = 100,
                PromotionQuantity = 2,
                SKUName = "Test"
            });
            var result = promotionEngineRepositery.GetPromotions();
            Assert.IsNotNull(result);
        }
    }
}
