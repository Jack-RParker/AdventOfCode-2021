using AdventOfCode.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Day12 : Day
    {
        public override void Run()
        {
            DisplayOuput(RunParts());
        }
        Output RunParts()
        {
            Cave[] data = ProcessData();
            string p1 = Part1(data);
            string p2 = Part2(data);
            return new Output(p1, p2);
        }
        Cave[] ProcessData()
        {
            string[] raw = Resources.Day12Input.Trim().Split('\n');
            SortedDictionary<string, Cave> dict = new SortedDictionary<string, Cave>(new StringComparer());
            for (int i = 0; i < raw.Length; i++)
            {
                for (int c = 0; c <= 1; c++)
                {
                    string cave = raw[i].Split('-')[c];
                    if (!dict.ContainsKey(cave))
                    {
                        dict.Add(cave, new Cave(cave));
                    }
                }
            }
            for (int i = 0; i < raw.Length; i++)
            {
                string c1 = raw[i].Split('-')[0];
                string c2 = raw[i].Split('-')[1];
                dict[c1].Link(dict[c2]);
                dict[c2].Link(dict[c1]);
            }
            return dict.Values.ToArray();
        }
        string Part1(Cave[] data)
        {
            Cave start = data[data.Length - 1];
            Cave end = data[data.Length - 2];
            List<string> paths = new List<string>();
            Stack<Cave> stack = new Stack<Cave>();
            int count = 0;
            Explore(start, end, ref stack, ref count, ref paths);
            return count.ToString();
        }
        string Part2(Cave[] data)
        {
            Cave start = data[data.Length - 1];
            Cave end = data[data.Length - 2];
            List<string> paths = new List<string>();
            Stack<Cave> stack = new Stack<Cave>();
            int count = 0;
            List<Cave> smalls = new List<Cave>();
            foreach (Cave cave in data)
            {
                if (cave != start && cave != end && !cave.large) smalls.Add(cave);
            }
            foreach (Cave small in smalls)
            {
                Explore(start, end, ref stack, ref count, ref paths, small);
            }
            int repeats = 0;
            paths.Clear();
            stack.Clear();
            Explore(start, end, ref stack, ref repeats, ref paths);
            count -= repeats * (smalls.Count - 1);
            return count.ToString();
        }

        void Explore(Cave current, Cave end, ref Stack<Cave> path, ref int count, ref List<string> paths, Cave small = new Cave())
        {
            path.Push(current);
            if (current == end)
            {
                count++;
            }
            else
            {
                for (int i = 0; i < current.links.Count; i++)
                {
                    Cave cave = current.links[i];
                    if (!path.Contains(cave) || cave.large || (cave == small && Repeat(small, path)))
                    {
                        Explore(cave, end, ref path, ref count, ref paths, small);
                    }
                }
            }
            path.Pop();
        }
        bool Repeat(Cave cave, Stack<Cave> path)
        {
            int count = 0;
            foreach (Cave c in path)
            {
                if (c == cave) count++;
            }
            return count <= 1;
        }
        struct Cave : IEquatable<Cave>
        {
            public string name;
            public bool large;
            public List<Cave> links;
            public Cave(string name) : this()
            {
                this.name = name;
                this.large = !(name.ToLower() == name);
                this.links = new List<Cave>();
            }
            internal void Link(Cave cave)
            {
                if (!links.Contains(cave))
                {
                    links.Add(cave);
                }
            }
            public override bool Equals(object obj)
            {
                return obj is Cave cave && Equals(cave);
            }

            public bool Equals(Cave other)
            {
                return name == other.name;
            }

            public override int GetHashCode()
            {
                return 363513814 + EqualityComparer<string>.Default.GetHashCode(name);
            }

            public static bool operator ==(Cave left, Cave right)
            {
                return left.Equals(right);
            }

            public static bool operator !=(Cave left, Cave right)
            {
                return !(left == right);
            }
        }
    }
    class StringComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            if (x == null) throw new ArgumentNullException(nameof(x));
            if (y == null) throw new ArgumentNullException(nameof(y));
            var lengthComparison = x.Length.CompareTo(y.Length);
            return lengthComparison == 0 ? string.Compare(x, y, StringComparison.Ordinal) : lengthComparison;
        }
    }
}
