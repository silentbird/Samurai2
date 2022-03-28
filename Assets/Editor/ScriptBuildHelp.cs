using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


namespace CustomTool
{
    public class ScriptBuildHelp
    {
        private StringBuilder _stringBuilder;
        private string _lineBrake = "\r\n";
        private int currentIndex = 0;
        public int IndentTimes { get; set; }

        public ScriptBuildHelp()
        {
            _stringBuilder = new StringBuilder();
        }

        private void Write(string context, bool needIndent = false)
        {
            if (needIndent)
            {
                context = GetIndent() + context;
            }

            if (currentIndex == _stringBuilder.Length)
            {
                _stringBuilder.Append(context);
            }
            else
            {
                _stringBuilder.Insert(currentIndex, context);
            }

            currentIndex += context.Length;
        }

        private void WriteLine(string context, bool needIndent = false)
        {
            Write(context + _lineBrake, needIndent);
        }

        private string GetIndent()
        {
            string indent = "";
            for (int i = 0; i < IndentTimes; i++)
            {
                indent += "    ";
            }

            return indent;
        }

        public void WriteEmptyLine()
        {
            WriteLine("");
        }
        
        public void WriteCurlyBrackets()
        {
            var start = GetIndent() + _lineBrake + GetIndent() + "{" + _lineBrake;
            var end = GetIndent() + "}" + _lineBrake;

            Write(start + end, true);
            currentIndex -= end.Length;
        }

        public void WriteUsing(string nameSpacename)
        {
            WriteLine("using " + nameSpacename + ";");
        }

        public void WriteNamespace(string name)
        {
            Write("namespace " + name);
            WriteCurlyBrackets();
        }

        public void WriteClass(string name)
        {
            Write("public class " + name + " : MonoBehaviour", true);
            WriteCurlyBrackets();
        }

        public void WriteFun(string name, params string[] paramNames)
        {
            StringBuilder tmp = new StringBuilder();
            tmp.Append("public void " + name + "()");
            foreach (var s in paramNames)
            {
                tmp.Insert(tmp.Length - 1, s + ",");
            }

            if (paramNames.Length > 0)
            {
                tmp.Remove(tmp.Length - 2, 1);
            }

            Write(tmp.ToString(), true);
            WriteCurlyBrackets();
        }

        public override string ToString()
        {
            return _stringBuilder.ToString();
        }
    }
}