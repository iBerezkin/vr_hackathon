using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject rightflicker;
    public Transform rightflickerp;
    public GameObject leftflicker;
    public Transform leftflickerp;
    public GameObject topflicker;
    public Transform topflickerp;
    public GameObject botflicker;
    public Transform botflickerp;

    public GameObject[] toEnable;

    public GameObject flickertoDisable;

    public TrafficLightController trafficlights;
    public CarLogic carLogic;
    public GameObject score;

    public TextMeshPro worldTextMesh; // Reference to the TextMesh component

   


    private bool counting = false;
    private float scoreval;


    public void loose() {
        worldTextMesh.text = "Inf";
        counting = false;
    }

    public void startGame() { 

        rightflicker.transform.position = rightflickerp.position;
        leftflicker.transform.position = leftflickerp.position;
        topflicker.transform.position = topflickerp.position;
        botflicker.transform.position = botflickerp.position;
        trafficlights.Go();
        Invoke("StartCar", 7);
        flickertoDisable.SetActive(false);

        foreach (GameObject go in toEnable) { 
            go.SetActive(true);
        }
    }

    void StartCar() {
        carLogic.go();
        counting = true;
        StartSequence();

    }

    void Finisch() {
        counting = false;
    }


    public void PrintEventInvoke()
    {
        Debug.Log("Eventtriggered");
        score.SetActive(true);
    }

    private void StartSequence() {
        StartCoroutine(DelayedMethod());
    }
    


// Coroutine with delay
    IEnumerator DelayedMethod()
    {
        // Print before delay
        Debug.Log("Sequence started!");

        // Wait for 2 seconds
        yield return new WaitForSeconds(0.5f);
        carLogic.pause();
        yield return new WaitForSeconds(0.1f);

        carLogic.go();
        carLogic.HandleRightSphereCollision();

        //yield return new WaitForSeconds(1f);
        //carLogic.HandleRightSphereCollision();


        yield return new WaitForSeconds(2f);
        carLogic.HandleLeftSphereCollision();

        yield return new WaitForSeconds(3f);
        carLogic.HandleRightSphereCollision();

        yield return new WaitForSeconds(1f);
        carLogic.HandleLeftSphereCollision();

        yield return new WaitForSeconds(4f);
        //carLogic.HandleLeftSphereCollision();

        yield return new WaitForSeconds(4f);
        carLogic.HandleLeftSphereCollision();


        yield return new WaitForSeconds(2.5f);
        carLogic.pause();

        yield return new WaitForSeconds(4.5f);
        carLogic.go();

        // Print after delay
        Debug.Log("Action resumed after 2 seconds!");

        yield break; // Properly ends the coroutine
    }



private void Update()
    {
        if (counting)
        {
            scoreval += Time.deltaTime;
            worldTextMesh.text = scoreval.ToString();
        }


        // Detect if the 'W' key is pressed down
        if (Input.GetKeyDown(KeyCode.Space))
        {
            startGame();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) 
        {
            Debug.Log("W key was pressed down!");
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) 
        {
            Debug.Log("W key was pressed down!");
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
            Debug.Log("W key was pressed down!");
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) 
        {
            Debug.Log("W key was pressed down!");
        }

    }


}
