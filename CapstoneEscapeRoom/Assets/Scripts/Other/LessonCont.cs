using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LessonCont : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      
        GameObject.FindGameObjectWithTag("ContButton").SetActive(false);
        
        StartCoroutine(ExampleCoroutine());
        GameObject.FindGameObjectWithTag("ContButton").SetActive(true);
    }

    IEnumerator ExampleCoroutine()
    {
        //Print the time of when the function is first called.
        

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);

        //After we have waited 5 seconds print the time again.
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
