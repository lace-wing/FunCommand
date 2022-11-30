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
                        string axnList = "***";
                        if (QueryLang(para1, out bool en, out bool zh))
                        {
                            for (int i = 1; i < ActionName.Length; i++)
                            {

                                axnList += $"\n{AxnUsage(ActionName[i], en, zh)}";
                            }
                        }
                        axnList += "\n***";
                        Main.NewText(axnList, Colors.RarityGreen);
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
                #region Player Reforge Cost
                else if (name == ActionName[6])
                {
                    if (QueryAxn(para1, name, para2))
                    {
                        if (ParaTrig("Clear").Contains(para1))
                        {
                            if (QueryPara(para2, "Clear"))
                            {
                                mPlayer.playerReforgeCost = 0;
                                return;
                            }
                        }
                        else if (para1 != default)
                        {
                            ReplyInvalidPara(para1);
                            return;
                        }
                        int[] count = CuToSci(mPlayer.playerReforgeCost);
                        Main.NewText($"{ComText("IHaveSpent")} {count[0]} {ComText("Platinum")} {count[1]} {ComText("Gold")} {count[2]} {ComText("Silver")} {count[3]} {ComText("Copper")} {ComText("ToReforge")}");
                    }
                }
                #endregion
                #region WorldReforgeCost
                else if (name == ActionName[7])
                {
                    if (QueryAxn(para1, name, para2))
                    {
                        if (ParaTrig("Clear").Contains(para1))
                        {
                            if (QueryPara(para2, "Clear"))
                            {
                                ExecutionSystem.Instance.worldReforgeCost = 0;
                            }
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
                #endregion
            }
        }
    }
}
