using Assets.Project.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Jorrik : MonoBehaviour
{
    private bool activatable;
    private bool dialogIsActive;
    public PlayerController playerController; 
    [SerializeField] TextMeshPro dialogText;
    private List<DialogueModel> dialogues;
    // Start is called before the first frame update
    void Start()
    {
        
        dialogues = Dialogues.Instance.dialogueList;

    }

    // Update is called once per frame
    void Update()
    {
 
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (activatable)
        {
            StartDialog();
        }
    }

    void StartDialog()
    {
        activatable = false;
        dialogIsActive = true;
        playerController.set
        var currentDialogue = dialogues.FirstOrDefault(x => x.DialogName == SOMENAME );
        if (currentDialogue == null)
        {
            currentDialogue = new DialogueModel();
            var phraseModel = new PhraseModel();
            phraseModel.actor = Actors.NPC;
            phraseModel.text = "...";
            currentDialogue.Phrases.Add(phraseModel);
        }
        StartCoroutine(DialogueSequence(currentDialogue.Phrases, 0));
    }

    IEnumerator DialogueSequence(List<PhraseModel> Phrases, int startIndex)
    {
        if (startIndex >= Phrases.Count)
        {
            dialogIsActive = false;
            yield break;
        }
        dialogText.text = Phrases[startIndex].text;
        dialogText.transform.position = new Vector2(transform.position.x, transform.position.y + 2);
        var dialog = Instantiate(dialogText);
        startIndex++;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        Destroy(dialog.transform.gameObject);
        StartCoroutine(DialogueSequence(Phrases, startIndex));
    }

}
