using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Person : GenericPerson
{
    public GameObject person;
    public Transform mySeat;
    public GameObject food;
    public GameObject happyObject;
    public GameObject sadObject;

    public static Transform entrance;
    public static Transform exit;

    public enum states { Entering, Sitting, Waiting, Eating, Leaving}
    public states currentState;

    public bool hasFood;
    public bool isBeingServed;
    bool isHappy;
    int waitingTimer;
    int eatingTimer;

    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        food = transform.Find("Food").gameObject;
        happyObject = transform.Find("Happy").gameObject;
        sadObject = transform.Find("Sad").gameObject;

        hasFood = false;
        isBeingServed = false;

        entrance = GameObject.Find("Entrance").transform;
        exit = GameObject.Find("Exit").transform;
        person.transform.position = entrance.position;

        currentState = states.Entering;
    }

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

        if (isHappy == true)
        {
            happyObject.SetActive(true);
            sadObject.SetActive(false);
        }
        else
        {
            happyObject.SetActive(false);
            sadObject.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        switch (currentState)
        {
            case states.Entering:
                person.GetComponent<NavMeshAgent>().enabled = true;
                mySeat = findSeat();
                agent.destination = mySeat.position;

                if (mySeat.position != entrance.position)
                {
                    currentState = states.Sitting;
                }
                Debug.Log("I am Entering");
                break;

            case states.Sitting:
                if (isClose(person.transform.position, mySeat.position, 2.0f) == true)
                {
                    currentState = states.Waiting;
                }
                Debug.Log("I am Sitting");
                break;

            case states.Waiting:
                waitingTimer++;

                person.GetComponent<NavMeshAgent>().enabled = false;
                person.transform.position = new Vector3(mySeat.position.x, 1.15f, mySeat.position.z);

                if (waitingTimer > 2000)
                {
                    currentState = states.Leaving;
                    isHappy = false;
                    Debug.Log("I am not happy");
                    mySeat.gameObject.GetComponent<Seat>().isTaken = false;
                }

                if (hasFood == true)
                {
                    isHappy = true;
                    Debug.Log("I am happy");
                    currentState = states.Eating;
                }
                Debug.Log("I am Waiting");
                break;

            case states.Eating:
                eatingTimer++;

                if (eatingTimer > 1000)
                {
                    mySeat.gameObject.GetComponent<Seat>().isTaken = false;
                    hasFood = false;
                    currentState = states.Leaving;

                }

                Debug.Log("I am Eating");
                break;

            case states.Leaving:
                person.GetComponent<NavMeshAgent>().enabled = true;
                agent.destination = exit.position;
                if (isClose(person.transform.position, agent.destination, 3) == true)
                {
                    Destroy(person);
                }
                Debug.Log("I am leaving");
                break;
        }
    }

    public string getState()
    {
        string currentStateString = currentState.ToString();
        return currentStateString;
    }

    public void setFoodTrue()
    {
        hasFood = true;
    }

    Transform findSeat()
    {
        Transform newGoal = entrance;

        GameObject Seats = GameObject.Find("Seats");
        foreach (Transform child in Seats.transform)
        {
            if (child.GetComponent<Seat>().isTaken == false)
            {
                newGoal = child.transform;
                child.GetComponent<Seat>().isTaken = true;
                break;
            }
        }

        return newGoal;
    }
}