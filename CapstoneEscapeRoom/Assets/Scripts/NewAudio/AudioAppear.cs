using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioAppear : MonoBehaviour
{
    private AudioSource m_AudioSource;

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    public void appear()
    {
        Game.globalInstance.sndPlayer.PlaySound(SoundType.APPEAR, m_AudioSource);
    }
}
