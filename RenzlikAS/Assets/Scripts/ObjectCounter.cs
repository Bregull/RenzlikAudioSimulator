using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectCounter : MonoBehaviour
{
    public int objectNumber = 1;
    string sceneName;
    int selectedObject;

    void Start()
    {
        DontDestroyOnLoad(this);
        SceneManager.LoadScene("File Browser", LoadSceneMode.Single);
        selectedObject = objectNumber;
    }

    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        if (sceneName == "RenzlikAS")
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                selectedObject -= 1;
                if (selectedObject == 0)
                {
                    selectedObject = objectNumber;
                }
                for (int i = 1; i <= objectNumber; i++)
                {
                    Debug.Log("kontroler nr " + i);
                    GameObject turnOff = GameObject.Find("AudioController" + i);
                    if (i != selectedObject)
                    {
                        turnOff.GetComponent<Movement>().enabled = false;
                    }
                    else
                    {
                        turnOff.GetComponent<Movement>().enabled = true;
                    }
                }
            }
        }
    }
}
