//#define Sample


public class Puzzle4 : IPuzzle
{
    static bool print = true;

    public void Excute(FileInfo input)
    {
#if Sample
        var lines = sample.Split("\r\n");
#else
        var lines = File.ReadAllLines(input.FullName);
        print = false;
#endif

        var grid = new char[lines[0].Length, lines.Length];
        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                grid[j, i] = lines[i][j];
            }
        }

        (long result, grid) = Part1(grid);

        System.Console.WriteLine("Part 1 = {0}", result);


        while (true)
        {
            (long r, char[,] newGrid) = Part1(grid);
            if (r == 0)
                break;

            result += r;
            grid = newGrid;
        }


        System.Console.WriteLine("Part 2 = {0}", result);

    }

    private static (long, char[,]) Part1(char[,] grid)
    {
        long result = 0;

        int maxY = grid.GetUpperBound(1) + 1;
        int maxX = grid.GetUpperBound(0) + 1;

        char[,] newGrid = new char[maxX, maxY];

        for (int y = 0; y < maxY; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                var c = grid[x, y];

                if (c != '@')
                {
                    if (print)
                        System.Console.Write(c);

                    newGrid[x, y] = '.';
                    continue;
                }

                int neighbors = 0;

                for (int dx = -1; dx <= 1; dx++)
                {
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        if (dx == 0 && dy == 0)
                            continue;

                        int nx = x + dx;
                        int ny = y + dy;

                        if (nx >= 0 && nx < maxX && ny >= 0 && ny < maxY)
                        {
                            if (grid[nx, ny] == '@')
                                neighbors++;
                        }
                    }
                }

                if (neighbors < 4)
                {
                    result++;
                    newGrid[x, y] = '.';
                    if (print)
                        System.Console.Write('x');

                    continue;
                }

                newGrid[x, y] = '@';
                if (print)
                    System.Console.Write('@');

            }
            if (print)
                System.Console.WriteLine();

        }

        return (result, newGrid);
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