using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServingTable : MonoBehaviour
{
    public bool inUse;
    public bool hasFood;

    int counter;
    // Start is called before the first frame update
    void Start()
    {
        hasFood = true;
        inUse = false;

        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (inUse == true)
        {
            counter++;
            if (counter > 120)
            {
                counter = 0;
                inUse = false;
            }
        }
    }
}
