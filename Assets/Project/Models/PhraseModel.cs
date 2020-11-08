using UnityEngine;
using UnityEditor;
using System;

namespace Assets.Project.Models
{
    [Serializable]
    public class PhraseModel
    {
        [SerializeField]
        public Actors actor;
        [SerializeField]
        public string text;
    }
}

public enum Actors
{
    NPC,
    PLAYER
}