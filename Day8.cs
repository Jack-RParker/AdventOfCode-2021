using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Properties;

namespace AdventOfCode
{
    class Day8 : Day
    {
        public override void Run()
        {
            DisplayOuput(RunParts());
        }

        Output RunParts()
        {
            Segment[] data = ProcessData();
            string p1 = Part1(data);
            string p2 = Part2(data);
            return new Output(p1, p2);
        }
        Segment[] ProcessData()
        {
            string[] raw = Resources.Day8Input.Trim('\n').Split('\n');
            Segment[] segments = new Segment[raw.Length];

            for (int i = 0; i < raw.Length; i++)
            {
                segments[i] = new Segment(raw[i]);
            }
            return segments;
        }
        string Part1(Segment[] data)
        {
            int[] uniques = new int[] { 2, 3, 4, 7 };
            int count = 0;
            foreach (Segment seg in data)
            {
                foreach (string s in seg.outputs)
                {
                    if (uniques.Contains(s.Length))
                    {
                        count++;
                    }
                }
            }
            return count.ToString();
        }
        string Part2(Segment[] data)
        {
            int sum = 0;
            foreach (Segment seg in data)
            {
                sum += seg.GetOutput();
            }
            return sum.ToString();
        }

        struct Segment
        {
            public string[] inputs, outputs;

            public Segment(string raw)
            {
                string[] parts = raw.Split('|');
                this.inputs = parts[0].Trim().Split(' ');
                this.outputs = parts[1].Trim().Split(' ');
            }
            internal int GetOutput()
            {
                string key = BruteForce();
                string output = "";
                foreach (string s in outputs)
                {
                    output += Decode(s, key);
                }
                return int.Parse(output);
            }

            internal string BruteForce()
            {
                string s1 = "";
                string s7 = "";
                string sa = "";
                foreach (string s in inputs)
                {
                    if (s.Length == 2) s1 = s;
                    if (s.Length == 3) s7 = s;
                }
                for (int i = 0; i < s7.Length; i++)
                {
                    if (!s1.Contains(s7[i])) sa = s7[i].ToString();
                }
                string remaining = "abcdefg".Replace(sa, "");
                foreach (var v in Permutations(remaining.ToCharArray()))
                {
                    string potKey = sa + string.Join("", v);
                    bool fail = false;
                    foreach (string input in inputs)
                    {
                        if (Decode(input, potKey) == -1)
                        {
                            fail = true;
                            break;
                        }
                    }
                    if (fail) continue;

                    return potKey;
                }
                return "";
            }
            internal int Decode(string num, string key)
            {
                int sum = 0;
                for (int i = 0; i < key.Length; i++)
                {
                    if (num.Contains(key[i])) sum += (int)Math.Pow(2, i);
                }
                return (Map(sum));
            }

            int Map(int n)
            {
                string[] h = new string[] { "3F", "06", "5B", "4F", "66", "6D", "7D", "07", "7F", "6F" };
                for (int i = 0; i < h.Length; i++)
                {
                    if (n.ToString("X2") == h[i]) return i;
                }
                return -1;
            }
        }

        static IEnumerable<T[]> Permutations<T>(T[] values, int fromInd = 0)
        {
            if (fromInd + 1 == values.Length)
                yield return values;
            else
            {
                foreach (var v in Permutations(values, fromInd + 1))
                    yield return v;

                for (var i = fromInd + 1; i < values.Length; i++)
                {
                    SwapValues(values, fromInd, i);
                    foreach (var v in Permutations(values, fromInd + 1))
                        yield return v;
                    SwapValues(values, fromInd, i);
                }
            }
        }

        static void SwapValues<T>(T[] values, int pos1, int pos2)
        {
            if (pos1 != pos2)
            {
                T tmp = values[pos1];
                values[pos1] = values[pos2];
                values[pos2] = tmp;
            }
        }
    }
}
