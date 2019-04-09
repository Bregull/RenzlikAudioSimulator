using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraSwitch : MonoBehaviour
{
    public Camera dontDestroyOnLoadCamera;
    public Camera cameraTwo;


    void FixedUpdate()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if(currentScene.name == "RenzlikAS")
        {
            ShowdontDestroyCameraOnLoadCamera();
            ShowCameraTwo();
        }

    }

    public void ShowdontDestroyCameraOnLoadCamera()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            dontDestroyOnLoadCamera.enabled = true;
            cameraTwo.enabled = false;
        }
       
    }
    public void ShowCameraTwo()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            dontDestroyOnLoadCamera.enabled = false;
            cameraTwo.enabled = true;
        }

    }
    
}
