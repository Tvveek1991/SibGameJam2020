using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlay : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioSource source;

    public void PlaySound()
    {
        source.clip = clip;
        source.Play();
    }
}
