using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SocketInvSystem : MonoBehaviour

{
    public GameObject HMD;


    private Vector3 _currentHMDlocalPosition;
    private Quaternion _currentHMDRotation;

    void Update()
    {
        _currentHMDlocalPosition = HMD.transform.localPosition;
        _currentHMDRotation = HMD.transform.rotation;

        UpdateSocketInventory();
    }


    private void UpdateSocketInventory()
    {
        transform.localPosition = new Vector3(_currentHMDlocalPosition.x, _currentHMDlocalPosition.y - 1.5f, _currentHMDlocalPosition.z);
        transform.rotation = new Quaternion(transform.rotation.x, _currentHMDRotation.y, transform.rotation.z, _currentHMDRotation.w);
    }
}
