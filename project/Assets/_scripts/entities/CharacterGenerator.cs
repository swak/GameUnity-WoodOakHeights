using UnityEngine;
using System.Collections;
using System; // used for Enum class

public class CharacterGenerator : MonoBehaviour
{
	private PlayerCharacter _toon;
	private const int STARTING_POINTS = 300;
	private const int MIN_STARTING_ATTRIBUTE_VALUE = 10;
	private const int STARTING_VALUE = 100;
	private int pointsLeft;
	
	private const int OFFSET = 5;
	private const int LINE_HEIGHT = 20;
	
	private const int STAT_LABEL_WIDTH = 100;
	private const int BASEVALUE_LABEL_WIDTH = 30;
	private const int BUTTON_WIDTH = 20;
	private const int BUTTON_HEIGHT = 20;
	
	private int statStartingPos = 40;
	
	public GameObject playerPrefab;
	
	void Start()
	{
		// initialize character generator
		GameObject pc = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		pc.name = "pc";
		
		_toon = pc.GetComponent<PlayerCharacter>();
		
		pointsLeft = STARTING_POINTS;
		
		for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++)
		{
			_toon.GetPrimaryAttribute(cnt).BaseValue = STARTING_VALUE;
			pointsLeft -= (STARTING_VALUE - MIN_STARTING_ATTRIBUTE_VALUE);
		}
		
		_toon.StatUpdate();
	}
	
	void OnGUI()
	{
		DisplayName();
		DisplayPointsLeft();
		DisplayAttributes();
		DisplayVitals();
		DisplaySkills();
	}
	
	private void DisplayName()
	{
		GUI.Label(new Rect(10, 10, 50, 25), "Name:");
		_toon.Name = GUI.TextField(new Rect(65, 10, 100, 25), _toon.Name);
	}
	
	private void DisplayPointsLeft()
	{
		GUI.Label(new Rect(250, 10, 100, 25), "Points Left:" + pointsLeft.ToString());
	}
	
	private void DisplayAttributes()
	{
		for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++)
		{
			GUI.Label(new Rect(	OFFSET,									// x
								statStartingPos + (cnt * LINE_HEIGHT),	// y
								STAT_LABEL_WIDTH,						// width
								LINE_HEIGHT								// height
				), ((AttributeName)cnt).ToString());
			GUI.Label(new Rect(	STAT_LABEL_WIDTH + OFFSET,				// x
								statStartingPos + (cnt * LINE_HEIGHT),	// y
								BASEVALUE_LABEL_WIDTH,					// width
								LINE_HEIGHT								// height
				), _toon.GetPrimaryAttribute(cnt).AdjustedBaseValue.ToString());
			
			if(GUI.Button(new Rect(	OFFSET + STAT_LABEL_WIDTH + BASEVALUE_LABEL_WIDTH,	// x
									statStartingPos + (cnt * BUTTON_HEIGHT),			// y
									BUTTON_WIDTH,										// width
									BUTTON_HEIGHT										// height
				), "+")) {
				if(pointsLeft > 0) {
					_toon.GetPrimaryAttribute(cnt).BaseValue++;
					pointsLeft--;
					_toon.StatUpdate();
				}
			}
			
			if(GUI.Button(new Rect(	OFFSET + STAT_LABEL_WIDTH + BASEVALUE_LABEL_WIDTH + BUTTON_WIDTH,	// x
									statStartingPos + (cnt * BUTTON_HEIGHT),							// y
									BUTTON_WIDTH,														// width
									BUTTON_HEIGHT														// height
				), "-")) {
				if(_toon.GetPrimaryAttribute(cnt).BaseValue > MIN_STARTING_ATTRIBUTE_VALUE) {
					_toon.GetPrimaryAttribute(cnt).BaseValue--;
					pointsLeft++;
					_toon.StatUpdate();
				}
			}
		}
	}
	
	private void DisplayVitals()
	{
		for(int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++)
		{
			GUI.Label(new Rect(	OFFSET,
								statStartingPos + ((cnt + 7) * LINE_HEIGHT),
								STAT_LABEL_WIDTH,
								LINE_HEIGHT
				), ((VitalName)cnt).ToString());
			GUI.Label(new Rect(	OFFSET + STAT_LABEL_WIDTH,
								statStartingPos + ((cnt + 7) * LINE_HEIGHT),
								BASEVALUE_LABEL_WIDTH,
								LINE_HEIGHT
				), _toon.GetVital(cnt).AdjustedBaseValue.ToString());
		}
	}
	
	private void DisplaySkills()
	{
		for(int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt++)
		{
			GUI.Label(new Rect(	OFFSET + STAT_LABEL_WIDTH + BASEVALUE_LABEL_WIDTH + BUTTON_WIDTH * 2 + OFFSET * 2,
								statStartingPos + (cnt * LINE_HEIGHT),
								STAT_LABEL_WIDTH,
								LINE_HEIGHT
				), ((SkillName)cnt).ToString());
			GUI.Label(new Rect(	OFFSET + STAT_LABEL_WIDTH + BASEVALUE_LABEL_WIDTH + BUTTON_WIDTH * 2 + OFFSET * 2 + STAT_LABEL_WIDTH,
								statStartingPos + (cnt * LINE_HEIGHT),
								BASEVALUE_LABEL_WIDTH,
								LINE_HEIGHT
				), _toon.GetSkill(cnt).AdjustedBaseValue.ToString());
		}
	}
	
	private void UpdateCurVitalValues()
	{
		for(int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++)
		{
			_toon.GetVital(cnt).CurValue = _toon.GetVital(cnt).AdjustedBaseValue;
		}
	}
	
	private void DisplayCreateLabel()
	{
	}
	
	private void DisplayCreateButtonLabel()
	{
	}
}
