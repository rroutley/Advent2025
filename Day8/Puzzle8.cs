// #define Sample

public class Puzzle8 : IPuzzle
{

    public void Excute(FileInfo input)
    {
#if Sample
        var lines = sample.Split("\r\n");
        var top = 10;
#else
        var lines = File.ReadAllLines(input.FullName);
        var top = 1000;
#endif

        var points = new List<Point3d>();
        var distances = new PriorityQueue<(Point3d, Point3d), double>();

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            (int x, int y, int z) = line.Split(',', 3).Select(int.Parse).ToArray();
            points.Add(new Point3d(x, y, z));

            for (int j = 0; j < i; j++)
            {
                var distance = points[i].Dist(points[j]);
                distances.Enqueue((points[i], points[j]), distance);
            }

        }
        Console.WriteLine(distances.Count);


        long result = 1;

        var circuits = points.Select(p => new HashSet<Point3d>([p])).ToList();

        BuildCircuits(top, distances, circuits);

        Console.WriteLine("{0} Circuits", circuits.Count);
        circuits.Sort((a, b) => b.Count - a.Count);

        Console.WriteLine("Top 3 are:");
        for (int i = 0; i < 3; i++)
        {
            HashSet<Point3d> circuit = circuits[i];
            result *= circuit.Count;
            Console.Write("{0} points: ", circuit.Count);
            foreach (var point in circuit)
            {
                Console.Write(point);
                Console.Write(' ');

            }
            Console.WriteLine();
        }


        Console.WriteLine("Part 1 = {0}", result);


        var lastPair = BuildCircuits(int.MaxValue, distances, circuits);

        result = lastPair.Item1.X * lastPair.Item2.X;

        Console.WriteLine("Part 2 = {0}", result);

    }

    private (Point3d, Point3d) BuildCircuits(int top, PriorityQueue<(Point3d, Point3d), double> distances, List<HashSet<Point3d>> circuits)
    {
        while (distances.Count > 0 && top > 0)
        {
            var pair = distances.Dequeue();

            var c1 = circuits.First(c => c.Contains(pair.Item1));
            var c2 = circuits.First(c => c.Contains(pair.Item2));

            if (c1 != c2)
            {
                // Merge 2 Circuits
                foreach (var p in c2)
                {
                    c1.Add(p);
                }
                circuits.Remove(c2);
            }
            top--;

            if (circuits.Count == 1)
            {
                return pair;
            }
        }
        return (Point3d.Zero, Point3d.Zero);
    }

    record Point3d(int X, int Y, int Z)
    {
        public double Dist(Point3d other)
        {
            long dx = this.X - other.X;
            long dy = this.Y - other.Y;
            long dz = this.Z - other.Z;
            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }

        public static readonly Point3d Zero = new(0, 0, 0);

        public override string ToString()
        {
            return $"({X},{Y},{Z})";
        }
    }


    private string sample = """
162,817,812
57,618,57
906,360,560
592,479,940
352,342,300
466,668,158
542,29,236
431,825,988
739,650,466
52,470,668
216,146,977
819,987,18
117,168,530
805,96,715
346,949,466
970,615,88
941,993,340
862,61,35
984,92,344
425,690,689
""";
}
