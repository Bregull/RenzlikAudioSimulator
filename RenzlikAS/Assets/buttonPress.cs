using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class buttonPress : MonoBehaviour
{
    public string path;
    public Button chooseFile;
     // Start is called before the first frame update
    void Start()
    {
        chooseFile.onClick.AddListener(onClick);
    }

    void onClick()
    {
        FileSelect();
        Debug.Log("click");
    }

    public void FileSelect()
    {
        path = EditorUtility.OpenFilePanel("Wybierz utwór", "", "mp3");
        print(path);
    }
}
