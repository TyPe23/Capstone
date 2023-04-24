using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioDoor : MonoBehaviour
{
    private AudioSource m_AudioSource;

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    public void open()
    {
        Game.globalInstance.sndPlayer.PlaySound(SoundType.DOOR, m_AudioSource);
    }
}
