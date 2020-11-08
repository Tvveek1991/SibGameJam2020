using UnityEngine;
using UnityEditor;
using System;

namespace Assets.Project.Models
{
    [Serializable]
    public class DialogueModel
    {
        [SerializeField]
        string DialogName;
        [SerializeField]
        string NPCName;
        [SerializeField]
        PhraseModel[] Phrases;
    }
}