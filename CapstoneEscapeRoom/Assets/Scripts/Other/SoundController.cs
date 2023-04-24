using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SoundController : MonoBehaviour
{
    [SerializeField] private Slider soundSlid = null;


    private void Start()
    {
        LoadValues();
    }

    public void SaveButton()
    {
        float soundValue = soundSlid.value;
        PlayerPrefs.SetFloat("Sound", soundValue / 100);
        LoadValues();
    }

    public void LoadValues()
    {
        float soundValue = PlayerPrefs.GetFloat("Sound");
        soundSlid.value = soundValue * 100;
        AudioListener.volume = soundValue;
    }
}
