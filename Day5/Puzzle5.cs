// #define Sample

using System.Collections.Concurrent;
using System.ComponentModel;

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


        List<Range> ranges = [];
        List<long> ingredients = [];
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
                if (range.Contains(ingredient))
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


        result = 0;

        List<Range> combined = [];

        foreach (var range in ranges)
        {
            // Completely contained in an existing range
            if (combined.Any(r => r.Contains(range)))
            {
                continue;
            }

            var rangeToAdd = range;

            // Overlaps with an existing range
            while (true)
            {
                var overlapping = combined.FirstOrDefault(r => r.Overlaps(rangeToAdd));
                if (overlapping == default)
                {
                    break;
                }

                combined.Remove(overlapping);
                
                // Expand the range we will add
                rangeToAdd = overlapping.Union(rangeToAdd);
            }

            combined.Add(rangeToAdd);

        }


        foreach (var range in combined.OrderBy(r => r.Start))
        {
            System.Console.WriteLine(range);
            result += range.Length;
        }


        System.Console.WriteLine("Part 2 = {0}", result);

    }

    record Range(long Start, long End)
    {
        public bool Contains(long value) => value >= Start && value <= End;
        public bool Contains(Range other) => Contains(other.Start) && Contains(other.End);
        public bool Overlaps(Range other) => Contains(other) || other.Contains(Start) || other.Contains(End);
        public Range Union(Range other) => new(Math.Min(Start, other.Start), Math.Max(End, other.End));

        public long Length = End - Start + 1;
    }

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