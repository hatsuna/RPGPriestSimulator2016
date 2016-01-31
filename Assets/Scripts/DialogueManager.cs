using UnityEngine;
using System.Collections;

public class DialogueManager : MonoBehaviour {

	Animator anim;
	public GameObject spriteHolder;

	// Use this for initialization
	void Awake () {
		anim = GetComponent<Animator> ();
	}

	public void UpdateDialogueLeft() {
		anim.SetTrigger("StartDialogueLeft");
	}

	public void UpdateDialogueRight() {
		anim.SetTrigger("StartDialogueRight");
	}
		
	/*// Update is called once per frame
	void Update () {
		if(Input.GetButton("Fire1")) 
		{
			anim.SetTrigger("StartDialogueLeft");
		}

		if(Input.GetMouseButton(2)) 
		{
			anim.SetTrigger("StartDialogueRight");
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			spriteHolder.GetComponent<ChangeImage>().LoadSprite();
			Debug.LogError ("spacebar", this);
		}
	}*/
}
