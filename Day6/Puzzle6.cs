//#define Sample

public class Puzzle6 : IPuzzle
{

    public void Excute(FileInfo input)
    {
#if Sample
        var lines = sample.Split("\r\n");
#else
        var lines = File.ReadAllLines(input.FullName);
#endif
        long result = 0;

        var problems = new int[lines.Length - 1][];

        for (int i = 0; i < lines.Length - 1; i++)
        {
            var line = lines[i];
            problems[i] = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        }

        var operators = lines[^1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < operators.Length; i++)
        {
            string opeator = operators[i];

            long total = problems[0][i];
            for (int j = 1; j < problems.Length; j++)
            {
                if (opeator == "+")
                {
                    total += problems[j][i];
                }
                else
                {
                    total *= problems[j][i];
                }
            }

            System.Console.WriteLine(total);
            result += total;
        }

        System.Console.WriteLine("Part 1 = {0}", result);


        result = 0;

        // Use the position of the operations in the last line
        // to determine where the columns are
        List<(char opeator, int start, int end)> columns = [];
        string lastLine = lines[^1];
        int last = lastLine.Length + 1;
        for (int i = lastLine.Length - 1; i >= 0; i--)
        {
            var c = lastLine[i];
            if (c != ' ')
            {
                columns.Add((c, i, last - 1));
                last = i;
            }
        }

        foreach (var column in columns)
        {
            long total = 0;
            System.Console.WriteLine(column);
            for (int j = column.start; j < column.end; j++)
            {
                int value = 0;
                for (int i = 0; i < lines.Length - 1; i++)
                {
                    if (char.IsAsciiDigit(lines[i][j]))
                        value = value * 10 + lines[i][j] - '0';
                }

                if (j == column.start)
                {
                    total = value;
                }
                else if (column.opeator == '+')
                {
                    total += value;
                }
                else
                {
                    total *= value;
                }
            }
            System.Console.WriteLine(total);
            result += total;
        }


        System.Console.WriteLine("Part 2 = {0}", result);

    }


    private string sample = """
123 328  51 64 
 45 64  387 23 
  6 98  215 314
*   +   *   +  
""";
}