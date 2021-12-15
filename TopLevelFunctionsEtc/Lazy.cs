public static class Lazy
{
	public static void WriteThisStuffToConsole(this object?[] values, string? header = null) {
		if(header is not null) 
			WriteLine(header);

		values
			.Select((val,idx) => (val,idx))
			.ToList()
			.ForEach(x => WriteLine($"[{x.idx}] {x.val}"));
	}
}