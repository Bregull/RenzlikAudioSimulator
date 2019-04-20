using UnityEngine.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play_Stop : MonoBehaviour
{
    public Button stopPlayButton; // dodanie przycisku do sceny
    public Text buttonText; // zmienna pozwalająca na edycję tekstu wyświetlanego na przycisku
    int objectNumber; // ilość obiektów w scenie
    GameObject audioController; // służy do znajdowania obiektu AudioController
    Transform audioSource; // służy do wpływania na odtwarzanie źródła dźwięku
    bool stopState; // zmienna mówiąca o stanie odtwarzania dźwieków

    void Start()
    {
        stopPlayButton.onClick.AddListener(StopPlay); // przycisk "czeka" na input gracza
    }

    void Update()
    {
        objectNumber = GameObject.Find("ObjectCounter").GetComponent<ObjectCounter>().objectNumber; // przypisuje objectNumber liczbę równą liczbie obiektów w scenie
    }

    void StopPlay()
    {
        if (stopState == false)
        {
            for (int i = 1; i <= objectNumber; i++) // pętla kolejno zatrzymująca odtwarzania kolejnych źródeł dźwięku
            {
                audioController = GameObject.Find("AudioController" + i); // znajduje i-ty AudioController
                audioSource = audioController.transform.GetChild(0); // znajduje AudioSource przypisany do AudioControllera
                audioSource.GetComponent<AudioSource>().Stop(); // zatrzymuje odtwarzanie źródła dźwięku
            }
            stopState = true; // negacja zmiennej 
            buttonText.text = "PLAY"; // zmiana tekstu wyświetlanego na przycisku
        }
        else if (stopState == true)
        {
            for (int i = 1; i <= objectNumber; i++) // pętla kolejno zatrzymująca odtwarzania kolejnych źródeł dźwięku
            {
                audioController = GameObject.Find("AudioController" + i); // znajduje i-ty AudioController
                audioSource = audioController.transform.GetChild(0); // znajduje AudioSource przypisany do AudioControllera
                audioSource.GetComponent<AudioSource>().Play(); // odtwarza źródło dźwięku 
            }
            stopState = false; // negacja zmiennej 
            buttonText.text = "STOP"; // zmiana tekstu wyświetlanego na przycisku
        }

    }
}
