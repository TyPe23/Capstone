using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pinShowHide : MonoBehaviour
{
    public GameObject[] pins;
    public ScoreBoard scores;
    public GameObject levelParticles;
    public GameObject UI;

    private string[][][] data;

    // Start is called before the first frame update
    void Start()
    {
        data = scores.data;
        StartCoroutine(waitThenAddLevels());
    }


    private IEnumerator waitThenAddLevels()
    {
        yield return new WaitForSecondsRealtime(5);
        for (int i = 0; i < data.Length; i++)
        {
            pins[i].SetActive(true);
            if (data[i][0][0] == "")
            {
                var newParticles = Instantiate(levelParticles, pins[i].transform.position, Quaternion.Euler(0, 0, 0));
                break;
            }
        }
    }

    public void showPins()
    {
        if (UI.activeSelf)
        {
            for (int i = 0; i < data.Length; i++)
            {
                pins[i].SetActive(true);
                var newParticles = Instantiate(levelParticles, pins[i].transform.position, Quaternion.Euler(0, 0, 0));
            }
        }
    }
}
