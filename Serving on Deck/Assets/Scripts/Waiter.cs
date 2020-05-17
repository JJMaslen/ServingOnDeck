using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Waiter : GenericPerson
{
    public GameObject waitor;
    public Transform myServingTable;
    public GameObject myPerson;

    GameObject ServingTables;

    GameObject food;
    public bool hasFood;

    enum states { Waiting, GettingFood, Serving, Recalculating}
    states currentState;

    NavMeshAgent agent;

    int reCalcCounter;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        food = transform.Find("Food").gameObject;
        hasFood = false;

        ServingTables = GameObject.Find("ServingTables");
        currentState = states.Waiting;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasFood == true)
        {
            food.SetActive(true);
        }
        else
        {
            food.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        switch (currentState)
        {
            case states.Waiting:
                if (isPersonAvailable() == true && isServingTableAvailable() == true)
                {
                    if (findPerson() != waitor.transform && findServingTable() != waitor.transform)
                    {
                        myPerson.GetComponent<Person>().isBeingServed = true;

                        agent.destination = myServingTable.position;
                        currentState = states.GettingFood; 
                    }
                }
                Debug.Log("I am Waiting (Waitor)");
                break;
            case states.GettingFood:
                if (isClose(waitor.transform.position, myServingTable.position, 2.0f) == true)
                { 
                    agent.destination = myPerson.transform.position;
                    myServingTable.GetComponent<ServingTable>().inUse = true;
                    currentState = states.Serving;
                    hasFood = true;
                }
                Debug.Log("I am getting Food");
                break;
            case states.Serving:
                if (myPerson == null || myPerson.GetComponent<Person>().hasFood == true || myPerson.GetComponent<Person>().currentState.ToString() == "Leaving")
                {
                    findPerson();
                    agent.destination = myPerson.transform.position;
                }
                if (isClose(waitor.transform.position, myPerson.transform.position, 5.0f) == true)
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
            bool beingServed = child.gameObject.GetComponent<Person>().isBeingServed;
            if (state == "Waiting" && beingServed == false)
            {
                return true;
            }
        }
        return false;
    }

    bool isServingTableAvailable()
    {
        foreach (Transform child in ServingTables.transform)
        {
            bool state = child.gameObject.GetComponent<ServingTable>().inUse;
            if (state == false)
            {
                return true;
            }
        }
        return false;
    }

    Transform findPerson()
    {
        Transform newGoal = waitor.transform;

        int random = Random.Range(0, SpawnPerson.People.childCount);

        Transform peopleList = SpawnPerson.People;
        string state = peopleList.GetChild(random).gameObject.GetComponent<Person>().getState();

        if (state == "Waiting")
        {
            newGoal = peopleList.GetChild(random).transform;
            myPerson = peopleList.GetChild(random).gameObject;
        }
        return newGoal;
    }

    Transform findServingTable()
    {
        Transform newGoal = waitor.transform;

        int random = Random.Range(0, ServingTables.transform.childCount);

        if (ServingTables.transform.GetChild(random).gameObject.GetComponent<ServingTable>().inUse == false)
        {
            newGoal = ServingTables.transform.GetChild(random).transform;
            myServingTable = ServingTables.transform.GetChild(random);
        }
        return newGoal;
    }
}