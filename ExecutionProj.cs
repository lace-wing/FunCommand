using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunCommand
{
    public class ExecutionProj : GlobalProjectile
    {
        public override void ModifyHitPlayer(Projectile projectile, Player target, ref int damage, ref bool crit)
        {
            if (ExecutionSystem.Instance.fullOfLoveTimer > 0)
            {
                damage = (int)(damage * 0.5f);
                crit = crit && Main.rand.NextBool();
            }
        }
    }
}
