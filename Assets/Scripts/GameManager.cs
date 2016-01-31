
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

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

	// Use this for initialization
	void Start () {
		 
		SpawnVictim();
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
			
	}

		// Update is called once per frame
	void Update () {
		//getting the currently held object from the MouseControl Script
		//heldObject = GetComponent<MouseControl>().heldObject;

        //restart Level
        if (Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(0);
        }

	}

	void SpawnVictim(){
		// Instantiate adventurer prefab and move to table
		// This will automatically set up an affliction
		Instantiate(victimPrefab, victimLocation.transform.position, victimLocation.transform.rotation);
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
			case("Missing Limb"):
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
					spellRenderers [i].sortingLayerName = "Particle";
				};
				newSpell.SetActive (true);
				spellsToClean.Add (newSpell);

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


				//Advance Treatment State
				trigger.GetComponent<Victim>().treatmentState += 1;

				//check endstate
				if(trigger.GetComponent<Victim>().treatmentState ==
					trigger.GetComponent<Victim>().affliction.endState){
					trigger.GetComponent<Renderer>().material.color = Color.clear;
					Debug.Log("I'm Cured!");
					// Give Karma
					KarmaScore += 40;
					KarmaScoreObject.GetComponent<Text> ().text = KarmaScore.ToString();
					GenerateTools();
					Destroy(trigger);
					SpawnVictim();
				}

				//removed used tools
				collider.SetActive(false);

				// Check for Visual Changes
				if (collider.transform.parent.GetComponent<Victim> ().affliction.name == "Parasite") {
					//delete parasite
				}

			}else {
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
			}

		}
	}

}
