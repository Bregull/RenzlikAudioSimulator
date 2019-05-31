using UnityEngine.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play_Stop : MonoBehaviour
{
    public Button stopPlayButton; // dodanie przycisku do sceny
    public Text buttonText; // zmienna pozwalająca na edycję tekstu wyświetlanego na przycisku
    public Text audioClipNameText; // pole tekstowe 
    public Text audioClipDuration;
    GameObject audioController; // służy do znajdowania obiektu AudioController    
    GameObject selectedAudioController; // aktualnie aktywny AudioController
    Transform audioSource; // służy do wpływania na odtwarzanie źródła dźwięku
    int objectNumber; // ilość obiektów w scenie
    int selectedObject; // zmienna mówiąca nam o indeksie aktualnie wybranego AudioControllera
    int audioSourceLengthSec;
    int currentSec;
    int currentMin;
    string audioClipName; // string przechowujący nazwę odtwarzanego pliku audio
    bool stopState; // zmienna mówiąca o stanie odtwarzania dźwieków
    bool checkIfMuted; // zmienna mówiąca nam o tym czy AudioController jest wymutowany czy nie

    void Start()
    {
        stopPlayButton.onClick.AddListener(StopPlay); // przycisk "czeka" na input gracza
        ActiveSourcesCheck(); // funkcja sprawdzająca aktywne AudioControllery
        currentMin = 0;
    }

    void Update()
    {
        selectedObject = GameObject.Find("ObjectCounter").GetComponent<ObjectCounter>().selectedObject; // przypisuje selectedObject indeks aktywnego AudioControllera
        selectedAudioController = GameObject.Find("AudioController" + selectedObject); // selectedAudioController staje się aktywnym AudioControllerem
        audioClipNameText.text = selectedAudioController.GetComponent<Movement>().audioClipName; // przypisuje polu nazwę pliku podpiętego pod aktywny AudioController

        checkIfMuted = selectedAudioController.GetComponent<Movement>().isMuted; // sprawdza stan zmiennej isMuted, w celu ustalenia czy chcemy wyciszyć AudioController, czy nie

        if (checkIfMuted == true)
            audioClipNameText.text += "  IS MUTED"; // jeśli zmienna przyjmuje wartość true, to dopisujemy do pola tekstowego tekst "  IS MUTED"


        objectNumber = GameObject.Find("ObjectCounter").GetComponent<ObjectCounter>().objectNumber; // przypisuje objectNumber liczbę równą liczbie obiektów w scenie

        if (Input.GetKeyDown(KeyCode.M))
        {
            MuteObject(); // funkcja mutująca AudioControllery po naciśnieciu klawisza M
        }
        UpdateDuration();
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
                audioController.GetComponent<GetAudioAmplitude>().enabled = false; // wyłącza skrypt mrugania światłami
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
                audioController.GetComponent<GetAudioAmplitude>().enabled = true; // włącza skrypt mrugania światłami
            }
            stopState = false; // negacja zmiennej 
            buttonText.text = "STOP"; // zmiana tekstu wyświetlanego na przycisku
        }
    }

    void MuteObject()
    {
        audioSource = selectedAudioController.transform.GetChild(0);  // przypisuje audioSource obiekt z przypisanym do niego klipem audio
        if (checkIfMuted == false) // sprawdza stan boolowski zmiennej checkIfMuted
        {
            audioSource.GetComponent<AudioSource>().mute = true; // mutuje/wycisza wybrany AudioSource
            selectedAudioController.GetComponent<Movement>().isMuted = true; // zmienia stan boolowski zmiennej w skrypcie Movement
        }
        else if (checkIfMuted == true) // sprawdza stan boolowski zmiennej checkIfMuted
        {
            audioSource.GetComponent<AudioSource>().mute = false; // unmutuje/aktywuje wybrany AudioSource
            selectedAudioController.GetComponent<Movement>().isMuted = false;// zmienia stan boolowski zmiennej w skrypcie Movement
        }
        ActiveSourcesCheck(); // aktywuje funkcję ActiveSourcesCheck() sprawdzająca które AudioControllery aktualnie są wymutowane, a które nie
    }

    private void ActiveSourcesCheck()
    {
        for (int i = 1; i <= objectNumber; i++)
        {
            audioController = GameObject.Find("AudioController" + i); // przypisuje audioController obiekt AudioController z indeksem i
            bool checkIfMuted2 = audioController.GetComponent<Movement>().isMuted; // dodatkowa zmienna typu bool sprawdzająca czy aktywny AudioController jest wymutowany czy nie
            if (checkIfMuted2 == false)
            {
                audioController.GetComponent<Light>().enabled = true; // włacza podświetlanie AudioControllera
            }
            else
                audioController.GetComponent<Light>().enabled = false; // wyłacza podświetlanie AudioControllera
        }
    }

    private void UpdateDuration()
    {
        Transform getAudioSourceObject = selectedAudioController.transform.GetChild(0);
        AudioSource audioSourceToGetLength = getAudioSourceObject.GetComponent<AudioSource>();
        float lengthInSeconds = audioSourceToGetLength.clip.length;
        int minutes = (int)lengthInSeconds / 60;
        int sec = (int)lengthInSeconds - minutes * 60;

        currentSec = (int)audioSourceToGetLength.time % 60;

        currentMin = (int)audioSourceToGetLength.time / 60;

        if (currentMin == 0)
            audioClipDuration.text = currentSec + "sec  / " + minutes + "min " + sec + "sec";
        else
            audioClipDuration.text = currentMin +"min " + currentSec + "sec  / " + minutes + "min " + sec + "sec";
    }
}
