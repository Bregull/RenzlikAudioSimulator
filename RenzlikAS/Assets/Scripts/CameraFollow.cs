using UnityEngine;

public class CameraFollow : MonoBehaviour   
{
    private Camera myCamera ; 
    public Transform cameraAudioSource;
    public float smoothSpeed = 0.125f; // prędkość poruszania się kamery za obiektem
    public Vector3 offset; // pozwala na ustawienie offsetu, oddalenia od położenia kamery, robi się to w AudioController->Camera->Camera Follow(Script)->offset
    private void FixedUpdate() 
    {
        transform.position = cameraAudioSource.position+offset; // ciagle zczytuje pozycje kamery
    }
    private void Start()
    {
        myCamera = GetComponent<Camera>();
    }

}
