using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tool : MonoBehaviour {

	public DataStructures.ToolType tooltype;
	int id;
	//public affliction[] relevantAfflictions = new affliction[]();

	// Use this for initialization
	void Start () {
		id = tooltype.id;
		name = tooltype.name;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
