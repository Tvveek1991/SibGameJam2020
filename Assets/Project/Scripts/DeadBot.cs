using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using TMPro;
using Assets.Project.Models;
using System.Collections.Generic;
using System.Collections;

public class DeadBot : MonoBehaviour
{
    [SerializeField] private bool isAlive = true;

    [SerializeField] private int energyBoost;

    [SerializeField] private UnityEvent OnBatteryOff;
    [SerializeField] private UnityEvent OnVisualContact;
    [SerializeField] private UnityEvent OnDead;
    [SerializeField] private string dialogueName = "";
    [SerializeField] private float dialogueChangeTime;
    [SerializeField] private TextMeshPro dialogText = null;
    private bool dialogActive = false;

    private List<DialogueModel> dialogues;

    IEnergy _energy;
    IPlayerLogic _playerLogic;

    [SerializeField] private Animator animator = null;

    private void Start()
    {
        _energy = FindObjectsOfType<MonoBehaviour>().OfType<IEnergy>().FirstOrDefault();
        _playerLogic = FindObjectsOfType<MonoBehaviour>().OfType<IPlayerLogic>().FirstOrDefault();

        dialogues = Dialogues.Instance.dialogueList;
    }

    public void TakeBattery()
    {
        if (isAlive)
        {
            isAlive = !isAlive;
            _energy.ChangeEnergy(energyBoost);
            Die();
            OnDead?.Invoke();
        }
        else
        {
            OnBatteryOff?.Invoke();
            print("Place is clear");
        }
    }

    public void Die()
    {
        animator.SetTrigger("Die");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            if (!isAlive)
                return;

            _energy.SetDeadBot(this);
            _playerLogic.SetByBattery(true);
            OnVisualContact?.Invoke();
            if (!dialogActive)
            {
                startDialogue();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            _energy.SetDeadBot(null);
            _playerLogic.SetByBattery(false);
        }
    }
    private void startDialogue()
    {
        dialogActive = true;
        var currentDialogue = dialogues.SingleOrDefault(x => x.DialogName == dialogueName);
        if (currentDialogue == null)
        {
            currentDialogue = new DialogueModel();
            var phraseModel = new PhraseModel();
            phraseModel.actor = Actors.NPC;
            phraseModel.text = "...";
            currentDialogue.Phrases.Add(phraseModel);
        }
        Debug.Log(currentDialogue.Phrases[0]);
        StartCoroutine(DialogueSequense(currentDialogue.Phrases, 0));
    }

    IEnumerator DialogueSequense(List<PhraseModel> Phrases, int startIndex)
    {
        if (startIndex >= Phrases.Count)
        {
            dialogActive = false;
            yield break;
        }
        var dialog = Instantiate(dialogText);
        dialog.text = Phrases[startIndex].text;
        dialog.transform.position = new Vector2(transform.position.x, transform.position.y + 2);
        startIndex++;
        yield return new WaitForSeconds(dialogueChangeTime);
        Destroy(dialog.transform.gameObject);
        StartCoroutine(DialogueSequense(Phrases, startIndex));
    }
}
