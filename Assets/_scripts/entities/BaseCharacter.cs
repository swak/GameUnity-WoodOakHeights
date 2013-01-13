using UnityEngine;
using System.Collections;
using System; // added to access the enum class

public class BaseCharacter : MonoBehaviour {
	private string _name;
	private int _level;
	private uint _freeExp;
	
	private Attribute[] _primaryAttribute;
	private Vital[] _vital;
	private Skill[] _skill;
	
	public void Awake()
	{
		_name = string.Empty;
		_level = 0;
		_freeExp = 0;
		
		_primaryAttribute = new Attribute[Enum.GetValues(typeof(AttributeName)).Length];
		_vital = new Vital[Enum.GetValues(typeof(VitalName)).Length];
		_skill = new Skill[Enum.GetValues(typeof(SkillName)).Length];
		
		SetupPrimaryAttributes();
		SetupVitals();
		SetupSkills();
	}
	
	#region BaseCharacter Getters and Setters
	public string Name {
		get{ return _name; }
		set{ _name = value; }
	}
	public int Level {
		get{ return _level; }
		set{ _level = value; }
	}
	public uint FreeExp {
		get{ return _freeExp; }
		set{ _freeExp = value; }
	}
	#endregion
	
	public void AddExp(uint exp)
	{
		_freeExp += exp;
		CalculateLevel();
	}
	
	public void CalculateLevel()
	{
		// take average of all the player skills and assign that as a player level
	}
	
	private void SetupPrimaryAttributes()
	{
		for(int cnt = 0; cnt < _primaryAttribute.Length; cnt++)
		{
			_primaryAttribute[cnt] = new Attribute();
			_primaryAttribute[cnt].Name = ((AttributeName)cnt).ToString();
		}
	}
	
	private void SetupVitals()
	{
		for(int cnt = 0; cnt < _vital.Length; cnt++)
			_vital[cnt] = new Vital();
		
		SetupVitalModifiers();
	}
	
	private void SetupSkills()
	{
		for(int cnt = 0; cnt < _skill.Length; cnt++)
			_skill[cnt] = new Skill();
		
		SetupSkillModifiers();
	}
	
	public Attribute GetPrimaryAttribute(int index)
	{
		return _primaryAttribute[index];
	}
	
	public Vital GetVital(int index)
	{
		return _vital[index];
	}
	
	public Skill GetSkill(int index)
	{
		return _skill[index];
	}
	
	private void SetupVitalModifiers()
	{
		// health modifier
		GetVital((int)VitalName.Health).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Strength), .55f));
		GetVital((int)VitalName.Health).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Dexterity), .2f));
		GetVital((int)VitalName.Health).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Willpower), .12f));
		// stamnia modifier
		GetVital((int)VitalName.Stamina).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Dexterity), .4f));
		GetVital((int)VitalName.Stamina).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Willpower), .05f));
		GetVital((int)VitalName.Stamina).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Strength), .1f));
	}
	
	private void SetupSkillModifiers()
	{
		// melee attack
		GetSkill((int)SkillName.Melee_Attack).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Strength), .5f));
		GetSkill((int)SkillName.Melee_Attack).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Dexterity), .1f));
		// melee defense
		GetSkill((int)SkillName.Melee_Defense).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Strength), .7f));
		GetSkill((int)SkillName.Melee_Defense).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Dexterity), .2f));
		// ranged attack
		GetSkill((int)SkillName.Ranged_Attack).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Dexterity), .6f));
		GetSkill((int)SkillName.Ranged_Attack).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Willpower), .3f));
		// ranged defense
		GetSkill((int)SkillName.Ranged_Defense).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Strength), .2f));
		GetSkill((int)SkillName.Ranged_Defense).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Dexterity), .4f));
	}
	
	public void StatUpdate()
	{
		for(int cnt = 0; cnt < _vital.Length; cnt++)
			_vital[cnt].Update();
		for(int cnt = 0; cnt < _skill.Length; cnt++)
			_skill[cnt].Update();
	}
}
