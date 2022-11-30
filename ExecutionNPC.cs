using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace FunCommand
{
    public class ExecutionNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public static List<NPC> worms = new List<NPC>();
        public int spawnTimer;

        public override void ResetEffects(NPC npc)
        {
            Predicate<NPC> npcToRemove = npc => npc == null || !npc.active;
            worms.RemoveAll(npcToRemove);
            if (worms.Contains(npc))
            {
                spawnTimer = Math.Max(++spawnTimer, 0); ;
            }
            else
            {
                spawnTimer = 0;
            }
        }
        public override void PostAI(NPC npc)
        {
            if (worms.Contains(npc))
            {
                if (spawnTimer < 4)
                {
                    npc.velocity.Y += 3;
                }
                npc.AddBuff(BuffID.Lovestruck, 2);
            }
        }
        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
        }
    }
}
