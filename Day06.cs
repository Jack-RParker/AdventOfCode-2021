using AdventOfCode.Properties;
using System;
using System.Linq;

namespace AdventOfCode
{
    class Day06 : Day
    {
        public override void Run()
        {
            DisplayOuput(RunParts());
        }

        Output RunParts()
        {
            int[] data = ProcessData();
            string p1 = Part1(data);
            string p2 = Part2(data);
            return new Output(p1, p2);
        }
        int[] ProcessData()
        {
            string[] raw = Resources.Day6Input.TrimEnd('\n').Split(',');
            int[] data = Array.ConvertAll(raw, int.Parse);
            return data;
        }
        string Part1(int[] data)
        {
            long[] bins = new long[9];
            for (int i = 0; i < data.Length; i++)
            {
                bins[data[i]]++;
            }
            for (int i = 0; i < 80; i++)
            {
                bins = Step(bins);
            }
            return bins.Sum().ToString();
        }
        string Part2(int[] data)
        {
            long[] bins = new long[9];
            for (int i = 0; i < data.Length; i++)
            {
                bins[data[i]]++;
            }
            for (int i = 0; i < 256; i++)
            {
                bins = Step(bins);
            }
            return bins.Sum().ToString();

        }
        long[] Step(long[] bins)
        {
            long num = 0;
            for (int i = bins.Length - 1; i >= 0; i--)
            {
                long temp = bins[i];
                bins[i] = num;
                num = temp;
            }
            bins[8] = num;
            bins[6] += num;
            return bins;
        }
    }
}
