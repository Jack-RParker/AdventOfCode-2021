using AdventOfCode.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Day04 : Day
    {
        public override void Run()
        {
            DisplayOuput(RunParts());
        }

        Output RunParts()
        {
            int[] inputs = ProcessInput();
            BingoBoard[] boards = ProcessData();
            string p1 = Part1(inputs, boards);
            string p2 = Part2(inputs, boards);
            return new Output(p1, p2);
        }
        int[] ProcessInput()
        {
            string[] raw = Resources.Day4Input.Split('\n');
            string[] inputStrings = raw[0].Split(',');
            int[] input = new int[inputStrings.Length];
            for (int i = 0; i < inputStrings.Length; i++)
            {
                input[i] = Int32.Parse(inputStrings[i]);
            }
            return input;
        }

        BingoBoard[] ProcessData()
        {
            string[] raw = Resources.Day4Input.Split('\n');

            int n = (raw.Length - 2) / 6;
            List<BingoBoard> boards = new List<BingoBoard>();
            for (int i = 2; i < 2 + (6 * n); i += 6)
            {
                string s = "";
                for (int j = 0; j < 5; j++)
                {
                    string line = raw[i + j].Replace("  ", " ");
                    if (line[0] == ' ') s += line.Substring(1);
                    else s += line;
                    if (j != 4) s += ' ';
                }
                string[] sArr = s.Split(' ');
                int[] iArr = Array.ConvertAll(sArr, int.Parse);
                boards.Add(new BingoBoard(iArr));
            }
            return boards.ToArray();
        }
        string Part1(int[] inputs, BingoBoard[] boards)
        {
            bool won = false;
            int finalNum = 0;
            BingoBoard winner = boards[0];
            foreach (int input in inputs)
            {
                if (won) break;
                foreach (BingoBoard board in boards)
                {
                    board.Mark(input);
                    if (board.CheckWin())
                    {
                        won = true;
                        finalNum = input;
                        winner = board;
                        break;
                    }
                }
            }
            return (finalNum * winner.SumUnmarked()).ToString();
        }
        string Part2(int[] inputs, BingoBoard[] boards)
        {
            bool won = false;
            int finalNum = -1;
            BingoBoard winner = boards[0];
            List<BingoBoard> boardsList = boards.ToList();
            for (int i = 0; i < inputs.Length; i++)
            {
                int input = inputs[i];
                if (won) break;
                foreach (BingoBoard board in boardsList)
                {
                    board.Mark(input);
                    if (board.CheckWin())
                    {
                        if (boardsList.Count == 1)
                        {
                            won = true;
                            finalNum = input;
                            winner = board;
                            break;
                        }
                        else
                        {
                            boardsList.Remove(board);
                            i--;
                            break;
                        }

                    }
                }
            }
            return (finalNum * winner.SumUnmarked()).ToString();
        }
        struct BingoBoard
        {
            int[] board;
            bool[] marked;

            public BingoBoard(int[] board) : this()
            {
                this.board = board;
                this.marked = new bool[board.Length];
            }

            public void Mark(int i)
            {
                if (!board.Contains(i)) return;
                int index = Array.IndexOf(board, i);
                marked[index] = true;
            }
            public bool CheckWin()
            {
                for (int i = 0; i < 5; i++)
                {
                    bool row = marked[5 * i] && marked[5 * i + 1] && marked[5 * i + 2] && marked[5 * i + 3] && marked[5 * i + 4];
                    bool col = marked[i] && marked[i + 5] && marked[i + 10] && marked[i + 15] && marked[i + 20];
                    if (row || col) return true;
                }
                return false;
            }

            public int SumUnmarked()
            {
                int sum = 0;
                for (int i = 0; i < marked.Length; i++)
                {
                    if (!marked[i]) sum += board[i];
                }
                return sum;
            }

        }
    }
}
