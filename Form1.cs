using System;
using System.Windows.Forms;

namespace AdventOfCode
{
    public partial class Menu : Form
    {
        Day day;

        public Menu()
        {
            InitializeComponent();
        }
        private void RunButton_Click(object sender, EventArgs e)
        {
            if (day != null) day.Run();
            else Display.Text = "Select a Day first";
        }

        private void Day1_Click(object sender, EventArgs e)
        {
            Heading.Text = "Day 1";
            day = new Day1();
        }

        private void Day2_Click(object sender, EventArgs e)
        {
            Heading.Text = "Day 2";
            day = new Day2();
        }

        private void Day3_Click(object sender, EventArgs e)
        {
            Heading.Text = "Day 3";
            day = new Day3();
        }

        private void Day4_Click(object sender, EventArgs e)
        {
            Heading.Text = "Day 4";
            day = new Day4();
        }

        private void Day5_Click(object sender, EventArgs e)
        {
            Heading.Text = "Day 5";
            day = new Day5();
        }

        private void Day6_Click(object sender, EventArgs e)
        {
            Heading.Text = "Day 6";
            day = new Day6();
        }

        private void Day7_Click(object sender, EventArgs e)
        {
            Heading.Text = "Day 7";
            day = new Day7();
        }
    }
}
