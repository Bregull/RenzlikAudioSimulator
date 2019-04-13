using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraSwitch : MonoBehaviour
{
    public Camera dontDestroyOnLoadCamera;
    public Camera cameraTwo;
    public bool cameraState = false;
    int selectedObjectIndex;
    GameObject selectedObject;
    Transform turnOnCamera;


    void FixedUpdate() //w przeciwieństwie do Update, fixedUpdate cały czas sprawdza w jakim stanie jest scena
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if(currentScene.name == "RenzlikAS")
        {
            ChangeCamera();
            selectedObjectIndex = GameObject.Find("ObjectCounter").GetComponent<ObjectCounter>().selectedObject;
            selectedObject = GameObject.Find("AudioController" + selectedObjectIndex);
            turnOnCamera = selectedObject.transform.GetChild(1);
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
                turnOnCamera.GetComponent<Camera>().enabled = true;
                //cameraTwo.enabled = true;
                cameraState = !cameraState;
            }
        }
    }
}
