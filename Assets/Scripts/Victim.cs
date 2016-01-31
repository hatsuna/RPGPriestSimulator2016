﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Victim : MonoBehaviour {

	public DataStructures.Affliction affliction;
	public int treatmentState;
	public Sprite testSprite;

	public GameObject parasitePrefab;


	// Use this for initialization
	void Start () {
		// Apply sprite
		gameObject.GetComponent<SpriteRenderer>().sprite=testSprite;

		// Going to move this bit to game manager for a second, may put back
		// Apply a random condition to victim
		//this.affliction = GenerateAfflictions();
		//ApplyAffliction (affliction);
		//Debug.Log("I'm afflicted with: " + affliction.name);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
	public void ApplyAffliction (DataStructures.Affliction affliction) {
		// Name matching is ugly, consider switching to id
		if (affliction.name == "Frozen") {
			gameObject.GetComponent<Renderer>().material.color = Color.blue;
		} else if (affliction.name == "Possessed") {
			gameObject.GetComponent<Renderer>().material.color = Color.magenta;
		} else if (affliction.name == "Parasite") {
			gameObject.GetComponent<Renderer>().material.color = Color.yellow;
			GameObject newParasite = (GameObject)(Instantiate (parasitePrefab, new Vector3(gameObject.transform.position.x + 0.68f, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation));
		} else if (affliction.name == "Zombified") {
			gameObject.GetComponent<Renderer>().material.color = Color.green;
		} else {
		}
	}
}
