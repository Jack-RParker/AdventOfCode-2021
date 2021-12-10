using AdventOfCode.Properties;
using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day10 : Day
    {
        public override void Run()
        {
            DisplayOuput(RunParts());
        }

        Output RunParts()
        {
            string[] data = ProcessData();
            string p1 = Part1(data);
            string p2 = Part2(data);
            return new Output(p1, p2);
        }
        string[] ProcessData()
        {
            string[] raw = Resources.Day10Input.Trim().Split('\n');
            return raw;
        }
        string Part1(string[] data)
        {
            int sum = 0;
            foreach (string s in data)
            {
                if (Corrupted(s, out char c, out _))
                {
                    sum += Score(c);
                }
            }
            return sum.ToString();
        }
        string Part2(string[] data)
        {
            List<long> scores = new List<long>();
            foreach (string s in data)
            {
                if (!Corrupted(s, out _, out Stack<Chunk> chunks))
                {
                    long score = 0;
                    foreach (Chunk chunk in chunks)
                    {
                        score *= 5;
                        score += Score(chunk);
                    }
                    scores.Add(score);
                }
            }
            scores.Sort();
            return scores[(scores.Count - 1) / 2].ToString();
        }

        bool Corrupted(string s, out char c, out Stack<Chunk> chunks)
        {
            chunks = new Stack<Chunk>();
            bool corrupt = false;
            c = '#';
            for (int i = 0; i < s.Length; i++)
            {
                if (corrupt) break;
                switch (s[i])
                {
                    case '(':
                        chunks.Push(Chunk.Paren);
                        break;
                    case ')':
                        if (chunks.Pop() != Chunk.Paren)
                        {
                            c = s[i];
                            corrupt = true;
                        }
                        break;
                    case '[':
                        chunks.Push(Chunk.Square);
                        break;
                    case ']':
                        if (chunks.Pop() != Chunk.Square)
                        {
                            c = s[i];
                            corrupt = true;
                        }
                        break;
                    case '{':
                        chunks.Push(Chunk.Brace);
                        break;
                    case '}':
                        if (chunks.Pop() != Chunk.Brace)
                        {
                            c = s[i];
                            corrupt = true;
                        }
                        break;
                    case '<':
                        chunks.Push(Chunk.Wedge);
                        break;
                    case '>':
                        if (chunks.Pop() != Chunk.Wedge)
                        {
                            c = s[i];
                            corrupt = true;
                        }
                        break;
                }
            }
            return corrupt;
        }

        enum Chunk
        {
            Paren,
            Square,
            Brace,
            Wedge
        }
        int Score(char c)
        {
            switch (c)
            {
                case ')':
                    return 3;
                case ']':
                    return 57;
                case '}':
                    return 1197;
                case '>':
                    return 25137;
                default:
                    return 0;
            }
        }
        int Score(Chunk c)
        {
            switch (c)
            {
                case Chunk.Paren:
                    return 1;
                case Chunk.Square:
                    return 2;
                case Chunk.Brace:
                    return 3;
                case Chunk.Wedge:
                    return 4;
                default:
                    return 0;
            }
        }
    }
}
