//#define Sample

public class Puzzle2 : IPuzzle
{

    public void Excute(FileInfo input)
    {
#if Sample
        var line = sample;
#else
        var line = File.ReadAllText(input.FullName);
#endif
        long result;

        var ranges = from x in line.Split(',')
                     let parts = x.Split('-')
                     select (low: long.Parse(parts[0]), high: long.Parse(parts[1]));

        result = Part1(ranges);

        System.Console.WriteLine("Part 1 = {0}", result);


        result = Part2(ranges);

        System.Console.WriteLine("Part 2 = {0}", result);

    }

    private static long Part1(IEnumerable<(long low, long high)> ranges)
    {
        long result = 0;
        foreach (var (low, high) in ranges)
        {
            for (long value = low; value <= high; value++)
            {
                var digits = value.ToString();
                if (digits.Length % 2 != 0)
                {
                    continue;
                }

                bool isInvalid = digits[0..(digits.Length / 2)] == digits[(digits.Length / 2)..];

                if (isInvalid)
                {
                    System.Console.WriteLine("Invalid: {0}", digits);
                    result += value;
                }

            }
        }

        return result;
    }

    private static long Part2(IEnumerable<(long low, long high)> ranges)
    {
        long result = 0;
        foreach (var (low, high) in ranges)
        {
            for (long value = low; value <= high; value++)
            {
                var digits = value.ToString();

                for (int groupSize = 1; groupSize < digits.Length; groupSize += 1)
                {
                    if (digits.Length % groupSize != 0)
                    {
                        continue;
                    }

                    bool isInvalid = ContainsRepeatedDigitGroups(digits, groupSize);

                    if (isInvalid)
                    {
                        System.Console.WriteLine("Invalid: {0}", digits);
                        result += value;
                        break;
                    }
                }
            }
        }

        return result;
    }


    private static bool ContainsRepeatedDigitGroups(string digits, int g)
    {
        var first = digits.Substring(0, g);
        for (int i = 1; i < digits.Length / g; i++)
        {
            var group = digits.Substring(i * g, g);
            if (group != first)
            {
                return false;
            }
        }

        return true;
    }


    private string sample = """
11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124
""";
}