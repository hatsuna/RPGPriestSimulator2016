using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject adventurer;
	Transform interactionPlane;

	GameObject heldObject;

	// Use this for initialization
	void Start () {
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
		heldObject = GetComponent<MouseControl>().heldObject;
		//if (heldObject  INTERACTS 
	}
}
