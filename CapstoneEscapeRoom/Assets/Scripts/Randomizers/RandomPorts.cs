using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPorts : MonoBehaviour
{
    // all the port objects 
    public GameObject[] allPorts;
    // empty arry of vector 3s 
    private Vector3[] locations;
    private Quaternion[] rotationY;
    private int HARDCODE = -1; 

    // Start is called before the first frame update
    void Start()
    {
        locations = new Vector3[allPorts.Length]; // set length of locations
        rotationY = new Quaternion[allPorts.Length];
        // get locations of all ports 
        int i = 0;
        foreach(GameObject port in allPorts)
        {
            locations[i]= port.transform.position;
            rotationY[i]=port.transform.rotation;
            i++;
        }
        List<int> used = new List<int>();
        int x = 0;
        bool c = true;
        if (HARDCODE == -1)
        {
            foreach (GameObject port in allPorts)
            {
                c = true;
                while (c)
                {
                    x = Random.Range(0, allPorts.Length);
                    if (!(used.Contains(x)))
                    {
                        port.transform.position = locations[x];
                        port.transform.rotation = rotationY[x];
                        c = false;
                        used.Add(x);
                    }
                }

            }
        }

    }

}
