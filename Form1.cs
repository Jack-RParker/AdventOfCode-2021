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
            day = new Day01();
        }

        private void Day2_Click(object sender, EventArgs e)
        {
            Heading.Text = "Day 2";
            day = new Day02();
        }

        private void Day3_Click(object sender, EventArgs e)
        {
            Heading.Text = "Day 3";
            day = new Day03();
        }

        private void Day4_Click(object sender, EventArgs e)
        {
            Heading.Text = "Day 4";
            day = new Day04();
        }

        private void Day5_Click(object sender, EventArgs e)
        {
            Heading.Text = "Day 5";
            day = new Day05();
        }

        private void Day6_Click(object sender, EventArgs e)
        {
            Heading.Text = "Day 6";
            day = new Day06();
        }

        private void Day7_Click(object sender, EventArgs e)
        {
            Heading.Text = "Day 7";
            day = new Day07();
        }

        private void Day8_Click(object sender, EventArgs e)
        {
            Heading.Text = "Day 8";
            day = new Day08();
        }

        private void Day9_Click(object sender, EventArgs e)
        {
            Heading.Text = "Day 9";
            day = new Day09();
        }

        private void Day10_Click(object sender, EventArgs e)
        {
            Heading.Text = "Day 10";
            day = new Day10();
        }

        private void Day11_Click(object sender, EventArgs e)
        {
            Heading.Text = "Day 11";
            day = new Day11();
        }
    }
}
