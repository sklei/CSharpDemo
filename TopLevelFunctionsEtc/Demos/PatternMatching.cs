// namespace Demo;

public class PatternMatchingDemo : IDemo
{
	public void DoDemo()
	{
		WriteLine("- Smarties ----------------------");
		var smarties = new Smarty[] {
			new SmartyWithCount("Red", 12),
			new Smarty("Gray"),
			new SmartyWithCount("Green", 10) with { Color = "Toch Blauw" },
			new SmartyWithCount("Red", 9001),
			new SmartOverlord("PureBlack")
		};

		var smartiesWithCount = new SmartyWithCount[] {
			new SmartyWithCount("Red", 12),
			new SmartyWithCount("Yellow", 20),
			new SmartyWithCount("Green", 10),
			new SmartyWithCount("Red", 9001)
		};

		string GetSmarty(SmartyWithCount swc) => swc.Count switch
		{
			< 10 => "A",
			>= 10 => "B",
			// _ => "C" //The pattern is unreachable. It has already been handled by a previous arm of the switch expression or it is impossible to match.
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
			SmartOverlord so => "Don't question the Smarty Overlord!",
			SmartyWithCount s when s.Color == "Green" => "Green Smarties are cool!",
			SmartyWithCount s when s.Color == "Red" && s.Count > 9000 => "Red Smarties are cool if over 9000!",
			SmartyWithCount s when s.Count < 5 => "A",
			SmartyWithCount s when s.Count < 10 => "B",
			SmartyWithCount s when s.Count >= 10 && s.Count < 50 => "B",
			{ Color: "Gray"} => "Gray privileges!",
			_ => throw new ArgumentException($"{swc} is not a valid smarty!") 
		};

		var smtsWithCount = smartiesWithCount.Select((val, idx) => (val, idx)).ToList();
		smtsWithCount.ForEach(smt => WriteLine($"GetSmarty(s) #{smt.idx}: {GetSmarty(smt.val)}"));
		smtsWithCount.ForEach(smt => WriteLine($"GetSmartyEx1(s) #{smt.idx}: {GetSmartyEx1(smt.val)}"));
		
		var smtsNormal = smarties.Select((val, idx) => (val, idx)).ToList();
		smtsNormal.ForEach(smt => WriteLine($"GetSmartyEx2(s) #{smt.idx}: {GetSmartyEx2(smt.val)}"));
		
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

		var whoOhWho1 = demoTime1 is { stockholmSyndromeFamiliar: { name: "Matthijs", customer: { customerName: "GDH" }}};
		var whoOhWho2 = demoTime2 is { stockholmSyndromeFamiliar: { name: "Matthijs" }};
		var whoOhWho3 = demoTime3 is { stockholmSyndromeFamiliar: { name: "Matthijs", customer: { customerName: "GDH" }}};
		WriteLine($"Is Matthijs working for GHD!?: {whoOhWho1}");
		WriteLine($"Is Matthijs working!?: {whoOhWho2}");
		WriteLine($"Is Lou working for GHD!?: {whoOhWho3}");
	}
}

record OneFox(OneFoxee stockholmSyndromeFamiliar);
record OneFoxee(string name, OneFoxVictim customer);
record OneFoxVictim(string customerName);