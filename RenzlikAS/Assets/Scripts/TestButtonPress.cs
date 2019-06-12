using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class TestButtonPress : MonoBehaviour
{
    public Button testButton;
    int numberOfObjects;
    List<GameObject> gameObjects;

    // Start is called before the first frame update
    void Start()
    {
        testButton.onClick.AddListener(PerformTest);
    }

    // Update is called once per frame
    void Update()
    {
        numberOfObjects = GameObject.Find("ObjectCounter").GetComponent<ObjectCounter>().objectNumber;
    }

    void PerformTest()
    {

        string filePath = "Assets/Test/Test.txt";
        File.Delete(filePath);
        StreamWriter writer = new StreamWriter(filePath, true);
        string toWrite = "";

        gameObjects = new List<GameObject>(numberOfObjects);
        Debug.Log("TESTING");
        for(int i = 1; i <= numberOfObjects; i++)
        {
            GameObject controller = GameObject.Find("AudioController" + i);
            gameObjects.Add(controller);
        }

        foreach (GameObject controller in gameObjects)
        {
            toWrite += ($"File: {controller.GetComponent<Movement>().audioClipName}\nCoordinates: {controller.transform.position} \nDistance from Listener: {(int)(controller.GetComponent<Movement>().distanceFromListener / 17.88 * 100)}%\nCurrently at: " +
                $"{(int)controller.GetComponent<GetAudioAmplitude>().audioSource.time}s \n\n");
        }
        writer.Write(toWrite);
        writer.Close();
    }
}
