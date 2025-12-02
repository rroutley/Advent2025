class Puzzle1 : IPuzzle
{
    public void Excute(FileInfo input)
    {
        var lines = sample.Split("\r\n");
        lines = File.ReadAllLines(input.FullName);

        int result = Part1(lines);

        System.Console.WriteLine("Part 1 = {0}", result);

        result = Part2(lines);

        System.Console.WriteLine("Part 2 = {0}", result);
    }

    private static int Part1(string[] lines)
    {
        var position = 50;
        var zeros = 0;

        foreach (var line in lines)
        {
            var direction = line[0];
            var distance = int.Parse(line[1..]);

            if (direction == 'L')
            {
                position -= distance;

            }
            else if (direction == 'R')
            {
                position += distance;
            }
            else
            {
                throw new Exception("Unknown direction");
            }
            position = (position + 100) % 100;

            if (position == 0)
            {
                zeros += 1;
            }
        }

        return zeros;
    }



    private static int Part2(string[] lines)
    {
        var position = 50;
        var zeros = 0;

        foreach (var line in lines)
        {
            var direction = line[0];
            var distance = int.Parse(line[1..]);

            if (direction == 'L')
            {
                if (position == 0) position += 100;

                position -= distance;

                while (position <= 0)
                {
                    position += 100;
                    zeros += 1;
                }

            }
            else if (direction == 'R')
            {
                position += distance;

                while (position >= 100)
                {
                    position -= 100;
                    zeros += 1;
                }
            }
            else
            {
                throw new Exception("Unknown direction");
            }

            position = (position + 100) % 100;

        }

        return zeros;
    }

    private string sample = """
L68
L30
R48
L5
R60
L55
L1
L99
R14
L82
""";
}