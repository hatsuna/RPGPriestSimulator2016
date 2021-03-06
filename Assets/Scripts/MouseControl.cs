﻿using UnityEngine;
using System.Collections;

public class MouseControl : MonoBehaviour {

	public GameObject heldObject;

	bool holdingObject = false;

	// Use this for initialization
	void Start () {

		////Set Cursor to be visible
		Cursor.visible = true;
	
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetMouseButton(1)){
			float mouseY = -Input.GetAxis("Mouse Y");
			Camera.main.transform.Rotate(mouseY, 0f, 0f);
		}
		else{
			Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
			// generate a "RaycastHit" to remember where the raycast hit
			RaycastHit rayHit = new RaycastHit();

			if ( Physics.Raycast ( ray, out rayHit, 1000f ) ) {
				//Debug.Log ( "you are putting your cursor over " + rayHit.collider.name);
				Debug.DrawRay ( ray.origin, ray.direction * rayHit.distance, Color.yellow );

				//Player Clicks on interactable object
				if (Input.GetMouseButtonDown(0) && rayHit.collider.gameObject.tag == "Interactable"){
					//Debug.Log("MouseClick on Interactable");
					holdingObject = true;
					heldObject = rayHit.collider.gameObject;
					heldObject.GetComponent<Collider>().enabled = false;
				}

				//Dragging Interactable Object
				if (Input.GetMouseButton(0) && holdingObject){
					//Debug.Log("Dragging on Interactable");
					heldObject.transform.position = rayHit.point; // move cube to where it hit
					//cube.up = rayHit.normal; // aligns the cube with the sphere
				}

				//Let Go of interactable Object
				if (!Input.GetMouseButton(0) && holdingObject){
					//Debug.Log("Let go of Interactable");
					holdingObject = false;
					heldObject.GetComponent<Collider>().enabled = true;
					heldObject = null;
				}

			}
		}
	}
}
