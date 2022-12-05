using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.UI;
using FunCommand.Items;

namespace FunCommand
{
    public class ExecutionNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public int spawnTimer;

        public override void ResetEffects(NPC npc)
        {
            if (ExecutionSystem.worms.Contains(npc))
            {
                spawnTimer = Math.Max(++spawnTimer, 0); ;
            }
            else
            {
                spawnTimer = 0;
            }
        }
        public override bool PreAI(NPC npc)
        {
            if (npc.type == NPCID.Plantera)
            {
                ExecutionSystem.Instance.planteraAlive = true;
            }
            return base.PreAI(npc);
        }
        public override void PostAI(NPC npc)
        {
            if (ExecutionSystem.worms.Contains(npc))
            {
                if (spawnTimer < 12)
                {
                    npc.velocity.Y += 3;
                    npc.velocity.X *= 0.33f;
                }
                if (spawnTimer == 30)
                {
                    npc.AddBuff(BuffID.Lovestruck, 150);
                    EmoteBubble.NewBubble(EmoteID.BossEoW, new WorldUIAnchor(npc), 120);
                }
            }
            if (ExecutionSystem.Instance.fullOfLoveTimer > 0)
            {
                if (ExecutionSystem.Instance.fullOfLoveTimer % 120 == 0 && Main.rand.NextBool(6))
                {
                    EmoteBubble.NewBubble(EmoteID.EmotionLove, new WorldUIAnchor(npc), 120);
                }
                if (npc.FindBuffIndex(BuffID.Lovestruck) < -1)
                {
                    npc.buffTime[npc.FindBuffIndex(BuffID.Lovestruck)] += 1;
                }
                else npc.AddBuff(BuffID.Lovestruck, 2);
            }
        }
        public override void ModifyHitPlayer(NPC npc, Player target, ref int damage, ref bool crit)
        {
            if (ExecutionSystem.Instance.fullOfLoveTimer < 0)
            {
                damage = (int)(damage * 0.75f);
                crit = crit && Main.rand.NextBool();
            }
        }
        public override Color? GetAlpha(NPC npc, Color drawColor)
        {
            if (ExecutionSystem.Instance.pinkTimer > 0)
            {
                drawColor.R = 255;
                drawColor.G = 192;
                drawColor.B = 203;
            }
            return drawColor;
        }
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            //if (npc.type == NPCID.SeekerHead)
            //{
            //    List<IItemDropRule> rules = new List<IItemDropRule>();
            //    npcLoot.RemoveWhere(rule => 
            //    {
            //        if (rule is CommonDrop drop && drop.itemId == ItemID.CursedFlame)
            //        {
            //            rules.Add(rule);
            //            return true;
            //        }
            //        return false;
            //    });
            //    IItemDropRuleCondition condition = new WormRainHardmodeCondition();
            //    rules.ForEach(action => 
            //    {
            //        if (action is CommonDrop drop)
            //        {
            //            npcLoot.Add(ItemDropRule.ByCondition(condition, drop.itemId, drop.chanceDenominator, drop.amountDroppedMinimum, drop.amountDroppedMaximum, drop.chanceNumerator));
            //        }
            //    });
            //}
        }
    }
}
