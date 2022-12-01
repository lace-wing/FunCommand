using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader.IO;

namespace FunCommand
{
    public class ExecutionPlayer : ModPlayer
    {
        public int sprayWaterTimer;
        public int wormRainRimer;

        public int playerReforgeCost;

        public Rectangle screen;
        public Rectangle sky;

        public override void ResetEffects()
        {
            sprayWaterTimer = Math.Max(--sprayWaterTimer, 0);
            wormRainRimer = Math.Max(--wormRainRimer, 0);

            screen = new Rectangle((int)Player.Center.X - Main.maxScreenW / 2, (int)Player.Center.Y - Main.maxScreenH / 2, Main.maxScreenW, Main.maxScreenH);
            sky = new Rectangle((int)Player.Center.X - Main.maxScreenW / 2, (int)Player.Center.Y - Main.maxScreenH / 2, Main.maxScreenW, Main.maxScreenH / 8);
        }
        public override void PostUpdateEquips()
        {
            if (sprayWaterTimer > 0 && sprayWaterTimer % 60 == 0 && Main.myPlayer == Player.whoAmI)
            {
                for (int i = 0; i < Main.rand.Next(4); i++)
                {
                    Vector2 vel = new Vector2(Player.direction, 0).RotatedBy(Math.PI * 0.28 * Player.direction).RotatedByRandom(Math.PI * 0.22 * Player.direction) * Main.rand.NextFloat(1.1f, 3.3f) + Player.velocity * 0.33f;
                    Projectile water = Projectile.NewProjectileDirect(Player.GetSource_Misc("SprayWater"), Player.MountedCenter + new Vector2(0, 4), vel, ProjectileID.WaterGun, 0, 0);
                }
            }
            if (wormRainRimer > 0 && wormRainRimer % 60 == 0 && Main.myPlayer == Player.whoAmI && Main.npc.Count(a => a is NPC npc && npc.active) < 120)
            {
                Task task = new Task(() =>
                {
                    for (int i = 0; i < 4 - Main.npc.Count(a => a is NPC npc && npc.active) / 40; i++)
                    {
                        NPC[] worm = SpawnUtil.SpawnNPCBatch(Player.GetSource_Misc("WormRain"), sky, default, FunCommand.wormRainPool);
                        foreach (NPC npc in worm)
                        {
                            ExecutionSystem.worms.Add(npc);
                        }
                    }
                });
                task.Start();
            }
        }
        public override void SaveData(TagCompound tag)
        {
            tag.Add("playerReforgeCost", playerReforgeCost);
        }
        public override void LoadData(TagCompound tag)
        {
            playerReforgeCost = tag.GetInt("playerReforgeCost");
        }
    }
}
