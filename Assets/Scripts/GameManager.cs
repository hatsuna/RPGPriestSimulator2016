
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public bool gameStarted;

	//public GameObject adventurer;
	Transform interactionPlane;
	public GameObject victimPrefab;
	public GameObject victimLocation;

	public GameObject toolPrefab;
	public Transform toolLocation;
	public static List<GameObject> tools = new List<GameObject>();

	public float toolDistance;

	GameObject heldObject;

	public GameObject spellEffectPrefab;
	public GameObject spellEffectLocation;
	public List<GameObject> spellsToClean= new List<GameObject>();

	public GameObject KarmaScoreObject;
	public int KarmaScore = 0;

	public Text leftTextUI;
	public Text rightTextUI;

	public GameObject HuDCanvas;

	public AudioSource audioGood;
	public AudioSource audioBad;

	// Scoring mechanics
	public int curedLinks = 0;
	public float timeRemaining = 120;
	public int cureChain = 0;

	public Text timeRemainingText;
	public Text curedLinksText;
	public Text cureChainText;


	public GameObject bloodParticles;
	GameObject bloodSpray;

	// Use this for initialization
	void Start () {

		gameStarted = false;

		cureChain = 0;
		curedLinks = 0;
		//SpawnVictim();
		/*make sure there is a valid adventurer on the altar
		if (adventurer.tag == "Adventurer"){
			//Debug.Log("You have an adventurer on the altar");
			//if (adventurer.
			foreach (Transform child in adventurer.transform){
				if(child.tag == "InteractionPlane"){
					interactionPlane = child;
				}
			}
		}*/

		bloodSpray = (GameObject)(Instantiate(bloodParticles, victimLocation.transform.position, victimLocation.transform.rotation));
		bloodSpray.SetActive(false);
			
	}

		// Update is called once per frame
	void Update () {
		if (gameStarted) {
			timeRemaining -= Time.deltaTime;
			timeRemainingText.text = "Time Remaining: " + timeRemaining;
			curedLinksText.text = "Cured Links: " + curedLinks;
			cureChainText.text = "Cure Chain: " + cureChain;
		}
		if (timeRemaining <= 0) {
			gameStarted = false;
			timeRemainingText.text = "Game Over!  Press R to restart!";
		}
		//getting the currently held object from the MouseControl Script
		//heldObject = GetComponent<MouseControl>().heldObject;

        //restart Level
        if (Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(0);
        }


	}

	public void SpawnVictim(){

		gameStarted = true;

		DialogueManager dialogueManager = HuDCanvas.GetComponent<DialogueManager>();

		DataStructures.Affliction newAffliction = GenerateAfflictions();

		//Add starting dialogue
		DataStructures.StartingDialogue startDialogue = DataStructures.getStarting (newAffliction);
		leftTextUI.text = startDialogue.text;
		dialogueManager.UpdateDialogueLeft ();

		// Instantiate adventurer prefab and move to table
		// This will automatically set up an affliction
		GameObject newVictim = (GameObject)(Instantiate(victimPrefab, victimLocation.transform.position, victimLocation.transform.rotation));
		// Apply affliction to spawned victim
		newVictim.GetComponent<Victim> ().ApplyAffliction (newAffliction);
	}

	public void GenerateTools(){
		// Clear existing list of tools
		for (int i = 0; i < tools.Count; i++) {
			Destroy (tools [i]);
		}
		tools.Clear();


		List<DataStructures.ToolType> possibleToolTypes = DataStructures.possibleToolTypes;
		Debug.Log (possibleToolTypes);
		for(int i = 0; i < possibleToolTypes.Count; i++){
			float distance = i * toolDistance;
			Debug.Log ("My tool name is: " + possibleToolTypes [i].name);
			Debug.Log ("My prefab is: "+possibleToolTypes [i].prefab);
			GameObject itemPrefab = possibleToolTypes [i].prefab;
			GameObject newTool = (GameObject)(Instantiate(toolPrefab, new Vector3(toolLocation.position.x + distance, toolLocation.position.y, toolLocation.position.z), toolLocation.rotation));
			GameObject newToolItem = (GameObject)(Instantiate(itemPrefab, newTool.transform.position, itemPrefab.transform.rotation));
			newToolItem.transform.parent = newTool.transform;
			tools.Add (newTool);
			tools[i].GetComponent<Tool>().tooltype = possibleToolTypes[i];
		}

	}

	public void ProcessTriggers(GameObject trigger, GameObject collider){
		//interactable trigger sites call this when they collide with a rigidbody
		//check if the two match up to create an interaction

		DialogueManager dialogueManager = HuDCanvas.GetComponent<DialogueManager>();


		if(trigger.tag == "InteractionPlane" && collider.tag == "Interactable"){
			DataStructures.ToolType tool = collider.GetComponent<Tool>().tooltype;
			DataStructures.Affliction affliction = trigger.GetComponent<Victim>().affliction;
			int afflictionState = trigger.GetComponent<Victim>().treatmentState;

			//ugly hardcoded nastiness
			switch(affliction.name){
			case("Poison"):
				afflictionState += 10;
				break;
			case("Frozen"):
				afflictionState += 20;
				break;
			case("Possessed"):
				afflictionState += 30;
				break;
			case("Parasite"):
				afflictionState += 40;
				break;
			case("Zombified"):
				afflictionState += 50;
				break;
			}

			DataStructures.Dialogue speech;

			if(tool.treatmentDict.ContainsKey(afflictionState) && 
				(tool.treatmentDict[afflictionState] == affliction)){

				//Give Feedback
				GameObject newSpell = (GameObject)(Instantiate(spellEffectPrefab, spellEffectLocation.transform.position, spellEffectLocation.transform.rotation));
				newSpell.GetComponent<Renderer>().sortingLayerName = "Particles";
				Renderer[] spellRenderers = newSpell.GetComponentsInChildren<Renderer>();
				for (int i = 0; i < spellRenderers.Length - 1; i++) {
					spellRenderers [i].sortingLayerName = "Particles";
				};
				newSpell.SetActive (true);
				spellsToClean.Add (newSpell);

				bloodSpray.SetActive(false);

				// Give Audio Feedback
				//audioGood.time = 0.6f;
				audioGood.Play ();

				//Get Dialogue
				//1 bad, 2 neutral, 3 good
				speech = gameObject.GetComponent<DataStructures>().getDialogue(3, affliction);
				if (speech.spokenBy == 1) {
					rightTextUI.text = speech.text;
					dialogueManager.UpdateDialogueRight ();
				} else if (speech.spokenBy == 2) {
					leftTextUI.text = speech.text;
					dialogueManager.UpdateDialogueLeft ();
				}

				// Advance cure chain
				if (gameStarted) {
					cureChain += 1;
				}


				//Advance Treatment State
				trigger.GetComponent<Victim>().treatmentState += 1;
				//under the assumption that the affliction has been cured,
				//these afflictionStates will remove the child-ed affliction effect
				switch(afflictionState){
				case (11):
				case (21):
				case (32):
				case (42):
				case (52):
				
					trigger.transform.GetChild(1).gameObject.SetActive(false);
					break;
				default:
					break;
				}
					

				//check endstate
				if (trigger.GetComponent<Victim> ().treatmentState ==
				   trigger.GetComponent<Victim> ().affliction.endState) {
					trigger.GetComponent<Renderer> ().material.color = Color.clear;
					Debug.Log ("I'm Cured!");
					// Give Karma
					KarmaScore += 40;
					KarmaScoreObject.GetComponent<Text> ().text = KarmaScore.ToString ();
					if (gameStarted){
						curedLinks += 1;
					}
					GenerateTools();
					Destroy(trigger);
					SpawnVictim();
				}

				//removed used tools
				collider.SetActive(false);

				// Check for Visual Changes
				//if (collider.transform.parent.GetComponent<Victim> ().affliction.name == "Parasite") {
					//delete parasite
				//}

			} else {
				Debug.Log("these don't match");
				//Get Dialogue
				speech = gameObject.GetComponent<DataStructures>().getDialogue(1, affliction);
				if (speech.spokenBy == 1) {
					rightTextUI.text = speech.text;
					dialogueManager.UpdateDialogueRight ();
				} else if (speech.spokenBy == 2) {
					leftTextUI.text = speech.text;
					dialogueManager.UpdateDialogueLeft ();
				}
				bloodSpray.SetActive(true);

				// Reset cure chain
				if (gameStarted) {
					cureChain = 0;
				}

				// Play Error Sound
				audioBad.Play();
			}
		}
	}

	public DataStructures.Affliction GenerateAfflictions () {
		List<DataStructures.Affliction> possibleAfflictions = DataStructures.possibleAfflictions;
		int afflictionIndex = Random.Range (0, possibleAfflictions.Count);
		Debug.Log ("My affliction index is: " + afflictionIndex);
		return possibleAfflictions [afflictionIndex];
	} 

}
