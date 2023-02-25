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
        float soundValue = soundSlid.value/100;
        PlayerPrefs.SetFloat("Sound", soundValue);
        LoadValues();
    }

    void LoadValues()
    {
        float soundValue = PlayerPrefs.GetFloat("Sound");
        soundSlid.value = soundValue;
        AudioListener.volume = soundValue;
            
    }
}
