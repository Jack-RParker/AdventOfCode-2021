using AdventOfCode.Properties;
using System;

namespace AdventOfCode
{
    class Day02 : Day
    {
        public override void Run()
        {
            DisplayOuput(RunParts());
        }
        Output RunParts()
        {
            Move[] data = ProcessData();
            string p1 = Part1(data);
            string p2 = Part2(data);
            return new Output(p1, p2);
        }

        Move[] ProcessData()
        {
            string[] input = Resources.Day2Input.Split('\n');
            Move[] output = new Move[input.Length - 1];
            for (int i = 0; i < input.Length - 1; i++)
            {
                string[] s = input[i].Split(' ');
                output[i] = new Move(s[0], Int32.Parse(s[1]));
            }
            return output;
        }
        string Part1(Move[] data)
        {
            int forward = 0;
            int depth = 0;
            foreach (Move move in data)
            {
                switch (move.dir)
                {
                    case "forward":
                        forward += move.dist;
                        break;
                    case "down":
                        depth += move.dist;
                        break;
                    case "up":
                        depth -= move.dist;
                        break;
                }
            }
            return (forward * depth).ToString();
        }
        string Part2(Move[] data)
        {
            int forward = 0;
            int depth = 0;
            int aim = 0;
            foreach (Move move in data)
            {
                switch (move.dir)
                {
                    case "forward":
                        forward += move.dist;
                        depth += aim * move.dist;
                        break;
                    case "down":
                        aim += move.dist;
                        break;
                    case "up":
                        aim -= move.dist;
                        break;
                }
            }
            return (forward * depth).ToString();
        }
        struct Move
        {
            public string dir;
            public int dist;

            public Move(string dir, int dist)
            {
                this.dir = dir;
                this.dist = dist;
            }
        }
    }
}
