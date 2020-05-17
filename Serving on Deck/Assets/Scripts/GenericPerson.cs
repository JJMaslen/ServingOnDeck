using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericPerson : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isClose(Vector3 a, Vector3 b, float threshold)
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
}
