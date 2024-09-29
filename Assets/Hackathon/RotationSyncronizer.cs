using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationSyncronizer : MonoBehaviour
{
    public GameObject y_sync;
    public GameObject z_sync;
    private Quaternion originalRotation;

    bool isSane = true;

    private Quaternion yRotationQuaternion;
    private Quaternion zRotationQuaternion;

    private float lastyRotation;
    private float lastzRotation;
    // Start is called before the first frame update
    void Start()
    {
        // Store the initial rotation of the cube so we have a base to work from
        float lastyRotation = y_sync.transform.eulerAngles.y;
        float lastzRotation = z_sync.transform.eulerAngles.z;
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
            // Get the global rotation of y_sync and z_sync
            
            float yRotation = y_sync.transform.eulerAngles.y;
            float zRotation = z_sync.transform.eulerAngles.z;
            Vector3 cubePosition = gameObject.transform.position;
            float ydiff = lastyRotation - yRotation;
            float zdiff = lastzRotation - zRotation;
             lastyRotation = yRotation;
             lastzRotation = zRotation;


            // Reset rotation to identity to avoid accumulation of previous rotations
            //gameObject.transform.rotation = Quaternion.identity;

            // Apply global Y rotation by setting the rotation around the global Y axis
            //gameObject.transform.rotation = Quaternion.Euler(0, yRotation, 0);
            //gameObject.transform.rotation = Quaternion.Euler(0, yRotation, 0);

            // After setting the Y rotation, apply global Z rotation based on the original global Z-axis
            //gameObject.transform.RotateAround(cubePosition, Vector3.forward, zRotation);

            //gameObject.transform.RotateAround(cubePosition, Vector3.up, yRotation);

            gameObject.transform.Rotate(0, ydiff, zdiff, Space.World);
            //float yRotation = y_sync.transform.eulerAngles.y;
            //float zRotation = z_sync.transform.eulerAngles.z;

            //// Create separate quaternions for Y and Z rotations
            //yRotationQuaternion = Quaternion.AngleAxis(yRotation, Vector3.up);
            //zRotationQuaternion = Quaternion.AngleAxis(zRotation, Vector3.forward);

            //// Apply rotations independently by multiplying the quaternions
            //gameObject.transform.rotation = yRotationQuaternion * zRotationQuaternion;
        }
    }
}
