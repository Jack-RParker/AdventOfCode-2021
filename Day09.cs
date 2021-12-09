using AdventOfCode.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Day09 : Day
    {
        public override void Run()
        {
            DisplayOuput(RunParts());
        }

        Output RunParts()
        {
            int[][] data = ProcessData();
            string p1 = Part1(data);
            string p2 = Part2(data);
            return new Output(p1, p2);
        }
        int[][] ProcessData()
        {
            string[] raw = Resources.Day9Input.Trim('\n').Split('\n');
            int[][] iArr = Array.ConvertAll(raw, StrToIntArr);
            return iArr;
        }
        string Part1(int[][] data)
        {
            Point[] lowPoints = FindLowPoints(data);
            int risk = 0;
            foreach (Point p in lowPoints)
            {
                risk += data[p.y][p.x] + 1;
            }
            return risk.ToString();
        }
        string Part2(int[][] data)
        {
            List<Basin> basins = new List<Basin>();
            List<int> sizes = new List<int>();
            foreach (Point p in FindLowPoints(data))
            {
                basins.Add(new Basin(p));
            }
            foreach (Basin basin in basins)
            {
                basin.Fill(data);
                sizes.Add(basin.Size());
            }
            int[] sizeArr = sizes.OrderByDescending(c => c).ToArray();
            return (sizeArr[0] * sizeArr[1] * sizeArr[2]).ToString();
        }
        int[] StrToIntArr(string s)
        {
            string[] sArr = Array.ConvertAll(s.ToCharArray(), Convert.ToString);
            return Array.ConvertAll(sArr, int.Parse);
        }
        Point[] FindLowPoints(int[][] data)
        {
            int[] lim = new int[] { data[0].Length, data.Length };
            int[][] nbrs = new int[][]
            {
                new int[]{0,1},
                new int[]{1,0},
                new int[]{0,-1},
                new int[]{-1,0}
            };
            List<Point> lowPoints = new List<Point>();
            for (int y = 0; y < lim[1]; y++)
            {
                for (int x = 0; x < lim[0]; x++)
                {
                    int n = data[y][x];
                    bool lower = true;
                    foreach (int[] dir in nbrs)
                    {
                        int dx = x + dir[0];
                        int dy = y + dir[1];
                        if (dx >= 0 && dx <= lim[0] - 1 && dy >= 0 && dy <= lim[1] - 1)
                        {
                            if (data[dy][dx] <= n)
                            {
                                lower = false;
                                break;
                            }
                        }
                    }
                    if (lower) lowPoints.Add(new Point(x, y));
                }
            }
            return lowPoints.ToArray();
        }
        struct Point
        {
            public int x, y;

            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
            public static Point operator +(Point a, Point b)
                => new Point(a.x + b.x, a.y + b.y);

        }
        struct Basin
        {
            Point seed;
            List<Point> points;
            public Basin(Point seed)
            {
                this.seed = seed;
                points = new List<Point> { this.seed };
            }

            internal void Fill(int[][] data)
            {
                int[] lim = new int[] { data[0].Length, data.Length };
                Point[] dirs = new Point[]
                {
                    new Point(1,0),
                    new Point(0,1),
                    new Point(-1,0),
                    new Point(0,-1)
                };

                List<Point> newPoints = new List<Point>();
                List<Point> frontier = new List<Point> { seed };
                while (true)
                {
                    foreach (Point point in frontier)
                    {
                        foreach (Point dir in dirs)
                        {
                            Point p = point + dir;

                            if (p.x >= 0 && p.x <= lim[0] - 1 && p.y >= 0 && p.y <= lim[1] - 1)
                            {
                                if (data[p.y][p.x] != 9 && !(points.Contains(p) || newPoints.Contains(p)))
                                {
                                    newPoints.Add(p);
                                }
                            }
                        }
                    }
                    if (newPoints.Count > 0)
                    {
                        points.AddRange(newPoints);
                        frontier.Clear();
                        frontier.AddRange(newPoints);
                        newPoints.Clear();
                    }
                    else break;
                }

            }

            internal int Size()
            {
                return points.Count;
            }
        }
    }
}
