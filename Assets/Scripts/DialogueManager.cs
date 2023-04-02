using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    public TextAsset inkJson;
    Story dialogue;

    public static DialogueManager DialogueInterface;

    private DataManager dataManager;

    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Button choicePrefab;
    [SerializeField] private GameObject buttonPanel;
    [SerializeField] private TextMeshProUGUI nameText;
    private TextMeshProUGUI choiceText;

    private bool dialogueRunning = false;
    private bool finalLine = false;

    private void Awake()
    {
        dialogue = new Story(inkJson.text);
        DialogueInterface = this;
        choiceText = choicePrefab.GetComponent<TextMeshProUGUI>();

        dialogue.ObserveVariable("mooseTransform", (string varName, object varValue) => ExecuteDruidTransformation());
    }

    private void Start()
    {
        dataManager = DataManager.Instance;
    }

    private void Update()
    {
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

    public void DisplayDialogue(string character)
    {
        dialogueRunning = true;

        dialogueBox.SetActive(true);
        dialogueText.gameObject.SetActive(true);
        dialogueText.text = "";

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

    private void RunDialogue()
    {
        RemoveButtons();
        string dlgTxt = dialogue.Continue();

        dlgTxt.Trim();

        string[] parsedDlg = ParseText(dlgTxt);

        if (parsedDlg[0] == "N")
        {
            nameText.text = "";
            dialogueText.text = "<i>" + parsedDlg[1] + "</i>";
        }
        else
        {
            nameText.text = parsedDlg[0];
            dialogueText.text = parsedDlg[1];
        }

        if (dialogue.canContinue)
        {
            return;
        }
        else if (dialogue.currentChoices.Count > 0)
        {
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
            //Debug.Log("Final line reached.");
            dialogueRunning = false;
            StartCoroutine(FinalLineDelay());
        }
    }

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
                charName = "Moose?";
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

    private void SelectDecision(Choice decision)
    {
        dialogueRunning = true;
        dialogue.ChooseChoiceIndex(decision.index);
        RunDialogue();
    }

    public void EndDialogue()
    {
        dialogueRunning = false;

        dialogueBox.SetActive(false);
        dialogueText.gameObject.SetActive(false);
        RemoveButtons();
    }

    private void RemoveButtons()
    {
        int childCount = buttonPanel.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Destroy(buttonPanel.transform.GetChild(i).gameObject);
        }
    }

    public void UpdateInkVariables()
    {
        dialogue.variablesState["chickens"] = GameManager.Instance.ChickensCollected;
        dialogue.variablesState["barriers"] = GameManager.Instance.BarriersCollected;
        dialogue.variablesState["herbs"] = GameManager.Instance.HerbsCollected;
    }

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
