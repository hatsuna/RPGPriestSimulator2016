using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataStructures : MonoBehaviour {

	public static List<Affliction> possibleAfflictions;
	public static List<ToolType> possibleToolTypes;

	public class Affliction {

		public int id;
		public string name;
		// If this state is ever hit, treatment is over
		public int endState;

		public Affliction (string _name, int _endState){
			this.name = _name;
			this.endState = _endState;
			possibleAfflictions.Add(this);
		}
	}

	public class ToolType {

		public int id;
		public string name;
		public Dictionary<int, Affliction> treatmentDict;

		public ToolType(string _name, Dictionary<int, Affliction> _treatmentDict) {
			this.name = _name;
			this.treatmentDict = _treatmentDict;
			possibleToolTypes.Add(this);
		}
	}

	void Start() {
		// Afflictions
		possibleAfflictions = new List<Affliction> ();

		Affliction missingLimb = new Affliction ("Missing Limb" , 3);
		Affliction frozen = new Affliction ("Frozen", 3);
		Affliction possessed = new Affliction ("Possessed", 4);
		Affliction parasite = new Affliction ("Parasite", 4);
		Affliction zombified = new Affliction ("Zombified", 4);

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
			{ 1, possessed },
			{ 3, zombified },
			{ 1, parasite }
		});

		ToolType potion = new ToolType ("Potion", new Dictionary<int, Affliction> () {
			{ 2, missingLimb },
			{ 2, frozen },
			{ 3, parasite }
		});
			
		ToolType crowbar = new ToolType ("Crowbar", new Dictionary<int, Affliction> () {
			{ 2, parasite }
		});

		ToolType initialRelease = new ToolType ("Initial Release", new Dictionary<int, Affliction> () {
			{ 0, possessed },
			{ 0, frozen },
			{ 0, parasite },
			{ 0, frozen },
			{ 0, missingLimb },
			{ 0, zombified }
		});
			

		Debug.Log ("Possible Afflictions: ");
		for (int i=0; i<possibleToolTypes.Count; i++){
			Debug.Log(possibleToolTypes[i].name);
		}
	}
}
