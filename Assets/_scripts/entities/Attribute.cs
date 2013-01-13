public class Attribute : BaseStat {
	new public const int STARTING_EXP_COST = 50;
	
	public Attribute()
	{
		ExpToLevel = STARTING_EXP_COST;
		LevelModifier = 1.1f;
	}
}

public enum AttributeName
{
	Dexterity,
	Strength,
	Willpower
}