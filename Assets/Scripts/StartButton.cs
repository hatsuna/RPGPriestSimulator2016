using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartButton : MonoBehaviour {

	public int index;
	private Button myselfButton;

	// Use this for initialization
	void Start () {
		myselfButton = GetComponent<Button>();
		myselfButton.onClick.AddListener(() => actionToMaterial(index));
	}

	void actionToMaterial(int idx) {
			Debug.Log("change material to HIT  on material :  " + idx);
	}

	
	void Destroy()	{
		myselfButton.onClick.RemoveListener(() => actionToMaterial(index));
	}

// Update is called once per frame
	void Update () {
	}
}
