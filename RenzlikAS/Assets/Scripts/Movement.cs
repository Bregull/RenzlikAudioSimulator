using UnityEngine;
using UnityEngine.SceneManagement;

// ######### SKRYPT DO POPRAWY ##########

public class Movement : MonoBehaviour
{

    public float speed;  // prędkość a dokładniej siła jaka zadziała na nasz obiekt
    private Rigidbody rigid; // odnośnik do komponentu Rigidbody
    string sceneName; // nazwa sceny

    void Start()
    {
        rigid = GetComponent<Rigidbody>(); // przypisuje komponent Rigidbody z obiektu do zmiennej
    }

    void Update()
    {
        
        Scene currentScene = SceneManager.GetActiveScene(); // zmienna która sprawdza aktywną scenę
        sceneName = currentScene.name; // przypisanie nazwy sceny do stringa
        if (sceneName != "File Browser") // warunek sprawdzający nazwę sceny
        {
            //checkKey();

            float moveHorizontal = Input.GetAxis("Horizontal"); // sprawdza czy zostały wciśnięte strzałki prawo/lewo lub A/D
            float moveVertical = Input.GetAxis("Vertical"); // sprawdza czy zostały wciśnięte strzałki góra/dół lub W/S

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical); // dodaje nowy wektor (X,Z,Y), określający kierunek ruchu

            rigid.AddForce(movement * speed); // dodaje siłę zgodnie ze zmienną speed oraz o kierunku wektora
        }
    }
}