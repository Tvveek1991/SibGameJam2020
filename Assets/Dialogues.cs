using Assets.Project.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogues", menuName = "ScriptableObjects/DialoguesSO", order = 1)]
public class Dialogues : ScriptableObject
{
    [SerializeField]
    DialogueModel[] dialogueList;
}
