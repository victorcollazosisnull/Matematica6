using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Audio", menuName = "Audio/volumenSettings")]
public class VolumenSettings : ScriptableObject
{
    [Header("Musica")]
    public AudioClip Music;
    [Header("Sonidos")]
    public AudioClip SFX;
    [Header("Configuracion de audios")]
    public float masterVolumen;
    public float musicVolumen;
    public float sfxVolumen;
}

