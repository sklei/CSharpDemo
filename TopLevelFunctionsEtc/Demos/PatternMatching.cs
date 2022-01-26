using TopLevelFunctionsEtc.Models;

namespace TopLevelFunctionsEtc.Demos;

public class PatternMatchingDemo : IDemo
{
	public void DoDemo()
	{
		WriteLine("- Smarties ----------------------");
		var countSmarty = new SmartyWithCount("Red", 420);
		var overlordSmarty = new SmartyOverlord("PureBlack");

		void HandleSmarty(Smarty s)
		{
			if(s is SmartyWithCount smc) {
				WriteLine($"Has a count! {smc.Count}");
			} else if(s is SmartyOverlord os) {
				WriteLine($"Is {os.Color} overlord");
			}

			if(s is SmartyOverlord { Color: "PureBlack" }) {
				WriteLine("😎");
			}

			if(s is SmartyWithCount { Count: > 100 } swc) {
				WriteLine($"Has a count over 100! {swc.Count}");
			}
		}

		HandleSmarty(countSmarty);
		HandleSmarty(overlordSmarty);

		var smarties = new Smarty[] {
			new SmartyWithCount("Red", 12),
			new Smarty("Gray"),
			new SmartyWithCount("Green", 10) with { Color = "Toch Blauw" },
			new SmartyWithCount("Red", 9001),
			new SmartyOverlord("PureBlack")
		};

		var smartiesWithCount = new SmartyWithCount[] {
			new SmartyWithCount("Red", 12),
			new SmartyWithCount("Yellow", 20),
			new SmartyWithCount("Green", 10),
			new SmartyWithCount("Red", 9001)
		};

		WriteLine("GetSmartyOldWay ---------------------");

		smarties.ToList().ForEach(s => GetSmartyOldWay(s));

		void GetSmartyOldWay(Smarty s) 
		{
			switch(s)
			{
				case SmartyOverlord overlordSmarty:
					WriteLine("Overlord smarty");
					break;
				case SmartyWithCount swcOver10 when swcOver10.Count > 10 && swcOver10.Color == "Red":
					WriteLine("Has count and is red");
					break;
				case SmartyWithCount swcOver10 when swcOver10.Count > 10:
					WriteLine("Has count");
					break;
				case Smarty normalSmarty:
					WriteLine("Normal smarty");
					break;
			}
		}

		WriteLine("GetSmartyOldWay ---------------------");

		string GetSmarty(SmartyWithCount swc) => swc.Count switch
		{
			< 10 => "A",
			>= 10 => "B",
			//_ => "C" //The pattern is unreachable. It has already been handled by a previous arm of the switch expression or it is impossible to match.
		};

		string GetSmartyEx1(SmartyWithCount swc) => swc switch
		{
			{ Color: "Green" } => $"Green Smarties are cool! {swc}",
			{ Color: "Yellow" } => $"Yellow Smarties are cool! {swc}",
			{ Color: "Red", Count: > 9000 } => $"Red Smarties are also cool! {swc}",
			_ => $"Well I don't know! {swc}" 
		};

		string GetSmartyEx2(Smarty swc) => swc switch
		{
			SmartyOverlord so => "Don't question the Smarty Overlord!",
			SmartyWithCount s when s.Color == "Green" => "Green Smarties are cool!",
			SmartyWithCount s when s.Color == "Red" && s.Count > 9000 => "Red Smarties are cool if over 9000!",
			SmartyWithCount s when s.Count < 5 => "A",
			SmartyWithCount s when s.Count < 10 => "B",
			SmartyWithCount s when s.Count >= 10 && s.Count < 50 => "B",
			{ Color: "Gray"} => "Gray privileges!",
			_ => throw new ArgumentException($"{swc} is not a valid smarty!") 
		};

		string GetSmartyEx3(Smarty swc) => swc switch
		{
			SmartyOverlord so => "Don't question the Smarty Overlord!",
			SmartyWithCount { Color: "Green" } => "Green Smarties are cool!",
			SmartyWithCount { Color: "Red", Count: > 9000 } => "Red Smarties are cool if over 9000!",
			SmartyWithCount { Count: < 5 } smolSmarty => $"A {smolSmarty.Color}",
			SmartyWithCount { Count: < 10 } => "B",
			SmartyWithCount { Count: >= 10, Count: < 50 } => "B",
			{ Color: "Gray"} => "Gray privileges!",
			_ => throw new ArgumentException($"{swc} is not a valid smarty!") 
		};

		string GetTest(Smarty s) => s switch
		{
			SmartyOverlord so => "Overlord",
			SmartyWithCount swc when swc.Count > 100 => "Count over 100",
			{ Color: "Red" } => "Red",

			_ => "Oops"
		};

		var smtsWithCount = smartiesWithCount.Select((val, idx) => (val, idx)).ToList();
		smtsWithCount.ForEach(smt => WriteLine($"GetSmarty #{smt.idx}: {GetSmarty(smt.val)}"));
		smtsWithCount.ForEach(smt => WriteLine($"GetSmartyEx1 #{smt.idx}: {GetSmartyEx1(smt.val)}"));
		
		var smtsNormal = smarties.Select((val, idx) => (val, idx)).ToList();
		smtsNormal.ForEach(smt => WriteLine($"GetSmartyEx2(s) #{smt.idx}: {GetSmartyEx2(smt.val)}"));
		smtsNormal.ForEach(smt => WriteLine($"GetSmartyEx3(s) #{smt.idx}: {GetSmartyEx3(smt.val)}"));
		
		WriteLine("- Logical patterns ----------------------");
		static int GetTax(int muncipalityId) => muncipalityId switch
		{
			0 or 1 => 20,
			> 1 and < 5 => 21,
			> 5 and not 7 => 22,
			7 => 23,
			_ => 20
		};

		WriteLine($"GetTax for municipality 1: {GetTax(1)}");
		WriteLine($"GetTax for municipality 4: {GetTax(4)}");
		WriteLine($"GetTax for municipality 7: {GetTax(7)}");
		WriteLine($"GetTax for municipality 123: {GetTax(123)}");

		WriteLine("- Comparison ----------------------");
		ThisCantBeTrusted? thisIsVeryNull = null;
		object? thisIsAnObject = null;

		WriteLine($"thisIsVeryNull == null {thisIsVeryNull == null}");
		WriteLine($"thisIsVeryNull is null {thisIsVeryNull is null}");
		WriteLine($"thisIsVeryNull is not null {thisIsVeryNull is not null}");
		WriteLine($"thisIsVeryNull is string {thisIsVeryNull is string}");
		WriteLine($"thisIsAnObject is not DateTime {thisIsAnObject is not DateTime}");

		WriteLine("- Vehicles ----------------------");
		static int GetSpeed(Vehicle vehicle) => vehicle switch
		{
			Bus => 75,
			Car => 100,
			_ => 50
		};

		WriteLine($"Speed of car {GetSpeed(new Car())}");
		WriteLine($"Speed of bus {GetSpeed(new Bus())}");
		WriteLine($"Speed of vehicle {GetSpeed(new Vehicle())}");

		WriteLine("- Letters ----------------------");

		bool IsLetter1(char c) => c >= 'a' && c <= 'z' || c >= 'A' && c <= 'Z'; // C# 8
		bool IsLetter2(char c) => c is >= 'a' and <= 'z' or >= 'A' and <= 'Z';// C# 9

		WriteLine($"Is A a letter #1: {IsLetter1('A')}");
		WriteLine($"Is B a letter #1: {IsLetter2('A')}");
		WriteLine($"Is 6 a letter #1: {IsLetter2('6')}");

		WriteLine("- Extended Property Matching ----------------------");
		var demoTime1 = new OneFox(
			new OneFoxee(
				"Matthijs",
				new OneFoxVictim("AWVN")
			)
		);
		var demoTime2 = new OneFox(
			new OneFoxee(
				"Matthijs",
				new OneFoxVictim("GDH")
			)
		);
		var demoTime3 = new OneFox(
			new OneFoxee(
				"Lou",
				new OneFoxVictim("WRIJ")
			)
		);

		var whoOhWho1 = demoTime1 is { stockholmSyndromeFamiliar: { name: "Matthijs" }};
		var whoOhWho2 = demoTime2 is { stockholmSyndromeFamiliar: { name: "Matthijs", customer: { customerName: "GDH" }}}; //nesting
		var whoOhWho3 = demoTime3 is { stockholmSyndromeFamiliar: { name: "Lou", customer.customerName: "GDH" }}; //dot notation for nesting
		WriteLine($"Is Matthijs working!?: {whoOhWho1}");
		WriteLine($"Is Matthijs working for GDH!?: {whoOhWho2}");
		WriteLine($"Is Lou working for GDH!?: {whoOhWho3}");

		WriteLine("- Object deconstruction, Tuple creation and matching ----------------------");

		static decimal GetGroupTicketPriceDiscount(int groupSize, DateTime visitDate) =>
			(groupSize, visitDate.DayOfWeek) switch
		{
			(<= 0, _) => throw new ArgumentException( "Group size must be positive."),
			(_, DayOfWeek.Saturday or DayOfWeek.Sunday) => 0.0m,
			(>= 5 and < 10, DayOfWeek.Monday) => 20.0m,
			(>= 10, DayOfWeek.Monday) => 30.0m,
			(>= 5 and < 10, _) => 12.0m,
			(>= 10, _) => 15.0m,
			_ => 0.0m,
		};
	}
}
	

record OneFox(OneFoxee stockholmSyndromeFamiliar);
record OneFoxee(string name, OneFoxVictim customer);
record OneFoxVictim(string customerName);