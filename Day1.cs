using AdventOfCode.Properties;

namespace AdventOfCode
{
    class Day1 : Day
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
            string[] input = Resources.Day1Input.Split('\n');
            int[] data = new int[input.Length - 1];
            for (int i = 0; i < input.Length - 1; i++)
            {
                data[i] = int.Parse(input[i]);
            }

            return data;
        }

        string Part1(int[] data)
        {
            int counter = 0;
            for (int i = 1; i < data.Length; i++)
            {
                if (data[i] > data[i - 1]) counter++;
            }
            return counter.ToString();
        }
        string Part2(int[] data)
        {
            int counter = 0;
            for (int i = 0; i < data.Length - 3; i++)
            {
                int s1 = data[i] + data[i + 1] + data[i + 2];
                int s2 = data[i + 1] + data[i + 2] + data[i + 3];
                if (s2 > s1) counter++;
            }
            return counter.ToString();
        }

    }
}
