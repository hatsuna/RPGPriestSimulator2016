using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataStructures : MonoBehaviour {

	public static List<Affliction> possibleAfflictions;
	public static List<ToolType> possibleToolTypes;
	public static List<Dialogue> possibleDialogueChoices;

	public GameObject candleObject;
	public GameObject initialReleaseObject;
	public GameObject magicObject;
	public GameObject crowbarObject;
	public GameObject potionObject;

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
		public GameObject prefab;
		public Dictionary<int, Affliction> treatmentDict;

			public ToolType(string _name, GameObject _prefab, Dictionary<int, Affliction> _treatmentDict) {
			this.name = _name;
			this. treatmentDict = _treatmentDict;
			this.prefab = _prefab;
			possibleToolTypes.Add(this);
		}
	}

	public class Dialogue {
		public string text;
		// 1= player 2= friend 3= victim
		public int spokenBy;
		// 1= bad 2= neutral 3= good
		public int goodness;
		public List<Affliction> relevantAfflictions;

		public Dialogue( string _text, int _spokenBy, int _goodness, List<Affliction> _relevantAfflictions){
			this.text = _text;
			this.spokenBy = _spokenBy;
			this.goodness = _goodness;
			this.relevantAfflictions = _relevantAfflictions;
			possibleDialogueChoices.Add(this);
		}
	}

	public class StartingDialogue
	{
		public string text;
		public int sympathy;
		public List<Affliction> relevantAfflictions;

		public StartingDialogue ( string _text, int _sympathy, List<Affliction> _relevantAfflications){
			this.text = _text;
			this.sympathy = _sympathy;
			this.relevantAfflictions = _relevantAfflications;
		}
	}

	// Returns random dialogue choice with selected goodness and affliction
	Dialogue getDialogue (int goodness, Affliction affliction ) {
		// build list of dialogue choice with selected goodness and relevant affliction
		List<Dialogue> dialogueChoices = new List<Dialogue>();
		for (int i = 0; i < possibleDialogueChoices.Count; i++) {
			if (
					possibleDialogueChoices [i].goodness == goodness 
					&& possibleDialogueChoices [i].relevantAfflictions.Contains (affliction)) {
				dialogueChoices.Add (possibleDialogueChoices[i]);
			};
		};
		int randomNum = Random.Range (0, dialogueChoices.Count);
		Dialogue dialogueChoice = dialogueChoices [randomNum];
		return dialogueChoice;
	}

	void Start() {
		// Afflictions
		possibleAfflictions = new List<Affliction> ();

		Affliction missingLimb = new Affliction ("Missing Limb", 3);
		Affliction frozen = new Affliction ("Frozen", 3);
		Affliction possessed = new Affliction ("Possessed", 4);
		Affliction parasite = new Affliction ("Parasite", 4);
		Affliction zombified = new Affliction ("Zombified", 4);

		/*Debug.Log ("Possible Afflictions: ");
		for (int i=0; i<possibleAfflictions.Count-1; i++){
			Debug.Log(possibleAfflictions[i].name);
		}*/

		// Tools
		possibleToolTypes = new List<ToolType> ();

		ToolType candle = new ToolType ("Candle", candleObject, new Dictionary<int, Affliction> () {
			{ 32, possessed },
			{ 21, frozen }
		});

		ToolType magic = new ToolType ("Magic", magicObject, new Dictionary<int, Affliction> () {
			{ 31, possessed },
			{ 53, zombified },
			{ 41, parasite }
		});

		ToolType potion = new ToolType ("Potion", potionObject, new Dictionary<int, Affliction> () {
			{ 12, missingLimb },
			{ 22, frozen },
			{ 43, parasite }
		});
			
		ToolType crowbar = new ToolType ("Crowbar", crowbarObject, new Dictionary<int, Affliction> () {
			{ 42, parasite }
		});

		ToolType initialRelease = new ToolType ("Initial Release", initialReleaseObject, new Dictionary<int, Affliction> () {
			{ 30, possessed },
			{ 20, frozen },
			{ 40, parasite },
			{ 10, missingLimb },
			{ 50, zombified }
		});
			
		/*Debug.Log ("Possible Tools: ");
		for (int i=0; i<possibleToolTypes.Count; i++){
			Debug.Log(possibleToolTypes[i].name);
		}*/

		Debug.Log (gameObject);
		gameObject.GetComponent<GameManager> ().GenerateTools ();


		//Dialogue
		possibleDialogueChoices = new List<Dialogue> ();

		//GOOD dialogue spoken by FRIEND during NONSPECIFIC treatment
		Dialogue goodJob = new Dialogue (
               "Wow! Good as new! Almost.",
               2,
               3,
               new List<Affliction> (){ possessed, frozen, parasite, missingLimb, zombified }
           );

		Dialogue scarMuch = new Dialogue(
				"Looks like it won't scar...much.",
				2,
				3,
				new List<Affliction> (){ possessed, frozen, parasite, missingLimb, zombified }
			);

		Dialogue miracleWorker = new Dialogue (
             "You're a miracle worker! A minor miracle worker! You did a good job.",
             2,
             3,
             new List<Affliction> (){ possessed, frozen, parasite, missingLimb, zombified }
         );

		Dialogue likeMagic = new Dialogue (
             "It's like magic! Well, I guess it IS magic.",
             2,
             3,
             new List<Affliction> (){ possessed, frozen, parasite, missingLimb, zombified }
         );

		Dialogue fiveStar = new Dialogue (
			                    "I'm definitely giving you 5 out of 5 stars.",
			                    2,
			                    3,
			                    new List<Affliction> (){ possessed, frozen, parasite, missingLimb, zombified }
		                    );


		/*	//GOOD dialogue spoken by FRIEND during treatment for MISSING LIMB
			Dialogue goodJob = new Dialogue{
			text = "You did a much better job than I did with my IKEA furniture.";
			goodness = 3;
			spokenBy = 2;
		}             

			Dialogue goodJob = new Dialogue{
			text = "Looks like I'll have to throw out all my limericks. 'I once knew a one-armed man from Nantucket...'";
			goodness = 3;
			spokenBy = 2;
		}          

			Dialogue goodJob = new Dialogue{
			text = "A good, firm handshake is so important for making the best first impression.";
			goodness = 3;
			spokenBy = 2;
		}       

			Dialogue goodJob = new Dialogue{
			text = "Looks like he'll be up and arm-wrestling in no time. That's very important to him, you know.";
			goodness = 3;
			spokenBy = 2;
		}       

			Dialogue goodJob = new Dialogue{
			text = "They make it look so hard on TV, but I guess it's just a 'tab A in slot B' kind of thing, huh?";
			goodness = 3;
			spokenBy = 2;
		}       

			//GOOD dialogue spoken by FRIEND during treatment for FROZEN
			Dialogue goodJob = new Dialogue{
			text = "Getting warmer!";
			goodness = 3;
			spokenBy = 2;
		}             

			//GOOD dialogue spoken by FRIEND during treatment for POSSESSED
			Dialogue goodJob = new Dialogue{
			text = "Bye bye, Beezlebub!";
			goodness = 3;
			spokenBy = 2;
		}   

			//GOOD dialogue spoken by FRIEND during treatment for PARASITE
			Dialogue goodJob = new Dialogue{
			text = "One step closer to ending an unhealthy co-dependent relationship.";
			goodness = 3;
			spokenBy = 2;
		}   

			//GOOD dialogue spoken by FRIEND during treatment for ZOMBIFIED
			Dialogue goodJob = new Dialogue{
			text = "Can't wait to get my friend back. He was really the brains of the operation.";
			goodness = 3;
			spokenBy = 2;
		}   

			//BAD dialogue spoken by FRIEND during NONSPECIFIC treatment
			Dialogue goodJob = new Dialogue{
			text = "Uh...are you sure that goes there?";
			goodness = 1;
			spokenBy = 2;
		} 

			Dialogue goodJob = new Dialogue{
			text = "I think you're making it worse.";
			goodness = 1;
			spokenBy = 2;
		} 

			Dialogue goodJob = new Dialogue{
			text = "Do you do refunds?";
			goodness = 1;
			spokenBy = 2;
		} 

			Dialogue goodJob = new Dialogue{
			text = "I should've known better than to take doctor recommendations from that guy at the inn.";
			goodness = 1;
			spokenBy = 2;
		} 

			Dialogue goodJob = new Dialogue{
			text = "I've got a bad feeling about this.";
			goodness = 1;
			spokenBy = 2;
		} 

			Dialogue goodJob = new Dialogue{
			text = "Maybe you should try using the Force.";
			goodness = 1;
			spokenBy = 2;
		} 

			Dialogue goodJob = new Dialogue{
			text = "Can I see your license again?";
			goodness = 1;
			spokenBy = 2;
		}

			//BAD dialogue spoken by FRIEND during treatment for MISSING LIMB 
			Dialogue goodJob = new Dialogue{
			text = "Be careful with that thing -- that's his good arm!";
			goodness = 1;
			spokenBy = 2;
		} 


			//BAD dialogue spoken by FRIEND during treatment for FROZEN
			Dialogue goodJob = new Dialogue{
			text = "Worst. Slushie. Ever.";
			goodness = 1;
			spokenBy = 2;
		} 

			//BAD dialogue spoken by FRIEND during treatment for POSSESSED
			Dialogue goodJob = new Dialogue{
			text = "I guess I'm stuck with this demon, huh...think he'll pay rent?";
			goodness = 1;
			spokenBy = 2;
		} 

			//BAD dialogue spoken by FRIEND during treatment for PARASITE
			Dialogue goodJob = new Dialogue{
			text = "That's doing literally nothing besides annoying it.";
			goodness = 1;
			spokenBy = 2;
		} 

			//BAD dialogue spoken by FRIEND during treatment for ZOMBIFIED
			Dialogue goodJob = new Dialogue{
			text = "He's undead. You're unqualified. It's a perfect match.";
			goodness = 1;
			spokenBy = 2;
		} 

			//GOOD dialogue spoken by PLAYER during NONSPECIFIC treatment
			Dialogue goodJob = new Dialogue{
			text = "Looks like all my training paid off. All 234,000 GP of it.";
			goodness = 3;
			spokenBy = 1;
		} 

			Dialogue goodJob = new Dialogue{
			text = "Don't worry, I've totally done this before.";
			goodness = 3;
			spokenBy = 1;
		} 

			Dialogue goodJob = new Dialogue{
			text = "It's dangerous to go alone. Take this Blue Shield Wellcare Plus plan...it's only 1,000 GP!";
			goodness = 3;
			spokenBy = 1;
		}         

			Dialogue goodJob = new Dialogue{
			text = "I'll take my payment in cold hard cash, thanks.";
			goodness = 3;
			spokenBy = 1;
		} 

			Dialogue goodJob = new Dialogue{
			text = "Look, Ma! No hands!";
			goodness = 3;
			spokenBy = 1;
		} 

			//GOOD dialogue spoken by PLAYER during treatment for MISSING LIMB
			Dialogue goodJob = new Dialogue{
			text = "Easier than one of those 1,000-piece puzzles.";
			goodness = 3;
			spokenBy = 1;
		} 

			Dialogue goodJob = new Dialogue{
			text = "It's alive! It's aliiiiiive!";
			goodness = 3;
			spokenBy = 1;
		} 

			Dialogue goodJob = new Dialogue{
			text = "We can rebuild him! We have the technology!";
			goodness = 3;
			spokenBy = 1;
		} 

			//GOOD dialogue spoken by PLAYER during treatment for FROZEN
			Dialogue goodJob = new Dialogue{
			text = "I'm on a hot streak.";
			goodness = 3;
			spokenBy = 1;
		} 

			//GOOD dialogue spoken by PLAYER during treatment for POSSESSED
			Dialogue goodJob = new Dialogue{
			text = "The power of me compels you!";
			goodness = 3;
			spokenBy = 1;
		} 

			Dialogue goodJob = new Dialogue{
			text = "Just call me the Repo Man.";
			goodness = 3;
			spokenBy = 1;
		}

			//GOOD dialogue spoken by PLAYER during treatment for PARASITE
			Dialogue goodJob = new Dialogue{
			text = "Just call me pest control.";
			goodness = 3;
			spokenBy = 1;
		} 

			//GOOD dialogue spoken by PLAYER during treatment for ZOMBIFIED
			Dialogue goodJob = new Dialogue{
			text = "I'm a genius! It must be because of my enormous, undigested brain.";
			goodness = 3;
			spokenBy = 1;
		} 

			//BAD dialogue spoken by PLAYER during NONSPECIFIC treatment
			Dialogue goodJob = new Dialogue{
			text = "(I hope I still get paid.)";
			goodness = 1;
			spokenBy = 1;
		} 

			Dialogue goodJob = new Dialogue{
			text = "(Maybe I should have waited until I leveled up.)";
			goodness = 1;
			spokenBy = 1;
		}   

			Dialogue goodJob = new Dialogue{
			text = "(I wonder if I left the stove on? Wait, what was I doing again?)";
			goodness = 1;
			spokenBy = 1;
		}   

			Dialogue goodJob = new Dialogue{
			text = "Sorry, I made a clerical error.";
			goodness = 1;
			spokenBy = 1;
		}   

			Dialogue goodJob = new Dialogue{
			text = "(I have so much crushing debt.)";
			goodness = 1;
			spokenBy = 1;
		}          

			//BAD dialogue spoken by PLAYER during treatment for MISSING LIMB
			Dialogue goodJob = new Dialogue{
			text = "Well...they didn’t need that anyway.";
			goodness = 1;
			spokenBy = 1;
		}         

			Dialogue goodJob = new Dialogue{
			text = "Wait, why's there this leftover piece?";
			goodness = 1;
			spokenBy = 1;
		}     

			Dialogue goodJob = new Dialogue{
			text = "I guess I'll send this to the Lost and Found.";
			goodness = 1;
			spokenBy = 1;
		}  

			//BAD dialogue spoken by PLAYER during treatment for FROZEN
			Dialogue goodJob = new Dialogue{
			text = "Let's just...let it go.";
			goodness = 1;
			spokenBy = 1;
		}  

			//BAD dialogue spoken by PLAYER during treatment for POSSESSED
			Dialogue goodJob = new Dialogue{
			text = "I mean, maybe the demon has a good personality?";
			goodness = 1;
			spokenBy = 1;
		}  

			//BAD dialogue spoken by PLAYER during treatment for PARASITE
			Dialogue goodJob = new Dialogue{
			text = "Where's Ripley when you need her?";
			goodness = 1;
			spokenBy = 1;
		}  

			//BAD dialogue spoken by PLAYER during treatment for ZOMBIFIED
			Dialogue goodJob = new Dialogue{
			text = "Maybe it's time for my secret ingredient: a headshot.";
			goodness = 1;
			spokenBy = 1;
		} */
	}
}
