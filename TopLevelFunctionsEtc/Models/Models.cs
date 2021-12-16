namespace TopLevelFunctionsEtc.Models;

public record Test(string a, string b);

public record Person
{
	public string? FirstName { get; init; }
	public string? LastName { get; init; }
}

public record IdPerson : Person
{
	public int ID { get; init; }
}

public record Building(string Street, string City);
public record PublicBuilding(string Street, string City, bool IsOpen) : Building(Street, City);

record Smarty(string Color);
record SmartyWithCount(string Color, int Count) : Smarty(Color);
record SmartOverlord(string Color) : Smarty(Color);

class Vehicle { }
class Bus : Vehicle { }
class Car : Vehicle { }


class ThisCantBeTrusted
{
	public static bool operator == (ThisCantBeTrusted? left, ThisCantBeTrusted? right)
	{
		return false;
	}

	public static bool operator != (ThisCantBeTrusted? left, ThisCantBeTrusted? right)
	{
		return false;
	}
}
