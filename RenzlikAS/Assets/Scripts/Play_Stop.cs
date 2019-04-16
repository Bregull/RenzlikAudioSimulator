using UnityEngine.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play_Stop : MonoBehaviour
{
    public Button stopPlayButton;
    public Text buttonText;
    int objectNumber;
    GameObject audioController;
    Transform audioSource;
    bool stopState;

    void Start()
    {
        stopPlayButton.onClick.AddListener(StopPlay);
    }

    void Update()
    {
        objectNumber = GameObject.Find("ObjectCounter").GetComponent<ObjectCounter>().objectNumber;
    }

    void StopPlay()
    {
        if (stopState == false)
        {
            for (int i = 1; i <= objectNumber; i++)
            {
                audioController = GameObject.Find("AudioController" + i);
                audioSource = audioController.transform.GetChild(0);
                audioSource.GetComponent<AudioSource>().Stop();
            }
            stopState = true;
            buttonText.text = "PLAY";
        }
        else if (stopState == true)
        {
            for (int i = 1; i <= objectNumber; i++)
            {
                audioController = GameObject.Find("AudioController" + i);
                audioSource = audioController.transform.GetChild(0);
                audioSource.GetComponent<AudioSource>().Play();
            }
            stopState = false;
            buttonText.text = "STOP";
        }

    }
}
