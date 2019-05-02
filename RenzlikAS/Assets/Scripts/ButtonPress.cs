using UnityEngine;
using System.Collections;
using UnityEngine.Networking;  // pozwala nam na streamowanie muzyki z komputera (lub URL - do sprawdzenia) do naszego projektu Unity
using UnityEngine.UI;  // obsługa guzika
using UnityEngine.SceneManagement;  // pozwala nam zarządzać scenami projektu
using SFB; // implementacja Standalone File Browsera
using System.Windows.Forms; // potrzebne do wyświetlania error message


public class ButtonPress : MonoBehaviour
{
    string[] filePath; // zapisane w tej formie, gdyż Standalone File Browser zwraca nam tablicę string
    string filePath2, fileExtension;  // filePath2 służy do zamiany tablicy string filePath2 na zwykły string, fileExtension rozpoznaje rozszerzenie pliku audio
    public UnityEngine.UI.Button chooseFile;  // dodajemy przycisk do sceny
    public UnityEngine.UI.Button exit; // dodajemy przycisk do sceny
    public AudioClip audioClipSelected;  // zapisanie wybranego przez nas utworu muzycznego do zmiennej
    public AudioSource audioSource; // potrzebne do odtworzenia naszego utworu muzycznego
    public GameObject audioController; // obiekt do którego jest przypisane źródło dźwięku
    public Camera dontDestroyOnLoadCamera; // kamera która zostanie przeniesiona do drugiej sceny
    public Camera cameraTwo; // kamera podążająca za obiektem
    public string audioClipName;

    void Start()
    {
        GameObject.Find("AudioController").GetComponent<Movement>().enabled = true; // aktywuje skrypt umozliwiający poruszanie się AudioControllera (potrzebne przy dodawaniu kilku obiektów do sceny)
        int numberOfObjects = GameObject.Find("ObjectCounter").GetComponent<ObjectCounter>().objectNumber; // ilość obiektów do pętli poniżej
        if (numberOfObjects > 1) // pętla zmieniająca stan cameraState -> pojawiały się bugi ### ZNAJDŹ PRZYCZYNĘ
        {
            for (int i = 1; i < numberOfObjects; i++)
            {
                GameObject.Find("AudioController" + i).GetComponent<CameraSwitch>().cameraState = false; // zmienia cameraState na false -> ZNAJDŹ PRZYCZYNĘ
            }
        }
        DontDestroyOnLoad(audioController); // przeniesienie obiektu do drugiej sceny
        DontDestroyOnLoad(dontDestroyOnLoadCamera); // przeniesienie kamery do drugiej sceny
        chooseFile.onClick.AddListener(OnClick); // button "czeka" na naciśnięcie przez użytkownika, oraz po naciśnięciu wykonuje metodę On_Click  
        exit.onClick.AddListener(ExitGame); // button "czeka" na naciśnięcie przez użytkownika, oraz po naciśnięciu wykonuje metodę ExitGame
    }

    void OnClick()
    {
        SelectFile(); // instrukcje po naciśnięciu buttona
        StartCoroutine(UploadAudioFile()); //metoda pobierająca oraz odtwarzająca wybraną ścieżkę audio
    }

