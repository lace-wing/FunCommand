using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using static FunCommand.MiscUtil;

namespace FunCommand.Commands
{
    public class Fun : ModCommand
    {
        public override CommandType Type => CommandType.Chat;
        public override string Command => CmdTextImdt("Fun.Command");
        public override string Description => CmdTextImdt("Fun.Desc");
        public override string Usage => CmdTextImdt("Fun.Usage");

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            Player player = caller.Player;
            player.TryGetModPlayer(out ExecutionPlayer mPlayer);

            if (args.Length == 0)
            {
                ShowAbout();
            }
            else
            {
                string determinant = default, para1 = default, para2 = default, para3 = default;
                for (int i = 0; i < args.Length; i++)
                {
                    switch (i)
                    {
                        case 0: 
                            determinant = args[i];
                            break;
                        case 1: 
                            para1 = args[i];
                            break;
                        case 2:
                            para2 = args[i];
                            break;
                        case 3:
                            para3 = args[i];
                            break;
                        default:
                            break;
                    }
                }
                string name = CheckAxn(determinant);

                #region InvalidAction
                if (name == ActionName[0])
                {
                    if (QueryAxn(para1, name, para2))
                    {
                        ReplyInvalidAction(determinant);
                        return;
                    }
                }
                #endregion
                #region Help
                else if (name == ActionName[1])
                {
                    if (QueryAxn(para1, name, para2))
                    {
                        if (para1 != default)
                        {
                            ReplyInvalidPara(para1);
                            return;
                        }
                        Main.NewText(AxnText($"{name}.HelpList"), Colors.RarityYellow);
                    }
                }
                #endregion
                #region Actions
                else if (name == ActionName[2])
                {
                    if (QueryAxn(para1, name, para2))
                    {
                        if (QueryPara(para1, para2, QueriablePara[2], para3))
                        {
                            int batches = ActionName.Length / 10 + 1;
                            string axnList, warp;
                            Main.NewText("***");
                            if (QueryLang(para1, out bool en, out bool zh))
                            {
                                for (int b = 0; b < batches; b++)
                                {
                                    axnList = "";
                                    for (int i = b * 10; i < ActionName.Length && i < b * 10 + 10; i++)
                                    {
                                        warp = i == 0 ? "" : "\n";
                                        axnList += $"{warp}{AxnUsage(ActionName[i], en, zh)}";
                                    }
                                    Main.NewText(axnList, Colors.RarityGreen);
                                }
                            }
                            Main.NewText("***");
                        }
                    }
                }
                #endregion
                #region Tips
                else if (name == ActionName[3])
                {
                    if (QueryAxn(para1, name, para2))
                    {
                        if (para1 != default)
                        {
                            ReplyInvalidPara(para1);
                            return;
                        }
                        Main.NewText(AxnText($"{name}.TipList"), Colors.RarityBlue);
                    }
                }
                #endregion
                #region SprayWater
                else if (name == ActionName[4])
                {
                    if (QueryAxn(para1, name, para2))
                    {
                        int time = 600;
                        if (para1 == default)
                        {
                            mPlayer.sprayWaterTimer = time;
                            return;
                        }
                        else if (!int.TryParse(para1, out time) || time < 0)
                        {
                            Main.NewText($"\"{para1}\" {ComText("IsNotAValid")} {ComText("Int")}, {ComText("Range")}: >=0", Colors.RarityRed);
                            return;
                        }
                        mPlayer.sprayWaterTimer = time;
                    }
                }
                #endregion
                #region WormRain
                else if (name == ActionName[5])
                {
                    if (QueryAxn(para1, name, para2))
                    {
                        int time = 600;
                        if (para1 == default)
                        {
                            mPlayer.wormRainRimer = time;
                            return;
                        }
                        else if (!int.TryParse(para1, out time) || time < 0)
                        {
                            Main.NewText($"\"{para1}\" {ComText("IsNotAValid")} {ComText("Int")}, {ComText("Range")}: >=0", Colors.RarityRed);
                            return;
                        }
                        mPlayer.wormRainRimer = time;
                    }
                }
                #endregion
                #region PlayerReforgeCost
                else if (name == ActionName[6])
                {
                    if (QueryAxn(para1, name, para2))
                    {
                        if (QueryPara(para1, para2, QueriablePara[6], para3))
                        {
                            if (ParaTrig("Clear").Contains(para1))
                            {
                                mPlayer.playerReforgeCost = 0;
                            }
                            else if (para1 != default)
                            {
                                ReplyInvalidPara(para1);
                            }
                            else
                            {
                                int[] count = CuToSci(mPlayer.playerReforgeCost);
                                Main.NewText($"{ComText("IHaveSpent")} {count[0]} {ComText("Platinum")} {count[1]} {ComText("Gold")} {count[2]} {ComText("Silver")} {count[3]} {ComText("Copper")} {ComText("ToReforge")}");
                            }
                        }
                    }
                }
                #endregion
                #region WorldReforgeCost
                else if (name == ActionName[7])
                {
                    if (QueryAxn(para1, name, para2))
                    {
                        if (QueryPara(para1, para2, QueriablePara[7], para3))
                        {
                            if (ParaTrig("Clear").Contains(para1))
                            {
                                ExecutionSystem.Instance.worldReforgeCost = 0;
                            }
                            else if (para1 != default)
                            {
                                ReplyInvalidPara(para1);
                            }
                            else
                            {
                                int[] count = CuToSci(ExecutionSystem.Instance.worldReforgeCost);
                                Main.NewText($"{ComText("TinkersHaveEarn")} {count[0]} {ComText("Platinum")} {count[1]} {ComText("Gold")} {count[2]} {ComText("Silver")} {count[3]} {ComText("Copper")} {ComText("FromReforging")}");
                            }
                        }
                    }
                }
                #endregion
                #region FullOfLove
                else if (name == ActionName[8])
                {
                    if (QueryAxn(para1, name, para2))
                    {
                        int time = 600;
                        if (para1 == default)
                        {
                            ExecutionSystem.Instance.fullOfLoveTimer = time;
                            return;
                        }
                        else if (!int.TryParse(para1, out time) || time < 0)
                        {
                            Main.NewText($"\"{para1}\" {ComText("IsNotAValid")} {ComText("Int")}, {ComText("Range")}: >=0", Colors.RarityRed);
                            return;
                        }
                        ExecutionSystem.Instance.fullOfLoveTimer = time;
                    }
                }
                #endregion
                #region Pink
                else if (name == ActionName[9])
                {
                    if (QueryAxn(para1, name, para2))
                    {
                        int time = 600;
                        if (para1 == default)
                        {
                            ExecutionSystem.Instance.pinkTimer = time;
                            return;
                        }
                        else if (!int.TryParse(para1, out time) || time < 0)
                        {
                            Main.NewText($"\"{para1}\" {ComText("IsNotAValid")} {ComText("Int")}, {ComText("Range")}: >=0", Colors.RarityRed);
                            return;
                        }
                        ExecutionSystem.Instance.pinkTimer = time;
                    }
                }
                #endregion
                #region Plantera
                else if (name == ActionName[10])
                {
                    if (QueryAxn(para1, name, para2))
                    {
                        if (!NPC.downedMechBoss1 || !NPC.downedMechBoss2 || !NPC.downedMechBoss3)
                        {
                            Main.NewText($"{ComText("DefeatAllMechBossesFirst")}");
                        }
                        else if (ExecutionSystem.Instance.planteraAlive)
                        {
                            Main.NewText($"{ComText("PlanteraAlive")}");
                        }
                        else if (ExecutionSystem.Instance.planteraTimer > 0)
                        {
                            Main.NewText($"{ComText("SummoningPlantera")}");
                        }
                        else
                        {
                            ExecutionSystem.Instance.planteraSpawnPos = player.Center;
                            ExecutionSystem.Instance.planteraTimer = 360;
                            ExecutionSystem.Instance.planteraTarget = player.whoAmI;
                        }
                    }
                }
                #endregion
            }
        }
    }
}
