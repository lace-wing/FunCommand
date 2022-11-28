using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace FunCommand
{
    public class CommandPlayer : ModPlayer
    {
        public int sprayWaterTimer;

        public override void ResetEffects()
        {
            sprayWaterTimer = Math.Max(--sprayWaterTimer, 0);
        }

        public override void PostUpdateEquips()
        {
            if (sprayWaterTimer > 0 && sprayWaterTimer % 60 == 0)
            {
                for (int i = 0; i < Main.rand.Next(4); i++)
                {
                    Vector2 vel = new Vector2(Player.direction, 0).RotatedBy(Math.PI * 0.28 * Player.direction).RotatedByRandom(Math.PI * 0.22 * Player.direction) * Main.rand.NextFloat(1.1f, 3.3f) + Player.velocity * 0.33f;
                    Projectile water = Projectile.NewProjectileDirect(Player.GetSource_Misc("SprayWater"), Player.MountedCenter + new Vector2(0, 4), vel, ProjectileID.WaterGun, 0, 0);
                }
            }
        }
    }
}
