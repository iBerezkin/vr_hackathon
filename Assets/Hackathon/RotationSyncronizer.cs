using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationSyncronizer : MonoBehaviour
{
    public GameObject y_sync;
    public GameObject z_sync;

    bool isSane = true;

    // Start is called before the first frame update
    void Start()
    {
        if (y_sync == null)
        {
            Debug.Log("y Rotation Objeect not set");
            isSane = false;
        }
        if (z_sync == null)
        {
            Debug.Log("x Rotation Objeect not set");
            isSane = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isSane) { 
        float yrot = y_sync.transform.rotation.eulerAngles.y;
        float xrot =  gameObject.transform.rotation.eulerAngles.z;
        float zrot = z_sync.transform.rotation.eulerAngles.x;

        gameObject.transform.rotation = Quaternion.Euler(xrot, yrot, zrot);
        }
    }
}
