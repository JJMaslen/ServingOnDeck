using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Person : MonoBehaviour
{
    public GameObject person;
    public Transform mySeat;

    public Transform entrance;
    public Transform exit;

    enum states { Entering, Eating, Leaving}
    states currentState;

    int eatingTime;

    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();      
        currentState = states.Entering;

        entrance = GameObject.Find("Entrance").transform;
        exit = GameObject.Find("Exit").transform;
        person.transform.position = entrance.position;

        eatingTime = 0;
    }

    // Update is called once per frame
    void Update()
    {

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
                    currentState = states.Eating;
                }
                Debug.Log("I am Entering");
                break;

            case states.Eating:
                eatingTime++;
                if (eatingTime > 1000)
                {
                    currentState = states.Leaving;
                    mySeat.gameObject.GetComponent<Seat>().isTaken = false;
                }
                Debug.Log("I am Eating");
                break;

            case states.Leaving:
                agent.destination = exit.position;
                if (person.transform.position.x == exit.position.x && person.transform.position.z == exit.position.z)
                {
                    Destroy(person);
                }
                Debug.Log("I am leaving");
                break;
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