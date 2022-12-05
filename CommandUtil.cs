using Terraria.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Terraria.ID;
using IL.Terraria.GameContent.Generation;

namespace FunCommand
{
    public static class CommandUtil
    {
        public static string CommonKey = "Mods.FunCommand.Common.";
        public static string CommandsKey = "Mods.FunCommand.Commands.";
        public static string ActionsKey = "Mods.FunCommand.CommandActions.";
        public static string ParasKey = "Mods.FunCommand.ActionParameters.";

        /// <summary>
        /// A string[] which contains names of all actions
        /// </summary>
        public static string[] ActionName = new string[]
        {
            "InvalidAction",
            "Help",
            "Actions",
            "Tips",
            "SprayWater",
            "WormRain",
            "PlayerReforgeCost",
            "WorldReforgeCost", 
            "FullOfLove", 
            "Pink", 
            "Plantera"
        };

        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <returns>The value of Common.key</returns>
        public static string ComText(string key)
        {
            return Language.GetTextValue(CommonKey + key);
        }
        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <returns>The value of Commands.key</returns>
        public static string CmdText(string key)
        {
            return Language.GetTextValue(CommandsKey + key);
        }
        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <returns>The value of CommandActions.key</returns>
        public static string AxnText(string key)
        {
            return Language.GetTextValue(ActionsKey + key);
        }
        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <returns>The value of ActionParameters.key</returns>
        public static string ParaText(string key)
        {
            return Language.GetTextValue(ParasKey + key);
        }
        /// <summary>
        /// </summary>
        /// <param name="name"></param>
        /// <returns>A string[] contains all triggers of the action</returns>
        public static string[] AxnTrig(string name)
        {
            List<string> trig = new List<string>();
            trig.Add(AxnText(name + ".Trigger_En"));
            trig.Add(AxnText(name + ".Trigger_Zh"));
            return trig.ToArray();
        }
        /// <summary>
        /// </summary>
        /// <param name="name"></param>
        /// <returns>A string[] contains all paras of the action</returns>
        public static string[] AxnPara(string name)
        {
            List<string> para = new List<string>();
            para.Add(AxnText(name + ".Para_En"));
            para.Add(AxnText(name + ".Para_Zh"));
            return para.ToArray();
        }
        /// <summary>
        /// </summary>
        /// <param name="name"></param>
        /// <returns>A string[] contains all triggers of the parameter's action</returns>
        public static string[] ParaTrig(string name)
        {
            List<string> trig = new List<string>();
            trig.Add(ParaText(name + ".Trigger_En"));
            trig.Add(ParaText(name + ".Trigger_Zh"));
            return trig.ToArray();
        }
        /// <summary>
        /// Get the Command text value before localization is loaded
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string CmdTextImdt(string key)
        {
            ModTranslation translation = LocalizationLoader.GetOrCreateTranslation(CommandsKey + key);
            return translation.GetTranslation(Language.ActiveCulture);
        }

        /// <summary>
        /// Check what action it is
        /// </summary>
        /// <param name="determinant"></param>
        /// <returns>Name of the action triggered by the determinant</returns>
        public static string CheckAxn(string determinant)
        {
            string name = ActionName[0];
            determinant = determinant.ToLower();
            foreach (string n in ActionName)
            {
                foreach (string trigger in AxnTrig(n))
                {
                    if (determinant == trigger)
                    {
                        name = $"{n}";
                        return name;
                    }
                }
            }
            return name;
        }
        /// <summary>
        /// Check if the action is queried and show Usage if yes, returns true if not
        /// </summary>
        /// <param name="para"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool QueryAxn(string para, string name, string lang = default)
        {
            bool go = true;
            bool en = true, zh = true;
            if (para == "?" || para == "？")
            {
                if (QueryLang(lang, out en, out zh))
                {
                    Main.NewText(AxnUsage(name, en, zh), Colors.RarityYellow);
                }
                go = false;
            }
            return go;
        }
        public static bool QueryPara(string para, string name)
        {
            bool go = true;
            if (para == "?" || para == "？")
            {
                Main.NewText(ParaText($"{name}.Usage"));
                return !go;
            }
            return go;
        }
        /// <summary>
        /// Check if the lang triggers En or Zh, triggers both when lang = default
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="en"></param>
        /// <param name="zh"></param>
        /// <returns>true is the lang is supported</returns>
        public static bool QueryLang(string lang, out bool en, out bool zh)
        {
            bool go = true;
            en = zh = true;
            if (lang != default)
            {
                lang = lang.ToLower();
                if (ParaTrig("En").Contains(lang))
                {
                    zh = false;
                }
                else if (ParaTrig("Zh").Contains(lang))
                {
                    en = false;
                }
                if (!en && !zh)
                {
                    Main.NewText($"\"{lang}\" {ComText("IsNotASupported")} {ComText("Language")}", Colors.RarityRed);
                    go = false;
                }
            }
            return go;
        }
        public static void ShowAbout()
        {
            Main.NewText(ComText("About"), Colors.RarityYellow);
        }
        public static string AxnUsage(string name, bool en = true, bool zh = true)
        {
            string trigger = "" , para = "", usage = AxnText(name + ".Usage"), result = "";
            if (en)
            {
                trigger += AxnTrig(name)[0];
                para += AxnPara(name)[0];
            }
            if (en && zh)
            {
                trigger += "/";
                para += "/";
            }
            if (zh)
            {
                trigger += AxnTrig(name)[1];
                para += AxnPara(name)[1];
            }
            result = $"{trigger} {para} - {usage}";
            return result;
        }
        public static void ReplyInvalidAction(string action)
        {
            Main.NewText($"\"{action}\" {ComText("IsNotAValid")} {ComText("Action")}. {ComText("SeeActions")}", Colors.RarityRed);
        }
        public static void ReplyInvalidPara(string para)
        {
            Main.NewText($"\"{para}\" {ComText("IsNotAValid")} {ComText("Para")}. {ComText("SeeActions")}", Colors.RarityRed);
        }
    }
}
