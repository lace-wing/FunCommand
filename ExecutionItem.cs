using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunCommand
{
    public class ExecutionItem : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public bool checkReforge;
        public int reforgeCost;
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
    }
}
