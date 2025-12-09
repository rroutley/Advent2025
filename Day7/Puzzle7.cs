// #define Sample


public class Puzzle7 : IPuzzle
{

    public void Excute(FileInfo input)
    {
#if Sample
        var lines = sample.Split("\r\n");
#else
        var lines = File.ReadAllLines(input.FullName);
#endif


        long result = Part1(lines);

        System.Console.WriteLine("Part 1 = {0}", result);

        partialResult = new long[lines.Length, lines[0].Length];
        result = Part2(lines);

        System.Console.WriteLine("Part 2 = {0}", result);

    }

    private static long Part1(string[] lines)
    {
        long result = 0;
        int cols = lines[0].Length;
        var beams = new int[cols];
        int[] last;

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            last = beams;

            for (int j = 0; j < cols; j++)
            {
                if (i == 0)
                {
                    if (line[j] == 'S')
                    {
                        beams[j] = 1;
                    }
                }
                else
                {
                    if (last[j] == 1 && line[j] == '^')
                    {
                        // Beam Splits
                        if (j > 0)
                        {
                            beams[j - 1] = 1;
                        }
                        beams[j] = 0;
                        if (j < cols - 1)
                        {
                            beams[j + 1] = 1;
                        }

                        result++;
                    }
                }
            }
        }

        return result;
    }



    private static long[,] partialResult;

    public static long Part2(string[] lines, int i = 0, int beam = 0)
    {
        if (i == lines.Length)
        {
            return 1;
        }

        if (partialResult[i, beam] > 0)
        {
            return partialResult[i, beam];
        }

        int cols = lines[0].Length;
        var result = 0L;
        if (i == 0)
        {
            for (var j = 0; j < cols; j++)
            {
                if (lines[i][j] == 'S')
                {
                    result += Part2(lines, i + 1, j);
                }
            }
            return result;
        }

        if (lines[i][beam] == '^')
        {
            if (beam > 0)
            {
                result += Part2(lines, i + 1, beam - 1);
            }
            if (beam < cols - 1)
            {
                result += Part2(lines, i + 1, beam + 1);
            }
        }
        else
        {
            result += Part2(lines, i + 1, beam);
        }

        partialResult[i, beam] = result;
        return result;
    }

    private string sample = """
.......S.......
...............
.......^.......
...............
......^.^......
...............
.....^.^.^.....
...............
....^.^...^....
...............
...^.^...^.^...
...............
..^...^.....^..
...............
.^.^.^.^.^...^.
...............
""";
}