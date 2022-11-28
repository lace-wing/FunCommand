using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    Main.NewText(AxnText("Help.HelpList"));
                }
                #endregion
                #region Actions
                if (DetermineAxn(determinant, "Actions"))
                {
                    Main.NewText(AxnText("Actions.ActionList"));
                }
                #endregion
                #region Tips
                if (DetermineAxn(determinant, "Tips"))
                {
                    Main.NewText(AxnText("Tips.TipList"));
                }
                #endregion
                #region SprayWater
                if (DetermineAxn(determinant, "SprayWater"))
                {
                    if (para1 == default)
                    {
                        mPlayer.sprayWaterTimer = 600;
                    }
                    else if (!int.TryParse(para1, out int time))
                    {
                        throw new UsageException($"{args[0]} {ComText("IsNotAValid")} {ComText("Int")}");
                    }
                    else
                    {
                        mPlayer.sprayWaterTimer = time;
                    }
                }
                #endregion
            }
        }
        private bool DetermineAxn(string determinant, string key)
        {
            return determinant.ToLower() == AxnText($"{key}.Name_En") || determinant == AxnText($"{key}.Name_Zh");
        }
        private void ShowAbout()
        {
            Main.NewText(ComText("About"));
        }
    }
}
