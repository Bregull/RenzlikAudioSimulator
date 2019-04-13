using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraSwitch : MonoBehaviour
{
    public Camera dontDestroyOnLoadCamera;
    public Camera cameraTwo;
    public bool cameraState = false;


    void FixedUpdate() //w przeciwieństwie do Update, fixedUpdate cały czas sprawdza w jakim stanie jest scena
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if(currentScene.name == "RenzlikAS")
        {
            ChangeCamera();
        }

    }

    public void ChangeCamera()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            if (cameraState == true)
            {
                dontDestroyOnLoadCamera = GameObject.FindWithTag("ObjectCamera").GetComponent<Camera>();
                dontDestroyOnLoadCamera.enabled = true;
                cameraTwo.enabled = false;
                cameraState = !cameraState;
            }
            else
            {
                dontDestroyOnLoadCamera = GameObject.FindWithTag("ObjectCamera").GetComponent<Camera>();
                dontDestroyOnLoadCamera.enabled = false;
                cameraTwo.enabled = true;
                cameraState = !cameraState;
            }
        }
    }
}
