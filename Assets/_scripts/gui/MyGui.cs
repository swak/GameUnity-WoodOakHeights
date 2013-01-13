using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MyGui : MonoBehaviour {
	public GUISkin playerMyGui;
	
	public float lootWindowHeight = 90;
	
	public float buttonWidth = 40;
	public float buttonHeight = 40;
	public float closeButtonWidth = 20;
	public float closeButtonHeight = 20;
	
	
	private float _offset = 10;
	/**************************************************/
	/*	Loot Window Variables */
	/**************************************************/
	private bool _displayLootWindow = false;
	private const int LOOT_WINDOW_ID = 0;
	private Rect _lootWindowRect = new Rect(0,0,0,0);
	private Vector2 _lootWindowSlider = Vector2.zero;
	public static Chest chest;
	
	private string _toolTip = "";
	
	/**************************************************/
	/*	Inventory Window Variables */
	/**************************************************/
	private bool _displayInventoryWindow = true;
	private const int INVENTORY_WINDOW_ID = 1;
	private Rect _inventoryWindowRect = new Rect(10, 10, 170, 265);
	private int _inventoryRows = 6;
	private int _inventoryCols = 4;
	
	private float _doubleClickTimer = 0;
	private const float DOUBLE_CLICK_TIMER_THRESHOLD = .5f;
	private Item _selectedItem;
	
	/**************************************************/
	/*	Character Window Variables */
	/**************************************************/
	private bool _displayCharacterWindow = true;
	private const int CHARACTER_WINDOW_ID = 2;
	private Rect _characterWindowRect = new Rect(285, 10, 170, 265);
	private int _characterPanel = 0;
	private string[] _characterPanelNames = new string[] {"EQP", "ATT", "SKL"};
	
	// Use this for initialization
	void Start () {
//		_lootItems = new List<Item>();
	}
	
	private void OnEnable()
	{
//		Messenger<int>.AddListener("PopulateChest", PopulateChest);
		Messenger.AddListener("DisplayLoot", DisplayLoot);
		Messenger.AddListener("CloseChest", ClearWindow);
	}
	
	private void OnDisable()
	{
//		Messenger<int>.RemoveListener("PopulateChest", PopulateChest);
		Messenger.RemoveListener("DisplayLoot", DisplayLoot);
		Messenger.RemoveListener("CloseChest", ClearWindow);
	}
			
	void OnGUI()
	{
		if(_displayCharacterWindow)
			_characterWindowRect = GUI.Window(CHARACTER_WINDOW_ID, _characterWindowRect, CharacterWindow, "Character Window");
		
		if(_displayInventoryWindow)
			_inventoryWindowRect = GUI.Window(INVENTORY_WINDOW_ID, _inventoryWindowRect, InventoryWindow, "Inventory Window");
		
		if(_displayLootWindow)
			_lootWindowRect = GUI.Window(LOOT_WINDOW_ID, new Rect(_offset, Screen.height - (_offset + lootWindowHeight), Screen.width - (_offset * 2), lootWindowHeight), LootWindow, "Loot Window");
		
		DisplayToolTip();
	}
			
	private void LootWindow(int id)
	{
		GUI.skin = playerMyGui;
		
		if(GUI.Button(new Rect(_lootWindowRect.width - _offset * 2, 0, closeButtonWidth, closeButtonHeight), "x", "Close Window Button")) {
			ClearWindow();
		}
		
		if(chest == null)
			return;
		
		if(chest.loot.Count == 0) {
			ClearWindow();
			return;
		}
		
		_lootWindowSlider = GUI.BeginScrollView(new Rect(_offset * .5f, 15, _lootWindowRect.width - _offset, 70), _lootWindowSlider, new Rect(0, 0, (chest.loot.Count * buttonWidth) + _offset, buttonHeight + _offset));
		
		for(int cnt = 0;  cnt < chest.loot.Count; cnt++)
		{
			if(GUI.Button(new Rect(_offset * .5f + (buttonWidth * cnt), _offset, buttonWidth, buttonHeight), new GUIContent(chest.loot[cnt].Name, chest.loot[cnt].ToolTip()))) {
//				Debug.Log(chest.loot[cnt].ToolTip());
				PlayerCharacter.Inventory.Add(chest.loot[cnt]);
				chest.loot.RemoveAt(cnt);
			}
		}
		
		GUI.EndScrollView();
		
		SetToolTip();
	}
	
//	private void PopulateChest(int x)
//	{
//		for(int cnt = 0; cnt < x; cnt++)
//			_lootItems.Add(new Item());
//		
//		_displayLootWindow = true;
//	}
			
	private void DisplayLoot()
	{
		_displayLootWindow = true;
	}
	
	private void ClearWindow()
	{
		_displayLootWindow = false;
//		_lootItems.Clear();
		
		chest.OnMouseUp();
		chest = null;
	}
	
	public void InventoryWindow(int id)
	{
		int cnt = 0;
		
		for(int y = 0; y < _inventoryRows; y++)
		{
			for(int x = 0; x < _inventoryCols; x++)
			{
				if(cnt < PlayerCharacter.Inventory.Count) {
					if(GUI.Button(new Rect(5 + (x * buttonWidth), 20 + (y * buttonHeight), buttonWidth, buttonHeight), PlayerCharacter.Inventory[cnt].Name)) {
						if(_doubleClickTimer != 0 && _selectedItem != null) {
							if(Time.time - _doubleClickTimer < DOUBLE_CLICK_TIMER_THRESHOLD) {
//								Debug.Log("Double Click: " + PlayerCharacter.Inventory[cnt].Name);
								
								if(PlayerCharacter.EquipedWeapon == null) {
									PlayerCharacter.EquipedWeapon = PlayerCharacter.Inventory[cnt];
									PlayerCharacter.Inventory.RemoveAt(cnt);
								} else {
									Item temp = PlayerCharacter.EquipedWeapon;
									PlayerCharacter.EquipedWeapon = PlayerCharacter.Inventory[cnt];
									PlayerCharacter.Inventory[cnt] = temp;
								}
								
								_doubleClickTimer = 0;
								_selectedItem = null;
							} else {
//								Debug.Log("Reset the double click timer");
								_doubleClickTimer = Time.time;
							}
						} else {
							_doubleClickTimer = Time.time;
							_selectedItem = PlayerCharacter.Inventory[cnt];
						}
					}
				} else {
					GUI.Label(new Rect(5 + (x * buttonWidth), 20 + (y * buttonHeight), buttonWidth, buttonHeight), (x + y * _inventoryCols).ToString());
				}
				
				cnt++;
			}
		}
		
		SetToolTip();
		GUI.DragWindow();
	}
	
	public void ToggleInventoryWindow()
	{
		_displayInventoryWindow = !_displayInventoryWindow;
	}
	
	public void CharacterWindow(int id)
	{
		_characterPanel = GUI.Toolbar(new Rect(5, 25, _characterWindowRect.width - 10, 50), _characterPanel, _characterPanelNames);
		
		switch(_characterPanel)
		{
		case 0:
			DisplayEquipment();
			break;
		case 1:
			DisplayAttributes();
			break;
		case 2:
			DisplaySkills();
			break;
		}
		
		GUI.DragWindow();
	}
	
	public void ToggleCharacterWindow()
	{
		_displayCharacterWindow = !_displayCharacterWindow;
	}
	
	private void DisplayEquipment()
	{
//		Debug.Log("Displaying Equipment");
		if(PlayerCharacter.EquipedWeapon == null) {
			GUI.Label(new Rect(5, 100, 40, 40), "X");
		} else {
//			GUI.Button(new Rect(5, 100, 40, 40), PlayerCharacter.EquipedWeapon.Name);
			if(GUI.Button(new Rect(5, 100, 40, 40), new GUIContent(PlayerCharacter.EquipedWeapon.Name, PlayerCharacter.EquipedWeapon.ToolTip()))) {
				PlayerCharacter.Inventory.Add(PlayerCharacter.EquipedWeapon);
				PlayerCharacter.EquipedWeapon = null;
			}
		}
		
		SetToolTip();
	}
	
	private void DisplayAttributes()
	{
//		Debug.Log("Displaying Attributes");
		GUI.BeginGroup(new Rect(5, 80, _characterWindowRect.width - (_offset * 2), _characterWindowRect.height - 70));
		GUI.Label (new Rect(0, 0, 50, 25), "Label");
		GUI.EndGroup();
		
	}
	
	private void DisplaySkills()
	{
//		Debug.Log("Displaying Skills");
	}
	
	private void SetToolTip()
	{
		if(Event.current.type == EventType.Repaint && GUI.tooltip != _toolTip) {
			if(_toolTip != "")
				_toolTip = "";
			
			if(GUI.tooltip != "")
				_toolTip = GUI.tooltip;
		}
	}
	
	private void DisplayToolTip()
	{
		if(_toolTip != "")
			GUI.Box(new Rect(Screen.width / 2 - 100, 10, 200, 100), _toolTip);
	}
}
