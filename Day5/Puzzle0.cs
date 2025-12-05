// #define Sample

public class Puzzle5 : IPuzzle
{

    public void Excute(FileInfo input)
    {
#if Sample
        var lines = sample.Split("\r\n");
#else
        var lines = File.ReadAllLines(input.FullName);
#endif
        long result = 0;


        List<Range> ranges = new();
        List<long> ingredients = new();
        int state = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            if (string.IsNullOrWhiteSpace(line))
            {
                state++;
                continue;
            }

            if (state == 0)
            {
                var parts = line.Split('-');
                var range = new Range(long.Parse(parts[0]), long.Parse(parts[1]));
                ranges.Add(range);
            }
            else if (state == 1)
            {
                var ingredient = long.Parse(line);
                ingredients.Add(ingredient);
            }
        }


        foreach (var ingredient in ingredients)
        {
            bool found = false;
            foreach (var range in ranges)
            {
                if (ingredient >= range.Start && ingredient <= range.End)
                {
                    found = true;
                    break;
                }
            }

            if (found)
            {
                System.Console.WriteLine("Fresh ingredient: {0}", ingredient);
                result++;
            }
        }



        System.Console.WriteLine("Part 1 = {0}", result);



        System.Console.WriteLine("Part 2 = {0}", result);

    }

    record Range(long Start, long End);

    private string sample = """
3-5
10-14
16-20
12-18

1
5
8
11
17
32
""";
}