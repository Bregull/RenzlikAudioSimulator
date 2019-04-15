using UnityEngine;
using UnityEngine.SceneManagement;

// ######### SKRYPT DO POPRAWY ##########

public class Movement : MonoBehaviour
{

    float x, z;
    public GameObject audioController;
    bool cameraState = false;

    public float speed;  // prędkość a dokładniej siła jaka zadziała na nasz obiekt
    private Rigidbody rigid; // odnośnik do komponentu Rigidbody
    string sceneName; // nazwa sceny
    float rotation;

    void Start()
    {
        
        rigid = audioController.GetComponent<Rigidbody>(); // przypisuje komponent Rigidbody z obiektu do zmiennej
    }

    void FixedUpdate()
    {
        cameraState = GetComponent<CameraSwitch>().cameraState;
        Scene currentScene = SceneManager.GetActiveScene(); // zmienna która sprawdza aktywną scenę
        sceneName = currentScene.name; // przypisanie nazwy sceny do stringa
        if (sceneName != "File Browser") // warunek sprawdzający nazwę sceny
        {
            float moveHorizontal = Input.GetAxis("Horizontal"); // sprawdza czy zostały wciśnięte strzałki prawo/lewo lub A/D
            float moveVertical = Input.GetAxis("Vertical"); // sprawdza czy zostały wciśnięte strzałki góra/dół lub W/S

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical); // dodaje nowy wektor (X,Z,Y), określający kierunek ruchu

            if (cameraState == true)
            {
                if (moveVertical > 0)
                {
                    transform.position += new Vector3(transform.forward.x * Time.deltaTime * speed, 0.0f, transform.forward.z * Time.deltaTime * speed);
                }
                else if(moveVertical < 0)
                {
                    transform.position += new Vector3(-transform.forward.x * Time.deltaTime * speed, 0.0f, -transform.forward.z * Time.deltaTime * speed);
                }
                if(Input.GetKey(KeyCode.LeftShift))
                {
                    transform.position += new Vector3(0.0f, speed * Time.deltaTime, 0.0f);
                }
                else if (Input.GetKey(KeyCode.LeftControl))
                {
                    transform.position += new Vector3(0.0f, -speed * Time.deltaTime, 0.0f);
                }
                if(moveHorizontal > 0)
                {
                    transform.position += new Vector3(transform.forward.z * Time.deltaTime * speed, 0.0f, -transform.forward.x * Time.deltaTime * speed);
                }
                else if (moveHorizontal < 0)
                {
                    transform.position += new Vector3(-transform.forward.z * Time.deltaTime * speed, 0.0f, transform.forward.x * Time.deltaTime * speed);
                }

            }
            else
            {
                transform.position += movement/2;
            }
        }
    }
}