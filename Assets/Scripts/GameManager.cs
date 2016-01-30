using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public GameObject adventurer;
	Transform interactionPlane;
	public GameObject victimPrefab;
	public GameObject victimLocation;

	public GameObject toolPrefab;
	public Transform toolLocation;

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

	public void GenerateTools(){
		List<DataStructures.ToolType> possibleToolTypes = DataStructures.possibleToolTypes;
		foreach( DataStructures.ToolType tool in possibleToolTypes){
			Instantiate(toolPrefab, toolLocation.position, toolLocation.rotation);
		}

	}

	public void ProcessTriggers(GameObject trigger, GameObject collider){
		//interactable trigger sites call this when they collide with a rigidbody
		//check if the two match up to create an interaction

		if(trigger.tag == "InteractionPlane" && collider.tag == "Interactable"){

			//check database
			//check affliction
			//check tool
			//do they match?

			DataStructures.ToolType tool = collider.GetComponent<Tool>().tooltype;
			DataStructures.Affliction affliction = trigger.GetComponent<Victim>().affliction;
			int afflictionState = trigger.GetComponent<Victim>().treatmentState;
			if(tool.treatmentDict[afflictionState] == affliction){
				Debug.Log("these match");

			}

		}
	}

}
