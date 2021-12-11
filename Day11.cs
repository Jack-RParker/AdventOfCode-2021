using AdventOfCode.Properties;
using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day11 : Day
    {
        public override void Run()
        {
            DisplayOuput(RunParts());
        }

        Output RunParts()
        {
            string p1 = Part1(new Grid(ProcessData()));
            string p2 = Part2(new Grid(ProcessData()));
            return new Output(p1, p2);
        }
        int[][] ProcessData()
        {
            string[] raw = Resources.Day11Input.Trim().Split('\n');
            int[][] ints = Array.ConvertAll(raw, StrToIntArr);
            return ints;
        }
        string Part1(Grid data)
        {
            for (int i = 0; i < 100; i++)
            {
                data.Step();
            }
            return data.Flashes.ToString();
        }
        string Part2(Grid data)
        {
            int steps=0;
            while (true)
            {
                data.Step();
                steps++;
                if (data.Sum() == 0) break;
            }
            return steps.ToString();
        }
        struct Grid
        {
            int[][] vals, spread;
            bool[][] flashed;
            int n, flashes;

            public Grid(int[][] vals) : this()
            {
                this.vals = vals;
                n = vals.Length;
                flashes = 0;
                spread = MakeGrid<int>(n);
                flashed = MakeGrid<bool>(n);
            }
            public int Flashes
            {
                get { return flashes; }
            }
            internal void Show()
            {
                for (int i = 0; i < n; i++)
                {
                    Print(vals[i], " ");
                    Print("");
                }
            }
            internal void Step()
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        vals[i][j]++;
                    }
                }
                bool looping = true;
                while (looping)
                {
                    looping = false;
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            if (vals[i][j] > 9 && !flashed[i][j])
                            {
                                Flash(i, j);
                                looping = true;
                            }
                        }
                    }
                    if (looping)
                    {
                        for (int i = 0; i < n; i++)
                        {
                            for (int j = 0; j < n; j++)
                            {
                                vals[i][j] += spread[i][j];
                                spread[i][j] = 0;
                            }
                        }
                    }
                }
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (flashed[i][j]) vals[i][j] = 0;
                        flashed[i][j] = false;
                    }
                }

            }

            private void Flash(int i, int j)
            {
                flashes++;
                flashed[i][j] = true;
                vals[i][j] = 0;
                for (int di = -1; di <= 1; di++)
                {
                    for (int dj = -1; dj <= 1; dj++)
                    {
                        int i2 = i + di;
                        int j2 = j + dj;
                        if (i2 >= 0 && i2 < n && j2 >= 0 && j2 < n)
                        {
                            spread[i2][j2]++;
                        }
                    }
                }
            }
            T[][] MakeGrid<T>(int n)
            {
                List<T[]> l = new List<T[]>();
                for (int i = 0; i < n; i++)
                {
                    l.Add(new T[n]);
                }
                return l.ToArray();
            }
            internal int Sum()
            {
                int s = 0;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        s += vals[i][j];
                    }
                }
                return s;
            }
        }
    }
}
