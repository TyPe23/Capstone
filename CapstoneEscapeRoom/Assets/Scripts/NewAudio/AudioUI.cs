using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioUI : MonoBehaviour
{
    private AudioSource m_AudioSource;

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    public void btnClick()
    {
        Game.globalInstance.sndPlayer.PlaySound(SoundType.UI, m_AudioSource);
    }
}
