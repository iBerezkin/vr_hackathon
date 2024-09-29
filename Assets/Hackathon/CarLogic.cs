using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;


public enum Direction
{
    Left,
    Right,
    Straight,
    Stop
}

public class CarLogic : MonoBehaviour
{

    public float speed = 1;
    public SpherecollisionChecker leftColl = null;
    public SpherecollisionChecker rightColl = null;
    public SpherecollisionChecker groundColl = null;
    public SpherecollisionChecker frontColl = null;
    public Transform raycaster = null;

    public float directionThreshold = 0.01f;

    public LayerMask hitLayers;
    public LayerMask EnvironmentLayer;
    private Direction currentTarget = Direction.Straight;
    private float streetheight;

    // Start is called before the first frame update
    void Start()
    {
        leftColl.OnSphereCollision += HandleLeftSphereCollision;
        rightColl.OnSphereCollision += HandleRightSphereCollision;
        frontColl.OnSphereCollision += HandleFrontSphereNOCollision;
        streetheight = measureheight();

    }

    private void Update()
    {
        

  
    }
    private void FixedUpdate()
    {
        if (currentTarget != Direction.Stop)
        {
            gameObject.transform.position += gameObject.transform.forward * speed;
            //gameObject.transform.Translate(Vector3.forward * speed, Space.Self);
        }
    }

    private void stopdrive()
    {
        if (measureheight() == 10) {
            currentTarget = Direction.Straight;
            Debug.Log("Turning front");
            turnfront();

            return;
        }

        if (currentTarget != Direction.Left && currentTarget !=Direction.Right) { 
            currentTarget = Direction.Stop;
            Debug.Log("Stopping");
        }
        if (currentTarget == Direction.Left)
        {
            Debug.Log("Turning Left");
            turnleft();
            currentTarget = Direction.Straight;
        }
        if (currentTarget == Direction.Right)
        {
            Debug.Log("Turning Right");
            turnright();
            currentTarget = Direction.Straight;
        }
    }
    void turnright()
    {
        gameObject.transform.Rotate(Vector3.up, 90);
     
    }
    void turnleft()
    {
        gameObject.transform.Rotate(Vector3.up, -90);

    }
    void turnfront()
    {
        gameObject.transform.Rotate(Vector3.right, 90);
        //Rearrange height
        adjustHeight();

    }

    private void adjustHeight()
    {
        Debug.Log("Adjusting Heigt:");
        float cHeight = measureheight();
        float distance = streetheight - cHeight;
        transform.Translate(Vector3.up * distance, Space.Self);
    }
    float measureheight()
    {
        // Define the starting position of the raycast (e.g., from the object's position)
        Vector3 rayOrigin = raycaster.position;

        // Define the direction of the raycast (e.g., forward)
        Vector3 rayDirection = -transform.up;

        // Initialize a RaycastHit to store information about what the ray hits
        RaycastHit hit;

        // Perform the raycast and check for objects on the Street layer
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, 10, EnvironmentLayer))
        {
            // Measure the distance from the origin to the hit point
            float distance = hit.distance;

            // Log or process the distance
            Debug.Log("Hit the Street layer at a distance of: " + distance + " units.");
            return distance;
        }
        else
        {
            Debug.Log("No hit detected within " + 10 + " units.");
        }
        return 10;
    }


    // Method that handles the collision event
    void HandleLeftSphereCollision()
    {
        currentTarget = Direction.Left;
        // Here you can process the collision
        Debug.Log("A sphere collided with: ");
        //turnleft();
        // Further logic for specific collisions can go here
        // e.g., if (collision.gameObject.CompareTag("SpecificTag")) { }
    }

    // Method that handles the collision event
    void HandleRightSphereCollision()
    {
        currentTarget = Direction.Right;
        // Here you can process the collision
        Debug.Log("A sphere collided with: ");
        //turnright();
        // Further logic for specific collisions can go here
        // e.g., if (collision.gameObject.CompareTag("SpecificTag")) { }
    }
    void HandleFrontSphereNOCollision()
    {
        stopdrive();
        // Here you can process the collision
        //turnright();
        // Further logic for specific collisions can go here
        // e.g., if (collision.gameObject.CompareTag("SpecificTag")) { }
    }


}
