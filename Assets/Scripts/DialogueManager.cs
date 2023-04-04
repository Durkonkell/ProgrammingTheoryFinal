using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    //Controls the dialogue UI and interface between Ink and Unity

    public TextAsset inkJson;
    Story dialogue;

    public static DialogueManager DialogueInterface;

    private DataManager dataManager;

    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Button choicePrefab;
    [SerializeField] private GameObject buttonPanel;
    [SerializeField] private TextMeshProUGUI nameText;


    private bool dialogueRunning = false;
    private bool finalLine = false;

    private void Awake()
    {
        //Create the Ink story from the compiled JSON
        dialogue = new Story(inkJson.text);
        DialogueInterface = this;

        //Trigger druid transformation method when a line of dialogue is reached & variable changed in Ink
        dialogue.ObserveVariable("mooseTransform", (string varName, object varValue) => ExecuteDruidTransformation());
    }

    private void Start()
    {
        dataManager = DataManager.Instance;
    }

    private void Update()
    {

        //Enable LMB to advance dialogue if interface display and more content is available
        if (dialogueRunning && Input.GetMouseButtonDown(0) && dialogue.canContinue)
        {
            RunDialogue();
        }

        if (finalLine && Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Closing dialogue due to mouse click.");
            EndDialogue();
            finalLine = false;
        }
    }

    //Called when the player interacts with a character
    public void DisplayDialogue(string character)
    {
        dialogueRunning = true;

        dialogueBox.SetActive(true);
        dialogueText.gameObject.SetActive(true);
        dialogueText.text = "";

        //Switch to the correct knot for the specified character in Ink
        switch (character)
        {
            case "Farmer":
                dialogue.ChoosePathString("FARMER");
                RunDialogue();
                break;
            case "Druid_Moose":
                dialogue.ChoosePathString("DRUID");
                RunDialogue();
                break;
            case "Engineer":
                dialogue.ChoosePathString("ENGINEER");
                RunDialogue();
                break;
            default:
                dialogue.ChoosePathString("DOG");
                RunDialogue();
                break;
        }
    }

    //Display the next line of dialogue if one is available, and display choices or close if not.
    private void RunDialogue()
    {
        RemoveButtons();
        string dlgTxt = dialogue.Continue();

        dlgTxt.Trim();

        //Send the trimmed line of content to be parsed
        string[] parsedDlg = ParseText(dlgTxt);

        //If the returned, parsed line is supposed to be from the narrator, put it in italics and leave the name field blank
        if (parsedDlg[0] == "N")
        {
            nameText.text = "";
            dialogueText.text = "<i>" + parsedDlg[1] + "</i>";
        }
        else
        {
            //In the returned parsed text array, 0 is the character name to be displayed and 1 is the content
            nameText.text = parsedDlg[0];
            dialogueText.text = parsedDlg[1];
        }

        if (dialogue.canContinue)
        {
            return;
            //End this method if there's more content available - Update() waits for mouse click to advance
        }
        else if (dialogue.currentChoices.Count > 0)
        {
            //If there are choices for the player to make, call DisplayChoice for each one and set up the onClick action
            dialogueRunning = false;
            for (int i = 0; i < dialogue.currentChoices.Count; i++)
            {
                Choice currentChoice = dialogue.currentChoices[i];
                Button choiceButton = DisplayChoice(currentChoice.text.Trim());

                choiceButton.onClick.AddListener(delegate { SelectDecision(currentChoice); });
            }
        }
        else
        {
            //Set up for the next mouse click to close the dialogue interface
            //Debug.Log("Final line reached.");
            dialogueRunning = false;
            StartCoroutine(FinalLineDelay());
        }
    }

    //Split the string when ':' is encountered and return an array with the name of the currently speaking character and the content
    private string[] ParseText(string line)
    {
        string[] rawLine = line.Split(":");
        if (rawLine.Length == 1)
        {
            string[] parsedString = new string[2];
            parsedString[0] = dataManager.playerName;
            parsedString[1] = rawLine[0];
            return parsedString;
        }

        string charName = "";

        switch (rawLine[0])
        {
            case "F":
                charName = "Farmer";
                break;
            case "M":
                charName = "A Moose?";
                break;
            case "D":
                charName = "Druid";
                break;
            case "E":
                charName = "Engineer";
                break;
            case "Dog":
                charName = "Dog";
                break;
            case "N":
                charName = "N";
                break;
            default:
                break;
        }

        string[] parsedLine = new string[2];

        parsedLine[0] = charName;
        parsedLine[1] = rawLine[1];

        return parsedLine;
    }

    //Create button from prefab and give it the correct text
    private Button DisplayChoice(string choiceText)
    {
        Button choiceButton = Instantiate(choicePrefab);
        choiceButton.transform.SetParent(buttonPanel.transform, false);

        TextMeshProUGUI buttonText = choiceButton.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = choiceText;

        HorizontalLayoutGroup layoutGroup = choiceButton.GetComponent<HorizontalLayoutGroup>();
        layoutGroup.childForceExpandHeight = false;

        return choiceButton;
    }

    //Tell Ink what the player has chosen
    private void SelectDecision(Choice decision)
    {
        dialogueRunning = true;
        dialogue.ChooseChoiceIndex(decision.index);
        RunDialogue();
    }

    //Remove all dialogue elements from the screen
    public void EndDialogue()
    {
        dialogueRunning = false;

        dialogueBox.SetActive(false);
        dialogueText.gameObject.SetActive(false);
        RemoveButtons();
    }

    //The buttons are all child objects of the empty button panel object. Destroy them all.
    private void RemoveButtons()
    {
        int childCount = buttonPanel.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Destroy(buttonPanel.transform.GetChild(i).gameObject);
        }
    }

    //Set variables in ink to match variables in the game manager, so that we can refer to them in dialogue.
    public void UpdateInkVariables()
    {
        dialogue.variablesState["chickens"] = GameManager.Instance.ChickensCollected;
        dialogue.variablesState["barriers"] = GameManager.Instance.BarriersCollected;
        dialogue.variablesState["herbs"] = GameManager.Instance.HerbsCollected;
    }

    //Add a one frame delay before allowing mouse click to close the dialogue UI
    //Otherwise the click that shows the final line will also immediately cause it to close on the same frame
    IEnumerator FinalLineDelay()
    {
        yield return new WaitForEndOfFrame();
        finalLine = true;
    }

    void ExecuteDruidTransformation()
    {
        //Debug.Log("Executing Moose Transformation");

        Druid mooseControl = FindObjectOfType<Druid>();
        mooseControl.TriggerTransformation();
    }

}
