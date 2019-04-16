using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseChooseFile : MonoBehaviour
{
    bool objectClick;
    Scene currentScene;
    string sceneName;
    Camera dontDestroyOnLoadCamera;
    GameObject turnOffMovement;
    Transform turnOffCamera; // transform służący do włączania / wyłączania kamery
    bool cameraState = false; // zmiena boolowska mówiąca o tym jaka kamera jest aktualnie aktywna


    void FixedUpdate()
    {
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        if (sceneName == "RenzlikAS")
        {
            dontDestroyOnLoadCamera = GameObject.FindWithTag("ObjectCamera").GetComponent<Camera>();
            if (Input.GetMouseButtonDown(0))
            {
                int objectNumber = GameObject.Find("ObjectCounter").GetComponent<ObjectCounter>().objectNumber;
                RaycastHit hit;
                Ray ray = dontDestroyOnLoadCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.rigidbody != null)
                    {
                        for (int i = 1; i <= objectNumber; i++)
                        {
                            if (hit.rigidbody.name == "AudioController" + i)
                            {
                                ChangePlayer(i);
                            }
                        }
                    }
                }
            }
        }
    }

    void ChangePlayer(int selectedObject)
    {
        GameObject.Find("ObjectCounter").GetComponent<ObjectCounter>().selectedObject = selectedObject;
        int objectNumber = GameObject.Find("ObjectCounter").GetComponent<ObjectCounter>().objectNumber;

        for (int i = 1; i <= objectNumber; i++) // pętla wykonująca się tyle razy, ile jest obiektów w scenie
        {
            turnOffMovement = GameObject.Find("AudioController" + i); // obiektowi turnOff przypisujemy i-ty AudioController
            Renderer color = turnOffMovement.GetComponent<Renderer>(); // przypisuje zmiennej color komponent redera dla obiektu
            turnOffCamera = turnOffMovement.transform.GetChild(1); // przypisuje turnOffCamera kamerę zza obiektu
            cameraState = turnOffMovement.GetComponent<CameraSwitch>().cameraState; // przypisuje cameraState zmienną cameraState ze skryptu CameraSwitch
            if (i != selectedObject) // dla każdego audio controllera innego niż selectedObject, którego zmieniamy za pomocą Tab
            {
                color.material.SetColor("_Color", Color.red); // jeśli obiekty nie jest aktywny zmienia kolor na czerwony
                turnOffMovement.GetComponent<Movement>().enabled = false; // wyłączamy skrypt Movement odpowiadający za poruszanie się
                turnOffCamera.GetComponent<Camera>().enabled = false; // jeśli obiekt jest nieaktywny, to wyłącza kamerę zza gracza
            }
            else
            {
                color.material.SetColor("_Color", Color.green); // jelsi obiekt jest aktywny to zmienia kolor na zielony
                turnOffMovement.GetComponent<Movement>().enabled = true; // włączamy skrypt Movement odpowiadający za poruszanie się
                if (cameraState == true) // jeśli zmienna cameraState sugeruje, że kamera zza gracza powinna się aktywowac
                    turnOffCamera.GetComponent<Camera>().enabled = true; // aktywuje kamerę zza gracza
            }
        }
    }
}
