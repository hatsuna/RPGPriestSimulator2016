using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataStructures : MonoBehaviour {

	public static List<Affliction> possibleAfflictions;

	void Start(){
		possibleAfflictions = new List<Affliction>();

		Affliction missingLimb = new Affliction("Missing Limb");
		Affliction Frozen = new Affliction("Frozen");
		Affliction Posessed = new Affliction("Possessed");
		Affliction Parasite = new Affliction("Parasite");
		Affliction zombified = new Affliction("Zombified");

		Debug.Log ("Possible Afflictions: ");
		for (int i=0; i<possibleAfflictions.Count-1; i++){
			Debug.Log(possibleAfflictions[i].name);
		}
	}

	public class Affliction {

		public int id;
		public string name;

		public Affliction (string _name){
			this.name = _name;
			possibleAfflictions.Add(this);
		}
	}
}
