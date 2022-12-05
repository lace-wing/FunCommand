using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.IO;
using Terraria.ID;
using Terraria.GameContent.UI;

namespace FunCommand
{
    public class ExecutionSystem : ModSystem
    {
        public static ExecutionSystem Instance { get => ModContent.GetInstance<ExecutionSystem>(); }

        public int worldReforgeCost;

        public static List<NPC> worms = new List<NPC>();
        public bool shouldKillWorms;

        public int fullOfLoveTimer = 0;

        public int pinkTimer = 0;

        public bool planteraAlive;
        public Vector2 planteraSpawnPos = new Vector2(0, 0);
        public int planteraTimer = 0;
        public int planteraTarget = 0;

        public override void PostUpdateTime()
        {
            shouldKillWorms = true;
            fullOfLoveTimer = Math.Max(--fullOfLoveTimer, 0);
            pinkTimer = Math.Max(--pinkTimer, 0);
        }
        public override void PreUpdateNPCs()
        {
            planteraAlive = false;
            planteraTimer = Math.Max(--planteraTimer, 0);
        }
        public override void PostUpdateNPCs()
        {
            Predicate<NPC> npcToRemove = npc => npc == null || !npc.active;
            worms.RemoveAll(npcToRemove);
            foreach (Player player in Main.player)
            {
                if (player.active)
                {
                    if (player.GetModPlayer<ExecutionPlayer>().wormRainRimer > 0)
                    {
                        shouldKillWorms = false;
                        break;
                    }
                }
            }
            if (shouldKillWorms)
            {
                worms.ForEach(npc =>
                {
                    npc.active = false;
                });
            }
            if (planteraTimer > 0)
            {
                for (int i = 0; i < planteraTimer; i++)
                {
                    if (Main.rand.NextBool())
                    {
                        Dust dust = Dust.NewDustPerfect(planteraSpawnPos, DustID.AncientLight, Vector2.One.RotatedBy(i) * Main.rand.NextFloat(0.9f, 4.8f), 0, Color.DarkGreen);
                        dust.noGravity = true;
                    }
                }
                if (planteraTimer == 1)
                {
                    for (int i = 0; i < 128; i++)
                    {
                        Dust dust = Dust.NewDustPerfect(planteraSpawnPos, DustID.AncientLight, Vector2.One.RotatedBy(i) * Main.rand.NextFloat(16f, 48f), 0, Color.DarkGreen, 6);
                        dust.noGravity = true;
                    }
                    NPC plantera = NPC.NewNPCDirect(NPC.GetBossSpawnSource(planteraTarget), planteraSpawnPos, NPCID.Plantera, target: planteraTarget);
                    plantera.lifeMax += 1200;
                    plantera.life += 1200;
                    plantera.defense += 12;
                    plantera.damage += 12;
                    plantera.AddBuff(BuffID.Lovestruck, 360);
                    EmoteBubble.NewBubble(EmoteID.EmoteKiss, new WorldUIAnchor(plantera), 360);
                }
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
