using PromotionEngine.DTO.Enums;
using PromotionEngine.DTO.Models;
using PromotionEngine.Services.ISevices;
using PromotionEngine.Services.Services;
using System;
using System.Collections.Generic;

namespace PromotionEngine
{
    class Program
    {
        /// <summary>
        /// Program execution starts from here 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("************ Welcome to PromotionEngine ************");
                Console.WriteLine();
                DisplayCartInfo(ReadSKUData());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Occurred!\nPlease look into below information\n{ex.Message}");
            }
            Console.ReadLine();
        }

        /// <summary>
        /// Displays cart value for each sku 
        /// </summary>
        /// <param name="items"></param>
        private static void DisplayCartInfo(List<CartItem> items)
        {
            Console.WriteLine();
            Console.WriteLine("************ Cart Value ************");
            Console.WriteLine();

            IPromotionEngineService service = new PromotionEngineService();
            var cart = service.GetCartValue(items);

            int totalCost = 0;
            foreach (var item in cart)
            {
                Console.WriteLine($"Cost for SKU {item.SKUName} for quantity {item.ActualQuantity} is:\t {item.FinalCost}");
                totalCost += item.FinalCost;
            }
            Console.WriteLine($"Total Cost for the cart is:\t\t {totalCost}");
        }

        /// <summary>
        /// User has to enter SKU quantity
        /// </summary>
        /// <returns></returns>
        private static List<CartItem> ReadSKUData()
        {
            Console.Write("Enter quantity for item A: ");
            int sku_A = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter quantity for item B: ");
            int sku_B = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter quantity for item C: ");
            int sku_C = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter quantity for item D: ");
            int sku_D = Convert.ToInt32(Console.ReadLine());

            List<CartItem> items = new List<CartItem>();
            items.Add(new CartItem { SKUItem = SKU.A, SKUQuantity = sku_A });
            items.Add(new CartItem { SKUItem = SKU.B, SKUQuantity = sku_B });
            items.Add(new CartItem { SKUItem = SKU.C, SKUQuantity = sku_C });
            items.Add(new CartItem { SKUItem = SKU.D, SKUQuantity = sku_D });
            return items;
        }
    }
}
