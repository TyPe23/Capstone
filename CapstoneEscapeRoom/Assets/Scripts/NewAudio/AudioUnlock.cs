using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioUnlock : MonoBehaviour
{
    private AudioSource m_AudioSource;

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    public void unlock()
    {
        Game.globalInstance.sndPlayer.PlaySound(SoundType.UNLOCK, m_AudioSource);
    }
}
