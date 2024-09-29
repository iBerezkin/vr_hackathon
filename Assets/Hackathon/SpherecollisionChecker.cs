using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpherecollisionChecker : MonoBehaviour
{
    // Define an action that can be assigned to handle the collision
    public Action OnSphereCollision;

    public float sphereRadius = 0.5f;  // Adjust to the radius of your sphere object
    public float castDistance = 5f;    // How far you want to cast the sphere
    public LayerMask hitLayers;


    void OnCollisionEnter(Collision collision)
    {

    }

    void Update()
    {
        // Cast the sphere from the object's position in the forward direction
        RaycastHit hit;

        // Perform a SphereCast
        if (Physics.SphereCast(transform.position, sphereRadius, transform.forward, out hit, castDistance, hitLayers))
        {
            // If the SphereCast hits something, process the collision
            Debug.Log("SphereCast hit: " + hit.collider.gameObject.name);
            OnSphereCollision.Invoke();
            
            // You can add further logic here based on what was hit
            // For example, stopping movement, changing direction, etc.
        }
    }


}
