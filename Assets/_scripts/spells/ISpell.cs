using UnityEngine;

public interface ISpell
{
	string Name { get; set; }
	GameObject Effect { get; set; }
	RarityTypes Rarity { get; set; }
	bool LineOfSight { get; set; }
	string Description { get; set; }
	
	float BaseCoolDownTime { get; set; }
	float CoolDownVariance { get; set; }
	float CoolDownTimer { get; } // private set
	bool Ready { get; } // private set
	
	void Cast();
	void Update();
	
	//TODO
	//add to Spellbook
}
