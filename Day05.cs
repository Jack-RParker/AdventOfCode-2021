using AdventOfCode.Properties;
using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day05 : Day
    {
        public override void Run()
        {
            DisplayOuput(RunParts());
        }

        Output RunParts()
        {
            List<Line> data = ProcessData();
            string p1 = Part1(data);
            string p2 = Part2(data);
            return new Output(p1, p2);
        }
        List<Line> ProcessData()
        {
            string[] raw = Resources.Day5Input.TrimEnd('\n').Split('\n');
            string[] s = raw[0].Split();
            List<Line> lines = new List<Line>();
            foreach (string line in raw)
            {
                string[] strNums = line.Split(new string[] { " -> ", "," }, StringSplitOptions.None);
                int[] nums = Array.ConvertAll(strNums, int.Parse);
                lines.Add(new Line(nums));
            }
            return lines;
        }
        string Part1(List<Line> data)
        {
            int[] board = new int[1000 * 1000];
            foreach (Line line in data)
            {
                Func<(int x,int y)[]> f;
                if (line.horz)
                {
                    f = line.GetHorzPoints;
                }
                else if (line.vert)
                {
                    f = line.GetVertPoints;
                }
                else continue;
                foreach ((int x ,int y) in f())
                {
                    board[x + (1000 * y)]++;
                }
            }
            int count = 0;
            foreach (int i in board)
            {
                if (i >= 2) count++;
            }
            return count.ToString();
        }
        string Part2(List<Line> data)
        {
            int[] board = new int[1000 * 1000];
            foreach (Line line in data)
            {
                Func<(int x, int y)[]> f;
                if (line.horz)
                {
                    f = line.GetHorzPoints;
                }
                else if (line.vert)
                {
                    f = line.GetVertPoints;
                }
                else f = line.GetDiagPoints;
                foreach ((int x, int y) in f())
                {
                    board[x + (1000 * y)]++;
                }
            }
            int count = 0;
            foreach (int i in board)
            {
                if (i >= 2) count++;
            }
            return count.ToString();
        }

        struct Line
        {
            int x1, y1, x2, y2;
            public bool horz, vert;
            public Line(int[] nums)
            {
                this.x1 = nums[0];
                this.y1 = nums[1];
                this.x2 = nums[2];
                this.y2 = nums[3];

                this.horz = y1 == y2;
                this.vert = x1 == x2;
            }
            public (int,int)[] GetHorzPoints()
            {
                List<(int, int)> pts = new List<(int, int)>();
                if (x1 == x2 && y1 == y2)
                {
                    pts.Add((x1, y1));
                    return pts.ToArray();
                }
                int start = Math.Min(x1, x2);
                int end = Math.Max(x1, x2);
                for (int dx = start; dx <= end; dx++)
                {
                    pts.Add((dx, y1));
                }
                return pts.ToArray();
            }            
            public (int x,int y)[] GetVertPoints()
            {
                List<(int x, int y)> pts = new List<(int, int)>();
                if (x1 == x2 && y1 == y2)
                {
                    pts.Add((x1, y1));
                    return pts.ToArray();
                }
                int start = Math.Min(y1, y2);
                int end = Math.Max(y1, y2);
                for (int dy = start; dy <= end; dy++)
                {
                    pts.Add((x: x1, y: dy));
                }
                return pts.ToArray();
            }
            public (int x, int y)[] GetDiagPoints()
            {
                List<(int x, int y)> pts = new List<(int, int)>();
                if (x1 == x2 && y1 == y2) 
                {
                    pts.Add((x1, y1));
                    return pts.ToArray();
                }
                int dy = y1;
                for (int dx = x1; dx != x2; dx += Math.Sign(x2 - x1))
                {
                    pts.Add((x: dx, y: dy));
                    dy += Math.Sign(y2 - y1);
                }
                pts.Add((x: x2, y: y2));
                return pts.ToArray();
            }
        }
    }
}
