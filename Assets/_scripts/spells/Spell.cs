using UnityEngine;
using System.Collections;

public class Spell : ISpell {
	private float _coolDownTimer;
	private bool _ready;
	
	public string Name { get; set; }
	public RarityTypes Rarity { get; set; }
	public bool LineOfSight { get; set; }
	public string Description { get; set; }
	public float BaseCoolDownTime { get; set; }
	public float CoolDownVariance { get; set; }
	
	public Spell()
	{
		//add a path to the effects folder
		Name = "Need Name";
		Rarity = RarityTypes.Common;
		LineOfSight = true;
		Description = "Needs Description";
		BaseCoolDownTime = 2.0f;
		CoolDownVariance = 0.2f;
		CoolDownTimer = 0;
		Ready = true;
		
	}
	#region ISpell implementation
	public void Cast ()
	{
		throw new System.NotImplementedException ();
	}

	public void Update ()
	{
		throw new System.NotImplementedException ();
	}

	public GameObject Effect {
		get {
			throw new System.NotImplementedException ();
		}
		set {
			throw new System.NotImplementedException ();
		}
	}

	public float CoolDownTimer {
		get {
			return _coolDownTimer;
		}
		private set {
			_coolDownTimer = value;
		}
	}

	public bool Ready {
		get {
			return _ready;
		}
		private set {
			_ready = value;
		}
	}
	#endregion
}
