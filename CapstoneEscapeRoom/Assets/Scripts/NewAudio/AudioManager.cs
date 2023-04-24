using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    DROP,
    GRAB,
    UI,
    UNLOCK,
    ACCESS_DENIED,
    KEY_PRESS,
    APPEAR,
    DOOR,
}

public struct Range
{
    public float min;
    public float max;

    public Range(float min, float max)
    {
        this.min = min;
        this.max = max;
    }

    public float RandomValue()
    {
        return UnityEngine.Random.Range(min, max);
    }
}

public class SoundCollection
{
    private AudioClip[] clips;

    public bool HasClips
    {
        get { return clips.Length > 0; }
    }

    public SoundCollection(params string[] soundNames)
    {
        clips = new AudioClip[soundNames.Length];
        for (int i = 0; i < soundNames.Length; i++)
        {
            clips[i] = Resources.Load<AudioClip>(soundNames[i]);
            if (clips[i] == null)
            {
                MonoBehaviour.print($"Can't find clip with name '{soundNames[i]}'");
            }
        }
    }

    public AudioClip RandomClip()
    {
        if (clips.Length == 0)
        {
            return null;
        }
        else
        {
            int index = UnityEngine.Random.Range(0, clips.Length);
            return clips[index];
        }
    }
}

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public float masterVolumeMult = 1.0f;
    public static readonly Range pitchRange = new Range(0.75f, 1.25f);
    public static readonly Range volRange = new Range(0.75f, 1.0f);

    private AudioSource audioSrc;
    private Dictionary<SoundType, SoundCollection> sounds;

    private void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        sounds = new Dictionary<SoundType, SoundCollection>() {
            {SoundType.DROP, new SoundCollection("impact") },
            {SoundType.GRAB, new SoundCollection("grab") },
            {SoundType.UI, new SoundCollection("tick") },
            {SoundType.UNLOCK, new SoundCollection("unlockTech") },
            {SoundType.ACCESS_DENIED, new SoundCollection("lockedTech") },
            {SoundType.KEY_PRESS, new SoundCollection("keypress") },
            {SoundType.APPEAR, new SoundCollection("appear") },
            {SoundType.DOOR, new SoundCollection("door") },
        };
    }

    public void PlaySound(SoundType type, bool allowPitchShift = true, bool allowVolShift = true)
    {
        PlaySound(type, audioSrc, allowPitchShift, allowVolShift);
    }

    public void PlayOnce(SoundType type, bool allowPitchShift = true, bool allowVolShift = true)
    {
        PlayOnce(type, audioSrc);
    }
    private void PlayOnce(SoundType type, AudioSource audioSrc)
    {
        if (audioSrc == null)
        {
            audioSrc = this.audioSrc;
        }
        if (sounds.ContainsKey(type) && sounds[type].HasClips)
        {
            if (audioSrc.gameObject.activeSelf)
            {
                audioSrc.clip = sounds[type].RandomClip();
                audioSrc.PlayOneShot(audioSrc.clip, 0.01f);
            }
        }
    }

    private void PlaySound(SoundType type, AudioSource audioSrc, bool allowPitchShift = true, bool allowVolShift = true)
    {
        if (audioSrc == null)
        {
            audioSrc = this.audioSrc;
        }
        if (sounds.ContainsKey(type) && sounds[type].HasClips)
        {
            if (audioSrc.gameObject.activeSelf)
            {
                audioSrc.pitch = allowPitchShift ? pitchRange.RandomValue() : 1.0f;
                audioSrc.volume = allowVolShift ? volRange.RandomValue() : 1.0f;
                audioSrc.volume *= masterVolumeMult;
                audioSrc.clip = sounds[type].RandomClip();
                audioSrc.Play();
            }
        }
    }
}
