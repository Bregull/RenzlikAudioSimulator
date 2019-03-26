using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;  // obsługa guzika
using UnityEditor;  // Pozwala nam dodać EditorUtility, z pomocą którego otwieramy panel wyboru pliku, oraz panel błędu.

public class buttonPress : MonoBehaviour
{
    string File_Path, File_Extension;  // stringi deklarujące lokalizację, oraz rozszerzenie pliku audio
    public Button Choose_File;  // dodajemy przycisk do sceny
    public AudioClip naszaMuza;
    public AudioSource Audio_Source;
    


    void Start()
    {
        Choose_File.onClick.AddListener(On_Click); // button "czeka" na naciśnięcie przez użytkownika, oraz po naciśnięciu wykonuje metodę On_Click       
    }

    void On_Click()
    {
        Select_File(); // instrukcje po naciśnięciu buttona
        StartCoroutine(Upload_Audio_File());
    }

    public void Select_File()
    {
        File_Path = EditorUtility.OpenFilePanel("Wybierz utwór", "", "");  // otwiera okno przeglądania plików, w nawiasach wpisujemy ("nazwa okna","lokalizacje(?)","rozszerzenia")
        File_Extension = File_Path.Substring(File_Path.IndexOf('.') + 1); // pozwala nam określić rozszerzenie pliku poprzez znalezienie kropki w nazwie pliku, oraz zwrócenie nam wszystkiego co się znajduje za nią
        while (File_Extension != "ogg") // pętla sprawdzająca rozszerzenie pliku
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
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(File_Path, AudioType.OGGVORBIS))
        {
            yield return www.SendWebRequest();
            naszaMuza = DownloadHandlerAudioClip.GetContent(www);         
        }
        Play_Song();
    }

    public void Play_Song()
    {
        Audio_Source.clip = naszaMuza;
        Audio_Source.Play();
    }

}