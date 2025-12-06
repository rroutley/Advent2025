// #define Sample

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



        System.Console.WriteLine("Part 2 = {0}", result);

    }


    private string sample = """
123 328  51 64 
 45 64  387 23 
  6 98  215 314
*   +   *   +  
""";
}