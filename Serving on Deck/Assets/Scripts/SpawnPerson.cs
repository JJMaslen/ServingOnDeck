using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPerson : MonoBehaviour
{
    public GameObject Person;
    public static Transform People;
    public static Transform entrance;

    int counter;
    // Start is called before the first frame update
    void Start()
    {
        People = GameObject.Find("People").transform;
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            GameObject newPerson = Instantiate(Person, entrance, People);
            newPerson.transform.parent = People;
        }
    }

    void FixedUpdate()
    {
        counter++;
        if (counter >= 120)
        {
            GameObject newPerson = Instantiate(Person, entrance, People);
            newPerson.transform.parent = People;
            counter = 0;
        }
    }
}