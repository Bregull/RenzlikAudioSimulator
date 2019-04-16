using UnityEngine.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play_Stop : MonoBehaviour
{
    public Button play;
    public Button stop;
    int objectNumber;
    GameObject audioController;
    Transform audioSource;

    void Start()
    {
        play.onClick.AddListener(Play);
        stop.onClick.AddListener(Stop);

    }

    void Update()
    {
        objectNumber = GameObject.Find("ObjectCounter").GetComponent<ObjectCounter>().objectNumber;
    }

    void Play()
    {
        for (int i = 1; i <= objectNumber; i++)
        {
            audioController = GameObject.Find("AudioController" + i);
            audioSource = audioController.transform.GetChild(0);
            audioSource.GetComponent<AudioSource>().Play();
        }
    }


    void Stop()
    {
        for (int i = 1; i <= objectNumber; i++)
        {
            audioController = GameObject.Find("AudioController" + i);
            audioSource = audioController.transform.GetChild(0);
            audioSource.GetComponent<AudioSource>().Stop();
        }
    }
}
