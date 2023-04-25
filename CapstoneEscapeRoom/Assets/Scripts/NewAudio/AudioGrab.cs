using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioGrab : MonoBehaviour
{
    private AudioSource m_AudioSource;
    private bool canPlay = true;

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    public void grab()
    {
        Game.globalInstance.sndPlayer.PlaySound(SoundType.GRAB, m_AudioSource);
        StartCoroutine(Cooldown());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (canPlay && Time.time > 3)
        {
            Game.globalInstance.sndPlayer.PlaySound(SoundType.DROP, m_AudioSource);
            StartCoroutine(Cooldown());
        }
    }

    public IEnumerator Cooldown()
    {
        canPlay = false;
        yield return new WaitForSecondsRealtime(0.1f);
        canPlay = true;
    }
}
