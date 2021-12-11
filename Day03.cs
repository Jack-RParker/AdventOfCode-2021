using AdventOfCode.Properties;
using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day03 : Day
    {
        public override void Run()
        {
            DisplayOuput(RunParts());
        }

        Output RunParts()
        {
            List<int[]> data = ProcessData();
            string p1 = Part1(data);
            string p2 = Part2(data);
            return new Output(p1, p2);
        }
        List<int[]> ProcessData()
        {
            List<int[]> data = new List<int[]>();
            string[] raw = Resources.Day3Input.TrimEnd('\n').Split('\n');
            for (int i = 0; i < raw.Length; i++)
            {
                data.Add(StrToIntArr(raw[i]));
            }
            return data;
        }
        string Part1(List<int[]> data)
        {
            int l = data[0].Length;
            int[] gam = new int[l];
            int[] eps = new int[l];
            for (int i = 0; i < l; i++)
            {
                int mcb = MCB(data, i);
                gam[i] = mcb;
                eps[i] = 1 - mcb;
            }
            return (ArrToInt(gam) * ArrToInt(eps)).ToString();
        }
        string Part2(List<int[]> data)
        {
            int oxy = Step(data, 0, true);
            int co2 = Step(data, 0, false);
            return (oxy * co2).ToString();
        }
        int MCB(List<int[]> data, int index)
        {
            int sum = 0;
            for (int i = 0; i < data.Count; i++)
            {
                sum += data[i][index];
            }
            return 2 * sum >= data.Count ? 1 : 0;
        }
        List<int[]> Reduce(List<int[]> data, int index, int bit)
        {
            List<int[]> output = new List<int[]>();
            foreach (int[] num in data)
            {
                if (num[index] == bit) output.Add(num);
            }
            return output;
        }
        int ArrToInt(int[] arr)
        {
            string[] sArr = Array.ConvertAll(arr, Convert.ToString);
            string s = string.Join("", sArr);
            return Convert.ToInt32(s, 2);
        }
        int Step(List<int[]> data, int depth, bool most)
        {
            if (data.Count == 1) return ArrToInt(data[0]);
            int bit = most ? MCB(data, depth) : 1 - MCB(data, depth);
            return Step(Reduce(data, depth, bit), depth + 1, most);
        }
    }
}
