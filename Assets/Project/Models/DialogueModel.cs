using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

namespace Assets.Project.Models
{
    [Serializable]
    public class DialogueModel
    {
        [SerializeField]
        public OnBase onBase;
        [SerializeField]
        public string DialogName;
        [SerializeField]
        public string NPCName;
        [SerializeField]
        public List<PhraseModel> Phrases;
    }
}

public enum OnBase { 
    NO,
    YES
}