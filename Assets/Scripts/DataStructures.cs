using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataStructures : MonoBehaviour {

	public static List<Affliction> possibleAfflictions;
	public static List<ToolType> possibleToolTypes;

	public class Affliction {

		public int id;
		public string name;

		public Affliction (string _name){
			this.name = _name;
			possibleAfflictions.Add(this);
		}
	}

	public class ToolType {

		public int id;
		public string name;
		public Dictionary<int, Affliction> treatmentDict;

			public ToolType(string _name, Dictionary<int, Affliction> _treatmentDict) {
			this.name = _name;
			this. treatmentDict = _treatmentDict;
			possibleToolTypes.Add(this);
		}
	}

	void Start() {
		// Afflictions
		possibleAfflictions = new List<Affliction> ();

		Affliction missingLimb = new Affliction ("Missing Limb");
		Affliction frozen = new Affliction ("Frozen");
		Affliction possessed = new Affliction ("Possessed");
		Affliction parasite = new Affliction ("Parasite");
		Affliction zombified = new Affliction ("Zombified");

		//Debug.Log ("Possible Afflictions: ");
		//for (int i=0; i<possibleAfflictions.Count-1; i++){
		//	Debug.Log(possibleAfflictions[i].name);
		//}

		// Tools
		possibleToolTypes = new List<ToolType> ();

		ToolType candle = new ToolType ("Candle", new Dictionary<int, Affliction>(){
			{2, possessed},
			{1, frozen}
		});

		ToolType magic = new ToolType ("Magic", new Dictionary<int, Affliction> () {
			{ 1, possessed }
		});

		Debug.Log ("Possible Afflictions: ");
		for (int i=0; i<possibleToolTypes.Count; i++){
			Debug.Log(possibleToolTypes[i].name);
		}
	}
}