    public void SelectFile()
    {
        filePath = StandaloneFileBrowser.OpenFilePanel("Wybierz utwór", "", "", false);  // otwiera okno przeglądania plików i zapisuje jako tablica string
        filePath2 = string.Concat(filePath); // konwersja na string
        fileExtension = filePath2.Substring(filePath2.IndexOf('.') + 1); // pozwala nam określić rozszerzenie pliku poprzez znalezienie kropki w nazwie pliku, oraz zwrócenie nam wszystkiego co się znajduje za nią
        fileExtension = fileExtension.ToLower(); // zamiana rozszerzenia na małe litery
        audioClipName = filePath2.Substring(filePath2.LastIndexOf("\\") + 1);
        while (fileExtension != "wav" && fileExtension != "ogg") // pętla sprawdzająca rozszerzenie pliku -> DZIAŁA: Wav, Ogg; NIE DZIAŁA: Mp3, Flac
        {
            if (fileExtension == "")  // przerywa działanie pętli w momencie, gdy rozszerzenie pliku zwraca pustgo stringa.
            {
                break; // kończy pętlę
            }
            MessageBox.Show("Nieobsługiwany format pliku.\nWybierz jeszcze raz."); // error
            
            filePath = StandaloneFileBrowser.OpenFilePanel("Wybierz utwór", "", "", false); // ponowny wybór pliku
            filePath2 = string.Concat(filePath); // ponowna konwersja
            fileExtension = filePath2.Substring(filePath2.IndexOf('.') + 1); // ponowna analiza rozszerzenia
            fileExtension = fileExtension.ToLower(); // zamiana rozszerzenia na małe litery
        }
        Debug.Log(filePath2); // zwraca nam ścieżkę pliku w konsoli
    }

    IEnumerator UploadAudioFile()
    {
        using (UnityWebRequest getAudioClip = UnityWebRequestMultimedia.GetAudioClip(filePath2, AudioType.UNKNOWN)) // wysyłanie web requestu pobierającego multimedia z podanego adresu - podanym adresem jest ścieżka pliku
        {
            yield return getAudioClip.SendWebRequest(); // zwraca nam plik audio z adresu
            audioClipSelected = DownloadHandlerAudioClip.GetContent(getAudioClip); // pobiera plik audio i przypisuje do zmiennej Audio_Clip_Selected będącą Audio_Clipem
        }
        audioController.GetComponent<GetAudioAmplitude>().enabled = true;
        PlaySong(); // metoda odtwarzająca dźwięk
    }

    public void PlaySong()
    {
        audioController.GetComponent<Movement>().audioClipName = audioClipName;
        audioSource.clip = audioClipSelected; // przypisujemy nasz pobrany klip audio pod źródło dźwięku
        audioSource.Play(); // play audio
        dontDestroyOnLoadCamera.enabled = true; // aktywuje kamerę z góry
        cameraTwo.enabled = false; // upewnia się, że kamera podążająca za graczem jest wyłączona
        int objectCounterNumber = GameObject.Find("ObjectCounter").GetComponent<ObjectCounter>().objectNumber; // deklaracja zmiennej która przyjmuje wartość taką, jak ilość obiektów w scenie (zmienna objectNumber)
        audioController.name += objectCounterNumber; // dodaje do nazwy naszego audioControllera odpowiedni numer
        SceneManager.LoadScene("RenzlikAS", LoadSceneMode.Single); // przenosi nas do sceny głównej RenzlikAS
        for (int i = 1; i <= objectCounterNumber; i++)
        {
            GameObject notTheFirstAudioController = GameObject.Find("AudioController" + i); // znajduje n-ty kontroller audio
            Renderer color = notTheFirstAudioController.GetComponent<Renderer>(); // zmienna odpowiadająca za kolor materiału obiektu

            if (i == objectCounterNumber)
            {
                color.material.SetColor("_Color", Color.green); // każdy obiekt który nie jest obiektem pierwszym (aktualnie sterowanym) przyjmuje kolor czerwony
                notTheFirstAudioController.GetComponent<Movement>().enabled = true;  // dla każdego kolejnego obiektu wyłącza skrypt odpowiadający za ruch
            }
            else
            {
                color.material.SetColor("_Color", Color.red); // aktywny obiekt zmienia kolor na zielony
                notTheFirstAudioController.GetComponent<Movement>().enabled = false; // wyłącza ruch każdego obiektu oprócz najmłodszego 
            }
        }
        GameObject.Find("ObjectCounter").GetComponent<ObjectCounter>().selectedObject = objectCounterNumber; // aktywuje najmłodszy obiekt
    }

    void ExitGame()
    {
        UnityEngine.Application.Quit(); // kończy działanie aplikacji
    }
}