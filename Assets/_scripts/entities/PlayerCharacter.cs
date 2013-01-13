using System.Collections.Generic;

public class PlayerCharacter : BaseCharacter {
	private static PlayerCharacter instance = null;
	
	
	
	private static List<Item> _inventory = new List<Item>();
	
	
	
	
//	public static PlayerCharacter Instance {
//		get{
//			if(instance == null) {
//				Debug.Log("Instancing a new PC");
//				GameObject go = Instantiate(Resources.Load("_assets/Characters/PlayerDefault")) as GameObject;
//				go.name = "PC";
//			}
//			return instance;
//		}
//		
//	}
//	
//	public void Awake()
//	{
//		instance = this;
//	}
//	
	public static List<Item> Inventory {
		get{ return _inventory; }
	}
	
	private static Item _equipedWeapon;
	public static Item EquipedWeapon {
		get{ return _equipedWeapon; }
		set{ _equipedWeapon = value; }
	}
	
}
