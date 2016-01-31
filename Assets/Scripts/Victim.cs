using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Victim : MonoBehaviour {

	public DataStructures.Affliction affliction;
	public int treatmentState;
	public Sprite testSprite;

	public GameObject frozenPrefab;
	public GameObject possessedPrefab;
	public GameObject parasitePrefab;
	public GameObject poisonPrefab;
	public GameObject zombiePrefab;


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

		this.affliction = affliction;

		if (affliction.name == "Frozen") {
			gameObject.GetComponent<Renderer> ().material.color = Color.blue;
			GameObject newFrozen = (GameObject)(Instantiate (frozenPrefab, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation));
			newFrozen.transform.parent = gameObject.transform;
		} else if (affliction.name == "Possessed") {
			gameObject.GetComponent<Renderer> ().material.color = Color.magenta;
			GameObject newPossessed = (GameObject)(Instantiate (possessedPrefab, new Vector3 (gameObject.transform.position.x + 0.68f, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation));
			newPossessed.transform.parent = gameObject.transform;
		} else if (affliction.name == "Parasite") {
			gameObject.GetComponent<Renderer> ().material.color = Color.yellow;
			GameObject newParasite = (GameObject)(Instantiate (parasitePrefab, new Vector3 (gameObject.transform.position.x + 0.68f, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation));
			newParasite.transform.parent = gameObject.transform;
		} else if (affliction.name == "Zombified") {
			gameObject.GetComponent<Renderer> ().material.color = Color.grey;
			GameObject newZombie = (GameObject)(Instantiate (zombiePrefab, new Vector3 (gameObject.transform.position.x + 0.68f, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation));
			newZombie.transform.parent = gameObject.transform;
		} else if (affliction.name == "Poison") {
			gameObject.GetComponent<Renderer> ().material.color = Color.green;
			GameObject newPoison = (GameObject)(Instantiate (poisonPrefab, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 1.0f), gameObject.transform.rotation));
			newPoison.GetComponent<Renderer>().sortingLayerName = "Particles";
			newPoison.transform.parent = gameObject.transform;
		} else {
			gameObject.GetComponent<Renderer> ().material.color = Color.red;
		}
	}
}
