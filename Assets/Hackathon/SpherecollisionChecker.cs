using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpherecollisionChecker : MonoBehaviour
{
    // Define an action that can be assigned to handle the collision
    public Action OnSphereCollision;

    private float sphereRadius;   // Adjust to the radius of your sphere object
    public LayerMask hitLayers;         // Layers to check for hits, you can assign these in the inspector
    public bool invertEvent = false;
    private bool disableChecking = false;
    

    private void Start()
    {
        sphereRadius = gameObject.transform.lossyScale.x/2;
    }
    void Update()
    {
        // Perform an OverlapSphere from the current position of the object
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, sphereRadius, hitLayers);

        if (hitColliders.Length > 0 && ! invertEvent && !disableChecking) {
            foreach (Collider hitCollider in hitColliders)
            {
                Debug.Log(gameObject.name + ": Overlap detected with: " + hitCollider.gameObject.name);
            }
            OnSphereCollision.Invoke();
            disableChecking = true;
        }
        if (hitColliders.Length == 0 && invertEvent)
        {
            Debug.Log(" No overlap any more");
            OnSphereCollision.Invoke();
        }

        if (hitColliders.Length == 0)
        {
            disableChecking = false;
        }

    }
    void OnDrawGizmos()
    {
        // This draws a gizmo in the scene view to help visualize the OverlapSphere
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sphereRadius);
    }

    public float getSphereRadius() {  return sphereRadius; }
}
