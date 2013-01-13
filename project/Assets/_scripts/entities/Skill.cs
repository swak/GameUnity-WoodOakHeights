public class Skill : ModifiedStat {
	private bool _known;
	
	public Skill()
	{
		_known = false;
		ExpToLevel = 25;
		LevelModifier = 1.7f;
	}
	
	public bool Known {
		get{ return _known; }
		set{ _known = value; }
	}
}

public enum SkillName
{
	Melee_Attack,
	Melee_Defense,
	Ranged_Attack,
	Ranged_Defense
}