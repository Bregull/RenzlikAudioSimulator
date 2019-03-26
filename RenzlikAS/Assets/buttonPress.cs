using UnityEngine;
using UnityEngine.UI;  // obsługa guzika
using UnityEditor;  // Pozwala nam dodać EditorUtility, z pomocą którego otwieramy panel wyboru pliku, oraz panel błędu.

public class buttonPress : MonoBehaviour
{
    public GameObject Audio_Source;
    string File_Path, File_Extension;  // stringi deklarujące lokalizację, oraz rozszerzenie pliku audio
    public Button Choose_File;  // dodajemy przycisk do sceny
    


    void Start()
    {
        Choose_File.onClick.AddListener(On_Click); // button "czeka" na naciśnięcie przez użytkownika, oraz po naciśnięciu wykonuje metodę On_Click
    }

    void On_Click()
    {
        Select_File(); // instrukcje po naciśnięciu buttona
    }

    public void Select_File()
    {
        File_Path = EditorUtility.OpenFilePanel("Wybierz utwór", "", "");  // otwiera okno przeglądania plików, w nawiasach wpisujemy ("nazwa okna","lokalizacje(?)","rozszerzenia")
        File_Extension = File_Path.Substring(File_Path.IndexOf('.') + 1); // pozwala nam określić rozszerzenie pliku poprzez znalezienie kropki w nazwie pliku, oraz zwrócenie nam wszystkiego co się znajduje za nią
        while (File_Extension != "mp3") // pętla sprawdzająca rozszerzenie pliku
        {
            EditorUtility.DisplayDialog("Error", "Niepoprawne rozszerzenie\nWybierz jeszcze raz", "OK", ""); // error 
            File_Path = EditorUtility.OpenFilePanel("Wybierz utwór", "", ""); // ponowny wybór pliku
            File_Extension = File_Path.Substring(File_Path.IndexOf('.') + 1); // ponowna analiza rozszerzenia
        }
        Debug.Log(File_Path); // zwraca nam ścieżkę pliku w konsoli
    }
}