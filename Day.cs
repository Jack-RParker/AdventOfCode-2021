using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AdventOfCode
{
    public abstract class Day
    {
        static readonly Menu menu = Application.OpenForms[0] as Menu;

        public Day()
        {
            Clear();
            DisplayOuput(new Output());
        }
        public static void Print(string s, string end = "\n")
        {
            string text = menu.Display.Text;
            menu.Display.Text = text + s + end;
        }
        public static void Print(int i, string end = "\n")
        {
            Print(i.ToString(), end);
        }
        public static void Print(IEnumerable<int> list, string end = "\n")
        {
            foreach (var item in list)
            {
                Print(item, end);
            }
        }
        public static void Print(IEnumerable<string> list, string end = "\n")
        {
            foreach (var item in list)
            {
                Print(item, end);
            }
        }
        public static void Clear()
        {
            menu.Display.Text = string.Empty;
        }
        public void DisplayOuput(Output output)
        {
            menu.Part1Out.Text = output.p1;
            menu.Part2Out.Text = output.p2;
        }
        internal int[] StrToIntArr(string s)
        {
            string[] sArr = Array.ConvertAll(s.ToCharArray(), Convert.ToString);
            return Array.ConvertAll(sArr, int.Parse);
        }

        public abstract void Run();
        public struct Output
        {
            public string p1;
            public string p2;

            public Output(string p1, string p2)
            {
                this.p1 = p1;
                this.p2 = p2;
            }
        }
    }
}
