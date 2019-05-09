using UnityEngine.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play_Stop : MonoBehaviour
{
    public Button stopPlayButton; // dodanie przycisku do sceny
    public Text buttonText; // zmienna pozwalająca na edycję tekstu wyświetlanego na przycisku
    int objectNumber; // ilość obiektów w scenie
    int selectedObject;
    GameObject audioController; // służy do znajdowania obiektu AudioController
    Transform audioSource; // służy do wpływania na odtwarzanie źródła dźwięku
    bool stopState; // zmienna mówiąca o stanie odtwarzania dźwieków
    string audioClipName;
    public Text audioClipNameText;
    GameObject selectedAudioController;
    bool checkIfMuted;

    void Start()
    {
        stopPlayButton.onClick.AddListener(StopPlay); // przycisk "czeka" na input gracza
        ActiveSourcesCheck();
    }

    void Update()
    {
        selectedObject = GameObject.Find("ObjectCounter").GetComponent<ObjectCounter>().selectedObject;
        selectedAudioController = GameObject.Find("AudioController" + selectedObject);
        audioClipNameText.text = selectedAudioController.GetComponent<Movement>().audioClipName;
        checkIfMuted = selectedAudioController.GetComponent<Movement>().isMuted;

        if (checkIfMuted == true)
            audioClipNameText.text += "  IS MUTED";

        objectNumber = GameObject.Find("ObjectCounter").GetComponent<ObjectCounter>().objectNumber; // przypisuje objectNumber liczbę równą liczbie obiektów w scenie

        if (Input.GetKeyDown(KeyCode.M))
        {
            MuteObject();
        }

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
                audioController.GetComponent<GetAudioAmplitude>().enabled = false;
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
                audioController.GetComponent<GetAudioAmplitude>().enabled = true;
            }
            stopState = false; // negacja zmiennej 
            buttonText.text = "STOP"; // zmiana tekstu wyświetlanego na przycisku
        }
    }

    void MuteObject()
    {
        if (checkIfMuted == false)
        {
            audioSource = selectedAudioController.transform.GetChild(0);
            audioSource.GetComponent<AudioSource>().mute = true;
            selectedAudioController.GetComponent<Movement>().isMuted = true;
        }
        else if (checkIfMuted == true)
        {
            audioSource = selectedAudioController.transform.GetChild(0);
            audioSource.GetComponent<AudioSource>().mute = false;
            selectedAudioController.GetComponent<Movement>().isMuted = false;
        }
        ActiveSourcesCheck();
    }

    void ActiveSourcesCheck()
    {
        for (int i = 1; i <= objectNumber; i++)
        {
            audioController = GameObject.Find("AudioController" + i);
            bool checkIfMuted2 = audioController.GetComponent<Movement>().isMuted;
            if (checkIfMuted2 == false)
            {
                audioController.GetComponent<Light>().enabled = true;
            }
            else
                audioController.GetComponent<Light>().enabled = false;
        }
    }
}
