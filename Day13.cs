using AdventOfCode.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Day13 : Day
    {
        public override void Run()
        {
            DisplayOuput(RunParts());
        }

        Output RunParts()
        {
            Point[] points = ProcessData().points;
            Move[] moves = ProcessData().moves;
            string p1 = Part1(points, moves);
            string p2 = Part2(points, moves);
            return new Output(p1, p2);
        }
        (Point[] points, Move[] moves) ProcessData()
        {
            string[] raw = Resources.Day13Input.Trim().Split('\n');
            List<Point> points = new List<Point>();
            List<Move> moves = new List<Move>();
            foreach (string s in raw)
            {
                if (s != string.Empty)
                {
                    if (char.IsDigit(s[0])) points.Add(new Point(s));
                    else moves.Add(new Move(s));
                }
            }
            return (points: points.ToArray(), moves: moves.ToArray());
        }
        string Part1(Point[] points, Move[] moves)
        {
            for (int i = 0; i < points.Length; i++)
            {
                Fold(moves[0], ref points[i]);
            }
            return points.Distinct().Count().ToString();
        }
        string Part2(Point[] points, Move[] moves)
        {
            for (int m = 0; m < moves.Length; m++)
            {
                for (int p = 0; p < points.Length; p++)
                {
                    Fold(moves[m], ref points[p]);
                }
            }
            points = points.Distinct().ToArray();
            int maxX = int.MinValue;
            int maxY = int.MinValue;
            foreach (Point p in points)
            {
                maxX = Math.Max(maxX, p.x);
                maxY = Math.Max(maxY, p.y);
            }
            string[][] output = new string[maxY + 1][];
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = new string[maxX + 1];
                for (int j = 0; j < output[i].Length; j++)
                {
                    output[i][j] = " ";
                }
            }

            foreach (Point p in points)
            {
                output[p.y][p.x] = "#";
            }

            foreach (string[] arr in output)
            {
                Print(arr, "");
                Print("");
            }
            return "See below";
        }

        void Fold(Move m, ref Point p)
        {
            if (m.axis == 'x')
            {
                if (p.x > m.val) p.x = (2 * m.val) - p.x;
            }
            else
            {
                if (p.y > m.val) p.y = (2 * m.val) - p.y;
            }
        }
        struct Point : IEquatable<Point>
        {
            public int x, y;
            public Point(string s)
            {
                x = int.Parse(s.Split(',')[0]);
                y = int.Parse(s.Split(',')[1]);
            }

            public override bool Equals(object obj)
            {
                return obj is Point point && Equals(point);
            }

            public bool Equals(Point other)
            {
                return x == other.x &&
                       y == other.y;
            }

            public override int GetHashCode()
            {
                int hashCode = 1502939027;
                hashCode = hashCode * -1521134295 + x.GetHashCode();
                hashCode = hashCode * -1521134295 + y.GetHashCode();
                return hashCode;
            }

            public override string ToString()
            {
                return "x: " + x.ToString() + ", y: " + y.ToString();
            }

            public static bool operator ==(Point left, Point right)
            {
                return left.Equals(right);
            }

            public static bool operator !=(Point left, Point right)
            {
                return !(left == right);
            }
        }
        struct Move
        {
            public char axis;
            public int val;

            public Move(string s)
            {
                string[] sArr = s.Split(' ');
                axis = sArr[2].Split('=')[0][0];
                val = int.Parse(sArr[2].Split('=')[1]);
            }
        }
    }
}
