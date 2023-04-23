using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioGrab : MonoBehaviour
{
    private AudioSource m_AudioSource;

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    public void grab()
    {
        Game.globalInstance.sndPlayer.PlaySound(SoundType.GRAB, m_AudioSource);
    }
}
