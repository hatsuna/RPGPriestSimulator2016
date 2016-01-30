using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Victim : MonoBehaviour {

	public DataStructures.Affliction affliction;

	// Use this for initialization
	void Start () {
		// Apply a random condition to victim
		this.affliction = generateAfflictions();
		Debug.Log("I'm afflicted with: " + affliction.name);

		// Move to the VictimPosition marker 
		GetComponent<Transform>().position = transform.parent.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public DataStructures.Affliction generateAfflictions () {
		List<DataStructures.Affliction> possibleAfflictions = DataStructures.possibleAfflictions;
		int afflictionIndex = Random.Range (0, possibleAfflictions.Count - 1);
		Debug.Log ("My affliction index is: " + afflictionIndex);
		return possibleAfflictions [afflictionIndex];
	} 
}
