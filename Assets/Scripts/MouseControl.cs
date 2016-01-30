using UnityEngine;
using System.Collections;

public class MouseControl : MonoBehaviour {

	public Transform cube;

	bool bodyPartSelected = false;

	// Use this for initialization
	void Start () {

		////Set Cursor to be visible
		Cursor.visible = true;
	
	}
	
	// Update is called once per frame
	void Update () {
	
		Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
		// generate a "RaycastHit" to remember where the raycast hit
		RaycastHit rayHit = new RaycastHit();

		if ( Physics.Raycast ( ray, out rayHit, 1000f ) ) {
			Debug.Log ( "you are putting your cursor over a collider!");
			Debug.DrawRay ( ray.origin, ray.direction * rayHit.distance, Color.yellow );

			//if (bodyPartSelected){
				cube.position = rayHit.point; // move cube to where it hit
				//cube.up = rayHit.normal; // aligns the cube with the sphere
			//}
		}

	}
}
