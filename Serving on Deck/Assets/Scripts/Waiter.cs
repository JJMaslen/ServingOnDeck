using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Waiter : GenericPerson
{
    public GameObject waitor;
    public Transform myServingTable;
    public GameObject myPerson;
    public bool hasFood;

    enum states { Waiting, GettingFood, Serving}
    states currentState;

    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        hasFood = false;
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
                if (isPersonAvailable() == true)
                {
                    myServingTable = findServingTable();
                    agent.destination = myServingTable.position;
                    currentState = states.GettingFood;
                }
                Debug.Log("I am Waiting (Waitor)");
                break;
            case states.GettingFood:
                if (isClose(waitor.transform.position, myServingTable.position, 2.0f) == true)
                {
                    myPerson = findPerson().gameObject;
                    agent.destination = myPerson.transform.position;
                    currentState = states.Serving;
                    hasFood = true;
                }
                Debug.Log("I am getting Food");
                break;
            case states.Serving:
                if (isClose(waitor.transform.position, myPerson.transform.position, 3.0f) == true)
                {
                    agent.destination = waitor.transform.position;
                    currentState = states.Waiting;
                    hasFood = false;
                    myPerson.GetComponent<Person>().setFoodTrue();
                }
                Debug.Log("I am Serving");
                break;
        }
    }

    bool isPersonAvailable()
    {
        foreach (Transform child in SpawnPerson.People)
        {
            string state = child.gameObject.GetComponent<Person>().getState();
            if (state == "Waiting")
            {
                return true;
            }
        }
        return false;
    }

    Transform findPerson()
    {
        Transform newGoal = waitor.transform;

        foreach (Transform child in SpawnPerson.People)
        {
            string state = child.gameObject.GetComponent<Person>().getState();
            if (state == "Waiting")
            {
                newGoal = child.transform;
                myPerson = child.gameObject;
            }
        }
        return newGoal;
    }

    Transform findServingTable()
    {
        Transform newGoal = waitor.transform;
        GameObject ServingTables = GameObject.Find("ServingTables");
        foreach (Transform child in ServingTables.transform)
        {
            if (child.GetComponent<ServingTable>().hasFood == true)
            {
                newGoal = child.transform;
                break;
                //child.GetComponent<ServingTable>().hasFood = false;
            }
        }

        return newGoal;
    }
}