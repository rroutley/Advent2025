//#define Sample

public class Puzzle3 : IPuzzle
{

    public void Excute(FileInfo input)
    {
#if Sample
        var lines = sample.Split("\r\n");
#else
        var lines = File.ReadAllLines(input.FullName);
#endif

        long result = TotalJoltage(lines, 2);

        System.Console.WriteLine("Part 1 = {0}", result);

        result = TotalJoltage(lines, 12);

        System.Console.WriteLine("Part 2 = {0}", result);
    }

    private static long TotalJoltage(string[] lines, int length)
    {
        long result = 0;
        foreach (var bank in lines)
        {
            var batteries = (from c in bank select c - '0').ToArray();

            long joltage = 0;
            int startDigit = 0;
            for (int i = length - 1; i >= 0; i--)
            {
                int j = GreatestInRegion(batteries, i, ref startDigit);
                joltage = joltage * 10 + j;
                startDigit++;
            }

            System.Console.WriteLine("Bank {0} can deliver {1} jolts", bank, joltage);

            result += joltage;
        }

        return result;
    }

    private static int GreatestInRegion(int[] batteries, int skipLast, ref int digit)
    {
        int max = 0;
        for (int i = digit; i < batteries.Length - skipLast; i++)
        {
            var charge = batteries[i];
            if (charge > max)
            {
                max = charge;
                digit = i;
            }
        }

        return max;
    }

    private string sample = """
987654321111111
811111111111119
234234234234278
818181911112111
""";
}