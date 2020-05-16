using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    float speedH;
    float speedV;

    float yaw;
    float pitch;

    float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        speedH = 2.0f;
        speedV = 2.0f;

        yaw = 0.0f;
        pitch = 0.0f;

        movementSpeed = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        FirstPersonCamera();
        UpdatePosition();
    }

    void FirstPersonCamera()
    {
        Cursor.lockState = CursorLockMode.Locked;

        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        pitch = Mathf.Clamp(pitch, -90f, 90f);

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }

    void UpdatePosition()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += this.transform.forward * movementSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= this.transform.forward * movementSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= this.transform.right * movementSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += this.transform.right * movementSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            transform.position += this.transform.up * movementSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.E))
        {
            transform.position -= this.transform.up * movementSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = 20.0f;
        }
        else
        {
            movementSpeed = 5.0f;
        }
    }
}