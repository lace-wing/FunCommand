using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunCommand
{
    public static class MiscUtil
    {
        public static int GetTotalSaving(Player player)
        {
            long num = Utils.CoinsCount(out _, player.inventory, new[] { 58, 57, 56, 55, 54 });
            long num2 = Utils.CoinsCount(out _, player.bank.item, Array.Empty<int>());
            long num3 = Utils.CoinsCount(out _, player.bank2.item, Array.Empty<int>());
            long num4 = Utils.CoinsCount(out _, player.bank3.item, Array.Empty<int>());
            long num5 = Utils.CoinsCombineStacks(out _, new[] { num, num2, num3, num4 });
            return (int)num5;
        }
        /// <summary>
        /// p, g, s, c
        /// </summary>
        /// <param name="copper"></param>
        /// <returns></returns>
        public static int[] CuToSci(int copper)
        {
            int[] sci = new int[4];
            for (int i = 3; i > 0; sci[i] = copper % 100, copper /= 100, i--) ;
            sci[0] += copper;
            return sci;
        }
        public static int GetReforgePrice(Item item)
        {
            int price = item.value * item.stack;
            bool canDiscount = true;
            if (ItemLoader.ReforgePrice(item, ref price, ref canDiscount))
            {
                if (canDiscount && Main.LocalPlayer.discount)
                {
                    price = (int)(price * 0.8);
                }
                price = (int)(price * Main.player[item.whoAmI].currentShoppingSettings.PriceAdjustment);
                price /= 3;
            }
            return price;
        }
    }
}
