using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPoints : MonoBehaviour
{
    public int bPoints = 0;
    public int pointsPer = 300;
    //PlayerPrefs.SetInt("BonusPoints", 0);
    public bool[] items = { false, false, false, false, false };

    public void updatePoints(int i)
    {
        if(!items[i])
        {
            try
            {
                bPoints = PlayerPrefs.GetInt("BonusPoints");
            }
            catch 
            { 
                bPoints = 0;
            }
            bPoints += pointsPer;
            PlayerPrefs.SetInt("BonusPoints", bPoints);
            items[i] = true;
        }
    }
       


}
