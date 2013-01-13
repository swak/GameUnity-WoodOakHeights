using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(BoxCollider))]
[RequireComponent (typeof(AudioSource))]

public class Chest : MonoBehaviour {
	public enum State
	{
		open,							// the chest is completly open
		close,							// the chest is completely closed
		inbetween						// the chest is being open or closed
	}
	
	public AudioClip openSound;			// sound to play when the chest is opened
	public AudioClip closeSound;		// the sound to play when the chest is being closed
	
//	public GameObject particleEffect;	// link to a particle effect for when the chest is open
	
	public GameObject[] parts;			// the parts of the chest that you want to apply the highlight to it has focus
	private Color[] _defaultColors;		// the default colors of the parts you are using for the highlight
	
	public State state;					// our current state
	
	public float maxDistance = 2;		// the max distance the player can be to open this chest
	
	private GameObject _player;
	private Transform _myTransform;
	private bool _used = false;			// track if the chest has been used or not
	
	public List<Item> loot = new List<Item>();
	public bool inUse = false;
	
	// Use this for initialization
	void Start () {
		_myTransform = transform;
		state = Chest.State.close;
		
		_defaultColors = new Color[parts.Length];
		if(parts.Length > 0)
			for(int cnt = 0; cnt < _defaultColors.Length; cnt++)
				_defaultColors[cnt] = parts[cnt].renderer.material.GetColor("_Color");
	}
	
	// Update is called once per frame
	void Update () {
		if(!inUse)
			return;
		
		if(_player == null)
			return;
		
		if(Vector3.Distance(transform.position, _player.transform.position) > maxDistance)
//			Messenger.Broadcast("CloseChest");
			MyGui.chest.ForceClose();
	}
	
	public void OnMouseEnter()
	{
		Debug.Log("Enter");
		HighLight(true);
	}
	
	public void OnMouseExit()
	{
		Debug.Log("Exit");
		HighLight(false);
	}
	
	public void OnMouseUp()
	{
		Debug.Log("Up");
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		
		if(go == null)
			return;
		
		if(Vector3.Distance(_myTransform.position, go.transform.position) > maxDistance && !inUse)
			return;
		
		switch(state)
		{
		case State.open:
			state = Chest.State.inbetween;
//			StartCoroutine("Close");
			ForceClose();
			break;
		case State.close:
			if(MyGui.chest != null) {
				MyGui.chest.ForceClose();
			}
			state = Chest.State.inbetween;
			StartCoroutine("Open");
			break;
		}
	}
	
	private IEnumerator Open()
	{
		// set this script to be the one that is holding the items
		MyGui.chest = this;
		// find the player so we can track his distance after opening the chest
		_player = GameObject.FindGameObjectWithTag("Player");
		// make this chest as being in use
		inUse = true;
		// play the open animation
		animation.Play("ChestOpen");
		// play the chest open sound effect
		audio.PlayOneShot(openSound);
		
		if(!_used)
			PopulateChest(5);
		
		// wait until the chest is done opening
		yield return new WaitForSeconds(animation["ChestOpen"].length);
		// change the chest state to open
		state = Chest.State.open;
		// send a message to the GUI to create 5 items and display them in the loot window
//		Messenger<int>.Broadcast("PopulateChest", 5, MessengerMode.DONT_REQUIRE_LISTENER);
		Messenger.Broadcast("DisplayLoot");
	}
	
	private IEnumerator Close()
	{
		_player = null;
		inUse = false;
		
		animation.Play("ChestClose");
		audio.PlayOneShot(closeSound);
		
		yield return new WaitForSeconds(animation["ChestClose"].length);
		state = Chest.State.close;
		
		if(loot.Count == 0)
			Destroy(gameObject);
	}
	
	public void ForceClose()
	{
		Messenger.Broadcast("CloseChest");
		StopCoroutine("Open");
		StartCoroutine("Close");
	}
	
	private void HighLight(bool glow)
	{
		if(glow) {
			if(parts.Length > 0)
				for(int cnt = 0; cnt < _defaultColors.Length; cnt++)
				parts[cnt].renderer.material.SetColor("_Color", Color.green);
		} else {
			if(parts.Length > 0)
				for(int cnt = 0; cnt < _defaultColors.Length; cnt++)
				parts[cnt].renderer.material.SetColor("_Color", _defaultColors[cnt]);
		}
	}
	
	private void PopulateChest(int x)
	{
		for(int cnt = 0; cnt < x; cnt++)
		{
			loot.Add(ItemGenerator.CreateItem());
//			loot[cnt].Name = "I:" + Random.Range(1,100);
		}
		_used = true;
	}
}
