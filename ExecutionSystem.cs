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
