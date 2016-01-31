using UnityEngine;
using System.Collections;

public class DialogueManager : MonoBehaviour {

	Animator anim;
	public GameObject spriteHolder;
	private bool gameStarted;
	public GameManager gameManager;
	public GameObject ourCamera;
	bool hasStarted;
	//bool docOpen;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		gameStarted = false;
		//docOpen = false;
	}

	public void UpdateDialogueLeft() {
		anim.SetTrigger("StartDialogueLeft");
	}

	public void UpdateDialogueRight() {
		anim.SetTrigger("StartDialogueRight");
	}

	public void UpdateDocument() {
		spriteHolder.GetComponent<ChangeSprite> ().LoadSprite ();
		anim.SetTrigger ("LoadDocument");
	}
		
	public void StartGameText() {
		anim.SetTrigger ("StartGame");
	}

	// Update is called once per frame
	void Update () {
		if (Input.anyKey && gameStarted == false) {
			gameStarted = true;
		}
		if (ourCamera.transform.eulerAngles.x == 40 && !hasStarted) {
			//anim.SetTrigger ("StartGameText");
			gameManager.SpawnVictim ();
			hasStarted = true;
		}
		/*if(Input.GetButton("Fire1")) 
		{
			anim.SetTrigger("StartDialogueLeft");
		}

		if(Input.GetMouseButton(2)) 
		{
			anim.SetTrigger("StartDialogueRight");
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			spriteHolder.GetComponent<ChangeSprite> ().LoadSprite ();
			anim.SetTrigger ("LoadDocument");
		}*/

		/*if (Input.GetKeyDown (KeyCode.Space)) {
			if (docOpen = false) {
				spriteHolder.GetComponent<ChangeSprite> ().LoadSprite ();
				anim.SetTrigger ("LoadDocument");
				//Debug.LogError ("spacebar", this);
			}
			else {
				anim.SetTrigger ("CloseDocument");
				docOpen = false;
				//Debug.LogError ("spacebar", this);
			}

			docOpen = !docOpen;
		}*/
	}
}
