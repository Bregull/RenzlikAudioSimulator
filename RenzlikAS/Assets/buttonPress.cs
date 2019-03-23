using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonPress : MonoBehaviour
{

    public Button chooseFile;
     // Start is called before the first frame update
    void Start()
    {
        chooseFile.onClick.AddListener(onClick);
    }

    void onClick()
    {
        Debug.Log("CLICK");
    }
}
