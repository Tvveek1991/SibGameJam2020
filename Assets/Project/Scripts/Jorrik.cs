using Assets.Project.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Jorrik : MonoBehaviour
{
    private bool activatable = true;
    private bool dialogIsActive;
    private bool winConditionMet;
    public PlayerController playerController;
    public SaveSystem saveSystem;
    public UnityEvent wonGame;
    public UnityEvent dialogStarted;
    public UnityEvent dialogEnded;
    [SerializeField] TextMeshPro dialogText;
    private List<DialogueModel> dialogues;
    

    void Start()
    {
        dialogues = Dialogues.Instance.dialogueList;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player") && activatable)
        {
            if (saveSystem.GetCountFinishedLines() == 0 || Input.GetKeyDown(KeyCode.E))
            {
                StartDialog();
            }
        }
    }

    void StartDialog()
    {
        activatable = false;
        dialogIsActive = true;
        dialogStarted.Invoke();
        playerController.SetMoveAccess(false);
        int dialogNumber = saveSystem.GetCountFinishedLines();
        if (dialogNumber > 3)
        {
            winConditionMet = true;
        }
        var currentDialogue = dialogues.FirstOrDefault(x => x.DialogName == $"JORRIK_{dialogNumber}");
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
            if (winConditionMet)
            {
                wonGame.Invoke();
            }
            playerController.SetMoveAccess(true);
            dialogIsActive = false;
            dialogEnded.Invoke();
            yield break;
        }
        dialogText.text = Phrases[startIndex].text;
        dialogText.transform.position = new Vector2(transform.position.x, transform.position.y + 2);
        var dialog = Instantiate(dialogText);
        startIndex++;
        yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.E));
        yield return new WaitForEndOfFrame();
        Destroy(dialog.transform.gameObject);
        StartCoroutine(DialogueSequence(Phrases, startIndex));
    }

    IEnumerator WaitFewSecondsAndContinue(List<PhraseModel> Phrases, int startIndex)
    {
        yield return new WaitForEndOfFrame();

    }



    public void ActivateJorrikDialogueAbility()
    {
        activatable = true;
    }

}
