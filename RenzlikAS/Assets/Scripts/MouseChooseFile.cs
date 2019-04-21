using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseChooseFile : MonoBehaviour
{
    Scene currentScene; // infomracja o aktualnej scenie
    string sceneName; // nazwa aktywnej sceny
    Camera dontDestroyOnLoadCamera; // kamera z widokiem znad głowy
    GameObject turnOffMovement; // obiekt, z którego później będziemy wyłączali, bądź włączali ruch
    Transform turnOffCamera; // transform służący do włączania / wyłączania kamery
    bool cameraState = false; // zmiena boolowska mówiąca o tym jaka kamera jest aktualnie aktywna


    void FixedUpdate()
    {
        currentScene = SceneManager.GetActiveScene(); // znajduje aktywną scenę
        sceneName = currentScene.name; // przypisuje zmiennej nazwę aktywnej sceny
        if (sceneName == "RenzlikAS") // jeśli nazwa sceny = "RenzlikAS"
        {
            dontDestroyOnLoadCamera = GameObject.FindWithTag("ObjectCamera").GetComponent<Camera>(); // znajduję kamerę znad głowy
            if (Input.GetMouseButtonDown(0)) // przy wciśnięciu lewego klawisza myszy
            {
                int objectNumber = GameObject.Find("ObjectCounter").GetComponent<ObjectCounter>().objectNumber; // znajduje liczbę obiektów w scenie
                RaycastHit hit; // zmienna typu RaycastHit
                Ray ray = dontDestroyOnLoadCamera.ScreenPointToRay(Input.mousePosition); // "cień" rzucony przez naciśnięcie myszy

                if (Physics.Raycast(ray, out hit)) // jeżeli cień rzucony przez zmienną 'ray' ląduje na obiekcie
                {
                    if (hit.rigidbody != null) // na obiekcie typu rigidbody
                    {
                        for (int i = 1; i <= objectNumber; i++) // pętla przechodząca przez ilość obiektów w scenie
                        {
                            if (hit.rigidbody.name == "AudioController" + i) // jeżeli nazwa oibektu na którego pada 'cień' pokrywa się z nazwą AudioControllera
                            {
                                ChangePlayer(i); // wykonaj metodę ChangePlayer z argumentem i
                            }
                        }
                    }
                }
            }
        }
    }

    void ChangePlayer(int selectedObject)
    {
        GameObject.Find("ObjectCounter").GetComponent<ObjectCounter>().selectedObject = selectedObject; // zmienia selectedObject ze skryptu ObjectCounter na wartość i z metody FixedUpdate
        int objectNumber = GameObject.Find("ObjectCounter").GetComponent<ObjectCounter>().objectNumber; // przypisuje objectNumber ilość obiektów w scenie

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
