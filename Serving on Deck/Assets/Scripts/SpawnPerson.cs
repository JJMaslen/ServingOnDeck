using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPerson : MonoBehaviour
{
    public GameObject Person;
    public static Transform entrance;

    int counter;
    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            Instantiate(Person,entrance);
        }
    }

    void FixedUpdate()
    {
        counter++;
        if (counter >= 120)
        {
            Instantiate(Person, entrance);
            counter = 0;
        }
    }
}