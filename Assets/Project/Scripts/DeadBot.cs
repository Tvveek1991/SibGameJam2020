using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using System.Collections.Generic;
using Assets.Project.Models;
using System.Collections;
using TMPro;
using System.Threading;

public class DeadBot : MonoBehaviour
{
    [SerializeField] private bool isAlive = true;

    [SerializeField] private int energyBoost;

    [SerializeField] private UnityEvent OnBatteryOff;
    [SerializeField] private UnityEvent OnVisualContact;
    [SerializeField] private string dialogueName;
    [SerializeField] private float dialogueChangeTime;
    [SerializeField] private TextMeshPro dialogText = null;

    private List<DialogueModel> dialogues;
    IEnergy _energy;
    IPlayerLogic _playerLogic;

    private void Start()
    {
        dialogues = Dialogues.Instance.dialogueList;
        _energy = FindObjectsOfType<MonoBehaviour>().OfType<IEnergy>().FirstOrDefault();
        _playerLogic = FindObjectsOfType<MonoBehaviour>().OfType<IPlayerLogic>().FirstOrDefault();
    }

    public void TakeBattery()
    {
        if (isAlive)
        {
            isAlive = !isAlive;
            _energy.ChangeEnergy(energyBoost);
        }
        else
        {
            OnBatteryOff?.Invoke();
            print("Place is clear");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(dialogues);
        if (collision.tag.Equals("Player"))
        {
            Debug.Log("Is Player");
            if (!isAlive)
                return;
            Debug.Log("Is Alive");
            _energy.SetDeadBot(this);
            _playerLogic.SetByBattery(true);
            OnVisualContact?.Invoke();
            startDialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            if (!isAlive)
                return;
            _energy.SetDeadBot(null);
            _playerLogic.SetByBattery(false);
        }
    }

    private void startDialogue()
    {
        var currentDialogue = dialogues.FirstOrDefault(x => x.DialogName == dialogueName);
        if (currentDialogue == null)
        {
            currentDialogue = new DialogueModel();
            var phraseModel = new PhraseModel();
            phraseModel.actor = Actors.NPC;
            phraseModel.text = "...";
            currentDialogue.Phrases.Add(phraseModel);
        }
        StartCoroutine(DialogueSequense(currentDialogue.Phrases, 0));
    }

    IEnumerator DialogueSequense(List<PhraseModel> Phrases, int startIndex)
    {
        if (startIndex >= Phrases.Count)
        {
            yield break;
        }
        dialogText.text = Phrases[startIndex].text;
        dialogText.transform.position = new Vector2(transform.position.x, transform.position.y + 2);
        var dialog = Instantiate(dialogText);
        startIndex++;
        yield return new WaitForSeconds(dialogueChangeTime);
        Destroy(dialog.transform.gameObject);
        StartCoroutine(DialogueSequense(Phrases, startIndex));
    }

}
