using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.IO;

namespace FunCommand
{
    public class ExecutionSystem : ModSystem
    {
        public static ExecutionSystem Instance { get => ModContent.GetInstance<ExecutionSystem>(); }

        public int worldReforgeCost;

        public static List<NPC> worms = new List<NPC>();
        public int shouldKillWorms = 0;

        public override void PostUpdateNPCs()
        {
            Predicate<NPC> npcToRemove = npc => npc == null || !npc.active;
            worms.RemoveAll(npcToRemove);
            foreach (Player player in Main.player)
            {
                if (player.active)
                {
                    shouldKillWorms += player.GetModPlayer<ExecutionPlayer>().wormRainRimer <= 0 ? 1 : 0;
                }
            }
            if (shouldKillWorms > 0)
            {
                worms.ForEach(npc =>
                {
                    npc.active = false;
                });
                shouldKillWorms = 0;
            }
        }
        public override void OnWorldLoad()
        {
            worldReforgeCost = 0;
        }
        public override void OnWorldUnload()
        {
            worldReforgeCost = 0;
        }
        public override void SaveWorldData(TagCompound tag)
        {
            tag["worldReforgeCost"] = worldReforgeCost;
        }
        public override void LoadWorldData(TagCompound tag)
        {
            worldReforgeCost = tag.GetInt("worldReforgeCost");
        }
        public override void NetSend(BinaryWriter writer)
        {
            writer.Write(worldReforgeCost);
        }
        public override void NetReceive(BinaryReader reader)
        {
            worldReforgeCost = reader.ReadInt32();
        }
    }
}
