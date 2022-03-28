using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace CustomTool
{
    public class AutoAddNamespace : UnityEditor.AssetModificationProcessor
    {
        private static void OnWillCreateAsset(string path)
        {
            path = path.Replace(".meta", "");
            if (path.EndsWith(".cs"))
            {
                string text = File.ReadAllText(path);
                var newTxt = GetNewScriptContext(GetClassName(text));
                File.WriteAllText(path, newTxt);
            }
        }

        private static string GetNewScriptContext(string className)
        {
            var script = new ScriptBuildHelp();
            script.WriteUsing("UnityEngine");
            script.WriteEmptyLine();
            script.WriteNamespace("UIFrame");
            script.IndentTimes++;
            script.WriteClass(className);
            script.IndentTimes++;
            script.WriteFun("Start");
            return script.ToString();
        }

        //获取类名
        public static string GetClassName(string text)
        {
            // string[] data = text.Split(' ');
            // int index = 0;
            // for (int i = 0; i < data.Length; i++)
            // {
            //     if (data[i].Contains("class"))
            //     {
            //         index = i + 1;
            //     }
            // }
            // return data.Contains(":") ? data[index].Split(':')[0] : data[index];

            //正则表达式
            // public class NewBehaviourScript : MonoBehaviour

            string patterm = "public class ([A-Za-z0-9_]+)\\s*:\\s*MonoBehaviour";
            var match = Regex.Match(text, patterm);
            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            return "";
        }
    }
}