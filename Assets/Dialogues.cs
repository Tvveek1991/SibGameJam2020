using Assets.Project.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogues", menuName = "ScriptableObjects/DialoguesSO", order = 1)]
public class Dialogues : ScriptableObject
{
    private static Dialogues instance;
    public static Dialogues Instance { get { return instance; }}

    [SerializeField]
    public List<DialogueModel> dialogueList;

    public Dialogues(){
        instance = this;
    }
}
