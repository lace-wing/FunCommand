using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;

namespace FunCommand
{
    public class ExecutionItem : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public bool checkReforge;
        public int reforgeCost;
        bool bestiaryNotesAdded = false;
        public override bool PreReforge(Item item)
        {
            reforgeCost = MiscUtil.GetReforgePrice(item);
            checkReforge = true;
            return base.PreReforge(item);
        }
        public override void PostReforge(Item item)
        {
            if (checkReforge)
            {
                Player player = Main.player[item.whoAmI];
                player.GetModPlayer<ExecutionPlayer>().playerReforgeCost += reforgeCost;
                ExecutionSystem.Instance.worldReforgeCost += reforgeCost;
            }
            checkReforge = false;
            reforgeCost = 0;
            base.PostReforge(item);
        }
        public override void OnSpawn(Item item, IEntitySource source)
        {
            if (!Main.hardMode)
            {
                if (ExecutionSystem.worms.Count > 0)
                {
                    if (item.type == ItemID.CursedFlame && source is EntitySource_Loot loot && loot.Entity is NPC npc && npc.type == NPCID.SeekerHead)
                    {
                        item.active = false;
                    }
                }
            }
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type == ItemID.CursedFlame && !bestiaryNotesAdded)
            {
                item.BestiaryNotes += $"\n{ComText("WormRainHardmodeCondition")}";
                bestiaryNotesAdded = true;
            }
        }
    }
}
