using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TitleStartGame : MonoBehaviour {



	Animator anim;
	public Animation angleCameraAnim;
	private bool gameStarted;
	public GameManager gameManager;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		gameStarted = false;
	}

	// Update is called once per frame
	void Update () {
		Debug.Log (this.GetComponentInParent<Transform>());
		if (Input.anyKey && gameStarted == false) {
			gameStarted = true;
			anim.SetTrigger ("StartGame");
			//gameManager.SpawnVictim ();
		}
	}
}
