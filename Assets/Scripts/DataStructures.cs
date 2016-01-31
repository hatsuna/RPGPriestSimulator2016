using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataStructures : MonoBehaviour {

	public static List<Affliction> possibleAfflictions;
	public static List<ToolType> possibleToolTypes;
	public static List<Dialogue> possibleDialogueChoices;
	public static List<StartingDialogue> possibleStartingDialogueChoices;

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
			possibleStartingDialogueChoices.Add(this);
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

	public static StartingDialogue getStarting (Affliction affliction) {
		List<StartingDialogue> possibleStarting = new List<StartingDialogue> ();
		for (int i = 0; i < possibleStartingDialogueChoices.Count; i++) {
			for (int j = 0; j < possibleStartingDialogueChoices [i].relevantAfflictions.Count; j++) {
				if (possibleStartingDialogueChoices [i].relevantAfflictions [j] == affliction) {
					possibleStarting.Add (possibleStartingDialogueChoices [i]);
				}
			}
		}
		int randomNum = Random.Range (0, possibleStarting.Count);
		StartingDialogue startingDialogueChoice = possibleStarting [randomNum];
		return startingDialogueChoice;
	}

	void Start() {
		// Afflictions
		possibleAfflictions = new List<Affliction> ();

		Affliction poison = new Affliction ("Poison", 3);
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
			{ 31, possessed },
			{ 21, frozen },
			{ 51, zombified }
		});

		ToolType magic = new ToolType ("Magic", magicObject, new Dictionary<int, Affliction> () {
			{ 12, poison },
			{ 33, possessed },
			{ 41, parasite }
		});

		ToolType potion = new ToolType ("Potion", potionObject, new Dictionary<int, Affliction> () {
			{ 11, poison },
			{ 22, frozen },
			{ 43, parasite }
		});
			
		ToolType crowbar = new ToolType ("Crowbar", crowbarObject, new Dictionary<int, Affliction> () {
			{ 42, parasite },
			{ 52, zombified }
		});

		ToolType holySymbol = new ToolType ("Holy Symbol", holySymbolObject, new Dictionary<int, Affliction> () {
			{32, possessed},
			{53, zombified}
		});
			

		ToolType initialRelease = new ToolType ("Initial Release", initialReleaseObject, new Dictionary<int, Affliction> () {
			{ 30, possessed },
			{ 20, frozen },
			{ 40, parasite },
			{ 10, poison },
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
		possibleStartingDialogueChoices = new List<StartingDialogue> ();

		//UPDATE DIALOGUE BELOW HERE

				//GOOD dialogue spoken by FRIEND during NONSPECIFIC treatment
		Dialogue goodJob = new Dialogue (
			"Wow! Good as new! Almost.",
			2,
			3,
			new List<Affliction> (){ possessed, frozen, parasite, poison, zombified }
		);

		Dialogue scarMuch = new Dialogue(
			"Looks like it won't scar...much.",
			2,
			3,
			new List<Affliction> (){ possessed, frozen, parasite, poison, zombified }
		);

		Dialogue miracleWorker = new Dialogue (
			"You're a miracle worker! A minor miracle worker! You did a good job.",
			2,
			3,
			new List<Affliction> (){ possessed, frozen, parasite, poison, zombified }
		);

		Dialogue likeMagic = new Dialogue (
			"It's like magic! Well, I guess it IS magic.",
			2,
			3,
			new List<Affliction> (){ possessed, frozen, parasite, poison, zombified }
		);

		Dialogue fiveStar = new Dialogue (
			"I'm definitely giving you 5 out of 5 stars.",
			2,
			3,
			new List<Affliction> (){ possessed, frozen, parasite, poison, zombified }
		);

		//GOOD dialogue spoken by FRIEND during treatment for FROZEN

		Dialogue getWarmer = new Dialogue (
			"Getting warmer!",
			2,
			3,
			new List<Affliction> (){ frozen }
		); 

		Dialogue mmToasty = new Dialogue (
			"Mmm...toasty!",
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

		Dialogue totallyThanks = new Dialogue (
			"My friend would totally thank you right now. If he didn't have his uh condition, of course.",
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

		Dialogue hoseOff = new Dialogue (
			"This is way better than the last thing I tried, which was just to hose him off.",
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

		Dialogue easyGreen = new Dialogue (
			"Almost there! Good thing, too. It ain't easy being green!",
			2,
			3,
			new List<Affliction> (){ zombified }
		); 

		//GOOD dialogue spoken by FRIEND during treatment for POISON
        
        Dialogue antiDope = new Dialogue (
             "That antidote is dope!",
             2,
             3,
             new List<Affliction> (){ poison }
         );

        Dialogue noPurple = new Dialogue (
             "Hurray, you de-purpled him!",
             2,
             3,
             new List<Affliction> (){ poison }
         );

		//BAD dialogue spoken by FRIEND during NONSPECIFIC treatment

		Dialogue notSure = new Dialogue (
			"Uh...are you sure that goes there?",
			2,
			1,
			new List<Affliction> (){ possessed, frozen, parasite, poison, zombified }
		);

		Dialogue makeWorse = new Dialogue (
			"I think you're making it worse.",
			2,
			1,
			new List<Affliction> (){ possessed, frozen, parasite, poison, zombified }
		);        

		Dialogue doRefunds = new Dialogue (
			"Do you do refunds?",
			2,
			1,
			new List<Affliction> (){ possessed, frozen, parasite, poison, zombified }
		);  

		Dialogue innRec = new Dialogue (
			"I should've known better than to take doctor recommendations from that guy at the inn.",
			2,
			1,
			new List<Affliction> (){ possessed, frozen, parasite, poison, zombified }
		); 

		Dialogue badFeeling = new Dialogue (
			"I've got a bad feeling about this.",
			2,
			1,
			new List<Affliction> (){ possessed, frozen, parasite, poison, zombified }
		);  

		Dialogue useForce = new Dialogue (
			"Maybe you should try using the Force.",
			2,
			1,
			new List<Affliction> (){ possessed, frozen, parasite, poison, zombified }
		); 

		Dialogue seeLicense = new Dialogue (
			"Can I see your license again?",
			2,
			1,
			new List<Affliction> (){ possessed, frozen, parasite, poison, zombified }
		); 

		//BAD dialogue spoken by FRIEND during treatment for POISON 
        
        Dialogue imSick = new Dialogue (
             "Even I'M starting to feel sick while watching you work.",
             2,
             1,
             new List<Affliction> (){ poison }
         );

        Dialogue notTelevision = new Dialogue (
             "This is definitely not what they do on TV.",
             2,
             1,
             new List<Affliction> (){ poison }
         );

		//BAD dialogue spoken by FRIEND during treatment for FROZEN
		Dialogue worstSlushie = new Dialogue (
			"Worst. Slushie. Ever.",
			2,
			1,
			new List<Affliction> (){ frozen }
		); 

		Dialogue hairDryer = new Dialogue (
			"Never mind, just give him back to me and I'll use a hair dryer or something.",
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

		Dialogue changedParty = new Dialogue (
			"He's just not the person I asked to join my party. He's...changed.",
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

		Dialogue betterWay = new Dialogue (
			"There has to be a better way.",
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

		Dialogue hideCloset = new Dialogue (
			"Never mind, I'll just hide him in a closet or something.",
			2,
			1,
			new List<Affliction> (){ zombified }
		); 

		//GOOD dialogue spoken by PLAYER during NONSPECIFIC treatment
		Dialogue goodTraining = new Dialogue (
			"Looks like all my training paid off. All 234,000 GP of it.",
			1,
			3,
			new List<Affliction> (){ possessed, frozen, parasite, poison, zombified }
		);

		Dialogue notNoob = new Dialogue (
			"Don't worry, I've totally done this before.",
			1,
			3,
			new List<Affliction> (){ possessed, frozen, parasite, poison, zombified }
		);

		Dialogue blueShield = new Dialogue (
			"It's dangerous to go alone. Take this Blue Shield Wellcare Plus plan...it's only 1,000 GP!",
			1,
			3,
			new List<Affliction> (){ possessed, frozen, parasite, poison, zombified }
		);

		Dialogue cashThanks = new Dialogue (
			"I'll take my payment in cold hard GP, thanks.",
			1,
			3,
			new List<Affliction> (){ possessed, frozen, parasite, poison, zombified }
		);

		Dialogue lookMa = new Dialogue (
			"Look, Ma! No hands!",
			1,
			3,
			new List<Affliction> (){ possessed, frozen, parasite, poison, zombified }
		);

		//GOOD dialogue spoken by PLAYER during treatment for POISON (need to add POISON to list of nonspecific treatment dialogue)
        
        Dialogue awesomeSnake = new Dialogue (
             "I'm so awesome, even snakes are afraid of me.",
             1,
             3,
             new List<Affliction> (){ poison }
         );

        Dialogue remedyMe = new Dialogue (
             "Looks like *I* am the remedy.",
             1,
             3,
             new List<Affliction> (){ poison }
         );

        Dialogue pieceCake = new Dialogue (
             "Piece of cake! Not poisoned cake, though.",
             1,
             3,
             new List<Affliction> (){ poison }
         );

		//GOOD dialogue spoken by PLAYER during treatment of FROZEN
		Dialogue hotStreak = new Dialogue (
			"I'm on a hot streak.",
			1,
			3,
			new List<Affliction> (){ frozen }
		);

		Dialogue mrFreeze = new Dialogue (
			"Just call me Mr. UnFreeze.",
			1,
			3,
			new List<Affliction> (){ frozen }
		);

		Dialogue walkingSunshine = new Dialogue (
			"Soon this guy will be walking on sunshine.",
			1,
			3,
			new List<Affliction> (){ frozen }
		);

		Dialogue hotHere = new Dialogue (
			"It's getting hot in here.",
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

		Dialogue cripplingDebt = new Dialogue (
			"Now if only I could shake off my crippling debt as easily.",
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

		Dialogue brainCleric = new Dialogue (
			"Brrrrrrains: The reason why I am good at being a cleric.",
			1,
			3,
			new List<Affliction> (){ zombified }
		);

		Dialogue donDead = new Dialogue (
			"I dub thee...'Don of the Dead.'",
			1,
			3,
			new List<Affliction> (){ zombified }
		);		

		Dialogue tobeZombie = new Dialogue (
			"To be or zombie? I choose c) to not be zombie.",
			1,
			3,
			new List<Affliction> (){ zombified }
		);

		//BAD dialogue spoken by PLAYER during NONSPECIFIC treatment
		Dialogue stillPaid = new Dialogue (
			"(I hope I still get paid.)",
			1,
			1,
			new List<Affliction> (){ possessed, frozen, parasite, poison, zombified }
		);

		Dialogue waitLevel = new Dialogue (
			"(Maybe I should have waited until I leveled up.)",
			1,
			1,
			new List<Affliction> (){ possessed, frozen, parasite, poison, zombified }
		);

		Dialogue clericalError = new Dialogue (
			"Sorry, I made a clerical error.",
			1,
			1,
			new List<Affliction> (){ possessed, frozen, parasite, poison, zombified }
		);

		Dialogue noNotice = new Dialogue (
			"(Maybe no one will notice...)",
			1,
			1,
			new List<Affliction> (){ possessed, frozen, parasite, poison, zombified }
		);

		//BAD dialogue spoken by PLAYER during treatment for POISON
        
        Dialogue oopsAspirin = new Dialogue (
             "Wait, I think this was just aspirin.",
             1,
             1,
             new List<Affliction> (){ poison }
         );

        Dialogue waitEnough = new Dialogue (
             "Maybe if we wait long enough, the poison will just go away.",
             1,
             1,
             new List<Affliction> (){ poison }
         );

		//BAD dialogue spoken by PLAYER during treatment for FROZEN            
		Dialogue letGo = new Dialogue (
			"Let's just...let it go.",
			1,
			1,
			new List<Affliction> (){ frozen }
		);   

		Dialogue shakenStirred = new Dialogue (
			"So, do you take your drinks shaken or stirred? Because this ice ain't go anywhere soon.",
			1,
			1,
			new List<Affliction> (){ frozen }
		); 

		Dialogue noIce = new Dialogue (
			"I guess I shouldn't recommend putting it on ice.",
			1,
			1,
			new List<Affliction> (){ frozen }
		); 

		Dialogue frozenPeas = new Dialogue (
			"That was about as effective as a bag of frozen peas.",
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

		Dialogue badMusic = new Dialogue (
			"I have another idea: just play really annoying music and maybe the ghost will go away.",
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

		Dialogue ratherFashionable = new Dialogue (
			"I think it's rather fashionable, don't you?",
			1,
			1,
			new List<Affliction> (){ parasite }
		); 

		Dialogue shrimpTempura = new Dialogue (
			"Are you sure it's not just a piece of shrimp tempura?",
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

		Dialogue zombieApocalypse = new Dialogue (
			"Ooh, yikes. I hope I'm not unwittingly contributing to the zombie apocalypse.",
			1,
			1,
			new List<Affliction> (){ zombified }
		);    

		Dialogue checkUp = new Dialogue (
			"Uh, just bring him in for a check-up. Like...28 days from now, maybe?",
			1,
			1,
			new List<Affliction> (){ zombified }
		);  		

		//STARTING DIALOGUE BELOW
	    //UNSYMPATHETIC starting dialogue for POISON

        StartingDialogue drinkStealer = new StartingDialogue (
            "Maybe some people here should stop going around taking other people's drinks.",
            1,
            new List<Affliction> (){ poison }
            );

        StartingDialogue milkExpire = new StartingDialogue (
            "We didn't realize the milk was past the expiration date.",
            1,
            new List<Affliction> (){ poison }
            );        

        StartingDialogue nightShade = new StartingDialogue (
            "Who knew eating something called 'deadly nightshade' could be dangerous?",
            1,
            new List<Affliction> (){ poison }
            ); 

        //UNSYMPATHETIC starting dialogue for FROZEN
        StartingDialogue meanSquirtle = new StartingDialogue (
            "I told him he shouldn’t tease Squirtles in the dead of winter, but no one ever listens to me.",
            1,
            new List<Affliction> (){ frozen }
            );

        StartingDialogue foxCoat = new StartingDialogue (
            "He refused to wear his 100,000 GP coat because he claimed that fox fur is SO not on trend right now.",
            1,
            new List<Affliction> (){ frozen }
            );

        StartingDialogue brainFreeze = new StartingDialogue (
            "I think it might just be brain freeze.",
            1,
            new List<Affliction> (){ frozen }
            );        

        StartingDialogue deathPeak = new StartingDialogue (
            "We went up Death Peak to admire the view. It was pretty romantic until the frostbite set in.",
            1,
            new List<Affliction> (){ frozen }
            );

        //UNSYMPATHETIC starting dialogue for POSSESSED
        StartingDialogue oweMoney = new StartingDialogue (
            "To be honest, I kind of prefer the ghost over him. But he owes me money, so.",
            1,
            new List<Affliction> (){ possessed }
            );

        StartingDialogue latinClass = new StartingDialogue (
            "I just thought he'd started taken Latin classes.",
            1,
            new List<Affliction> (){ possessed }
            );        

        //UNSYMPATHETIC starting dialogue for PARASITE
        StartingDialogue garbagePerson = new StartingDialogue (
            "What do you expect from a garbage person like this guy?",
            1,
            new List<Affliction> (){ parasite }
            );

        StartingDialogue isContagious = new StartingDialogue (
            "More importantly, though -- is this contagious, doc?",
            1,
            new List<Affliction> (){ parasite }
            );

        StartingDialogue notBedbugs = new StartingDialogue (
            "At least we didn't get bedbugs.",
            1,
            new List<Affliction> (){ parasite }
            );

        //UNSYMPATHETIC starting dialogue for ZOMBIFIED
        StartingDialogue graveyardFreak = new StartingDialogue (
            "I told him to stop wandering around the graveyard and pushing on tombstones like a freak.",
            1,
            new List<Affliction> (){ zombified }
            );

        StartingDialogue greenPaint = new StartingDialogue (
            "ZOMBIE? You mean it's not just green paint?",
            1,
            new List<Affliction> (){ zombified }
            );

        StartingDialogue thoughtGrain = new StartingDialogue (
            "I thought he was saying 'Graaaaains.' We just went vegan, you know.",
            1,
            new List<Affliction> (){ zombified }
            );

        StartingDialogue oldBroccoli = new StartingDialogue (
            "One day, he just turned the color of old broccoli.",
            1,
            new List<Affliction> (){ zombified }
            );

        //SYMPATHETIC starting dialogue for POISON

        StartingDialogue lifeSaver = new StartingDialogue (
            "Someone was trying to kill me! He saved my life! Who knew a pork chop could be so important?",
            3,
            new List<Affliction> (){ poison }
            );

        //SYMPATHETIC starting dialogue for FROZEN
        StartingDialogue hugeShip = new StartingDialogue (
            "He met this girl and they were in love but long story short, the ship they were on sank, there was this tiny piece of wreckage that was actually PERFECTLY serviceable yadda yadda yadda you get the story.",
            3,
            new List<Affliction> (){ frozen }
            );

        StartingDialogue polarPlunge = new StartingDialogue (
            "He was participating in the Snowpeak Polar Bear Plunge and I guess he plunged a little too long.",
            3,
            new List<Affliction> (){ frozen }
            );        

        //SYMPATHETIC starting dialogue for POSSESSED
        StartingDialogue familyCurse = new StartingDialogue (
            "It's not his fault he has a family curse. His grandma was a jerk to a necromancer one time or something.",
            3,
            new List<Affliction> (){ possessed }
            );

        //SYMPATHETIC starting dialogue for PARASITE
        StartingDialogue badLandlord = new StartingDialogue (
            "We've been telling the landlord for months about the infestation. It was only a matter of time before we got overrun. Btw, we're subletting if you're in the market...",
            3,
            new List<Affliction> (){ parasite }
            );

        //SYMPATHETIC starting dialogue for ZOMBIFIED
        StartingDialogue twoPregnant = new StartingDialogue (
            "He saved TWO pregnant ladies!",
            3,
            new List<Affliction> (){ zombified }
            );

        //RESPONSE DIALOGUE BELOW - Dialogue said by the PLAYER when you have/haven't applied the RELEASE FORM first
        //RESPONSE DIALOGUE - NEGATIVE            
        Dialogue forgettingSth = new Dialogue (
               "I feel like I'm forgetting something.",
               1,
               1,
               new List<Affliction> (){ possessed, frozen, parasite, zombified, poison }
        );    

        Dialogue paperworkSense = new Dialogue (
               "My paperwork sense is tingling.",
               1,
               1,
               new List<Affliction> (){ possessed, frozen, parasite, zombified, poison }
        );  

        Dialogue oneRule = new Dialogue (
               "The number one rule of the Healer's Guild: Protect thyself from liability.",
               1,
               1,
               new List<Affliction> (){ possessed, frozen, parasite, zombified, poison }
        );  

        Dialogue hangOn = new Dialogue (
               "Whoa, hang on, something’s missing.",
               1,
               1,
               new List<Affliction> (){ possessed, frozen, parasite, zombified, poison }
        );  

        Dialogue noExcitement = new Dialogue (
               "Trying to contain my excitement.",
               1,
               1,
               new List<Affliction> (){ possessed, frozen, parasite, zombified, poison }
        );                  

        Dialogue preCondition = new Dialogue (
               "What if this is a pre-existing condition?",
               1,
               1,
               new List<Affliction> (){ possessed, frozen, parasite, zombified, poison }
        );  

        //POSITIVE RESPONSE DIALOGUE BELOW

        Dialogue yayPaperwork = new Dialogue (
               "Oh good. You brought your release form. I love paperwork.",
               1,
               3,
               new List<Affliction> (){ possessed, frozen, parasite, zombified, poison }
        );         

        Dialogue releaseGP = new Dialogue (
               "Your release form releases me to do what I need to do to help your friend. It also releases GP from your pocket.",
               1,
               3,
               new List<Affliction> (){ possessed, frozen, parasite, zombified, poison }
        );         

        Dialogue timeWork = new Dialogue (
               "Time to get to work. It's a thing I do now.",
               1,
               3,
               new List<Affliction> (){ possessed, frozen, parasite, zombified, poison }
        );         

        Dialogue trueGenius = new Dialogue (
               "Now that the paperwork's out of the way, you can witness my true genius!",
               1,
               3,
               new List<Affliction> (){ possessed, frozen, parasite, zombified, poison }
        );         


	}
}
