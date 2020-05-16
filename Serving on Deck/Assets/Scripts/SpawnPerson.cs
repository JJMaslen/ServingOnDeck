using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPerson : MonoBehaviour
{
    public GameObject Person;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            Instantiate(Person);
        }
    }
}