using AdventOfCode.Properties;
using System;
using System.Linq;

namespace AdventOfCode
{
    class Day7 : Day
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
            string[] raw = Resources.Day7Input.TrimEnd('\n').Split(',');
            int[] data = Array.ConvertAll(raw, int.Parse);
            return data;
        }
        string Part1(int[] data)
        {
            int maxPos = data.Max();
            int minCost = int.MaxValue;
            for (int pos = 0; pos < maxPos; pos++)
            {
                int cost = 0;
                for (int i = 0; i < data.Length && cost < minCost; i++)
                {
                    cost += Math.Abs(data[i] - pos);
                }
                minCost = Math.Min(minCost, cost);
            }
            return minCost.ToString();
        }
        string Part2(int[] data)
        {
            int maxPos = data.Max();
            int minCost = int.MaxValue;
            for (int pos = 0; pos < maxPos; pos++)
            {
                int cost = 0;
                for (int i = 0; i < data.Length && cost < minCost; i++)
                {
                    int nSteps = Math.Abs(data[i] - pos);
                    cost += (nSteps * (nSteps + 1)) / 2;
                }
                minCost = Math.Min(minCost, cost);
            }
            return minCost.ToString();
        }
    }
}
