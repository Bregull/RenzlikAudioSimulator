using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraSwitch : MonoBehaviour
{
    public Camera dontDestroyOnLoadCamera; 
    public Camera cameraTwo;


    void FixedUpdate() //w przeciwieństwie do Update, fixedUpdate cały czas sprawdza w jakim stanie jest scena
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if(currentScene.name == "RenzlikAS")
        {
            ShowdontDestroyCameraOnLoadCamera(); // uaktywnia kamery w scenie RenzlikAS
            ShowCameraTwo();
        }

    }

    public void ShowdontDestroyCameraOnLoadCamera() // Przenosimy kamerę ze sceny File Browser do sceny RenzlikAs
    {
        if (Input.GetKeyDown(KeyCode.V)) // Funkcja podporządkowująca literę "V" do włączenia kamery dontDestroyOnLoad i wyłączenia kamery cameraTwo przy naciśnięciu klawisza
        {
            dontDestroyOnLoadCamera.enabled = true; // jak naciśniemy "V" to aktywujemy kamere dontDestroyOnLoad
            cameraTwo.enabled = false; // jak naciśniemy "V" to dezaktywujemy kamere cameraTwo
        }
       
    }
    public void ShowCameraTwo() // jak wyżej
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            dontDestroyOnLoadCamera.enabled = false;
            cameraTwo.enabled = true;
        }

    }
    
}
