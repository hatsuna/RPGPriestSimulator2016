using UnityEngine;
using System.Collections;

public class TitleCanvasScript : MonoBehaviour {
	
	Animator anim;
	private bool gameStarted;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		gameStarted = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKey && gameStarted == false) {
			gameStarted = true;
			anim.SetTrigger ("StartGameText");
		}
	}
}
