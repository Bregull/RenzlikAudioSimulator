using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Windows.Forms;

public class MainMenuPause : MonoBehaviour
{
    private static bool escPress = false; // zmienna odpowiadająca stanowi klawisza Esc
    public GameObject pauseMenu; // objekt, odpowiadjący panelowi menu pauzy
    public UnityEngine.UI.Button chooseNewFile; // przycisk umożliwiający wybranie nowego pliku audio (usuwa poprzedni)
    public UnityEngine.UI.Button addFile; // przycisk umożliwiający dodanie kolejnego pliku audio (nie usuwa poprzedniego)
    public UnityEngine.UI.Button exit; // dodanie przycisku exit do sceny
    public UnityEngine.UI.Button deleteFile;
    public UnityEngine.UI.Button resumeButton;
    int numberOfObjects; // zmienna zliczająca ilośc obiektów w scenie)
    int selectedObject;

   
    void Start()
    {
        AudioListener.pause = false; // przy każdym przejściu do sceny RenzlikAS umożliwia odtwarzanie plików audio zawartych w scenie
        numberOfObjects = GameObject.Find("ObjectCounter").GetComponent<ObjectCounter>().objectNumber; // przypisuje zmiennej numberOfObjects wartość równą liczbie obiektów w scenie
        pauseMenu.SetActive(false); // deaktywuje menuPauzy
        chooseNewFile.onClick.AddListener(OnClickNewFile); // przycisk czeka na input użytkownika
        addFile.onClick.AddListener(OnClickAddFile); // przycisk czeka na input użytkownika
        exit.onClick.AddListener(OnClickExit); // przycisk czeka na input użytkownika
        deleteFile.onClick.AddListener(OnClickDeleteFile);
        resumeButton.onClick.AddListener(OnClickResumeButton);
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
        selectedObject = GameObject.Find("ObjectCounter").GetComponent<ObjectCounter>().selectedObject;
        GameObject audioController = GameObject.Find("AudioController" + selectedObject); // znajduje najmłodszy audioControlller
        Destroy(audioController); // usuwa najmłodszy audioController

        for (int i = selectedObject + 1; i <= numberOfObjects; i++)
        {
            GameObject.Find("AudioController" + i).name = "AudioController" + (i - 1);
        }
        numberOfObjects--;
        for (int i = 1; i <= numberOfObjects; i++) // pętla wykona się tyle razy ile jest obiektów w scenie
        {
            GameObject turnOffCamera = GameObject.Find("AudioController" + i); // znajduje AudioController z indeksem 'i'
            turnOffCamera.GetComponent<CameraSwitch>().cameraState = false; // zmienia stan zmiennej cameraState ze skryptu CameraSwitch na false
            Transform cameraTarget = turnOffCamera.transform.GetChild(1); // dostęp do kamery zza obiektu
            cameraTarget.GetComponent<Camera>().enabled = false; // wyłącza kamerę zza obiektu
            Debug.Log("AudioController " + i);
        }
        escPress = false; // zamienia stan klawisza esc
        Time.timeScale = 1f; // przywraca czas do normalnej prędkości
        GameObject dontDestroyOnLoadCamera = GameObject.Find("DontDestroyOnLoadCamera"); // znajduje kamerę nad graczem
        Destroy(dontDestroyOnLoadCamera); // usuwa kamerę nad graczem (aby przy kolejnym wejściu w scenę się kamery nie dublowały)
        SceneManager.LoadScene("File Browser", LoadSceneMode.Single); // wraca do sceny File Browser
    }

