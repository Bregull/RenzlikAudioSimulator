using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuPause : MonoBehaviour
{
    private static bool escPress = false; // zmienna odpowiadająca stanowi klawisza Esc
    public GameObject pauseMenu; // objekt, odpowiadjący panelowi menu pauzy
    public Button chooseNewFile; // przycisk umożliwiający wybranie nowego pliku audio (usuwa poprzedni)
    public Button addFile; // przycisk umożliwiający dodanie kolejnego pliku audio (nie usuwa poprzedniego)
    public Button exit;
    int numberOfObjects; // zmienna zliczająca ilośc obiektów w scenie)

   
    void Start()
    {
        AudioListener.pause = false; // przy każdym przejściu do sceny RenzlikAS umożliwia odtwarzanie plików audio zawartych w scenie
        numberOfObjects = GameObject.Find("ObjectCounter").GetComponent<ObjectCounter>().objectNumber; // przypisuje zmiennej numberOfObjects wartość równą liczbie obiektów w scenie
        pauseMenu.SetActive(false); // deaktywuje menuPauzy
        chooseNewFile.onClick.AddListener(OnClickNewFile); // przycisk czeka na input użytkownika
        addFile.onClick.AddListener(OnClickAddFile); // przycisk czeka na input użytkownika
        exit.onClick.AddListener(OnClickExit);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) // w momencie wciśnięcia klawisza Esc
        {
            if (escPress == false) // gdy esc nie był wcześniej wciśnięty
            {
                Pause(); // uruchamia funkcję pauzowania
            }
            else
            {
                Resume(); // uruchamia funkcję wznowienia
            }
        }
    }

    void Pause()
    {
        AudioListener.pause = true; // pauzuje odtwarzanie dźwięków
        pauseMenu.SetActive(true); // aktywuje panel menu pauzy
        Time.timeScale = 0f; // zatrzymuje czas
        escPress = true; // zamienia stan klawisza Esc
    }

    void Resume()
    {
        AudioListener.pause = false; // uruchamia odtwarzanie dźwięków
        pauseMenu.SetActive(false); // wyłącza panel menu pauzy
        Time.timeScale = 1f; // przywraca czas do normalnej prędkości
        escPress = false; // zamienia stan klawisza esc
    }

    void OnClickNewFile() // gdy kilkniemy w przycisk ChooseNewFile
    {
        escPress = false; // zamienia stan klawisza esc
        Time.timeScale = 1f; // przywraca czas do normalnej prędkości
        GameObject audioController = GameObject.Find("AudioController" + numberOfObjects); // znajduje najmłodszy audioControlller
        Destroy(audioController); // usuwa najmłodszy audioController
        GameObject dontDestroyOnLoadCamera = GameObject.Find("DontDestroyOnLoadCamera"); // znajduje kamerę nad graczem
        Destroy(dontDestroyOnLoadCamera); // usuwa kamerę nad graczem (aby przy kolejnym wejściu w scenę się kamery nie dublowały)
        SceneManager.LoadScene("File Browser", LoadSceneMode.Single); // wraca do sceny File Browser
    }

    void OnClickAddFile()
    {
        for(int i = 1; i <= numberOfObjects; i++)
        {
            GameObject turnOffCamera = GameObject.Find("AudioController" + i);
            Transform cameraTarget = turnOffCamera.transform.GetChild(1);
            cameraTarget.GetComponent<Camera>().enabled = false;
        }
        escPress = false; // zamienia stan klawisza esc
        Time.timeScale = 1f; // przywraca czas do normalnej prędkości 
        GameObject dontDestroyOnLoadCamera = GameObject.Find("DontDestroyOnLoadCamera"); // znajduje kamerę nad graczem
        Destroy(dontDestroyOnLoadCamera);   // usuwa kamerę nad graczem (aby przy kolejnym wejściu w scenę się kamery nie dublowały)
        SceneManager.LoadScene("File Browser", LoadSceneMode.Single);   // wraca do sceny File Browser
        GameObject.Find("ObjectCounter").GetComponent<ObjectCounter>().objectNumber++; // inkrementuje objectCounter, by przy wybraniu następnego źródła dźwięku przyjął indeks o jeden większy
    }

    void OnClickExit()
    {
        escPress = false;
        Time.timeScale = 1f;
        var allObjects = GameObject.FindGameObjectsWithTag("Player");
        for(int i = 0; i < allObjects.Length; i++)
        {
            Destroy(allObjects[i]);
        }
        GameObject dontDestroyOnLoadCamera = GameObject.Find("DontDestroyOnLoadCamera"); // znajduje kamerę nad graczem
        Destroy(dontDestroyOnLoadCamera);   // usuwa kamerę nad graczem (aby przy kolejnym wejściu w scenę się kamery nie dublowały)
        SceneManager.LoadScene("File Browser", LoadSceneMode.Single);
        GameObject.Find("ObjectCounter").GetComponent<ObjectCounter>().objectNumber = 1;
    }
}
