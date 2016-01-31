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
	public GameObject holySymbolObject;

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
	public Dialogue getDialogue (int goodness, Affliction affliction ) {
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

		ToolType holySymbol = new ToolType ("Holy Symbol", holySymbolObject, new Dictionary<int, Affliction> () {
			{23, possessed},
			{51, possessed}
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


		// dialogue
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

		//GOOD dialogue spoken by FRIEND during treatment for MISSING LIMB
		Dialogue ikeaFurniture = new Dialogue (
			"You did a much better job than I did with my IKEA furniture.",
			2,
			3,
			new List<Affliction> (){ missingLimb }
		);

		Dialogue noLimericks = new Dialogue (
			"Looks like I'll have to throw out all my limericks. 'I once knew a one-armed man from Nantucket...'",
			2,
			3,
			new List<Affliction> (){ missingLimb }
		);

		Dialogue bestHandshake = new Dialogue (
			"A good, firm handshake is so important for making the best first impression.",
			2,
			3,
			new List<Affliction> (){ missingLimb }
		);        

		Dialogue armWrestling = new Dialogue (
			"Looks like he'll be up and arm-wrestling in no time. That's very important to him, you know.",
			2,
			3,
			new List<Affliction> (){ missingLimb }
		);  

		Dialogue notTelevision = new Dialogue (
			"They make it look so hard on TV, but I guess it's just a 'tab A in slot B' kind of thing, huh?",
			2,
			3,
			new List<Affliction> (){ missingLimb }
		); 

		//GOOD dialogue spoken by FRIEND during treatment for FROZEN

		Dialogue getWarmer = new Dialogue (
			"Getting warmer!",
			2,
			3,
			new List<Affliction> (){ frozen }
		); 

		//GOOD dialogue spoken by FRIEND during treatment for POSSESSED

		Dialogue byeBub = new Dialogue (
			"Bye bye, Beezlebub!",
			2,
			3,
			new List<Affliction> (){ possessed }
		);         

		//GOOD dialogue spoken by FRIEND during treatment for PARASITE

		Dialogue breakUp = new Dialogue (
			"One step closer to ending an unhealthy co-dependent relationship.",
			2,
			3,
			new List<Affliction> (){ parasite }
		);  

		//GOOD dialogue spoken by FRIEND during treatment for ZOMBIFIED

		Dialogue brainsBrawn = new Dialogue (
			"Can't wait to get my friend back. He was really the brains of the operation.",
			2,
			3,
			new List<Affliction> (){ zombified }
		); 

		/*GOOD dialogue spoken by FRIEND during treatment for POISON (need to add POISON to list of nonspecific treatment dialogue)
        
        Dialogue antiDope = new Dialogue (
             "That antidote is dope!",
             2,
             3,
             new List<Affliction> (){ poison }
         ); */

		//BAD dialogue spoken by FRIEND during NONSPECIFIC treatment

		Dialogue notSure = new Dialogue (
			"Uh...are you sure that goes there?",
			2,
			1,
			new List<Affliction> (){ possessed, frozen, parasite, missingLimb, zombified }
		);

		Dialogue makeWorse = new Dialogue (
			"I think you're making it worse.",
			2,
			1,
			new List<Affliction> (){ possessed, frozen, parasite, missingLimb, zombified }
		);        

		Dialogue doRefunds = new Dialogue (
			"Do you do refunds?",
			2,
			1,
			new List<Affliction> (){ possessed, frozen, parasite, missingLimb, zombified }
		);  

		Dialogue innRec = new Dialogue (
			"I should've known better than to take doctor recommendations from that guy at the inn.",
			2,
			1,
			new List<Affliction> (){ possessed, frozen, parasite, missingLimb, zombified }
		); 

		Dialogue badFeeling = new Dialogue (
			"I've got a bad feeling about this.",
			2,
			1,
			new List<Affliction> (){ possessed, frozen, parasite, missingLimb, zombified }
		);  

		Dialogue useForce = new Dialogue (
			"Maybe you should try using the Force.",
			2,
			1,
			new List<Affliction> (){ possessed, frozen, parasite, missingLimb, zombified }
		); 

		Dialogue seeLicense = new Dialogue (
			"Can I see your license again?",
			2,
			1,
			new List<Affliction> (){ possessed, frozen, parasite, missingLimb, zombified }
		); 

		/*BAD dialogue spoken by FRIEND during treatment for POISON (need to add POISON to list of nonspecific treatment dialogue)
        
        Dialogue imSick = new Dialogue (
             "Even I'M starting to feel sick while watching you work.",
             2,
             1,
             new List<Affliction> (){ poison }
         ); */

		//BAD dialogue spoken by FRIEND during treatment for MISSING LIMB
		Dialogue goodArm = new Dialogue (
			"Be careful with that thing -- that's his good arm!",
			2,
			1,
			new List<Affliction> (){ missingLimb }
		); 
		//BAD dialogue spoken by FRIEND during treatment for FROZEN
		Dialogue worstSlushie = new Dialogue (
			"Worst. Slushie. Ever.",
			2,
			1,
			new List<Affliction> (){ frozen }
		); 

		//BAD dialogue spoken by FRIEND during treatment for POSSESSED
		Dialogue newRoomie = new Dialogue (
			"I guess I'm stuck with this demon, huh...think he'll pay rent?",
			2,
			1,
			new List<Affliction> (){ possessed }
		); 

		//BAD dialogue spoken by FRIEND during treatment for PARASITE
		Dialogue buggingBug = new Dialogue (
			"That's doing literally nothing besides annoying it.",
			2,
			1,
			new List<Affliction> (){ parasite }
		); 

		//BAD dialogue spoken by FRIEND during treatment for ZOMBIFIED
		Dialogue unMatch = new Dialogue (
			"He's undead. You're unqualified. It's a perfect match.",
			2,
			1,
			new List<Affliction> (){ zombified }
		); 


		//GOOD dialogue spoken by PLAYER during NONSPECIFIC treatment
		Dialogue goodTraining = new Dialogue (
			"Looks like all my training paid off. All 234,000 GP of it.",
			1,
			3,
			new List<Affliction> (){ possessed, frozen, parasite, missingLimb, zombified }
		);

		Dialogue notNoob = new Dialogue (
			"Don't worry, I've totally done this before.",
			1,
			3,
			new List<Affliction> (){ possessed, frozen, parasite, missingLimb, zombified }
		);

		Dialogue blueShield = new Dialogue (
			"It's dangerous to go alone. Take this Blue Shield Wellcare Plus plan...it's only 1,000 GP!",
			1,
			3,
			new List<Affliction> (){ possessed, frozen, parasite, missingLimb, zombified }
		);

		Dialogue cashThanks = new Dialogue (
			"I'll take my payment in cold hard cash, thanks.",
			1,
			3,
			new List<Affliction> (){ possessed, frozen, parasite, missingLimb, zombified }
		);

		Dialogue lookMa = new Dialogue (
			"Look, Ma! No hands!",
			1,
			3,
			new List<Affliction> (){ possessed, frozen, parasite, missingLimb, zombified }
		);

		/*GOOD dialogue spoken by PLAYER during treatment for POISON (need to add POISON to list of nonspecific treatment dialogue)
        
        Dialogue awesomeSnake = new Dialogue (
             "I'm so awesome, even snakes are afraid of me.",
             1,
             3,
             new List<Affliction> (){ poison }
         ); */

		//GOOD dialogue spoken by PLAYER during treatment for MISSING LIMB

		Dialogue easyPuzzle = new Dialogue (
			"Easier than one of those 1,000-piece puzzles.",
			1,
			3,
			new List<Affliction> (){ missingLimb }
		);

		Dialogue itsAlive = new Dialogue (
			"It's alive! It's aliiiiiive!",
			1,
			3,
			new List<Affliction> (){ missingLimb }
		);

		Dialogue haveTechnology = new Dialogue (
			"We can rebuild him! We have the technology!",
			1,
			3,
			new List<Affliction> (){ missingLimb }
		);

		//GOOD dialogue spoken by PLAYER during treatment of FROZEN
		Dialogue hotStreak = new Dialogue (
			"I'm on a hot streak.",
			1,
			3,
			new List<Affliction> (){ frozen }
		);

		//GOOD dialogue spoken by PLAYER during treatment of POSSESSED
		Dialogue powerCompel = new Dialogue (
			"The power of me compels you!",
			1,
			3,
			new List<Affliction> (){ possessed }
		);

		Dialogue repoMan = new Dialogue (
			"Just call me the Repo Man.",
			1,
			3,
			new List<Affliction> (){ possessed }
		);

		//GOOD dialogue spoken by PLAYER during treatment of PARASITE
		Dialogue pestControl = new Dialogue (
			"Just call me pest control.",
			1,
			3,
			new List<Affliction> (){ parasite }
		);

		//GOOD dialogue spoken by PLAYER during treatment of ZOMBIFIED       
		Dialogue geniusBrain = new Dialogue (
			"I'm a genius! It must be because of my enormous, undigested brain.",
			1,
			3,
			new List<Affliction> (){ zombified }
		);

		//BAD dialogue spoken by PLAYER during NONSPECIFIC treatment
		Dialogue stillPaid = new Dialogue (
			"(I hope I still get paid.)",
			1,
			1,
			new List<Affliction> (){ possessed, frozen, parasite, missingLimb, zombified }
		);

		Dialogue waitLevel = new Dialogue (
			"(Maybe I should have waited until I leveled up.)",
			1,
			1,
			new List<Affliction> (){ possessed, frozen, parasite, missingLimb, zombified }
		);

		Dialogue stoveOn = new Dialogue (
			"(I wonder if I left the stove on? Wait, what was I doing again?)",
			1,
			1,
			new List<Affliction> (){ possessed, frozen, parasite, missingLimb, zombified }
		);

		Dialogue clericalError = new Dialogue (
			"Sorry, I made a clerical error.",
			1,
			1,
			new List<Affliction> (){ possessed, frozen, parasite, missingLimb, zombified }
		);

		Dialogue suchDebt = new Dialogue (
			"(I have so much crushing debt.)",
			1,
			1,
			new List<Affliction> (){ possessed, frozen, parasite, missingLimb, zombified }
		);

		/*BAD dialogue spoken by PLAYER during treatment for POISON (need to add POISON to list of nonspecific treatment dialogue)
        
        Dialogue oopsAspirin = new Dialogue (
             "Wait, I think this was just aspirin.",
             1,
             1,
             new List<Affliction> (){ poison }
         ); */

		//BAD dialogue spoken by PLAYER during treatment for MISSING LIMB

		Dialogue noNeed = new Dialogue (
			"Well...they didn’t need that anyway.",
			1,
			1,
			new List<Affliction> (){ missingLimb }
		);

		Dialogue whyLeftover = new Dialogue (
			"Wait, why's there this leftover piece?",
			1,
			1,
			new List<Affliction> (){ missingLimb }
		);

		Dialogue lostFound = new Dialogue (
			"I guess I'll send this to the Lost and Found.",
			1,
			1,
			new List<Affliction> (){ missingLimb }
		);

		//BAD dialogue spoken by PLAYER during treatment for FROZEN            
		Dialogue letGo = new Dialogue (
			"Let's just...let it go.",
			1,
			1,
			new List<Affliction> (){ frozen }
		);   

		//BAD dialogue spoken by PLAYER during treatment for POSSESSED            
		Dialogue goodPersonality = new Dialogue (
			"I mean, maybe the demon has a good personality?",
			1,
			1,
			new List<Affliction> (){ possessed }
		);    

		//BAD dialogue spoken by PLAYER during treatment for PARASITE            
		Dialogue whereRipley = new Dialogue (
			"Where's Ripley when you need her?",
			1,
			1,
			new List<Affliction> (){ parasite }
		);                             

		//BAD dialogue spoken by PLAYER during treatment for ZOMBIFIED            
		Dialogue headshotTime = new Dialogue (
			"Maybe it's time for my secret ingredient: a headshot.",
			1,
			1,
			new List<Affliction> (){ zombified }
		);    

		//INITIAL DIALOGUE BELOW
		//UNSYMPATHETIC starting dialogue for MISSING LIMB
		StartingDialogue testTest = new StartingDialogue (
			                            "OMG, we ran into this totally rad dragon. Well, it was rad until it took a bite out of my friend. Oh, it ate some villagers too but who cares?",
			                            1,
			                            new List<Affliction> (){ missingLimb }
		                            );


	}
}
