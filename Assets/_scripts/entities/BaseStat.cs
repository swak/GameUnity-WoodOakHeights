/// <summary>
/// BaseStat
/// </summary>
public class BaseStat
{
	public const int STARTING_EXP_COST = 100;
	
	private int _baseValue; // the base value of this stat
	private int _buffValue; // the amount of the buff to this stat
	private int _expToLevel; // the total amout of experience needed to raise this skill
	private float _levelModifier; // the modifier applied to the exp need to raise the skill
	
	private string _name;		// this is the name of the attribute
	
	public BaseStat()
	{
		UnityEngine.Debug.Log("BaseStat Created");
		_baseValue = 0;
		_buffValue = 0;
		_levelModifier = 1.1f;
		_expToLevel = STARTING_EXP_COST;
		_name = "";
	}
	
	#region Basic Setters and Getters
	public int BaseValue {
		get{ return _baseValue; }
		set{ _baseValue = value; }
	}
	public int BuffValue {
		get{ return _buffValue; }
		set{ _buffValue = value; }
	}
	public int ExpToLevel {
		get{ return _expToLevel; }
		set{ _expToLevel = value; }
	}
	public float LevelModifier {
		get{ return _levelModifier; }
		set{ _levelModifier = value; }
	}
	public string Name {
		get{ return _name; }
		set{ _name = value; }
	}
	#endregion
	
	private int CalculateExpToLevel()
	{
		return (int)(_expToLevel * _levelModifier); // typecasting to an integer
	}
	
	public int AdjustedBaseValue
	{
		get{ return _baseValue + _buffValue; }
	}
	
	public void LevelUp()
	{
		_expToLevel = CalculateExpToLevel();
		_baseValue++;
	}
}
