﻿using System.Windows.Forms;

namespace AdventOfCode
{
    public abstract class Day
    {
        static readonly Menu menu = Application.OpenForms[0] as Menu;
        public static void Print(string s, string end = "\n")
        {
            string text = menu.Display.Text;
            menu.Display.Text = text + s + end;
        }
        public void Print(int i, string end = "\n")
        {
            Print(i.ToString(), end);
        }
        public void DisplayOuput(Output output)
        {
            menu.Part1Out.Text = output.p1;
            menu.Part2Out.Text = output.p2;
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
