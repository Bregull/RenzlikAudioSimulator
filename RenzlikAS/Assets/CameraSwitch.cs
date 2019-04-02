using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{

    public GameObject mainCamera;
    public GameObject cameraTwo;



    void Start()
    {

        cameraPositionChange(PlayerPrefs.GetInt("CameraPosition"));
    }


    void Update()
    {
  
        switchCamera();
    }

  
    public void cameraPositonM()
    {
        cameraChangeCounter();
    }

  
    void switchCamera()
    {
        if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
        {
            cameraChangeCounter();
        }
    }

   
    void cameraChangeCounter()
    {
        int cameraPositionCounter = PlayerPrefs.GetInt("CameraPosition");
        cameraPositionCounter++;
        cameraPositionChange(cameraPositionCounter);
    }

   
    void cameraPositionChange(int camPosition)
    {
        if (camPosition > 1)
        {
            camPosition = 0;
        }

        
        PlayerPrefs.SetInt("CameraPosition", camPosition);

       
        if (camPosition == 0)
        {
            mainCamera.SetActive(true);
            cameraTwo.SetActive(false);
        }

        
        if (camPosition == 1)
        {
            cameraTwo.SetActive(true);
            mainCamera.SetActive(false);
        }

    }
}
