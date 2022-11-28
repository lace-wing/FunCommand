using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

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
            player.TryGetModPlayer(out CommandPlayer mPlayer);
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
                #region Help
                if (DetermineAxn(determinant, "Help"))
                {
                    if (CheckUsage(para1, "Help")) ;
                    else if (para1 != default)
                    {
                        ReplyInvalidPara(para1);
                    }
                    else 
                    {
                        Main.NewText(AxnText("Help.HelpList"));
                    }
                }
                #endregion
                #region Actions
                else if (DetermineAxn(determinant, "Actions"))
                {
                    if (CheckUsage(para1, "Actions")) ;
                    else if (para1 != default)
                    {
                        ReplyInvalidPara(para1);
                    }
                    else
                    {
                        Main.NewText(AxnText("Actions.ActionList"));
                    }
                }
                #endregion
                #region Tips
                else if (DetermineAxn(determinant, "Tips"))
                {
                    if (CheckUsage(para1, "Tips")) ;
                    else if (para1 != default)
                    {
                        ReplyInvalidPara(para1);
                    }
                    else
                    {
                        Main.NewText(AxnText("Tips.TipList"));
                    }
                }
                #endregion
                #region SprayWater
                else if (DetermineAxn(determinant, "SprayWater"))
                {
                    if (para1 == default)
                    {
                        mPlayer.sprayWaterTimer = 600;
                    }
                    else if (CheckUsage(para1, "SprayWater")) ;
                    else if (!int.TryParse(para1, out int time) || time < 0)
                    {
                        Main.NewText($"\"{para1}\" {ComText("IsNotAValid")} {ComText("Int")}, {ComText("Range")}: >=0", Colors.RarityRed);
                    }
                    else
                    {
                        mPlayer.sprayWaterTimer = time;
                    }
                }
                #endregion
                #region Invalid Action
                else
                {
                    ReplyInvalidAction(determinant);
                }
                #endregion
            }
        }
        private bool DetermineAxn(string determinant, string name)
        {
            return determinant.ToLower() == AxnText($"{name}.Name_En") || determinant == AxnText($"{name}.Name_Zh");
        }
        private bool CheckUsage(string para, string name)
        {
            bool check = para == "?" || para == "？";
            if (check)
            {
                ShowUsage(name);
            }
            return check;
        }
        private void ShowAbout()
        {
            Main.NewText(ComText("About"), Colors.RarityYellow);
        }
        private void ShowUsage(string name)
        {
            Main.NewText(AxnText($"{name}.Usage"), Colors.RarityYellow);
        }
        private void ReplyInvalidAction(string action)
        {
            Main.NewText($"\"{action}\" {ComText("IsNotAValid")} {ComText("Action")}. {ComText("SeeActions")}", Colors.RarityRed);
        }
        private void ReplyInvalidPara(string para)
        {
            Main.NewText($"\"{para}\" {ComText("IsNotAValid")} {ComText("Para")}. {ComText("SeeActions")}", Colors.RarityRed);
        }
    }
}
