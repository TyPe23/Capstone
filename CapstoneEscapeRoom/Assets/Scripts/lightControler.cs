// Name: Matthew Tucker 
// Description: Light on and off switch for one light - THIS DONES NOT WORK - (unity not updating with new lighting)
// Date: 1/24/2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightControler : MonoBehaviour
{

    public GameObject sourceLight;
    public Material uniqueMaterial;
    public int state = 1;
    public Material[] Materials; // materials list / colors
    public Renderer rend;

        

    // Start is called before the first frame update
    void Start()
    {
        Renderer renderer = sourceLight.GetComponent<Renderer>();
        uniqueMaterial = renderer.material;
        rend.enabled = true;
    }

    public void Switch()
    {
        if (state == 1)
        {
            state= 0;
            Off();
        }
        else
        {
            state= 1;
            On();
        }
    }

    void Off()
    {
        uniqueMaterial.SetColor("_EmissionColor", Color.black);
        //rend.sharedMaterial = Materials[1];
        uniqueMaterial.DisableKeyword("_EMISSION");
        uniqueMaterial.globalIlluminationFlags = MaterialGlobalIlluminationFlags.EmissiveIsBlack;
    }

    void On()
    {
        uniqueMaterial.SetColor("_EmissionColor", Color.white);
        //rend.sharedMaterial = Materials[0];
        uniqueMaterial.EnableKeyword("_EMISSION");
        uniqueMaterial.globalIlluminationFlags = MaterialGlobalIlluminationFlags.None;
    }
}
