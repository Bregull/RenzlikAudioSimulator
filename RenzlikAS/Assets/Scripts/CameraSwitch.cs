using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraSwitch : MonoBehaviour
{
    public Camera dontDestroyOnLoadCamera; // kamera z widokiem ponad głową
    public Camera cameraTwo; // kamera zza pleców
    public bool cameraState = false; // stanKamery --> false - nad głową, true - zza pleców
    int selectedObjectIndex; // numer/indeks wybranego obiektu
    GameObject selectedObject; // wybrany obiekt
    Transform turnOnCamera; // transform służący do włączania lub wyłączania kamery


    void FixedUpdate() //w przeciwieństwie do Update, fixedUpdate cały czas sprawdza w jakim stanie jest scena
    {
        Scene currentScene = SceneManager.GetActiveScene(); // sprawdza w jakiej scenie się znajdujemy
        if(currentScene.name == "RenzlikAS")
        {
            ChangeCamera(); // funkcja zmieniająca aktywną kamerę
            selectedObjectIndex = GameObject.Find("ObjectCounter").GetComponent<ObjectCounter>().selectedObject; // w obiekcie ObjectCounter znajdujemy selectedObject i zapisujemy do zmiennej
            selectedObject = GameObject.Find("AudioController" + selectedObjectIndex); // przypisuje zmiennej selectedObject AudioController z odpowiednim indeksem
            turnOnCamera = selectedObject.transform.GetChild(1); // dostęp do kamery przypiętej do AudioControllera
        }
    }

    public void ChangeCamera()
    {
        if(Input.GetKeyDown(KeyCode.C)) 
        {
            if (cameraState == true)
            {
                dontDestroyOnLoadCamera = GameObject.FindWithTag("ObjectCamera").GetComponent<Camera>(); // znajduje w scenie kamerę znad głowy
                dontDestroyOnLoadCamera.enabled = true; // włącza kamerę znad głowy
                cameraTwo.enabled = false; // wyłacza kamerę zza obiektu
                cameraState = false; // zamienia stan zmiennej cameraState
            }
            else
            {
                dontDestroyOnLoadCamera = GameObject.FindWithTag("ObjectCamera").GetComponent<Camera>(); // znajduje w scenie kamerę znad głowy
                dontDestroyOnLoadCamera.enabled = false; // wyłącza kamerę znad głowy
                turnOnCamera.GetComponent<Camera>().enabled = true; // włącza kamerę zza obiektu
                cameraState = true; // zamienia stan zmiennej cameraState
            }
        }
    }
}
