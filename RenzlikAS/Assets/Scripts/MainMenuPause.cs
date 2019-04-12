using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuPause : MonoBehaviour
{
    private static bool escPress = false;
    public GameObject pauseMenu;
    public Button chooseNewFile;
    public Button addFile;
    int numberOfObjects;

   
    void Start()
    {
        AudioListener.pause = false;
        numberOfObjects = GameObject.Find("ObjectCounter").GetComponent<ObjectCounter>().objectNumber;
        pauseMenu.SetActive(false);
        chooseNewFile.onClick.AddListener(OnClickNewFile);
        addFile.onClick.AddListener(OnClickAddFile);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (escPress == false)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    void OnClickNewFile()
    {
        escPress = false;
        Time.timeScale = 1f;
        GameObject audioController = GameObject.Find("AudioController" + numberOfObjects);
        Destroy(audioController);
        GameObject dontDestroyOnLoadCamera = GameObject.Find("DontDestroyOnLoadCamera");
        Destroy(dontDestroyOnLoadCamera);
        SceneManager.LoadScene("File Browser", LoadSceneMode.Single);
    }

    void OnClickAddFile()
    {
        escPress = false;
        Time.timeScale = 1f;
        GameObject dontDestroyOnLoadCamera = GameObject.Find("DontDestroyOnLoadCamera");
        Destroy(dontDestroyOnLoadCamera);
        SceneManager.LoadScene("File Browser", LoadSceneMode.Single);
        GameObject.Find("ObjectCounter").GetComponent<ObjectCounter>().objectNumber++;
    }

    void PauseGame()
    {
        AudioListener.pause = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        escPress = true;
    }

    void ResumeGame()
    {
        AudioListener.pause = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        escPress = false;
    }
}
