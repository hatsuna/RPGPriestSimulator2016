using UnityEngine;
using System.Collections;

public class TextTime : MonoBehaviour {
	public float timeLeft = 45;

	void Update()
	{
		timeLeft -= Time.deltaTime;
		//gameObject.text = timeLeft.ToString ();
		if ( timeLeft < 0 )
		{
		//	gameObject.text="Game Over!";
		}
	}
}
