using Terraria.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunCommand
{
    public static class CommandUtil
    {
        public static string CommonKey = "Mods.FunCommand.Common.";
        public static string CommandsKey = "Mods.FunCommand.Commands.";
        public static string ActionsKey = "Mods.FunCommand.CommandActions.";

        public static string ComText(string key)
        {
            return Language.GetTextValue(CommonKey + key);
        }
        /// <summary>
        /// Get the text value of Mods.FunCommand.Commands.key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string CmdText(string key)
        {
            return Language.GetTextValue(CommandsKey + key);
        }
        public static string AxnText(string key)
        {
            return Language.GetTextValue(ActionsKey + key);
        }
        public static string CmdTextImdt(string key)
        {
            ModTranslation translation = LocalizationLoader.GetOrCreateTranslation(CommandsKey + key);
            return translation.GetTranslation(Language.ActiveCulture);
        }
    }
}
