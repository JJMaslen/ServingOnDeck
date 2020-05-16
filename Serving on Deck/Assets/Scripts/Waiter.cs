using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waiter : MonoBehaviour
{
    public GameObject waitor;
    public Transform myServingTable;
    public bool hasFood;

    enum states { Waiting, Serving}
    states currentState;

    // Start is called before the first frame update
    void Start()
    {
        currentState = states.Waiting;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        switch (currentState)
        {
            case states.Waiting:
                Debug.Log("I am Waiting (Waitor)");
                break;
            case states.Serving:
                Debug.Log("I am Serving");
                break;
        }
    }

    Transform findServingTable()
    {
        Transform newGoal = waitor.transform;
        GameObject ServingTables = GameObject.Find("ServingTables");
        foreach (Transform child in ServingTables.transform)
        {
            if (child.GetComponent<ServingTable>().hasFood == false)
            {
                newGoal = child.transform;
                //child.GetComponent<ServingTable>().hasFood = true;
                break;
            }
        }

        return newGoal;
    }
}