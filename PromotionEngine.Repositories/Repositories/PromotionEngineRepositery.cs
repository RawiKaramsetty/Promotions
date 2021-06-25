using PromotionEngine.DTO.DTOs;
using PromotionEngine.DTO.Enums;
using PromotionEngine.DTO.Models;
using PromotionEngine.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PromotionEngine.Repositories.Repositories
{
    public class PromotionEngineRepositery : IPromotionEngineRepositery
    {
        private static List<Promotion> GetPromotions()
        {
            List<Promotion> promotions = new List<Promotion>();
            promotions.Add(new Promotion { SKUName = SKU.A.ToString(), PromotionPrice = 130, PromotionQuantity = 3, IsPromotionActive = true });
            promotions.Add(new Promotion { SKUName = SKU.B.ToString(), PromotionPrice = 45, PromotionQuantity = 2, IsPromotionActive = true });
            promotions.Add(new Promotion { SKUName = $"{SKU.C} & {SKU.D}", PromotionPrice = 30, PromotionQuantity = 1, IsPromotionActive = true });
            return promotions;
        }

        public List<CartDTO> ProcessPromotion(List<CartItem> cartItems)
        {
            return ProcessPromotionHelper(cartItems);
        }

        private static List<CartDTO> ProcessPromotionHelper(List<CartItem> cartItems)
        {
            List<CartDTO> items = new List<CartDTO>();
            CartDTO cartItem; Promotion promotion;

            #region Calculate for SKU A, B
            foreach (var item in cartItems)
            {
                cartItem = new CartDTO();
                promotion = GetPromotions().FirstOrDefault(x => x.SKUName.Equals(item.SKUItem.ToString()));
                if (promotion != null)
                {
                    switch (item.SKUItem)
                    {
                        case SKU.A:
                            cartItem.SKUName = SKU.A.ToString();
                            cartItem.ActualCost = Convert.ToInt32(SKU.A);
                            break;
                        case SKU.B:
                            cartItem.SKUName = SKU.B.ToString();
                            cartItem.ActualCost = Convert.ToInt32(SKU.B);
                            break;
                    }
                    cartItem.OfferedQuantity = item.SKUQuantity / promotion.PromotionQuantity;
                    cartItem.ActualQuantity = item.SKUQuantity;
                    cartItem.EligibleQuantityOfferedCost = cartItem.OfferedQuantity * promotion.PromotionPrice;
                    cartItem.FinalCost = cartItem.EligibleQuantityOfferedCost + ((item.SKUQuantity % promotion.PromotionQuantity) * cartItem.ActualCost);
                    items.Add(cartItem);
                }
            }
            #endregion


            #region Calculating for SKU C&D combo
            int skuC_Quantity = cartItems.Where(x => x.SKUItem.Equals(SKU.C)).Select(x => x.SKUQuantity).First();
            int skuD_Quantity = cartItems.Where(x => x.SKUItem.Equals(SKU.D)).Select(x => x.SKUQuantity).First();
            cartItem = new CartDTO();
            promotion = GetPromotions().FirstOrDefault(x => x.SKUName.Equals($"{SKU.C} & {SKU.D}"));

            if (promotion != null)
            {
                cartItem.SKUName = $"{SKU.C} & {SKU.D}";

                if (skuC_Quantity > skuD_Quantity)
                {
                    cartItem.ActualCost = Convert.ToInt32(SKU.C);
                    cartItem.ActualQuantity = skuC_Quantity;
                    cartItem.OfferedQuantity = skuD_Quantity;
                    cartItem.EligibleQuantityOfferedCost = cartItem.OfferedQuantity * promotion.PromotionPrice;
                    cartItem.FinalCost = cartItem.EligibleQuantityOfferedCost + ((skuC_Quantity - skuD_Quantity) * cartItem.ActualCost);
                }
                else if (skuD_Quantity > skuC_Quantity)
                {
                    cartItem.ActualCost = Convert.ToInt32(SKU.D);
                    cartItem.ActualQuantity = skuD_Quantity;
                    cartItem.OfferedQuantity = skuC_Quantity;
                    cartItem.EligibleQuantityOfferedCost = cartItem.OfferedQuantity * promotion.PromotionPrice;
                    cartItem.FinalCost = cartItem.EligibleQuantityOfferedCost + ((skuD_Quantity - skuC_Quantity) * cartItem.ActualCost);
                }
                else
                {
                    cartItem.ActualQuantity = cartItem.OfferedQuantity = skuD_Quantity; // or skuC_Quantity
                    cartItem.EligibleQuantityOfferedCost = cartItem.OfferedQuantity * promotion.PromotionPrice;
                    cartItem.FinalCost = cartItem.EligibleQuantityOfferedCost;
                }

                items.Add(cartItem);
            }
            #endregion

            return items;
        }
    }
}
