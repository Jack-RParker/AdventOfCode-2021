using System;

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
            object[] data = ProcessData();
            string p1 = Part1(data);
            string p2 = Part2(data);
            return new Output(p1, p2);
        }
        object[] ProcessData()
        {
            throw new NotImplementedException();
        }
        string Part1(object[] data)
        {
            return "";
        }
        string Part2(object[] data)
        {
            return "";
        }
    }
}
