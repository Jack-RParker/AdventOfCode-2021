using AdventOfCode.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day14 : Day
    {
        public override void Run()
        {
            DisplayOuput(RunParts());
        }

        Output RunParts()
        {
            (string polymer, Pair[] pairs) = ProcessData();
            string p1 = Part1(polymer, pairs);
            string p2 = Part2(polymer, pairs);
            return new Output(p1, p2);
        }
        (string polymer, Pair[] pairs) ProcessData()
        {
            string[] raw = Resources.Day14Input.Trim().Split('\n');
            string polymer = raw[0];
            Pair[] pairs = new Pair[raw.Length - 2];
            for (int i = 0; i < raw.Length-2; i++)
            {
                pairs[i] = new Pair(raw[i + 2]);
            }
            return (polymer, pairs);
        }
        string Part1(string polymer, Pair[] pairs)
        {
            Dictionary<string, long> dict = new Dictionary<string, long>();
            foreach (Pair p in pairs)
            {
                dict.Add(p.pair, 0);
            }
            for (int i = 0; i < polymer.Length - 1; i++)
            {
                string s = "" + polymer[i] + polymer[i + 1];
                dict[s]++;
            }
            for (int i = 0; i < 10; i++)
            {
                dict = Step(dict, pairs);
            }
            return MinMax(dict, polymer[0]).ToString();
        }
        string Part2(string polymer, Pair[] pairs)
        {
            Dictionary<string, long> dict = new Dictionary<string, long>();
            foreach (Pair p in pairs)
            {
                dict.Add(p.pair, 0);
            }
            for (int i = 0; i < polymer.Length-1; i++)
            {
                string s = "" + polymer[i] + polymer[i + 1];
                dict[s]++;
            }
            for (int i = 0; i < 40; i++)
            {
                dict = Step(dict, pairs);
            }
            return MinMax(dict,polymer[0]).ToString();
        }

        long MinMax(Dictionary<string, long> dict, char first)
        {
            (char, long)[] counts = CountDict(dict);
            long min = long.MaxValue;
            long max = long.MinValue;
            foreach ((char c,long num) count in counts)
            {
                long val = count.num;
                if (count.c == first) val++;
                min = Math.Min(min, val);
                max = Math.Max(max, val);
            }
            return max - min;
        }

        Dictionary<string, long> Step(Dictionary<string, long> dict, Pair[] pairs)
        {
            Dictionary<string, long> output = new Dictionary<string, long>();
            foreach (Pair p in pairs)
            {
                output.Add(p.pair, 0);
            }
            foreach (string key in dict.Keys)
            {
                (string first, string last) = Lookup(key, pairs);
                output[first] += dict[key];
                output[last] += dict[key];
            }
            return output;
        }
        (char,long)[] CountDict(Dictionary<string,long> dict)
        {
            char[] chars = string.Join("", dict.Keys).ToCharArray().Distinct().ToArray();
            (char, long)[] outs = new (char, long)[chars.Length];
            for (int i = 0; i < chars.Length; i++)
            {
                long count = 0;
                foreach (KeyValuePair<string,long> kvp in dict)
                {
                    if (kvp.Key[1] == chars[i]) count += kvp.Value;
                }
                outs[i] = (chars[i], count);
            }
            return outs;
        }
        (string,string) Lookup(string key, Pair[] pairs)
        {
            foreach (Pair p in pairs)
            {
                if (key == p.pair) return (p.first, p.last);
            }
            return ("", "");
        } 
        struct Pair
        {
            public char left, right, middle;
            public string pair, first, last;

            public Pair(string s)
            {
                left = s[0];
                right = s[1];
                middle = s[s.Length - 1];
                pair = "" + left+right;
                first = "" + left + middle;
                last = "" + middle + right;
            }
        }
    }
}
