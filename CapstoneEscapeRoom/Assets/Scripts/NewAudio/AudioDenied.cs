using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioDenied : MonoBehaviour
{
    private AudioSource m_AudioSource;

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    public void deny()
    {
        Game.globalInstance.sndPlayer.PlaySound(SoundType.ACCESS_DENIED, m_AudioSource);
    }
}
