using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pinShowHide : MonoBehaviour
{
    public GameObject[] pins;
    public ScoreBoard scores;
    public GameObject levelParticles;

    private string[][][] data;

    // Start is called before the first frame update
    void Start()
    {
        data = scores.data;
        StartCoroutine(waitThenAddLevels());
    }


    private IEnumerator waitThenAddLevels()
    {
        yield return new WaitForSecondsRealtime(3);
        for (int i = 0; i < data.Length; i++)
        {
            pins[i].SetActive(true);
            if (data[i] == null)
            {
                var newParticles = Instantiate(levelParticles, pins[i].transform.position, Quaternion.Euler(0, 0, 0));
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
