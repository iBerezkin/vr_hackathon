using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.PlasticSCM.Editor.WebApi;
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
    private Direction currentTarget = Direction.Straight;

    // Start is called before the first frame update
    void Start()
    {
        leftColl.OnSphereCollision += HandleLeftSphereCollision;
        rightColl.OnSphereCollision += HandleLeftSphereCollision;
    }

    private void Update()
    {
        if (currentTarget != Direction.Stop)
        {
            gameObject.transform.position += gameObject.transform.forward * speed;
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



    // Method that handles the collision event
    void HandleLeftSphereCollision()
    {
        // Here you can process the collision
        Debug.Log("A sphere collided with: ");
        turnleft();
        // Further logic for specific collisions can go here
        // e.g., if (collision.gameObject.CompareTag("SpecificTag")) { }
    }

    // Method that handles the collision event
    void HandleRightSphereCollision()
    {
        // Here you can process the collision
        Debug.Log("A sphere collided with: ");
        turnright();
        // Further logic for specific collisions can go here
        // e.g., if (collision.gameObject.CompareTag("SpecificTag")) { }
    }


}
