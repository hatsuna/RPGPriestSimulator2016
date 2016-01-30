using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Victim : MonoBehaviour {

	public DataStructures.Affliction affliction;
	public int treatmentState;
	public Sprite testSprite;


	// Use this for initialization
	void Start () {
		// Apply sprite
		gameObject.GetComponent<SpriteRenderer>().sprite=testSprite;

		// Apply a random condition to victim
		this.affliction = GenerateAfflictions();
		ApplyAffliction (affliction);
		//Debug.Log("I'm afflicted with: " + affliction.name);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public DataStructures.Affliction GenerateAfflictions () {
		List<DataStructures.Affliction> possibleAfflictions = DataStructures.possibleAfflictions;
		int afflictionIndex = Random.Range (0, possibleAfflictions.Count - 1);
		Debug.Log ("My affliction index is: " + afflictionIndex);
		return possibleAfflictions [afflictionIndex];
	} 

	public void ApplyAffliction (DataStructures.Affliction affliction) {
		// Name matching is ugly, consider switching to id
		if (affliction.name == "Frozen") {
			gameObject.GetComponent<Renderer>().material.color = Color.blue;
		} else if (affliction.name == "Possessed") {
			gameObject.GetComponent<Renderer>().material.color = Color.magenta;
		} else if (affliction.name == "Parasite") {
			gameObject.GetComponent<Renderer>().material.color = Color.yellow;
		} else if (affliction.name == "Zombified") {
			gameObject.GetComponent<Renderer>().material.color = Color.green;
		} else {
		}
	}
}
