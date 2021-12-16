using TopLevelFunctionsEtc.Models;

namespace TopLevelFunctionsEtc.Demos;

public class RecordsDemo : IDemo
{
	public void DoDemo()
	{
		var p = new Person() { FirstName = "" };
		Console.WriteLine(p);

		p = new Person() { FirstName = "First", LastName = "Last" };
		Console.WriteLine(p);

		p = p with { FirstName = "Different" };
		Console.WriteLine(p);

		var cp = new IdPerson() { FirstName = "Thomas", LastName = "van Duin", ID = 123 };
		Console.WriteLine(cp);
		Console.WriteLine(cp.FirstName);

		// cp.FirstName = "";
		// Init-only property or indexer 'CoolPerson.FirstName' can only be assigned in an 
		// object initializer, or on 'this' or 'base' in an instance constructor or an 'init' accessor. 

		var t = new Test("Value of A", "Value of B"); // positional construction
		var (a, b) = t; // positional deconstruction
		Console.WriteLine(a);
		Console.WriteLine(b);

		var p1 = new Person() { FirstName = "Lou", LastName = "B" };
		var p2 = new Person() { FirstName = "Lou", LastName = "B" };
		WriteLine($"p1 == p2: {p1 == p2}");
		WriteLine($"p1.Equals(p2): {p1.Equals(p2)}");
		WriteLine($"ReferenceEquals(p1, p2): {ReferenceEquals(p1, p2)}");


		WriteLine("Value-based equality and inheritance");
		var normalBuilding = new Building("Oudegracht 231", "Utrecht");
		var publicBuilding = new PublicBuilding("Oudegracht 231", "Utrecht", true);
		WriteLine($"normalBuilding == publicBuilding: {normalBuilding == publicBuilding}");

		WriteLine("SmartySort");
		var colors = new[] { "Red", "Green", "Yellow", "Pink", "Orange", "Brown", "Blue" };

		var smarties = Enumerable.Range(0, 50)
			.Select(x => new Smarty(colors[Random.Shared.Next(colors.Length)]))
			.GroupBy(s => s)
			.OrderBy(sg => sg.Count());

		smarties.ToList().ForEach(sg =>
			Console.WriteLine($"{sg.Key} #{sg.Count()}")
		);

	}
}


