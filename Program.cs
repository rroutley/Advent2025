using System.Reflection;

int? @override = null;

var lastPuzzle = Assembly.GetEntryAssembly()
                             .GetTypes()
                             .Where(t => typeof(IPuzzle).IsAssignableFrom(t) && t.IsClass)
                             .Select(type => (type, num: int.Parse(type.Name[6..])))
                           //  .Where(t => @override.HasValue && @override.Value == t.num)
                             .OrderByDescending(t => t.num)
                             .FirstOrDefault();

var puzzle = (IPuzzle)Activator.CreateInstance(lastPuzzle.type);

var input = new FileInfo($"Day{lastPuzzle.num}\\input.txt");
System.Console.WriteLine("Running {0} with input {1}", lastPuzzle.type.Name, input.FullName);

puzzle.Excute(input);