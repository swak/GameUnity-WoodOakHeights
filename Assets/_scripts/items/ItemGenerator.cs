using UnityEngine;

public static class ItemGenerator
{
	public const int BASE_MELEE_RANGE = 1;
	public const int BASE_RANGE_RANGE = 5;
	
	public static Item CreateItem()
	{
		// decide on what type of item to make
		// call the method to create the base item type
		Item item = CreateWeapon();
		
		item.Value = Random.Range(1,51);
		item.Rarity = RarityTypes.Common;
		item.MaxDurability = Random.Range(50,61);
		item.CurDurability = item.MaxDurability;
		
		
		// return the new item
		return item;
	}
	private static Weapon CreateWeapon()
	{
		// decide if we make a melee or ranged weapon
		Weapon weapon = CreateMeleeWeapon();
		
		// return the weapon created
		return weapon;
	}
	
	private static Weapon CreateMeleeWeapon()
	{
		Weapon meleeWeapon = new Weapon();
		// fill in all teh values for that item type
		meleeWeapon.Name = "MW:" + Random.Range(1,100);
		
		// assign the max damage of the weapon
		meleeWeapon.MaxDamage = Random.Range(5, 11);
		meleeWeapon.DamageVariance = Random.Range(.2f, .76f);
		meleeWeapon.TypeOfDamage = DamageType.Slash;
		meleeWeapon.MaxRange = BASE_MELEE_RANGE;
		
		return meleeWeapon;
	}
}


public enum ItemTypes
{
	Consumable,
	Weapon
}