// Name: Matthew Tucker
// Description: Give a object multiple locations and randomly select 1 in a local variable 
// Date: 4/22/23
using UnityEngine;

public class RabdinLocationLocal : MonoBehaviour
{
    public Vector3[] Locations; // locations the object could be (x,y,z)

    public void Start()
    {
        if (Locations.Length > 0) // if more then 0 locations to place object 
        {
            int y = UnityEngine.Random.Range(0, Locations.Length);// select position 
            transform.localPosition = Locations[y]; // set position 
        }
    }
}
