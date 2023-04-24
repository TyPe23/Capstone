using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pinShowHide : MonoBehaviour
{
    public GameObject[] pins;
    public ScoreBoard scores;
    public GameObject levelParticles;
    public GameObject UI;
    public AudioAppear appear;

    private string[][][] data;

    // Start is called before the first frame update
    void Start()
    {
        data = scores.data;
        StartCoroutine(waitThenAddLevels());
    }


    private IEnumerator waitThenAddLevels()
    {
        for (int i = 0; i < data.Length; i++)
        {
            if (data[i][0][0] == "")
            {
                yield return new WaitForSecondsRealtime(3);
                pins[i].SetActive(true);
                appear.appear();
                var newParticles = Instantiate(levelParticles, pins[i].transform.position, Quaternion.Euler(0, 0, 0));
                break;
            }
            pins[i].SetActive(true);
        }
    }

    public void showPins()
    {
        if (UI.activeSelf)
        {
            for (int i = 0; i < data.Length; i++)
            {
                pins[i].SetActive(true);
                appear.appear();
                var newParticles = Instantiate(levelParticles, pins[i].transform.position, Quaternion.Euler(0, 0, 0));
            }
        }
    }
}
