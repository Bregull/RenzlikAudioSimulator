using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectCounter : MonoBehaviour
{
    public int objectNumber = 1; // deklaracja zmiennej, która będzie naliczała ile źródeł dźwięku znajduje się w scenie
    string sceneName; // nazwa sceny
    public int selectedObject; // obiekt, który aktualnie jest kontrolowany przez użytkownika
    private GameObject turnOffMovement; // obiekt, z którego później będziemy wyłączali, bądź włączali ruch
    Transform turnOffCamera; // transform służący do włączania / wyłączania kamery
    bool cameraState = false; // zmiena boolowska mówiąca o tym jaka kamera jest aktualnie aktywna

    void Start()
    {
        DontDestroyOnLoad(this); // przenosimy obiekt ObjectCounter do następnej sceny, w celu utrzymiana zmiennej objectNumber
        SceneManager.LoadScene("File Browser", LoadSceneMode.Single); // zmieniamy scenę na File Browser
        selectedObject = objectNumber; // przy starcie selectedObject przyjmuje wartość 1
    }

    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();  // znajduje aktywną scenę
        sceneName = currentScene.name; // otrzymujemy nazwę sceny w string

        if (sceneName == "RenzlikAS") // sprawdzamy, czy nazwa sceny to RenzlikAS, aby móc kontrolować obiekt tylko w tej scenie
        {
            if (Input.GetKeyDown(KeyCode.Tab))  // naciśnięcie klawisza Tab
            {
                selectedObject -= 1; // zmiejszamy selectedObject o jeden aby cyrklować po obiektach
                if (selectedObject == 0) // jeśli wynosi zero, to ponownie wracamy na wartość maksymalną
                {
                    selectedObject = objectNumber; // wartość maksymalna równa liczbie obiektów
                }
                for (int i = 1; i <= objectNumber; i++) // pętla wykonująca się tyle razy, ile jest obiektów w scenie
                {
                    //Debug.Log("kontroler nr " + i);
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
                        if ( cameraState == true ) // jeśli zmienna cameraState sugeruje, że kamera zza gracza powinna się aktywowac
                            turnOffCamera.GetComponent<Camera>().enabled = true; // aktywuje kamerę zza gracza
                    }
                }
            }
        }
    }
}
