using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeSprite : MonoBehaviour {

	public Image myImageComponent;

	// Use this for initialization
	void Start () {
		myImageComponent = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadSprite() {
		gameObject.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Documents/InsuranceCard0");
	}

}
