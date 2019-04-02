using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{

    public GameObject mainCamera;
    public GameObject cameraTwo;



    void Start()
    {

        CameraPositionChange(PlayerPrefs.GetInt("CameraPosition"));
    }


    void Update()
    {
  
        SwitchCamera();
    }

  
    public void CameraPositonM()
    {
        CameraChangeCounter();
    }

  
    void SwitchCamera()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            CameraChangeCounter();
        }
    }

   
    void CameraChangeCounter()
    {
        int CameraPositionCounter = PlayerPrefs.GetInt("CameraPosition");
        CameraPositionCounter++;
        CameraPositionChange(CameraPositionCounter);
    }

   
    void CameraPositionChange(int CamPosition)
    {
        if (CamPosition > 1)
        {
            CamPosition = 0;
        }

        
        PlayerPrefs.SetInt("CameraPosition", CamPosition);

       
        if (CamPosition == 0)
        {
            mainCamera.SetActive(true);
            cameraTwo.SetActive(false);
        }

        
        if (CamPosition == 1)
        {
            cameraTwo.SetActive(true);
            mainCamera.SetActive(false);
        }

    }
}
