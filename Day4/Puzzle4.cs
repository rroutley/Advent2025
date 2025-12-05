// #define Sample

public class Puzzle4 : IPuzzle
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



        System.Console.WriteLine("Part 2 = {0}", result);

    }

    private static long Part1(string[] lines)
    {
        long result = 0;

        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                var c = lines[i][j];

                if (c != '@')
                {
                    System.Console.Write(c);
                    continue;
                }

                int neighbors = 0;

                for (int dx = -1; dx <= 1; dx++)
                {
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        if (dx == 0 && dy == 0)
                            continue;

                        int ni = i + dx;
                        int nj = j + dy;

                        if (ni >= 0 && ni < lines.Length && nj >= 0 && nj < lines[i].Length)
                        {
                            if (lines[ni][nj] == '@')
                                neighbors++;
                        }
                    }
                }

                if (neighbors < 4)
                {
                    result++;
                    System.Console.Write('x');
                    continue;
                }

                System.Console.Write('@');
            }

            System.Console.WriteLine();
        }

        return result;
    }

    private string sample = """
..@@.@@@@.
@@@.@.@.@@
@@@@@.@.@@
@.@@@@..@.
@@.@@@@.@@
.@@@@@@@.@
.@.@.@.@@@
@.@@@.@@@@
.@@@@@@@@.
@.@.@@@.@.
""";
}