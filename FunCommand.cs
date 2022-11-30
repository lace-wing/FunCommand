global using Terraria;
global using Terraria.ModLoader;
global using static FunCommand.CommandUtil;
using Terraria.ID;
using static FunCommand.SpawnUtil;

namespace FunCommand
{
	public class FunCommand : Mod
	{
        public static SpawnBatchPool wormRainPool;
        public static int[] wormType = new int[]
        {
            // Devourer heads
            NPCID.DevourerHead, NPCID.SeekerHead, 
            // Giant worm heads
            NPCID.GiantWormHead, NPCID.DiggerHead, 
            // Desert worm heads
            NPCID.TombCrawlerHead, NPCID.DuneSplicerHead, 
            // Leech head
            NPCID.LeechHead, 
            // Milkyway Weaver head
            NPCID.StardustWormHead, 
            // Normal worms
            NPCID.Worm, NPCID.EnchantedNightcrawler, 
            // Treasure worms
            NPCID.GoldWorm, NPCID.TruffleWormDigger
        };
        public static int[] wormWeight = new int[]
        {
            6, 4,
            12, 10,
            7, 5,
            4,
            2,
            1, 1
        };

        public override void PostSetupContent()
        {
            SetUpSets();
        }
        public static void SetUpSets()
        {
            wormRainPool.Initialize(wormType.Length);
            wormRainPool.type = wormType;
            wormRainPool.weight = wormWeight;
            wormRainPool.randomType = true;
            wormRainPool.totalType = 3;
            for (int i = 0; i < wormRainPool.length; i++)
            {
                wormRainPool.ignoreTile[i] = true;
            }
        }
    }
}