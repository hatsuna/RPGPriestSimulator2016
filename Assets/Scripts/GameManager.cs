using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public GameObject adventurer;
	Transform interactionPlane;
	public GameObject victimPrefab;
	public GameObject victimLocation;

	GameObject heldObject;

	public Text textUI;

	// Use this for initialization
	void Start () {
		// Instantiate adventurer prefab and move to table
		// This will automatically set up an affliction
		Instantiate(victimPrefab, victimLocation.transform.position, victimLocation.transform.rotation); 

		//make sure there is a valid adventurer on the altar
		if (adventurer.tag == "Adventurer"){
			Debug.Log("You have an adventurer on the altar");
			//if (adventurer.
			foreach (Transform child in adventurer.transform){
				if(child.tag == "InteractionPlane"){
					interactionPlane = child;
				}
			}
		}
			
	}

		// Update is called once per frame
	void Update () {
		//getting the currently held object from the MouseControl Script
		//heldObject = GetComponent<MouseControl>().heldObject;
	}

	// 
	public void ProcessTriggers(GameObject trigger, GameObject collider){
		//interactable trigger sites call this when they collide with a rigidbody
		//check if the two match up to create an interaction

		if(trigger.tag == "InteractionPlane" && collider.tag == "Interactable"){
			
			//DataStructures

			//DataStructures.Affliction VictimAffliction = trigger.GetComponent<Victim>().affliction;

			//check database
			//check affliction
			//check tool
			//do they match?
		}
	}

}
