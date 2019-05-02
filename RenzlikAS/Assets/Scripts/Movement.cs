using UnityEngine;
using UnityEngine.SceneManagement;


public class Movement : MonoBehaviour
{

    bool cameraState = false; // tworzy zmienną boolowską która nam mówi o tym jaka kamera jest aktywna
    public float speed;  // prędkość a dokładniej siła jaka zadziała na nasz obiekt
    string sceneName; // nazwa sceny
    public string audioClipName;
    public bool isMuted = false;

    void FixedUpdate()
    {
        cameraState = GetComponent<CameraSwitch>().cameraState; // przypisuje zmiennej cameraState zmienną cameraState ze skryptu CameraSwitch
        Scene currentScene = SceneManager.GetActiveScene(); // zmienna która sprawdza aktywną scenę
        sceneName = currentScene.name; // przypisanie nazwy sceny do stringa
        if (sceneName != "File Browser") // warunek sprawdzający nazwę sceny
        {
            float moveHorizontal = Input.GetAxis("Horizontal"); // sprawdza czy zostały wciśnięte strzałki prawo/lewo lub A/D
            float moveVertical = Input.GetAxis("Vertical"); // sprawdza czy zostały wciśnięte strzałki góra/dół lub W/S

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical); // dodaje nowy wektor (X,Z,Y), określający kierunek ruchu

            if (cameraState == true)
            {
                if (moveVertical > 0) // gdy wartość moveVertical jest dodatnia -> przycisk W / strzałka w górę
                {
                    transform.position += new Vector3(transform.forward.x * Time.deltaTime * speed, 0.0f, transform.forward.z * Time.deltaTime * speed); // zmienia pozycję obiektu o wektor o wartościach podanych w nawiasach
                }                                                                                                                                       // transform.forward słuzy do przyjęcia nowego układu współrzędnych dla obiektu, gdzie x,y,z = 0
                else if(moveVertical < 0)                                                                                                              // Time.deltaTime -> w zależności od tego jak długo trzymamy klawisz
                {
                    transform.position += new Vector3(-transform.forward.x * Time.deltaTime * speed, 0.0f, -transform.forward.z * Time.deltaTime * speed); // odwraca znak x oraz z -> obrót o 180 stopni
                }
                if(Input.GetKey(KeyCode.Space))
                {
                    transform.position += new Vector3(0.0f, speed * Time.deltaTime, 0.0f); // poruszanie w górę
                }
                else if (Input.GetKey(KeyCode.LeftControl))
                {
                    transform.position += new Vector3(0.0f, -speed * Time.deltaTime, 0.0f); // poruszanie w dół
                }
                if(moveHorizontal > 0)
                {
                    transform.position += new Vector3(transform.forward.z * Time.deltaTime * speed, 0.0f, -transform.forward.x * Time.deltaTime * speed); // zamienia tylko z -> obrót o 90 stopni
                }
                else if (moveHorizontal < 0)
                {
                    transform.position += new Vector3(-transform.forward.z * Time.deltaTime * speed, 0.0f, transform.forward.x * Time.deltaTime * speed); // zamienia tylko x -> obrót o 270 stopni
                }

            }
            else
            {
                transform.position += movement/2; // ruch dla kamery znad głowy
            }
        }
    }
}