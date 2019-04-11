using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuPause : MonoBehaviour
{
    private static bool escPress = false;
    public GameObject pauseMenu;
    public Button chooseNewFile;

   
    void Start()
    {
        pauseMenu.SetActive(false);
        chooseNewFile.onClick.AddListener(OnClick);
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

    void OnClick()
    {
        escPress = false;
        Time.timeScale = 1f;
        escPress = false;
        GameObject AudioController = GameObject.Find("AudioController");
        Destroy(AudioController);
        GameObject DontDestroyOnLoadCamera = GameObject.Find("DontDestroyOnLoadCamera");
        Destroy(DontDestroyOnLoadCamera);
        SceneManager.LoadScene("File Browser", LoadSceneMode.Single);

    }

    void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        escPress = true;
    }

    void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        escPress = false;
    }
}
