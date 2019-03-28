using UnityEngine;
using System.Collections;
using UnityEngine.Networking;  // pozwala nam na streamowanie muzyki z komputera (lub URL - do sprawdzenia) do naszego projektu Unity
using UnityEngine.UI;  // obsługa guzika
using UnityEditor;  // Pozwala nam dodać EditorUtility, z pomocą którego otwieramy panel wyboru pliku, oraz panel błędu.
using UnityEngine.SceneManagement;


public class buttonPress : MonoBehaviour
{
    string File_Path, File_Extension;  // stringi deklarujące lokalizację, oraz rozszerzenie pliku audio
    public Button Choose_File;  // dodajemy przycisk do sceny
    public AudioClip Audio_Clip_Selected;
    public AudioSource Audio_Source;
    public GameObject Audio_Controller;



    void Start()
    {
        DontDestroyOnLoad(Audio_Controller);
        Choose_File.onClick.AddListener(On_Click); // button "czeka" na naciśnięcie przez użytkownika, oraz po naciśnięciu wykonuje metodę On_Click       
    }

    void On_Click()
    {
        Select_File(); // instrukcje po naciśnięciu buttona
        StartCoroutine(Upload_Audio_File()); //metoda pobierająca oraz odtwarzająca wybraną ścieżkę audio
    }

    public void Select_File()
    {
        File_Path = EditorUtility.OpenFilePanel("Wybierz utwór", "", "");  // otwiera okno przeglądania plików, w nawiasach wpisujemy ("nazwa okna","lokalizacje(?)","rozszerzenia")
        File_Extension = File_Path.Substring(File_Path.IndexOf('.') + 1); // pozwala nam określić rozszerzenie pliku poprzez znalezienie kropki w nazwie pliku, oraz zwrócenie nam wszystkiego co się znajduje za nią
        while (File_Extension != "wav" && File_Extension != "ogg") // pętla sprawdzająca rozszerzenie pliku -> DZIAŁA: Wav, Ogg; NIE DZIAŁA: Mp3, Flac
        {
            if (File_Extension == "")  // przerywa działanie pętli w momencie, gdy rozszerzenie pliku zwraca pustgo stringa.
            {
                break;
            }
            EditorUtility.DisplayDialog("Error", "Niepoprawne rozszerzenie\nWybierz jeszcze raz", "OK", ""); // error 
            File_Path = EditorUtility.OpenFilePanel("Wybierz utwór", "", ""); // ponowny wybór pliku
            File_Extension = File_Path.Substring(File_Path.IndexOf('.') + 1); // ponowna analiza rozszerzenia
        }
        Debug.Log(File_Path); // zwraca nam ścieżkę pliku w konsoli
    }

    IEnumerator Upload_Audio_File()
    {
        using (UnityWebRequest Get_Audio_Clip = UnityWebRequestMultimedia.GetAudioClip(File_Path, AudioType.UNKNOWN)) // wysyłanie web requestu pobierającego multimedia z podanego adresu - podanym adresem jest ścieżka pliku
        {
            yield return Get_Audio_Clip.SendWebRequest(); // zwraca nam plik audio z adresu
            Audio_Clip_Selected = DownloadHandlerAudioClip.GetContent(Get_Audio_Clip); // pobiera plik audio i przypisuje do zmiennej Audio_Clip_Selected będącą Audio_Clipem
        }
        Play_Song(); // metoda odtwarzająca dźwięk
    }

    public void Play_Song()
    {   
        Audio_Source.clip = Audio_Clip_Selected; // przypisujemy nasz pobrany klip audio pod źródło dźwięku
        Audio_Source.Play(); // play audio
        SceneManager.LoadScene("RenzlikAS", LoadSceneMode.Single);
    }
}