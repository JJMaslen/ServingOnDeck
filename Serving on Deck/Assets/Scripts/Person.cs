using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Person : MonoBehaviour
{
    public GameObject person;
    public Transform mySeat;

    public static Transform entrance;
    public static Transform exit;

    enum states { Entering, Sitting, Waiting, Eating, Leaving}
    states currentState;

    bool isHappy;
    int waitingTimer;
    int eatingTimer;

    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();      
        currentState = states.Entering;

        entrance = GameObject.Find("Entrance").transform;
        exit = GameObject.Find("Exit").transform;
        person.transform.position = entrance.position;
        waitingTimer = 0;
    }

    void FixedUpdate()
    {
        switch (currentState)
        {
            case states.Entering:
                mySeat = findSeat();
                agent.destination = mySeat.position;

                if (mySeat.position != entrance.position)
                {
                    currentState = states.Sitting;
                }
                Debug.Log("I am Entering");
                break;

            case states.Sitting:
                if (isClose(person.transform.position, mySeat.position, 1.5f) == true)
                {
                    currentState = states.Eating;
                }
                Debug.Log("I am Sitting");
                break;

            case states.Waiting:

                Debug.Log("I am Waiting");
                break;

            case states.Eating:
                waitingTimer++;
     
                if (waitingTimer > 1)
                {
                    currentState = states.Leaving;
                    mySeat.gameObject.GetComponent<Seat>().isTaken = false;
                }
                Debug.Log("I am Eating");
                break;

            case states.Leaving:
                agent.destination = exit.position;
                if (isClose(person.transform.position, agent.destination, 3) == true)
                {
                    Destroy(person);
                }
                Debug.Log("I am leaving");
                break;
        }
    }

    bool isClose(Vector3 a, Vector3 b, float threshold)
    {
        float distance = Vector3.Distance(a, b);
        if (distance < threshold)
        {
            return true;
        }
        else
        {
            return false;
        }
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