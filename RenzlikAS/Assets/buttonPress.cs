using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class buttonPress : MonoBehaviour
{
    public GameObject Audio_Source;
    string File_Path, File_Extension;
    public Button Choose_File;
    


    void Start()
    {
        Choose_File.onClick.AddListener(On_Click);
    }

    void On_Click()
    {
        Select_File();
        Debug.Log("click");
    }

    public void Select_File()
    {
        File_Path = EditorUtility.OpenFilePanel("Wybierz utwór", "", "");
        File_Extension = File_Path.Substring(File_Path.IndexOf('.') + 1);
        while (File_Extension != "mp3")
        {
            EditorUtility.DisplayDialog("Error", "Niepoprawne rozszerzenie\nWybierz jeszcze raz", "OK", "");
            File_Path = EditorUtility.OpenFilePanel("Wybierz utwór", "", "");
            File_Extension = File_Path.Substring(File_Path.IndexOf('.') + 1);
        }
        Debug.Log(File_Path);
    }
}