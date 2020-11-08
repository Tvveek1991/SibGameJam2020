using UnityEngine;
using UnityEditor;
using System;

namespace Assets.Project.Models
{
    [Serializable]
    public class PhraseModel
    {
        [SerializeField]
        Actors actor;
        [SerializeField]
        string text;
    }
}

enum Actors
{
    NPC,
    PLAYER
}