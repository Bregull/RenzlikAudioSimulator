using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    

    private Vector2 rotation = Vector2.zero; // pozwala nam wykonywać rotacje wokół obiektu z offsetem równym (0,0)
    public float lookSpeed = 3; // prędkość ruszania kamerą wokół obiektu

    private void FixedUpdate()  
    {
        if (Input.GetMouseButton(1)) // warunek- wciśnięcie prawego klawisza myszy
        {
            rotation.y += Input.GetAxis("Mouse X");
            rotation.x += -Input.GetAxis("Mouse Y");
            rotation.x = Mathf.Clamp(rotation.x, -15f, 15f);//clamp zwraca wartosc value jeżeli znajduje się w przedziale (float value, float min, float max)
            transform.localRotation = Quaternion.Euler(rotation.x * lookSpeed, rotation.y * lookSpeed, 0); //obraca kamere wokół osi x i y, quaternion representuje rotacje 
        }
    }
    


}
