using UnityEngine;
using System.Collections;

public class SpellGenerator : MonoBehaviour {
	Spell spell = new Buff();

	// Use this for initialization
	void Start ()
	{
		Spell spell = CreateSpell();
		
		Debug.Log( spell.Name );
		Debug.Log( spell.Rarity );
		Debug.Log( spell.LineOfSight );
		Debug.Log( spell.Description );
		Debug.Log( spell.BaseCoolDownTime );
		Debug.Log( spell.CoolDownVariance );
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public Spell CreateSpell()
	{
//		spell.Name = "Spell One";
//		spell.Rarity = RarityTypes.Common;
//		spell.LineOfSight = true;
//		spell.Description = "This is Spell One";
//		spell.BaseCoolDownTime = 2.5f;
//		spell.CoolDownVariance = Random.Range( -.2f, 0.2f );
		if( spell is Buff ) {
			Debug.Log( "Buff" );
		}
		else if ( spell is AoE) {
			Debug.Log( "AoE" );
		}
		else if ( spell is Bolt) {
			Debug.Log( "Bolt" );
		}
		
		return spell;
	}
}