    void OnClickAddFile()
    {
        for (int i = 1; i <= numberOfObjects; i++) // pętla wykona się tyle razy ile jest obiektów w scenie
        {
            GameObject turnOffCamera = GameObject.Find("AudioController" + i); // znajduje AudioController z indeksem 'i'
            turnOffCamera.GetComponent<CameraSwitch>().cameraState = false; // zmienia stan zmiennej cameraState ze skryptu CameraSwitch na false
            Transform cameraTarget = turnOffCamera.transform.GetChild(1); // dostęp do kamery zza obiektu
            cameraTarget.GetComponent<Camera>().enabled = false; // wyłącza kamerę zza obiektu
        }
        escPress = false; // zamienia stan klawisza esc
        Time.timeScale = 1f; // przywraca czas do normalnej prędkości 
        GameObject dontDestroyOnLoadCamera = GameObject.Find("DontDestroyOnLoadCamera"); // znajduje kamerę nad graczem
        Destroy(dontDestroyOnLoadCamera);   // usuwa kamerę nad graczem (aby przy kolejnym wejściu w scenę się kamery nie dublowały)
        SceneManager.LoadScene("File Browser", LoadSceneMode.Single);   // wraca do sceny File Browser
        GameObject.Find("ObjectCounter").GetComponent<ObjectCounter>().objectNumber++; // inkrementuje objectCounter, by przy wybraniu następnego źródła dźwięku przyjął indeks o jeden większy
    }

    void OnClickDeleteFile()
    {
        if (numberOfObjects > 1)
        {
            selectedObject = GameObject.Find("ObjectCounter").GetComponent<ObjectCounter>().selectedObject;
            GameObject audioController = GameObject.Find("AudioController" + selectedObject); // znajduje najmłodszy audioControlller
            Destroy(audioController); // usuwa najmłodszy audioController
            for (int i = selectedObject + 1; i <= numberOfObjects; i++)
            {
                GameObject.Find("AudioController" + i).name = "AudioController" + (i - 1); // przesuwa indeks każdego obiektu większego od usuniętego o jeden w dół
            }
            GameObject.Find("ObjectCounter").GetComponent<ObjectCounter>().objectNumber--; // zmniejsza zmienną naliczająca ilość obiektów w scenie o jeden
            numberOfObjects = GameObject.Find("ObjectCounter").GetComponent<ObjectCounter>().objectNumber; // przypisuje zmiennej lokalnej numberOfObjects wartość liczby obiektów w scenie
            GameObject.Find("ObjectCounter").GetComponent<ObjectCounter>().ChangePlayer(); // wywołuje funkcję zmiany obiektu ze skryptu ObjectCounter.cs
            Resume(); // wznawia grę
        }
        else
        {
            MessageBox.Show("There is only one object in the scene, try replacing it");
        }
    }

    void OnClickResumeButton()
    {
        AudioListener.pause = false; // uruchamia odtwarzanie dźwięków
        pauseMenu.SetActive(false); // wyłącza panel menu pauzy
        Time.timeScale = 1f; // przywraca czas do normalnej prędkości
        escPress = false; // zamienia stan klawisza esc
    }

    void OnClickExit()
    {
        escPress = false; // zmienia stan klawisza Esc
        Time.timeScale = 1f; // przywraca czas do normalnej prędkosci
        var allObjects = GameObject.FindGameObjectsWithTag("Player"); // tworzy listę wszystkich obiektów w scenie z tagiem 'Player'
        for(int i = 0; i < allObjects.Length; i++) // wykonuje sie tyle razy ile obiektów z tagiem 'Player' w scenie
        {
            Destroy(allObjects[i]); // usuwa każdy obiekt
        }
        GameObject dontDestroyOnLoadCamera = GameObject.Find("DontDestroyOnLoadCamera"); // znajduje kamerę nad graczem
        Destroy(dontDestroyOnLoadCamera);   // usuwa kamerę nad graczem (aby przy kolejnym wejściu w scenę się kamery nie dublowały)
        SceneManager.LoadScene("File Browser", LoadSceneMode.Single); // zamienia scenę na "File Browser"
        GameObject.Find("ObjectCounter").GetComponent<ObjectCounter>().objectNumber = 1; // przywraca zmiennej objectNumber ze skryptu ObjectCounter wartość 1
        GameObject.Find("ObjectCounter").GetComponent<ObjectCounter>().selectedObject = 1; // przywraca zmiennej selectedObject ze skryptu ObjectCounter wartość 1
    }
}
