using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MouseLook : MonoBehaviour
{


    public float lookSpeed = 3; // prędkość ruszania kamerą wokół obiektu
    Vector3 objectPosition;
    Vector3 listenerPosition;
    Quaternion rotate;
    Vector3 lookVector;
    Vector3 mouseInput = Vector3.zero;
    Vector3 cameraDirection;
    Vector2 look = Vector2.zero;


    void Start()
    {
        objectPosition = transform.position;
        listenerPosition = new Vector3(0, 0, -30);
        lookVector = objectPosition - listenerPosition;
        rotate = Quaternion.LookRotation(-lookVector, Vector3.up);
        transform.rotation = rotate;
    }

    private void FixedUpdate()
    {

        objectPosition = transform.position;
        listenerPosition = new Vector3(0, 0, -30);
        lookVector = objectPosition - listenerPosition;
        rotate = Quaternion.LookRotation(-lookVector, Vector3.up);
        transform.rotation = rotate;

/*
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            objectPosition = transform.position;
            listenerPosition = new Vector3(0, 0, -30);
            lookVector = objectPosition - listenerPosition;
            rotate.SetLookRotation(-lookVector, Vector3.up);
            transform.rotation = rotate;
        }

        if (Input.GetMouseButton(1)) // warunek- wciśnięcie prawego klawisza myszy
        {
            lookVector.x += Input.GetAxis("Mouse X");
            lookVector.y += Input.GetAxis("Mouse Y");

            rotate = Quaternion.Euler(lookVector);

            //rotate.SetLookRotation(-lookVector, Vector3.up);
            transform.rotation = rotate;
        }

        Debug.Log(rotate); */
    }
}


/* if (Input.GetMouseButton(1)) // warunek- wciśnięcie prawego klawisza myszy
 {
     rotate.y += Input.GetAxis("Mouse X");
      rotate.x += -Input.GetAxis("Mouse Y");
      rotate.x = Mathf.Clamp(rotate.x, -15f, 15f);//clamp zwraca wartosc value jeżeli znajduje się w przedziale (float value, float min, float max)
      transform.localRotation = Quaternion.Euler(rotate.x * lookSpeed, rotate.y * lookSpeed, 0); //obraca kamere wokół osi x i y, quaternion representuje rotacje  


     Vector3 rotation = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);
     lookVector += new Vector3(rotation.x, rotation.y, -rotation.z);
     rotate.SetLookRotation(-lookVector, Vector3.up);

     //transform.rotation = Quaternion.Euler(-lookVector.x * (float)Math.PI, -lookVector.z * (float)Math.PI, 0);//-lookVector.z * (float)Math.PI
     transform.rotation.SetLookRotation(-lookVector, Vector3.up);

     Debug.Log(transform.localRotation);



 } */
