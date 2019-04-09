using UnityEngine;
using System.Collections;
using UnityEngine.Networking;  // pozwala nam na streamowanie muzyki z komputera (lub URL - do sprawdzenia) do naszego projektu Unity
using UnityEngine.UI;  // obsługa guzika
using UnityEditor;  // Pozwala nam dodać EditorUtility, z pomocą którego otwieramy panel wyboru pliku, oraz panel błędu.
using UnityEngine.SceneManagement;
using SFB;


public class ButtonPress : MonoBehaviour
{
    string[] filePath;
    string filePath2, fileExtension;  // stringi deklarujące lokalizację, oraz rozszerzenie pliku audio
    public Button chooseFile;  // dodajemy przycisk do sceny
    public AudioClip audioClipSelected;
    public AudioSource audioSource;
    public GameObject audioController;
    public Camera dontDestroyOnLoadCamera;
    public Camera cameraTwo;

    void Start()

    {
        DontDestroyOnLoad(audioController);
        DontDestroyOnLoad(dontDestroyOnLoadCamera);
        chooseFile.onClick.AddListener(OnClick); // button "czeka" na naciśnięcie przez użytkownika, oraz po naciśnięciu wykonuje metodę On_Click       
    }


    void OnClick()
    {
        SelectFile(); // instrukcje po naciśnięciu buttona
        StartCoroutine(UploadAudioFile()); //metoda pobierająca oraz odtwarzająca wybraną ścieżkę audio
    }

    public void SelectFile()
    {
        filePath = StandaloneFileBrowser.OpenFilePanel("Wybierz utwór", "", "", false);  // otwiera okno przeglądania plików, w nawiasach wpisujemy ("nazwa okna","lokalizacje(?)","rozszerzenia")
        filePath2 = string.Concat(filePath);
        fileExtension = filePath2.Substring(filePath2.IndexOf('.') + 1); // pozwala nam określić rozszerzenie pliku poprzez znalezienie kropki w nazwie pliku, oraz zwrócenie nam wszystkiego co się znajduje za nią
        while (fileExtension != "wav" && fileExtension != "ogg") // pętla sprawdzająca rozszerzenie pliku -> DZIAŁA: Wav, Ogg; NIE DZIAŁA: Mp3, Flac
        {
            if (fileExtension == "")  // przerywa działanie pętli w momencie, gdy rozszerzenie pliku zwraca pustgo stringa.
            {
                break;
            }
           // StandaloneFileBrowser.DisplayDialog("Error", "Nieobsługiwane rozszerzenie\nWybierz jeszcze raz", "OK"); // error 
            filePath = StandaloneFileBrowser.OpenFilePanel("Wybierz utwór", "", "", false); // ponowny wybór pliku
            filePath2 = string.Concat(filePath);
            fileExtension = filePath2.Substring(filePath2.IndexOf('.') + 1); // ponowna analiza rozszerzenia
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
        PlaySong(); // metoda odtwarzająca dźwięk
    }

    public void PlaySong()
    {   
        audioSource.clip = audioClipSelected; // przypisujemy nasz pobrany klip audio pod źródło dźwięku
        audioSource.Play(); // play audio
        dontDestroyOnLoadCamera.enabled = true;
        cameraTwo.enabled = false;
        SceneManager.LoadScene("RenzlikAS", LoadSceneMode.Single);
    }
}
