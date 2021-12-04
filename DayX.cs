using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Properties;

namespace AdventOfCode
{
    class DayX : Day
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
