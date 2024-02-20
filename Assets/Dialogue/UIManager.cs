using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class UIManager : Singleton<UIManager>
{
	//public Image selectionRectangle;
	//public Image cameraLockedIcon;

	public TextMeshProUGUI dialogueLineText;
	//public GameObject toggleSpacebarMessage, dialoguePanel;

	private string tsvFilePath = "Timelines/Cutscene_Test_KO.tsv";


	private void Start()
	{
		//selectionRectangle.enabled = false;
		//cameraLockedIcon.enabled = false;
		string filePath = Path.Combine(Application.dataPath, tsvFilePath);
        ReadTSVFile(filePath);
	}

	private void Update()
	{
		
	}
	void ReadTSVFile(string fullPath)
    {
        string[] lines = File.ReadAllLines(fullPath);
        foreach (string line in lines)
        {
            string[] fields = line.Split('\t');
            DialogueEntry entry = new DialogueEntry
            {
                ActorCode = fields[0],
                DialogueText = fields[1]
            };
            dialogueList.Add(entry);
        }
    }
	public struct DialogueEntry
    {
        public string ActorCode;
        public string DialogueText;
    }

	 private List<DialogueEntry> dialogueList = new List<DialogueEntry>();

	public void ToggleSelectionRectangle(bool active)
	{
		//selectionRectangle.enabled = active;
	}

	public void ToggleCameraLockedIcon(bool active)
	{
		//cameraLockedIcon.enabled = active;
	}

	public void SetSelectionRectangle(Rect rectSize)
	{
		//selectionRectangle.rectTransform.position = rectSize.center;
		//selectionRectangle.rectTransform.ForceUpdateRectTransforms();
		//selectionRectangle.rectTransform.sizeDelta = new Vector2(rectSize.width, rectSize.height);
	}

	public void SetDialogue(string charName, string lineOfDialogue, int sizeOfDialogue)
	{
		//charNameText.SetText(charName);
		dialogueLineText.SetText(lineOfDialogue);
		dialogueLineText.fontSize = sizeOfDialogue;

		//ToggleDialoguePanel(true);
		
	}

	
	public void TogglePressSpacebarMessage(bool active)
	{
		//toggleSpacebarMessage.SetActive(active);
	}

	public void ToggleDialoguePanel(bool active)
	{
		//dialoguePanel.SetActive(active);
	}
}
