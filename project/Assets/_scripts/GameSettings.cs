using UnityEngine;
using System.Collections;
using System; // added to access the enum class

public class GameSettings : MonoBehaviour {
	
	void Awake()
	{
		DontDestroyOnLoad(this);
	}
	
	public void SaveCharacterData()
	{
		GameObject pc = GameObject.Find("pc");
		PlayerCharacter pcClass = pc.GetComponent<PlayerCharacter>();
	}
	
	public void LoadCharacterData()
	{
		GameObject pc = GameObject.Find("pc");
		PlayerCharacter pcClass = pc.GetComponent<PlayerCharacter>();
		
		pcClass.Name = PlayerPrefs.GetString("Player Name", "NAMELESS");
	}
}
