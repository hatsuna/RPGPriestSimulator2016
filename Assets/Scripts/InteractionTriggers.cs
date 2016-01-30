using UnityEngine;
using System.Collections;

public class InteractionTriggers : MonoBehaviour {

	GameManager gameManager;
	
	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	//COLLISIONS ARE BROKEN BECAUSE YOU HAVENT ADDED RIGIDBODIES YET DUMBASS
	
	
	//interactable objects lose their collider while the mouse is holding them
	//they regain their collider when the mouse lets go of them
	
	void OnTriggerEnter(Collider collider){
		Debug.Log(collider.name + " collided with " + name);
		gameManager.ProcessTriggers(gameObject, collider.gameObject);
	}
}