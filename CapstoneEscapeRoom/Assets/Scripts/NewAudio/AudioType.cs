using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioType : MonoBehaviour
{
    private AudioSource m_AudioSource;

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    public void btnClick()
    {
        Game.globalInstance.sndPlayer.PlaySound(SoundType.KEY_PRESS, m_AudioSource);
    }
}
