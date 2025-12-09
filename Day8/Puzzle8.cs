//#define Sample


using System.Collections.Concurrent;

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
        System.Console.WriteLine(distances.Count);


        long result = 1;

        var circuits = new List<HashSet<Point3d>>();
        var singles = new HashSet<Point3d>(points);

        while (distances.Count > 0 && top > 0)
        {
            var pair = distances.Dequeue();
            //    System.Console.WriteLine(pair);
            singles.Remove(pair.Item1);
            singles.Remove(pair.Item2);

            var c1 = circuits.FirstOrDefault(c => c.Contains(pair.Item1));
            var c2 = circuits.FirstOrDefault(c => c.Contains(pair.Item2));
            if (c1 != null && c2 != null && c1 != c2)
            {
                // Merge 2 Circuits
                foreach (var p in c2)
                {
                    c1.Add(p);
                }
                circuits.Remove(c2);
            }
            else
            {
                var found = false;
                foreach (var circuit in circuits)
                {
                    // Can we extend an existing circut?
                    if (circuit.Contains(pair.Item1) || circuit.Contains(pair.Item2))
                    {
                        // Add both, the hashset will eliminate the duplicate
                        circuit.Add(pair.Item1);
                        circuit.Add(pair.Item2);
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    // Start a new circut
                    circuits.Add(new HashSet<Point3d>([pair.Item1, pair.Item2]));
                }
            }
            top--;

        }

        System.Console.WriteLine("{0} Circuits", circuits.Count + singles.Count);
        circuits.Sort((a, b) => b.Count - a.Count);

        System.Console.WriteLine("Top 3 are:");
        for (int i = 0; i < 3; i++)
        {
            HashSet<Point3d> circuit = circuits[i];
            result *= circuit.Count;
            System.Console.Write("{0} points: ", circuit.Count);
            foreach (var point in circuit)
            {
                System.Console.Write(point);
                System.Console.Write(' ');

            }
            System.Console.WriteLine();
        }


        System.Console.WriteLine("Part 1 = {0}", result);



        System.Console.WriteLine("Part 2 = {0}", result);

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
